using MDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.ExamDesign
{
    public class BLL_ExamTypeManagement:BLL_Base
    {
        /// <summary>
        /// 添加考试类别 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultInfo<object> AddExamType(T_EXAM_TYPE entity)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                result = base.dbContext.AddEntity<T_EXAM_TYPE>(entity);
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 根据ID查询考试类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo<object> QueryExamType(Guid? id)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var entity = base.dbContext.QueryEntitys<T_EXAM_TYPE>(x => x.ID == id).FirstOrDefault();
                result.Data = entity;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.BindAllException(ex);
            }
            return result;
        }

        public ResultInfo<object> UpdateExamType(T_EXAM_TYPE entity)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var entity1 = base.T_EXAM_TYPE.Where(x => x.ID == entity.ID).FirstOrDefault();
                if(entity1.NotNull())
                {
                    entity1.EXAM_TYPE_NAME = entity.EXAM_TYPE_NAME;
                    entity1.EXAM_TYPE_DESC = entity.EXAM_TYPE_DESC;
                    entity1.PARENT_ID = entity.PARENT_ID;
                    entity1.SEQUENCE = entity.SEQUENCE;
                    var count = base.SaveChanges();
                    result.Success = true;
                }
                else
                {
                    throw new Exception("未能找到更新的对象!");
                }
                
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 根据ID删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo<object> DeleteExamType(Guid? id)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var childrenCount = base.dbContext.QueryEntitys<T_EXAM_TYPE>(x => x.PARENT_ID == id).Count();
                if(childrenCount>0)
                {
                    throw new Exception("该节点存在子节点，不能直接删除!");
                }

                var entity = base.T_EXAM_TYPE.Where(x => x.ID == id).FirstOrDefault();
                if (entity.NotNull())
                {
                    if(entity.T_PAPER.Count>0)
                    {
                        throw new Exception(string.Format("有{0}套试卷属于该考试分类，无法删除!",entity.T_PAPER.Count));
                    }
                    List<T_EXAM_TYPE> list=new List<MDL.T_EXAM_TYPE>();
                    list.Add(entity);
                    result = base.dbContext.DeleteEntitys<T_EXAM_TYPE, string>(list, "ID");//result = base.dbContext.DeleteEntity<T_EXAM_TYPE>(entity);
                }
                else
                {
                    throw new Exception("未能找到要删除的对象!");
                }

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.BindAllException(ex);
            }
            return result;
        }
    }
}
