using System;
using System.Collections.Generic;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 日志操作类
    /// 默认日志名称是 default
    /// 此类的接口方法只使用默认日志实现来输出
    /// </summary>
    /// <remarks>
    /// 如果用户要自己定义输出格式请指定<see cref="ILogFormatter"/>实现类
    /// </remarks>
    public class LogFactory : BaseFactory<ILog>
    {
        private static readonly LogFactory _instance = new LogFactory();
        
        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override ILog GetDefaultProvider()
        {
            return new FileLog();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal LogFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static LogFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static ILog Current
        {
            get { return _instance.Default; }
        }
    }
}