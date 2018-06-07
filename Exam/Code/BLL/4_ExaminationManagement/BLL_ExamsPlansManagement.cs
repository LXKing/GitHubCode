using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExaminationManagement
{
    public class BLL_ExamsPlansManagement:BLL_Base
    {
        /// <summary>
        /// 查询所有的出题方式
        /// </summary>
        /// <returns></returns>
        public List<MDL.V_TABLE_CODE_MAKE_WAY> QueryAllPaperMakeWay()
        {
            return base.V_TABLE_CODE_MAKE_WAY.ToList();
        }

        /// <summary>
        /// 分页查询考试安排
        /// </summary>
        /// <param name="predications"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedList<MDL.V_EXAM_PLAN_INFO> QueryExamPlanInfoByConditionsPaged(IEnumerable<Func<MDL.V_EXAM_PLAN_INFO, bool>> predications, int pageIndex, int pageSize, bool expired=false)
        {
            PagedList<V_EXAM_PLAN_INFO> data = new PagedList<MDL.V_EXAM_PLAN_INFO>();
            IQueryable<V_EXAM_PLAN_INFO> query = base.V_EXAM_PLAN_INFO.AsQueryable();
            predications.ToList().ForEach(x => {
                query = query.Where(x).AsQueryable();
            });
            if(expired)
            {
                query = query.Where(x => x.END_TIME < System.DateTime.Now).AsQueryable();
            }
            else
            {
                query = query.Where(x => x.END_TIME >= System.DateTime.Now).AsQueryable();
            }
            query=query.OrderBy(x => x.CREATE_DATE);
            data = new PagedList<V_EXAM_PLAN_INFO>(query, pageIndex, pageSize);
            return data;
        }
        /// <summary>
        /// 删除考试安排
        /// </summary>
        /// <returns></returns>
        public ResultInfo<object> DeleteExamPlan()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                
            }
            catch(Exception ex)
            {
                result.BindAllException(ex);
            }
            return result;
        }
    }
}
