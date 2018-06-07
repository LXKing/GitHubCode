using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace COMMON.Net
{
    /// <summary>
    /// IP地址工具类
    /// </summary>
    public class IPAddressHelper
    {
        /// <summary>
        /// 获取当前机器的IP(IP4)
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalMachineIP4Address()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

            foreach (IPAddress ip in ipEntry.AddressList)
            {
                //IPV4
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip;
            }

            return ipEntry.AddressList[0];
        }
        /// <summary>
        /// 获取当前机器的IP(IP6)
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalMachineIP6Address()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

            foreach (IPAddress ip in ipEntry.AddressList)
            {
                //IPV6
                if (ip.AddressFamily == AddressFamily.InterNetworkV6)
                    return ip;
            }

            return ipEntry.AddressList[0];
        }
    }
}
