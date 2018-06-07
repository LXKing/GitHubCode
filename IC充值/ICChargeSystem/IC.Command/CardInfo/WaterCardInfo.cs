using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IC.Command
{
    public class WaterCardInfo:BaseCardInfo
    {
        private ICollection<WaterMeterInfoEntity> waterCollection = new List<WaterMeterInfoEntity>();
        public WaterCardInfo(IEnumerable<byte> data,int areaCode,int cardCode)
        {
            if (data.Count() != 0x60)
                return;
            #region 区号
            base.AreaCode=areaCode;
            base.AreaCodeHexString=areaCode.ToHexByte(4).ByteToHexString(" "); 
            #endregion

            #region 卡号
            base.CardCode=cardCode;
            base.CardCodeHexString=cardCode.ToHexByte(4).ByteToHexString(" "); 
            #endregion

            for(int i=1;i<=6;i++)
            {
                var tmpData=data.Skip(0x10 * (i - 1)).Take(0x10).ToList();
                if(tmpData.Take(1).ToList().HexToInt()==i)
                {
                    var water = new WaterMeterInfoEntity(tmpData, i);
                    waterCollection.Add(water);
                }
            }
        }
        public ICollection<WaterMeterInfoEntity> WaterMetersCollection
        {
            get
            {
                return waterCollection;
            }
            set
            {
                waterCollection = value;
            }
        }
    }

    public class WaterMeterInfoEntity
    {
        public WaterMeterInfoEntity()
        {

        }
        public WaterMeterInfoEntity(IEnumerable<byte> data,int meterNO)
        {
            if (data==null || data.Count() != 0x10)
                return;
            try
            {
                this.MeterNO = meterNO;

                #region 充值次数
                data = data.Skip(1).ToList();
                ChargeCountInCard = data.Take(1).ToList().HexToInt();
                ChargeCountInCardHexString = data.Take(1).ToList().ByteToHexString(" ");
                #endregion

                #region 购水量
                data = data.Skip(1).ToList();
                ChargeWater = data.Take(2).ToList().HexToDouble() * 0.1;
                ChargeWaterHexString = data.Take(2).ToList().ByteToHexString(" ");
                #endregion

                #region 累计用水量
                data = data.Skip(2).ToList();
                SumUsedWater = data.Take(3).ToList().HexToDouble() * 0.01;
                SumUsedWaterHexString = data.Take(3).ToList().ByteToHexString(" ");
                #endregion

                #region 剩余量
                data = data.Skip(3).ToList();
                SurplusWater = data.Take(2).ToList().HexToDouble() * 0.1;
                SumUsedWaterHexString = data.Take(2).ToList().ByteToHexString(" ");
                #endregion
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 水表编号1字节
        /// </summary>
        public int MeterNO
        {
            get;
            set;
        }
        /// <summary>
        /// 购水次数1字节
        /// </summary>
        public int ChargeCountInCard
        {
            get;
            set;
        }
        public string ChargeCountInCardHexString
        {
            get;
            set;
        }
        /// <summary>
        /// 购水量2字节
        /// </summary>
        public double ChargeWater
        {
            get;
            set;
        }
        /// <summary>
        /// 购水量(2字节)
        /// </summary>
        public string ChargeWaterHexString
        {
            get;
            set;
        }
        /// <summary>
        /// 累计用水量3字节
        /// </summary>
        public double SumUsedWater
        {
            get;
            set;
        }
        /// <summary>
        /// 累计使用量(3字节)
        /// </summary>
        public string SumUsedWaterHexString
        {
            get;
            set;
        }
        /// <summary>
        /// 剩余水量(2字节)
        /// </summary>
        public double SurplusWater
        {
            get;
            set;
        }
        /// <summary>
        /// 剩余水量(2字节)
        /// </summary>
        public string SurplusWaterHexString
        {
            get;
            set;
        }
    }
}
