using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Diagnostics;
namespace XCI.Helper
{
    /// <summary>
    /// 文件管理
    /// </summary>
    public static class FileHelper
    {
        #region 目录操作

        /// <summary>
        /// 获取一个目录下的所有目录(递归实现,不包括自身)
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <returns>返回目录下所有目录</returns>
        public static IList<string> GetAllDirectorys(string dir)
        {
            return GetAllDirectorys(null, dir);
        }


        /// <summary>
        /// 获取一个目录下的所有目录(递归实现)
        /// </summary>
        /// <param name="list">要返回的list,首次调用传递null即可</param>
        /// <param name="dir">目录路径</param>
        /// <returns>返回目录下所有目录</returns>
        private static IList<string> GetAllDirectorys(IList<string> list, string dir)
        {
            string[] directories = Directory.GetDirectories(dir);
            list = list ?? new List<string>();
            foreach (string item in directories)
            {
                list.Add(item);
                GetAllDirectorys(list, item);
            }
            return list;
        }


        /// <summary>
        /// 拷贝旧目录到新目录
        /// </summary>
        /// <param name="oldDirectoryStr">旧目录</param>
        /// <param name="newDirectoryStr">新目录</param>
        public static void CopyDirectory(string oldDirectoryStr, string newDirectoryStr)
        {
            DirectoryInfo oldDirectory = new DirectoryInfo(oldDirectoryStr);
            DirectoryInfo newDirectory = new DirectoryInfo(newDirectoryStr);
            CopyDirectory(oldDirectory, newDirectory);
        }


