using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Net.Mail;
namespace XCI.Component
{
    /// <summary>
    /// 发送电子邮件组件
    /// </summary>
    [XCIComponentDescription("发送电子邮件组件", "系统组件")]
    public interface IEmail : IManager
    {
        /// <summary>
        /// 邮件设置
        /// </summary>
        EmailSetting Setting { get; set; }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="recipients">收件人</param>
        /// <param name="subject">标题</param>
        /// <param name="body">正文</param>
        /// <returns>发送成功返回True</returns>
        bool Send(string recipients, string subject, string body);
        
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="entity">邮件实体</param>
        /// <returns>发送成功返回True</returns>
        bool Send(MailMessage entity);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="entity">邮件实体</param>
        /// <param name="setting">服务器设置</param>
        /// <returns>发送成功返回True</returns>
        bool Send(MailMessage entity, EmailSetting setting);

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="entity">邮件实体</param>
        /// <param name="userToken">一个用户定义对象，此对象将被传递给完成异步操作时所调用的方法</param>
        /// <param name="sendCompletedCallback">在异步电子邮件发送操作完成时回调</param>
        void SendAsync(MailMessage entity, object userToken, Action<AsyncCompletedEventArgs> sendCompletedCallback);

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="entity">邮件实体</param>
        /// <param name="setting">服务器设置</param>
        /// <param name="userToken">一个用户定义对象，此对象将被传递给完成异步操作时所调用的方法</param>
        /// <param name="sendCompletedCallback">在异步电子邮件发送操作完成时回调</param>
        void SendAsync(MailMessage entity, EmailSetting setting, object userToken, Action<AsyncCompletedEventArgs> sendCompletedCallback);

    }
}
