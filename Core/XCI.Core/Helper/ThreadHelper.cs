using System;
using System.Diagnostics;
using System.Threading;

namespace XCI.Helper
{
    /// <summary>
    /// ���̲߳���������
    /// </summary>
    public static class ThreadHelper
    {
        /// <summary>
        /// ��ʼһ���߳�
        /// </summary>
        /// <param name="action">ִ�еĴ���</param>
        /// <param name="isBackground">�Ƿ��Ǻ�̨����,Ĭ���Ǻ�̨����</param>
        public static void StartThread(Action action,bool isBackground = true)
        {
            Thread thread = new Thread(new ThreadStart(action));
            thread.IsBackground = isBackground;
            thread.Start();
        }

        /// <summary>
        /// ���ִ������
        /// </summary>
        /// <param name="lastAction">���ִ��</param>
        /// <param name="updateAction">ѭ��ִ��</param>
        /// <param name="count">ִ�д���</param>
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