using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class DateTimeEx
    {
        /// <summary>
        /// 获取某个时间的时间戳(字符串)，将当前时间转为UTC
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetTimeStampString(this DateTime dt, bool ToUTC = true)
        {
            DateTime dtNew=dt;
            if(ToUTC)
            {
                dtNew = dt.ToUniversalTime();
            }
            TimeSpan ts = dtNew - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 获取某个时间的时间戳(浮点数)，将当前时间转为UTC
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static double GetTimeStamp(this DateTime dt,bool ToUTC=true)
        {
            DateTime dtNew = dt;
            if (ToUTC)
            {
                dtNew = dt.ToUniversalTime();
            }
            TimeSpan ts = dtNew - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return ts.TotalSeconds;
        }

    }
}
