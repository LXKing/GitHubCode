using System;
using System.Collections.Generic;
using System.Threading;

namespace XCI.Helper
{
    /// <summary>
    /// 定时任务管理    
    /// </summary>
    public static class TaskHelper
    {
        private static readonly List<Timer> TimerList = new List<Timer>();
        /// <summary>
        /// 开始启动任务
        /// </summary>
        /// <param name="interval">间隔时间(毫秒)</param>
        /// <param name="execMethod">要执行的方法</param>
        /// <returns>任务句柄 用于停止任务</returns>
        public static Timer Start(int interval, ExecMethod execMethod)
        {
            int isRunning=0;
            Timer timerItem = new Timer(delegate
            {
                if (Interlocked.Exchange(ref isRunning, 1) == 0)
                {
                    try
                    {
                        execMethod();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Interlocked.Exchange(ref isRunning, 0);
                    }
                }
            }, null, interval, interval);
            TimerList.Add(timerItem);
            return timerItem;
        }
        
        /// <summary>
        /// 停止计划任务
        /// </summary>
        /// <param name="timer">任务句柄</param>
        public static void Stop(Timer timer)
        {
            if (timer != null)
            {
                TimerList.Remove(timer);
                timer.Dispose();
                timer = null;
            }
        }
        /// <summary>
        /// 停止全部任务
        /// </summary>
        public static void StopAll()
        {
            while (TimerList.Count > 0)
            {
                Timer timer = TimerList[0];
                Stop(timer);
            }
        }
    }

    /// <summary>
    /// 每隔一段时间执行的函数
    /// </summary>
    public delegate void ExecMethod();
}