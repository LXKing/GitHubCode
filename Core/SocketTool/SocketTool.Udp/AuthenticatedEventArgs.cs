using System;
namespace SocketTool.Udp
{
	public class AuthenticatedEventArgs : EventArgs
	{
		public readonly IConnection ServerConnection;
		public readonly bool Accepted;
		public readonly string Reason;
		public AuthenticatedEventArgs(IConnection cn, bool a, string r)
		{
			this.ServerConnection = cn;
			this.Accepted = a;
			this.Reason = r;
		}
	}
}
