using System;
using System.Collections.Generic;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 实体元数据管理
    /// </summary>
    public class EntityMetadataFactory : BaseFactory<IEntityMetadata>
    {
        private static readonly EntityMetadataFactory _instance = new EntityMetadataFactory();
        
        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override IEntityMetadata GetDefaultProvider()
        {
            return new XmlEntityMetadata();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal EntityMetadataFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static EntityMetadataFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IEntityMetadata Current
        {
            get { return _instance.Default; }
        }
        
    }
}