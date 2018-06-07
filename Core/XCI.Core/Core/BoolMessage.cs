using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace XCI.Core
{
    /// <summary>
    /// ״̬��Ϣ
    /// </summary>
    public class BoolMessage
    {
        /// <summary>
        /// �ɹ���Ϣ
        /// </summary>
        public static readonly BoolMessage True = new BoolMessage(true, string.Empty);


        /// <summary>
        /// ʧ����Ϣ
        /// </summary>
        public static readonly BoolMessage False = new BoolMessage(false, string.Empty);


        /// <summary>
        /// �Ƿ�ɹ�
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// ״̬��Ϣ
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public object Item
        {
            get;
            set;
        }

        /// <summary>
        /// Ĭ�Ϲ���
        /// </summary>
        public BoolMessage()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="success">�Ƿ�ɹ�</param>
        public BoolMessage(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="success">�Ƿ�ɹ�</param>
        /// <param name="message">״̬��Ϣ</param>
        public BoolMessage(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="success">�Ƿ�ɹ�</param>
        /// <param name="message">״̬��Ϣ</param>
        /// <param name="item">��������</param>
        public BoolMessage(bool success, string message,object item)
        {
            Success = success;
            Message = message;
            Item = item;
        }
    }

}
