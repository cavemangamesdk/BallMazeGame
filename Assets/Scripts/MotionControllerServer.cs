using System;
using System.Linq;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace CMG.BallMazeGame
{
    public class MotionControllerServer : MonoBehaviour
    {
        public Action<string, int> OnMulticastResponse;

        [SerializeField] private string _host;
        private WebSocketServer _ws;

        internal static IMulticastConnector MulticastConnector { get; private set; }

        private void Start()
        {
            MulticastConnector = GetComponent<IMulticastConnector>();

            var ip = NetworkUtils.GetLocalIPAddress();

            _host = $"ws://{ip}:80/";
            _ws = new WebSocketServer(_host);

            _ws.AddWebSocketService<MessageBehavior>("/MotionController");
            _ws.Start();
        }

        private void OnApplicationQuit()
        {
            _ws.Stop();
        }
    }

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