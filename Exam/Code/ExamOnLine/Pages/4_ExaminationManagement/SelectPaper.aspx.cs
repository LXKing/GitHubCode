using Ext.Net;
using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Extension.Message;

namespace ExamOnLine.Pages.ExaminationManagement
{
    public partial class SelectPaper : System.Web.UI.Page
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

        private void InitData()
        {
            InitcmbMakeQuestionTypeData();
            InitcmbPaperTypeData();
        }

        private void InitcmbPaperTypeData()
        {
            var data = new BLL.ExaminationManagement.BLL_SelectPaper().QueryAllPaperType();
            cmbPaperType.GetStore().DataSource = data;
        }

        private void InitcmbMakeQuestionTypeData()
        {
            var data = new BLL.ExaminationManagement.BLL_SelectPaper().QueryAllPaperMakeWay();
            cmbMakeQuestionType.GetStore().DataSource = data;
        }

        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            List<V_PAPER_INFO> dataList = new List<V_PAPER_INFO>();
            int count = 0;

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);//;prms.Page;
                int pageSize = Convert.ToInt32(extraParams["limit"]); //prms.Limit;
                List<Func<V_PAPER_INFO, bool>> conditions = new List<Func<V_PAPER_INFO, bool>>();

                #region 试卷类型
                Func<V_PAPER_INFO, bool> paperTypePredicate = x => true;
                if (cmbPaperType.Text.Length > 0)
                {
                    var id = Guid.Parse(cmbPaperType.Text);
                    paperTypePredicate = x => x.PAPER_TYPE == id;
                    conditions.Add(paperTypePredicate);
                }
                #endregion

                #region 出题方式
                Func<V_PAPER_INFO, bool> makePapeWayPredicate = x => true;
                if (cmbMakeQuestionType.Text.Length > 0)
                {
                    var id = Guid.Parse(cmbMakeQuestionType.Text);
                    makePapeWayPredicate = x => x.MAKE_QUESTION_TYPE == id;
                    conditions.Add(makePapeWayPredicate);
                }
                #endregion

                #region 试卷名称
                Func<V_PAPER_INFO, bool> paperNamePredicate = x => true;
                if (txtPaperName.Text.Trim().Length > 0)
                {
                    paperNamePredicate = x => x.PAPER_NAME.Contains(txtPaperName.Text.Trim());
                    conditions.Add(paperNamePredicate);
                }
                #endregion
                var result = QueryPaperInfoByPaged(pageIndex, pageSize, conditions);
                dataList = result.ToList();
                count = result.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
            return new { data = dataList, total = count };
        }
        public PagedList<V_PAPER_INFO> QueryPaperInfoByPaged(int pageIndex, int pageSize,
            IEnumerable<Func<V_PAPER_INFO, bool>> conditions
            )
        {
            var queryResult = new BLL.ExaminationManagement.BLL_SelectPaper().QueryPaperInfoByConditionPaged(conditions, pageIndex, pageSize);
            return queryResult;
        }
    }
}