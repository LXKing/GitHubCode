using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.ExaminationManagement
{
    public class BLL_AddAuthorUser : BLL_Base
    {
        public PagedList<T_USER> QueryPaperByPaged(int pageSize, int pageIndex,
Func<T_USER, bool> cmbRolePredicate,
Func<T_USER, bool> txtDeptPredicate,
Func<T_USER, bool> txtPosPredicate,
Func<T_USER, bool> txtNamePredicate
)
        {
            var queryResult = base.T_USER.OrderBy(x => x.CREATE_DATE).AsQueryable().
                Where(cmbRolePredicate).
                Where(txtDeptPredicate).
                Where(txtPosPredicate).
                Where(txtNamePredicate).AsQueryable();
            //生成PagedList<T>集合返回
            var pagedList = new PagedList<T_USER>(queryResult, pageIndex, pageSize);

            return pagedList;
        }
    }
}
