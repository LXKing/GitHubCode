using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExaminationManagement
{
    public class BLL_PerformanceRulesManage:BLL_Base
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public PagedList<V_PERFORMANCE_RULES_INFO> QueryPerformanceRulesByPaged(IEnumerable<Func<V_PERFORMANCE_RULES_INFO, bool>> conditions, int pageIndex, int pageSize)
        {
            try
            {
                PagedList<V_PERFORMANCE_RULES_INFO> data = new PagedList<V_PERFORMANCE_RULES_INFO>();
                var query = base.V_PERFORMANCE_RULES_INFO.AsQueryable();
                conditions.ToList().ForEach(x =>
                        {
                            query = query.Where(x).AsQueryable();
                        }
                    );
                query = query.OrderByDescending(x => x.SEQUENCE).AsQueryable();
                data = new PagedList<V_PERFORMANCE_RULES_INFO>(query, pageIndex, pageSize);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_PERFORMANCE_RULES QueryPerformanceRuleByID(Guid id)
        {
            return base.dbContext.QueryEntitys<T_PERFORMANCE_RULES>(x => x.ID == id).FirstOrDefault();
        }
        /// <summary>
        /// 删除考试成绩规则
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo<object> DeletePerformanceRuleByID(Guid id)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var entity = QueryPerformanceRuleByID(id);
                if (entity.T_EXAM_PLAN.Count > 0)
                    throw new Exception(string.Format("有{0}个考试安排与之关联,无法进行删除!",entity.T_EXAM_PLAN.Count));
                var r1 = base.dbContextTran.DeleteEntitysAsTran<T_PERFORMANCE_RULES_ITEMS>(entity.T_PERFORMANCE_RULES_ITEMS);
                if(r1.Success)
                {
                    r1 =  base.dbContextTran.DeleteEntityAsTran<T_PERFORMANCE_RULES>(entity);
                    if(!r1.Success)
                        result = r1;
                }
                else
                    result=r1;
                var count0 = entity.T_EXAM_PLAN.Count + 1;
                var count  = base.SaveChanges();
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
                result.BindAllException(ex);
            }
            return result;
        }
    }
}
