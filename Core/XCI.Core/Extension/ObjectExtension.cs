using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace XCI.Extension
{
    /// <summary>
    /// Object扩展操作
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 测试对象是否为空或者是否是空字符串
        /// </summary>
        /// <param name="obj">测试对象</param>
        public static bool IsEmpty(this object obj)
        {
            return obj == null || string.IsNullOrEmpty(obj.ToString());
        }


        /// <summary>
        /// 测试对象是否不为空并且不是空字符串
        /// </summary>
        /// <param name="obj">测试对象</param>
        public static bool IsNotEmpty(this object obj)
        {
            return obj != null && !string.IsNullOrEmpty(obj.ToString());
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="args">替换参数</param>
        public static string FormatString(this object obj, params object[] args)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return string.Format(obj.ToString(), args);
        }

        /// <summary>
        /// 转为整数
        /// </summary>
        /// <param name="obj">转换对象</param>
        public static int ToInt(this object obj)
        {
            int _result = 0;
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out _result);
            }
            return _result;
        }

        /// <summary>
        /// 转为整数
        /// </summary>
        /// <param name="obj">转换对象</param>
        /// <param name="defalutValue">默认值</param>
        public static int ToInt(this object obj, int defalutValue)
        {
            int _result = defalutValue;
            if (obj != null)
            {
                if (int.TryParse(obj.ToString(), out _result))
                {
                    return _result;
                }
                return defalutValue;
            }
            return _result;
        }

        /// <summary>
        /// 转为十进制数
        /// </summary>
        /// <param name="obj">转换对象</param>
        public static decimal ToDecimal(this object obj)
        {
            decimal _result = 0;
            if (obj != null)
            {
                decimal.TryParse(obj.ToString(), out _result);
            }
            return _result;
        }

        /// <summary>
        /// 转为64位整数
        /// </summary>
        /// <param name="obj">转换对象</param>
        public static long ToLong(this object obj)
        {
            long _result = 0;
            if (obj != null)
            {
                long.TryParse(obj.ToString(), out _result);
            }
            return _result;
        }

        /// <summary>
        /// 转为双精度浮点数
        /// </summary>
        /// <param name="obj">转换对象</param>
        public static double ToDouble(this object obj)
        {
            double _result = 0;
            if (obj != null)
            {
                double.TryParse(obj.ToString(), out _result);
            }
            return _result;
        }

        /// <summary>
        /// 转为单精度浮点数
        /// </summary>
        /// <param name="obj">转换对象</param>
        public static float ToFloat(this object obj)
        {
            float _result = 0;
            if (obj != null)
            {
                float.TryParse(obj.ToString(), out _result);
            }
            return _result;
        }


        /// <summary>
        /// 转为日期
        /// </summary>
        /// <param name="obj">转换对象</param>
        public static DateTime ToDateTime(this object obj)
        {
            DateTime _result = DateTime.Now;
            if (obj != null)
            {
                DateTime.TryParse(obj.ToString(), out _result);
            }
            return _result;
        }


        /// <summary>
        /// 转为时间
        /// </summary>
        /// <param name="obj">转换对象</param>
        public static TimeSpan ToTime(this object obj)
        {
            TimeSpan _result = new TimeSpan();
            if (obj != null)
            {
                TimeSpan.TryParse(obj.ToString(), out _result);
            }
            return _result;
        }


        /// <summary>
        /// 转为布尔
        /// </summary>
        /// <param name="obj">转换对象</param>
        public static bool ToBool(this object obj)
        {
            if (obj == null)
            {
                return false;
            }

            string str = obj.ToString().Trim().ToLower();
            if (str.Equals("yes") || str.Equals("true") || str.Equals("1"))
            {
                return true;
            }

            return false;
        }
    }
}
