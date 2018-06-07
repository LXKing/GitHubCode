using System.Collections.Generic;
using System;

namespace XCI.Helper
{
    /// <summary>
    /// 静态对象缓存
    /// </summary>
    public static class StaticCacheHelper
    {
        private static readonly Dictionary<string, object> CacheObj = new Dictionary<string, object>();
        private static readonly object SynLockobj = new object();

        /// <summary>
        /// 从缓存中获取对象
        /// </summary>
        /// <param name="key">键名</param>
        public static object Get(string key)
        {
            object find = null;
            CacheObj.TryGetValue(key, out find);
            return find;
        }


        /// <summary>
        /// 判断是否存在缓存Key
        /// </summary>
        /// <param name="key">键名</param>
        public static bool Contain(string key)
        {
            return CacheObj.ContainsKey(key);
        }


        /// <summary>
        /// 把对象写入缓存中
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="obj">对象</param>
        public static bool Add(string key, object obj)
        {
            lock (SynLockobj)
            {
                if (!CacheObj.ContainsKey(key))
                {
                    CacheObj.Add(key, obj);
                    return true;
                }
                return false;
            }
        }


        /// <summary>
        /// 把对象从缓存中移除
        /// </summary>
        /// <param name="key">键名</param>
        public static bool Remove(string key)
        {
            lock (SynLockobj)
            {
                if (CacheObj.ContainsKey(key))
                {
                    CacheObj.Remove(key);
                    return true;
                }
                return false;
            }
        }
    }
}
