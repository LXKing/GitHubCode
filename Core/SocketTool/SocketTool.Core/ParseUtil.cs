using System;
using System.IO;
using System.Net;
using System.Text;
namespace SocketTool.Core
{
	public class ParseUtil
	{
		protected BinaryReader reader;
		public BinaryReader newReader(byte[] byBuffer, int nReceived)
		{
			MemoryStream input = new MemoryStream(byBuffer, 0, nReceived);
			this.reader = new BinaryReader(input);
			return this.reader;
		}
		public byte Parse()
		{
			return this.reader.ReadByte();
		}
		public DateTime ParseDateTime()
		{
			int year = this.reader.ReadInt32();
			byte month = this.reader.ReadByte();
			byte day = this.reader.ReadByte();
			byte hour = this.reader.ReadByte();
			byte minute = this.reader.ReadByte();
			byte second = this.reader.ReadByte();
			return new DateTime(year, (int)month, (int)day, (int)hour, (int)minute, (int)second);
		}
		public uint ParseUInt32()
		{
			byte[] array = this.reader.ReadBytes(4);
			Array.Reverse(array);
			return BitConverter.ToUInt32(array, 0);
		}
		public int ParseInt32()
		{
			int network = this.reader.ReadInt32();
			return IPAddress.NetworkToHostOrder(network);
		}
		public short ParseInt16()
		{
			short network = this.reader.ReadInt16();
			return IPAddress.NetworkToHostOrder(network);
		}
		public int ParseIntEx()
		{
			return this.reader.ReadInt32();
		}
		public string ParseString(int len)
		{
			byte[] bytes = this.reader.ReadBytes(len);
			return ParseUtil.ParseString(bytes, len);
		}
		public static string ParseString(byte[] bytes, int len)
		{
			if (bytes[0] == 0)
			{
				return "";
			}
			string @string = Encoding.Default.GetString(bytes);
			int num = @string.IndexOf('\0');
			if (num > 0)
			{
				return @string.Substring(0, num);
			}
			return @string;
		}

        public static string ParseStringByUTF8(byte[] bytes, int len)
        {
            if (bytes[0] == 0)
            {
                return "";
            }
            string @string = Encoding.UTF8.GetString(bytes);
            int num = @string.IndexOf('\0');
            if (num > 0)
            {
                return @string.Substring(0, num);
            }
            return @string;
        }
		public static string ToHexString(byte[] bytes, int len)
		{
			return ParseUtil.ToHexString(bytes, 0, len);
		}
		public static string ToHexString(byte[] bytes, int start, int len)
		{
			string text = "";
			for (int i = start; i < start + len; i++)
			{
				byte b = bytes[i];
				text += b.ToString("x2");
			}
			return text;
		}
		public static byte[] ToByesByHex(string hexStr)
		{
			int length = hexStr.Length;
			byte[] array = new byte[length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToByte(hexStr.Substring(i * 2, 2), 16);
			}
			return array;
		}
	}
}
