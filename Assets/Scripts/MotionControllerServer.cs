using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace CMG.BallMazeGame
{
    public class MotionControllerServer : MonoBehaviour
    {
        public Action<string, int> OnMulticastResponse;
        internal static IMulticastConnector MulticastConnector { get; private set; }
        private WebSocketServer _ws;
        
        [SerializeField] private int _port;
        
        private UdpClient _listener;
        private IPEndPoint _endPoint;
        private AsyncCallback _asyncCallback;
        private object _object = null;
        private byte[] _receivedPacket;
        
        public float[] Inputs = new float[2];

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
        
        // private void Start()
        // {
        //     MulticastConnector = GetComponent<IMulticastConnector>();
        //
        //     var ip = NetworkUtils.GetLocalIPAddress();
        //
        //     _host = $"ws://{ip}:80/";
        //     _ws = new WebSocketServer(_host);
        //
        //     _ws.AddWebSocketService<MessageBehavior>("/MotionController");
        //     _ws.Start();
        // }
        //
        // private void OnApplicationQuit()
        // {
        //     _ws.Stop();
        // }
    }

    //Used by websocket...
    public class MessageBehavior : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            MotionControllerServer.MulticastConnector.Shutdown();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            //TextTester.Instance.SetText(e.Data);
            Debug.Log($"Data: {e.Data}");
            var splitString = e.Data.Split(',');
            float[] dataSet = new float[2];
            for (int i = 0; i < splitString.Length; i++)
            {
                dataSet[i] = float.Parse(splitString[i]);
            }

            GameManager.Instance.HandleInput(dataSet);
        }
    }
}