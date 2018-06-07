using System;

namespace XCI.Component
{
    /// <summary>
    /// 日志基类
    /// </summary>
    /// <remarks>
    /// 此类是抽象类 如果用户要自己实现 可以继承此类 并且实现LogCore方法 
    /// </remarks>
    public abstract class LogBase : ILog
    {
        private LogLevel _level = LogLevel.Debug;
        private ILogFormatter _formatter = new LogDefaultFormatter();

        /// <summary>
        /// 获取或者设置日志输出级别 默认输出级别 LogLevel.Debug
        /// </summary>
        public LogLevel Level
        {
            get { return _level; }
            set { _level = value; }
        }
        
        /// <summary>
        /// 获取或者设置日志格式
        /// </summary>
        public ILogFormatter Formatter
        {
            get { return _formatter; }
            set { _formatter = value; }
        }

        /// <summary>
        /// 是否输出调试消息
        /// </summary>
        public bool IsDebugEnabled { get { return IsEnabled(LogLevel.Debug); } }
        
        /// <summary>
        /// 是否输出普通消息
        /// </summary>
        public bool IsMessageEnabled { get { return IsEnabled(LogLevel.Message); } }

        /// <summary>
        /// 是否输出错误消息
        /// </summary>
        public bool IsErrorEnabled { get { return IsEnabled(LogLevel.Error); } }

        /// <summary>
        /// 测试指定的日志级别是否输出
        /// </summary>
        /// <param name="level">日志级别</param>
        public bool IsEnabled(LogLevel level)
        {
            return level >= this.Level;
        }

        /// <summary>
        /// 记录调试消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public void Debug(string message)
        {
            Debug(message, null);
        }

        /// <summary>
        /// 记录调试消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="category">分类</param>
        public void Debug(string message, string category)
        {
            LogCore(LogLevel.Debug, message, category);
        }

        /// <summary>
        /// 记录普通消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public void Message(string message)
        {
            Message(message, null);
        }

        /// <summary>
        /// 记录普通消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="category">分类</param>
        public void Message(string message, string category)
        {
            LogCore(LogLevel.Message, message, category);
        }

        /// <summary>
        /// 记录错误消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public void Error(string message)
        {
            Error(message, null);
        }

        /// <summary>
        /// 记录错误消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="category">分类</param>
        public void Error(string message, string category)
        {
            LogCore(LogLevel.Error, message, category);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">消息内容</param>
        /// <param name="category">分类</param>
        private void LogCore(LogLevel level, string message, string category)
        {
            if (!IsEnabled(level))
            {
                return;
            }
            LogEntity entity = new LogEntity();
            entity.Level = level;
            entity.Category = category;
            entity.Message = message;
            entity.CreateDateTime = DateTime.Now;
            entity.IP = "";
            LogCore(entity);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logEntity">日志实体</param>
        public abstract void LogCore(LogEntity logEntity);
    }
}