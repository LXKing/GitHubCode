using System;
using System.Diagnostics;
using System.Net;
using System.Text;
namespace SocketTool.Udp
{
	internal class Util
	{
		public static string GetMethod(int skipframes)
		{
			StackFrame stackFrame = new StackFrame(skipframes, true);
			return stackFrame.GetMethod().Name + "()";
		}
		public static byte[] StringToBytes(string s)
		{
			byte[] array = new byte[s.Length];
			for (int i = 0; i < s.Length; i++)
			{
				array[i] = (byte)s[i];
			}
			return array;
		}
		public static string BytesToString(byte[] b)
		{
			string text = "";
			for (int i = 0; i < b.Length; i++)
			{
				text += (char)b[i];
			}
			return text;
		}
		public static string LongToBytes(long num)
		{
			byte[] bytes = BitConverter.GetBytes(num);
			string text = "";
			for (int i = 0; i < bytes.Length; i++)
			{
				text += (char)bytes[i];
			}
			return text;
		}
		public static long BytesToLong(string input)
		{
			byte[] array = new byte[8];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)input[i];
			}
			return BitConverter.ToInt64(array, 0);
		}
		public static string ShortToBytes(short ch)
		{
			byte[] bytes = BitConverter.GetBytes(ch);
			string text = "";
			for (int i = 0; i < bytes.Length; i++)
			{
				text += (char)bytes[i];
			}
			return text;
		}
		public static short BytesToShort(string input)
		{
			byte[] array = new byte[2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)input[i];
			}
			return BitConverter.ToInt16(array, 0);
		}
		public static string IntToBytes(int num)
		{
			byte[] bytes = BitConverter.GetBytes(num);
			string text = "";
			for (int i = 0; i < bytes.Length; i++)
			{
				text += (char)bytes[i];
			}
			return text;
		}
		public static int BytesToInt(string input)
		{
			byte[] array = new byte[4];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)input[i];
			}
			return BitConverter.ToInt32(array, 0);
		}
		public static string UintToBytes(uint num)
		{
			byte[] bytes = BitConverter.GetBytes(num);
			new StringBuilder(4);
			string text = "";
			for (int i = 0; i < bytes.Length; i++)
			{
				text += (char)bytes[i];
			}
			return text;
		}
		public static uint BytesToUint(string input)
		{
			byte[] array = new byte[4];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)input[i];
			}
			return BitConverter.ToUInt32(array, 0);
		}
		public static string CreatePacketHeader(uint seq_num, byte flags, string opcode, string encrypt_key, string[] fields)
		{
			string text = "";
			if (fields == null)
			{
				fields = new string[0];
			}
			text += opcode;
			text += Util.UintToBytes(seq_num);
			text += (char)flags;
			text += Util.ShortToBytes((short)fields.Length);
			if (fields.Length > 0)
			{
				for (int i = 0; i < fields.Length; i++)
				{
					text += Util.ShortToBytes((short)fields[i].Length);
				}
			}
			if (encrypt_key != "")
			{
				text += Util.XORCrypt(UdpConsts.ENCRYPT_CHECK_STRING, encrypt_key);
			}
			else
			{
				text += UdpConsts.ENCRYPT_CHECK_STRING;
			}
			return text;
		}
		public static string XORCrypt(string data, string key)
		{
			if (key == "")
			{
				return data;
			}
			string text = "";
			int[] array = new int[data.Length];
			int num = 0;
			for (int i = 0; i < data.Length; i++)
			{
				text += (data[i] ^ key[num]);
				array[i] = (int)(data[i] ^ key[num]);
				num++;
				if (num >= key.Length)
				{
					num = 0;
				}
			}
			return text;
		}
		public static string GenerateEncryptionKey()
		{
			int num = 40;
			StringBuilder stringBuilder = new StringBuilder();
			Random random = new Random();
			for (int i = 0; i < num; i++)
			{
				char value = Convert.ToChar(Convert.ToInt32(26.0 * random.NextDouble() + 65.0));
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}
		public static string[] GetLocalAddresses()
		{
			string hostName = Dns.GetHostName();
			IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
			string[] array = new string[hostEntry.AddressList.Length];
			int num = 0;
			IPAddress[] addressList = hostEntry.AddressList;
			for (int i = 0; i < addressList.Length; i++)
			{
				IPAddress iPAddress = addressList[i];
				array[num] = iPAddress.ToString();
				num++;
			}
			return array;
		}
	}
}
