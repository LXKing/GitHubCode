using BLL.ExamDesign;
using Ext.Extension.Message;
using Ext.Extension.TreePanelEx;
using Ext.Net;
using MDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnLine.Pages.ExamDesign
{
    public partial class AddPapersQuestionType : TabBasePage
    {
        BLL_AddPapersQuestionType BLLAddPapersQuestionType = new BLL_AddPapersQuestionType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                InitQuestionTypes();
                ClearContent();
                string optype = Request.QueryString["optype"];

                switch (optype)
                {
                    case "a": // 添加试题
                        hidIsAdd.Value = true;
                        btnSave.Visible = true;
                        hidPaperID.Value = Request.QueryString["paperid"];
                        break;

                    case "v": // 查看试题
                        btnSave.Visible = false;
                        InitPaperQuestionTypeInfo(Request.QueryString["id"]);
                        break;

                    case "u": // 修改试题
                        hidIsAdd.Value = false;
                        btnSave.Visible = true;
                        InitPaperQuestionTypeInfo(Request.QueryString["id"]);
                        break;

                    default:
                        break;
                }
            }
        }

        private void InitQuestionTypes()
        {
            try
            {
                var ds = new BLL_QuestionsBankManagement().QueryQuestionsType().Select(a => new
                {
                    ID = a.ID.ToString(),
                    QUESTION_TYPE_NAME = a.QUESTION_TYPE_NAME
                }).ToList();

                cmbType.GetStore().DataSource = ds;
                cmbType.SelectedItems.Clear();
                cmbType.SelectedItems.Add(new Ext.Net.ListItem(ds[0].QUESTION_TYPE_NAME, ds[0].ID));
                hidQuestionTypeID.Value = ds[0].ID;
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void ClearContent()
        {
            txthead.Reset();
            txtsubhead.Text = "";
            txtSequence.Text = "";
            txtScore.Reset();
            txtLow.Reset();
            txtMiddle.Reset();
            txtHight.Reset();
            txtDescript.Text = "";
        }

        private void InitPaperQuestionTypeInfo(string id)
        {
            hidPaperQuetionTypeID.Value = id;
            var qType = new T_PAPER_QUESTION_TYPE();
            qType = BLLAddPapersQuestionType.QueryPapersQuestionTypeByID(id);

            hidQuestionTypeID.Value = qType.QUESTION_TYPE_ID;
            cmbType.SelectedItems.Clear();
            cmbType.SelectedItems.Add(new Ext.Net.ListItem(qType.T_QUESTION_TYPE.QUESTION_TYPE_NAME, qType.QUESTION_TYPE_ID));
            hidPaperID.Value = qType.EXAM_ID;
            txthead.Text = qType.TITLE;
            txtsubhead.Text = qType.SUBTITLE;
            txtSequence.Text = qType.SEQUENCE;
            txtScore.Text = qType.SCORE.ToString();

            string knowledge = string.Empty;
            string knowledgeID = string.Empty;

            foreach (var item in qType.T_QUESTION_TYPE_RF_KNOWLEDGE)
            {
                knowledgeID += item.T_KNOWLEDGE.ID.ToString() + ";";
                knowledge += item.T_KNOWLEDGE.KNOWLEDGE_NAME + ";";
            }

            knowledge = knowledge.Remove(knowledge.Length - 1, 1);
            txtKnowledge.Text = knowledge;
            knowledgeID = knowledgeID.Remove(knowledgeID.Length - 1, 1);
            hidKnowledgeID.Value = knowledgeID;
            
            txtLow.Text = qType.LOW_DIFFICULTY_QUESTIONS_COUNT == null ? "0" : qType.LOW_DIFFICULTY_QUESTIONS_COUNT.ToString();
            txtMiddle.Text = qType.MEDIUM_DIFFICULTY_QUESTIONS_COUNT == null ? "0" : qType.MEDIUM_DIFFICULTY_QUESTIONS_COUNT.ToString();
            txtHight.Text = qType.HIGH_DIFFICULTY_QUESTIONS_COUNT == null ? "0" : qType.HIGH_DIFFICULTY_QUESTIONS_COUNT.ToString();
            txtDescript.Text = qType.REMARK;
        }

        protected void cmbType_Change(object sender, DirectEventArgs e)
        {
            hidQuestionTypeID.Value = cmbType.SelectedItem.Value.ToString();

            var data = BLLAddPapersQuestionType.QueryDifficultyCount(hidQuestionTypeID.Value.ToString(), hidKnowledgeID.Value.ToString()).Data.ToJsonString().JsonToObject<List<DiffCount>>();

            this.ResetDifficultyNum();

            foreach (var item in data)
            {
                if (item.DIFFICULTY == "0")
                {
                    lblLow.Text = item.Num.ToString();
                    txtLow.MaxValue = item.Num;
                }
                else if (item.DIFFICULTY == "1")
                {
                    lblMiddle.Text = item.Num.ToString();
                    txtMiddle.MaxValue = item.Num;
                }
                else if (item.DIFFICULTY == "2")
                {
                    lblHight.Text = item.Num.ToString();
                    txtHight.MaxValue = item.Num;
                }
            }

        }

        private void ResetDifficultyNum()
        {
            lblLow.Text = "0";
            lblMiddle.Text = "0";
            lblHight.Text = "0";
        }

        protected void GetKnowledge_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_KNOWLEDGE>("KNOWLEDGE_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                tplKnowledge.Tree.SetdataSource(data);
                tplKnowledge.Show();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void tplKnowledge_SubmittedNode(object sender, Ext.Extension.TreePanelEx.TreePanelSubmittedNodeEventArgs e)
        {
            string knowledge = string.Empty;
            string knowledgeID = string.Empty;
            
            foreach (var item in e.CheckedNodes)
            {
                knowledge += item.Text + ";";
                knowledgeID += item.NodeID + ";";
            }

            knowledge = knowledge.Remove(knowledge.Length - 1, 1);
            txtKnowledge.Text = knowledge;
            knowledgeID = knowledgeID.Remove(knowledgeID.Length - 1, 1);
            hidKnowledgeID.Value = knowledgeID;

            var data = BLLAddPapersQuestionType.QueryDifficultyCount(hidQuestionTypeID.Value.ToString(), knowledgeID).Data.ToJsonString().JsonToObject<List<DiffCount>>();

            this.ResetDifficultyNum();

            foreach (var item in data)
            {
                if (item.DIFFICULTY == "0")
                {
                    lblLow.Text = item.Num.ToString();
                    txtLow.MaxValue = item.Num;
                }
                else if (item.DIFFICULTY == "1")
                {
                    lblMiddle.Text = item.Num.ToString();
                    txtMiddle.MaxValue = item.Num;
                }
                else if (item.DIFFICULTY == "2")
                {
                    lblHight.Text = item.Num.ToString();
                    txtHight.MaxValue = item.Num;
                }
            }
        }

        protected void btnSave_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(hidIsAdd.Value))
                {
                    DoAddQuestionTypes();
                }
                else
                {
                    DoUpdateQuestionTypes();
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void DoAddQuestionTypes()
        {
            var qType = new T_PAPER_QUESTION_TYPE();
            qType.ID = Guid.NewGuid();
            Guid typeID;
            if (Guid.TryParse(hidQuestionTypeID.Value.ToString(), out typeID))
            {
                qType.QUESTION_TYPE_ID = typeID;
            }
            Guid paperID;
            if (Guid.TryParse(hidPaperID.Value.ToString(), out paperID))
            {
                qType.EXAM_ID = paperID;
            }
            qType.TITLE = txthead.Text;
            qType.SUBTITLE = txtsubhead.Text;
            qType.SEQUENCE = txtSequence.Text;
            qType.SCORE = Convert.ToDecimal(txtScore.Text);
            qType.LOW_DIFFICULTY_QUESTIONS_COUNT = txtLow.Text == "" ? 0 : Convert.ToInt32(txtLow.Text);
            qType.MEDIUM_DIFFICULTY_QUESTIONS_COUNT = txtMiddle.Text == "" ? 0 : Convert.ToInt32(txtMiddle.Text);
            qType.HIGH_DIFFICULTY_QUESTIONS_COUNT = txtHight.Text == "" ? 0 : Convert.ToInt32(txtHight.Text);
            qType.REMARK = txtDescript.Text;
            qType.CREATE_USER_ID = base.LOGIN_USER.ID;
            qType.CREATE_DATE = DateTime.Now;

            if (BLLAddPapersQuestionType.AddPapersQuestionType(qType, hidKnowledgeID.Text).Success)
            {
                MessageBoxExt.ShowPrompt("添加试卷题型成功!");
            }
            else
            {
                MessageBoxExt.ShowError("添加试卷题型失败!");
            }
        }

        private void DoUpdateQuestionTypes()
        {
            var qType = new T_PAPER_QUESTION_TYPE();
            qType.ID = Guid.NewGuid();
            Guid typeID;
            if (Guid.TryParse(hidQuestionTypeID.Value.ToString(), out typeID))
            {
                qType.QUESTION_TYPE_ID = typeID;
            }
            Guid paperID;
            if (Guid.TryParse(hidPaperID.Value.ToString(), out paperID))
            {
                qType.EXAM_ID = paperID;
            }
            qType.TITLE = txthead.Text;
            qType.SUBTITLE = txtsubhead.Text;
            qType.SEQUENCE = txtSequence.Text;
            qType.SCORE = Convert.ToDecimal(txtScore.Text);
            qType.LOW_DIFFICULTY_QUESTIONS_COUNT = txtLow.Text == "" ? 0 : Convert.ToInt32(txtLow.Text);
            qType.MEDIUM_DIFFICULTY_QUESTIONS_COUNT = txtMiddle.Text == "" ? 0 : Convert.ToInt32(txtMiddle.Text);
            qType.HIGH_DIFFICULTY_QUESTIONS_COUNT = txtHight.Text == "" ? 0 : Convert.ToInt32(txtHight.Text);
            qType.REMARK = txtDescript.Text;
            qType.CREATE_USER_ID = base.LOGIN_USER.ID;
            qType.CREATE_DATE = DateTime.Now;

            if (BLLAddPapersQuestionType.UpdatePapersQuestionType(hidPaperQuetionTypeID.Value.ToString(), qType, hidKnowledgeID.Text).Success)
            {
                MessageBoxExt.ShowPrompt("修改试卷题型成功!");
            }
            else
            {
                MessageBoxExt.ShowError("修改试卷题型失败!");
            }
        }
    }

    public class DiffCount
    {
        public string DIFFICULTY { get; set; }
        public int Num { get; set; }
    }
}