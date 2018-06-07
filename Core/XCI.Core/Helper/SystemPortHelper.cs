using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace XCI.Helper
{
    /// <summary>
    /// 系统端口操作帮助类
    /// </summary>
    public static class SystemPortHelper
    {
        /// <summary> 
        /// 获取第一个可用的端口号 
        /// </summary>
        /// <param name="beginPort">开始端口</param>
        public static int GetFirstAvailablePort(int beginPort = 10000)
        {
            const int maxPort = 65535; //系统tcp/udp端口数最大是65535 

            for (int i = beginPort; i < maxPort; i++)
            {
                if (PortIsAvailable(i)) return i;
            }

            return -1;
        }

        /// <summary> 
        /// 获取操作系统已用的端口号 
        /// </summary> 
        public static int[] GetAllUsePort()
        {
            //获取本地计算机的网络连接和通信统计数据的信息 
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //返回本地计算机上的所有Tcp监听程序 
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();

            //返回本地计算机上的所有UDP监听程序 
            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

            //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。 
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            List<int> allPorts = new List<int>();
            foreach (IPEndPoint ep in ipsTCP) allPorts.Add(ep.Port);
            foreach (IPEndPoint ep in ipsUDP) allPorts.Add(ep.Port);
            foreach (TcpConnectionInformation conn in tcpConnInfoArray) allPorts.Add(conn.LocalEndPoint.Port);

            return allPorts.ToArray();
        }


        /// <summary> 
        /// 检查指定端口是否已用
        /// </summary> 
        /// <param name="port">测试的端口</param> 
        public static bool PortIsAvailable(int port)
        {
            var portUsed = GetAllUsePort();
            foreach (int p in portUsed)
            {
                if (p == port)
                {
                    return false;
                }
            }
            return true;
        }

    }
}