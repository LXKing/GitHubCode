using System;
namespace SocketTool.Core
{
	public interface IPacket
	{
		void Initialize(byte[] metadata);
		byte[] GetBytes();
	}
}
