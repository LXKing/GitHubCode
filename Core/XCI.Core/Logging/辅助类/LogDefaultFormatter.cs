namespace XCI.Component
{
    /// <summary>
    /// 默认日志输出格式
    /// 默认格式是->yyyy-MM-dd HH:mm:ss 时间 级别 消息内容
    /// </summary>
    public class LogDefaultFormatter:ILogFormatter
    {
        /// <summary>
        /// 格式化日期输出
        /// </summary>
        /// <param name="entity">日志实体</param>
        /// <returns>返回格式化后的日志消息</returns>
        public string Format(LogEntity entity)
        {
            return string.Format("{0}  {1}  {2} ",
                entity.CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                entity.Level,
                entity.Message);
        }
    }
}