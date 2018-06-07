using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamOnLine
{
    public class AppConst
    {
        #region 应用程序全局变量
        public const string Domain = "Domain";

        public const string HomeUrl = "HomeUrl";
        #endregion

        public const string ResourceManager_Local = "zh-CN";
        /// <summary>
        /// 统计在线用户
        /// </summary>
        public const string Application_LoginUserDic = "loginUserDic";
        //public static Dictionary<Guid, LoginUser> Application_LoginUserDic;
        
        /// <summary>
        /// 登陆页url
        /// </summary>
        public static string LoginPageUrl = System.Configuration.ConfigurationManager.AppSettings["LoginPage"];

        #region Session和Cookie名称
        public const string Session_LoginUser = "loginUser";

        public const string Session_LoginID = "ID";
        public const string Session_UserName = "USER_NAME";
        public const string Session_LoginName = "LOGIN_NAME";
        public const string Session_RoleID = "ROLE_ID";
        public const string Session_Degree_ID = "DEGREE_ID";
        public const string Session_Position_ID = "POSITION_ID";
        public const string Session_Theme = "Theme";

        public const string Cookie_LoginKey = "loginCookie";
        public const string Cookie_IDKey = "idCookie";
        public const string Cookie_Theme = "themeCookie";
        #endregion

        public const string Default_WellComeLanguage = "{0} 您好，欢迎进入最畅销的考试系统,欢迎使用该考试系统-西安***科技有限公司";
        public const string Default_UserNameDisplay = "用  户: <span style='color:blue;'>{0}</span>";
        public const string Default_LoginTime = "系统登录时间：<span style='color:blue;'>{0}</span>";

        #region 查看、增加、更新的类型定义
        public const string ManagementOperateType = "optype";
        public const string ManagementOperateTypeView = "v";
        public const string ManagementOperateTypeAdd = "a";
        public const string ManagementOperateTypeUpdate = "u";
        #endregion
    }
}