using COMMON.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDL;

namespace BLL.OrganizationManagement
{
    public class BLL_AddUser : BLL_Base
    {
        public List<T_DEGREE> QueryDegree()
        {
            var result = new List<T_DEGREE>();

            try
            {
                result = base.T_DEGREE.OrderBy(x => x.SEQUENCE).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteException("查询学历异常", ex);
            }

            return result;
        }

        public string QueryFilePath()
        {
            string result = string.Empty;

            try
            {
                result = (from a in base.T_SYS_FILE_PATH_CONFIG
                          where a.FILE_TYPE == "0"
                          select a.FILE_PATH).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.WriteException("查询头像保存路径异常", ex);
            }

            return result;
        }

        public ResultInfo<object> AddSysFileInfo(T_SYS_FILE_INFO file)
        {
            var result = new ResultInfo<object>();

            try
            {
                result = base.dbContext.AddEntity(file);
            }
            catch (Exception ex)
            {
                Log.WriteException("添加文件信息异常", ex);
            }

            return result;
        }

        public ResultInfo<object> UpdateSysFileInfo(T_SYS_FILE_INFO file)
        {
            var result = new ResultInfo<object>();

            try
            {
                var data = base.dbContext.QueryEntitys<T_SYS_FILE_INFO>(x => x.FILE_TYPE == file.FILE_TYPE && x.UPLOAD_USER_ID == file.UPLOAD_USER_ID).FirstOrDefault();

                data.FILE_EXT_NAME = file.FILE_EXT_NAME;
                data.FILE_NAME = file.FILE_NAME;
                data.FILE_SIZE = file.FILE_SIZE;
                data.UPDATE_DATETIME = file.UPDATE_DATETIME;
                data.UPDATE_USER_ID = file.UPDATE_USER_ID;

                result = base.dbContext.UpdateEntitys();
            }
            catch (Exception ex)
            {
                Log.WriteException("修改文件信息异常", ex);
            }

            return result;
        }

        public bool CheckLoginName(string name)
        {
            try
            {
                var data = (from a in base.T_USER
                            where a.LOGIN_NAME == name
                            select a).Count();
                if (data < 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException("检测用户名异常", ex);
            }

            return false;
        }

        public ResultInfo<object> AddUser(T_USER user)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                result = base.dbContext.AddEntity(user);
            }
            catch (Exception ex)
            {
                Log.WriteException("添加用户异常", ex);
            }

            return result;
        }

        public V_USER_INFO QueryUser(Guid userID)
        {
            V_USER_INFO result = new V_USER_INFO();

            try
            {
                result = (from a in base.V_USER_INFO
                            where a.ID == userID
                            select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.WriteException("根据ID查询用户异常", ex);
            }

            return result;
        }

        public ResultInfo<object> UpdateUser(T_USER user)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                var data = base.dbContext.QueryEntitys<T_USER>(x => x.ID == user.ID).FirstOrDefault();

                data.LOGIN_NAME = user.LOGIN_NAME;
                data.USER_PWD = user.USER_PWD;
                data.USER_NAME = user.USER_NAME;
                data.SEX = user.SEX;
                data.DEPARTMENT_ID = user.DEPARTMENT_ID;
                data.POSITION_ID = user.POSITION_ID;
                data.ROLE_ID = user.ROLE_ID;
                data.IDENTITY_CARD_CODE = user.IDENTITY_CARD_CODE;
                data.USER_TEL = user.USER_TEL;
                data.USER_MOBILE = user.USER_MOBILE;
                data.USER_CONTACT = user.USER_CONTACT;
                data.DEGREE_ID = user.DEGREE_ID;
                data.ADDRESS = user.ADDRESS;
                data.POSTCODE = user.POSTCODE;
                data.CREATE_USER_ID = user.CREATE_USER_ID;
                data.CREATE_DATE = user.CREATE_DATE;
                data.USER_STATUS = user.USER_STATUS;

                result = base.dbContext.UpdateEntitys();

            }
            catch (Exception ex)
            {
                Log.WriteException("修改用户异常", ex);
            }

            return result;
        }

        public ResultInfo<object> DeleteUser(Guid userID)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                var data = base.dbContext.QueryEntitys<T_USER>(x => x.ID == userID).FirstOrDefault();

                result = base.dbContext.DeleteEntity<T_USER>(data);
            }
            catch (Exception ex)
            {
                Log.WriteException("删除用户异常", ex);
            }

            return result;
        }
    }
}
