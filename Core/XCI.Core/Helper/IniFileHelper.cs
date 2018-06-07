using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// ini文件读写操作帮助类
    /// </summary>
    public static class IniFileHelper
    {

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        //[DllImport("kernel32")]
        //private static extern int GetPrivateProfileString(string section, string dataSourceName, string defVal, Byte[] retVal, int size, string filePath);

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">文件路径</param>
        private static bool CreateIniFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="path">INI文件路径</param>
        /// <param name="section">段落</param>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        public static void WriteValue(string path, string section, string key, string value)
        {
            if (path == null) throw new ArgumentNullException("path");
            CreateIniFile(path);
            WritePrivateProfileString(section, key, value, path);
        }


        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="path">INI文件路径</param>
        /// <param name="section">段落</param>
        /// <param name="key">关键字</param>
        /// <returns>值</returns>
        public static string ReadValue(string path, string section, string key)
        {
            if (path == null) throw new ArgumentNullException("path");
            CreateIniFile(path);
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString();
        }


        /// <summary>
        /// 删除INI文件下指定段落下的所有键
        /// </summary>
        /// <param name="path">INI文件路径</param>
        /// <param name="section">段落</param>
        public static void ClearSection(string path, string section)
        {
            if (path == null) throw new ArgumentNullException("path");
            WriteValue(path, section, null, null);
        }
    }


}
