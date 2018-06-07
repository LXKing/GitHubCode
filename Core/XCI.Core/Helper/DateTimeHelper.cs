using System;
using System.Globalization;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// ���ڲ���������
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// �ж�ָ�������Ƿ�������
        /// </summary>
        /// <param name="dateTime">����</param>
        public static bool IsLeapYear(DateTime dateTime)
        {
            return dateTime.Year % 4 == 0 && (dateTime.Year % 100 != 0 || dateTime.Year % 400 == 0);
        }


        /// <summary>
        /// ��ȡ���ں�ʱ��
        /// </summary>
        /// <param name="dateTime">����</param>
        /// <param name="time">ʱ��</param>
        public static DateTime GetDateWithTime(DateTime dateTime, TimeSpan time)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, time.Hours, time.Minutes, time.Seconds);
        }


        /// <summary>
        /// �ж�ָ�������Ƿ�����ĩ
        /// </summary>
        /// <param name="datetime">ָ��������</param>
        public static bool IsWeekend(DateTime datetime)
        {
            return datetime.DayOfWeek == DayOfWeek.Saturday ||
                   datetime.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// ��ȡ�·����һ��
        /// </summary>
        /// <param name="date">ָ������</param>
        public static int GetMonthLastDay(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = new GregorianCalendar().GetDaysInMonth(year, month);//��ȡָ���µ�����
            DateTime lastDay = new DateTime(year, month, day);
            return lastDay.Day;
        }


        /// <summary>
        /// ��ȡʱ����ַ�������(xx��xxСʱxx����)
        /// </summary>
        /// <param name="hours">Сʱ��</param>
        public static string GetTimeString(decimal hours)
        {
            StringBuilder sb = new StringBuilder();
            int minutes = Convert.ToInt32(hours * 60);
            TimeSpan ts = new TimeSpan(0, minutes, 0);
            if (ts.Days >= 1)
            {
                sb.AppendFormat("{0}��", ts.Days);
                ts -= new TimeSpan(ts.Days, 0, 0, 0);
            }
            if (ts.Hours >= 1)
            {
                sb.AppendFormat("{0}Сʱ", ts.Hours);
                ts -= new TimeSpan(ts.Hours, 0, 0);
            }
            if (ts.Minutes >= 1)
            {
                sb.AppendFormat("{0}����", ts.Minutes);
            }
            return sb.ToString();
        }


        /// <summary>
        /// ����ʱ�����ַ�������(xxСʱǰ,xx����ǰ)
        /// </summary>
        /// <param name="date">ʱ��1</param>
        /// <param name="nowDate">ʱ��2</param>
        /// <returns>xxСʱǰ,xx����ǰ</returns>
        public static string GetDateDiffString(DateTime date, DateTime nowDate)
        {
            string dateDiff;
            TimeSpan ts = nowDate - date;
            if (ts.Days >= 1)
            {
                dateDiff = date.Month + "��" + date.Day + "��";
            }
            else
            {
                if (ts.Hours > 1)
                {
                    dateDiff = ts.Hours + "Сʱǰ";
                }
                else
                {
                    dateDiff = ts.Minutes + "����ǰ";
                }
            }
            return dateDiff;
        }


        /// <summary>
        /// ��ȡ��ǰ��������
        /// </summary>
        /// <returns>���ص�ǰ�����ڼ�</returns>
        public static string GetWeekDay()
        {
            return GetWeekDay(DateTime.Now);
        }


        /// <summary>
        /// ��ȡ���ڶ�Ӧ��������
        /// </summary>
        /// <param name="dateTime">ָ��������</param>
        /// <returns>�������ڶ�Ӧ��������(������,����һ...)</returns>
        public static string GetWeekDay(DateTime dateTime)
        {
            string[] weekDay = { "������", "����һ", "���ڶ�", "������", "������", "������", "������" };
            return weekDay[(int)(dateTime.DayOfWeek)];
        }


        /// <summary>
        /// ��ȡ��ʽ���������ַ���
        /// </summary>
        /// <param name="datetime">ָ��������</param>
        /// <param name="format">��ʽ�ַ���</param>
        public static string GetFormatDate(DateTime datetime, string format)
        {
            return datetime.ToString(format);
        }


        /// <summary>
        /// ��ȡ��ʽ���������ַ���(��ʽ yyyy-MM-dd)
        /// </summary>
        /// <param name="datetime">ָ��������</param>
        public static string GetFormatDate(DateTime datetime)
        {
            return GetFormatDate(datetime, "yyyy-MM-dd");
        }


        /// <summary>
        /// ��ȡ��ʽ���������ַ���(��ʽ yyyy-MM-dd HH:mm:ss.FFF)
        /// </summary>
        /// <param name="datetime">ָ��������</param>
        public static string GetFormatDateHasMilliSecond(DateTime datetime)
        {
            return GetFormatDate(datetime,"yyyy-MM-dd HH:mm:ss.FFF");
        }


        /// <summary>
        /// ��ȡ��ʽ���������ַ���(�� yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="datetime">ָ��������</param>
        public static string GetFormatDateHasSecond(DateTime datetime)
        {
            return GetFormatDate(datetime, "yyyy-MM-dd HH:mm:ss");
        }


        /// <summary>
        /// ת����ð��ʱ��Ϊһ��ʱ�����
        /// </summary>
        /// <param name="military">��ð��ʱ��</param>
        public static TimeSpan ConvertFromMilitaryTime(int military)
        {
            TimeSpan time = TimeSpan.MinValue;
            int hours = military / 100;
            int minutes = military % 100;

            time = new TimeSpan(hours, minutes, 0);
            return time;
        }


        /// <summary>
        /// ת��Ϊ��ð��ʱ��
        /// </summary>
        /// <param name="timeSpan">ʱ�����</param>
        public static int ConvertToMilitary(TimeSpan timeSpan)
        {
            return (timeSpan.Hours * 100) + timeSpan.Minutes;
        }


        /// <summary>
        /// ��ʽ��ʱ��
        /// </summary>
        /// <param name="militaryTime">��ð��ʱ��</param>
        public static string Format(int militaryTime)
        {
            TimeSpan t = ConvertFromMilitaryTime(militaryTime);
            return Format(t);
        }


        /// <summary>
        /// ��ʽ��ʱ��
        /// </summary>
        /// <param name="time">ʱ�����</param>
        public static string Format(TimeSpan time)
        {
            int hours = time.Hours;
            string amPm = hours < 12 ? "����" : "����";

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
