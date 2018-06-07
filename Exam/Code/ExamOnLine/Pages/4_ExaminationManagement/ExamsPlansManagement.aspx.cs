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

namespace ExamOnLine.Pages.ExaminationManagement
{
    public partial class ExamsPlansManagement : TabBasePage
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
            InitPaperMakeWay();
        }
        /// <summary>
        /// 初始化下拉出题方式
        /// </summary>
        private void InitPaperMakeWay()
        {
            var data = new BLL.ExaminationManagement.BLL_ExamsPlansManagement().QueryAllPaperMakeWay();
            cmbMakeQuestionType.GetStore().DataSource = data;
        }
        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            List<V_EXAM_PLAN_INFO> dataList = new List<V_EXAM_PLAN_INFO>();
            int count = 0;

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);//;prms.Page;
                int pageSize = Convert.ToInt32(extraParams["limit"]); //prms.Limit;
                List<Func<V_EXAM_PLAN_INFO, bool>> conditions = new List<Func<V_EXAM_PLAN_INFO, bool>>();

                if(cmbMakeQuestionType.SelectedItems.Count>0 && cmbMakeQuestionType.Text.Length>0)
                {
                    var id=Guid.Parse(cmbMakeQuestionType.Text);
                    conditions.Add(x => x.MAKE_QUESTION_TYPE_ID == id);
                }

                if(hidden_ExamTypeID.Value.ToString().Length>0)
                {
                    var id = Guid.Parse(hidden_ExamTypeID.Value.ToString());
                    conditions.Add(x => x.EXAM_TYPE_ID == id);
                }

                if(txtExamName.Text.Length>0)
                {
                    conditions.Add(x => x.EXAM_PLAN_NAME.Contains(txtExamName.Text));
                }

                var data = new BLL.ExaminationManagement.BLL_ExamsPlansManagement().QueryExamPlanInfoByConditionsPaged(conditions, pageIndex, pageSize);
                data.ForEach(x => x.PAPER_MODEL = x.PAPER_MODEL == "0" ? "单卷" : "整卷");
                dataList = data;
                count = data.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
            return new { data = dataList, total = count };
        }
        /// <summary>
        /// 选择安排分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtExamType_IndicatorIconClick(object sender,DirectEventArgs e)
        {
            var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_EXAM_TYPE>("EXAM_TYPE_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
            treePanelExamTypeList.Tree.SetdataSource(data);
            treePanelExamTypeList.Show();
        }

        protected void treePanelExamTypeList_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            txtExamType.Text = e.NodeDbClick.Text;
            hidden_ExamTypeID.Value = e.NodeDbClick.NodeID;
        }

        protected void CommandColumn_Delete_Command(object sender, DirectEventArgs e)
        {
            var recordID = JSON.Deserialize<Guid>(e.ExtraParams["id"]);

        }
    }
}