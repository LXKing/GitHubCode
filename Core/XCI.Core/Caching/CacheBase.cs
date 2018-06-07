using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Caching;

namespace XCI.Component
{
    /// <summary>
    /// 缓存基类
    /// </summary>
    public abstract class CacheBase : ICache
    {
        private int _defaultSlidingExpiration = 20;

        /// <summary>
        /// 获取或者设置最后一次访问所插入对象时与该对象到期时之间的时间间隔(单位分钟)(现在时间与最后一次访问缓存对象的时间间隔超过指定间隔 则缓存对象过过期) 不过期TimeSpan.Zero
        /// </summary>
        public int DefaultSlidingExpiration
        {
            get { return _defaultSlidingExpiration; }
            set { _defaultSlidingExpiration = value; }
        }

        /// <summary>
        /// 获取缓存项数量
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// 获取全部缓存项的键集合
        /// </summary>
        public abstract string[] Keys { get; }

        /// <summary>
        /// 添加一个缓存对象(键值不能重复)
        /// </summary>
        /// <param name="key">缓存项键</param>
        /// <param name="value">缓存项值</param>
        public void Add(string key, object value)
        {
            Add(key, value, new TimeSpan(DefaultSlidingExpiration));
        }

        /// <summary>
        /// 添加一个缓存对象(键值不能重复)
        /// </summary>
        /// <param name="key">缓存项键</param>
        /// <param name="value">缓存项值</param>
        /// <param name="slidingExpiration">最后一次访问所插入对象时与该对象到期时之间的时间间隔(单位分钟)(现在时间与最后一次访问缓存对象的时间间隔超过指定间隔 则缓存对象过过期) 不过期TimeSpan.Zero</param>
        public void Add(string key, object value, TimeSpan slidingExpiration)
        {
            Add(key, value, DateTime.MaxValue, slidingExpiration, CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// 添加一个缓存对象(键值不能重复)
        /// </summary>
        /// <param name="key">缓存项键</param>
        /// <param name="value">缓存项值</param>
        /// <param name="absoluteExpiration">绝对到期时间 如果指定此时间 则slidingExpiration必须是TimeSpan.Zero</param>
        /// <param name="slidingExpiration">最后一次访问所插入对象时与该对象到期时之间的时间间隔(现在时间与最后一次访问缓存对象的时间间隔超过指定间隔 则缓存对象过过期) 如果指定此间隔 则absoluteExpiration必须是DateTime.MaxValue</param>
        /// <param name="priority">优先级</param>
        /// <param name="onRemoveCallback">移除缓存项时通知应用程序的回调方法</param>
        public abstract void Add(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority,
                                 Action<string, object, CacheItemRemovedReason> onRemoveCallback);

        /// <summary>
        /// 清空全部缓存项
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// 是否存在指定键值的缓存项
        /// </summary>
        /// <param name="key">缓存项键</param>
        /// <returns>存在返回True</returns>
        public abstract bool Contains(string key);

        /// <summary>
        /// 移除指定键值的缓存项
        /// </summary>
        /// <param name="key">缓存项键</param>
        public abstract object Remove(string key);

        /// <summary>
        /// 返回指定键值的缓存项
        /// </summary>
        /// <param name="key">缓存项键</param>
        /// <returns>从 Cache 移除的项。如果未找到键参数中的值，则返回 null。</returns>
        public abstract object Get(string key);

        /// <summary>
        /// 返回指定键值的缓存项
        /// 如果不存在,则使用指定的Lamda 表达式创建缓存项,并添加到缓存中
        /// </summary>
        /// <param name="key">缓存项键</param>
        /// <param name="slidingExpiration">最后一次访问所插入对象时与该对象到期时之间的时间间隔(单位分钟)(现在时间与最后一次访问缓存对象的时间间隔超过指定间隔 则缓存对象过) 不过期TimeSpan.Zero</param>
        /// <param name="onUpdateCallback">从缓存中获取对象时 如果对象失效 自动获取对象到缓存</param>
        public object TryGet(string key, TimeSpan slidingExpiration, Func<object> onUpdateCallback)
        {
            object obj = Get(key);
            if (obj == null && onUpdateCallback != null)
            {
                obj = onUpdateCallback();
                Add(key, obj, slidingExpiration);
            }

            return obj;
        }

        /// <summary>
        /// 返回指定键值的缓存项
        /// 如果不存在,则使用指定的Lamda 表达式创建缓存项,并添加到缓存中
        /// </summary>
        /// <param name="key">缓存项键</param>
        /// <param name="absoluteExpiration">绝对到期时间 如果指定此时间 则slidingExpiration必须是TimeSpan.Zero</param>
        /// <param name="onUpdateCallback">从缓存中获取对象时 如果对象失效 自动获取对象到缓存</param>
        public object TryGet(string key, DateTime absoluteExpiration, Func<object> onUpdateCallback)
        {
            object obj = Get(key);
            if (obj == null && onUpdateCallback != null)
            {
                obj = onUpdateCallback();
                Add(key, obj, absoluteExpiration, TimeSpan.Zero, CacheItemPriority.Default, null);
            }

            return obj;
        }
    }
}