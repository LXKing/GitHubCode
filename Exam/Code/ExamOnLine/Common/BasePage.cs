using COMMON.Logs;
using Ext.Extension.Message;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ExamOnLine
{
    public abstract class BasePage:System.Web.UI.Page
    {
        protected LoginUser LOGIN_USER;
        protected ResourceManager resourceManager;
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            var result = JudgeUserLoginState();
            switch (result)
            {
                case LoginState.Login:
                    break;
                case LoginState.NotLogin:
                    PageRedirect();
                    break;
                case LoginState.Off:
                    MessageBoxExt.ShowError("您已经被管理员强制下线或者已经在别的客户端登录，有问题请咨询管理人员!", "this.document.body.innerHTML='';");
                    break;
            }
        }
        /// <summary>
        /// 验证是否已经登录
        /// </summary>
        /// <returns></returns>
        protected LoginState JudgeUserLoginState()
        {
            LoginUser userInfo;
            if (!Session[AppConst.Session_LoginUser].IsNull())
            {
                userInfo = Session[AppConst.Session_LoginUser] as LoginUser;
            }
            else
            {
                return LoginState.NotLogin;
            }
            
            if (userInfo.ID.IsNull() || userInfo.LOGIN_NAME.IsNull())
            {
                return LoginState.NotLogin;
            }
            else
            {
                var applicationCurrentLoginUser = ((Dictionary<Guid, LoginUser>)Application[AppConst.Application_LoginUserDic]).Where(x => x.Key == userInfo.ID).FirstOrDefault().Value;
                if (applicationCurrentLoginUser == null)
                    return LoginState.NotLogin;
                if (applicationCurrentLoginUser.SESSION_ID != userInfo.SESSION_ID)
                    return LoginState.Off;

                if (Request.Cookies[AppConst.Cookie_LoginKey] != null)
                {
                    var cookies = Request.Cookies[AppConst.Cookie_LoginKey].Values;
                    if (cookies[AppConst.Session_LoginName] != userInfo.LOGIN_NAME.ToString() || cookies[AppConst.Session_LoginID] != userInfo.ID.ToString())
                    {
                        return LoginState.NotLogin;
                    }
                    else
                    {
                        #region 判断Application中是否包含用户信息
                        Application.Lock();
                        var dicUser = (Dictionary<Guid, LoginUser>)Application[AppConst.Application_LoginUserDic];
                        if (!dicUser.Keys.Contains(userInfo.ID))
                        {
                            Application.UnLock();
                            return LoginState.Off;
                        }
                        Application.UnLock();
                        #endregion

                        AddLoginUser(userInfo);
                        this.LOGIN_USER = Session[AppConst.Session_LoginUser] as LoginUser;
                        return LoginState.Login;
                    }
                }
                else
                {
                    return LoginState.NotLogin;
                }
            }
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(X.IsAjaxRequest)
            {

            }
            else
            {
                SettingResourceManager(this);
                WriteLog(this.Request.Url.ToString(),new List<string>(){});
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentControl"></param>
        private void SettingResourceManager(Control parentControl)
        {
            foreach (var cl in parentControl.Controls)
            {
                var clTmp=(Control)cl;
                InitResourceManagerLocal(clTmp);
            }
        }
        /// <summary>
        /// 设置ResourceManager的Local属性
        /// </summary>
        /// <param name="control"></param>
        private void InitResourceManagerLocal(Control control)
        {
            if (control is ResourceManager)
            {
                ((ResourceManager)control).Locale = AppConst.ResourceManager_Local;
                if(LOGIN_USER!=null)
                {
                    ((ResourceManager)control).Theme = LOGIN_USER.Theme;
                    Session["Ext.Net.Theme"] = LOGIN_USER.Theme;
                }
            }
            else
            {
                if (control.HasControls())
                {
                    SettingResourceManager(control);
                }
            }
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            OnLoadCompleted(e);
        }
        /// <summary>
        /// 权限初始化(抽象方法)
        /// </summary>
        public abstract void InitPermission();
        /// <summary>
        /// 页面加载完成时(虚方法,可重写)
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnLoadCompleted(EventArgs e)
        {
            
        }
        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="userInfo"></param>
        protected void AddLoginUser(LoginUser userInfo)
        {
            Application.Lock();      //临界变量,使用加锁功能,其他用户不能访问。
            var dicUser = (Dictionary<Guid, LoginUser>)Application[AppConst.Application_LoginUserDic];
            Application[AppConst.Application_LoginUserDic] = dicUser.AddOrReplace(userInfo.ID, userInfo);//Int32.Parse(Application[AppConst.Application_LoginUserDic].ToString()) + 1;
            Application.UnLock();       //临界变量被解锁。
        }
        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="userInfo"></param>
        protected bool RemoveLoginUser(Guid userID)
        {
            bool success = false;
            Application.Lock();      //临界变量,使用加锁功能,其他用户不能访问。
            var dicUser = (Dictionary<Guid, LoginUser>)Application[AppConst.Application_LoginUserDic];
            success = dicUser.Remove(userID);//Int32.Parse(Application[AppConst.Application_LoginUserDic].ToString()) + 1;
            Application.UnLock();       //临界变量被解锁。
            return success;
        }
        /// <summary>
        /// 页面跳转
        /// </summary>
        protected virtual void PageRedirect()
        {
            Response.Redirect(AppConst.LoginPageUrl);
        }
        /// <summary>
        /// 向客户端输出js
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        /// <param name="jsContent"></param>
        protected void ResponseWriteJs(bool success,string jsContent)
        {
            var js = string.Format(ResourceManager.SimpleScriptBlockTemplate, jsContent).Replace(@"\n","").Trim();
            var resp = HttpContext.Current.Response;
            resp.Clear();
            resp.Write(js);
            resp.End();
        }

        /// <summary>
        /// 写入异常(带页面名Title的)
        /// </summary>
        /// <param name="title"></param>
        /// <param name="exception"></param>
        protected void WriteException(string title,Exception exception)
        {
            var newTitle = string.Format("[页面:{0}]", this.Title) + title;
            Log.WriteException(newTitle,exception);
        }
        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msgList"></param>
        protected void WriteLog(string title, IEnumerable<string> msgList)
        {
            var newTitle = string.Format("[页面:{0}]", this.Title) + title;
            Log.WriteLog(newTitle, msgList);
        }

        #region Cookie方法
        /// <summary>
        /// 创建cookie集合
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cookieDic"></param>
        /// <param name="minuts"></param>
        /// <returns></returns>
        protected HttpCookie CreateCookie(string name,Dictionary<string,string> cookieDic,int? minuts=null)
        {
            HttpCookie cookie = new HttpCookie(name);//初使化并设置Cookie的名称
            DateTime dt=DateTime.Now;
            
            cookieDic.ToList().ForEach(x=>{
                cookie.Values.Add(x.Key,x.Value);
            });
            if(minuts.NotNull())
            {
                TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(minuts),0,0);
                cookie.Expires = dt.Add(ts);
            } 
            
            return cookie;
        }
        /// <summary>
        /// 创建单值cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cookieValue"></param>
        /// <param name="minuts"></param>
        /// <returns></returns>
         protected HttpCookie CreateCookie(string name,string cookieValue,int? minuts=null)
        {
            HttpCookie cookie = new HttpCookie(name);//初使化并设置Cookie的名称
            cookie.Value = cookieValue;
            if (minuts.NotNull())
            {
                DateTime dt = DateTime.Now;
                TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(minuts), 0, 0);
                cookie.Expires = dt.Add(ts);
            } 
            return cookie;
        }
        #endregion
    }
    /// <summary>
    /// 登陆状态
    /// </summary>
    public enum LoginState
    {
        /// <summary>
        /// 已登录
        /// </summary>
        Login,
        /// <summary>
        /// 掉线
        /// </summary>
        Off,
        /// <summary>
        /// 未登录
        /// </summary>
        NotLogin
    }
}