using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using COMMON;
using MDL;
namespace BLL.OrganizationManagement
{
    public class BLL_UserManagement:BLL_Base
    {
        /// <summary>
        /// 传入表达式
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="express"></param>
        /// <returns></returns>
        public PagedList<V_USER_INFO> QueryUserInforByPaged(int pageSize, int pageIndex,
            Func<V_USER_INFO, bool> rolePredicate,
            Func<V_USER_INFO, bool> departmentPredicate,
            Func<V_USER_INFO, bool> positionPredicate,
            Func<V_USER_INFO, bool> sexPredicate,
            Func<V_USER_INFO, bool> namePredicate
            )
        {
            var queryResult = base.V_USER_INFO.OrderBy(x => x.LOGIN_NAME).AsQueryable().
                Where(rolePredicate).
                Where(departmentPredicate).
                Where(positionPredicate).
                Where(sexPredicate).
                Where(namePredicate).ForEach(x=>x.USER_PWD="").AsQueryable();
            //生成PagedList<T>集合返回
            var pagedList = new PagedList<V_USER_INFO>(queryResult, pageIndex, pageSize);
            return pagedList;
        }
    }
}
