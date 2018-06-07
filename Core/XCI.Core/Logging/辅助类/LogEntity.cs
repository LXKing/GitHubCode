using System;

namespace XCI.Component
{
    /// <summary>
    /// 日志实体
    /// </summary>
    [Serializable]
    public class LogEntity
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel Level { get; set; }


        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }


        /// <summary>
        /// 日志消息
        /// </summary>
        public string Message { get; set; }
        

        /// <summary>
        /// 操作计算机
        /// </summary>
        public string IP { get; set; }


        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDateTime { get; set; }
    }
}