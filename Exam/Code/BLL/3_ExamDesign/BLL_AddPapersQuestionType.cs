using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL;
using COMMON.Logs;

namespace BLL.ExamDesign
{
    public class BLL_AddPapersQuestionType : BLL_Base
    {
        public ResultInfo<object> AddPapersQuestionType(T_PAPER_QUESTION_TYPE type, string typeID)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            //string sql = string.Empty;
            try
            {
                #region
//                base.adoDb.BeginTransaction();
//                sql = @"INSERT INTO T_PAPER_QUESTION_TYPE 
//                            (ID, 
//                             QUESTION_TYPE_ID,
//                             EXAM_ID,
//                             TITLE,
//                             SUBTITLE,
//                             SCORE,
//                             LOW_DIFFICULTY_QUESTIONS_COUNT,
//                             MEDIUM_DIFFICULTY_QUESTIONS_COUNT,
//                             HIGH_DIFFICULTY_QUESTIONS_COUNT,
//                             SEQUENCE,
//                             CREATE_USER_ID,
//                             CREATE_DATE) 
//                        VALUES 
//                             (@ID,
//                             @QUESTION_TYPE_ID,
//                             @EXAM_ID,
//                             @TITLE,
//                             @SUBTITLE,
//                             @SCORE,
//                             @LOW_DIFFICULTY_QUESTIONS_COUNT,
//                             @MEDIUM_DIFFICULTY_QUESTIONS_COUNT,
//                             @HIGH_DIFFICULTY_QUESTIONS_COUNT,
//                             @SEQUENCE,
//                             @CREATE_USER_ID,
//                             @CREATE_DATE)";
//                List<System.Data.SqlClient.SqlParameter> paralst = new List<System.Data.SqlClient.SqlParameter>();
//                paralst.Add(new System.Data.SqlClient.SqlParameter("@ID", type.ID));
//                base.adoDb.DBExecuteNonQueryAsTran(sql, paralst);

//                base.adoDb.CommitTransaction();
                #endregion

                var result1 = base.dbContextTran.AddEntityAsTran(type);

                #region 添加领域
                var qTK = new List<MDL.T_QUESTION_TYPE_RF_KNOWLEDGE>();
                var knowID = typeID.Split(';');

                foreach (var item in knowID)
                {
                    qTK.Add(new T_QUESTION_TYPE_RF_KNOWLEDGE() {
                        ID = Guid.NewGuid(),
                        PAPER_QUESTION_TYPE_ID = type.ID,
                        KNOWLEDGE_ID = Guid.Parse(item)
                    });
                }

                var result2 = base.dbContextTran.AddEntitysAsTran(qTK);
                #endregion

                #region 添加问题
                List<Guid?> kID = new List<Guid?>();
                foreach (var item in knowID)
                {
                    kID.Add(Guid.Parse(item));
                }

                var pTQ = new List<MDL.T_PAPERQUESTION_TYPE_QUESTION>();

                if (Convert.ToInt32(type.LOW_DIFFICULTY_QUESTIONS_COUNT) > 0)
                {
                    var low = (from a in base.T_QUESTION
                               where a.QUESTION_TYPE_ID == type.QUESTION_TYPE_ID
                                && kID.Contains(a.KNOWLEDGE_ID)
                                && a.DIFFICULTY == "0"
                               select a.ID).ToList();
                    low = RandomSortList(low).Take(Convert.ToInt32(type.LOW_DIFFICULTY_QUESTIONS_COUNT)).ToList();

                    foreach (var item in low)
                    {
                        pTQ.Add(new T_PAPERQUESTION_TYPE_QUESTION()
                        {
                            ID = Guid.NewGuid(),
                            PAPER_QUESTION_TYPE_ID = type.ID,
                            QUESTIONS_ID = item
                        });
                    }
                }
                if (Convert.ToInt32(type.MEDIUM_DIFFICULTY_QUESTIONS_COUNT) > 0)
                {
                    var mid = (from a in base.T_QUESTION
                               where a.QUESTION_TYPE_ID == type.QUESTION_TYPE_ID
                                && kID.Contains(a.KNOWLEDGE_ID)
                                && a.DIFFICULTY == "1"
                               select a.ID).ToList();
                    mid = RandomSortList(mid).Take(Convert.ToInt32(type.LOW_DIFFICULTY_QUESTIONS_COUNT)).ToList();

                    foreach (var item in mid)
                    {
                        pTQ.Add(new T_PAPERQUESTION_TYPE_QUESTION()
                        {
                            ID = Guid.NewGuid(),
                            PAPER_QUESTION_TYPE_ID = type.ID,
                            QUESTIONS_ID = item
                        });
                    }
                }
                if (Convert.ToInt32(type.HIGH_DIFFICULTY_QUESTIONS_COUNT) > 0)
                {
                    var high = (from a in base.T_QUESTION
                                where a.QUESTION_TYPE_ID == type.QUESTION_TYPE_ID
                                && kID.Contains(a.KNOWLEDGE_ID)
                                && a.DIFFICULTY == "2"
                               select a.ID).ToList();
                    high = RandomSortList(high).Take(Convert.ToInt32(type.HIGH_DIFFICULTY_QUESTIONS_COUNT)).ToList();

                    foreach (var item in high)
                    {
                        pTQ.Add(new T_PAPERQUESTION_TYPE_QUESTION()
                        {
                            ID = Guid.NewGuid(),
                            PAPER_QUESTION_TYPE_ID = type.ID,
                            QUESTIONS_ID = item
                        });
                    }
                }

                var result3 = base.dbContextTran.AddEntitysAsTran(pTQ);
                #endregion

                if (result1.Success && result2.Success && result3.Success)
                {
                   result = base.dbContextTran.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                Log.WriteException("添加试卷题型异常", ex);
            }

            return result;
        }

