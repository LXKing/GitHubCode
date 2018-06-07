using System;
namespace SocketTool.Core
{
	public class SocketEventArgs : EventArgs
	{
		public readonly int ErrorCode;
		public readonly string Message;
		public SocketEventArgs(int code, string msg)
		{
			this.ErrorCode = code;
			this.Message = msg;
		}
	}
}
