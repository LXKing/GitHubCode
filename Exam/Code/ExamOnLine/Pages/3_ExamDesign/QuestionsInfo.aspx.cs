using BLL.ExamDesign;
using Ext.Extension.Message;
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
    public partial class QuestionsInfo : TabBasePage
    {
        BLL_QuestionsInfo BLLQuestionsInfo = new BLL_QuestionsInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                hidPaperQuestionTypeID.Value = Request.QueryString["id"];
            }
        }

        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<V_PAPER_QUESTION_INFO> data = new List<V_PAPER_QUESTION_INFO>();
            int total = 0;
            var result = QueryPaperByConditions(extraParams);
            total = result.TotalCount;
            data = result.ToList();
            return new { data, total };
        }

        private PagedList<V_PAPER_QUESTION_INFO> QueryPaperByConditions(Dictionary<string, object> extraParams)
        {
            PagedList<V_PAPER_QUESTION_INFO> data = new PagedList<V_PAPER_QUESTION_INFO>();

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);
                int pageSize = Convert.ToInt32(extraParams["limit"]);

                var result = BLLQuestionsInfo.QueryPaperQuestionInfoByPaged(Request.QueryString["id"], pageSize, pageIndex);
                data = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        [DirectMethod(Namespace = "CompanyX")]
        public void Edit(string id, string field, string oldValue, string newValue, object customer)
        {
            try
            {
                var result = BLLQuestionsInfo.UpdatePapersQuestion(id, newValue);

                if (result.Success)
                {
                    this.GridPanel1.GetStore().GetById(id).Commit();
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void CommandColumn_Delete_Command(object sender, DirectEventArgs e)
        {           
            try
            {
                var id = JSON.Deserialize(e.ExtraParams["id"]).ToString();
                List<Guid> idList = new List<Guid>();
                idList.Add(Guid.Parse(id));

                var result = BLLQuestionsInfo.DeletePaperQuestion(idList);

                if (result.Success)
                {
                    MessageBoxExt.ShowPrompt("删除试卷试题成功!");
                    this.GridPanel1.GetStore().Reload();
                }
                else
                {
                    MessageBoxExt.ShowWarning("删除试卷试题失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteMany_Click(object sender, DirectEventArgs e)
        {
            if (GridPanel1.SelectionModel.ToList().Count > 0)
            {
                try
                {
                    var list = GridPanel1.GetSelectionModel() as RowSelectionModel;
                    List<Guid> idList = new List<Guid>();
                    list.SelectedRows.ToList().ForEach(x =>
                    {
                        var id = Guid.Parse(x.RecordID);
                        idList.Add(id);
                    });
                    var result = BLLQuestionsInfo.DeletePaperQuestion(idList);

                    if (result.Success)
                    {
                        MessageBoxExt.ShowPrompt("删除试卷试题成功!");
                        this.GridPanel1.GetStore().Reload();
                    }
                    else
                    {
                        MessageBoxExt.ShowWarning("删除试卷试题失败!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxExt.ShowError(ex.Message);
                }
            }
        }
    }
}