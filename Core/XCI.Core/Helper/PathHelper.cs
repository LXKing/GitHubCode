using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Windows.Forms;

namespace XCI.Helper
{
    /// <summary>
    /// ·���Ĳ�����
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// תΪ���·��
        /// </summary>
        /// <param name="url">����·�� ���� "~/Scripts/Javascript/UI.js"</param>
        public static string GetRelativeSiteUrl(string url)
        {
            return GetRelativeSiteUrl(HttpContext.Current.Request.ApplicationPath, url);
        }


        /// <summary>
        /// תΪ���·��
        /// </summary>
        /// <param name="applicationPath">����Ӧ�ó����·��</param>
        /// <param name="url">����·��</param>
        internal static string GetRelativeSiteUrl(string applicationPath, string url)
        {
            // Get proper application path ending with "/"
            if (!applicationPath.EndsWith("/"))
            {
                applicationPath += "/";
            }

            // Remove the "~/" from the url since we are using application path.
            if (!string.IsNullOrEmpty(url) && url.StartsWith("~/"))
            {
                url = url.Substring(2, url.Length - 2);
            }
            return applicationPath + url;
        }


        /// <summary>
        /// ��ȡ��·�� ���� http:/https://www.baidu.com:3402
        /// </summary>
        public static string GetSiteRoot()
        {
            string protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (protocol == null || protocol == "0")
                protocol = "http://";
            else
                protocol = "https://";

            string port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (port == null || port == "80" || port == "443")
                port = "";
            else
                port = ":" + port;

            string siteRoot = protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + HttpContext.Current.Request.ApplicationPath;
            return siteRoot;
        }


        /// <summary>
        /// ��ȡ��ǰҳ������
        /// </summary>
        /// <param name="rawUrl">Url</param>
        /// <param name="includeExtension">�Ƿ������չ��</param>
        public static string GetRequestedFileName(string rawUrl, bool includeExtension)
        {
            string file = rawUrl.Substring(rawUrl.LastIndexOf("/") + 1);

            if (includeExtension)
                return file;

            return file.Substring(0, file.IndexOf("."));
        }


        #region URL Base64 ����

        /// <summary>
        /// Url Base64����
        /// </summary>
        /// <param name="sourthUrl">Url</param>
        public static string Base64Encrypt(string sourthUrl)
        {
            if (sourthUrl == null) throw new ArgumentNullException("sourthUrl");
            string eurl = HttpUtility.UrlEncode(sourthUrl);
            if (eurl != null) eurl = Convert.ToBase64String(Encoding.UTF8.GetBytes(eurl));
            return eurl;
        }


        /// <summary>
        /// Url Base64����
        /// </summary>
        /// <param name="bStr">Base64����</param>
        /// <returns></returns>
        public static string Base64Decrypt(string bStr)
        {
            if (!IsBase64(bStr))
            {
                return bStr;
            }
            byte[] buffer = Convert.FromBase64String(bStr);
            string sourthUrl = Encoding.UTF8.GetString(buffer);
            sourthUrl = HttpUtility.UrlDecode(sourthUrl);
            return sourthUrl;
        }


