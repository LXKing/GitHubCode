using Ext.Extension.Message;
using Ext.Net;
using MDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnLine.Pages.ExaminationManagement
{
    public partial class SubsectionManagementRules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            List<T_PERFORMANCE_RULES_ITEMS> dataList = new List<T_PERFORMANCE_RULES_ITEMS>();
            int count = 0;

            try
            {
                Func<T_PERFORMANCE_RULES_ITEMS, bool> condition = x => true;
                int pageIndex = Convert.ToInt32(extraParams["page"]);//;prms.Page;
                int pageSize = Convert.ToInt32(extraParams["limit"]); //prms.Limit;
                Guid id;
                var success = Guid.TryParse(Request.QueryString["id"], out id);
                if (!success)
                    throw new Exception("无效的考试成绩规则ID!");

                condition = x => x.PERFORMANCE_RULES_ID == id;

                var result = new BLL.ExaminationManagement.BLL_SubsectionManagementRules().QueryPerformanceRulesItemsPaged(condition, pageIndex, pageSize);
                dataList = result.ToList();
                count = result.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
            return new { data = dataList, total = count };
        }
        /// <summary>
        /// 删除考试分数规则段
        /// </summary>
        /// <param name="snder"></param>
        /// <param name="e"></param>
        protected void CommandColumn_DeleteCommand(object snder, Ext.Net.DirectEventArgs e)
        {
            try
            {
                var id = JSON.Deserialize<Guid>(e.ExtraParams["id"]);
                var result = new BLL.ExaminationManagement.BLL_SubsectionManagementRules().DeletePerformanceRuleItemByID(id);
                if (result.Success)
                {
                    MessageBoxExt.ShowPrompt("删除考试成绩规则段成功!");
                }
                else
                    MessageBoxExt.ShowError("删除考试成绩规则段失败:"+result.Message);
            }
            catch(Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
    }
}