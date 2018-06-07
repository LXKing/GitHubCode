using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL;
using COMMON.Logs;

namespace BLL.ExamDesign
{
    public class BLL_AddPapers : BLL_Base
    {
        public ResultInfo<object> AddPapers(T_PAPER paper)
        {
            ResultInfo<object> result = null;

            try
            {
                result = base.dbContext.AddEntity(paper);
            }
            catch (Exception ex)
            {
                Log.WriteException("添加试卷异常", ex);
            }

            return result;
        }

        public ResultInfo<object> UpdatePapers(T_PAPER paper)
        {
            ResultInfo<object> result = null;

            try
            {
                var data = base.dbContext.QueryEntitys<T_PAPER>(x => x.ID == paper.ID).FirstOrDefault();

                data.EXAM_TYPE_ID = paper.EXAM_TYPE_ID;
                data.PAPER_NAME = paper.PAPER_NAME;
                data.PAPER_DESC = paper.PAPER_DESC;
                data.MAKE_QUESTION_TYPE = paper.MAKE_QUESTION_TYPE;
                data.PAPER_TYPE = paper.PAPER_TYPE;
                data.REMARK = paper.REMARK;
                data.CREATE_USER_ID = paper.CREATE_USER_ID;
                data.CREATE_DATE = paper.CREATE_DATE;

                result = base.dbContext.UpdateEntitys();
            }
            catch (Exception ex)
            {
                Log.WriteException("修改试卷异常", ex);
            }

            return result;
        }

        public T_PAPER QueryPapersByID(string paperID)
        {
            T_PAPER result = null;

            try
            {
                var pID = Guid.Parse(paperID);

                result = (from a in base.T_PAPER
                          where a.ID == pID
                          select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.WriteException("根据ID查询试卷异常", ex);
            }

            return result;
        }
    }
}
