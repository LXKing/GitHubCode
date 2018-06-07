using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Net.Mail;
namespace XCI.Component
{
    /// <summary>
    /// ���͵����ʼ����
    /// </summary>
    [XCIComponentDescription("���͵����ʼ����", "ϵͳ���")]
    public interface IEmail : IManager
    {
        /// <summary>
        /// �ʼ�����
        /// </summary>
        EmailSetting Setting { get; set; }

        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="recipients">�ռ���</param>
        /// <param name="subject">����</param>
        /// <param name="body">����</param>
        /// <returns>���ͳɹ�����True</returns>
        bool Send(string recipients, string subject, string body);
        
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="entity">�ʼ�ʵ��</param>
        /// <returns>���ͳɹ�����True</returns>
        bool Send(MailMessage entity);

        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="entity">�ʼ�ʵ��</param>
        /// <param name="setting">����������</param>
        /// <returns>���ͳɹ�����True</returns>
        bool Send(MailMessage entity, EmailSetting setting);

        /// <summary>
        /// �첽�����ʼ�
        /// </summary>
        /// <param name="entity">�ʼ�ʵ��</param>
        /// <param name="userToken">һ���û�������󣬴˶��󽫱����ݸ�����첽����ʱ�����õķ���</param>
        /// <param name="sendCompletedCallback">���첽�����ʼ����Ͳ������ʱ�ص�</param>
        void SendAsync(MailMessage entity, object userToken, Action<AsyncCompletedEventArgs> sendCompletedCallback);

        /// <summary>
        /// �첽�����ʼ�
        /// </summary>
        /// <param name="entity">�ʼ�ʵ��</param>
        /// <param name="setting">����������</param>
        /// <param name="userToken">һ���û�������󣬴˶��󽫱����ݸ�����첽����ʱ�����õķ���</param>
        /// <param name="sendCompletedCallback">���첽�����ʼ����Ͳ������ʱ�ص�</param>
        void SendAsync(MailMessage entity, EmailSetting setting, object userToken, Action<AsyncCompletedEventArgs> sendCompletedCallback);

    }
}
