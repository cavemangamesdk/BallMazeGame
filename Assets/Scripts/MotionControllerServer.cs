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