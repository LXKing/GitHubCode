using Ext.Extension.Message;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnLine.Pages.ExaminationManagement
{
    public partial class AddPerformanceRulesItem : TabBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(X.IsAjaxRequest)
            {

            }
            else
            {
                InitData();
            }
        }

        private void InitData()
        {
            if (Request.QueryString["optype"] == "a")
            {

            }
            if(Request.QueryString["optype"]=="u")
            {
                var id = Guid.Parse(Request.QueryString["id1"]);

                var entity = new BLL.ExaminationManagement.BLL_AddPerformanceRuleItem().QueryPerformanceRuleItemByID(id);
                if (entity.NotNull())
                {
                    numBeginScore.Value = entity.BEGIN_SCORE;
                    numEndScore.Value = entity.END_SCORE;
                    txtSequence.Text = entity.SEQUENCE;
                    htmlDescription.Value = entity.PERFORMANCE_RULES_ITEMS_DESC;
                }
            }
        }

        protected void btnSave_Click(object sender,Ext.Net.DirectEventArgs e)
        {
            try
            {
                if (Request.QueryString["optype"] == "a")
                {
                    AddPerformanceRuleItem();
                }
                if (Request.QueryString["optype"] == "u")
                {
                    UpdatePerformanceRuleItem();
                }
            }
            catch(Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void UpdatePerformanceRuleItem()
        {
            var id = Guid.Parse(Request.QueryString["id1"]);
            MDL.T_PERFORMANCE_RULES_ITEMS entity = new MDL.T_PERFORMANCE_RULES_ITEMS();
            entity.ID = id;
            entity.PERFORMANCE_RULES_ID = Guid.Parse(Request.QueryString["id"]);
            entity.BEGIN_SCORE = Convert.ToDecimal(numBeginScore.Value);
            entity.END_SCORE = Convert.ToDecimal(numEndScore.Value);
            entity.SEQUENCE = txtSequence.Text;
            entity.PERFORMANCE_RULES_ITEMS_DESC = htmlDescription.Value.ToString();

            entity.CREATE_DATE = System.DateTime.Now;
            entity.CREATE_USER_ID = base.LOGIN_USER.ID;

            var result = new BLL.ExaminationManagement.BLL_AddPerformanceRuleItem().UpdatePerformanceRuleItem(entity);
            if (result.Success)
            {
                MessageBoxExt.ShowPrompt("更新成绩规则段成功!");
            }
            else
                MessageBoxExt.ShowError("更新成绩规则段失败:" + result.ExceptionCollection.LastOrDefault().Message);
        }

        private void AddPerformanceRuleItem()
        {
            MDL.T_PERFORMANCE_RULES_ITEMS entity = new MDL.T_PERFORMANCE_RULES_ITEMS();
            entity.ID = Guid.NewGuid();
            entity.PERFORMANCE_RULES_ID = Guid.Parse(Request.QueryString["id"]);
            entity.BEGIN_SCORE = Convert.ToDecimal(numBeginScore.Value);
            entity.END_SCORE = Convert.ToDecimal(numEndScore.Value);
            entity.SEQUENCE = txtSequence.Text;
            entity.PERFORMANCE_RULES_ITEMS_DESC = htmlDescription.Value.ToString();

            entity.CREATE_DATE = System.DateTime.Now;
            entity.CREATE_USER_ID = base.LOGIN_USER.ID;

            var result = new BLL.ExaminationManagement.BLL_AddPerformanceRuleItem().AddPerformanceRuleItem(entity);
            if(result.Success)
            {
                MessageBoxExt.ShowPrompt("添加成绩规则段成功!");
            }
            else
                MessageBoxExt.ShowError("添加成绩规则段失败:"+result.ExceptionCollection.LastOrDefault().Message);
        }
    }
}