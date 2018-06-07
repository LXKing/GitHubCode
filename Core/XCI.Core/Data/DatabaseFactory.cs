using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 数据访问管理
    /// </summary>
    public class DatabaseFactory : BaseFactory<IDatabase>
    {
        private static readonly DatabaseFactory _instance = new DatabaseFactory();

        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override IDatabase GetDefaultProvider()
        {
            return new SqlServerDatabase();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal DatabaseFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static DatabaseFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IDatabase Current
        {
            get { return _instance.Default; }
        }
    }
}