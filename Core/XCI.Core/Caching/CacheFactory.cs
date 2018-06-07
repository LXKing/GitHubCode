using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.Caching;
using System.Web;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 缓存管理
    /// </summary>
    public class CacheFactory : BaseFactory<ICache>
    {
        private static readonly CacheFactory _instance = new CacheFactory();
        
        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override ICache GetDefaultProvider()
        {
            return new HttpRuntimeCache();
        }
        
        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal CacheFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static CacheFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static ICache Current
        {
            get { return _instance.Default; }
        }
    }
}
