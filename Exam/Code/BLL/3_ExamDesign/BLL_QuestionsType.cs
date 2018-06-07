using MDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.ExamDesign
{
    public class BLL_QuestionType:BLL_Base
    {
        /// <summary>
        /// 增加题型
        /// </summary>
        /// <param name="questionsType"></param>
        /// <returns></returns>
        public ResultInfo<object> AddQuestionsType(T_QUESTION_TYPE questionsType)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                result = base.dbContext.AddEntity(questionsType);
                return result;
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 根据ID查询题型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo<object> QueryQuestionsTypeByID(Guid id)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            var entity = base.dbContext.QueryEntitys<T_QUESTION_TYPE>(x => x.ID == id).FirstOrDefault();
            if(entity!=null)
            {
                result.Success = true;
                result.Data = entity;
            }
            else
            {
                result.Success = false;
            }
            return result;
        }

        /// <summary>
        /// 更新保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultInfo<object> UpdateQuestionsType(T_QUESTION_TYPE entity)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                result = base.dbContext.UpdateEntitys();
            }
            catch(Exception ex)
            {
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 获取试题模板
        /// </summary>
        /// <returns></returns>
        public ResultInfo<object> QueryQuestionTemplate()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                result.Success = true;
                result.Data = base.dbContext.SqlQueryEntitys<T_QUESTION_TEMPLATE>("select * from T_QUESTION_TEMPLATE",null).ToList();
            }
            catch(Exception ex)
            {
                result.BindAllException(ex);
            }
            return result;
        }
    }
}
