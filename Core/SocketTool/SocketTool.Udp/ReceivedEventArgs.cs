using System;
using System.Net;
namespace SocketTool.Udp
{
	public class ReceivedEventArgs : EventArgs
	{
		public readonly IPEndPoint RemoteHost;
		public readonly string Data;
		public ReceivedEventArgs(IPEndPoint Remote, string ReceivedData)
		{
			this.Data = ReceivedData;
			this.RemoteHost = Remote;
		}
	}
}
