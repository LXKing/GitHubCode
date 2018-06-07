using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 邮件发送管理
    /// </summary>
    public class EmailFactory : BaseFactory<IEmail>
    {
        private static readonly EmailFactory _instance = new EmailFactory();
        
        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override IEmail GetDefaultProvider()
        {
            return new DefaultEmail();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal EmailFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static EmailFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IEmail Current
        {
            get { return _instance.Default; }
        }
    }
}