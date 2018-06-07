using Ext.Extension.Message;
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
    public partial class AddQuestionsType : TabBasePage
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
        /// 初始化
        /// </summary>
        private void InitData()
        {
            InitQuestionTemplate();

            #region 初始化标题
        var hashCollection = this.Request.QueryString;
        if (hashCollection.AllKeys.Contains("optype"))
        {
            this.hiddenOptype.Value = hashCollection["optype"].ToString();
            if (this.hiddenOptype.Value.ToString() == "a")
            {
                Panel1.Title = "▏" + "增加题型";
            }
            if (this.hiddenOptype.Value.ToString() == "u")
            {
                Panel1.Title = "▏" + "编辑题型";
            }
            if (this.hiddenOptype.Value.ToString() == "v")
            {
                Panel1.Title = "▏" + "查看题型";
            }

            if (hashCollection.AllKeys.Contains("return"))
            {
                this.hiddenReturn.Value = hashCollection["return"].ToString();
            }

            if (hashCollection.AllKeys.Contains("id"))
            {
                this.hiddenID.Value = hashCollection["id"].ToString();
            }
        } 
        #endregion

            #region 增加题型逻辑
            if(this.hiddenOptype.Text=="a")
            {

            }
            #endregion

            #region 编辑题型逻辑
            if (this.hiddenOptype.Text == "u")
            {
                BindQuestionsType();
            }
            #endregion

            #region 查看题型逻辑
            if (this.hiddenOptype.Text == "v")
            {
                BindQuestionsType();

                cmbQuestionTypeTemplate.Selectable = false;
                txtQuestionTypeName.ReadOnly = true;
                txtSequence.ReadOnly = true;
                numScore.Editable = false;
                numScore.MouseWheelEnabled = false;
                numScore.SpinDownEnabled = false;
                numScore.SpinUpEnabled = false;
                btnSave.Hidden = true;
            }
            #endregion
        }
        /// <summary>
        /// 绑定
        /// </summary>
        private void InitQuestionTemplate()
        {
            try
            {
                var result = new BLL.ExamDesign.BLL_QuestionType().QueryQuestionTemplate();
                if(result.Success)
                {
                    var ds = (result.Data as List<T_QUESTION_TEMPLATE>).OrderBy(x=>x.SEQUENCE).ToList();
                    cmbQuestionTypeTemplate.GetStore().DataSource = ds;
                }
            }
            catch(Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void BindQuestionsType()
        {
            try
            {
                Guid id;
                var success = Guid.TryParse(hiddenID.Text, out id);
                if (!success)
                {
                    MessageBoxExt.ShowError("无效的题型ID!");
                    return;
                }

                if (id != null)
                {
                    var result = new BLL.ExamDesign.BLL_QuestionType().QueryQuestionsTypeByID(id);
                    if (result.Success)
                    {
                        var entity = result.Data as T_QUESTION_TYPE;

                        var item = (cmbQuestionTypeTemplate.GetStore().DataSource as List<T_QUESTION_TEMPLATE>) .Where(x => x.ID == entity.TEMPLATE_ID).FirstOrDefault();
                        if (item != null)
                        {
                            cmbQuestionTypeTemplate.SelectedItems.Clear();
                            cmbQuestionTypeTemplate.SelectedItems.Add(item);
                        }

                        txtQuestionTypeName.Text = entity.QUESTION_TYPE_NAME.IsNull() ? "" : entity.QUESTION_TYPE_NAME;
                        txtSequence.Text = entity.SEQUENCE.IsNull() ? "" : entity.SEQUENCE;
                        numScore.Number = entity.SCORE.IsNull() ? Convert.ToDouble(0) : Convert.ToDouble(entity.SCORE);

                        btnClear.Hidden = true;
                    }
                }
                else
                    MessageBoxExt.ShowError("无效的题型ID!");
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void btnSave_Click(object sender, DirectEventArgs e)
        {
            //增加题型
            if(hiddenOptype.Text=="a")
            {
                T_QUESTION_TYPE entity = new T_QUESTION_TYPE();
                AddQuestionsTypeEntity(entity);
            }

            //查看题型
            if (hiddenOptype.Text == "u")
            {
                UpdateQuestionsType();
            }
        }
        /// <summary>
        /// 更新题型
        /// </summary>
        private void UpdateQuestionsType()
        {
            try
            {
                Guid id;
                var success = Guid.TryParse(hiddenID.Text, out id);
                if (!success)
                {
                    MessageBoxExt.ShowError("无效的题型ID!");
                    return;
                }

                if (id != null)
                {
                    var bll = new BLL.ExamDesign.BLL_QuestionType();
                    var result = bll.QueryQuestionsTypeByID(id);
                    if (result.Success)
                    {
                        var entity = result.Data as T_QUESTION_TYPE;
                        entity.TEMPLATE_ID = Guid.Parse(cmbQuestionTypeTemplate.Value.ToString());
                        entity.QUESTION_TYPE_NAME = txtQuestionTypeName.Text;
                        entity.SEQUENCE = txtSequence.Text;
                        entity.SCORE = Convert.ToDecimal(numScore.Number);

                        var result1 = bll.UpdateQuestionsType(entity);
                        if (result.Success)
                        {
                            MessageBoxExt.ShowPrompt("题型保存成功!");
                        }
                        else
                        {
                            MessageBoxExt.ShowError(result1.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void AddQuestionsTypeEntity(T_QUESTION_TYPE entity)
        {
            try
            {
                entity.ID = Guid.NewGuid();
                entity.TEMPLATE_ID = Guid.Parse(cmbQuestionTypeTemplate.Value.ToString());
                entity.QUESTION_TYPE_NAME = txtQuestionTypeName.Text;
                entity.SEQUENCE = txtSequence.Text;
                entity.SCORE = Convert.ToDecimal(numScore.Number);

                var result = new BLL.ExamDesign.BLL_QuestionType().AddQuestionsType(entity);
                if (result.Success)
                {
                    MessageBoxExt.ShowPrompt("题型添加成功!");
                    btnClear.FireEvent("click");
                }
                else
                {
                    MessageBoxExt.ShowError(result.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
    }
}