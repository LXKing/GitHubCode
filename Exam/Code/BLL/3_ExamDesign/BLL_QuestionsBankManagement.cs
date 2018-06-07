using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExamDesign
{
    public class BLL_QuestionsBankManagement:BLL_Base
    {
        /// <summary>
        /// 查询所有的题型
        /// </summary>
        /// <returns></returns>
        public List<T_QUESTION_TYPE> QueryQuestionsType()
        {
            return base.dbContext.QueryEntitys<T_QUESTION_TYPE>().ToList();
        }
        /// <summary>
        /// 根据条件获取分页题目数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="txtKnowledgePredicate"></param>
        /// <param name="txtSubjectPredicate"></param>
        /// <param name="cmbQuestionsTypePredicate"></param>
        /// <param name="cmbDifficultytPredicate"></param>
        /// <param name="cmbStatusPredicate"></param>
        /// <returns></returns>
        public PagedList<V_QUESTION_INFO> QueryQuestionByPaged(int pageSize, int pageIndex,
            Func<V_QUESTION_INFO, bool> txtKnowledgePredicate,
            Func<V_QUESTION_INFO, bool> txtSubjectPredicate,
            Func<V_QUESTION_INFO, bool> cmbQuestionsTypePredicate,
            Func<V_QUESTION_INFO, bool> cmbDifficultytPredicate,
            Func<V_QUESTION_INFO, bool> cmbStatusPredicate
            )
        {
            var queryResult = base.V_QUESTION_INFO.OrderBy(x => x.CREATE_DATE).AsQueryable().
                Where(txtKnowledgePredicate).
                Where(txtSubjectPredicate).
                Where(cmbQuestionsTypePredicate).
                Where(cmbDifficultytPredicate).
                Where(cmbStatusPredicate).AsQueryable();
            //生成PagedList<T>集合返回
            var pagedList = new PagedList<V_QUESTION_INFO>(queryResult, pageIndex, pageSize);
            return pagedList;
        }

        public ResultInfo<object> DeleteQuestions(List<Guid> idList)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                if(idList==null || idList.Count==0)
                {
                    throw new Exception("没有任何要删除的对象!");
                }
                else
                {
                    var objList = base.T_QUESTION.Join(idList, (x) => x.ID, y => y, (q, z) =>   q).ToList();
                    result  = base.dbContext.DeleteEntitys(objList);
                    var count=Convert.ToInt32(result.Data);
                    result.Message = string.Format("成功删除{0}条数据;\n\r失败删除{1}条",count,idList.Count-count);
                    result.Success = true;
                }
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
