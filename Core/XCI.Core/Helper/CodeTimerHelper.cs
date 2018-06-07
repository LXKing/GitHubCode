using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace XCI.Helper
{
    /// <summary>
    /// 代码执行时间统计帮助类
    /// </summary>
    public static class CodeTimerHelper
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Time("", 1, () => { });
        }


        /// <summary>
        /// 统计时间
        /// </summary>
        /// <param name="name">统计名</param>
        /// <param name="iteration">循环次数</param>
        /// <param name="action">测试的代码</param>
        public static void Time(string name, int iteration, Action action)
        {
            Time(name, iteration, action, Console.WriteLine);
        }


        /// <summary>
        /// 统计时间
        /// </summary>
        /// <param name="name">统计名</param>
        /// <param name="iteration">循环次数</param>
        /// <param name="action">测试的代码</param>
        /// <param name="output">输出结果</param>
        public static void Time(string name, int iteration, Action action, Action<string> output)
        {
            if (String.IsNullOrEmpty(name)) return;

            // 1.
            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            output(name);

            // 2.
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            // 3.
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ulong cycleCount = GetCycleCount();
            for (int i = 0; i < iteration; i++) action();
            ulong cpuCycles = GetCycleCount() - cycleCount;
            watch.Stop();

            // 4.
            Console.ForegroundColor = currentForeColor;
            output("\tTime Elapsed:\t" + watch.ElapsedMilliseconds.ToString("N0") + "ms");
            output("\tCPU Cycles:\t" + cpuCycles.ToString("N0"));

            // 5.
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                output("\tGen " + i + ": \t\t" + count);
            }

            output(string.Empty);
        }


        /// <summary>
        /// 获取循环次数
        /// </summary>
        private static ulong GetCycleCount()
        {
            ulong cycleCount = 0;
            QueryThreadCycleTime(GetCurrentThread(), ref cycleCount);
            return cycleCount;
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentThread();
    }
}
