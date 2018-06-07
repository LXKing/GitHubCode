using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.ExaminationManagement
{
    public class BLL_AddPerformanceRuleItem:BLL_Base
    {
        /// <summary>
        /// 增加成绩规则段
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultInfo<object>  AddPerformanceRuleItem(MDL.T_PERFORMANCE_RULES_ITEMS entity)
        {
            return base.dbContext.AddEntity<MDL.T_PERFORMANCE_RULES_ITEMS>(entity);
        }
        /// <summary>
        /// 更新成绩规则段
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultInfo<object> UpdatePerformanceRuleItem(MDL.T_PERFORMANCE_RULES_ITEMS entity)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var v = base.T_PERFORMANCE_RULES_ITEMS.Where(x => x.ID == entity.ID && x.PERFORMANCE_RULES_ID == entity.PERFORMANCE_RULES_ID).FirstOrDefault();
                if(v.IsNull())
                {
                    throw new Exception("未能找到对应的修改对象！");
                }
                else
                {
                    entity.Apply(v);
                    result =  base.dbContext.UpdateEntitys();
                }
            }
            catch(Exception ex)
            {
                result.BindAllException(ex);
                result.Message = result.ExceptionCollection.LastOrDefault().Message;
            }
            return result;
        }
        /// <summary>
        /// 根据ID查询考试成绩规则段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MDL.T_PERFORMANCE_RULES_ITEMS QueryPerformanceRuleItemByID(Guid? id)
        {
            return base.T_PERFORMANCE_RULES_ITEMS.Where(x => x.ID == id).FirstOrDefault();
        }
    }
}
