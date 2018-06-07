using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace XCI.Core
{
	/// <summary>
	/// 异常检测
	/// </summary>      
    public sealed class Guard
    {
        /// <summary>
        /// 测试条件是否为True 如果条件为False 抛出参数异常
        /// </summary>
        /// <param name="condition">测试条件</param>
        /// <param name="message">错误消息</param>
        public static void IsTrue(bool condition, string message="提供的条件是False")
        {
            if (condition == false)
            {
                throw new ArgumentException(message);
            }
        }


        /// <summary>
        /// 测试条件是否为False 如果条件为True 抛出参数异常
        /// </summary>
        /// <param name="condition">测试条件</param>
        /// <param name="message">错误消息</param>
        public static void IsFalse(bool condition, string message="提供的条件是True")
        {
            if (condition)
            {
                throw new ArgumentException(message);
            }
        }


        /// <summary>
        /// 测试对象不能为空 如果为空 抛出参数异常
        /// </summary>
        /// <param name="obj">测试对象</param>
        /// <param name="message">错误消息</param>
        public static void IsNotNull(object obj, string message="指定对象为空")
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message);
            }
        }
        

        /// <summary>
        /// 测试对象为空 如果不为空 抛出参数异常
        /// </summary>
        /// <param name="obj">测试对象</param>
        /// <param name="message">错误消息</param>
        public static void IsNull(object obj, string message = "指定对象不为空")
        {
            if (obj != null)
            {
                throw new ArgumentNullException(message);
            }
        }
        

        /// <summary>
        /// 测试文件是否存在 如果不存在 抛出异常
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="message">错误消息</param>
        public static void FileExists(string path,string message)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(message);
            }
        }

        /// <summary>
        /// 测试文件是否存在 如果存在 抛出异常
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="message">错误消息</param>
        public static void FileNotExists(string path, string message)
        {
            if (File.Exists(path))
            {
                throw new FileNotFoundException(message);
            }
        }
    }
   
}
