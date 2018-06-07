using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumEx
    {
        /// <summary>
        /// 枚举转Int数值
        /// </summary>
        /// <typeparam name="Enum">枚举对象类型</typeparam>
        /// <param name="enumObj">枚举对象</param>
        /// <returns></returns>
        public static int ToInt(this Enum enumObj)
        {
            try
            {
                return enumObj.GetHashCode();
            }
            catch(Exception ex)
            {
                COMMON.Logs.Log.WriteException("EnumEx.EnumToInt异常",ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取枚举的Description描述
        /// </summary>
        /// <typeparam name="Enum"></typeparam>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumObj)
        {
            try
            {
                var em = enumObj.ToString();
                FieldInfo fieldInfo = enumObj.GetType().GetField(em);
                if (fieldInfo == null) 
                    return em;
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length < 1) return em;
                return attributes[0].Description;
            }
            catch (Exception ex)
            {
                COMMON.Logs.Log.WriteException("EnumEx.GetDescription异常", ex);
                throw ex;
            }
            
        }

        /// <summary>
        /// 将枚举以字典的形式返回
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static Dictionary<int, string> ToDictionary<TEnum>(this TEnum enumObj) where TEnum : struct
        {
            var type = typeof(TEnum);
            var values = from Enum e in Enum.GetValues(type)
                         select new KeyValuePair<int, string>(e.GetHashCode(), e.ToString());
            return values.ToDictionary(a => a.Key, b => b.Value);
        }
        /// <summary>
        /// 通过枚举值获取所有值与枚举字符串的键值对List集合
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumObj"></param>
        /// <returns>键值对集合List<KeyValuePair<int, string>></returns>
        public static IEnumerable<KeyValuePair<int, string>> ToIEnumerable<TEnum>(this TEnum enumObj) where TEnum : struct
        {
            var type = typeof(TEnum);
            var values = from Enum e in Enum.GetValues(type)
                         select new KeyValuePair<int, string>(e.GetHashCode(), e.ToString());
            return values;
        }

        /// <summary>
        /// 返回枚举值列表
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static List<TEnum> ToEnumList<TEnum>(this TEnum enumObj) where TEnum : struct
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select e;
            return values.ToList();
        }
    }
}
