using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamOnLine
{
    public class LoginUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public System.Guid ID { get; set; }
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string LOGIN_NAME { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string USER_NAME { get; set; }
        /// <summary>
        /// 用岗位ID
        /// </summary>
        public Nullable<System.Guid> POSITION_ID { get; set; }
        /// <summary>
        /// 用户角色ID
        /// </summary>
        public Nullable<System.Guid> ROLE_ID { get; set; }
        /// <summary>
        /// 用户学历ID
        /// </summary>
        public Nullable<System.Guid> DEGREE_ID { get; set; }
        /// <summary>
        /// 会话ID
        /// </summary>
        public string SESSION_ID
        {
            get;
            set;
        }
        /// <summary>
        /// 主题
        /// </summary>
        public Theme Theme
        {
            get;
            set;
        }
        //public string USER_PWD { get; set; }
        //public string IDENTITY_CARD_CODE { get; set; }
        //public string USER_TEL { get; set; }
        //public string USER_MOBILE { get; set; }
        //public string USER_CONTACT { get; set; }
        //public string ADDRESS { get; set; }
        //public Nullable<System.Guid> USER_PHOTO { get; set; }
        //public string POSTCODE { get; set; }
        //public Nullable<System.Guid> CREATE_USER_ID { get; set; }
        //public Nullable<System.DateTime> CREATE_DATE { get; set; }
        //public string POSITION_NAME { get; set; }
        //public string DEGREE_NAME { get; set; }
        //public string ROLE_NAME { get; set; }
        //public string DEPARTMENT_NAME { get; set; }
        //public string SERVER_FULL_PATH { get; set; }
    }
}