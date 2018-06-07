using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Enumerable可枚举的扩展方法
    /// </summary>
    public static class IEnumerableEx
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> instance, Action<T> action)
        {
            try
            {
                foreach (T local in instance)
                {
                    action(local);
                }
                return instance;
            }
            catch (Exception ex)
            {
                COMMON.Logs.Log.WriteException("异常",ex);
                throw ex;
            }
        }

        public static string ToStringSplitByChar(this IEnumerable<string> instance,string splitChar)
        {
            return string.Join(splitChar, instance.Select(x => x.ToString()));
        }
    }
}
