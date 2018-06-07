using System;
namespace Globitrust.Net.P2PMessaging
{
	internal static class BitConverter
	{
		public static byte[] GetBytes(int value)
		{
			byte[] bytes = System.BitConverter.GetBytes(value);
			BitConverter.CheckByteArray(bytes);
			return bytes;
		}
		public static byte[] GetBytes(uint value)
		{
			byte[] bytes = System.BitConverter.GetBytes(value);
			BitConverter.CheckByteArray(bytes);
			return bytes;
		}
		private static void CheckByteArray(byte[] toCheck)
		{
			if (System.BitConverter.IsLittleEndian)
			{
				Array.Reverse(toCheck);
			}
		}
		public static uint ToUInt32(byte[] value, int startIndex)
		{
			byte[] array = new byte[4];
			Buffer.BlockCopy(value, startIndex, array, 0, 4);
			if (System.BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return System.BitConverter.ToUInt32(array, 0);
		}
	}
}
