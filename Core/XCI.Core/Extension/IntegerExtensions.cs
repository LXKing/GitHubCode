using System;

namespace XCI.Extension
{
    /// <summary>
    /// 整数扩展操作
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// 是否是奇数
        /// </summary>
        /// <param name="num">数字</param>
        public static bool IsOdd(this int num)
        {
            return num % 2 != 0;
        }


        /// <summary>
        /// 是否是偶数
        /// </summary>
        /// <param name="num">数字</param>
        public static bool IsEven(this int num)
        {
            return num % 2 == 0;
        }


        /// <summary>
        /// 返回MB字节数
        /// </summary>
        /// <param name="num">数字</param>
        public static int MegaBytes(this int num)
        {
            return num * 1000000;
        }


        /// <summary>
        /// 返回KB字节数
        /// </summary>
        /// <param name="num">数字</param>
        public static int KiloBytes(this int num)
        {
            return num * 1000;
        }


        /// <summary>
        /// 返回TB字节数
        /// </summary>
        /// <param name="num">数字</param>
        public static int TeraBytes(this int num)
        {
            return num * 1000000000;
        }

        /// <summary>
        /// 返回TimeSpan对象使用指定的天数
        /// </summary>
        /// <param name="num">天数</param>
        public static TimeSpan Days(this int num)
        {
            return new TimeSpan(num, 0, 0, 0);
        }


        /// <summary>
        /// 返回TimeSpan对象使用指定的小时数
        /// </summary>
        /// <param name="num">小时数</param>
        public static TimeSpan Hours(this int num)
        {
            return new TimeSpan(0, num, 0, 0);
        }


        /// <summary>
        /// 返回TimeSpan对象使用指定的分钟数
        /// </summary>
        /// <param name="num">分钟数</param>
        public static TimeSpan Minutes(this int num)
        {
            return new TimeSpan(0, 0, num, 0);
        }


        /// <summary>
        /// 返回TimeSpan对象使用指定的秒数
        /// </summary>
        /// <param name="num">秒数</param>
        public static TimeSpan Seconds(this int num)
        {
            return new TimeSpan(0, 0, 0, num);
        }
    }
}