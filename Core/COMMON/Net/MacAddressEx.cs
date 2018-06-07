using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
namespace COMMON.Net
{
    public class MacAddressEx
    {
        public static string GetHostMac()
        {
            string mac = string.Empty;
            ManagementObjectSearcher nisc = new ManagementObjectSearcher("select * from Win32_NetworkAdapterConfiguration");//"select * from Win32_NetworkAdapterConfiguration"
            foreach (ManagementObject nic in nisc.Get())
            {
                if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                {
                    mac =  nic["MACAddress"].ToString();
                }
            }
            return mac;
        }
    }
}
