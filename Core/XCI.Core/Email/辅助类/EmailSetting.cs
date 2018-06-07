using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using XCI.Helper;

namespace XCI.Component
{
    /// <summary>
    /// 邮件设置
    /// </summary>
    [Serializable]
    public class EmailSetting
    {
        /// <summary>
        /// 获取或设置SMTP服务器
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// 发送者地址
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 获取或设置授权用户名
        /// </summary>
        public string AuthenticationUserName { get; set; }

        /// <summary>
        /// 获取或设置授权用户密码
        /// </summary>
        public string AuthenticationPassword { get; set; }

        /// <summary>
        /// 获取或设置是否需要授权验证
        /// </summary>
        public bool IsAuthenticationRequired { get; set; }

        private int _port = 25;

        /// <summary>
        /// 端口号 默认25
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        /// <summary>
        /// 是否使用安全套接字层 (SSL) 加密连接
        /// </summary>
        public bool EnableSsl { get; set; }

        private int _timeout = 100;

        /// <summary>
        /// 发送超时时间 默认值为100 秒
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }
    }
}
