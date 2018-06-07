using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExaminationManagement
{
    public class BLL_SelectPaper:BLL_Base
    {
        /// <summary>
        /// 返回所有的试卷类型
        /// </summary>
        /// <returns></returns>
        public List<MDL.V_TABLE_CODE_PAPER_TYPE> QueryAllPaperType()
        {
            return base.V_TABLE_CODE_PAPER_TYPE.ToList();
        }
        /// <summary>
        /// 返回所有的出题方式
        /// </summary>
        /// <returns></returns>
        public List<MDL.V_TABLE_CODE_MAKE_WAY> QueryAllPaperMakeWay()
        {
            return base.V_TABLE_CODE_MAKE_WAY.ToList();
        }

        /// <summary>
        /// 分页查询试卷信息
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedList<V_PAPER_INFO> QueryPaperInfoByConditionPaged(IEnumerable<Func<V_PAPER_INFO,bool>> conditions,int pageIndex,int pageSize)
        {
            PagedList<V_PAPER_INFO> list = new PagedList<V_PAPER_INFO>();
            IQueryable<V_PAPER_INFO> query=base.V_PAPER_INFO.AsQueryable();
            conditions.ToList().ForEach(x => {
                query = query.Where(x).AsQueryable();
            });
            query = query.OrderBy(x => x.CREATE_DATE);
            list = new PagedList<V_PAPER_INFO>(query, pageIndex, pageSize);
            return list;
        }
    }
}
