using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace XCI.Core
{
	/// <summary>
	/// �쳣���
	/// </summary>      
    public sealed class Guard
    {
        /// <summary>
        /// ���������Ƿ�ΪTrue �������ΪFalse �׳������쳣
        /// </summary>
        /// <param name="condition">��������</param>
        /// <param name="message">������Ϣ</param>
        public static void IsTrue(bool condition, string message="�ṩ��������False")
        {
            if (condition == false)
            {
                throw new ArgumentException(message);
            }
        }


        /// <summary>
        /// ���������Ƿ�ΪFalse �������ΪTrue �׳������쳣
        /// </summary>
        /// <param name="condition">��������</param>
        /// <param name="message">������Ϣ</param>
        public static void IsFalse(bool condition, string message="�ṩ��������True")
        {
            if (condition)
            {
                throw new ArgumentException(message);
            }
        }


        /// <summary>
        /// ���Զ�����Ϊ�� ���Ϊ�� �׳������쳣
        /// </summary>
        /// <param name="obj">���Զ���</param>
        /// <param name="message">������Ϣ</param>
        public static void IsNotNull(object obj, string message="ָ������Ϊ��")
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message);
            }
        }
        

        /// <summary>
        /// ���Զ���Ϊ�� �����Ϊ�� �׳������쳣
        /// </summary>
        /// <param name="obj">���Զ���</param>
        /// <param name="message">������Ϣ</param>
        public static void IsNull(object obj, string message = "ָ������Ϊ��")
        {
            if (obj != null)
            {
                throw new ArgumentNullException(message);
            }
        }
        

        /// <summary>
        /// �����ļ��Ƿ���� ��������� �׳��쳣
        /// </summary>
        /// <param name="path">�ļ�·��</param>
        /// <param name="message">������Ϣ</param>
        public static void FileExists(string path,string message)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(message);
            }
        }

        /// <summary>
        /// �����ļ��Ƿ���� ������� �׳��쳣
        /// </summary>
        /// <param name="path">�ļ�·��</param>
        /// <param name="message">������Ϣ</param>
        public static void FileNotExists(string path, string message)
        {
            if (File.Exists(path))
            {
                throw new FileNotFoundException(message);
            }
        }
    }
   
}
