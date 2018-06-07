using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace XCI.Helper
{
    /// <summary>
    /// 枚举操作帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 字符串转为枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="str">要转换的字符串</param>
        /// <returns>如果字符串为空 返回枚举默认值</returns>
        public static T ToEnum<T>(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return default(T);
            }
            return (T)Enum.Parse(typeof(T), str);
        }

    }

}
