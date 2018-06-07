using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using ExamOnLine;
//using COMMOM;
namespace ExamOnLine
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            COMMON.Logs.Log.WriteLog("网站启动", new List<string> { "Application_Start" });

            Application.Lock();      //临界变量,使用加锁功能,其他用户不能访问。
            Application[AppConst.Application_LoginUserDic] = new Dictionary<Guid,LoginUser>();
            Application.UnLock();     //临界变量被解锁。

            Application[AppConst.Domain] = new BLL.BLL_Base().T_AppConfig.Where(x => x.AppKey == "Domain").FirstOrDefault().AppValue;
            Application[AppConst.HomeUrl] = new BLL.BLL_Base().T_AppConfig.Where(x => x.AppKey == "HomeUrl").FirstOrDefault().AppValue;
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码
            COMMON.Logs.Log.WriteLog("网站关闭", new List<string> { "Application_End" });
        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
            COMMON.Logs.Log.WriteLog("网站错误", new List<string> { "Application_Error" });
        }

        void Session_Start(object sender, EventArgs e) 
        {
            //Application.Lock();      //临界变量,使用加锁功能,其他用户不能访问。
            //Application[AppConst.Application_LoginUserDic] = ((Dictionary<Guid,LoginUser>)Application[AppConst.Application_LoginUserDic]).ad//Int32.Parse(Application[AppConst.Application_LoginUserDic].ToString()) + 1;
            //Application.UnLock();       //临界变量被解锁。
        }

        void Session_End(object sender, EventArgs e)
        {
            //Application.Lock();      //临界变量,使用加锁功能,其他用户不能访问。
            //Application["UserCount"] = Int32.Parse(Application[AppConst.Application_LoginUserDic].ToString()) - 1;
            //Application.UnLock();       //临界变量被解锁。
            if(!Session[AppConst.Session_LoginUser].IsNull())
            {
                var id = (Session[AppConst.Session_LoginUser] as LoginUser).ID;
                RemoveLoginUser(id);
            }
                
        }

        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="userID"></param>
        protected void RemoveLoginUser(Guid userID)
        {
            Application.Lock();      //临界变量,使用加锁功能,其他用户不能访问。
            var dicUser = (Dictionary<Guid, LoginUser>)Application[AppConst.Application_LoginUserDic];
            dicUser.Remove(userID);//Int32.Parse(Application[AppConst.Application_LoginUserDic].ToString()) + 1;
            Application.UnLock();       //临界变量被解锁。
        }
    }
}
