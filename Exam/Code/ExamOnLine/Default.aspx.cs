using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Ext.Net.Utilities;
using MDL;
using BLL.Default;
using COMMON;
using System.Text;

namespace ExamOnLine
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitSystemInfo();
            InitUserInfo();
            InitPermission();
        }
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }
        public override void OnLoadCompleted(EventArgs e)
        {
            base.OnLoadCompleted(e);
        }

        /// <summary>
        /// 初始化系统信息
        /// </summary>
        private void InitSystemInfo()
        {
            lblCurrentDate.Html = string.Format(AppConst.Default_LoginTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            tabHome.Loader.Url = Application[AppConst.HomeUrl].ToString();
            tabHome.StyleSpec = "";
        }

        /// <summary>
        /// 初始化用户信息
        /// </summary>
        private void InitUserInfo()
        {
            if (base.LOGIN_USER == null)
                return;
            var data = new BLL_InitOperation().GetUserInforByID(Request.Cookies[AppConst.Cookie_LoginKey][AppConst.Session_LoginID].ToString());
            imgAvatar.ImageUrl = data.SERVER_FULL_PATH;
            lblUserName.Html = string.Format(AppConst.Default_UserNameDisplay, data.USER_NAME == null || data.USER_NAME == "" ? data.LOGIN_NAME : data.USER_NAME);
            lblUserCount.Text = "在线人数: " + (Application[AppConst.Application_LoginUserDic] as Dictionary<Guid, LoginUser>).Count.ToString() + "人";
            lblWelcome.Text = string.Format(AppConst.Default_WellComeLanguage, data.USER_NAME == null || data.USER_NAME == "" ? data.LOGIN_NAME : data.USER_NAME);
            HttpCookie cookieTheme = base.CreateCookie(AppConst.Cookie_Theme, base.LOGIN_USER.Theme.ToString());
            if (this.Request.Cookies.AllKeys.Contains(AppConst.Cookie_Theme))
            {
                Response.SetCookie(cookieTheme);
            }
            else
            {
                Response.AppendCookie(cookieTheme);
            }
        }

        /// <summary>
        /// 初始化权限
        /// </summary>
        public override void InitPermission()
        {
            var cookiesList=Request.Cookies[AppConst.Cookie_LoginKey];
            List<T_OPERATION> data = new BLL_InitOperation().GetOperationByRoleID(cookiesList[AppConst.Session_RoleID].ToString());
            var rootData = data.Where(a => a.PARENT_OP_ID.IsNull()).OrderBy(b=>b.SEQUENCE).ToList();
            rootData.ForEach(a =>
            {
                MenuPanel panelTmp = new MenuPanel();
                panelTmp.Title = a.OP_NAME;
                panelTmp.IconPath = a.ICON_PATH;
                AppendMenuItem(panelTmp, a, data);
                pnlMenu.Add(panelTmp);
            });
            
        }

        /// <summary>
        /// 根据权限构造菜单
        /// </summary>
        public void AppendMenuItem(MenuPanel menuPanel,T_OPERATION data, List<T_OPERATION> allData)
        {
            var childrenData = allData.Where(a => a.PARENT_OP_ID == data.ID).OrderBy(b => b.SEQUENCE).ToList();
            childrenData.ForEach(a => {
                #region 内部循环
                Ext.Net.MenuItem item = new Ext.Net.MenuItem();
                item.Text = a.OP_NAME;
                item.ID = a.ID.ToString();
                item.TagString = a.APPLICATION_FILE;

                string text = string.Empty;
                if (Application[AppConst.Domain].ToString().Contains("/"))//if (base.Request.ApplicationPath.Equals("/"))
                {
                    text = Application[AppConst.Domain].ToString() + item.TagString;
                }
                else
                {
                    text = Application[AppConst.Domain].ToString() + "/" + item.TagString;
                }
                item.Listeners.Click.Handler = string.Concat(new string[]
                {
                    "addTab(#{ExampleTabs}, '",
                    a.OP_NAME,
                    "','",
                    text,
                    "', this);"
                });
                base.WriteLog(item.Text, new List<string>() { item.Listeners.Click.Handler });
                menuPanel.Menu.Items.Add(item); 
                #endregion
            });
        }

        /// <summary>
        /// 刷新在线人数
        /// </summary>
        protected void RefreshUserCount_Click(object sender, DirectEventArgs e)
        {
            lblUserCount.Text = "在线人数: " + (Application[AppConst.Application_LoginUserDic] as Dictionary<Guid, LoginUser>).Count.ToString() + "人";
        }

        /// <summary>
        /// 注销退出
        /// </summary>
        protected void img_Logout_Click(object sender,DirectEventArgs e)
        {
            if (!Session[AppConst.Session_LoginUser].IsNull())
            {
                var user = Session[AppConst.Session_LoginUser] as LoginUser;
                RemoveLoginUser(user.ID);
                Session.Abandon();
            }
            Response.Redirect(AppConst.LoginPageUrl);
        }

        protected void img_Skin_Click(object sender,DirectEventArgs e)
        {
            themeWindow.Show();
        }

        protected void btnSaveTheme_Click(object sender, DirectEventArgs e)
        {
            if (cmbTheme.Value.NotNull() && cmbTheme.Value.ToString().IsNotEmpty())
            {
                var theme = cmbTheme.Value.ToString().ToEnumType<Ext.Net.Theme>();
                X.ResourceManager.Theme = theme ;
                Session["Ext.Net.Theme"] = theme;
                base.LOGIN_USER.Theme = theme;
                HttpCookie cookieTheme = base.CreateCookie(AppConst.Cookie_Theme, theme.ToString());
                if(this.Request.Cookies.AllKeys.Contains(AppConst.Cookie_Theme))
                {
                    Response.SetCookie(cookieTheme);
                }
                else
                {
                    Response.AppendCookie(cookieTheme);
                }
            }
        }
    }
}