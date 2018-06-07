using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace System.IO
{
    public static class DirectoryInfoEx
    {
        /// <summary>
        /// 查找指定文件夹下指定后缀名的文件
        /// </summary>
        /// <param name="directory">文件夹</param>
        /// <param name="pattern">后缀名</param>
        /// <returns>文件路径</returns>
        public static List<KeyValuePair<string,string>> GetFilesEx(this DirectoryInfo directory, string pattern)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            if (directory.Exists || pattern.Trim() != string.Empty)
            {
                try
                {
                    //result.Add(new KeyValuePair<string, string>(directory.FullName, ""));
                    foreach (FileInfo info in directory.GetFiles(pattern))
                    {
                        result.Add(new KeyValuePair<string, string>(info.FullName.ToString(), info.Directory.FullName));
                        //num++;
                    }

                    foreach (DirectoryInfo info in directory.GetDirectories())
                    {
                        result.AddRange(GetFilesEx(info, pattern));
                    }
                }
                catch { 
                }
                
            }
            return result;
        }

        public static List<KeyValuePair<string, string>> GetFolders(this DirectoryInfo directory,Func<DirectoryInfo,bool> where)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            if (directory.Exists)
            {
                try
                {
                    //result.Add(new KeyValuePair<string, string>(directory.FullName, ""));
                    foreach (var info in directory.GetDirectories().Where(where).ToList())
                    {
                        result.Add(new KeyValuePair<string, string>(info.FullName.ToString(), info.Parent.FullName));
                        result.AddRange(info.GetFolders(where));
                    }
                }
                catch
                {
                }

            }
            return result;
        }
    }
}
