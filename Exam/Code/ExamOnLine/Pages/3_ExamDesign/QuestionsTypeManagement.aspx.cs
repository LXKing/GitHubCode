using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;
using Ext.Net.Utilities;
using Ext.Extension.Message;

using Ext.Extension.Windows;
using COMMON;
using MDL;
using System.Data;

namespace ExamOnLine.Pages.ExamDesign
{
    public partial class QuestionsTypeManagement : TabBasePage
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
            
        }

        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<T_QUESTION_TYPE> data = new List<T_QUESTION_TYPE>();
            int total = 0;
            var result = QueryQuestionTypeByConditions(extraParams);
            total = result.TotalCount;
            result.ToList().ForEach(x => {
                x.T_PAPER_QUESTION_TYPE.Clear();
                x.T_QUESTION.Clear();
            });
            data = result;
            return new { data, total };
        }

        private PagedList<T_QUESTION_TYPE> QueryQuestionTypeByConditions(Dictionary<string, object> extraParams)
        {
            PagedList<T_QUESTION_TYPE> data = new PagedList<T_QUESTION_TYPE>();

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);//;prms.Page;
                int pageSize = Convert.ToInt32(extraParams["limit"]); //prms.Limit;

                Func<T_QUESTION_TYPE, bool> namePredicate = (x) => true;

                #region 模板名称
                if (txtQuestionTypeName.Text.Length>0)
                {
                    var name = txtQuestionTypeName.Text;
                    namePredicate = (x) =>
                    {
                        return x.T_QUESTION_TEMPLATE.TEMPLATE_NAME.Contains(name) || x.QUESTION_TYPE_NAME.Contains(name);
                    };
                }
                #endregion

                var result = new BLL.ExamDesign.BLL_QuestionManagement().QueryQuestionTypeByCondition(pageSize, pageIndex,namePredicate);
                data = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommandColumn_View_Command(object sender,DirectEventArgs e)
        {
            try
            {
                var id = JSON.Deserialize(e.ExtraParams["id"]);
                var hash = AppConst.ManagementOperateType + "=" + AppConst.ManagementOperateTypeView + "&id=" + id + "&return=QuestionsTypeManagement.aspx";
                X.Redirect("AddQuestionsType.aspx?" + hash);
            }
            catch(Exception ex)
            {
                base.WriteException("题型查看异常",ex);
                MessageBoxExt.ShowError("题型查看异常");
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommandColumn_Edit_Command(object sender, DirectEventArgs e)
        {
            try
            {
                var id = JSON.Deserialize(e.ExtraParams["id"]);
                var hash = AppConst.ManagementOperateType + "=" + AppConst.ManagementOperateTypeUpdate + "&id=" + id + "&return=QuestionsTypeManagement.aspx";
                X.Redirect("AddQuestionsType.aspx?" + hash);
            }
            catch (Exception ex)
            {
                base.WriteException("题型编辑异常", ex);
                MessageBoxExt.ShowError("题型编辑异常");
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommandColumn_Delete_Command(object sender, DirectEventArgs e)
        {
            try
            {
                var id = JSON.Deserialize<Guid>(e.ExtraParams["id"]);
                var result = new BLL.ExamDesign.BLL_QuestionManagement().DeleteQuestionType(id);
                if(result.Success)
                {
                    this.GridPanel1.GetStore().Reload();
                }
                else
                {
                    MessageBoxExt.ShowError(result.Message);
                }
            }
            catch (Exception ex)
            {
                base.WriteException("题型删除异常", ex);
                MessageBoxExt.ShowError("题型删除异常");
            }
        }
    }
}