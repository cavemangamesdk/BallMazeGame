using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace CMG.BallMazeGame.Tester
{
    internal static class NetworkUtils
    {
        internal static (string hostname, string ip) GetLocalHostNameAndIPAddress()
        {
            var hostname = Dns.GetHostName();

            var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList
                .First(ips => ips.AddressFamily == AddressFamily.InterNetwork)
                .ToString();

            return (hostname, ip);
        }

        internal static string GetLocalIPAddress()
        {
            (var _, var ip) = GetLocalHostNameAndIPAddress();
            return ip;
        }
    }
}