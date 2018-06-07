using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Text;
using XCI.Helper;

namespace XCI.Extension
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 格式化字符串输出
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="args">参数数组</param>
        public static string FS(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

    }
}
