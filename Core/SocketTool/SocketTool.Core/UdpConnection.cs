using log4net;
using System;
using System.Net;
namespace SocketTool.Core
{
	public class UdpConnection : IConnection
	{
		private static ILog logger = LogManager.GetLogger(typeof(TcpConnection));
		public string ID
		{
			get;
			set;
		}
		public DateTime CreateDate
		{
			get;
			set;
		}
		public DateTime OnlineDate
		{
			get;
			set;
		}
		public IPEndPoint ClientIP
		{
			get;
			set;
		}
		public UdpConnection()
		{
			this.CreateDate = DateTime.Now;
			this.OnlineDate = DateTime.Now;
		}
		public void Send(byte[] data, int length)
		{
		}
	}
}
