using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace CMG.BallMazeGame
{
    internal interface IMulticastConnector
    {
        void Shutdown();
    }

    [DisallowMultipleComponent]
    internal sealed class SocketMulticastConnector : MonoBehaviour, IMulticastConnector
    {
        [Header("Multicast Settings")]
        [SerializeField] private string _multicastGroup = "224.1.1.1";
        [SerializeField] private int _multicastPort = 58008;

        private Socket _socket;
        private Thread _multicastSenderThread;

        private void Start()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            var multicastGroupIPAddress = IPAddress.Parse(_multicastGroup);

            // Join multicast group
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastGroupIPAddress));

            // TTL
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);

            // Create an endpoint
            IPEndPoint ipep = new(multicastGroupIPAddress, _multicastPort);

            _multicastSenderThread = new Thread(OnMulticastSenderThreadFunc);

            // Connect to the endpoint
            _socket.Connect(ipep);

            _multicastSenderThread.Start();
        }

        private void OnApplicationQuit()
        {
            Shutdown();
        }

        private void OnMulticastSenderThreadFunc()
        {
            var ip = NetworkUtils.GetLocalIPAddress();

            lock (_socket)
            {
                while (_socket.Connected)
                {
                    var buffer = Encoding.ASCII.GetBytes(ip);
                    _socket.Send(buffer, buffer.Length, SocketFlags.None);

                    Thread.Sleep(1000);
                }
            }
        }

        public void Shutdown()
        {
            _multicastSenderThread.Abort();

            _socket.Close();
            _socket.Dispose();
        }
    }
}