using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace CMG.BallMazeGame
{
    internal static class NetworkUtils
    {
        internal static (string hostname, string ip) GetLocalHostNameAndIPAddress()
        {
            var hostname = Dns.GetHostName();
            string ip = null;

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                    ni.OperationalStatus == OperationalStatus.Up &&
                    !ni.Description.ToLowerInvariant().Contains("virtual") &&
                    !ni.Description.ToLowerInvariant().Contains("vEthernet") &&
                    !ni.Description.ToLowerInvariant().Contains("hyper-v"))
                {
                    foreach (UnicastIPAddressInformation ipInfo in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ipInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ip = ipInfo.Address.ToString();
                            break;
                        }
                    }
                }

                if (ip != null)
                {
                    break;
                }
            }

            return (hostname, ip);
        }


        //internal static (string hostname, string ip) GetLocalHostNameAndIPAddress()
        //{
        //    var hostname = Dns.GetHostName();

        //    var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList
        //        .First(ips => ips.AddressFamily == AddressFamily.InterNetwork)
        //        .ToString();

        //    return (hostname, ip);
        //}

        internal static string GetLocalIPAddress()
        {
            (var _, var ip) = GetLocalHostNameAndIPAddress();
            return ip;
        }
    }
}