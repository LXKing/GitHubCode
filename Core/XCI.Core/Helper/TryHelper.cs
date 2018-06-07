using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using XCI.Component;
using XCI.Core;

namespace XCI.Helper
{
    /// <summary>
    /// 异常操作帮辅助类
    /// </summary>
    public class TryHelper
    {
        /// <summary>
        /// 捕获异常并记录日志
        /// </summary>
        /// <param name="action">执行函数</param>
        /// <param name="logCategory">日志类型</param>
        /// <param name="logMessage">日志消息</param>
        /// <param name="rethrow">是否抛出异常</param>
        public static void CatchLog(Action action, string logCategory = null, string logMessage = null, bool rethrow = false)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                string msg = logMessage ?? ex.Message;
                LogFactory.Current.Error(msg, logCategory);
                if (rethrow) throw ex;
            }
        }
        

        /// <summary>
        /// 捕获异常
        /// </summary>
        /// <param name="action">执行函数</param>
        /// <param name="exceptionHandler">异常执行函数</param>
        public static void Catch(Action action, Action<Exception> exceptionHandler = null)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null)
                    exceptionHandler(ex);
            }
        }


        /// <summary>
        /// 捕获异常
        /// </summary>
        /// <param name="action">执行函数</param>
        /// <param name="exceptionHandler">异常执行函数</param>
        /// <param name="finallyHandler">清理执行函数</param>
        public static void Catch(Action action, Action<Exception> exceptionHandler, Action finallyHandler)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null)
                    exceptionHandler(ex);
            }
            finally
            {
                if (finallyHandler != null)
                    finallyHandler();
            }
        }



        /// <summary>
        /// 捕获异常 返回对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="action">执行函数</param>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="rethrow">是否抛出异常</param>
        public static T CatchLogGet<T>(Func<T> action, string errorMessage = null, bool rethrow = false)
        {
            T result = default(T);
            try
            {
                result = action();
            }
            catch (Exception ex)
            {
                string msg = errorMessage?? ex.Message;
                LogFactory.Current.Error(msg, null);
                if (rethrow) throw ex;
            }
            return result;
        }



        /// <summary>
        /// 捕获异常 返回对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="action">执行函数</param>
        /// <param name="errorMessage">错误消息</param>
        public static BoolMessage CatchLogGetBoolResult<T>(Func<T> action,string errorMessage=null)
        {
            T result = default(T);
            bool success = false;
            string message = string.Empty;
            try
            {
                result = action();
                success = true;
            }
            catch (Exception ex)
            {
                string msg = errorMessage ?? ex.Message;
                LogFactory.Current.Error(msg, null);
                message = ex.Message;
            }
            return new BoolMessage(success, message, result);
        }


        /// <summary>
        /// 捕获异常 返回异常对象
        /// </summary>
        /// <param name="action">执行函数</param>
        /// <param name="errorMessage">错误消息</param>
        public static BoolMessage CatchLogGetBoolResultEx(Action action,string errorMessage=null)
        {
            string finalMessage = string.Empty;
            bool success = false;
            Exception ex = null;
            try
            {
                action();
                success = true;
            }
            catch (Exception exception)
            {
                success = false;
                finalMessage = errorMessage ?? exception.Message;
                ex = exception;
                LogFactory.Current.Error(finalMessage, null);
            }

            return new BoolMessage(success, finalMessage, ex);
        }

    }
}
