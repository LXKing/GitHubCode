using Ext.Net;
using MDL;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Extension.Message;

namespace ExamOnLine.Pages.ExaminationManagement
{
    public partial class PerformanceRulesManage : TabBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            List<V_PERFORMANCE_RULES_INFO> dataList = new PagedList<V_PERFORMANCE_RULES_INFO>();
            int count = 0;

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);//;prms.Page;
                int pageSize = Convert.ToInt32(extraParams["limit"]); //prms.Limit;
                List<Func<V_PERFORMANCE_RULES_INFO, bool>> conditions = new List<Func<V_PERFORMANCE_RULES_INFO, bool>>();

                #region 试卷类型
                Func<V_PERFORMANCE_RULES_INFO, bool> paperTypePredicate = x => true;
                if (txtRulesName.Text.Length > 0)
                {
                    var name = txtRulesName.Text.Trim();
                    paperTypePredicate = x => x.PERFORMANCE_RULES_NAME.Contains(name);
                    conditions.Add(paperTypePredicate);
                }
                #endregion
                var result = QueryPerformanceRulesByPaged(pageIndex, pageSize, conditions);
                dataList = result.ToList();
                count = result.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
            return new { data = dataList, total = count };
        }

        private PagedList<V_PERFORMANCE_RULES_INFO> QueryPerformanceRulesByPaged(int pageIndex, int pageSize, List<Func<V_PERFORMANCE_RULES_INFO, bool>> conditions)
        {
            return new BLL.ExaminationManagement.BLL_PerformanceRulesManage().QueryPerformanceRulesByPaged(conditions, pageIndex, pageSize);
        }

        protected void CommandColumn_DeleteCommand(object sender, DirectEventArgs e)
        {
            try
            {
                var id = JSON.Deserialize<Guid>(e.ExtraParams["id"]);
                var result = new BLL.ExaminationManagement.BLL_PerformanceRulesManage().DeletePerformanceRuleByID(id);
                if(result.Success)
                    MessageBoxExt.ShowPrompt("删除考试成绩规则成功!");
                else
                    MessageBoxExt.ShowPrompt("删除考试成绩规则失败:"+result.Message);
            }
            catch(Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
    }
}