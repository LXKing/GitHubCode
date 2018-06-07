using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using XCI.Component;
using XCI.Extension;

namespace XCI.Helper
{
    public class StartupHelper
    {
        /// <summary>
        /// 单个线程执行任务(不允许启动多个实例)
        /// </summary>
        /// <param name="name">互斥体名称</param>
        /// <param name="action">执行方法</param>
        /// <param name="message">当启动多个实例时的消息提示</param>
        /// <param name="isExitApp">是否退出应用程序</param>
        /// <param name="args">启动参数</param>
        public static void SingleStartup(string name, Action action, string message, bool isExitApp, string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                #region 重启等待

                if (args.Length > 0)
                {
                    if (args[0] == "/wait" && args.Length >= 2)
                    {
                        try
                        {
                            Process p = Process.GetProcessById(args[1].ToInt());
                            p.WaitForExit(); //等待终止
                        }
                        catch (Exception ex)
                        {
                            LogFactory.Current.Error(ex.Message);
                        }
                    }
                }

                #endregion


                bool mutexObject = false;
                Mutex mutex = new Mutex(true, name, out mutexObject);
                if (mutexObject)
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    //处理UI线程异常 
                    Application.ThreadException += Application_ThreadException;
                    //处理非UI线程异常  
                    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                    action();
                    mutex.ReleaseMutex();
                }
                else
                {
                    if (!String.IsNullOrEmpty(message))
                    {
                        MessageBoxHelper.ShowError(message); //"已经有一个客户端程序在运行了,一台计算机上不能同时运行多个客户端"
                    }
                    if (isExitApp)
                    {
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        /// <summary>
        /// 重启客户端
        /// </summary>
        public static void RestartApp()
        {
            Process.Start(Application.ExecutablePath, String.Format("/wait {0}", Process.GetCurrentProcess().Id));
        }

        public static void ExitApp()
        {
            //Application.Exit();
            Environment.Exit(0);
        }

        /// <summary>
        /// 显示异常对话框
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void ShowExceptionForm(Exception ex)
        {
            ExceptionForm exForm = new ExceptionForm();
            exForm.ExceptionObject = ex;
            exForm.ShowDialog();
            exForm.Dispose();
        }

        /// <summary>
        /// 系统级别异常捕获
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="e">异常对象</param>
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ProcessException(e.Exception);
            ShowExceptionForm(e.Exception);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ProcessException(e.ExceptionObject as Exception);
        }

        static void ProcessException(Exception ex)
        {
            //string message = String.Format("{0}\r\n系统捕获到一个未处理的异常,请您记录此消息,然后联系开发商,以解决此问题,谢谢合作\r\n{1}", ex.Message, DateTime.Now);
            //MessageBox.Show(message);
            LogFactory.Current.Error(ex.Message);
        }
    }
}