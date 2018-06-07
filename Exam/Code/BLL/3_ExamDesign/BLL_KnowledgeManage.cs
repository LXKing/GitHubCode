using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL;
using COMMON.Logs;

namespace BLL.ExamDesign
{
    public class BLL_KnowledgeManage : BLL_Base
    {
        public ResultInfo<object> AddKnowledge(T_KNOWLEDGE know)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                result = base.dbContext.AddEntity(know);
            }
            catch (Exception ex)
            {
                Log.WriteException("添加知识点异常", ex);
            }

            return result;
        }

        public ResultInfo<object> UpdateKnowledge(T_KNOWLEDGE know)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                var data = base.dbContext.QueryEntitys<T_KNOWLEDGE>(x => x.ID == know.ID).FirstOrDefault();

                data.PARENT_ID = know.PARENT_ID;
                data.SEQUENCE = know.SEQUENCE;
                data.KNOWLEDGE_NAME = know.KNOWLEDGE_NAME;
                data.KNOWLEDGE_DESC = know.KNOWLEDGE_DESC;

                result = base.dbContext.UpdateEntitys();

            }
            catch (Exception ex)
            {
                Log.WriteException("修改知识点异常", ex);
            }

            return result;
        }

        public bool QueryDeletable(string knowID)
        {
            int count = 0;

            try
            {
                var kID = Guid.Parse(knowID);
                count = base.T_KNOWLEDGE.Where(x => x.PARENT_ID == kID).Count();
            }
            catch (Exception ex)
            {
                Log.WriteException("查询是否可以删除知识点异常", ex);
            }

            return count > 0 ? false : true;
        }

        public ResultInfo<object> DeleteKnowledge(string knowID)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            var kID = Guid.Parse(knowID);
            try
            {
                var data = base.dbContext.QueryEntitys<T_KNOWLEDGE>(x => x.ID == kID).FirstOrDefault();

                result = base.dbContext.DeleteEntity<T_KNOWLEDGE>(data);
            }
            catch (Exception ex)
            {
                Log.WriteException("删除知识点异常", ex);
            }

            return result;
        }

        public KnowledgeInfo QueryKnowledgeByID(string knowID)
        {
            KnowledgeInfo result = new KnowledgeInfo();
            var kID = Guid.Parse(knowID);

            try
            {
                var data = (from a in base.T_KNOWLEDGE
                            where a.ID == kID
                            select a).FirstOrDefault();

                result.ID = data.ID;
                result.PARENT_ID = data.PARENT_ID;
                result.SEQUENCE = data.SEQUENCE;
                result.KNOWLEDGE_NAME = data.KNOWLEDGE_NAME;
                result.KNOWLEDGE_DESC = data.KNOWLEDGE_DESC;

                var pareant = (from a in base.T_KNOWLEDGE
                               where a.ID == data.PARENT_ID
                               select a).FirstOrDefault();

                if (pareant != null)
                {
                    result.ParentName = pareant.KNOWLEDGE_NAME;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException("根据ID查询知识点异常", ex);
            }

            return result;
        }
    }

    public class KnowledgeInfo : T_KNOWLEDGE
    {
        public string ParentName { get; set; }
    }
}
