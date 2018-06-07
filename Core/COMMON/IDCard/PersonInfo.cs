using System.Drawing;


namespace IDCardRead
{
    public class PersonInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday
        {
            get;
            set;
        }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string CardNO
        {
            get;
            set;
        }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string Address
        {
            get;
            set;
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get;
            set;
        }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation
        {
            get;
            set;
        }
        /// <summary>
        /// 发证机关
        /// </summary>
        public string GrantDept
        {
            get;
            set;
        }
        /// <summary>
        /// 有效期开始日期
        /// </summary>
        public string IDCardBeginDate
        {
            get;
            set;
        }
        /// <summary>
        /// 有效期结束日期
        /// </summary>
        public string IDCardEndDate
        {
            get;
            set;
        }

        /// <summary>
        /// 照片
        /// </summary>
        public Image Photo
        {
            get;
            set;
        }
    }
}
