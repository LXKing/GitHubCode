using System;
using System.Net;
namespace SocketTool.Udp
{
	public class CommandEventArgs : EventArgs
	{
		public readonly IConnection Sender;
		public readonly ICommand SentCommand;
		public readonly IPEndPoint SenderEndPoint;
		public CommandEventArgs(ICommand c, IConnection cn, IPEndPoint rep)
		{
			this.SentCommand = c;
			this.Sender = cn;
			this.SenderEndPoint = rep;
		}
	}
}