        private List<T> RandomSortList<T>(List<T> ListT)
        {
            Random random = new Random();
            List<T> newList = new List<T>();
            foreach (T item in ListT)
            {
                newList.Insert(random.Next(newList.Count + 1), item);
            }
            return newList;
        }

        public ResultInfo<object> UpdatePapersQuestionType(string id, T_PAPER_QUESTION_TYPE type, string typeID)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                Guid pQTID = Guid.Parse(id);

                #region 删除原数据
                var questions = base.T_PAPERQUESTION_TYPE_QUESTION.Where(a => a.PAPER_QUESTION_TYPE_ID == pQTID).ToList();
                var result1 = base.dbContextTran.DeleteEntitysAsTran(questions, "ID");

                var knowledges = base.T_QUESTION_TYPE_RF_KNOWLEDGE.Where(a => a.PAPER_QUESTION_TYPE_ID == pQTID).ToList();
                var result2 = base.dbContextTran.DeleteEntitysAsTran(knowledges, "ID");

                var qType = base.T_PAPER_QUESTION_TYPE.Where(a => a.ID == pQTID).FirstOrDefault();
                var result3 = base.dbContextTran.DeleteEntityAsTran(qType);
                #endregion

                var result4 = base.dbContextTran.AddEntityAsTran(type);

                #region 添加领域
                var qTK = new List<MDL.T_QUESTION_TYPE_RF_KNOWLEDGE>();
                var knowID = typeID.Split(';');

                foreach (var item in knowID)
                {
                    qTK.Add(new T_QUESTION_TYPE_RF_KNOWLEDGE()
                    {
                        ID = Guid.NewGuid(),
                        PAPER_QUESTION_TYPE_ID = type.ID,
                        KNOWLEDGE_ID = Guid.Parse(item)
                    });
                }

                var result5 = base.dbContextTran.AddEntitysAsTran(qTK);
                #endregion

                #region 添加问题
                List<Guid?> kID = new List<Guid?>();
                foreach (var item in knowID)
                {
                    kID.Add(Guid.Parse(item));
                }

                var pTQ = new List<MDL.T_PAPERQUESTION_TYPE_QUESTION>();

