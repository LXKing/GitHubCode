namespace XCI.Component
{
    /// <summary>
    /// 日志级别
    /// 输出顺序 Debug -> Message -> Error
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 调试
        /// </summary>
        Debug = 0,
        
        /// <summary>
        /// 消息
        /// </summary>
        Message = 1,
        
        /// <summary>
        /// 错误
        /// </summary>
        Error = 2,

        /// <summary>
        /// 不输出
        /// </summary>
        None = 3
    }
}