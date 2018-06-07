namespace XCI.Component
{
    /// <summary>
    /// 日志输出格式接口
    /// </summary>
    public interface ILogFormatter
    {
        /// <summary>
        /// 格式化日期输出
        /// </summary>
        /// <param name="entity">日志实体</param>
        /// <returns>返回格式化后的日志消息</returns>
        string Format(LogEntity entity);
    }
}