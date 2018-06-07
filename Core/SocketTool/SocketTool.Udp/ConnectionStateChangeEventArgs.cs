using System;
namespace SocketTool.Udp
{
	public class ConnectionStateChangeEventArgs : EventArgs
	{
		public readonly IConnection Connection;
		public readonly bool Connected;
		public readonly string Disconnect_Reason;
		public ConnectionStateChangeEventArgs(IConnection cn, bool ced, string reason)
		{
			this.Connection = cn;
			this.Connected = ced;
			this.Disconnect_Reason = reason;
		}
	}
}
