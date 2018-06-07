using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 基于HttpRuntime.Cache的缓存实现类 
    /// </summary>
    [XCIComponent(
        "基于HttpRuntime.Cache的缓存实现",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "2.1.0.0",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "基于HttpRuntime.Cache的缓存实现",
        "XCI.XCIComponent.ComponentLogo.png")]
    public class HttpRuntimeCache : CacheBase, ICache
    {
        private System.Web.Caching.Cache cache;

        /// <summary>
        /// 默认构造
        /// </summary>
        public HttpRuntimeCache()
        {
            cache = HttpRuntime.Cache;
        }

        /// <summary>
        /// 获取缓存项数量
        /// </summary>
        public override int Count
        {
            get { return cache.Count; }
        }

        /// <summary>
        /// 获取全部缓存项的键集合
        /// </summary>
        public override string[] Keys
        {
            get
            {
                List<string> keys = new List<string>();
                foreach (DictionaryEntry entry in cache)
                {
                    string key = (string)entry.Key;
                    keys.Add(key);
                }

                return keys.ToArray();
            }
        }

        /// <summary>
        /// 添加一个缓存对象(键值不能重复)
        /// </summary>
        /// <param name="key">缓存项键</param>
        /// <param name="value">缓存项值</param>
        /// <param name="absoluteExpiration">绝对到期时间 如果指定此时间 则slidingExpiration必须是TimeSpan.Zero</param>
        /// <param name="slidingExpiration">最后一次访问所插入对象时与该对象到期时之间的时间间隔(单位分钟)(现在时间与最后一次访问缓存对象的时间间隔超过指定间隔 则缓存对象过过期) 如果指定此间隔 则absoluteExpiration必须是DateTime.MaxValue</param>
        /// <param name="priority">优先级</param>
        /// <param name="onRemoveCallback">移除缓存项时通知应用程序的回调方法</param>
        public override void Add(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, Action<string, object, CacheItemRemovedReason> onRemoveCallback)
        {
            Guard.IsNotNull(key, "键不能为空");
            
            if (Contains(key))
            {
                throw new ArgumentNullException("key", "键已经存在");
            }

            CacheItemRemovedCallback removeCallback = (s, o, c) =>
            {
                if (onRemoveCallback != null)
                {
                    onRemoveCallback(s, o, c);
                }
            };

            System.Web.Caching.CacheItemPriority aspNetPriority = ConvertToAspNetPriority(priority);

            
            cache.Insert(key, value, null, absoluteExpiration, slidingExpiration, aspNetPriority, removeCallback);
        }

        /// <summary>
        /// 清空全部缓存项
        /// </summary>
        public override void Clear()
        {
            foreach (string key in Keys)
            {
                cache.Remove(key);
            }
        }

        /// <summary>
        /// 是否存在指定键值的缓存项
        /// </summary>
        /// <param name="key">缓存项键</param>
        /// <returns>存在返回True</returns>
        public override bool Contains(string key)
        {
            object obj = cache.Get(key);
            if (obj == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 移除指定键值的缓存项
        /// </summary>
        /// <param name="key">缓存项键</param>
        public override object Remove(string key)
        {
            return cache.Remove(key);
        }

        /// <summary>
        /// 返回指定键值的缓存项
        /// </summary>
        /// <param name="key">缓存项键</param>
        /// <returns>从 Cache 移除的项。如果未找到键参数中的值，则返回 null。</returns>
        public override object Get(string key)
        {
            return cache.Get(key);
        }

        /// <summary>
        /// 转换缓存优先级
        /// </summary>
        /// <param name="priority">缓存优先级</param>
        private System.Web.Caching.CacheItemPriority ConvertToAspNetPriority(CacheItemPriority priority)
        {
            if (priority == CacheItemPriority.Default) { return System.Web.Caching.CacheItemPriority.Default; }
            if (priority == CacheItemPriority.High) { return System.Web.Caching.CacheItemPriority.High; }
            if (priority == CacheItemPriority.Low) { return System.Web.Caching.CacheItemPriority.Low; }
            if (priority == CacheItemPriority.Normal) { return System.Web.Caching.CacheItemPriority.Normal; }

            return System.Web.Caching.CacheItemPriority.NotRemovable;
        }

    }
}
