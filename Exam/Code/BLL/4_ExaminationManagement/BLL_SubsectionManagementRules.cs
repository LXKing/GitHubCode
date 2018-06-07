using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExaminationManagement
{
    public class BLL_SubsectionManagementRules:BLL_Base
    {
        public PagedList<T_PERFORMANCE_RULES_ITEMS> QueryPerformanceRulesItemsPaged(Func<MDL.T_PERFORMANCE_RULES_ITEMS,bool> condition,int pageIndex,int pageSize)
        {
            try
            {
                PagedList<T_PERFORMANCE_RULES_ITEMS> data;

                var query = base.dbContext.QueryEntitys<T_PERFORMANCE_RULES_ITEMS>(condition).AsQueryable();
                query = query.OrderBy(x => x.SEQUENCE);
                data = new PagedList<T_PERFORMANCE_RULES_ITEMS>(query, pageIndex, pageSize);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据ID删除考试成绩段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo<object> DeletePerformanceRuleItemByID(Guid? id)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var entity = this.dbContext.QueryEntitys<MDL.T_PERFORMANCE_RULES_ITEMS>(x => x.ID == id).FirstOrDefault();
                if (entity.IsNull())
                    throw new Exception("未找到要删除的考试成绩段!");
                else
                    result = this.dbContext.DeleteEntity<MDL.T_PERFORMANCE_RULES_ITEMS>(entity);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.BindAllException(ex);
            }
            return result;
        }
    }
}
