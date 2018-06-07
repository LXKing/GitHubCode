using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataAccess.Logs
{
    /// <summary>
    ///     <appSettings>必须使用配置文件
    ///         <!--日志文件路径-->
    ///         <add key="LogPath" value="Logs\"/>
    ///         <!--日志文件的后缀名-->
    ///         <add key="LogExName" value=".log"/>
    ///     </appSettings>
    /// </summary>
    public class Log
    {
        private static string _path=@"C:\Logs";
        static Log()
        {
            var configPath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            _path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,configPath);
            _path = _path.Replace("/", @"\");
            if (_path[_path.Length - 1].ToString() != @"\")
            {
                _path += @"\";
            }
        }
        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="title"></param>
        public static void WriteLog(string title)
        {
            try
            {
                StringBuilder strBuild = new StringBuilder();
                strBuild.AppendFormat("时间:{1}    标题:[{0}]", title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF"));
                strBuild.AppendLine();
                strBuild.AppendLine(string.Format("     {0}. {1}", 0, GetMethodName(new object(),2)));
                WriteMsg(strBuild);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msgList"></param>
        public static void WriteLog(string title,IEnumerable<string> msgList=null)
        {
            try
            {
                StringBuilder strBuild = new StringBuilder();
                strBuild.AppendFormat("时间:{1}    标题:[{0}]", title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF"));
                strBuild.AppendLine();
                strBuild.AppendLine(string.Format("     {0}. {1}",0,GetMethodName(new object(),2)));
                int i=1;
                if (msgList!=null)
                {
                    msgList.ToList().ForEach(x =>
                    {
                        strBuild.AppendLine(string.Format("     {0}. {1}", i, x));
                        i++;
                    });
                }
                WriteMsg(strBuild);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selfDefineFileName"></param>
        /// <param name="title"></param>
        /// <param name="msgList"></param>
        public static void WriteLog(string title, IEnumerable<string> msgList=null,string selfDefineFileName="")
        {
            try
            {
                StringBuilder strBuild = new StringBuilder();
                strBuild.AppendFormat("时间:{1}    标题:[{0}]", title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF"));
                strBuild.AppendLine();
                strBuild.AppendLine(string.Format("     {0}. {1}", 0, GetMethodName(new object(),2)));
                int i = 1;
                if (msgList!=null)
                {
                    msgList.ToList().ForEach(x =>
                    {
                        strBuild.AppendLine(string.Format("     {0}. {1}", i, x));
                        i++;
                    });
                }
                
                WriteMsg(strBuild,selfDefineFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="exception"></param>
        public static void WriteException(string title,Exception exception)
        {
            try
            {
                StringBuilder strBuild = new StringBuilder();
                strBuild.AppendFormat("时间:{1}    标题:[{0}]", title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF"));
                strBuild.AppendLine();
                strBuild.AppendLine(string.Format("     {0}. {1}", 0, GetMethodName(new object(),2)));
                int i = 1;
                Exception exTmp = exception;
                bool haInnerException = true;
                while (haInnerException)
                {
                    strBuild.AppendLine(string.Format("     {0}. {1}", i, exTmp.Message));
                    if (exTmp.InnerException != null)
                    {
                        exTmp = exTmp.InnerException;
                        haInnerException = true;
                        i++;
                    }
                    else
                        haInnerException = false;
                }
                WriteMsg(strBuild);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// (日期-自定义名)自定义文件名的记录异常
        /// </summary>
        /// <param name="title">日志标题</param>
        /// <param name="exception">异常对象</param>
        /// <param name="selfDefineFileName">自定义的文件名</param>
        public static void WriteException(string title, Exception exception, string selfDefineFileName="")
        {
            try
            {
                StringBuilder strBuild = new StringBuilder();
                strBuild.AppendFormat("时间:{1}    标题:[{0}]", title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF"));
                strBuild.AppendLine();
                strBuild.AppendLine(string.Format("     {0}. {1}", 0, GetMethodName(new object(),2)));
                int i = 1;
                Exception exTmp = exception;
                bool haInnerException = true;
                while (haInnerException)
                {
                    strBuild.AppendLine(string.Format("     {0}. {1}", i, exTmp.Message));
                    if (exTmp.InnerException != null)
                    {
                        exTmp = exTmp.InnerException;
                        haInnerException = true;
                        i++;
                    }
                    else
                        haInnerException = false;
                }
                WriteMsg(strBuild,selfDefineFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 日志文件写消息
        /// </summary>
        /// <param name="strBuild"></param>
        /// <param name="selfDefineName"></param>
        private static void WriteMsg(StringBuilder strBuild,string selfDefineName="")
        {
            try
            {
                strBuild.AppendLine();
                var fileExtesion=".log";
                try
                {
                    fileExtesion=System.Configuration.ConfigurationManager.AppSettings["LogExName"];
                }
                catch(System.Configuration.ConfigurationErrorsException ex)
                {

                }
                string file = string.Format("{0}{1}{2}{3}", _path, System.DateTime.Now.ToString("yyyy-MM-dd"),selfDefineName, fileExtesion);
                DirectoryInfo dr = new DirectoryInfo(Path.GetDirectoryName(file));
                lock (dr)
                {
                    if (!dr.Exists)
                    {
                        dr.Create();
                    }
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(file, true))
                    {
                        sw.Write(strBuild.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取当前执行方法的类名(包含命名空间)
        /// </summary>
        /// <param name="o"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static  string GetClassName( object o, int n = 1)
        {
            try
            {
                StackTrace trace = new StackTrace();
                MethodBase method = trace.GetFrame(n).GetMethod();
                Type type = method.ReflectedType;
                string className = type.FullName;
                return className + ".cs";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取当前方法的名称(包含命名空间)
        /// </summary>
        /// <param name="o"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static  string GetMethodName( object o, int n = 1)
        {
            try
            {
                StackTrace trace = new StackTrace();
                MethodBase method = trace.GetFrame(n).GetMethod();
                Type type = method.ReflectedType;
                return string.Format("Function Name:{0}.{1}", type.FullName, method.Name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
