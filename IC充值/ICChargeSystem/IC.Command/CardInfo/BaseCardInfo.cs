using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IC.Command
{
    public class BaseCardInfo
    {
        /// <summary>
        /// 内码数值
        /// </summary>
        public int InnerNO
        {
            get;
            set;
        }
        /// <summary>
        /// 内码16进制字符串
        /// </summary>
        public string InnerNOHexString
        {
            get;
            set;
        }
        /// <summary>
        /// 区号(4字节)
        /// </summary>
        public int AreaCode
        {
            get;
            set;
        }
        /// <summary>
        /// 区号16进制
        /// </summary>
        public string AreaCodeHexString
        {
            get;
            set;
        }
        /// <summary>
        /// 卡号
        /// </summary>
        public int CardCode
        {
            get;
            set;
        }
        /// <summary>
        /// 卡号16进制字符串
        /// </summary>
        public string CardCodeHexString
        {
            get;
            set;
        }
    }
}
