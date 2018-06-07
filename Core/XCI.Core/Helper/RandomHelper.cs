using System;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// 随机产生器帮助类
    /// </summary>
    public static class RandomHelper
    {
        #region 生成指定位数随机数

        /// <summary>
        /// 生成随机字符串(0-9 a-b A-B)
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        public static string GetStringRandom(int length)
        {
            char[] constant =   
              {   
                '0','1','2','3','4','5','6','7','8','9',   
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'   
              };
            StringBuilder newRandom = new StringBuilder(62);
            Random rd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }


        /// <summary>
        ///  生成随机数字(0-9)
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机数字串</returns>
        public static string GetNumberRandom(int length)
        {
            StringBuilder newRandom = new StringBuilder(10);
            char[] NumStr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Random rd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(NumStr[rd.Next(10)]);
            }
            return newRandom.ToString();
        }


        /// <summary>
        /// 获取英文字符随机串
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="isToUpper">是否转为大写</param>
        /// <returns>随机英文串</returns>
        public static string GetEnglistCharRandom(int length, bool isToUpper = false)
        {
            StringBuilder newRandom = new StringBuilder(26);
            char[] NumStr = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            Random rd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(NumStr[rd.Next(26)]);
            }
            string result = newRandom.ToString();
            if (isToUpper)
            {
                result = result.ToUpper();
            }
            return result;
        }
        #endregion
    }
}
