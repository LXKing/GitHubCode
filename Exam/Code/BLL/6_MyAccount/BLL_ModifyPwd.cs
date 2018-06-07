using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.MyAccount
{
    public class BLL_ModifyPwd:BLL_Base
    {
        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public ResultInfo<object> UpdatePwd(Guid id,string oldPwd,string newPwd)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var user = base.dbContext.QueryEntitys<MDL.T_USER>(x => x.ID == id).FirstOrDefault();
                if(user.IsNull())
                {
                    throw new Exception("修改密码的用户不存在!");
                }
                if(COMMON.Cipher.MD5Encrypt32(oldPwd)!=user.USER_PWD)
                {
                    throw new Exception("原密码输入错误!");
                }
                user.USER_PWD = COMMON.Cipher.MD5Encrypt32(newPwd);
                var count = base.dbContext.UpdateEntitys();
                result.Success = true;
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
