using System;
namespace SocketTool.Core
{
	public interface IClient
	{
		event ReceivedHandler OnDataReceived;
		event SocketErrorHandler OnSocketError;
		void Init(string serverIp, int port);
		void Send(byte[] data);
		void Close();
	}
}
