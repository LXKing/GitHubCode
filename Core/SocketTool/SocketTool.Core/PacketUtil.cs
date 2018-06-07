using System;
using System.IO;
using System.Net;
using System.Text;
namespace SocketTool.Core
{
	public class PacketUtil
	{
		protected BinaryWriter binWriter;
		private static int PacketLength = 1024;
		public int TotalPacketLength
		{
			get;
			set;
		}
		public byte[] PacketBytes
		{
			get;
			set;
		}
		public BinaryWriter beginWriteContent()
		{
			MemoryStream output = new MemoryStream(this.PacketBytes);
			this.binWriter = new BinaryWriter(output);
			return this.binWriter;
		}
		public void Write(byte[] data, int len)
		{
			this.TotalPacketLength += len;
			this.binWriter.Write(data, 0, len);
		}
		public int Write(string str, int length)
		{
			int i = 0;
			if (str != null)
			{
				if (str.Length > length)
				{
					str = str.Substring(0, length);
				}
				i = this.Write(str);
			}
			while (i < length)
			{
				this.Write(0);
				i++;
			}
			return length;
		}
		public void Write(int data)
		{
			data = IPAddress.HostToNetworkOrder(data);
			this.binWriter.Write(data);
			this.TotalPacketLength += 4;
		}
		public void Write(short data)
		{
			data = IPAddress.HostToNetworkOrder(data);
			this.binWriter.Write(data);
			this.TotalPacketLength += 2;
		}
		public void Write(uint data)
		{
			byte[] bytes = BitConverter.GetBytes(data);
			this.binWriter.Write(bytes);
			this.TotalPacketLength += 4;
		}
		public int Write(string str)
		{
			byte[] bytes = Encoding.Default.GetBytes(str);
			int num = bytes.Length;
			this.binWriter.Write(bytes);
			this.TotalPacketLength += num;
			return num;
		}
		public void Write(byte b)
		{
			this.binWriter.Write(b);
			this.TotalPacketLength++;
		}
		public int WriteAscII(string str)
		{
			char[] array = str.ToCharArray();
			int num = array.Length;
			this.binWriter.Write(array);
			this.TotalPacketLength += num;
			return num;
		}
		public void Write(DateTime d)
		{
			this.Write((short)d.Year);
			this.binWriter.Write((byte)d.Month);
			this.binWriter.Write((byte)d.Day);
			this.binWriter.Write((byte)d.Hour);
			this.binWriter.Write((byte)d.Minute);
			this.binWriter.Write((byte)d.Second);
			this.TotalPacketLength += 7;
		}
	}
}
