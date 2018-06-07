using System;
namespace SocketTool.Udp
{
	public interface ICommand
	{
		string OPCode
		{
			get;
		}
		uint SequenceNum
		{
			get;
		}
		byte Flags
		{
			get;
		}
		short NumFields
		{
			get;
		}
		short[] FieldSizes
		{
			get;
		}
		string[] Fields
		{
			get;
		}
		string AllFields
		{
			get;
		}
	}
}
