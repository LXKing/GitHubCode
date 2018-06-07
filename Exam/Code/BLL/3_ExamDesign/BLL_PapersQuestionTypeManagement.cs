using COMMON.Logs;
using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExamDesign
{
    public class BLL_PapersQuestionTypeManagement : BLL_Base
    {
        public PagedList<V_EXAM_QUESTION_TYPE> QueryPaperQuestionTypeByPaged(string paperID, int pageSize, int pageIndex
)
        {
            var pID = Guid.Parse(paperID);
            var queryResult = base.V_EXAM_QUESTION_TYPE.Where(a => a.EXAM_ID == pID).OrderBy(x => x.SEQUENCE).AsQueryable();
            //生成PagedList<T>集合返回
            var pagedList = new PagedList<V_EXAM_QUESTION_TYPE>(queryResult, pageIndex, pageSize);

            return pagedList;
        }

        public ResultInfo<object> DeletePaperQuestionType(Guid questionID)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                var questions = base.T_PAPERQUESTION_TYPE_QUESTION.Where(a => a.PAPER_QUESTION_TYPE_ID == questionID).ToList();
                var result1 = base.dbContextTran.DeleteEntitysAsTran(questions, "ID");

                var knowledges = base.T_QUESTION_TYPE_RF_KNOWLEDGE.Where(a => a.PAPER_QUESTION_TYPE_ID == questionID).ToList();
                var result2 = base.dbContextTran.DeleteEntitysAsTran(knowledges, "ID");

                var qType = base.T_PAPER_QUESTION_TYPE.Where(a => a.ID == questionID).FirstOrDefault();
                var result3 = base.dbContextTran.DeleteEntityAsTran(qType);

                if (result1.Success && result2.Success && result3.Success)
                {
                    base.dbContextTran.CommitTransaction();
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                Log.WriteException("删除试卷试题类型异常", ex);
            }

            return result;
        }
    }
}
