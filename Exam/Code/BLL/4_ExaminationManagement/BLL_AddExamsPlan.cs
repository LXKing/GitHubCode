using MDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.ExaminationManagement
{
    public class BLL_AddExamsPlan:BLL_Base
    {
        /// <summary>
        /// 根据ID查询考试安排对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_EXAM_PLAN QueryExamPlan(Guid? id)
        {
            return base.dbContext.QueryEntitys<T_EXAM_PLAN>(x => x.ID == id).FirstOrDefault();
        }
        /// <summary>
        /// 增加考试安排
        /// </summary>
        /// <param name="planEntity"></param>
        /// <returns></returns>
        public ResultInfo<object> AddExamPlan(T_EXAM_PLAN planEntity)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var count = base.dbContext.QueryEntitys<T_EXAM_PLAN>(x => x.EXAM_PLAN_NAME == planEntity.EXAM_PLAN_NAME).Count();
                if (count > 0)
                    throw new Exception("已经存在了相同的考试安排名称,请更改!");
                result = base.dbContext.AddEntity<T_EXAM_PLAN>(planEntity);
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="planEntity"></param>
        /// <returns></returns>
        public ResultInfo<object> UpdateExamPlan(T_EXAM_PLAN planEntity)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                result = base.dbContext.UpdateEntity<T_EXAM_PLAN, string>(planEntity, "ID");
                //var entity = base.dbContext.QueryEntitys<T_EXAM_PLAN>(x => x.ID == planEntity.ID).FirstOrDefault();
                //if (entity.IsNull())
                //    throw new Exception("不存在要修改的对象!");
                //var newEntity = planEntity.Apply(entity);
                //if(newEntity.IsNull())
                //{
                //    throw new Exception("更新对象值失败!!");
                //}
                //result = base.dbContext.UpdateEntitys();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
    }
}
