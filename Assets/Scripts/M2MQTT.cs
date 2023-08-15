using System;
using System.Security.Authentication;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class MqttClientExample
{
    public static void Main(string[] args)
    {
        // MQTT broker details
        string host = "3c6ea0ec32f6404db6fd0439b0d000ce.s2.eu.hivemq.cloud";
        int port = 8883;
        string username = "mvp2023";
        string password = "wzq6h2hm%WLaMh$KYXj5";
        string client_id = "Client_4242";

        // Create a new MqttClient instance
        MqttClient client = new MqttClient(host, port, true, MqttSslProtocols.TLSv1_2);

        // Set MQTT client options
        client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
        client.ConnectionClosed += Client_ConnectionClosed;

        // Connect to the MQTT broker with credentials
        client.Connect(client_id, username, password);

        // Subscribe to all topics
        client.Subscribe(new string[] { "#" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

        // Wait for messages
        Console.WriteLine("Subscribed to all topics. Waiting for messages...");

        // Keep the application running
        Console.ReadLine();

        // Disconnect from the MQTT broker
        client.Disconnect();
    }

    private static void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        // Handle received message
        string topic = e.Topic;
        string message = Encoding.UTF8.GetString(e.Message);
        Console.WriteLine($"Received message on topic '{topic}': {message}");
    }

    private static void Client_ConnectionClosed(object sender, EventArgs e)
    {
        // Handle connection closed event
        Console.WriteLine("Connection to MQTT broker closed.");
    }
}
