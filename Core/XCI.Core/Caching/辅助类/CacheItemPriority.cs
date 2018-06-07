using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace XCI.Component
{
    /// <summary>
    /// 缓存优先级
    /// </summary>
    public enum CacheItemPriority
    {
        /// <summary>
        /// 在服务器释放系统内存时，具有该优先级级别的缓存项最有可能被从缓存删除
        /// </summary>
        Low,

        /// <summary>
        /// 在服务器释放系统内存时，具有该优先级级别的缓存项很有可能被从缓存删除，其被删除的可能性仅次于具有 CacheItemPriority.Low
        /// 优先级的那些项。这是默认选项。
        /// </summary>        
        Normal,

        /// <summary>
        /// 在服务器释放系统内存时，具有该优先级级别的缓存项最不可能被从缓存删除
        /// </summary>
        High,

        /// <summary>
        /// 在服务器释放系统内存时，具有该优先级级别的缓存项将不会被自动从缓存删除。但是，具有该优先级级别的项会根据项的绝对到期时间或可调整到期时间与其他项一起被移除
        /// </summary>
        NotRemovable,

        /// <summary>
        /// 缓存项优先级的默认值为 CacheItemPriority.Normal。
        /// </summary>
        Default = Normal
    }
}
