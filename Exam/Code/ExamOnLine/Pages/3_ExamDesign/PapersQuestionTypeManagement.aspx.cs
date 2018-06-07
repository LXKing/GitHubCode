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
    public partial class PapersQuestionTypeManagement : System.Web.UI.Page
    {
        BLL_PapersQuestionTypeManagement BLLPapersQuestionTypeManagement = new BLL_PapersQuestionTypeManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                hidPaperID.Value = Request.QueryString["id"];
            }
        }

        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<V_EXAM_QUESTION_TYPE> data = new List<V_EXAM_QUESTION_TYPE>();
            int total = 0;
            var result = QueryPaperByConditions(extraParams);
            total = result.TotalCount;
            data = result.ToList();
            return new { data, total };
        }

        private PagedList<V_EXAM_QUESTION_TYPE> QueryPaperByConditions(Dictionary<string, object> extraParams)
        {
            PagedList<V_EXAM_QUESTION_TYPE> data = new PagedList<V_EXAM_QUESTION_TYPE>();

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);
                int pageSize = Convert.ToInt32(extraParams["limit"]);

                var result = BLLPapersQuestionTypeManagement.QueryPaperQuestionTypeByPaged(Request.QueryString["id"], pageSize, pageIndex);
                data = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        protected void CommandColumn_Delete_Command(object sender, DirectEventArgs e)
        {
            try
            {
                var id = JSON.Deserialize(e.ExtraParams["id"]).ToString();
                var result = BLLPapersQuestionTypeManagement.DeletePaperQuestionType(Guid.Parse(id));

                if (result.Success)
                {
                    this.GridPanel1.GetStore().Reload();
                    MessageBoxExt.ShowPrompt("删除试卷试题类型成功!");
                }
                else
                {
                    MessageBoxExt.ShowError("删除试卷试题类型失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
    }
}