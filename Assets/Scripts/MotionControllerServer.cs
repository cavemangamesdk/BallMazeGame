using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class MotionControllerServer : MonoBehaviour
    {
        public event Action OnJoystickPressed ;
        
        [SerializeField] private int _port;

        public string JoystickState = "";
        
        private UdpClient _orientationListener;
        private UdpClient _joystickListener;

        private IPEndPoint _orientationEndPoint;
        private IPEndPoint _joystickEndPoint;

        private AsyncCallback _orientationAsyncCallback;
        private AsyncCallback _joystickAsyncCallback;

        private byte[] _orientationPacket;
        private byte[] _joystickPacket;

        private object _object = null;
        private void Start()
        {
            _orientationEndPoint = new IPEndPoint(IPAddress.Any, _port);
            _joystickEndPoint = new IPEndPoint(IPAddress.Any, _port+1);

            _orientationListener = new UdpClient();
            _joystickListener = new UdpClient();

            _orientationListener.Client.Bind(_orientationEndPoint);
            _joystickListener.Client.Bind(_joystickEndPoint);

            _orientationAsyncCallback = new AsyncCallback(ReceiveOrientationData);
            _joystickAsyncCallback = new AsyncCallback(ReceiveJoystickData);

            _orientationListener.BeginReceive(_orientationAsyncCallback,_object);
            _joystickListener.BeginReceive(_joystickAsyncCallback,_object);
        }

        private void ReceiveOrientationData(IAsyncResult result)
        {
            _orientationPacket = _orientationListener.EndReceive(result, ref _orientationEndPoint);
            ParseOrientationPacket();

            _orientationListener.BeginReceive(_orientationAsyncCallback, _object);
        }

        private void ReceiveJoystickData(IAsyncResult result)
        {
            _joystickPacket = _joystickListener.EndReceive(result, ref _joystickEndPoint);
            ParseJoystickPacket();

            _joystickListener.BeginReceive(_joystickAsyncCallback, _object);
        }
        
        private void ParseOrientationPacket()
        {
            var data = Encoding.ASCII.GetString(_orientationPacket).Split(',');
            //Debug.Log(data);
            float[] dataSet = new float[2];
            for (int i = 0; i < data.Length; i++)
            {
                dataSet[i] = float.Parse(data[i]);
            }
            
            GameManager.Instance.HandleInput(dataSet);
        }
        
        private void ParseJoystickPacket()
        {
            if (GameManager.Instance.GameState == GameState.GameRunning) return;

            var data = Encoding.ASCII.GetString(_joystickPacket).Split(',');

            var state = data[1].ToLower();

            Debug.Log(state);
            
            JoystickState = state;
        }

        private void OnApplicationQuit()
        {
            _orientationListener.Close();
            _joystickListener.Close();
        }
    }
}