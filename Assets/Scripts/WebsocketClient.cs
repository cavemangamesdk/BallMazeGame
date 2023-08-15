using System;
using UnityEngine;
using WebSocketSharp;

public class WebsocketClient : MonoBehaviour
{
    private WebSocket _ws;
    [SerializeField] private string _host;

    private void Start()
    {
        _ws = new WebSocket(_host);
        _ws.OnMessage += WsOnOnMessage;
        _ws.Connect();
    }

    private void WsOnOnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log($"Message from: {((WebSocket)sender)}, Data: {e}");
    }

    private void Update()
    {
        if (_ws == null)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            _ws.Send("Hello Michael!");
        }
    }
    
    
}
