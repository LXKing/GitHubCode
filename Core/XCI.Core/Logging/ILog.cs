using System;

namespace XCI.Component
{
    /// <summary>
    /// 日志记录组件
    /// </summary>
    [XCIComponentDescription("日志记录组件", "系统组件")]
    public interface ILog : IManager
    {
        /// <summary>
        /// 获取或者设置日志输出级别
        /// </summary>
        LogLevel Level { get; set; }

        /// <summary>
        /// 获取或者设置日志格式
        /// </summary>
        ILogFormatter Formatter { get; set; }

        /// <summary>
        /// 记录调试消息
        /// </summary>
        /// <param name="message">消息内容</param>
        void Debug(string message);
        
        /// <summary>
        /// 记录调试消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="category">分类</param>
        void Debug(string message, string category);
        
        /// <summary>
        /// 记录普通消息
        /// </summary>
        /// <param name="message">消息内容</param>
        void Message(string message);
        
        /// <summary>
        /// 记录普通消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="category">分类</param>
        void Message(string message, string category);
        
        /// <summary>
        /// 记录错误消息
        /// </summary>
        /// <param name="message">消息内容</param>
        void Error(string message);
        
        /// <summary>
        /// 记录错误消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="category">分类</param>
        void Error(string message, string category);
    }
}