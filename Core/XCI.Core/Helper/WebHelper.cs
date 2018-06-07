using System.Web;

namespace XCI.Helper
{
    /// <summary>
    /// Web操作辅助类
    /// </summary>
    public static class WebHelper
    {
        /// <summary>
        /// 获取Request.QueryString参数
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>如果不存在返回空字符串</returns>
        public static string GetQueryString(string key)
        {
            return GetQueryString(key, string.Empty);
        }

        /// <summary>
        /// 获取Request.QueryString参数
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">不存在时返回的默认值</param>
        public static string GetQueryString(string key,string defaultValue)
        {
            object obj = HttpContext.Current.Request.QueryString[key];
            if (obj != null)
            {
                return obj.ToString();
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取Request.Form参数
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>如果不存在返回空字符串</returns>
        public static string GetFormString(string key)
        {
            return GetFormString(key, string.Empty);
        }

        /// <summary>
        /// 获取Request.Form参数
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">不存在时返回的默认值</param>
        public static string GetFormString(string key, string defaultValue)
        {
            object obj = HttpContext.Current.Request.Form[key];
            if (obj != null)
            {
                return obj.ToString();
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取Request.Form参数或者Request.QueryString参数
        /// </summary>
        /// <param name="key">键值</param>
        public static string GetParamString(string key)
        {
            return GetParamString(key, string.Empty);
        }

        /// <summary>
        /// 获取Request.Form参数或者Request.QueryString参数
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">不存在时返回的默认值</param>
        public static string GetParamString(string key, string defaultValue)
        {
            object obj = HttpContext.Current.Request[key];
            if (obj != null)
            {
                return obj.ToString();
            }
            return defaultValue;
        }
    }
}