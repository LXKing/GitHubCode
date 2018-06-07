using IC.Command;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IC.BLL
{
    /// <summary>
    /// 电表充值相关业务
    /// </summary>
    public class BLL_EleMeterCharge:BLL_Base
    {
        /// <summary>
        /// 开新卡
        /// </summary>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        public ResultInfo<object> NewCard(string cardCode)
        {
            var result = new  ResultInfo<object>();
            try
            {
                #region 写数据库的表,新卡开卡失败,则回滚数据库
                
                #region 创建表信息对象
                var eleMeterInfo = new IC.DATA.T_ELE_METER_INFO();
                eleMeterInfo.CARD_CODE = cardCode;
                eleMeterInfo.ID = Guid.NewGuid();
                eleMeterInfo.CREATE_DATE = System.DateTime.Now;
                #endregion
                db.BeginTransaction();
                var sql = @"insert into T_ELE_METER_INFO (ID,CARD_CODE,CREATE_DATE)values(@ID,@CARD_CODE,@CREATE_DATE)";
                var parmList=new List<SqlParameter>();
                parmList.Add(new SqlParameter() { DbType = System.Data.DbType.Guid,ParameterName="ID",Value=eleMeterInfo.ID});
                parmList.Add(new SqlParameter() { DbType = System.Data.DbType.String, ParameterName = "CARD_CODE", Value = eleMeterInfo.CARD_CODE });
                parmList.Add(new SqlParameter() { DbType = System.Data.DbType.DateTime, ParameterName = "CREATE_DATE", Value = eleMeterInfo.CREATE_DATE });
                var res = db.DBExecuteNonQueryAsTran(sql, parmList);
                if(res.Success)
                {
                    #region 重置新卡
                    //var res1 =  new UserCardCommand().ResetNewCard();
                    //var text = res1.Message;
                    #endregion
                    //db.CommitTransaction();
                }
                else
                {

                }
                db.RollBackTransaction();
                #endregion
            }
            catch(Exception ex)
            {

            }
            return result;
        }
    }
}
