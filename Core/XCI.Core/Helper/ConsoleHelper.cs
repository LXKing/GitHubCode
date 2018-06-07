using System.Runtime.InteropServices;

namespace XCI.Helper
{
    /// <summary>
    /// 控制台操作帮助类
    /// </summary>
    public class ConsoleHelper
    {
        /// <summary>
        /// 启动控制台
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        /// <summary>
        /// 释放控制台
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole(); 
    }
}