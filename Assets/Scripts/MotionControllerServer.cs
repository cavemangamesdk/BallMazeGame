using System.Linq;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace CMG.BallMazeGame.Tester
{
    public class MotionControllerServer : MonoBehaviour
    {
        [SerializeField] private string _host;
        private WebSocketServer _ws;
        
        private void Start()
        {
            var hostname = Dns.GetHostName();

            var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList
                .First(ips => ips.AddressFamily == AddressFamily.InterNetwork)
                .ToString();

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
        protected override void OnMessage( MessageEventArgs e)
        {
            //TextTester.Instance.SetText(e.Data);
            Debug.Log($"Data: {e.Data}");
            var splitString = e.Data.Split(',');
            float[] dataSet = new float[3];
            for (int i = 0; i < splitString.Length; i++)
            {
                dataSet[i] = float.Parse(splitString[i]);
            }
        
            GameManager.Instance.HandleInput(dataSet);
        }
    }
}