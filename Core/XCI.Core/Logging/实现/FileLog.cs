using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace XCI.Component
{
    /// <summary>
    /// 日志记录到文件
    /// 用户可以自己制定日志文件的路径 
    /// 默认文件路径为->应用程序目录\Log\yyyy-MM-dd(当日).log
    /// </summary>
    [XCIComponent(
        "文件日志",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "2.1.5243.0",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "记录日志到文件中 如果没有指定 LogPath(文件路径) 则按天建文件存储到应用程序" +
        "Log目录 用户也可以自己指定 LogPath(文件路径) 则存储到用户指定的文件中",
        "XCI.Logging.LoggingLogo.png")]
    public class FileLog : LogBase, ILog
    {
        /// <summary>
        /// 写文件锁对象
        /// </summary>
        private static readonly object writeFileObj = new object();
        private string _logPath;

        /// <summary>
        /// 日志文件路径
        /// </summary>
        public string LogPath
        {
            get
            {
                if (string.IsNullOrEmpty(_logPath))
                {
                    _logPath = GetDefaultPath();
                }
                return _logPath;
            }
        }

        /// <summary>
        /// 获取默认文件路径
        /// </summary>
        /// <returns>返回文件完整路径</returns>
        private string GetDefaultPath()
        {
            string path = string.Format("{0}\\Log\\{1}.log"
                        , AppDomain.CurrentDomain.SetupInformation.ApplicationBase
                        , DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(path))
            {
                string dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            return path;
        }

        /// <summary>
        /// 日志写入文件
        /// </summary>
        /// <param name="logEntity">日志实体</param>
        private void WriteFile(LogEntity logEntity)
        {
            string path = LogPath;
            StreamWriter streamWriter = !File.Exists(path)
                                            ? File.CreateText(path)
                                            : File.AppendText(path);
            using (streamWriter)
            {
                string outMessage = logEntity.Message;
                if (base.Formatter != null)
                {
                    outMessage = base.Formatter.Format(logEntity);
                }
                streamWriter.WriteLine(outMessage);
            }
        }
        
        /// <summary>
        /// 写入日志 具体实现
        /// </summary>
        /// <param name="logEntity">日志实体</param>
        public override void LogCore(LogEntity logEntity)
        {
            lock (writeFileObj)
            {
                WriteFile(logEntity);
            }
        }

    }
}