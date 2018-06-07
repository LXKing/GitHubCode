using System;
namespace SocketTool.Udp
{
	public class InterfaceFactory
	{
		public static IGenesisUDP CreateGenesisUDP(string Name)
		{
			return new CommonUdp(Name);
		}
	}
}
