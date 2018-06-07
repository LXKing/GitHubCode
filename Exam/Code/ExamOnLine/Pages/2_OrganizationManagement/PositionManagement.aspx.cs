using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;
using MDL;

using Ext.Extension.Message;
using BLL.OrganizationManagement;
using Ext.Extension.TreePanelEx;

namespace ExamOnLine.Pages.OrganizationManagement
{
    public partial class PositionManagement : TabBasePage
    {
        private BLL_PositionManage PositManage = new BLL_PositionManage();

        /// <summary>
        /// 获取左侧岗位列表
        /// </summary>
        private void GetPosition()
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_POSITION>("POSITION_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                position1.Tree.SetdataSource(data);
                //position1.ExpandAllNode();
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
                this.GetPosition();
            }
        }

        /// <summary>
        /// 页面加载完成，展开所有岗位
        /// </summary>
        public override void OnLoadCompleted(EventArgs e)
        {
            base.OnLoadCompleted(e);
            position1.Tree.ExpandAllNode();
            position2.Tree.ExpandAllNode();
        }

        /// <summary>
        /// 单击左侧岗位列表，查看岗位信息
        /// </summary>
        protected void position1_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            try
            {
                var data = PositManage.QueryPositionByID(e.NodeDbClick.NodeID);
                if (data.ID != null)
                {
                    hidID.Value = data.ID;
                    hidParentID.Value = data.PARENT_ID;
                    txtPosParent.Text = data.ParentName;
                    txtPosName.Text = data.POSITION_NAME;
                    txtFulPath.Text = data.FullPath;
                    txtPosDescript.Text = data.POSITION_DESC;
                    txtPosSequence.Text = data.SEQUENCE;
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
        /// 根据岗位名称，查询岗位信息
        /// </summary>
        protected void btnSearchPos_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = PositManage.QueryPositionByName(txtSearchPos.Text.Trim());
                if (data.ID != null)
                {
                    hidID.Value = data.ID;
                    txtPosParent.Text = data.ParentName;
                    txtPosName.Text = data.POSITION_NAME;
                    txtFulPath.Text = data.FullPath;
                    txtPosDescript.Text = data.POSITION_DESC;
                    txtPosSequence.Text = data.SEQUENCE;

                    txtSearchPos.Clear();
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
        /// 岗位列表弹出窗口
        /// </summary>
        protected void GetPosition_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_POSITION>("POSITION_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                position2.Tree.SetdataSource(data);
                position2.SetWindowTitle("岗位选择");
                position2.Show();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 选择岗位
        /// </summary>
        protected void position2_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            txtPosParent.Text = e.NodeDbClick.Text;
            hidParentID.Value = e.NodeDbClick.NodeID;
        }

        /// <summary>
        /// 保存岗位
        /// </summary>
        protected void Save_Click(object sender, DirectEventArgs e)
        {
            if (Convert.ToBoolean(hidIsAdd.Value)) // 新增
            {
                InsertPosition();
            }
            else // 修改
            {
                UpdatePosition();
            }
        }

        private void InsertPosition()
        {
            try
            {
                T_POSITION data = new T_POSITION();
                data.ID = Guid.NewGuid();
                if (!string.IsNullOrEmpty(txtPosParent.Text))
                {
                    data.PARENT_ID = Guid.Parse(hidParentID.Value.ToString());
                }
                data.POSITION_NAME = txtPosName.Text.Trim();
                data.POSITION_DESC = txtPosDescript.Text.Trim();
                data.SEQUENCE = txtPosSequence.Text.Trim();
                data.CREATE_USER_ID = LOGIN_USER.ID;
                data.CREATE_DATE = DateTime.Now;
                var result = PositManage.AddPosition(data);

                if (result.Success)
                {
                    hidIsAdd.Value = false;
                    MessageBoxExt.ShowPrompt("添加岗位成功!");
                    this.GetPosition();
                }
                else
                {
                    MessageBoxExt.ShowError("添加岗位失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void UpdatePosition()
        {
            try
            {
                T_POSITION data = new T_POSITION();
                data.ID = Guid.Parse(hidID.Value.ToString());

                if (!string.IsNullOrEmpty(txtPosParent.Text))
                {
                    data.PARENT_ID = Guid.Parse(hidParentID.Value.ToString());
                }
                data.POSITION_NAME = txtPosName.Text.Trim();
                data.POSITION_DESC = txtPosDescript.Text.Trim();
                data.SEQUENCE = txtPosSequence.Text.Trim();
                data.CREATE_USER_ID = LOGIN_USER.ID;
                data.CREATE_DATE = DateTime.Now;
                var result = PositManage.UpdatePosition(data);

                if (result.Success)
                {
                    MessageBoxExt.ShowPrompt("修改岗位成功!");
                    this.GetPosition();
                }
                else
                {
                    MessageBoxExt.ShowError("修改岗位失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 新增岗位
        /// </summary>
        protected void Add_Click(object sender, DirectEventArgs e)
        {
            txtSearchPos.Clear();
            txtPosParent.Clear();
            txtPosName.Clear();
            txtFulPath.Clear();
            txtPosDescript.Clear();
            txtPosSequence.Clear();
            btnDelete.Hidden = true;
            hidIsAdd.Value = true;
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        protected void Delete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (PositManage.QueryDeletable(hidID.Value.ToString()))
                {
                    var result = PositManage.DeletePosition(hidID.Value.ToString());

                    if (result.Success)
                    {
                        this.Add_Click(null, null);
                        MessageBoxExt.ShowPrompt("删除岗位成功!");
                        this.GetPosition();
                    }
                    else
                    {
                        MessageBoxExt.ShowError("删除岗位失败!");
                    }
                }
                else
                {
                    MessageBoxExt.ShowError("该岗位下有子岗位,不能删除!");
                }

            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
    }
}