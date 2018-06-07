using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExamDesign
{
    public class BLL_QuestionManagement:BLL_Base
    {
        /// <summary>
        /// 按条件查询题型，并进行分页
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="namePredicate"></param>
        /// <returns></returns>
        public PagedList<T_QUESTION_TYPE> QueryQuestionTypeByCondition(int pageSize, int pageIndex,Func<T_QUESTION_TYPE, bool> namePredicate)
        {
            var queryResult = base.T_QUESTION_TYPE.OrderBy(x => x.SEQUENCE).AsQueryable().
                Where(namePredicate).ForEach(x => {
                    x.T_PAPER_QUESTION_TYPE.Clear();
                    x.T_QUESTION.Clear();
                }).AsQueryable();
            //生成PagedList<T>集合返回
            var pagedList = new PagedList<T_QUESTION_TYPE>(queryResult, pageIndex, pageSize);
            return pagedList;
        }
        /// <summary>
        /// 根据ID删除题型
        /// </summary>
        /// <param name="qTypeID"></param>
        /// <returns></returns>
        public ResultInfo<object> DeleteQuestionType(Guid qTypeID)
        {
            var qType = base.dbContext.QueryEntitys<T_QUESTION_TYPE>(x => x.ID == qTypeID).FirstOrDefault();
            var result = base.dbContext.DeleteEntity<T_QUESTION_TYPE>(qType);
            return result;
        }
    }
}
