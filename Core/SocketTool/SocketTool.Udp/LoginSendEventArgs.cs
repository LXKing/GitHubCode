using System;
namespace SocketTool.Udp
{
	public class LoginSendEventArgs : EventArgs
	{
		public readonly IConnection ServerConnection;
		public bool Connected;
		public string Reason;
		public LoginSendEventArgs(IConnection cn, bool cd, string r)
		{
			this.ServerConnection = cn;
			this.Connected = cd;
			this.Reason = r;
		}
	}
}
