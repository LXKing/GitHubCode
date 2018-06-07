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
    public partial class AddQuestions : TabBasePage
    {
        BLL_AddQuestions BLLAddQuestions = new BLL_AddQuestions();
        public T_QUESTION ques = new T_QUESTION();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                InitQuestionTypes();
                string optype = Request.QueryString["optype"];

                switch (optype)
                {
                    case "a": // 添加试题
                        hidIsAdd.Value = true;
                        btnSave.Visible = true;
                        sltType.Select(0);
                        sltDifficulty.Select(0);
                        sltDisplay.Select(0);
                        break;

                    case "v": // 查看试题
                        btnSave.Visible = false;
                        InitQuestionInfo(Request.QueryString["id"]);
                        break;

                    case "u": // 修改试题
                        hidIsAdd.Value = false;
                        btnSave.Visible = true;
                        InitQuestionInfo(Request.QueryString["id"]);
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
                var ds = new BLL_QuestionsBankManagement().QueryQuestionsType().Select(a=>new{
                    ID = a.ID.ToString() + "," + a.TEMPLATE_ID.ToString(),
                    QUESTION_TYPE_NAME = a.QUESTION_TYPE_NAME
                }).ToList();

                sltType.GetStore().DataSource = ds;
                sltType.SelectedItems.Add(new Ext.Net.ListItem(ds[0].QUESTION_TYPE_NAME, ds[0].ID));
                hidQuestionTypeID.Value = ds[0].ID.Split(',')[0];
                hidTemplateID.Value = ds[0].ID.Split(',')[1];
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void InitQuestionInfo(string id)
        {
            hidQuestionsID.Value = id;
            var question = new T_QUESTION();
            question = BLLAddQuestions.QueryQuestionsByID(id);
            txtKnow.Text = question.T_KNOWLEDGE.KNOWLEDGE_NAME;
            hidKnowID.Value = question.KNOWLEDGE_ID;
            //sltType.Select(question.QUESTION_TYPE_ID);
            hidQuestionTypeID.Value = question.QUESTION_TYPE_ID;
            hidTemplateID.Value = question.T_QUESTION_TYPE.TEMPLATE_ID;
            var lstType = (this.sltType.GetStore().DataSource.ToJsonString().JsonToObject< List<dynamic>>()).Select(a => new
            {
                ID = a.ID.Value.ToString(),
                QUESTION_TYPE_NAME = a.QUESTION_TYPE_NAME.Value.ToString()
            }).ToList();

            sltType.SelectedItems.Clear();
            lstType.ForEach(a =>
            {
                string ids = a.ID;
                string tpID = ids.Split(',')[0];

                if (tpID == question.QUESTION_TYPE_ID.ToString())
                {
                    sltType.SelectedItems.Add(new Ext.Net.ListItem(a.QUESTION_TYPE_NAME, a.ID));
                }
            });

            sltDifficulty.Select(question.DIFFICULTY);
            sltDisplay.Select(question.SHOW_IN_PRACTICE);
            ques.QUESTION_CONTENT = question.QUESTION_CONTENT;
            ques.QUESTION_OPTIONS = question.QUESTION_OPTIONS;
            sltNum.Select(question.QUESTION_OPTIONS_COUNT);

            if (hidTemplateID.Value.ToString() == "5eb171e0-c68d-44cc-a371-c56c87079eb2")
            {
                rdoGpSingle.Items.ForEach(a =>
                {
                    var b = a as Radio;
                    if (b.BoxLabel == question.ANSWERS)
                    {
                        b.Checked = true;
                    }
                });
            }
            else if (hidTemplateID.Value.ToString() == "22361beb-d5c9-4a49-b14d-0385c5ceb4a4")
            {
                chbGpMultiple.Items.ForEach(a =>
                {
                    var b = a as Checkbox;
                    if (question.ANSWERS.Contains(b.BoxLabel))
                    {
                        b.Checked = true;
                    }
                });
            }
            else if (hidTemplateID.Value.ToString() == "3115bd1f-a88e-42f4-9117-787bccd5a73d")
            {
                rdoGpJudge.Items.ForEach(a =>
                {
                    var b = a as Radio;
                    if (b.BoxLabel == question.ANSWERS)
                    {
                        b.Checked = true;
                    }
                });
            }

            ques.ANSWER_ANALYSIS = question.ANSWER_ANALYSIS;
        }

        /// <summary>
        /// 知识体系列表弹出窗口
        /// </summary>
        protected void GetKnow_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_KNOWLEDGE>("KNOWLEDGE_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                trpnlExt.Tree.SetdataSource(data);
                trpnlExt.SetWindowTitle("知识点选择");
                trpnlExt.Show();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 选择知识点
        /// </summary>
        protected void trpnlExt_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            txtKnow.Text = e.NodeDbClick.Text;
            hidKnowID.Value = e.NodeDbClick.NodeID;
        }

        protected void sltType_Change(object sender, DirectEventArgs e)
        {
            var typeArray = sltType.SelectedItem.Value.ToString().Split(',');
            hidQuestionTypeID.Value = typeArray[0];
            hidTemplateID.Value = typeArray[1];
        }

        protected void btnSave_click(object sender, DirectEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(hidIsAdd.Value))
                {
                    DoAddQuestions();
                }
                else
                {
                    DoUpdateQuestions();
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void DoAddQuestions()
        {
            #region 添加
            var question = new T_QUESTION();
            question.ID = Guid.NewGuid();
            Guid knowID;
            if (Guid.TryParse(hidKnowID.Value.ToString(), out knowID))
            {
                question.KNOWLEDGE_ID = knowID;
            }
            Guid typeID;
            if (Guid.TryParse(hidQuestionTypeID.Value.ToString(), out typeID))
            {
                question.QUESTION_TYPE_ID = typeID;
            }

            question.DIFFICULTY = sltDifficulty.SelectedItem.Value;
            question.SHOW_IN_PRACTICE = sltDisplay.SelectedItem.Value;
            question.QUESTION_CONTENT = Request.Form["content"];
            question.QUESTION_OPTIONS = Request.Form["option"];
            question.QUESTION_OPTIONS_COUNT = Convert.ToInt32(sltNum.SelectedItem.Value);

            if (hidTemplateID.Value.ToString() == "5eb171e0-c68d-44cc-a371-c56c87079eb2")
            {
                question.ANSWERS = rdoGpSingle.CheckedItems[0].BoxLabel;
            }
            else if (hidTemplateID.Value.ToString() == "22361beb-d5c9-4a49-b14d-0385c5ceb4a4")
            {
                foreach (var item in chbGpMultiple.CheckedItems)
                {
                    question.ANSWERS += item.BoxLabel;
                }
            }
            else if (hidTemplateID.Value.ToString() == "3115bd1f-a88e-42f4-9117-787bccd5a73d")
            {
                question.ANSWERS = rdoGpJudge.CheckedItems[0].BoxLabel;
            }
            
            question.ANSWER_ANALYSIS = Request.Form["analyze"];
            question.CREATE_USER_ID = base.LOGIN_USER.ID;
            question.CREATE_DATE = DateTime.Now;

            if (BLLAddQuestions.AddQuestions(question).Success)
            {
                MessageBoxExt.ShowPrompt("添加试题成功!");
            }
            else
            {
                MessageBoxExt.ShowError("添加试题失败!");
            }
            #endregion
        }

        private void DoUpdateQuestions()
        {
            #region 修改
            var question = new T_QUESTION();
            Guid id;
            if (Guid.TryParse(hidQuestionsID.Value.ToString(), out id))
            {
                question.ID = id;
            }
            Guid knowID;
            if (Guid.TryParse(hidKnowID.Value.ToString(), out knowID))
            {
                question.KNOWLEDGE_ID = knowID;
            }
            Guid typeID;
            if (Guid.TryParse(hidQuestionTypeID.Value.ToString(), out typeID))
            {
                question.QUESTION_TYPE_ID = typeID;
            }

            question.DIFFICULTY = sltDifficulty.SelectedItem.Value;
            question.SHOW_IN_PRACTICE = sltDisplay.SelectedItem.Value;
            question.QUESTION_CONTENT = Request.Form["content"];
            question.QUESTION_OPTIONS = Request.Form["option"];
            question.QUESTION_OPTIONS_COUNT = Convert.ToInt32(sltNum.SelectedItem.Value);

            if (hidTemplateID.Value.ToString() == "5eb171e0-c68d-44cc-a371-c56c87079eb2")
            {
                question.ANSWERS = rdoGpSingle.CheckedItems[0].BoxLabel;
            }
            else if (hidTemplateID.Value.ToString() == "22361beb-d5c9-4a49-b14d-0385c5ceb4a4")
            {
                foreach (var item in chbGpMultiple.CheckedItems)
                {
                    question.ANSWERS += item.BoxLabel;
                }
            }
            else if (hidTemplateID.Value.ToString() == "3115bd1f-a88e-42f4-9117-787bccd5a73d")
            {
                question.ANSWERS = rdoGpJudge.CheckedItems[0].BoxLabel;
            }

            question.ANSWER_ANALYSIS = Request.Form["analyze"];
            question.CREATE_USER_ID = base.LOGIN_USER.ID;
            question.CREATE_DATE = DateTime.Now;

            if (BLLAddQuestions.UpdateQuestions(question).Success)
            {
                MessageBoxExt.ShowPrompt("修改试题成功!");
            }
            else
            {
                MessageBoxExt.ShowError("修改试题失败!");
            }
            #endregion
        }
    }
}