using COMMON.Logs;
using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExamDesign
{
    public class BLL_PapersManagement : BLL_Base
    {
        public PagedList<V_PAPER_INFO> QueryPaperByPaged(int pageSize, int pageIndex,
    Func<V_PAPER_INFO, bool> cmbPaperTypePredicate,
    Func<V_PAPER_INFO, bool> cmbMakeWayPredicate,
    Func<V_PAPER_INFO, bool> txtPaperFieldPredicate,
    Func<V_PAPER_INFO, bool> txtPaperNamePredicate
    )
        {
            var queryResult = base.V_PAPER_INFO.OrderBy(x => x.CREATE_DATE).AsQueryable().
                Where(cmbPaperTypePredicate).
                Where(cmbMakeWayPredicate).
                Where(txtPaperFieldPredicate).
                Where(txtPaperNamePredicate).AsQueryable();
            //生成PagedList<T>集合返回
            var pagedList = new PagedList<V_PAPER_INFO>(queryResult, pageIndex, pageSize);

            return pagedList;
        }

        public ResultInfo<object> DeletePaper(Guid paperID)
        {
            ResultInfo<object> result = new ResultInfo<object>();

            try
            {
                var data = base.dbContext.QueryEntitys<T_PAPER>(x => x.ID == paperID).FirstOrDefault();

                result = base.dbContext.DeleteEntity<T_PAPER>(data);
            }
            catch (Exception ex)
            {
                Log.WriteException("删除试卷异常", ex);
            }

            return result;
        }
    }
}
