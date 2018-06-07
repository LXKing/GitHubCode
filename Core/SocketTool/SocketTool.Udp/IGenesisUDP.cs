using System;
namespace SocketTool.Udp
{
	public interface IGenesisUDP
	{
		event DebugHandler OnDebugMessage;
		event ReceivedHandler OnDataReceived;
		event ListenHandler OnListenStateChanged;
		event SocketErrorHandler OnSocketError;
		event IncomingCommandHandler OnConnectionlessCommand;
		event ConnectionAuthHandler OnConnectionAuth;
		event SendLoginHandler OnLoginRequested;
		event IncomingCommandHandler OnCommandReceived;
		event ConnectionStateChangeHandler OnConnectionStateChanged;
		event AuthenticatedHandler OnAuthFeedback;
		event RequestTimedOutHandler OnConnectionRequestTimedOut;
		int State
		{
			get;
		}
		bool Encrypt
		{
			get;
			set;
		}
		int StartListen(string IP, int Port);
		int StopListen();
		int SendConnectionlessCommand(string IP, int Port, string opcode, string[] fields);
		int SendUnreliableCommandToAll(BroadcastFilter filter, byte flags, string opcode, string[] fields);
		int SendReliableCommandToAll(BroadcastFilter filter, byte flags, string opcode, string[] fields);
		int RequestConnect(ref string RemoteIP, int RemotePort, out string request_id);
		int CancelConnect(string RemoteIP, int RemotePort);
		int CancelConnect(string RequestID);
		int ConnectionByIPPort(string IP, int Port, out IConnection conn);
		int RemoveConnection(IConnection conn, bool send_disconnect_packet, string reason);
		int GetConnections(bool servers, out IConnection[] found);
		string[] GetLocalAddresses();
	}
}
