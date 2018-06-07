using System;
namespace SocketTool.Udp
{
	internal class TimeoutEntry
	{
		public string ServerIP;
		public int ServerPort;
		public DateTime TimeoutTime;
		public string ConnectionRequestID;
	}
}
