using System;
using System.Runtime.InteropServices;

namespace XCI.Helper
{
    /// <summary>
    /// Win32函数调用
    /// </summary>
    public static class Win32Helper
    {
        /// <summary>
        /// 提取指定文件中的ICO图标
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="index">提取的图标索引 -1返回所有图标数量</param>
        /// <param name="largeHandle">大图标句柄</param>
        /// <param name="smallHandle">小图标句柄</param>
        /// <param name="nIcons"></param>
        /// <returns>图标索引 -1返回所有图标数量</returns>
        [DllImport("shell32.dll")]
        public static extern int ExtractIconEx(string filePath, int index, ref IntPtr largeHandle, ref IntPtr smallHandle, int nIcons);

        /// <summary>
        /// 销毁ICO
        /// </summary>
        /// <param name="handle">ICO句柄</param>
        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr handle);

        /// <summary>
        /// 提取指定大小的ICO图标
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="index">提取的图标索引</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="icoHandle">ICO句柄</param>
        /// <param name="iconid"></param>
        /// <param name="nIcons"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int PrivateExtractIcons(string filePath, int index, int width, int height, ref IntPtr icoHandle, ref int iconid, int nIcons, int flags);


        /// <summary>
        /// 该函数设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// <summary>
        /// 查找窗口
        /// </summary>
        /// <param name="lpClassName">窗口类名</param>
        /// <param name="lpWindowName">窗口标题名称</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 最大化窗口，最小窗口，正常大小窗口
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        /// <summary>
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        //注销
        [DllImport("user32.dll", EntryPoint = "ExitWindowsEx", CharSet = CharSet.Ansi)]
        public static extern int ExitWindowsEx(int uFlags, int dwReserved);

        //锁定
        [DllImport("User32.DLL")]
        public static extern void LockWorkStation();

        [DllImport("user32.dll")]
        public static extern int GetCursorPos(ref POINTAPI lpPoint);
        [DllImport("user32.dll")]
        public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        public static long GetLastInputTime()
        {
            LASTINPUTINFO structure = new LASTINPUTINFO();
            structure.cbSize = Marshal.SizeOf(structure);
            if (!GetLastInputInfo(ref structure))
            {
                return 0L;
            }
            return (Environment.TickCount - structure.dwTime);
        }

        #region SetWindowPos
        [DllImport("User32", SetLastError = true)]
        public static extern int SetWindowPos(
            IntPtr hWnd,//{窗口句柄}
            IntPtr hWndInsertAfter,//{窗口的 Z 顺序}
            int X,//{位置}
            int Y,//{位置}
            int cx,//{大小}
            int cy,//{大小}
            int uFlags);//{选项}

        ////hWndInsertAfter 参数可选值:
        //HWND_TOP       = 0;        {在前面}
        //HWND_BOTTOM    = 1;        {在后面}
        //HWND_TOPMOST   = HWND(-1); {在前面, 位于任何顶部窗口的前面}
        //HWND_NOTOPMOST = HWND(-2); {在前面, 位于其他顶部窗口的后面}

        ////uFlags 参数可选值:
        //SWP_NOSIZE         = 1;    {忽略 cx、cy, 保持大小}
        //SWP_NOMOVE         = 2;    {忽略 X、Y, 不改变位置}
        //SWP_NOZORDER       = 4;    {忽略 hWndInsertAfter, 保持 Z 顺序}
        //SWP_NOREDRAW       = 8;    {不重绘}
        //SWP_NOACTIVATE     = $10;  {不激活}
        //SWP_FRAMECHANGED   = $20;  {强制发送 WM_NCCALCSIZE 消息, 一般只是在改变大小时才发送此消息}
        //SWP_SHOWWINDOW     = $40;  {显示窗口}
        //SWP_HIDEWINDOW     = $80;  {隐藏窗口}
        //SWP_NOCOPYBITS     = $100; {丢弃客户区}
        //SWP_NOOWNERZORDER  = $200; {忽略 hWndInsertAfter, 不改变 Z 序列的所有者}
        //SWP_NOSENDCHANGING = $400; {不发出 WM_WINDOWPOSCHANGING 消息}
        //SWP_DRAWFRAME      = SWP_FRAMECHANGED; {画边框}
        //SWP_NOREPOSITION   = SWP_NOOWNERZORDER;{}
        //SWP_DEFERERASE     = $2000;            {防止产生 WM_SYNCPAINT 消息}
        //SWP_ASYNCWINDOWPOS = $4000;            {若调用进程不拥有窗口, 系统会向拥有窗口的线程发出需求}

        #endregion

        [DllImport("kernel32.dll")]
        private static extern bool SetLocalTime(ref DT dt);
        /// <summary>
        /// 设置本地日期时间
        /// </summary>
        /// <param name="dataTime">日期</param>
        public static void SetLocalDateTime(DateTime dataTime)
        {
            DT dt = new DT();
            dt.wYear = short.Parse(dataTime.Year.ToString());
            dt.wMonth = short.Parse(dataTime.Month.ToString());
            dt.wDay = short.Parse(dataTime.Day.ToString());
            dt.wHour = short.Parse(dataTime.Hour.ToString());
            dt.wMinute = short.Parse(dataTime.Minute.ToString());
            dt.wSecond = short.Parse(dataTime.Second.ToString());
            SetLocalTime(ref dt);
        }
        public class POINTAPI
        {
            public int x;
            public int y;
        }
        public struct DT
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMillisecond;
        }
        [StructLayout(LayoutKind.Sequential)]
        public class SystemTime
        {
            public ushort year;
            public ushort month;
            public ushort dayofweek;
            public ushort day;
            public ushort hour;
            public ushort minute;
            public ushort second;
            public ushort milliseconds;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }
    }
}