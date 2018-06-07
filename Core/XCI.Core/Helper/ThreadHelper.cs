using System;
using System.Diagnostics;
using System.Threading;

namespace XCI.Helper
{
    /// <summary>
    /// 多线程操作帮助类
    /// </summary>
    public static class ThreadHelper
    {
        /// <summary>
        /// 开始一个线程
        /// </summary>
        /// <param name="action">执行的代码</param>
        /// <param name="isBackground">是否是后台进程,默认是后台进程</param>
        public static void StartThread(Action action,bool isBackground = true)
        {
            Thread thread = new Thread(new ThreadStart(action));
            thread.IsBackground = isBackground;
            thread.Start();
        }

        /// <summary>
        /// 多次执行任务
        /// </summary>
        /// <param name="lastAction">最后执行</param>
        /// <param name="updateAction">循环执行</param>
        /// <param name="count">执行次数</param>
        public static void CountDownAction(Action lastAction, Action<int> updateAction, int count)
        {
            System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
            time.Interval = 1000;
            time.Enabled = true;
            time.Tick += (s, e) =>
            {
                //Debug.WriteLine(count);
                if (count <= 1)
                {
                    time.Enabled = false;
                    if (lastAction != null)
                    {
                        lastAction();
                    }
                    if (updateAction != null)
                    {
                        updateAction(0);
                    }
                }
                else
                {
                    if (updateAction != null)
                    {
                        updateAction(count);
                    }
                }
                count -= 1;
            };
        }
    }
}