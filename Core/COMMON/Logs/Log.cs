using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace COMMON.Logs
{
    /// <summary>
    ///     <appSettings>使用配置文件,不适用默认在操作系统盘符根目录下的Logs文件夹下
    ///         <!--日志文件路径-->
    ///         <add key="LogPath" value="Logs\"/>
    ///         <!--日志文件的后缀名-->
    ///         <add key="LogExName" value=".log"/>
    ///     </appSettings>
    /// </summary>
    public class Log
    {
        private static string _path=@"Logs";
        private static string _LogExName = ".log";
        static Log()
        {
            int a = Environment.SystemDirectory.Length;
            _path = Environment.SystemDirectory.Remove(3, a - 3) + _path;

            var configPath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            if(!configPath.IsNullOrEmpty())
            {
                if(!configPath.Contains(@":\"))
                {
                    _path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, configPath);
                }
                else
                {
                    _path = configPath;
                }
            }
            
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
                strBuild.AppendFormat("时间:{1}    标题:[{0}]", title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                strBuild.AppendLine();
                strBuild.AppendLine(string.Format("     {0}. {1}", 0, new object().GetMethodName(2)));
                WriteMsg(strBuild);
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
        public static void WriteLog(string title, string msg, string selfDefineFileName = "")
        {
            try
            {
                StringBuilder strBuild = new StringBuilder();
                strBuild.AppendFormat("时间:{1}    标题:[{0}]", title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                strBuild.AppendLine();
                strBuild.AppendFormat("     {0}. {1}", 0, new object().GetMethodName(2));
                strBuild.AppendLine();
                strBuild.AppendFormat("     {0}. {1}", 1, msg);
                strBuild.AppendLine();
                WriteMsg(strBuild,selfDefineFileName);
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
                strBuild.AppendFormat("时间:{1}    标题:[{0}]", title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                strBuild.AppendLine();
                strBuild.AppendLine(string.Format("     {0}. {1}",0,new object().GetMethodName(2)));
                int i=1;
                if (msgList.NotNull())
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
                strBuild.AppendFormat("时间:{1}    标题:[{0}]", title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                strBuild.AppendLine();
                strBuild.AppendLine(string.Format("     {0}. {1}", 0, new object().GetMethodName(2)));
                int i = 1;
                if (msgList.NotNull())
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
                strBuild.AppendFormat("时间:{1}    标题:[{0}]", title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                strBuild.AppendLine();
                strBuild.AppendLine(string.Format("     {0}. {1}", 0, new object().GetMethodName(2)));
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
                strBuild.AppendFormat("时间:{1}    标题:[{0}]", title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                strBuild.AppendLine();
                strBuild.AppendLine(string.Format("     {0}. {1}", 0, new object().GetMethodName(2)));
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
                //var fileExtesion=".log";
                try
                {
                    var fileExtesion=System.Configuration.ConfigurationManager.AppSettings["LogExName"];
                    if(fileExtesion!=null)
                    {
                        _LogExName = fileExtesion;
                    }
                }
                catch(System.Configuration.ConfigurationErrorsException ex)
                {

                }
                string file = string.Format("{0}{1}{2}{3}", _path, System.DateTime.Now.ToString("yyyy-MM-dd"),selfDefineName, _LogExName);
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
    }
}
