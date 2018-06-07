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
    public partial class AddPerformanceRule : TabBasePage
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
            if(Request.QueryString["optype"]=="a")
            {
                
            }
            if (Request.QueryString["optype"] == "u")
            {
                BindEntityData("u");
            }
            if (Request.QueryString["optype"] == "v")
            {
                BindEntityData("v");
            }
        }

        private void BindEntityData(string optype)
        {
            try
            {
                Guid id = Guid.Parse(Request.QueryString["id"]);
                var entity = new BLL.ExaminationManagement.BLL_AddPerformanceRule().QueryPerformanceRuleByID(id);
                if(entity.IsNull())
                {
                    throw new Exception("未能根据ID找到对应的考试成绩规则!");
                }
                else
                {
                    txtRuleName.Text = entity.PERFORMANCE_RULES_NAME;
                    txtSequence.Text = entity.SEQUENCE;
                    txtRuleDescription.Text = entity.REMARK;
                    if(optype=="v")
                    {
                        txtRuleName.ReadOnly = true;
                        txtSequence.ReadOnly = true;
                        txtSequence.ReadOnly = true;

                        btnSave.Hidden = true;
                        btnReturn.StyleSpec = "margin-left:0px;";
                    }
                }
            }
            catch(Exception ex)
            {
                btnSave.Disabled = true;
                MessageBoxExt.ShowError(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender,DirectEventArgs e)
        {
            
            if(this.Request.QueryString["optype"]=="a")
            {
                AddPerformanceRuleEntity();
            }
            if(this.Request.QueryString["optype"]=="u")
            {
                UpdatePerformanceRuleEntity();
            }
        }

        private void AddPerformanceRuleEntity()
        {
            MDL.T_PERFORMANCE_RULES entity = new MDL.T_PERFORMANCE_RULES();
            try
            {
                entity.ID = Guid.NewGuid();
                entity.PERFORMANCE_RULES_NAME = txtRuleName.Text;
                entity.SEQUENCE = txtSequence.Text;
                entity.REMARK = txtRuleDescription.Text;

                entity.CREATE_USER_ID = base.LOGIN_USER.ID;
                entity.CREATE_DATE = System.DateTime.Now;

                var result = new BLL.ExaminationManagement.BLL_AddPerformanceRule().AddPerformanceRule(entity);
                if(result.Success)
                {
                    MessageBoxExt.ShowPrompt("添加考试成绩规则成功!");
                }
                else
                    MessageBoxExt.ShowError("添加考试成绩规则失败:\n\r" + result.ExceptionCollection.LastOrDefault().Message);
            }
            catch(Exception ex)
            {

            }
        }

        private void UpdatePerformanceRuleEntity()
        {
            MDL.T_PERFORMANCE_RULES entity = new MDL.T_PERFORMANCE_RULES();
            try
            {
                var idStr = this.Request.QueryString["id"];
                Guid id;
                if (!Guid.TryParse(idStr, out id))
                    throw new Exception("无效的更新对象ID：" + idStr);
                entity.ID = id;
                entity.PERFORMANCE_RULES_NAME = txtRuleName.Text;
                entity.SEQUENCE = txtSequence.Text;
                entity.REMARK = txtRuleDescription.Text;

                entity.CREATE_USER_ID = base.LOGIN_USER.ID;
                entity.CREATE_DATE = System.DateTime.Now;

                var result = new BLL.ExaminationManagement.BLL_AddPerformanceRule().UpdatePerformanceRule(entity);
                if (result.Success)
                {
                    MessageBoxExt.ShowPrompt("更新考试成绩规则成功!");
                }
                else
                    MessageBoxExt.ShowError("更新考试成绩规则失败:\n\r" + result.ExceptionCollection.LastOrDefault().Message);
            }
            catch (Exception ex)
            {

            }
        }
    }
}