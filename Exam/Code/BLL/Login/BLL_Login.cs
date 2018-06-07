using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using MDL;
using COMMON;
namespace BLL.Login
{
    public class BLL_Login:BLL_Base
    {
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ResultInfo<V_USER_INFO> ValidateUser(string loginName, string pwd)
        {
            ResultInfo<V_USER_INFO> result = new ResultInfo<V_USER_INFO>();
            try
            {
                var pwdInDb=Cipher.MD5Encrypt32(pwd);
                var user = base.V_USER_INFO.Where(x => x.LOGIN_NAME == loginName && x.USER_PWD == pwdInDb).FirstOrDefault();
                if (user.IsNull())
                {
                    result.Success = false;
                    result.Message = "用户名或者密码错误,请检查!";

                }
                else
                {
                    if(user.USER_STATUS==0)
                    {
                        result.Success = false;
                        result.Message = "当前用户已经被禁用，请联系相关管理员!";
                    }
                    else
                    {
                        result.Success = true;
                        result.Data = user;
                    }
                    
                }
            }
            catch(Exception ex)
            {
                result.BindAllException(ex);
                result.Message = ex.Message;
            }
            finally
            {

            }
            return result;
        }
    }
}
