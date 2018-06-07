using BLL.ExamDesign;
using Ext.Extension.Message;
using Ext.Extension.TreePanelEx;
using Ext.Net;
using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnLine.Pages.ExamDesign
{
    public partial class PapersManagement : System.Web.UI.Page
    {
        BLL_PapersManagement BLLPapersManagement = new BLL_PapersManagement();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 试卷领域列表弹出窗口
        /// </summary>
        protected void GetPaperField_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_EXAM_TYPE>("EXAM_TYPE_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                trpnlPaperField.Tree.SetdataSource(data);
                trpnlPaperField.SetWindowTitle("选择试卷分类");
                trpnlPaperField.Show();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 选择试卷领域
        /// </summary>
        protected void trpnlPaperField_SelectedChange(Ext.Net.ParameterCollection extraParams, NodeEx nodeClick)
        {
            txtPaperField.Text = nodeClick.Text;
        hidPaperFieldID.Value = nodeClick.NodeID;
        }

        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<V_PAPER_INFO> data = new List<V_PAPER_INFO>();
            int total = 0;
            var result = QueryPaperByConditions(extraParams);
            total = result.TotalCount;
            data = result.ToList();
            return new { data, total };
        }

        private PagedList<V_PAPER_INFO> QueryPaperByConditions(Dictionary<string, object> extraParams)
        {
            PagedList<V_PAPER_INFO> data = new PagedList<V_PAPER_INFO>();

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);
                int pageSize = Convert.ToInt32(extraParams["limit"]);
                
                Func<V_PAPER_INFO, bool> cmbPaperTypePredicate = (x) => true;
                Func<V_PAPER_INFO, bool> cmbMakeWayPredicate = (x) => true;
                Func<V_PAPER_INFO, bool> txtPaperFieldPredicate = (x) => true;
                Func<V_PAPER_INFO, bool> txtPaperNamePredicate = (x) => true;

                #region 试卷类型
                if (cmbPaperType.Value.NotNull() && cmbPaperType.Value.ToString().Length > 0)
                {
                    var id = Guid.Parse(cmbPaperType.Value.ToString());
                    cmbPaperTypePredicate = (x) =>
                    {
                        return x.PAPER_TYPE == id;
                    };
                }
                #endregion

                #region 出题方式
                if (cmbMakeWay.Value.NotNull() && cmbMakeWay.Value.ToString().Length > 0)
                {
                    var id = Guid.Parse(cmbMakeWay.Value.ToString());
                    cmbMakeWayPredicate = (x) =>
                    {
                        return x.MAKE_QUESTION_TYPE == id;
                    };
                }
                #endregion

                #region 试卷分类
                if (txtPaperField.Text.Length > 0 && hidPaperFieldID.Value.NotNull() && hidPaperFieldID.ToString().Length > 0)
                {
                    var id = Guid.Parse(hidPaperFieldID.Value.ToString());
                    txtPaperFieldPredicate = (x) =>
                    {
                        return x.EXAM_TYPE_ID == id;
                    };
                }
                #endregion

                #region 试卷名称
                if (txtPaperName.Text.Length > 0)
                {
                    txtPaperNamePredicate = (x) =>
                    {
                        return x.PAPER_NAME == txtPaperName.Text;
                    };
                }
                #endregion

                var result = BLLPapersManagement.QueryPaperByPaged(pageSize, pageIndex,
                    cmbPaperTypePredicate, cmbMakeWayPredicate, txtPaperFieldPredicate, txtPaperNamePredicate);
                data = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        /// <summary>
        /// 删除试卷
        /// </summary>
        protected void CommandColumn_Delete_Command(object sender, DirectEventArgs e)
        {
            try
            {
                var id = JSON.Deserialize(e.ExtraParams["id"]).ToString();
                var result = BLLPapersManagement.DeletePaper(Guid.Parse(id));

                if (result.Success)
                {
                    this.GridPanel1.GetStore().Reload();
                    MessageBoxExt.ShowPrompt("删除试卷成功!");
                }
                else
                {
                    MessageBoxExt.ShowError("删除试卷失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
    }
}