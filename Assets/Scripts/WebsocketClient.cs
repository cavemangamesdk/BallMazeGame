using System;
using CMG.BallMazeGame;
using CMG.BallMazeGame.Models;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;

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
        Debug.Log($"Data: {e.Data}");
        var splitString = e.Data.Split(',');
        float[] dataSet = new float[2];
        for (int i = 0; i < splitString.Length; i++)
        {
            dataSet[i] = float.Parse(splitString[i]);
        }
        
        GameManager.Instance.HandleInput(dataSet);
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

    private void OnApplicationQuit()
    {
        _ws.Close();
    }
}
