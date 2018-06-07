using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IC.Command
{
    public class CardEntity
    {
        /// <summary>
        /// 表类型
        /// </summary>
        public CardType Type
        {
            get;
            set;
        }
        /// <summary>
        /// 内码(0x20,4)
        /// </summary>
        public IEnumerable<byte> InnerCode
        {
            get;
            set;
        }
        /// <summary>
        /// 区号(0x24,4)
        /// </summary>
        public IEnumerable<byte> AreaCode
        {
            get;
            set;
        }
        /// <summary>
        /// 卡号(0x28,4)
        /// </summary>
        public IEnumerable<byte> CradCode
        {
            get;
            set;
        }
        /// <summary>
        /// 返回购电次数(0x2C,2)
        /// </summary>
        public IEnumerable<byte> ReturnBackChargeCount
        {
            get;
            set;
        }
        /// <summary>
        /// 写入购电次数(0x2E,2)
        /// </summary>
        public int ChargeCount
        {
            get;
            set;
        }
        /// <summary>
        /// 表参数(0x30,4)
        /// </summary>
        public IEnumerable<byte> MeterParms
        {
            get
            {
                return AreaCode;
            }
        }

    }
    public enum CardType
    {
        /// <summary>
        /// 普通用户卡
        /// </summary>
        UserCard,
        /// <summary>
        /// 单项清零卡
        /// </summary>
        SingleClearCard,
        /// <summary>
        /// 单项断电卡
        /// </summary>
        SingleBreakCard,
        /// <summary>
        /// 三项清零卡
        /// </summary>
        ThreeClearCard
    }
}
