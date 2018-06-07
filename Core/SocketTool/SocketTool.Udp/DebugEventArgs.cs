using System;
namespace SocketTool.Udp
{
	public class DebugEventArgs : EventArgs
	{
		public readonly string DebugMessage;
		public DebugEventArgs(string DebugMsg)
		{
			this.DebugMessage = DebugMsg;
		}
	}
}
