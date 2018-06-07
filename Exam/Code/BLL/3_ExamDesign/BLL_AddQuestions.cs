using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL;
using COMMON.Logs;

namespace BLL.ExamDesign
{
    public class BLL_AddQuestions : BLL_Base
    {
        public ResultInfo<object> AddQuestions(T_QUESTION question)
        {
            ResultInfo<object> result = null;

            try
            {
                result = base.dbContext.AddEntity(question);
            }
            catch (Exception ex)
            {
                Log.WriteException("添加试题异常", ex);
            }

            return result;
        }

        public ResultInfo<object> UpdateQuestions(T_QUESTION question)
        {
            ResultInfo<object> result = null;

            try
            {
                var data = base.dbContext.QueryEntitys<T_QUESTION>(x => x.ID == question.ID).FirstOrDefault();

                data.KNOWLEDGE_ID = question.KNOWLEDGE_ID;
                data.QUESTION_TYPE_ID = question.QUESTION_TYPE_ID;
                data.DIFFICULTY = question.DIFFICULTY;
                data.SHOW_IN_PRACTICE = question.SHOW_IN_PRACTICE;
                data.QUESTION_CONTENT = question.QUESTION_CONTENT;
                data.QUESTION_OPTIONS = question.QUESTION_OPTIONS;
                data.QUESTION_OPTIONS_COUNT = question.QUESTION_OPTIONS_COUNT;
                data.ANSWERS = question.ANSWERS;
                data.ANSWER_ANALYSIS = question.ANSWER_ANALYSIS;
                data.CREATE_USER_ID = question.CREATE_USER_ID;
                data.CREATE_DATE = question.CREATE_DATE;

                result = base.dbContext.UpdateEntitys();
            }
            catch (Exception ex)
            {
                Log.WriteException("修改试题异常", ex);
            }

            return result;
        }

        public T_QUESTION QueryQuestionsByID(string questionID)
        {
            T_QUESTION result = null;

            try
            {
                var qID = Guid.Parse(questionID);

                result = (from a in base.T_QUESTION
                            where a.ID == qID
                            select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.WriteException("根据ID查询试题异常", ex);
            }

            return result;
        }
    }
}
