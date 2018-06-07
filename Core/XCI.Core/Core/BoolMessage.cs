using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace XCI.Core
{
    /// <summary>
    /// 状态消息
    /// </summary>
    public class BoolMessage
    {
        /// <summary>
        /// 成功消息
        /// </summary>
        public static readonly BoolMessage True = new BoolMessage(true, string.Empty);


        /// <summary>
        /// 失败消息
        /// </summary>
        public static readonly BoolMessage False = new BoolMessage(false, string.Empty);


        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 状态消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 附带对象
        /// </summary>
        public object Item
        {
            get;
            set;
        }

        /// <summary>
        /// 默认构造
        /// </summary>
        public BoolMessage()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="success">是否成功</param>
        public BoolMessage(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="message">状态消息</param>
        public BoolMessage(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="message">状态消息</param>
        /// <param name="item">附带对象</param>
        public BoolMessage(bool success, string message,object item)
        {
            Success = success;
            Message = message;
            Item = item;
        }
    }

}
