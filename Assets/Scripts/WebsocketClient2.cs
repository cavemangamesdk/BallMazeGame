using System;
using CMG.BallMazeGame;
using CMG.BallMazeGame.Models;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class WebsocketClient2 : MonoBehaviour
{
    private WebSocket _ws;
    [SerializeField] private string _host;

    private void Start()
    {
        // Create a UDP client
        //UdpClient udpClient = new UdpClient(12345);

        // Enable broadcasting
        //udpClient.EnableBroadcast = true;

        // Receive the broadcasted message
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        //byte[] receivedBytes = udpClient.Receive(ref remoteEndPoint);
        //string message = Encoding.UTF8.GetString(receivedBytes);

        // Display the received message
        //Debug.Log("Received message: " + message);

        // ws://192.168.109.173:8765

        //_host = $"ws://{message}:8765";

        _ws = new WebSocket(_host);
        _ws.OnMessage += WsOnOnMessage;
        _ws.Connect();
    }

    private void WsOnOnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log($"Data: {e.Data}");
        var splitString = e.Data.Split(',');
        float[] dataSet = new float[6];
        for (int i = 0; i < splitString.Length; i++)
        {
            dataSet[i] = float.Parse(splitString[i]);
        }
        
        GameManager2.Instance.HandleInput(dataSet);
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
