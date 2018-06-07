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
    public partial class DepartmentManagement : TabBasePage
    {
        private BLL_DepartmentManage DepartManage = new BLL_DepartmentManage();

        /// <summary>
        /// 获取左侧部门列表
        /// </summary>
        private void GetDepartMent()
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_DEPARTMENT>("DEPARTMENT_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                departMent1.SetdataSource(data);
                //departMent1.ExpandAllNode();
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
                this.GetDepartMent();
            }
        }

        /// <summary>
        /// 页面加载完成，展开所有部门
        /// </summary>
        public override void OnLoadCompleted(EventArgs e)
        {
            base.OnLoadCompleted(e);
            departMent1.ExpandAllNode();
            departMent2.Tree.ExpandAllNode();
        }

        /// <summary>
        /// 单击左侧部门列表，查看部门信息
        /// </summary>
        protected void departMent1_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            try
            {
                var data = DepartManage.QueryDepartmentByID(e.NodeDbClick.NodeID);
                if (data.ID != null)
                {
                    hidID.Value = data.ID;
                    hidParentID.Value = data.PARENT_ID;
                    txtDeptParent.Text = data.ParentName;
                    txtDeptName.Text = data.DEPARTMENT_NAME;
                    txtFulPath.Text = data.FullPath;
                    txtDeptDescript.Text = data.DEPARTMENT_DESC;
                    txtDeptSequence.Text = data.SEQUENCE;
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
        /// 根据部门名称，查询部门信息
        /// </summary>
        protected void btnSearchDept_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = DepartManage.QueryDepartmentByName(txtSearchDept.Text.Trim());
                if (data.ID != null)
                {
                    hidID.Value = data.ID;
                    txtDeptParent.Text = data.ParentName;
                    txtDeptName.Text = data.DEPARTMENT_NAME;
                    txtFulPath.Text = data.FullPath;
                    txtDeptDescript.Text = data.DEPARTMENT_DESC;
                    txtDeptSequence.Text = data.SEQUENCE;

                    txtSearchDept.Clear();
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
        /// 部门列表弹出窗口
        /// </summary>
        protected void GetDepartment_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_DEPARTMENT>("DEPARTMENT_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                departMent2.Tree.SetdataSource(data);
                departMent2.SetWindowTitle("部门选择");
                departMent2.Show();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }            
        }

        /// <summary>
        /// 选择部门
        /// </summary>
        protected void departMent2_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            txtDeptParent.Text = e.NodeDbClick.Text;
            hidParentID.Value = e.NodeDbClick.NodeID;
        }

        /// <summary>
        /// 保存部门
        /// </summary>
        protected void Save_Click(object sender, DirectEventArgs e)
        {
            if (Convert.ToBoolean(hidIsAdd.Value)) // 新增
            {
                InsertDepartment();
            }
            else // 修改
            {
                UpdateDepartment();
            }
        }

        private void InsertDepartment()
        {
            try
            {
                T_DEPARTMENT data = new T_DEPARTMENT();
                data.ID = Guid.NewGuid();
                if (!string.IsNullOrEmpty(txtDeptParent.Text))
                {
                    data.PARENT_ID = Guid.Parse(hidParentID.Value.ToString());
                }
                data.DEPARTMENT_NAME = txtDeptName.Text.Trim();
                data.DEPARTMENT_DESC = txtDeptDescript.Text.Trim();
                data.SEQUENCE = txtDeptSequence.Text.Trim();
                data.CREATE_USER_ID = LOGIN_USER.ID;
                data.CREATE_DATE = DateTime.Now;
                var result = DepartManage.AddDepartment(data);

                if (result.Success)
                {
                    hidIsAdd.Value = false;
                    MessageBoxExt.ShowPrompt("添加部门成功!");
                    this.GetDepartMent();                    
                }
                else
                {
                    MessageBoxExt.ShowError("添加部门失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void UpdateDepartment()
        {
            try
            {
                T_DEPARTMENT data = new T_DEPARTMENT();
                data.ID = Guid.Parse(hidID.Value.ToString());

                if (!string.IsNullOrEmpty(txtDeptParent.Text))
                {
                    data.PARENT_ID = Guid.Parse(hidParentID.Value.ToString());
                }
                data.DEPARTMENT_NAME = txtDeptName.Text.Trim();
                data.DEPARTMENT_DESC = txtDeptDescript.Text.Trim();
                data.SEQUENCE = txtDeptSequence.Text.Trim();
                data.CREATE_USER_ID = LOGIN_USER.ID;
                data.CREATE_DATE = DateTime.Now;
                var result = DepartManage.UpdateDepartment(data);

                if (result.Success)
                {
                    MessageBoxExt.ShowPrompt("修改部门成功!");
                    this.GetDepartMent();
                }
                else
                {
                    MessageBoxExt.ShowError("修改部门失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        protected void Add_Click(object sender, DirectEventArgs e)
        {
            txtSearchDept.Clear();
            txtDeptParent.Clear();
            txtDeptName.Clear();
            txtFulPath.Clear();
            txtDeptDescript.Clear();
            txtDeptSequence.Clear();
            btnDelete.Hidden = true;
            hidIsAdd.Value = true;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        protected void Delete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (DepartManage.QueryDeletable(hidID.Value.ToString()))
                {
                    var result = DepartManage.DeleteDepartment(hidID.Value.ToString());

                    if (result.Success)
                    {
                        this.Add_Click(null, null);
                        MessageBoxExt.ShowPrompt("删除部门成功!");
                        this.GetDepartMent();
                    }
                    else
                    {
                        MessageBoxExt.ShowError("删除部门失败!");
                    }
                }
                else
                {
                    MessageBoxExt.ShowError("该部门下有子部门,不能删除!");
                }

            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
    }
}