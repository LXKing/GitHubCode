using System;
using System.Collections.Generic;
namespace SocketTool.Core
{
	public interface IServer
	{
		event ReceivedHandler OnDataReceived;
		event SocketErrorHandler OnSocketError;
		void Init(string serverIp, int port);
		void Send(string connId, byte[] data, int length);
		int Listen();
		List<IConnection> GetConnectionList();
		void Close();
	}
}
