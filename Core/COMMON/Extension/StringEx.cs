using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// String扩展方法类
    /// </summary>
    public static class StringEx
    {
        #region 类型转换
        /// <summary>
        /// Json串转任意类型
        /// </summary>
        /// <typeparam name="T">要转换成的目标类型</typeparam>
        /// <param name="jsonString">要转换的Json字符串</param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string jsonString)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception ex)
            {
                COMMON.Logs.Log.WriteException("ObjectEx.ToJsonString方法异常", ex);
                throw ex;
            }
        }
        /// <summary>
        /// 字符串转值类型方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ToType(this string value, Type type)
        {
            try
            {
                return System.ComponentModel.TypeDescriptor.GetConverter(type).ConvertFrom(value);
            }
            catch (Exception ex)
            {
                COMMON.Logs.Log.WriteException("StringEx.ToType方法异常", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 字符串转值类型方法
        /// </summary>
        /// <typeparam name="T">值类型(int decimal double byte)</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToValueType<T>(this string value) where T : struct
        {
            try
            {
                return (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value);
            }
            catch (Exception ex)
            {
                COMMON.Logs.Log.WriteException("StringEx.ToValueType<T>方法异常", ex);
                throw ex;
            }
        }
        #endregion

        #region 判断空字符串或null
        /// <summary>
        /// 判断字符串是否为null或者空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        #endregion

        #region 格式化字符
        /// <summary>
        /// 将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        /// </summary>
        /// <param name="value">复合格式字符串。</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        /// <returns></returns>
        public static string FormatWith(this string value,params object[] args)
        {
            return string.Format(value, args);
        }
        #endregion

        #region 字符串简体繁体全角半角转换(不常用)

        /// <summary>
        /// 英文全角转换为半角字符串("wｗｘｙ"=>"wwxy")
        /// </summary>
        public static string ToDBC(this string value)
        {
            return Strings.StrConv(value, VbStrConv.Narrow, 0);
        }
        /// <summary>
        /// 任意字符串半角转全角(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        public static string ToSBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        /// <summary>
        /// 繁体中文转换为简体中文
        /// </summary>
        public static string ToChineseSimplified(this string value)
        {
            return Strings.StrConv(value, VbStrConv.SimplifiedChinese, 0);
        }

        /// <summary>
        /// 简体中文转换为繁体中文
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToChineseTraditional(this string value)
        {
            return Strings.StrConv(value, VbStrConv.TraditionalChinese, 0);
        }
        #endregion

        #region 正则匹配
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string s, string pattern)
        {
            if (s == null) 
                return false;
            else 
                return Regex.IsMatch(s, pattern);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string Match(this string s, string pattern)
        {
            if (s == null) 
                return "";
            return 
                Regex.Match(s, pattern).Value;
        }
        #endregion
    }
}
