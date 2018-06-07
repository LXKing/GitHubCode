using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Routing;
using System.Web.SessionState;

using BLL;
using MDL;
using Ext.Net;
namespace ExamOnLine.Ashx
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : BaseFuncHandler, IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var response = new ResultInfo<object>();
            try
            {
                
                base.context = context;
                var name = base.GetParameter("name");
                var pwd = base.GetParameter("pwd");

                var result = new BLL.Login.BLL_Login().ValidateUser(name, pwd);
                response.Success = result.Success;
                response.Message = result.Message;

                if (result.Success)
                {
                    HttpApplicationState applicationState = HttpContext.Current.Application;

                    //HttpApplicationState applicationState = context.Application;

                    //applicationState["usrname"] = "ddddd";
                    var dicUser = (Dictionary<Guid, LoginUser>)applicationState[AppConst.Application_LoginUserDic];
                    var count = dicUser.Where(x => x.Key == result.Data.ID && x.Value.LOGIN_NAME == result.Data.LOGIN_NAME).Count();

                    #region 初始化
                    var user = result.Data;
                    var userInfo = new LoginUser()
                    {
                        ID = user.ID,
                        LOGIN_NAME = user.LOGIN_NAME,
                        ROLE_ID = user.ROLE_ID,
                        DEGREE_ID = user.DEGREE_ID,
                        POSITION_ID = user.POSITION_ID,
                        USER_NAME = user.USER_NAME,
                        SESSION_ID = HttpContext.Current.Session.SessionID,
                        Theme = Theme.Neptune
                    };
                    var cookieTheme = context.Request.Cookies[AppConst.Cookie_Theme];
                    if (cookieTheme.NotNull() && cookieTheme.Value.NotNullAndEpmty())
                    {
                        userInfo.Theme = cookieTheme.Value.ToEnumType<Theme>();
                    }
                    response.Data = userInfo;
                    HttpContext.Current.Session[AppConst.Session_LoginUser] = userInfo;

                    HttpApplicationState application = context.Application;
                    AddLoginUser(userInfo, application);

                    //HttpContext.Current.Session[AppConst.Session_LoginName] = userInfo.LOGIN_NAME;
                    //HttpContext.Current.Session[AppConst.Session_LoginID] = userInfo.ID;
                    //HttpContext.Current.Session[AppConst.Session_RoleID] = userInfo.ROLE_ID;

                    #region ID cookie
                    var cookieID = userInfo.ID.ToString();
                    HttpCookie cookieUserID = base.CreateCookie(AppConst.Cookie_IDKey, cookieID);
                    #endregion

                    #region LoginUser cookie
                    var dicCookie = new Dictionary<string, string>();
                    dicCookie.Add(AppConst.Session_LoginID, userInfo.ID.ToString());
                    dicCookie.Add(AppConst.Session_RoleID, userInfo.ROLE_ID.ToString());
                    dicCookie.Add(AppConst.Session_Position_ID, user.POSITION_ID.ToString());
                    dicCookie.Add(AppConst.Session_Degree_ID, user.DEGREE_ID.ToString());
                    dicCookie.Add(AppConst.Session_LoginName, userInfo.LOGIN_NAME);
                    dicCookie.Add(AppConst.Session_UserName, HttpUtility.UrlEncodeUnicode(user.USER_NAME));
                    HttpCookie loginCookie = base.CreateCookie(AppConst.Cookie_LoginKey, dicCookie);
                    #endregion

                    context.Response.AppendCookie(loginCookie);
                    context.Response.AppendCookie(cookieUserID); 
                    #endregion
                    //if(count>0)
                    //{
                    //    throw new Exception("对不起，该用户已经登录，请先下线后再重新登录!");
                    //}
                    //else
                    //{

                    //}
                }
                context.Response.ContentType = "text/plain";
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            finally
            {

            }
            context.Response.Write(response.ToJsonString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected void AddLoginUser(LoginUser userInfo,HttpApplicationState application)
        {
            application.Lock();      //临界变量,使用加锁功能,其他用户不能访问。
            var dicUser = (Dictionary<Guid, LoginUser>)application[AppConst.Application_LoginUserDic];
            application[AppConst.Application_LoginUserDic] = dicUser.AddOrReplace(userInfo.ID, userInfo);//Int32.Parse(Application[AppConst.Application_LoginUserDic].ToString()) + 1;
            application.UnLock();       //临界变量被解锁。
        }
    }
}