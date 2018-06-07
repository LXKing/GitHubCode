using System;
namespace SocketTool
{
	[Flags]
	public enum BroadcastFilter
	{
		None = 0,
		Servers = 1,
		Clients = 2,
		All = 3,
		AuthedOnly = 4
	}
}
