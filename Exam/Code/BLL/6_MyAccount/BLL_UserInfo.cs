using COMMON.Logs;
using MDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.MyAccount
{
    public class BLL_UserInfo : BLL_Base
    {
        public ResultInfo<object> UpdateUser(T_USER user)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                var data = base.dbContext.QueryEntitys<T_USER>(x => x.ID == user.ID).FirstOrDefault();

                data.USER_NAME = user.USER_NAME;
                data.SEX = user.SEX;
                data.IDENTITY_CARD_CODE = user.IDENTITY_CARD_CODE;
                data.USER_TEL = user.USER_TEL;
                data.USER_MOBILE = user.USER_MOBILE;
                data.USER_CONTACT = user.USER_CONTACT;
                data.DEGREE_ID = user.DEGREE_ID;
                data.ADDRESS = user.ADDRESS;
                data.POSTCODE = user.POSTCODE;

                result = base.dbContext.UpdateEntitys();
            }
            catch (Exception ex)
            {
                Log.WriteException("修改用户异常", ex);
            }

            return result;
        }
    }
}
