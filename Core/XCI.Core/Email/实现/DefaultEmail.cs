using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Collections;
using System.Net;
using System.Net.Mail;

namespace XCI.Component
{
    /// <summary>
    /// 发送电子邮件
    /// </summary>
    [XCIComponent(
        "邮件发送模块使用SmtpClient实现",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "2.1.0.0",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "邮件发送实现模块 使用 System.Net.Mail.SmtpClient 实现",
        "XCI.Email.EmailLogo.png")]
    public class DefaultEmail : EmailBase, IEmail
    {
        /// <summary>
        /// 获取协议对象
        /// </summary>
        /// <param name="entity">邮件实体</param>
        /// <param name="setting">服务器设置</param>
        protected SmtpClient ConfigClient(MailMessage entity, EmailSetting setting)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Host = setting.SmtpServer;
            smtpClient.Port = setting.Port;
            if (setting.IsAuthenticationRequired)
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(setting.AuthenticationUserName, setting.AuthenticationPassword);
            }
            smtpClient.Timeout = setting.Timeout;
            smtpClient.EnableSsl = setting.EnableSsl;
            return smtpClient;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="entity">邮件实体</param>
        /// <param name="setting">服务器设置</param>
        /// <returns>发送成功返回True</returns>
        public override bool Send(MailMessage entity, EmailSetting setting)
        {
            try
            {
                var smtpClient = ConfigClient(entity, setting);

                smtpClient.Send(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="entity">邮件实体</param>
        /// <param name="setting">服务器设置</param>
        /// <param name="userToken">一个用户定义对象，此对象将被传递给完成异步操作时所调用的方法。</param>
        /// <param name="sendCompletedCallback">在异步电子邮件发送操作完成时回调</param>
        public override void SendAsync(MailMessage entity, EmailSetting setting, object userToken, Action<AsyncCompletedEventArgs> sendCompletedCallback)
        {
            try
            {
                var smtpClient = ConfigClient(entity, setting);

                SendCompletedEventHandler completedCallback = (o, e) =>
                {
                    if (sendCompletedCallback != null)
                    {
                        sendCompletedCallback(e);
                    }
                };
                smtpClient.SendCompleted += completedCallback;
                smtpClient.SendAsync(entity, userToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
