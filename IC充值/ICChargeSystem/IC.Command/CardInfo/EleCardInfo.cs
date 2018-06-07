using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IC.Command
{
    /// <summary>
    /// 电卡信息
    /// </summary>
    public class EleCardInfo:BaseCardInfo
    {
        public EleCardInfo(IEnumerable<byte> data)
        {
            //跳过内码
            var mydata = data;
            #region 内码
            base.InnerNOHexString = mydata.Take(4).ByteToHexString(" ");
            base.InnerNO = (int)mydata.Take(4).ToList().HexToInt();
            #endregion

            #region 区号
            //跳过内码
            mydata = mydata.Skip(4);
            AreaCodeHexString = mydata.Take(4).ByteToHexString(" ");
            AreaCode = (int)mydata.Take(4).ToList().HexToInt();
            #endregion

            #region 卡号
            //跳过区号
            mydata = mydata.Skip(4);
            CardCodeHexString = mydata.Take(4).ByteToHexString(" ");
            base.CardCode = (int)mydata.Take(4).ToList().HexToInt();
            #endregion

            #region 电表中回写次数
            //跳过卡号
            mydata = mydata.Skip(4);
            ChargeCountsInMeterHexString = mydata.Take(2).ByteToHexString(" ");
            ChargeCountsInMeter = (int)mydata.Take(2).ToList().HexToInt();
            #endregion

            #region 卡中充值后未插卡的次数
            //跳过回写次数
            mydata = mydata.Skip(2);
            ChargeCountsInCard = (int)mydata.Take(2).ToList().HexToInt();
            ChargeCountsInCardHexString = mydata.Take(2).ByteToHexString(" ");
            #endregion

            #region 报警量
            //跳过卡中次数和系统参数
            mydata = mydata.Skip(6);
            ChargeEleInCard = (int)mydata.Take(4).ToList().HexToInt();
            ChargeEleInCardHexString = mydata.Take(4).ByteToHexString(" ");
            #endregion

            #region 总电量
            //跳过卡中次数和系统参数
            mydata = mydata.Skip(4);
            SumEleInMeter = (int)mydata.Take(4).ToList().HexToInt();
            SumEleInMeterHexString = mydata.Take(4).ByteToHexString(" ");
            #endregion

            #region 欠电量
            //跳过总电量
            mydata = mydata.Skip(4);
            TuitionEle = (int)mydata.Take(4).ToList().HexToInt();
            TuitionEleHexString = mydata.Take(4).ByteToHexString(" ");
            #endregion

            #region 稽查码
            //跳过总电量+限负荷数字+ 倍率+断电量+报警量
            mydata = mydata.Skip(12);
            CheckCodeValue = (int)mydata.Take(4).ToList().HexToInt();
            CheckCodeValueHexString = mydata.Take(4).ByteToHexString(" ");
            #endregion

            #region 窃电标志
            //跳过稽查码
            mydata = mydata.Skip(4);
            int flag = (int)mydata.Take(2).ToList().HexToInt();
            StealEleHexString = mydata.Take(2).ByteToHexString(" ");
            if (StealEleHexString == "00 00")
                StealEleFalg = false;
            if (StealEleHexString == "0F 00")
                StealEleFalg = true;
            #endregion

            #region 表中上次剩余电量
            //跳过窃电标志
            mydata = mydata.Skip(2);
            SurplusEle = (int)mydata.Take(2).ToList().HexToInt();
            SurplusEleHexString = mydata.Take(2).ByteToHexString(" ");
            #endregion
        }
        
        /// <summary>
        /// 表中返回的购电次数
        /// </summary>
        public int ChargeCountsInMeter
        {
            set;
            get;
        }
        /// <summary>
        /// 表中返回的购电次数16进制字符串
        /// </summary>
        public string ChargeCountsInMeterHexString
        {
            set;
            get;
        }
        /// <summary>
        /// 卡中写入的购电次数
        /// </summary>
        public int ChargeCountsInCard
        {
            get;
            set;
        }
        /// <summary>
        /// 卡中写入的购电次数(16进制)
        /// </summary>
        public string ChargeCountsInCardHexString
        {
            get;
            set;
        }
        
        /// <summary>
        /// 卡中的购电量
        /// </summary>
        public int ChargeEleInCard
        {
            get;
            set;
        }
        /// <summary>
        /// 卡中写入的购电次数(16进制)
        /// </summary>
        public string ChargeEleInCardHexString
        {
            get;
            set;
        }
        /// <summary>
        /// 表中总电量
        /// </summary>
        public int SumEleInMeter
        {
            get;
            set;
        }
        /// <summary>
        /// 表中总电量(16进制)
        /// </summary>
        public string SumEleInMeterHexString
        {
            get;
            set;
        }
        /// <summary>
        /// 欠电量
        /// </summary>
        public int TuitionEle
        {
            get;
            set;
        }
        /// <summary>
        /// 欠电量(16进制)
        /// </summary>
        public string TuitionEleHexString
        {
            get;
            set;
        }
        /// <summary>
        /// 稽查码
        /// </summary>
        public int CheckCodeValue
        {
            get;
            set;
        }
        /// <summary>
        /// 稽查码(16进制)
        /// </summary>
        public string CheckCodeValueHexString
        {
            get;
            set;
        }
        /// <summary>
        /// 窃电标志
        /// </summary>
        public bool StealEleFalg
        {
            get;
            set;
        }
        /// <summary>
        /// 窃电标志(16进制)
        /// </summary>
        public string StealEleHexString
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余电量
        /// </summary>
        public int SurplusEle
        {
            get;
            set;
        }
        /// <summary>
        /// 剩余电量(16进制)
        /// </summary>
        public string SurplusEleHexString
        {
            get;
            set;
        }
    }
}
