using System;
using System.Net;
namespace SocketTool.Udp
{
	public interface IConnection
	{
		string EncryptionKey
		{
			get;
		}
		string RequestID
		{
			get;
		}
		IPEndPoint RemoteEP
		{
			get;
		}
		uint LastSentPacket
		{
			get;
		}
		uint LastReceivedPacket
		{
			get;
		}
		uint LastSentPacketR
		{
			get;
		}
		uint LastReceivedPacketR
		{
			get;
		}
		uint LastReceivedPacketSeq
		{
			get;
		}
		bool Authed
		{
			get;
		}
		bool Server
		{
			get;
		}
		DateTime TimeoutTime
		{
			get;
		}
		object UserObject
		{
			get;
			set;
		}
		int SendUnreliableCommand(byte flags, string opcode, string[] fields);
		int SendReliableCommand(byte flags, string opcode, string[] fields);
	}
}
