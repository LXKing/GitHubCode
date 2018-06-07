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

namespace ExamOnLine.Pages.MyExaminations
{
    public partial class MyExams : TabBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            List<P_QUERY_MY_EXAMS_Result> dataList = new List<P_QUERY_MY_EXAMS_Result>();
            int count = 0;

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);//;prms.Page;
                int pageSize = Convert.ToInt32(extraParams["limit"]); //prms.Limit;
                List<Func<P_QUERY_MY_EXAMS_Result, bool>> conditions = new List<Func<P_QUERY_MY_EXAMS_Result, bool>>();
                if(hidden_ExamTypeID.Text.Length>0)
                {
                    conditions.Add(new Func<P_QUERY_MY_EXAMS_Result, bool>(x => x.EXAM_TYPE_ID == Guid.Parse(hidden_ExamTypeID.Text)));
                }

                if(txtExamName.Text.Length>0)
                {
                    conditions.Add(new Func<P_QUERY_MY_EXAMS_Result, bool>(x => x.EXAM_PLAN_NAME.Contains(txtExamName.Text)));
                }
                //if (cmbMakeQuestionType.SelectedItems.Count > 0 && cmbMakeQuestionType.Text.Length > 0)
                //{
                //    var id = Guid.Parse(cmbMakeQuestionType.Text);
                //    conditions.Add(x => x.MAKE_QUESTION_TYPE_ID == id);
                //}

                //if (hidden_ExamTypeID.Value.ToString().Length > 0)
                //{
                //    var id = Guid.Parse(hidden_ExamTypeID.Value.ToString());
                //    conditions.Add(x => x.EXAM_TYPE_ID == id);
                //}

                //if (txtExamName.Text.Length > 0)
                //{
                //    conditions.Add(x => x.EXAM_PLAN_NAME.Contains(txtExamName.Text));
                //}

                var data = new BLL.MyExaminations.BLL_MyExams().QueryMyExam(conditions, pageIndex, pageSize,base.LOGIN_USER.ID);
                //data.ForEach(x => x.PAPER_MODEL = x.PAPER_MODEL == "0" ? "单卷" : "整卷");
                dataList = data;
                count = data.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
            return new { data = dataList, total = count };
        }
        protected void txtExamType_IndicatorIconClick(object sender,Ext.Net.DirectEventArgs e)
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
    }
}