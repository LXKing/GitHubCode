using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace XCI.Helper
{
    /// <summary>
    /// ASP.NET 页面脚本管理
    /// </summary>
    public class ScriptHelper
    { 
        /// <summary>
        /// 注册js启动脚本
        /// </summary>
        /// <param name="page">页面对象</param>
        /// <param name="script">脚本,不带script标记</param>
        public static void RegisterSetupScript(Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), DateTime.Now.Ticks.ToString(), script, true);
        }


        /// <summary>
        /// 注册包含js脚本
        /// </summary>
        /// <param name="page">页面对象</param>
        /// <param name="url">脚本url(虚拟路径)</param>
        public static void RegisterIncludeScript(Page page, string url)
        {
            page.ClientScript.RegisterClientScriptInclude(DateTime.Now.Ticks.ToString(), page.ResolveClientUrl(url));
        }


        /// <summary>
        /// 注册css
        /// </summary>
        /// <param name="page">页面对象</param>
        /// <param name="url">css虚拟路径</param>
        public static void RegisterCssLink(Page page, string url)
        {
            HtmlLink link = new HtmlLink();            
            link.EnableViewState = false;
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            link.Href = url;
            page.Header.Controls.Add(link);
        }
    }
}
