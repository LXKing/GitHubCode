using System;
using System.Collections.Generic;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace XCI.Helper
{
    /// <summary>
    /// 网络操作帮助类
    /// </summary>
    public static class NetWorkHelper
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        [DllImport(@"wininet", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "InternetSetOption", CallingConvention = CallingConvention.StdCall)]
        private static extern bool InternetSetOption(int hInternet, int dmOption, IntPtr lpBuffer, int dwBufferLength);


        /// <summary>
        /// 检测是否有活动连接
        /// </summary>
        /// <returns>有活动连接 返回True</returns>
        public static bool IsConnected()
        {
            int i;
            bool state = InternetGetConnectedState(out i, 0);
            return state;
        }


        /// <summary>
        /// 是否能 Ping 通指定的主机
        /// </summary>
        /// <param name="ip">ip 地址或主机名或域名</param>
        /// <returns>true 通，false 不通</returns>
        public static bool Ping(string ip)
        {
            try
            {
                Ping p = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                const string data = "Test Data!";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                const int timeout = 5000; // Timeout 时间，单位：毫秒
                PingReply reply = p.Send(ip, timeout, buffer, options);
                if (reply != null && reply.Status == IPStatus.Success)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 获取所有网络适配器
        /// </summary>
        public static string[] GetAllNetworkAdapter()
        {
            List<string> allNetworkAdapte = new List<string>();
            ManagementObjectCollection moc = WMIHelper.GetObjectCollection(WMIPath.Win32_NetworkAdapterConfiguration);
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"])
                {
                    allNetworkAdapte.Add(mo["Caption"].ToString());
                }
            }
            return allNetworkAdapte.ToArray();
        }


        /// <summary>
        /// 根据适配器名称获取适配器对象
        /// </summary>
        /// <param name="name">适配器名称</param>
        public static ManagementObject GetNetworkAdapterByName(string name)
        {
            ManagementObjectCollection moc = WMIHelper.GetObjectCollection(WMIPath.Win32_NetworkAdapterConfiguration);
            foreach (ManagementObject mo in moc)
            {
                if (mo["Caption"].ToString().Equals(name))
                {
                    return mo;
                }
            }
            return null;
        }


        /// <summary>
        /// 设置自动获取Dns服务器 
        /// </summary>
        /// <param name="name">适配器名称</param>
        public static void SetAutoGetDnsServer(string name)
        {
            ManagementObject networkAdapter = GetNetworkAdapterByName(name);
            ManagementBaseObject parDNSRegistration = networkAdapter.GetMethodParameters("SetDynamicDNSRegistration");//自动获取DNS

            parDNSRegistration["FullDNSRegistrationEnabled"] = false;
            parDNSRegistration["DomainDNSRegistrationEnabled"] = false;

            networkAdapter.InvokeMethod("SetDynamicDNSRegistration", parDNSRegistration, null);
        }


        /// <summary>
        /// 设置自动获取IP地址
        /// </summary>
        /// <param name="name">适配器名称</param>
        public static void SetAutoGetIPAdress(string name)
        {
            ManagementObject networkAdapter = GetNetworkAdapterByName(name);
            networkAdapter.InvokeMethod("EnableDHCP", null, null);//自动获取IP
        }


        /// <summary>
        /// 设置IP地址(可以设置多个)
        /// </summary>
        /// <param name="name">适配器名称</param>
        /// <param name="ipAddress">IP地址数组</param>
        /// <param name="subNetMask">子网掩码数组</param>
        /// <param name="defaultIPGateway">默认网关数组</param>
        public static void SetIPAdress(string name, string[] ipAddress, string[] subNetMask, string[] defaultIPGateway)
        {
            ManagementObject networkAdapter = GetNetworkAdapterByName(name);
            ManagementBaseObject parIPSetting = networkAdapter.GetMethodParameters("EnableStatic");//静态IP
            parIPSetting["IPAddress"] = ipAddress;
            parIPSetting["SubnetMask"] = subNetMask;
            networkAdapter.InvokeMethod("EnableStatic", parIPSetting, null);

            ManagementBaseObject parSetGateways = networkAdapter.GetMethodParameters("SetGateways");//默认网关
            parSetGateways["DefaultIPGateway"] = defaultIPGateway;
            networkAdapter.InvokeMethod("SetGateways", parSetGateways, null);
        }


        /// <summary>
        /// 设置Dns服务器(可以设置多个)
        /// </summary>
        /// <param name="name">适配器名称</param>
        /// <param name="dsnServer">Dns服务器数组</param>
        public static void SetDsnServer(string name, string[] dsnServer)
        {
            ManagementObject networkAdapter = GetNetworkAdapterByName(name);
            ManagementBaseObject parDNSRegistration = networkAdapter.GetMethodParameters("SetDynamicDNSRegistration");

            parDNSRegistration["FullDNSRegistrationEnabled"] = true;
            parDNSRegistration["DomainDNSRegistrationEnabled"] = false;

            networkAdapter.InvokeMethod("SetDynamicDNSRegistration", parDNSRegistration, null);

            ManagementBaseObject parsDsnServer = networkAdapter.GetMethodParameters("SetDNSServerSearchOrder");
            parsDsnServer["DNSServerSearchOrder"] = dsnServer;
            networkAdapter.InvokeMethod("SetDNSServerSearchOrder", parsDsnServer, null);
        }


        /// <summary>
        /// 设置IE代理服务器
        /// </summary>
        /// <param name="isProxyEnable">是否启用代理</param>
        /// <param name="proxyServer">代理服务器</param>
        public static void SetIEProxy(bool isProxyEnable, string proxyServer)
        {
            RegistryKey regKey = Registry.CurrentUser;
            const string subKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";
            RegistryKey optionKey = regKey.OpenSubKey(subKeyPath, true);
            if (optionKey != null)
            {
                optionKey.SetValue("ProxyEnable", (isProxyEnable ? 1 : 0));
                if (isProxyEnable)
                    optionKey.SetValue("ProxyServer", proxyServer);
                regKey.Close();
                //激活代理设置
                InternetSetOption(0, 39, IntPtr.Zero, 0);
                InternetSetOption(0, 37, IntPtr.Zero, 0);
            }
        }


        /// <summary>
        /// 获取客户机IP地址
        /// </summary>
        public static string[] GetIP()
        {
            IPHostEntry ips = Dns.GetHostEntry(Dns.GetHostName());
            List<string> list = new List<string>();
            if (ips.AddressList.Length > 0)
            {
                foreach (IPAddress address in ips.AddressList)
                {
                    if (address.AddressFamily.ToString().Equals("InterNetwork"))
                    {
                        list.Add(address.ToString());
                    }
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 获取本机Mac
        /// </summary>
        /// <returns>返回获取本机Mac</returns>
        public static string GetLocalMac()
        {
            var mac = "00-00-00-00-00-00";
            try
            {
                ManagementObjectCollection moc2 = WMIHelper.GetObjectCollection(WMIPath.Win32_NetworkAdapterConfiguration);
                foreach (ManagementObject mo in moc2)
                {
                    if (Convert.ToBoolean(mo["IPEnabled"]))
                    {
                        mac = mo["MacAddress"].ToString();
                        mo.Dispose();
                        return mac;
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                
            }
            finally
            {

            }
            return mac;
        }
    }
}