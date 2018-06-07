using System;
using System.Globalization;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// 日期操作帮助类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 判断指定日期是否是闰年
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static bool IsLeapYear(DateTime dateTime)
        {
            return dateTime.Year % 4 == 0 && (dateTime.Year % 100 != 0 || dateTime.Year % 400 == 0);
        }


        /// <summary>
        /// 获取日期和时间
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="time">时间</param>
        public static DateTime GetDateWithTime(DateTime dateTime, TimeSpan time)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, time.Hours, time.Minutes, time.Seconds);
        }


        /// <summary>
        /// 判断指定日期是否是周末
        /// </summary>
        /// <param name="datetime">指定的日期</param>
        public static bool IsWeekend(DateTime datetime)
        {
            return datetime.DayOfWeek == DayOfWeek.Saturday ||
                   datetime.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// 获取月份最后一天
        /// </summary>
        /// <param name="date">指定日期</param>
        public static int GetMonthLastDay(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = new GregorianCalendar().GetDaysInMonth(year, month);//获取指定月的天数
            DateTime lastDay = new DateTime(year, month, day);
            return lastDay.Day;
        }


        /// <summary>
        /// 获取时间的字符串描述(xx天xx小时xx分钟)
        /// </summary>
        /// <param name="hours">小时数</param>
        public static string GetTimeString(decimal hours)
        {
            StringBuilder sb = new StringBuilder();
            int minutes = Convert.ToInt32(hours * 60);
            TimeSpan ts = new TimeSpan(0, minutes, 0);
            if (ts.Days >= 1)
            {
                sb.AppendFormat("{0}天", ts.Days);
                ts -= new TimeSpan(ts.Days, 0, 0, 0);
            }
            if (ts.Hours >= 1)
            {
                sb.AppendFormat("{0}小时", ts.Hours);
                ts -= new TimeSpan(ts.Hours, 0, 0);
            }
            if (ts.Minutes >= 1)
            {
                sb.AppendFormat("{0}分钟", ts.Minutes);
            }
            return sb.ToString();
        }


        /// <summary>
        /// 返回时间差的字符串描述(xx小时前,xx分钟前)
        /// </summary>
        /// <param name="date">时间1</param>
        /// <param name="nowDate">时间2</param>
        /// <returns>xx小时前,xx分钟前</returns>
        public static string GetDateDiffString(DateTime date, DateTime nowDate)
        {
            string dateDiff;
            TimeSpan ts = nowDate - date;
            if (ts.Days >= 1)
            {
                dateDiff = date.Month + "月" + date.Day + "日";
            }
            else
            {
                if (ts.Hours > 1)
                {
                    dateDiff = ts.Hours + "小时前";
                }
                else
                {
                    dateDiff = ts.Minutes + "分钟前";
                }
            }
            return dateDiff;
        }


        /// <summary>
        /// 获取当前是星期数
        /// </summary>
        /// <returns>返回当前是星期几</returns>
        public static string GetWeekDay()
        {
            return GetWeekDay(DateTime.Now);
        }


        /// <summary>
        /// 获取日期对应的星期数
        /// </summary>
        /// <param name="dateTime">指定的日期</param>
        /// <returns>返回日期对应的星期数(星期日,星期一...)</returns>
        public static string GetWeekDay(DateTime dateTime)
        {
            string[] weekDay = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return weekDay[(int)(dateTime.DayOfWeek)];
        }


        /// <summary>
        /// 获取格式化的日期字符串
        /// </summary>
        /// <param name="datetime">指定的日期</param>
        /// <param name="format">格式字符串</param>
        public static string GetFormatDate(DateTime datetime, string format)
        {
            return datetime.ToString(format);
        }


        /// <summary>
        /// 获取格式化的日期字符串(格式 yyyy-MM-dd)
        /// </summary>
        /// <param name="datetime">指定的日期</param>
        public static string GetFormatDate(DateTime datetime)
        {
            return GetFormatDate(datetime, "yyyy-MM-dd");
        }


        /// <summary>
        /// 获取格式化的日期字符串(格式 yyyy-MM-dd HH:mm:ss.FFF)
        /// </summary>
        /// <param name="datetime">指定的日期</param>
        public static string GetFormatDateHasMilliSecond(DateTime datetime)
        {
            return GetFormatDate(datetime,"yyyy-MM-dd HH:mm:ss.FFF");
        }


        /// <summary>
        /// 获取格式化的日期字符串(如 yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="datetime">指定的日期</param>
        public static string GetFormatDateHasSecond(DateTime datetime)
        {
            return GetFormatDate(datetime, "yyyy-MM-dd HH:mm:ss");
        }


        /// <summary>
        /// 转换无冒号时间为一个时间对象
        /// </summary>
        /// <param name="military">无冒号时间</param>
        public static TimeSpan ConvertFromMilitaryTime(int military)
        {
            TimeSpan time = TimeSpan.MinValue;
            int hours = military / 100;
            int minutes = military % 100;

            time = new TimeSpan(hours, minutes, 0);
            return time;
        }


        /// <summary>
        /// 转换为无冒号时间
        /// </summary>
        /// <param name="timeSpan">时间对象</param>
        public static int ConvertToMilitary(TimeSpan timeSpan)
        {
            return (timeSpan.Hours * 100) + timeSpan.Minutes;
        }


        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="militaryTime">无冒号时间</param>
        public static string Format(int militaryTime)
        {
            TimeSpan t = ConvertFromMilitaryTime(militaryTime);
            return Format(t);
        }


        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="time">时间对象</param>
        public static string Format(TimeSpan time)
        {
            int hours = time.Hours;
            string amPm = hours < 12 ? "上午" : "下午";

            // Convert military time 13 hours to standard time 1pm
            if (hours > 12)
                hours = hours - 12;

            if (time.Minutes == 0)
                return hours + amPm;

            // Handles 11:10 - 11:59
            if (time.Minutes > 10)
                return hours + ":" + time.Minutes + amPm;

            // Handles 11:01 - 11:09
            return hours + ":0" + time.Minutes + amPm;
        }

    }
}
