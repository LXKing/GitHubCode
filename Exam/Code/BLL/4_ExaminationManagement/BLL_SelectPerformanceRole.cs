using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExaminationManagement
{
    public class BLL_SelectPerformanceRole:BLL_Base
    {
        /// <summary>
        /// 根据条件分页查询规则
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedList<MDL.T_PERFORMANCE_RULES> QueryPerformanceRulesPaged(Func<T_PERFORMANCE_RULES,bool> expression,int pageIndex,int pageSize)
        {
            PagedList<T_PERFORMANCE_RULES> list = new PagedList<MDL.T_PERFORMANCE_RULES>();
            var query1 = base.T_PERFORMANCE_RULES.Where(expression).OrderBy(x=>x.SEQUENCE).AsQueryable();
            list = new PagedList<T_PERFORMANCE_RULES>(query1, pageIndex, pageSize);
            return list;
        }
    }
}
