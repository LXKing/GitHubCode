using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace COMMON.Logs
{
    /// <summary>
    /// 日志归档
    /// </summary>
    public class LogSort
    {
        /// <summary>
        /// 新目录创建(同步)
        /// </summary>
        public event Action<string> NewDirection_Create;
        /// <summary>
        /// 归档所有日志之前
        /// </summary>
        public event Action<int> BeforeSort;
        /// <summary>
        /// 归档所有日志完成之后
        /// </summary>
        public event Action AfterSort;

        /// <summary>
        /// 归档某一个日志之前
        /// </summary>
        public event Action<int,string> BeforeSortOne_Update;
        /// <summary>
        /// 归档某一个日志完成之后
        /// </summary>
        public event Action<int, string> AfterSortOne_Update;

        /// <summary>
        /// 归档某一个日志发生异常
        /// </summary>
        public event Action<int,string,Exception> AfterSortOne_Exception;
        private string _logPath;
        public LogSort(string logPath)
        {

            if (Directory.Exists(logPath))
            {
                _logPath = logPath;
            }
            else
                throw new Exception("无效的日志文件路径!");
        }
        public void SortByOneDate(DateTime dt, string pattern = "")
        {
            try
            {
                DirectoryInfo drInfo = new DirectoryInfo(_logPath);
                var files = drInfo.GetFiles(pattern).ToList();
                var dr = _logPath +@"\" +dt.ToString("yyyy-MM-dd");
                if (!Directory.Exists(dr))
                {
                    var newDrInfo = Directory.CreateDirectory(dr);
                    if (NewDirection_Create != null)
                    {
                        NewDirection_Create.BeginInvoke(dr,null,null);
                    }
                }
                if(BeforeSort!=null)
                {
                    BeforeSort.Invoke(files.Count);
                }
                var index = 0;
                files.ForEach(x =>
                {
                    if (BeforeSortOne_Update!=null)
                    {
                        BeforeSortOne_Update.Invoke(index,x.Name);
                    }
                    try
                    {
                        File.Move(x.FullName, dr + @"\" + x.Name);
                    }
                    catch (Exception ex)
                    {
                        if(AfterSortOne_Exception!=null)
                        {
                            AfterSortOne_Exception.Invoke(index,x.Name,ex);
                        }
                    }
                    if (AfterSortOne_Update != null)
                    {
                        AfterSortOne_Update.Invoke(index, x.Name);
                    }
                    index++;
                });
                if (AfterSort != null)
                {
                    AfterSort.Invoke();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
