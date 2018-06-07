using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// ini�ļ���д����������
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
        /// �����ļ�
        /// </summary>
        /// <param name="path">�ļ�·��</param>
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
        /// дINI�ļ�
        /// </summary>
        /// <param name="path">INI�ļ�·��</param>
        /// <param name="section">����</param>
        /// <param name="key">�ؼ���</param>
        /// <param name="value">ֵ</param>
        public static void WriteValue(string path, string section, string key, string value)
        {
            if (path == null) throw new ArgumentNullException("path");
            CreateIniFile(path);
            WritePrivateProfileString(section, key, value, path);
        }


        /// <summary>
        /// ��ȡINI�ļ�
        /// </summary>
        /// <param name="path">INI�ļ�·��</param>
        /// <param name="section">����</param>
        /// <param name="key">�ؼ���</param>
        /// <returns>ֵ</returns>
        public static string ReadValue(string path, string section, string key)
        {
            if (path == null) throw new ArgumentNullException("path");
            CreateIniFile(path);
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString();
        }


        /// <summary>
        /// ɾ��INI�ļ���ָ�������µ����м�
        /// </summary>
        /// <param name="path">INI�ļ�·��</param>
        /// <param name="section">����</param>
        public static void ClearSection(string path, string section)
        {
            if (path == null) throw new ArgumentNullException("path");
            WriteValue(path, section, null, null);
        }
    }


}
