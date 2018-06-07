using COMMON.Logs;
using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExaminationManagement
{
    public class BLL_Authorization : BLL_Base
    {
        public ResultInfo<object> AddAuthors(List<T_AUTHORIZED_EXAM> authors)
        {
            ResultInfo<object> result = null;

            try
            {
                result = base.dbContext.AddEntitys(authors);
            }
            catch (Exception ex)
            {
                Log.WriteException("添加考试授权异常", ex);
            }

            return result;
        }

        public ResultInfo<object> AddAuthorReviwers(List<T_AUTHORIZED_READ_PAPERS_USER> authors)
        {
            ResultInfo<object> result = null;

            try
            {
                result = base.dbContext.AddEntitys(authors);
            }
            catch (Exception ex)
            {
                Log.WriteException("添加评卷人授权异常", ex);
            }

            return result;
        }

        public ResultInfo<object> DeleteAuthors(List<Guid> idList)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var authors = base.T_AUTHORIZED_EXAM.Where(a => idList.Contains(a.ID)).ToList();
                result = base.dbContext.DeleteEntitys(authors);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.BindAllException(ex);
                Log.WriteException("删除考试授权异常", ex);
            }
            return result;
        }

        public ResultInfo<object> DeleteAuthorReviwers(List<Guid> idList)
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var authors = base.T_AUTHORIZED_READ_PAPERS_USER.Where(a => idList.Contains(a.ID)).ToList();
                result = base.dbContext.DeleteEntitys(authors);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.BindAllException(ex);
                Log.WriteException("删除评卷人授权异常", ex);
            }
            return result;
        }

        public PagedList<V_AUTHOR_DEPARTMENT> QueryAuthorDeptByPaged(string examID, int pageSize, int pageIndex
)
        {
            var eID = Guid.Parse(examID);
            var queryResult = base.V_AUTHOR_DEPARTMENT.Where(a => a.EXAM_PLAN_ID == eID).OrderBy(b=>b.DEPARTMENT_NAME).AsQueryable();
            //生成PagedList<T>集合返回
            var pagedList = new PagedList<V_AUTHOR_DEPARTMENT>(queryResult, pageIndex, pageSize);

            return pagedList;
        }

        public PagedList<V_AUTHOR_POSITION> QueryAuthorPosByPaged(string examID, int pageSize, int pageIndex
)
        {
            var eID = Guid.Parse(examID);
            var queryResult = base.V_AUTHOR_POSITION.Where(a => a.EXAM_PLAN_ID == eID).OrderBy(b => b.POSITION_NAME).AsQueryable();
            //生成PagedList<T>集合返回
            var pagedList = new PagedList<V_AUTHOR_POSITION>(queryResult, pageIndex, pageSize);

            return pagedList;
        }

        public PagedList<V_AUTHOR_USER> QueryAuthorUserByPaged(string examID, int pageSize, int pageIndex
)
        {
            var eID = Guid.Parse(examID);
            var queryResult = base.V_AUTHOR_USER.Where(a => a.EXAM_PLAN_ID == eID).OrderBy(b => b.USER_NAME).AsQueryable();
            //生成PagedList<T>集合返回
            var pagedList = new PagedList<V_AUTHOR_USER>(queryResult, pageIndex, pageSize);

            return pagedList;
        }

        public PagedList<V_AUTHOR_REVIWERS> QueryAuthorReviwersByPaged(string examID, int pageSize, int pageIndex
)
        {
            var eID = Guid.Parse(examID);
            var queryResult = base.V_AUTHOR_REVIWERS.Where(a => a.EXAM_PLAN_ID == eID).OrderBy(b => b.USER_NAME).AsQueryable();
            //生成PagedList<T>集合返回
            var pagedList = new PagedList<V_AUTHOR_REVIWERS>(queryResult, pageIndex, pageSize);

            return pagedList;
        }
    }
}
