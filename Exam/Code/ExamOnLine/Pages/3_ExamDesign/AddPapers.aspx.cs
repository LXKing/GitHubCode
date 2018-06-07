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
    public partial class AddPapers : TabBasePage
    {
        BLL_AddPapers BLLAddPapers = new BLL_AddPapers();
        public T_PAPER pap = new T_PAPER();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string optype = Request.QueryString["optype"];

                switch (optype)
                {
                    case "a": // 添加试卷
                        hidIsAdd.Value = true;
                        btnSave.Visible = true;
                        cmbPaperType.Select(0);
                        cmbMakeWay.Select(0);
                        break;

                    case "v": // 查看试卷
                        btnSave.Visible = false;
                        InitPaperInfo(Request.QueryString["id"]);
                        break;

                    case "u": // 修改试卷
                        hidIsAdd.Value = false;
                        btnSave.Visible = true;
                        InitPaperInfo(Request.QueryString["id"]);
                        break;

                    default:
                        break;
                }
            }
        }

        private void InitPaperInfo(string id)
        {
            hidPapersID.Value = id;
            var paper = new T_PAPER();
            paper = BLLAddPapers.QueryPapersByID(id);
            txtPaperName.Text = paper.PAPER_NAME;
            hidPaperFieldID.Value = paper.EXAM_TYPE_ID;
            txtPaperField.Text = paper.T_EXAM_TYPE.EXAM_TYPE_NAME;
            cmbPaperType.Select(paper.PAPER_TYPE);
            cmbMakeWay.Select(paper.MAKE_QUESTION_TYPE);
            pap.PAPER_DESC = paper.PAPER_DESC;
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
        protected void trpnlPaperField_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            txtPaperField.Text = e.NodeDbClick.Text;
            hidPaperFieldID.Value = e.NodeDbClick.NodeID;
        }

        protected void btnSave_click(object sender, DirectEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(hidIsAdd.Value))
                {
                    DoAddPapers();
                }
                else
                {
                    DoUpdatePapers();
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void DoAddPapers()
        {
            #region 添加
            var paper = new T_PAPER();
            paper.ID = Guid.NewGuid();
            paper.PAPER_NAME = txtPaperName.Text.Trim();
            Guid typeID;
            if (Guid.TryParse(hidPaperFieldID.Value.ToString(), out typeID))
            {
                paper.EXAM_TYPE_ID = typeID;
            }
            Guid type;
            if (Guid.TryParse(cmbPaperType.SelectedItem.Value, out type))
            {
                paper.PAPER_TYPE = type;
            }            
            Guid makeWay;
            if (Guid.TryParse(cmbMakeWay.SelectedItem.Value, out makeWay))
            {
                paper.MAKE_QUESTION_TYPE = makeWay;
            }            
            paper.PAPER_DESC = Request.Form["content"];
            paper.CREATE_USER_ID = LOGIN_USER.ID;
            paper.CREATE_DATE = DateTime.Now;


            if (BLLAddPapers.AddPapers(paper).Success)
            {
                MessageBoxExt.ShowPrompt("添加试卷成功!");
            }
            else
            {
                MessageBoxExt.ShowError("添加试卷失败!");
            }
            #endregion
        }

        private void DoUpdatePapers()
        {
            #region 修改
            var paper = new T_PAPER();
            Guid id;
            if (Guid.TryParse(hidPapersID.Value.ToString(), out id))
            {
                paper.ID = id;
            }
            paper.PAPER_NAME = txtPaperName.Text.Trim();
            Guid typeID;
            if (Guid.TryParse(hidPaperFieldID.Value.ToString(), out typeID))
            {
                paper.EXAM_TYPE_ID = typeID;
            }
            Guid type;
            if (Guid.TryParse(cmbPaperType.SelectedItem.Value, out type))
            {
                paper.PAPER_TYPE = type;
            }
            Guid makeWay;
            if (Guid.TryParse(cmbMakeWay.SelectedItem.Value, out makeWay))
            {
                paper.MAKE_QUESTION_TYPE = makeWay;
            }   
            paper.PAPER_DESC = Request.Form["content"];
            paper.CREATE_USER_ID = base.LOGIN_USER.ID;
            paper.CREATE_DATE = DateTime.Now;

            if (BLLAddPapers.UpdatePapers(paper).Success)
            {
                MessageBoxExt.ShowPrompt("修改试卷成功!");
            }
            else
            {
                MessageBoxExt.ShowError("修改试卷失败!");
            }
            #endregion
        }
    }
}