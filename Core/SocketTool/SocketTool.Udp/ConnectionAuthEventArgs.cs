using System;
namespace SocketTool.Udp
{
	public class ConnectionAuthEventArgs : EventArgs
	{
		public bool AllowConnection;
		private string m_disallow;
		public readonly IConnection ClientConnection;
		public readonly ICommand AuthCommand;
		public string DisallowReason
		{
			get
			{
				return this.m_disallow;
			}
			set
			{
				this.m_disallow = value;
				if (this.m_disallow.Length > 200)
				{
					this.m_disallow = this.m_disallow.Substring(0, 200);
				}
			}
		}
		public ConnectionAuthEventArgs(IConnection conn, ICommand cmd)
		{
			this.AllowConnection = false;
			this.DisallowReason = "Rejected by remote host software.";
			this.ClientConnection = conn;
			this.AuthCommand = cmd;
		}
	}
}
