using System;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// 调试管理
    /// </summary>
    public static class DebugHelper
    {
        /// <summary>
        /// 创建详细的异常信息
        /// </summary>
        /// <param name="exception">异常实例</param>
        /// <returns>
        /// 返回错误信息字符串
        /// <![CDATA[
        /// <strong style='color: red'>当前的异常信息:</strong>
        /// <strong style='color: red'>异常助链接:</strong>
        /// <strong style='color: red'>导致异常的对象源:</strong>
        /// <strong style='color: red'>引发当前异常的方法:</strong>
        /// <strong style='color: red'>当前异常发生时调用堆栈上的帧的字符串表示形式:</strong>
        /// ]]>
        /// </returns>
        public static string CreateError(Exception exception)
        {
            return "<strong style='color: red'>当前的异常信息:</strong>" + ReplaceExceptionMsg(exception.Message) + "<br><strong style='color: red'>异常助链接:</strong>" + exception.HelpLink + "<br><strong style='color: red'>导致异常的对象源:</strong>" + exception.Source + "<br><strong style='color: red'>引发当前异常的方法:</strong>" + exception.TargetSite + "<br><strong style='color: red'>当前异常发生时调用堆栈上的帧的字符串表示形式:</strong>" + exception.StackTrace;
        }

        /// <summary>
        /// 添加调试信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">db参数</param>
        public static void AddDebug(string title, string sql, DbParameterCollection parameters)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("------ ChinaSoft 项目调试信息 ------\n");
            if (title != null) sb.Append(title);
            //sb.Append("\n");
            if (sql != null) sb.Append("\nsql命令->" + sql);
            sb.Append("\n");
            sb.Append(GetDbParameterListAndValue(parameters));
            Debug.WriteLine(sb.ToString());
        }


        /// <summary>
        /// 添加调试异常信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="e">异常对象</param>
        public static void AddDebug(string title, Exception e)
        {
            StringBuilder sb = new StringBuilder();
            if (e != null)
            {
                if (title != null) sb.Append(string.Format("\n------ {0}时发生异常  ------\n", title));
                sb.Append(GetExceptionString(e));
                if (title != null) sb.Append(string.Format("\n------ {0}时发生异常  ------\n", title));
                Debug.WriteLine(sb.ToString());
            }
        }


        /// <summary>
        /// 替换异常信息中的特殊字符
        /// </summary>
        /// <param name="msg">异常信息</param>
        private static string ReplaceExceptionMsg(string msg)
        {
            return msg.Replace("'", "").Replace("\"", "").Replace("\r", "").Replace("\n", "");
        }


        /// <summary>
        /// 获取异常信息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetExceptionString(Exception e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--- 错误信息 ---\n");
            sb.Append(e.Message);
            sb.Append("\n--- 导致错误的应用程序或对象的名称 ---\n");
            sb.Append(e.Source);
            sb.Append("\n--- 引发当前异常的方法 ---\n");
            sb.Append(e.TargetSite.Name);
            sb.Append("\n--- 当前异常发生时调用堆栈上的帧的字符串表示形式 ---\n");
            sb.Append(e.StackTrace);
            return sb.ToString();
        }


        /// <summary>
        /// 把参数集合转换为字符串
        /// </summary>
        /// <param name="param">参数集合</param>
        /// <returns></returns>
        public static string GetDbParameterListAndValue(DbParameterCollection param)
        {
            if (param == null || param.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            //sb.Append("\n");
            foreach (DbParameter item in param)
            {
                sb.Append(item.ParameterName);
                sb.Append(" ->");
                sb.Append(" 值:");
                sb.Append(item.Value);
                sb.Append(" 类型:");
                sb.Append(item.DbType.ToString());
                sb.Append(" 大小:");
                sb.Append(item.Size);
                sb.Append(" 输入输出:");
                sb.Append(item.Direction.ToString());
                sb.Append("\n");
            }
            sb.Append("\n");
            return sb.ToString();
        }


        /// <summary>
        /// 开始调试
        /// </summary>
        /// <param name="methodBase">方法信息</param>
        /// <returns>获取系统启动后经过的毫秒数</returns>
        public static int StartDebug(MethodBase methodBase)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " :Begin: " + methodBase.ReflectedType.Name + "." + methodBase.Name);
            Trace.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " :Begin: " + methodBase.ReflectedType.Name + "." + methodBase.Name);
            return Environment.TickCount;
        }


        /// <summary>
        /// 结束调试
        /// </summary>
        /// <param name="methodBase">方法信息</param>
        /// <param name="milliStart">开始毫秒</param>
        /// <returns>执行时间</returns>
        public static int EndDebug(MethodBase methodBase, int milliStart)
        {
            int tickCount = Environment.TickCount;
            string[] strArray1 = new string[7];
            string[] strArray2 = strArray1;
            DateTime now = DateTime.Now;
            string str1 = now.ToString("yyyy-MM-dd HH:mm:ss");
            strArray2[0] = str1;
            strArray1[1] = " Ticks: ";
            strArray1[2] = TimeSpan.FromMilliseconds((tickCount - milliStart)).ToString();
            strArray1[3] = " :End: ";
            strArray1[4] = methodBase.ReflectedType.Name;
            strArray1[5] = ".";
            strArray1[6] = methodBase.Name;
            Console.WriteLine(string.Concat(strArray1));
            string[] strArray3 = new string[7];
            string[] strArray4 = strArray3;
            now = DateTime.Now;
            string str2 = now.ToString("yyyy-MM-dd HH:mm:ss");
            strArray4[0] = str2;
            strArray3[1] = " Ticks: ";
            strArray3[2] = TimeSpan.FromMilliseconds((tickCount - milliStart)).ToString();
            strArray3[3] = " :End: ";
            strArray3[4] = methodBase.ReflectedType.Name;
            strArray3[5] = ".";
            strArray3[6] = methodBase.Name;
            Trace.WriteLine(string.Concat(strArray3));
            return tickCount - milliStart;
        }

    }
}
