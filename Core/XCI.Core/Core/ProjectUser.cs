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
    /// ��Ŀ�û�����
    /// </summary>
    public static class ProjectUser
    {
        /// <summary>
        /// Ĭ���û�ID
        /// </summary>
        public static int DefaultUserID { get; set; }

        /// <summary>
        /// �û�ID
        /// </summary>
        public static int UserID { get; set; }

        /// <summary>
        /// �����û�ID
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
        /// �û�����
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// ��½����
        /// </summary>
        public static string LoginName { get; set; }

        /// <summary>
        /// �Ƿ��ǹ���Ա
        /// </summary>
        public static bool IsAdmin { get; set; }

        /// <summary>
        /// ��½ʱ��
        /// </summary>
        public static DateTime LoginTime { get; set; }

        /// <summary>
        /// ��½IP
        /// </summary>
        public static string LoginIP { get; set; }

        public static int AgentSaleAddress { get; set; }

        /// <summary>
        /// ������ʵ��
        /// </summary>
        public static object UserEntity { get; set; }

        /// <summary>
        /// ��ɫ�б�
        /// </summary>
        public static IList RoleList { get; set; }

        /// <summary>
        /// ģ���б�
        /// </summary>
        public static IList ModuleList { get; set; }

        /// <summary>
        /// �����б�
        /// </summary>
        public static IList DepartmentList { get; set; }

        /// <summary>
        /// ģ���ֵ�
        /// </summary>
        public static Dictionary<string, object> ModuleDic { get; set; }
    }
}