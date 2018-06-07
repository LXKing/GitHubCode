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
    /// 发送电子邮件基类
    /// </summary>
    public abstract class EmailBase : IEmail
    {
        /// <summary>
        /// 邮件设置
        /// </summary>
        public EmailSetting Setting { get; set; }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="recipients">收件人</param>
        /// <param name="subject">标题</param>
        /// <param name="body">正文</param>
        /// <returns>发送成功返回True</returns>
        public bool Send(string recipients, string subject, string body)
        {
            MailMessage entity = new MailMessage(Setting.From, recipients, subject, body);
            entity.IsBodyHtml = true;
            return Send(entity);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="entity">邮件实体</param>
        /// <returns>发送成功返回True</returns>
        public bool Send(MailMessage entity)
        {
            return Send(entity, Setting);
        }

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="entity">邮件实体</param>
        /// <param name="userToken">一个用户定义对象，此对象将被传递给完成异步操作时所调用的方法</param>
        /// <param name="sendCompletedCallback">在异步电子邮件发送操作完成时回调</param>
        public void SendAsync(MailMessage entity, object userToken, Action<AsyncCompletedEventArgs> sendCompletedCallback)
        {
            SendAsync(entity, Setting, userToken,sendCompletedCallback);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="entity">邮件实体</param>
        /// <param name="setting">服务器设置</param>
        /// <returns>发送成功返回True</returns>
        public abstract bool Send(MailMessage entity, EmailSetting setting);

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="entity">邮件实体</param>
        /// <param name="setting">服务器设置</param>
        /// <param name="userToken">一个用户定义对象，此对象将被传递给完成异步操作时所调用的方法</param>
        /// <param name="sendCompletedCallback">在异步电子邮件发送操作完成时回调</param>
        public abstract void SendAsync(MailMessage entity, EmailSetting setting, object userToken, Action<AsyncCompletedEventArgs> sendCompletedCallback);

    }
}