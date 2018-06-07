using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 加密解密操作
    /// </summary>
    public class EncryptFactory : BaseFactory<IEncrypt>
    {
        private static readonly EncryptFactory _instance = new EncryptFactory();
        
        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override IEncrypt GetDefaultProvider()
        {
            return new SymEncrypt();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal EncryptFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static EncryptFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IEncrypt Current
        {
            get { return _instance.Default; }
        }
    }
}
