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
        /// <typeparam name="TEnum">枚举对象类型</typeparam>
        /// <param name="enumObj">枚举对象</param>
        /// <returns></returns>
        public static int ToInt<TEnum>(this TEnum enumObj)where TEnum:struct
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
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static string GetDescription<TEnum>(this TEnum enumObj)where TEnum:struct
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
        /// 返回枚举类型的中文描述 DescriptionAttribute指定的名字
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static string Description<TEnum>(this TEnum enumObj) where TEnum : struct
        {
            var result = new StringBuilder(string.Empty);
            var em = enumObj.ToString();
            var emList = em.Split(',').ToList();
            emList.ForEach((x) =>
            {
                result.AppendLine(((TEnum)System.Enum.Parse(typeof(TEnum), x)).GetDescription());
            });
            return result.ToString();
        }
        /// <summary>
        /// 将枚举以字典的形式返回
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static Dictionary<int, string> ToDictionary<TEnum>(this TEnum enumObj) where TEnum : struct
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new KeyValuePair<int, string>(e.GetHashCode(), e.ToString());
            return values.ToDictionary(a => a.Key, b => b.Value);
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
        /// <summary>
        /// 转换成为枚举类型
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="strEnum"></param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this string strEnum)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), strEnum);
        }

        #region 新增
        public static IList<object> GetValues(Type enumType)
        {
            if (!enumType.IsEnum())
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum.");
            }
            List<object> list = new List<object>();
            IEnumerable<FieldInfo> enumerable =
                from field in enumType.GetFields()
                where field.IsLiteral
                select field;
            foreach (FieldInfo current in enumerable)
            {
                object value = current.GetValue(enumType);
                list.Add(value);
            }
            return list;
        }
        public static IList<string> GetNames(Type enumType)
        {
            if (!enumType.IsEnum())
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum.");
            }
            List<string> list = new List<string>();
            IEnumerable<FieldInfo> enumerable =
                from field in enumType.GetFields()
                where field.IsLiteral
                select field;
            foreach (FieldInfo current in enumerable)
            {
                list.Add(current.Name);
            }
            return list;
        }
        #endregion
    }
}
