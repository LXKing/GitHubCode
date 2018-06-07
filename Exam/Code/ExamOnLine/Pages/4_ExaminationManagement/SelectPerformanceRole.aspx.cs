using Ext.Net;
using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnLine.Pages.ExaminationManagement
{
    public partial class SelectPerformanceRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<T_PERFORMANCE_RULES> dataList = new List<T_PERFORMANCE_RULES>();
            int count = 0;

            int pageIndex = Convert.ToInt32(extraParams["page"]);//;prms.Page;
            int pageSize = Convert.ToInt32(extraParams["limit"]); //prms.Limit;

            Func<T_PERFORMANCE_RULES, bool> performanceRulesNamePredicate = x=>true;
            if(txtPeroformanceRoleName.Text.Trim().Length>0)
            {
                performanceRulesNamePredicate = x => x.PERFORMANCE_RULES_NAME.Contains( txtPeroformanceRoleName.Text.Trim());
            }
            var result = QueryPerformanceByPaged(pageIndex, pageSize, performanceRulesNamePredicate);
            dataList = result.ToList();
            count = result.TotalCount;
            return new { data=result.ToList(), total=result.TotalCount };
        }
        public PagedList<T_PERFORMANCE_RULES> QueryPerformanceByPaged(int pageIndex,int pageSize, 
            Func<T_PERFORMANCE_RULES, bool> performanceRulesNamePredicate
            )
        {
            var queryResult = new BLL.ExaminationManagement.BLL_SelectPerformanceRole().QueryPerformanceRulesPaged(performanceRulesNamePredicate,pageIndex,pageSize);
            return queryResult;
        }
    }
}