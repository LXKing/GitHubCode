using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExaminationManagement
{
    public class BLL_AddPerformanceRule:BLL_Base
    {
        /// <summary>
        /// 增加考试成绩规则
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultInfo<object> AddPerformanceRule(MDL.T_PERFORMANCE_RULES entity)
        {
            ResultInfo<object> result=new ResultInfo<object>();
            try
            {
                var count = base.dbContext.QueryEntitys<MDL.T_PERFORMANCE_RULES>(x => x.PERFORMANCE_RULES_NAME == entity.PERFORMANCE_RULES_NAME).Count();
                if (count > 0)
                    throw new Exception("已经存在相同的规则名称，请换一个尝试!");
                result = base.dbContext.AddEntity(entity);
            }
            catch(Exception ex)
            {
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 更新考试成绩规则
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultInfo<object> UpdatePerformanceRule(MDL.T_PERFORMANCE_RULES entity)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var count = base.dbContext.QueryEntitys<MDL.T_PERFORMANCE_RULES>(x => x.PERFORMANCE_RULES_NAME == entity.PERFORMANCE_RULES_NAME && x.ID!=entity.ID).Count();
                if (count > 0)
                    throw new Exception("已经存在相同的规则名称，请换一个尝试!");
                result = base.dbContext.UpdateEntity<MDL.T_PERFORMANCE_RULES,string>(entity,"ID");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 根据ID查询考试成绩规则
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_PERFORMANCE_RULES QueryPerformanceRuleByID(Guid id)
        {
           return  base.dbContext.QueryEntitys<MDL.T_PERFORMANCE_RULES>(x => x.ID == id).FirstOrDefault();
        }
    }
}
