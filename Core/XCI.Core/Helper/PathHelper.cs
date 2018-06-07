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
    /// 路径的操作类
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// 转为相对路径
        /// </summary>
        /// <param name="url">虚拟路径 例如 "~/Scripts/Javascript/UI.js"</param>
        public static string GetRelativeSiteUrl(string url)
        {
            return GetRelativeSiteUrl(HttpContext.Current.Request.ApplicationPath, url);
        }


        /// <summary>
        /// 转为相对路径
        /// </summary>
        /// <param name="applicationPath">虚拟应用程序根路径</param>
        /// <param name="url">虚拟路径</param>
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
        /// 获取根路径 例如 http:/https://www.baidu.com:3402
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
        /// 获取当前页面名称
        /// </summary>
        /// <param name="rawUrl">Url</param>
        /// <param name="includeExtension">是否包含扩展名</param>
        public static string GetRequestedFileName(string rawUrl, bool includeExtension)
        {
            string file = rawUrl.Substring(rawUrl.LastIndexOf("/") + 1);

            if (includeExtension)
                return file;

            return file.Substring(0, file.IndexOf("."));
        }


        #region URL Base64 编码

        /// <summary>
        /// Url Base64编码
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
        /// Url Base64解码
        /// </summary>
        /// <param name="bStr">Base64编码</param>
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
        /// 是否是Base64字符串
        /// </summary>
        /// <param name="eStr">字符串</param>
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
        /// 添加URL参数
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
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
        /// 更新URL参数
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
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
        /// 分析URL所属的域
        /// </summary>
        /// <param name="fromUrl">源地址</param>
        /// <param name="domain">域</param>
        /// <param name="subDomain">子域</param>
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
                        subDomain = domain = "客户端本地文件路径";
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
                            domain = "不明路径";
                            subDomain = "不明路径";
                        }
                    }
                }
                else
                {
                    if (u.IsFile)
                    {
                        subDomain = domain = "客户端本地文件路径";
                    }
                    else
                    {
                        subDomain = domain = "不明路径";
                    }
                }
            }
            catch
            {
                subDomain = domain = "不明路径";
            }
        }


        /// <summary>
        /// 分析 url 字符串中的参数信息
        /// </summary>
        /// <param name="url">输入的 URL</param>
        /// <param name="baseUrl">输出 URL 的基础部分</param>
        /// <param name="nvc">输出分析后得到的 (参数名,参数值) 的集合</param>
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

            // 开始分析参数对    
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);

            foreach (Match m in mc)
            {
                nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }
        }


        /// <summary>
        /// 获取基目录
        /// </summary>
        public static string GetCurrentDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 获取当前访问文件物理目录
        /// </summary>
        public static string GetScriptPath()
        {
            string paths = HttpContext.Current.Request.ServerVariables["PATH_TRANSLATED"];
            return paths.Remove(paths.LastIndexOf("\\"));
        }



        /// <summary>
        /// 获取此路径的相对路径
        /// </summary>
        /// <param name="request">传递当前页面的请求对象(Request)</param>
        /// <param name="path">文件的虚拟路径(~/upload/xx.xx)</param>
        /// <returns>文件的相对路径(../../xx.xx)</returns>
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
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
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
        /// 在路径前面增加WinForm启动路径
        /// </summary>
        /// <param name="path">相对路径</param>
        public static string AddStartupPath(string path)
        {
            //string basePath = string.Empty;

            //if (System.Environment.CurrentDirectory == AppDomain.CurrentDomain.BaseDirectory)//Windows应用程序则相等
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
        /// 创建一个临时文件
        /// </summary>
        /// <returns>返回临时文件路径</returns>
        public static string CreateTempFile()
        {
            string filePath = Path.Combine(Application.StartupPath, "Temp",StringHelper.GetGuidString()+".temp");
            FileHelper.CreateDirectoryByPath(filePath);
            return filePath;
        }

        /// <summary>
        /// 创建一个临时目录
        /// </summary>
        /// <returns>返回临时目录路径</returns>
        public static string CreateTempFolder()
        {
            string folderPath = Path.Combine(Application.StartupPath, "Temp", StringHelper.GetGuidString());
            FileHelper.CreateDirectory(folderPath);
            return folderPath;
        }
    }
}
