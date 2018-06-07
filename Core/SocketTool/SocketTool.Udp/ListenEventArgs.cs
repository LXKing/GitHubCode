using System;
namespace SocketTool.Udp
{
	public class ListenEventArgs : EventArgs
	{
		public readonly bool Listening;
		public ListenEventArgs(bool islistening)
		{
			this.Listening = islistening;
		}
	}
}
