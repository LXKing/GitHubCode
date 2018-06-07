using MDL;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace BLL.SystemManagement
{
    public class BLL_UserOnline:BLL_Base
    {
        /// <summary>
        /// 获取根据用户ID获取用户信息
        /// </summary>
        /// <param name="userIDCollection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public PagedList<V_USER_INFO> QueryUserOnlineByPaged(IEnumerable<Guid> userIDCollection,int pageSize,int pageIndex)//,out int totalCount
        {
            //写查询表达式
            var queryResult = userIDCollection
                .Join(
                    base.V_USER_INFO, 
                    (x) => { return x; }, 
                    (y) => { return y.ID; }, 
                    (a, b) => { b.USER_PWD = string.Empty; return b; }
                ).OrderBy(x=>x.LOGIN_NAME).ForEach(x=>x.USER_PWD="").AsQueryable();
            //生成PagedList<T>集合返回
            var pagedList = new PagedList<V_USER_INFO>(queryResult, pageIndex, pageSize);
            return pagedList;
        }
        /// <summary>
        /// 传入表达式
        /// </summary>
        /// <param name="userIDCollection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="express"></param>
        /// <returns></returns>
        public PagedList<V_USER_INFO> QueryUserOnlineByPaged(IEnumerable<Guid> userIDCollection, int pageSize, int pageIndex, 
            Func<V_USER_INFO, bool> rolePredicate, 
            Func<V_USER_INFO, bool> departmentPredicate,
            Func<V_USER_INFO, bool> positionPredicate,
            Func<V_USER_INFO, bool> sexPredicate, 
            Func<V_USER_INFO, bool> namePredicate
            )
        {
            var queryResult = userIDCollection
                .Join(
                    base.V_USER_INFO,
                    (x) => { return x; },
                    (y) => { return y.ID; },
                    (a, b) => { b.USER_PWD = string.Empty; return b; }
                ).OrderBy(x => x.LOGIN_NAME).AsQueryable().
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