        /// <summary>
        /// 拷贝旧目录到新目录
        /// </summary>
        /// <param name="oldDirectory">旧目录</param>
        /// <param name="newDirectory">新目录</param>
        private static void CopyDirectory(DirectoryInfo oldDirectory, DirectoryInfo newDirectory)
        {
            if (oldDirectory == null) throw new ArgumentNullException("oldDirectory");
            if (newDirectory == null) throw new ArgumentNullException("newDirectory");
            string newDirectoryFullName = newDirectory.FullName;

            if (!Directory.Exists(newDirectoryFullName))
                Directory.CreateDirectory(newDirectoryFullName);

            FileInfo[] oldFileAry = oldDirectory.GetFiles();
            foreach (FileInfo aFile in oldFileAry)
                File.Copy(aFile.FullName, newDirectoryFullName + @"\" + aFile.Name, true);

            DirectoryInfo[] oldDirectoryAry = oldDirectory.GetDirectories();
            foreach (DirectoryInfo aOldDirectory in oldDirectoryAry)
            {
                DirectoryInfo aNewDirectory = new DirectoryInfo(string.Concat(newDirectoryFullName, "\\", aOldDirectory.Name));
                CopyDirectory(aOldDirectory, aNewDirectory);
            }
        }


        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="oldDirectoryStr">目录路径(绝对路径)</param>
        public static void DeleteDirectory(string oldDirectoryStr)
        {
            DirectoryInfo oldDirectory = new DirectoryInfo(oldDirectoryStr);
            oldDirectory.Delete(true);
        }

        /// <summary>
        /// 根据一个文件路径创建路径中的所有目录
        /// </summary>
        /// <param name="path">要创建的完整路径</param>
        /// <returns>创建成功返回true</returns>
        public static bool CreateDirectoryByPath(string path)
        {
            FileInfo file = new FileInfo(path);
            DirectoryInfo dir = file.Directory;
            if (dir != null)
            {
                string dirFullName = dir.FullName;
                if (!Directory.Exists(dirFullName))
                {
                    Directory.CreateDirectory(dirFullName);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据一个路径创建路径中的所有目录
        /// </summary>
        /// <param name="path">要创建的完整路径</param>
        /// <returns>创建成功返回true</returns>
        public static bool CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }
            return false;
        }


        #endregion


        #region 文件操作

        #region 读取文件

        /// <summary>
        /// 获取一个目录下的所有文件(递归实现)
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <returns>返回目录下所有文件列表</returns>
        public static IList<string> GetAllFiles(string dir)
        {
            return GetAllFiles(null, dir);
        }


        /// <summary>
        /// 获取一个目录下的所有文件(递归实现)
        /// </summary>
        /// <param name="list">要返回的list,首次调用传递null即可</param>
        /// <param name="dir">目录路径</param>
        /// <returns>返回目录下所有文件列表</returns>
        private static IList<string> GetAllFiles(List<string> list, string dir)
        {
            string[] fileNames = Directory.GetFiles(dir);
            string[] directories = Directory.GetDirectories(dir);
            list = list ?? new List<string>();
            list.AddRange(fileNames);
            foreach (string item in directories)
            {
                GetAllFiles(list, item);
            }
            return list;
        }


        /// <summary>
        /// 文件改名
        /// </summary>
        /// <param name="sourceFileName">原文件路径</param>
        /// <param name="newName">文件新名字(一个名字,不包括路径)</param>
        public static bool RenameFile(string sourceFileName, string newName)
        {
            string dir = new FileInfo(sourceFileName).DirectoryName;
            if (dir != null)
            {
                string newpath = Path.Combine(dir, newName);
                File.Move(sourceFileName, newpath);
                return true;
            }
            return false;
        }


        #endregion

        #region 删除文件

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>成功返回true</returns>
        public static bool DeleteFile(string path)
        {
            if (path == null) throw new ArgumentNullException("path");
            try
            {
                if (File.Exists(path))//查看此文件是否存在
                {
                    File.Delete(path);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion


        #region Web下载文件

        /// <summary>
        /// (其他类型文件)文件下载
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void DownFile(string filePath)
        {
            if (filePath == null) throw new ArgumentNullException("filePath");
            string path = HttpContext.Current.Server.MapPath(filePath);
            if (File.Exists(path))
            {
                FileStream fileStream = new FileStream(path, FileMode.Open);
                string fileName = path.Substring(path.LastIndexOf('\\') + 1);
                long fileSize = fileStream.Length;
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + HttpUtility.UrlEncode(fileName, Encoding.UTF8) + "\"");
                HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                byte[] fileBuffer = new byte[fileSize];
                fileStream.Read(fileBuffer, 0, (int)fileSize);
                fileStream.Close();
                fileStream.Dispose();
                HttpContext.Current.Response.BinaryWrite(fileBuffer);
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// (文本型)下载文件
        /// </summary>
        /// <param name="str">要下载的字符串</param>
        /// <param name="fileName">下载时显示的文件名字</param>
        public static void DownFile(string str, string fileName)
        {
            Stream fileStream = new MemoryStream(Encoding.UTF8.GetBytes(str));
            long fileSize = fileStream.Length;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + HttpUtility.UrlEncode(fileName, Encoding.UTF8) + "\"");
            HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
            byte[] fileBuffer = new byte[fileSize];
            fileStream.Read(fileBuffer, 0, (int)fileSize);
            fileStream.Close();
            fileStream.Dispose();
            HttpContext.Current.Response.BinaryWrite(fileBuffer);
            HttpContext.Current.Response.End();
        }

        #endregion


        /// <summary>
        /// 获取文件版本号
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static string GetFileVersion(string filePath)
        {
            if (!File.Exists(filePath))
                return string.Empty;

            // Get the file version for the notepad.
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filePath);
            return versionInfo.FileVersion;
        }

        /// <summary>
        /// 获取文件名称 不包含扩展名
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>返回文件名称 不包括扩展名 比如 c:\aa\cde.xml 返回cde</returns>
        public static string GetFileName(string path)
        {
            string fileName = Path.GetFileName(path);
            if (!string.IsNullOrEmpty(fileName))
            {
                return fileName.Substring(0, fileName.IndexOf('.'));
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取文件md5码
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileMD5(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
            byte[] hash = md5.ComputeHash(fs);
            string hashCode = BitConverter.ToString(hash).Replace("-", "");
            return hashCode;
        }

    }
}
