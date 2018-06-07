using System;
namespace SocketTool.Udp
{
	public class RequestTimedOutEventArgs : EventArgs
	{
		public readonly string ServerIP;
		public readonly int ServerPort;
		public readonly string RequestID;
		public RequestTimedOutEventArgs(string IP, int Port, string RID)
		{
			this.ServerIP = IP;
			this.ServerPort = Port;
			this.RequestID = RID;
		}
	}
}
