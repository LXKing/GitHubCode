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
    /// �������������
    /// </summary>
    public static class NetWorkHelper
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        [DllImport(@"wininet", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "InternetSetOption", CallingConvention = CallingConvention.StdCall)]
        private static extern bool InternetSetOption(int hInternet, int dmOption, IntPtr lpBuffer, int dwBufferLength);


        /// <summary>
        /// ����Ƿ��л����
        /// </summary>
        /// <returns>�л���� ����True</returns>
        public static bool IsConnected()
        {
            int i;
            bool state = InternetGetConnectedState(out i, 0);
            return state;
        }


        /// <summary>
        /// �Ƿ��� Ping ָͨ��������
        /// </summary>
        /// <param name="ip">ip ��ַ��������������</param>
        /// <returns>true ͨ��false ��ͨ</returns>
        public static bool Ping(string ip)
        {
            try
            {
                Ping p = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                const string data = "Test Data!";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                const int timeout = 5000; // Timeout ʱ�䣬��λ������
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
        /// ��ȡ��������������
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
        /// �������������ƻ�ȡ����������
        /// </summary>
        /// <param name="name">����������</param>
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
        /// �����Զ���ȡDns������ 
        /// </summary>
        /// <param name="name">����������</param>
        public static void SetAutoGetDnsServer(string name)
        {
            ManagementObject networkAdapter = GetNetworkAdapterByName(name);
            ManagementBaseObject parDNSRegistration = networkAdapter.GetMethodParameters("SetDynamicDNSRegistration");//�Զ���ȡDNS

            parDNSRegistration["FullDNSRegistrationEnabled"] = false;
            parDNSRegistration["DomainDNSRegistrationEnabled"] = false;

            networkAdapter.InvokeMethod("SetDynamicDNSRegistration", parDNSRegistration, null);
        }


        /// <summary>
        /// �����Զ���ȡIP��ַ
        /// </summary>
        /// <param name="name">����������</param>
        public static void SetAutoGetIPAdress(string name)
        {
            ManagementObject networkAdapter = GetNetworkAdapterByName(name);
            networkAdapter.InvokeMethod("EnableDHCP", null, null);//�Զ���ȡIP
        }


        /// <summary>
        /// ����IP��ַ(�������ö��)
        /// </summary>
        /// <param name="name">����������</param>
        /// <param name="ipAddress">IP��ַ����</param>
        /// <param name="subNetMask">������������</param>
        /// <param name="defaultIPGateway">Ĭ����������</param>
        public static void SetIPAdress(string name, string[] ipAddress, string[] subNetMask, string[] defaultIPGateway)
        {
            ManagementObject networkAdapter = GetNetworkAdapterByName(name);
            ManagementBaseObject parIPSetting = networkAdapter.GetMethodParameters("EnableStatic");//��̬IP
            parIPSetting["IPAddress"] = ipAddress;
            parIPSetting["SubnetMask"] = subNetMask;
            networkAdapter.InvokeMethod("EnableStatic", parIPSetting, null);

            ManagementBaseObject parSetGateways = networkAdapter.GetMethodParameters("SetGateways");//Ĭ������
            parSetGateways["DefaultIPGateway"] = defaultIPGateway;
            networkAdapter.InvokeMethod("SetGateways", parSetGateways, null);
        }


        /// <summary>
        /// ����Dns������(�������ö��)
        /// </summary>
        /// <param name="name">����������</param>
        /// <param name="dsnServer">Dns����������</param>
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
        /// ����IE���������
        /// </summary>
        /// <param name="isProxyEnable">�Ƿ����ô���</param>
        /// <param name="proxyServer">���������</param>
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
                //�����������
                InternetSetOption(0, 39, IntPtr.Zero, 0);
                InternetSetOption(0, 37, IntPtr.Zero, 0);
            }
        }


        /// <summary>
        /// ��ȡ�ͻ���IP��ַ
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
        /// ��ȡ����Mac
        /// </summary>
        /// <returns>���ػ�ȡ����Mac</returns>
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