                if (Convert.ToInt32(type.LOW_DIFFICULTY_QUESTIONS_COUNT) > 0)
                {
                    var low = (from a in base.T_QUESTION
                               where a.QUESTION_TYPE_ID == type.QUESTION_TYPE_ID
                                && kID.Contains(a.KNOWLEDGE_ID)
                                && a.DIFFICULTY == "0"
                               select a.ID).ToList();
                    low = RandomSortList(low).Take(Convert.ToInt32(type.LOW_DIFFICULTY_QUESTIONS_COUNT)).ToList();

                    foreach (var item in low)
                    {
                        pTQ.Add(new T_PAPERQUESTION_TYPE_QUESTION()
                        {
                            ID = Guid.NewGuid(),
                            PAPER_QUESTION_TYPE_ID = type.ID,
                            QUESTIONS_ID = item
                        });
                    }
                }
                if (Convert.ToInt32(type.MEDIUM_DIFFICULTY_QUESTIONS_COUNT) > 0)
                {
                    var mid = (from a in base.T_QUESTION
                               where a.QUESTION_TYPE_ID == type.QUESTION_TYPE_ID
                                && kID.Contains(a.KNOWLEDGE_ID)
                                && a.DIFFICULTY == "1"
                               select a.ID).ToList();
                    mid = RandomSortList(mid).Take(Convert.ToInt32(type.LOW_DIFFICULTY_QUESTIONS_COUNT)).ToList();

                    foreach (var item in mid)
                    {
                        pTQ.Add(new T_PAPERQUESTION_TYPE_QUESTION()
                        {
                            ID = Guid.NewGuid(),
                            PAPER_QUESTION_TYPE_ID = type.ID,
                            QUESTIONS_ID = item
                        });
                    }
                }
                if (Convert.ToInt32(type.HIGH_DIFFICULTY_QUESTIONS_COUNT) > 0)
                {
                    var high = (from a in base.T_QUESTION
                                where a.QUESTION_TYPE_ID == type.QUESTION_TYPE_ID
                                && kID.Contains(a.KNOWLEDGE_ID)
                                && a.DIFFICULTY == "2"
                                select a.ID).ToList();
                    high = RandomSortList(high).Take(Convert.ToInt32(type.HIGH_DIFFICULTY_QUESTIONS_COUNT)).ToList();

                    foreach (var item in high)
                    {
                        pTQ.Add(new T_PAPERQUESTION_TYPE_QUESTION()
                        {
                            ID = Guid.NewGuid(),
                            PAPER_QUESTION_TYPE_ID = type.ID,
                            QUESTIONS_ID = item
                        });
                    }
                }

                var result6 = base.dbContextTran.AddEntitysAsTran(pTQ);
                #endregion

                if (result1.Success && result2.Success && result3.Success 
                    && result4.Success && result5.Success && result6.Success)
                {
                    result = base.dbContextTran.CommitTransaction();
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                Log.WriteException("修改试卷题型异常", ex);
            }

            return result;
        }

        public T_PAPER_QUESTION_TYPE QueryPapersQuestionTypeByID(string typeID)
        {
            T_PAPER_QUESTION_TYPE result = null;

            try
            {
                var tID = Guid.Parse(typeID);

                result = (from a in base.T_PAPER_QUESTION_TYPE
                          where a.ID == tID
                          select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.WriteException("根据ID查询试卷题型异常", ex);
            }

            return result;
        }

        public ResultInfo<object> QueryDifficultyCount(string typeID, string knowledgeID)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                Guid tID = Guid.Parse(typeID);

                if (knowledgeID.Length >= 36)
                {
                    List<Guid?> kID = new List<Guid?>();
                    var knowID = knowledgeID.Split(';');
                    foreach (var item in knowID)
                    {
                        kID.Add(Guid.Parse(item));
                    }

                    result.Data = (from a in base.T_QUESTION
                                   where a.QUESTION_TYPE_ID == tID
                                    && kID.Contains(a.KNOWLEDGE_ID)
                                   group a by a.DIFFICULTY into b
                                   select new { DIFFICULTY = b.Key, Num = b.Count() }).ToList();    
                }
                else
                {
                    result.Data = (from a in base.T_QUESTION
                                   where a.QUESTION_TYPE_ID == tID
                                   group a by a.DIFFICULTY into b
                                   select new { DIFFICULTY = b.Key, Num = b.Count() }).ToList();
                }

                
            }
            catch (Exception ex)
            {
                Log.WriteException("根据知识体系ID查询难度分布异常", ex);
            }

            return result;
        }
    }
}
