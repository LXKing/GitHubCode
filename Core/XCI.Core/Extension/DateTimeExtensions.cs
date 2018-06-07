using System;
using XCI.Helper;

namespace XCI.Extension
{
    /// <summary>
    /// 时间日期扩展操作
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 获取格式化的日期字符串(格式 yyyy-MM-dd)
        /// </summary>
        /// <param name="datetime">指定的日期</param>
        public static string GetFormatDate(this DateTime datetime)
        {
            return DateTimeHelper.GetFormatDate(datetime);
        }


        /// <summary>
        /// 获取格式化的日期字符串
        /// </summary>
        /// <param name="datetime">指定的日期</param>
        /// <param name="format">格式字符串</param>
        public static string GetFormatDate(this DateTime datetime, string format)
        {
            return DateTimeHelper.GetFormatDate(datetime,format);
        }
        
        
        /// <summary>
        /// 返回一个新的DateTime实例使用指定的日
        /// </summary>
        /// <param name="datetime">指定的日期</param>
        /// <param name="day">天 (1-31)</param>
        public static DateTime SetDay(this DateTime datetime, int day)
        {
            return new DateTime(datetime.Year, datetime.Month, day);
        }


        /// <summary>
        /// 返回一个新的DateTime实例使用指定的月份
        /// </summary>
        /// <param name="datetime">指定的日期</param>
        /// <param name="month">月份 (1-12)</param>
        public static DateTime SetMonth(this DateTime datetime, int month)
        {
            return new DateTime(datetime.Year, month, datetime.Day);
        }


        /// <summary>
        /// 返回一个新的DateTime实例使用指定的年份
        /// </summary>
        /// <param name="datetime">指定的日期</param>
        /// <param name="year">年份</param>
        public static DateTime SetYear(this DateTime datetime, int year)
        {
            return new DateTime(year, datetime.Month, datetime.Day);
        }


        
    }
}
