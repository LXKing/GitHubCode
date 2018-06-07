using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
namespace SocketTool.Core
{
	public class NetTest
	{
		public static bool DnsTest(string websiteUrl)
		{
			bool result;
			try
			{
				Dns.GetHostByName(websiteUrl);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
		public bool PingTest(string Ip)
		{
			Ping ping = new Ping();
			PingReply pingReply = ping.Send(IPAddress.Parse(Ip), 1000);
			return pingReply.Status == IPStatus.Success;
		}
		public static bool TcpSocketTest(string websiteUrl)
		{
			bool result;
			try
			{
				TcpClient tcpClient = new TcpClient(websiteUrl, 80);
				tcpClient.Close();
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
		public static bool WebRequestTest(string websiteUrl)
		{
			try
			{
				WebRequest webRequest = WebRequest.Create(websiteUrl);
				webRequest.GetResponse();
			}
			catch (WebException)
			{
				return false;
			}
			return true;
		}
	}
}
