using COMMON.Logs;
using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExamDesign
{
    public class BLL_QuestionsInfo : BLL_Base
    {
        public PagedList<V_PAPER_QUESTION_INFO> QueryPaperQuestionInfoByPaged(string typeID, int pageSize, int pageIndex
)
        {
            var tID = Guid.Parse(typeID);

            var queryResult = base.V_PAPER_QUESTION_INFO.Where(a=>a.PAPER_QUESTION_TYPE_ID == tID).OrderBy(x => x.SEQUENCE).AsQueryable();
            //生成PagedList<T>集合返回
            var pagedList = new PagedList<V_PAPER_QUESTION_INFO>(queryResult, pageIndex, pageSize);

            return pagedList;
        }

        public ResultInfo<object> UpdatePapersQuestion(string questionID, string value)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                var qID = Guid.Parse(questionID);
                var data = base.dbContext.QueryEntitys<T_PAPERQUESTION_TYPE_QUESTION>(x => x.ID == qID).FirstOrDefault();
                data.SEQUENCE = value;

                result = base.dbContext.UpdateEntitys();
            }
            catch (Exception ex)
            {
                result.Success = false;
                Log.WriteException("修改试卷试题排序异常", ex);
            }

            return result;
        }

        public ResultInfo<object> DeletePaperQuestion(List<Guid> idList)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var questions = base.T_PAPERQUESTION_TYPE_QUESTION.Where(a => idList.Contains(a.ID)).ToList();
                result = base.dbContext.DeleteEntitys(questions);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.BindAllException(ex);
                Log.WriteException("删除试卷试题异常", ex);
            }
            return result;
        }
    }
}
