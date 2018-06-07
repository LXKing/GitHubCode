using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace XCI.WinUtility
{
    public class TabLoadHelper
    {
        public TabLoadHelper(XtraTabControl tabControl, Action<string> pageLoadAction)
        {
            tabControl.SelectedPageChanging += (o, e) => LoadPage(e.Page);
            this.PageLoadAction = pageLoadAction;
        }

        /// <summary>
        /// 已经加载的页面
        /// </summary>
        private readonly IList<string> LoadPages = new List<string>();
        private Action<string> PageLoadAction { get; set; }

        /// <summary>
        /// 加载Tab页
        /// </summary>
        /// <param name="page">Tab页名称</param>
        public void LoadPage(Control page)
        {
            var pageName = page.Name;
            var has = LoadPages.Any(p => p.Equals(pageName));
            if (!has)
            {
                if (PageLoadAction != null)
                {
                    PageLoadAction(pageName);
                }
                LoadPages.Add(pageName);
            }
        }
    }
}