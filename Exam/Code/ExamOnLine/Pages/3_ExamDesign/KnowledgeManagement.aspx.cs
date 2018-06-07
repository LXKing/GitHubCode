using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;
using MDL;

using Ext.Extension.Message;
using BLL.ExamDesign;
using Ext.Extension.TreePanelEx;

namespace ExamOnLine.Pages.ExamDesign
{
    public partial class KnowledgeManagement : TabBasePage
    {
        private BLL_KnowledgeManage KnowledgeManage = new BLL_KnowledgeManage();

        /// <summary>
        /// 获取左侧知识体系列表
        /// </summary>
        private void GetKnowledge()
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_KNOWLEDGE>("KNOWLEDGE_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                knowledge1.Tree.SetdataSource(data);
                //knowledge1.ExpandAllNode();
                this.contain.UpdateContent();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                btnDelete.Hidden = true;
                hidIsAdd.Value = true;
                this.GetKnowledge();
            }
        }

        /// <summary>
        /// 页面加载完成，展开所有知识体系
        /// </summary>
        public override void OnLoadCompleted(EventArgs e)
        {
            base.OnLoadCompleted(e);
            knowledge1.Tree.ExpandAllNode();
            knowledge2.Tree.ExpandAllNode();
        }

        /// <summary>
        /// 单击左侧知识体系列表，查看知识体系信息
        /// </summary>
        protected void knowledge1_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            try
            {
                var data = KnowledgeManage.QueryKnowledgeByID(e.NodeDbClick.NodeID);
                if (data.ID != null)
                {
                    hidID.Value = data.ID;
                    hidParentID.Value = data.PARENT_ID;
                    txtKnowParent.Text = data.ParentName;
                    txtKnowName.Text = data.KNOWLEDGE_NAME;
                    txtKnowDescript.Text = data.KNOWLEDGE_DESC;
                    txtKnowSequence.Text = data.SEQUENCE;
                    btnDelete.Hidden = false;
                    hidIsAdd.Value = false;
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 知识体系列表弹出窗口
        /// </summary>
        protected void GetKnowledge_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_KNOWLEDGE>("KNOWLEDGE_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                knowledge2.Tree.SetdataSource(data);
                knowledge2.SetWindowTitle("知识点选择");
                knowledge2.Show();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 选择知识体系
        /// </summary>
        protected void knowledge2_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            txtKnowParent.Text = e.NodeDbClick.Text;
            hidParentID.Value = e.NodeDbClick.NodeID;
        }

        /// <summary>
        /// 保存知识体系
        /// </summary>
        protected void Save_Click(object sender, DirectEventArgs e)
        {
            if (Convert.ToBoolean(hidIsAdd.Value)) // 新增
            {
                InsertKnowledge();
            }
            else // 修改
            {
                UpdateKnowledge();
            }
        }

        private void InsertKnowledge()
        {
            try
            {
                T_KNOWLEDGE data = new T_KNOWLEDGE();
                data.ID = Guid.NewGuid();
                if (!string.IsNullOrEmpty(txtKnowParent.Text))
                {
                    data.PARENT_ID = Guid.Parse(hidParentID.Value.ToString());
                }
                data.KNOWLEDGE_NAME = txtKnowName.Text.Trim();
                data.KNOWLEDGE_DESC = txtKnowDescript.Text.Trim();
                data.SEQUENCE = txtKnowSequence.Text.Trim();
                data.CREATE_USER_ID = LOGIN_USER.ID;
                data.CREATE_DATE = DateTime.Now;
                var result = KnowledgeManage.AddKnowledge(data);

                if (result.Success)
                {
                    hidIsAdd.Value = false;
                    MessageBoxExt.ShowPrompt("添加知识体系成功!");
                    this.GetKnowledge();
                }
                else
                {
                    MessageBoxExt.ShowError("添加知识体系失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void UpdateKnowledge()
        {
            try
            {
                T_KNOWLEDGE data = new T_KNOWLEDGE();
                data.ID = Guid.Parse(hidID.Value.ToString());

                if (!string.IsNullOrEmpty(txtKnowParent.Text))
                {
                    data.PARENT_ID = Guid.Parse(hidParentID.Value.ToString());
                }
                data.KNOWLEDGE_NAME = txtKnowName.Text.Trim();
                data.KNOWLEDGE_DESC = txtKnowDescript.Text.Trim();
                data.SEQUENCE = txtKnowSequence.Text.Trim();
                data.CREATE_USER_ID = LOGIN_USER.ID;
                data.CREATE_DATE = DateTime.Now;
                var result = KnowledgeManage.UpdateKnowledge(data);

                if (result.Success)
                {
                    MessageBoxExt.ShowPrompt("修改知识体系成功!");
                    this.GetKnowledge();
                }
                else
                {
                    MessageBoxExt.ShowError("修改知识体系失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 新增知识体系
        /// </summary>
        protected void Add_Click(object sender, DirectEventArgs e)
        {
            txtKnowParent.Clear();
            txtKnowName.Clear();
            txtKnowDescript.Clear();
            txtKnowSequence.Clear();
            btnDelete.Hidden = true;
            hidIsAdd.Value = true;
        }

        /// <summary>
        /// 删除知识体系
        /// </summary>
        protected void Delete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (KnowledgeManage.QueryDeletable(hidID.Value.ToString()))
                {
                    var result = KnowledgeManage.DeleteKnowledge(hidID.Value.ToString());

                    if (result.Success)
                    {
                        this.Add_Click(null, null);
                        MessageBoxExt.ShowPrompt("删除知识体系成功!");
                        this.GetKnowledge();
                    }
                    else
                    {
                        MessageBoxExt.ShowError("删除知识体系失败!");
                    }
                }
                else
                {
                    MessageBoxExt.ShowError("该知识体系下有子知识体系,不能删除!");
                }

            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
    }
}