using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ByteEx
    {
        public static string ToHexString(this byte bt)
        {
            StringBuilder result = new StringBuilder();

            var tmp = String.Format("{0:X}", Convert.ToInt32(bt));
            if (tmp.Length == 1)
            {
                result.Append("0" + tmp);
            }
            else
            {
                result.Append(tmp);
            }
            return result.ToString();
        }

        /// <summary>
        /// 字节数组帮助
        /// </summary>
        /// <param name="btArray"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] btArray)
        {
            StringBuilder build = new StringBuilder("");
            foreach (var bt in btArray)
            {
                build.Append(bt.ToHexString());
            }
            return build.ToString();
        }
    }
}
