using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MDL;

namespace BLL.MyExaminations
{
    public class BLL_MyExams:BLL_Base
    {
        public PagedList<P_QUERY_MY_EXAMS_Result> QueryMyExam(IEnumerable<Func<P_QUERY_MY_EXAMS_Result, bool>> predications, int pageIndex, int pageSize,Guid userID)
        {
            PagedList<P_QUERY_MY_EXAMS_Result> data = new PagedList<P_QUERY_MY_EXAMS_Result>();
            var query = base.P_QUERY_MY_EXAMS(userID.ToString()).ToList();
            predications.ToList().ForEach(x=>{
                query=query.Where(x).ToList();
            });
            query = query.OrderByDescending(x => x.EXAM_TIME_PERIOD).ToList();
            data = new PagedList<P_QUERY_MY_EXAMS_Result>(query, pageIndex, pageIndex);
            return data;
        }
    }
}
