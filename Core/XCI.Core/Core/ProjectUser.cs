using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCI.Component;
using XCI.Extension;

namespace XCI.Core
{
    /// <summary>
    /// 项目用户对象
    /// </summary>
    public static class ProjectUser
    {
        /// <summary>
        /// 默认用户ID
        /// </summary>
        public static int DefaultUserID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public static int UserID { get; set; }

        /// <summary>
        /// 配置用户ID
        /// </summary>
        public static int ConfigUserID
        {
            get
            {
                bool isEnableUserConfig = ParamFactory.Current.GetOrAdd("IsEnableUserConfig", "True").ToBool();
                if (isEnableUserConfig)
                {
                    return UserID;
                }
                return DefaultUserID;
            }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// 登陆工号
        /// </summary>
        public static string LoginName { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public static bool IsAdmin { get; set; }

        /// <summary>
        /// 登陆时间
        /// </summary>
        public static DateTime LoginTime { get; set; }

        /// <summary>
        /// 登陆IP
        /// </summary>
        public static string LoginIP { get; set; }

        public static int AgentSaleAddress { get; set; }

        /// <summary>
        /// 关联的实体
        /// </summary>
        public static object UserEntity { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public static IList RoleList { get; set; }

        /// <summary>
        /// 模块列表
        /// </summary>
        public static IList ModuleList { get; set; }

        /// <summary>
        /// 部门列表
        /// </summary>
        public static IList DepartmentList { get; set; }

        /// <summary>
        /// 模块字典
        /// </summary>
        public static Dictionary<string, object> ModuleDic { get; set; }
    }
}