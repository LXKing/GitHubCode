using System;
using System.Net;
namespace SocketTool.Core
{
	public class ReceivedEventArgs : EventArgs
	{
		public readonly IPEndPoint RemoteHost;
		public readonly byte[] Data;
		public ReceivedEventArgs(IPEndPoint Remote, byte[] ReceivedData)
		{
			this.Data = ReceivedData;
			this.RemoteHost = Remote;
		}
	}
}