        /// <summary>
        /// �Ƿ���Base64�ַ���
        /// </summary>
        /// <param name="eStr">�ַ���</param>
        public static bool IsBase64(string eStr)
        {
            if ((eStr.Length % 4) != 0)
            {
                return false;
            }
            if (!Regex.IsMatch(eStr, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }

        #endregion


        /// <summary>
        /// ���URL����
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        public static string AddParam(string url, string paramName, string paramValue)
        {
            Uri uri = new Uri(url);
            string connFix = "?";
            string eval = HttpContext.Current.Server.UrlEncode(paramValue);
            if (!string.IsNullOrEmpty(uri.Query))
            {
                connFix = "&";
            }
            return String.Concat(url, connFix, paramName, "=", eval);
        }


        /// <summary>
        /// ����URL����
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        public static string UpdateParam(string url, string paramName, string paramValue)
        {
            string keyWord = paramName + "=";
            int index = url.IndexOf(keyWord) + keyWord.Length;
            int index1 = url.IndexOf("&", index);
            if (index1 == -1)
            {
                url = url.Remove(index, url.Length - index);
                url = string.Concat(url, paramValue);
                return url;
            }
            url = url.Remove(index, index1 - index);
            url = url.Insert(index, paramValue);
            return url;
        }


        /// <summary>
        /// ����URL��������
        /// </summary>
        /// <param name="fromUrl">Դ��ַ</param>
        /// <param name="domain">��</param>
        /// <param name="subDomain">����</param>
        public static void GetDomain(string fromUrl, out string domain, out string subDomain)
        {
            domain = "";
            subDomain = "";
            try
            {

                UriBuilder builder = new UriBuilder(fromUrl);
                fromUrl = builder.ToString();

                Uri u = new Uri(fromUrl);

                if (u.IsWellFormedOriginalString())
                {
                    if (u.IsFile)
                    {
                        subDomain = domain = "�ͻ��˱����ļ�·��";
                    }
                    else
                    {
                        string authority = u.Authority;
                        string[] ss = u.Authority.Split('.');
                        if (ss.Length == 2)
                        {
                            authority = "www." + authority;
                        }
                        int index = authority.IndexOf('.', 0);
                        domain = authority.Substring(index + 1, authority.Length - index - 1).Replace("comhttp", "com");
                        subDomain = authority.Replace("comhttp", "com");
                        if (ss.Length < 2)
                        {
                            domain = "����·��";
                            subDomain = "����·��";
                        }
                    }
                }
                else
                {
                    if (u.IsFile)
                    {
                        subDomain = domain = "�ͻ��˱����ļ�·��";
                    }
                    else
                    {
                        subDomain = domain = "����·��";
                    }
                }
            }
            catch
            {
                subDomain = domain = "����·��";
            }
        }


        /// <summary>
        /// ���� url �ַ����еĲ�����Ϣ
        /// </summary>
        /// <param name="url">����� URL</param>
        /// <param name="baseUrl">��� URL �Ļ�������</param>
        /// <param name="nvc">���������õ��� (������,����ֵ) �ļ���</param>
        public static void ParseUrl(string url, out string baseUrl, out NameValueCollection nvc)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            nvc = new NameValueCollection();
            baseUrl = "";

            if (url == "")
                return;

            int questionMarkIndex = url.IndexOf('?');

            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return;
            }
            baseUrl = url.Substring(0, questionMarkIndex);
            if (questionMarkIndex == url.Length - 1)
                return;
            string ps = url.Substring(questionMarkIndex + 1);

            // ��ʼ����������    
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);

            foreach (Match m in mc)
            {
                nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }
        }


        /// <summary>
        /// ��ȡ��Ŀ¼
        /// </summary>
        public static string GetCurrentDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// ��ȡ��ǰ�����ļ�����Ŀ¼
        /// </summary>
        public static string GetScriptPath()
        {
            string paths = HttpContext.Current.Request.ServerVariables["PATH_TRANSLATED"];
            return paths.Remove(paths.LastIndexOf("\\"));
        }



        /// <summary>
        /// ��ȡ��·�������·��
        /// </summary>
        /// <param name="request">���ݵ�ǰҳ����������(Request)</param>
        /// <param name="path">�ļ�������·��(~/upload/xx.xx)</param>
        /// <returns>�ļ������·��(../../xx.xx)</returns>
        public static string GetXiangDuPath(HttpRequest request, string path)
        {
            string res = string.Empty;
            {
                string s = request.Url.AbsolutePath;
                while (s.IndexOf("/") != -1)
                {
                    s = s.Substring(s.IndexOf("/") + 1);
                    res += "../";
                }
                res += path.Replace("~/", "");
            }
            return res;
        }



        /// <summary>
        /// ������һ��ҳ��ĵ�ַ
        /// </summary>
        /// <returns>��һ��ҳ��ĵ�ַ</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;
            try
            {
                if (HttpContext.Current.Request.UrlReferrer != null)
                    retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        /// <summary>
        /// ��·��ǰ������WinForm����·��
        /// </summary>
        /// <param name="path">���·��</param>
        public static string AddStartupPath(string path)
        {
            //string basePath = string.Empty;

            //if (System.Environment.CurrentDirectory == AppDomain.CurrentDomain.BaseDirectory)//WindowsӦ�ó��������
            //{
            //    basePath = AppDomain.CurrentDomain.BaseDirectory;
            //}
            //else
            //{
            //    basePath = AppDomain.CurrentDomain.BaseDirectory + "Bin\\";
            //}
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

        /// <summary>
        /// ����һ����ʱ�ļ�
        /// </summary>
        /// <returns>������ʱ�ļ�·��</returns>
        public static string CreateTempFile()
        {
            string filePath = Path.Combine(Application.StartupPath, "Temp",StringHelper.GetGuidString()+".temp");
            FileHelper.CreateDirectoryByPath(filePath);
            return filePath;
        }

        /// <summary>
        /// ����һ����ʱĿ¼
        /// </summary>
        /// <returns>������ʱĿ¼·��</returns>
        public static string CreateTempFolder()
        {
            string folderPath = Path.Combine(Application.StartupPath, "Temp", StringHelper.GetGuidString());
            FileHelper.CreateDirectory(folderPath);
            return folderPath;
        }
    }
}
