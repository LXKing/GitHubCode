using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Extension.Message;

namespace ExamOnLine.Pages.ExaminationManagement
{
    public partial class AddExamsPlan :TabBasePage
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
            if(this.Request.QueryString["optype"]=="a")
            {

            }
            if(this.Request.QueryString["optype"]=="u")
            {
                Guid id;
                var success = Guid.TryParse(this.Request.QueryString["id"],out id);
                if(success)
                {
                    BindExamPlanEntity(id);
                }
                else
                {
                    MessageBoxExt.ShowError("无效的编辑对象ID!");
                }
            }
            if(this.Request.QueryString["optype"]=="v")
            {
                Guid id;
                var success = Guid.TryParse(this.Request.QueryString["id"], out id);
                if (success)
                {
                    BindExamPlanEntity(id);
                }
                else
                {
                    MessageBoxExt.ShowError("无效的查看对象ID!");
                }

                btnSave.HideMode = HideMode.Display;
                btnSave.Hide();
            }
        }

        private void BindExamPlanEntity(Guid id)
        {
            var entity = new BLL.ExaminationManagement.BLL_AddExamsPlan().QueryExamPlan(id);
            if(entity.NotNull())
            {
                
                

                txtExamPlanName.Text = entity.EXAM_PLAN_NAME.IsNull() ? "" : entity.EXAM_PLAN_NAME;

                numExamTime.Value = entity.TOTAL_TIME.IsNull() ? 30 : entity.TOTAL_TIME;
                numAllowJoinCounts.Value = entity.JOIN_COUNTS.IsNull() ? 1 : entity.JOIN_COUNTS;
                numPaperTotalScore.Value = entity.TOTAL_SCORE.IsNull() ? 100 : entity.TOTAL_SCORE;
                numPassMinScore.Value = entity.PASSING_SCORE.IsNull() ? 60 : entity.PASSING_SCORE;

                dateExamBegin.Value = entity.BEGIN_TIME;
                dateExamEnd.Value = entity.END_TIME;

                #region 安排选项
                if (entity.ARRANGEMENT_OPTIONS == "0")
                {
                    radShowScoreNow.Checked = true;
                    radSettingPublicDate.Checked = false;
                }
                else
                {
                    radSettingPublicDate.Checked = true;
                    radShowScoreNow.Checked = false;
                    dateScorePublic.Value = entity.EXAM_SCORE_PUBLIC_DATE;
                    dateScorePublic.Disabled = false;
                } 
                #endregion

                #region 成绩规则
                 if(entity.T_PERFORMANCE_RULES.NotNull())
                 {
                     hidden_PerformanceRole.Value = entity.PERFORMANCE_RULES_ID.ToString();
                     txtPerformanceRole.Text = entity.T_PERFORMANCE_RULES.PERFORMANCE_RULES_NAME;
                 }
                
                #endregion

                #region 试卷模式
                if(entity.PAPER_MODEL=="0")
                {
                    radSingleModem.Checked = true;
                    radWholeModem.Checked = false;
                }
                else
                {
                    radSingleModem.Checked = false;
                    radWholeModem.Checked = true;
                }
                #endregion

                #region 多选分规则
                if(entity.MUL_CHOICE_GRADING_RULES=="0")
                {
                    radAllRightRules.Checked = true;
                    radAynRightRules.Checked = false;
                }
                else
                {
                    radAllRightRules.Checked =   false;
                    radAynRightRules.Checked = true;
                }
                #endregion

                #region 考完允许阅卷
                if(entity.EXAM_AFTER_ALLOW_VIEW=="0")
                {
                    radAllowView.Checked = true;
                    radForbidView.Checked = false;
                }
                else
                {
                    radAllowView.Checked = false;
                    radForbidView.Checked = true;
                }
                #endregion

                #region 选择试卷
                hidden_PaperID.Value = entity.PAPER_ID.ToString();
                txtPaperName.Text = entity.T_PAPER.PAPER_NAME;
                #endregion

                htmlRemark.Value = entity.REMARK;
            }
        }

        public void  btnSave_Click(object sender,DirectEventArgs e)
        {
            if(this.Request.QueryString["optype"]=="a")
            {
                #region 增加
                AddExamPlan();
                #endregion
            }
            if (this.Request.QueryString["optype"] == "u")
            {
                UpdateExamPlan();
            }
        }

        private void UpdateExamPlan()
        {
            Guid id0;
            var success0 = Guid.TryParse(this.Request.QueryString["id"], out id0);
            if(!success0)
                MessageBoxExt.ShowPrompt("无效的ID!");
            var planEntity = new MDL.T_EXAM_PLAN() { ID=id0 };
            try
            {
                planEntity.EXAM_PLAN_NAME = txtExamPlanName.Text;

                planEntity.TOTAL_TIME = Convert.ToInt32(numExamTime.Value.ToString());
                planEntity.JOIN_COUNTS = Convert.ToInt32(numAllowJoinCounts.Value.ToString());
                planEntity.TOTAL_SCORE = Convert.ToInt32(numPaperTotalScore.Value.ToString());
                planEntity.PASSING_SCORE = Convert.ToInt32(numPassMinScore.Value.ToString());

                planEntity.BEGIN_TIME = Convert.ToDateTime(this.Request.Form["dateExamBegin"]);//dateExamBegin.Value.ToString()
                planEntity.END_TIME = Convert.ToDateTime(this.Request.Form["dateExamEnd"]);//dateExamEnd.Value.ToString()
                if (radShowScoreNow.Checked)
                {
                    planEntity.ARRANGEMENT_OPTIONS = "0";
                    planEntity.EXAM_SCORE_PUBLIC_DATE = null;
                }
                else
                {
                    planEntity.ARRANGEMENT_OPTIONS = "1";
                    planEntity.EXAM_SCORE_PUBLIC_DATE = Convert.ToDateTime(this.Request.Form["dateScorePublic"]);//dateScorePublic.Value.ToString()
                }

                if (hidden_PerformanceRole.Value.ToString().Length > 0)
                {
                    Guid id;
                    var success = Guid.TryParse(hidden_PerformanceRole.Value.ToString(), out id);
                    if (!success)
                        throw new Exception("无效的试卷分数规则ID!");
                    planEntity.PERFORMANCE_RULES_ID = id;
                }

                if (radSingleModem.Checked)
                {
                    planEntity.PAPER_MODEL = "0";
                }
                else
                    planEntity.PAPER_MODEL = "1";


                if (radAllRightRules.Checked)
                {
                    planEntity.MUL_CHOICE_GRADING_RULES = "0";
                }
                else
                {
                    planEntity.MUL_CHOICE_GRADING_RULES = "1";
                }

                if (radAllowView.Checked)
                {
                    planEntity.EXAM_AFTER_ALLOW_VIEW = "0";
                }
                else
                {
                    planEntity.EXAM_AFTER_ALLOW_VIEW = "1";
                }

                if (hidden_PaperID.Value.ToString().Length > 0)
                {
                    Guid id;
                    var success = Guid.TryParse(hidden_PaperID.Value.ToString(), out id);
                    if (!success)
                        throw new Exception("无效的关联试卷ID!");
                    planEntity.PAPER_ID = id;
                }

                planEntity.REMARK = htmlRemark.Value.ToString();
                planEntity.CREATE_USER_ID = base.LOGIN_USER.ID;
                planEntity.CREATE_DATE = System.DateTime.Now;

                var result = new BLL.ExaminationManagement.BLL_AddExamsPlan().UpdateExamPlan(planEntity);
                if (result.Success)
                {
                    MessageBoxExt.ShowPrompt("更新考试安排成功!");
                }
                else
                    MessageBoxExt.ShowError(result.Message);
            }
            catch(Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void AddExamPlan()
        {
            try
            {
                MDL.T_EXAM_PLAN planEntity = new MDL.T_EXAM_PLAN();
                planEntity.ID = Guid.NewGuid();
                planEntity.EXAM_PLAN_NAME = txtExamPlanName.Text;

                planEntity.TOTAL_TIME = Convert.ToInt32(numExamTime.Value.ToString());
                planEntity.JOIN_COUNTS = Convert.ToInt32(numAllowJoinCounts.Value.ToString());
                planEntity.TOTAL_SCORE = Convert.ToInt32(numPaperTotalScore.Value.ToString());
                planEntity.PASSING_SCORE = Convert.ToInt32(numPassMinScore.Value.ToString());

                planEntity.BEGIN_TIME = Convert.ToDateTime(dateExamBegin.Value.ToString());
                planEntity.END_TIME = Convert.ToDateTime(dateExamEnd.Value.ToString());

                if (radShowScoreNow.Checked)
                {
                    planEntity.ARRANGEMENT_OPTIONS = "0";
                    planEntity.EXAM_SCORE_PUBLIC_DATE = null;
                }
                else
                {
                    planEntity.ARRANGEMENT_OPTIONS = "1";
                    planEntity.EXAM_SCORE_PUBLIC_DATE = Convert.ToDateTime(dateScorePublic.Value.ToString());
                }

                if (hidden_PerformanceRole.Value.ToString().Length > 0)
                {
                    Guid id;
                    var success = Guid.TryParse(hidden_PerformanceRole.Value.ToString(), out id);
                    if (!success)
                        throw new Exception("无效的试卷分数规则ID!");
                    planEntity.PERFORMANCE_RULES_ID = id;
                }

               if (radSingleModem.Checked)
                {
                    planEntity.PAPER_MODEL = "0";
                }
                else
                    planEntity.PAPER_MODEL = "1";


               if (radAllRightRules.Checked)
                {
                    planEntity.MUL_CHOICE_GRADING_RULES = "0";
                }
                else
                {
                    planEntity.MUL_CHOICE_GRADING_RULES = "1";
                }

                if (radAllowView.Checked)
                {
                    planEntity.EXAM_AFTER_ALLOW_VIEW = "0";
                }
                else
                {
                    planEntity.EXAM_AFTER_ALLOW_VIEW = "1";
                }

                if (hidden_PaperID.Value.ToString().Length > 0)
                {
                    Guid id;
                    var success = Guid.TryParse(hidden_PaperID.Value.ToString(), out id);
                    if (!success)
                        throw new Exception("无效的关联试卷ID!");
                    planEntity.PAPER_ID = id;
                }

                planEntity.REMARK = htmlRemark.Value.ToString();
                planEntity.CREATE_USER_ID = base.LOGIN_USER.ID;
                planEntity.CREATE_DATE = System.DateTime.Now;

                var result = new BLL.ExaminationManagement.BLL_AddExamsPlan().AddExamPlan(planEntity);
                if(result.Success)
                {
                    MessageBoxExt.ShowPrompt("添加考试安排成功!");
                }
                else
                    MessageBoxExt.ShowError(result.Message);
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
    }
}