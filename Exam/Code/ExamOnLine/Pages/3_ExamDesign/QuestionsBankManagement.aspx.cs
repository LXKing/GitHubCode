using Ext.Extension.Message;

using Ext.Net;
using MDL;
using System.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COMMON;
using Ext.Extension.TreePanelEx;

namespace ExamOnLine.Pages.ExamDesign
{
    public partial class QuestionsBankManagement : TabBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (X.IsAjaxRequest)
            {

            }
            else
            {
                InitData();
            }
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            InitQuestionTypes();
        }
        /// <summary>
        /// 初始化题型下拉框数据
        /// </summary>
        private void InitQuestionTypes()
        {
            try
            {
                var ds =new BLL.ExamDesign.BLL_QuestionsBankManagement().QueryQuestionsType();
                cmbQuestionsType.GetStore().DataSource = ds;
            }
            catch(Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<V_QUESTION_INFO> data = new List<V_QUESTION_INFO>();
            int total = 0;
            var result = QueryQuestionByConditions(extraParams);
            total = result.TotalCount;
            data = result.ToList();
            return new { data, total };
        }

        private PagedList<V_QUESTION_INFO> QueryQuestionByConditions(Dictionary<string, object> extraParams)
        {
            PagedList<V_QUESTION_INFO> data = new PagedList<V_QUESTION_INFO>();

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);
                int pageSize = Convert.ToInt32(extraParams["limit"]);

                Func<V_QUESTION_INFO, bool> txtKnowledgePredicate = (x) => true;
                Func<V_QUESTION_INFO, bool> txtSubjectPredicate = (x) => true;
                Func<V_QUESTION_INFO, bool> cmbQuestionsTypePredicate = (x) => true;
                Func<V_QUESTION_INFO, bool> cmbDifficultytPredicate = (x) => true;
                Func<V_QUESTION_INFO, bool> cmbStatusPredicate = (x) => true;

                #region 知识体系
                if (txtKnowledge.Text.Length > 0 && hidden_KnowledgeID.Value.NotNull() && hidden_KnowledgeID.ToString().Length>0)
                {
                    var id = Guid.Parse(hidden_KnowledgeID.Value.ToString());
                    txtKnowledgePredicate = (x) =>
                    {
                        return x.KNOWLEDGE_ID==id;
                    };
                }
                #endregion

                #region 题目内容
                if (txtSubject.Text.Length > 0)
                {
                    txtKnowledgePredicate = (x) =>
                    {
                        return x.QUESTION_CONTENT == txtSubject.Text;
                    };
                }
                #endregion

                #region 题型
                if (cmbQuestionsType.Value.NotNull() && cmbQuestionsType.Value.ToString().Length>0)
                {
                    var id = Guid.Parse(cmbQuestionsType.Value.ToString());
                    cmbQuestionsTypePredicate = (x) =>
                    {
                        return x.QUESTION_TYPE_ID == id;
                    };
                }
                #endregion

                #region 题目难度
                if (cmbDifficulty.Value.NotNull() && cmbDifficulty.Value.ToString().Length>0)
                {
                    var value = cmbDifficulty.Value.ToString();
                    cmbDifficultytPredicate = (x) =>
                    {
                        return x.DIFFICULTY == value;
                    };
                }
                #endregion

                #region 是否显示
                if (cmbStatus.Value.NotNull() && cmbStatus.Value.ToString().Length > 0)
                {
                    var value = cmbStatus.Value.ToString();
                    cmbStatusPredicate = (x) =>
                    {
                        return x.SHOW_IN_PRACTICE == value;
                    };
                }
                #endregion

                var result = new BLL.ExamDesign.BLL_QuestionsBankManagement().QueryQuestionByPaged(pageSize, pageIndex,
                    txtKnowledgePredicate, txtSubjectPredicate, cmbQuestionsTypePredicate, cmbDifficultytPredicate, cmbStatusPredicate);
                data = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        protected void Knowledge_IndicatorIconClick(object sender,DirectEventArgs e)
        {
            try
            {
                var ds = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_KNOWLEDGE>("KNOWLEDGE_NAME", "ID", "PARENT_ID").Select(x => new NodeEx { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID, SEQUENCE = x.SEQUENCE }).ToList();
                treePanelKnowledge.Tree.SetdataSource(ds);
                treePanelKnowledge.Show();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void treePanelKnowledge_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            txtKnowledge.Text = e.NodeDbClick.Text;
            hidden_KnowledgeID.Value=txtKnowledge.TagString = e.NodeDbClick.NodeID;
        }

        protected void CommandColumn_View_Command(object sender,DirectEventArgs e)
        {
            var id = JSON.Deserialize(e.ExtraParams["id"]);
            var hash = AppConst.ManagementOperateType + "=" + AppConst.ManagementOperateTypeView + "&id=" + id;
            X.Redirect("AddQuestions.aspx?" + hash);
        }

        protected void CommandColumn_Edit_Command(object sender, DirectEventArgs e)
        {
            var id = JSON.Deserialize(e.ExtraParams["id"]);
            var hash = AppConst.ManagementOperateType + "=" + AppConst.ManagementOperateTypeUpdate + "&user_id=" + id;
            X.Redirect("AddQuestions.aspx?" + hash);
        }
        protected void CommandColumn_Delete_Command(object sender, DirectEventArgs e)
        {
            var id = JSON.Deserialize(e.ExtraParams["id"]);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteMany_Click(object sender,DirectEventArgs e)
        {
            //X.Msg.Confirm("询问", "确定要删除选中的试题吗？", new MessageBoxButtonsConfig()
            //{
            //    Yes = new MessageBoxButtonConfig() { Text = "YES" },
            //    No = new MessageBoxButtonConfig() { Text = "NO" },
            //    Fn = new JFunction()
            //});
            if(GridPanel1.SelectionModel.ToList().Count>0)
            {
                try
                {
                    var list = GridPanel1.GetSelectionModel() as RowSelectionModel;
                    List<Guid> idList = new List<Guid>();
                    list.SelectedRows.ToList().ForEach(x => {
                        var id =Guid.Parse( x.RecordID);
                        idList.Add(id);
                    });
                    var result = new BLL.ExamDesign.BLL_QuestionsBankManagement().DeleteQuestions(idList);
                    if(result.Success)
                    {
                        MessageBoxExt.ShowPrompt(result.Message);
                        this.GridPanel1.GetStore().Reload();
                    }
                    else
                    {
                        MessageBoxExt.ShowWarning(result.Message);
                    }
                }
                catch(Exception ex)
                {
                    MessageBoxExt.ShowError(ex.Message);
                }
            }
        }
    }
}