using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class MotionControllerServer : MonoBehaviour
    {
        [SerializeField] private int _port;
        
        private UdpClient _listener;
        private IPEndPoint _endPoint;
        private AsyncCallback _asyncCallback;
        private object _object = null;
        private byte[] _receivedPacket;

        private void Start()
        {
            _endPoint = new IPEndPoint(IPAddress.Any, _port);
            _listener = new UdpClient();
            _listener.Client.Bind(_endPoint);
            _asyncCallback = new AsyncCallback(ReceiveData);
            _listener.BeginReceive(_asyncCallback,_object);
        }

        private void ReceiveData(IAsyncResult result)
        {
            _receivedPacket = _listener.EndReceive(result, ref _endPoint);
            ParsePacket();

            _listener.BeginReceive(_asyncCallback, _object);
        }

        private void ParsePacket()
        {
            var data = Encoding.ASCII.GetString(_receivedPacket).Split(',');
            //Debug.Log(data);
            float[] dataSet = new float[2];
            for (int i = 0; i < data.Length; i++)
            {
                dataSet[i] = float.Parse(data[i]);
            }
            
            GameManager.Instance.HandleInput(dataSet);
        }

        private void OnApplicationQuit()
        {
            _listener.Close();
        }
    }
}