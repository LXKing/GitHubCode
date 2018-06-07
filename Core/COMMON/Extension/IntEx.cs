using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Int的扩展方法
    /// </summary>
    public static class IntEx
    {
        /// <summary>
        /// 执行n次循环(从零开始)
        /// </summary>
        /// <typeparam name="Int32"></typeparam>
        /// <param name="instance"></param>
        /// <param name="action">带整形参数的委托</param>
        /// <returns></returns>
        public static int ForEach<Int32>(this int instance, Action<int> action)
        {
            for (int i = 0; i < instance; i++)
            {
                action(i);
            }
            return instance;
        }

        /// <summary>
        /// 整形转字节
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static byte ToByte(this int i)
        {
            try
            {
                byte bt = Convert.ToByte(i);
                return bt;
            }
            catch(OverflowException ex)
            {
                throw new Exception("int 转字节超出255,转换溢出!");
            }
        }
    }
}
