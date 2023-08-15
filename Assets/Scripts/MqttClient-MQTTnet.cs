//using System;
//using System.Text;
//using System.Threading.Tasks;
//using MQTTnet;
//using MQTTnet.Client;
//using MQTTnet.Protocol;
//using MQTTnet;
//using MQTTnet.Client;
//using MQTTnet.Client.Options;
//using MQTTnet.Client.Subscribing;
//using MQTTnet.Extensions.ManagedClient;
//using MQTTnet.Formatter;
//using System.Security.Authentication;

//public class MqttClientExample
//{
//    private static Task HandleIncomingMessage(MqttApplicationMessageReceivedEventArgs e)
//    {
//        string topic = e.ApplicationMessage.Topic;
//        string message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

//        Console.WriteLine($"Received message on topic: {topic}");
//        Console.WriteLine($"Message: {message}");

//        return Task.CompletedTask;
//    }

//    public static async Task Main()
//    {
//        var factory = new MqttFactory();
//        var client = factory.CreateMqttClient();

//        var options = new MqttClientOptionsBuilder()
//            .WithTcpServer("3c6ea0ec32f6404db6fd0439b0d000ce.s2.eu.hivemq.cloud", 8883)
//            .WithCredentials("mvp2023", "wzq6h2hm%WLaMh$KYXj5")
//            .WithClientId(Guid.NewGuid().ToString())
//            .WithProtocolVersion(MqttProtocolVersion.V500)
//            .WithTls(new MqttClientOptionsBuilderTlsParameters()
//            {
//                UseTls = true,
//                SslProtocol = SslProtocols.Tls12
//            })
//            .Build();

//        client.UseApplicationMessageReceivedHandler(HandleIncomingMessage);

//        client.UseDisconnectedHandler(async e =>
//        {
//            Console.WriteLine("Disconnected from MQTT broker");
//            await Task.Delay(TimeSpan.FromSeconds(5));

//            try
//            {
//                await client.ConnectAsync(options); // Reconnect to the broker
//            }
//            catch
//            {
//                Console.WriteLine("Reconnect failed");
//            }
//        });

//        var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
//            .WithTopicFilter("#") // Subscribe to all topics by using wildcard '#'
//            .Build();

//        await client.SubscribeAsync(subscribeOptions);

//        await client.ConnectAsync(options);

//        Console.WriteLine("Connected to MQTT broker");

//        // Keep the application running until it is manually stopped
//        Console.ReadLine();
//    }
//}
