namespace XCI.Component
{
    /// <summary>
    /// 日志输出到控制台
    /// </summary>
    [XCIComponent(
        "控制台日志",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "2.1.4243.36769",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "输出日志到控制台",
        "XCI.Logging.LoggingLogo.png")]
    public class ConsoleLog : LogBase, ILog
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logEntity">日志实体</param>
        public override void LogCore(LogEntity logEntity)
        {
            string outMessage = logEntity.Message;
            if (base.Formatter!=null)
            {
                outMessage = "XCILog " + base.Formatter.Format(logEntity);
            }
            System.Diagnostics.Debug.WriteLine(outMessage);
        }
    }
}