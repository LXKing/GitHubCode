using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using XCI.Extension;

namespace XCI.Core
{
    /// <summary>
    /// 存储列表消息和键/值消息实现
    /// </summary>
    public class Messages
    {
        private IList<string> _messageList;

        /// <summary>
        /// 消息列表
        /// </summary>
        private IList<string> MessageList
        {
            get
            {
                if (_messageList == null)
                {
                    _messageList = new List<string>();
                }
                return _messageList;
            }
        }


        /// <summary>
        /// 获取全部消息个数
        /// </summary>
        public int Count
        {
            get { return MessageList.Count; }
        }


        /// <summary>
        /// 是否存在消息
        /// </summary>
        public bool HasAny
        {
            get { return Count > 0; }
        }


        /// <summary>
        /// 添加简单消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public void Add(string message)
        {
            MessageList.Add(message);
        }


        /// <summary>
        /// 清除全部消息
        /// </summary>
        public void Clear()
        {
            MessageList.Clear();
        }


        /// <summary>
        /// 把当前消息复制到指定的消息中
        /// </summary>
        /// <param name="messages">复制到的消息对象</param>
        public void CopyTo(Messages messages)
        {
            if (messages == null)
            {
                return;
            }

            foreach (string error in MessageList)
            {
                messages.Add(error);
            }
        }


        /// <summary>
        /// 遍历所有消息
        /// </summary>
        /// <param name="callback">回调函数</param>
        public void EachFull(Action<string> callback)
        {
            foreach (string error in MessageList)
                callback(error);
        }


        /// <summary>
        /// 返回使用分隔符分割每个消息的字符串
        /// </summary>
        public string GetListString()
        {
            return GetListString(Environment.NewLine);
        }


        /// <summary>
        /// 返回使用分隔符分割每个消息的字符串
        /// </summary>
        /// <param name="separator">分隔符</param>
        public string GetListString(string separator)
        {
            StringBuilder buffer = new StringBuilder();

            foreach (string error in MessageList)
                buffer.Append(error + separator);
            
            return buffer.ToString();
        }


        /// <summary>
        /// 获取简单消息列表
        /// </summary>
        public IList<string> GetList()
        {
            if (MessageList == null)
            {
                return null;
            }

            return new ReadOnlyCollection<string>(MessageList);
        }

    }

}
