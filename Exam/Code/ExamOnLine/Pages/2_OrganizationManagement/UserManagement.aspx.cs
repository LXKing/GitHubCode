using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Ext.Extension.Message;
using Ext.Extension.Windows;
using Ext.Net;

using MDL;
using BLL;
using System.Data;
using COMMON;
using Ext.Net.Utilities;
using Ext.Extension.TreePanelEx;

namespace ExamOnLine.Pages.OrganizationManagement
{
    public partial class UserManagement : TabBasePage
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
            //绑定角色数据
            InitRoleData();
        }

        /// <summary>
        /// 初始化角色下拉框数据
        /// </summary>
        public void InitRoleData()
        {

            try
            {
                var data = new BLL_RoleManage().QueryAllRole().OrderBy(x => x.SEQUENCE).ToList();

                cmbRole.DisplayField = "ROLE_NAME";
                cmbRole.ValueField = "ID";
                cmbRole.GetStore().DataSource = data;
                cmbRole.DataBind();
            }
            catch (Exception ex)
            {
                base.WriteException("获取角色数据异常", ex);
                MessageBoxExt.ShowError("获取角色数据异常!");
            }
        }
        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<V_USER_INFO> data = new List<V_USER_INFO>();
            int total = 0;
            var result = QueryUserByConditions(extraParams);
            total = result.TotalCount;
            data = result.ToList();
            return new { data, total };
        }
        #region 事件
        /// <summary>
        /// 选择部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtDepartment_IndicatorIconClick(object sender, DirectEventArgs e)
        {
            var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_DEPARTMENT>("DEPARTMENT_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
            treePanel.Tree.SetdataSource(data);
            hidden_TreeType.SetValue(TreeType.Department.ToInt());
            treePanel.SetWindowTitle("部门选择");
            treePanel.Show();
        }

        protected void setDeapartment(TreePanelNodeClickEventArgs e)
        {
            txtDepartment.Text = e.NodeDbClick.Text;
            hidden_Department.Text = e.NodeDbClick.NodeID;
        }
        #endregion

        #region 职位
        protected void txtPosition_IndicatorIconClick(object sender, DirectEventArgs e)
        {
            var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_POSITION>("POSITION_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
            treePanel.Tree.SetdataSource(data);
            hidden_TreeType.SetValue(TreeType.Position.ToInt());
            treePanel.SetWindowTitle("职位选择");
            treePanel.Show();
        }
        protected void setPosition(TreePanelNodeClickEventArgs e)
        {
            txtPosition.Text = e.NodeDbClick.Text;
            hidden_Position.Text = e.NodeDbClick.NodeID;
        }
        #endregion
        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommandColumn_View_Command(object sender, DirectEventArgs e)
        {
            var user_id=JSON.Deserialize(e.ExtraParams["id"]);
            var hash = AppConst.ManagementOperateType + "=" + AppConst.ManagementOperateTypeView + "&user_id=" + user_id + "&return=UserManagement.aspx";
            X.Redirect("AddUser.aspx?" + hash);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommandColumn_Edit_Command(object sender, DirectEventArgs e)
        {
            var user_id = JSON.Deserialize(e.ExtraParams["id"]);
            var hash = AppConst.ManagementOperateType + "=" + AppConst.ManagementOperateTypeUpdate + "&user_id=" + user_id + "&return=UserManagement.aspx";
            X.Redirect("AddUser.aspx?" + hash);
        }

        protected void treePanel_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            try
            {
                var type = Convert.ToInt32(hidden_TreeType.Value);
                switch (type)
                {
                    case (int)TreeType.Department:
                        setDeapartment(e);
                        break;
                    case (int)TreeType.Position:
                        setPosition(e);
                        break;
                }

                if (hidden_TreeType.Value.ToString() == TreeType.Position.ToString())
                {
                    setPosition(e);
                }
            }
            catch (Exception ex)
            {

                base.WriteException("树节点选择变化发生异常", ex);
            }
        }

        private PagedList<V_USER_INFO> QueryUserByConditions(Dictionary<string, object> extraParams)
        {
            PagedList<V_USER_INFO> data = new PagedList<V_USER_INFO>();

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);//;prms.Page;
                int pageSize = Convert.ToInt32(extraParams["limit"]); //prms.Limit;

                Func<V_USER_INFO, bool> rolePredicate = (x) => true;
                Func<V_USER_INFO, bool> departmentPredicate = (x) => true;
                Func<V_USER_INFO, bool> positionPredicate = (x) => true;
                Func<V_USER_INFO, bool> sexPredicate = (x) => true;
                Func<V_USER_INFO, bool> namePredicate = (x) => true;

                #region 角色
                if (cmbRole.SelectedItems.Count > 0)
                {
                    var roleList = cmbRole.SelectedItems.ToList();
                    rolePredicate = (x) =>
                    {
                        bool equal = false;
                        roleList.ForEach(y =>
                        {
                            equal = equal || ((Guid.Parse(y.Value) == x.ROLE_ID));
                        });
                        return equal;
                    };
                }
                #endregion

                #region 部门
                if (!hidden_Department.Text.IsNullOrEmpty())
                {
                    departmentPredicate = (x) =>
                    {
                        Guid id;
                        bool success = Guid.TryParse(hidden_Department.Text, out id);
                        if (success)
                            return x.DEPARTMENT_ID == id;
                        else
                            return false;
                    };
                }
                #endregion

                #region 职位
                if (!hidden_Position.Text.IsNullOrEmpty())
                {
                    positionPredicate = (x) =>
                    {
                        Guid id;
                        bool success = Guid.TryParse(hidden_Position.Text.ToString(), out id);
                        if (success)
                            return x.POSITION_ID == id;
                        else
                            return false;
                    };
                }
                #endregion

                #region 性别
                if (cmbSex.SelectedItems.Count > 0)
                {
                    sexPredicate = (x) =>
                    {
                        return x.SEX == cmbSex.SelectedItems[0].Value.ToString();
                    };
                }
                #endregion

                #region 名称(登录名、用户名)
                if (txtName.Text.IsNotEmpty())
                {
                    namePredicate = (x) =>
                    {
                        return x.LOGIN_NAME.Contains(txtName.Text) || x.USER_NAME.Contains(txtName.Text);
                    };
                }

                #endregion
                var result = new BLL.OrganizationManagement.BLL_UserManagement().QueryUserInforByPaged(pageSize, pageIndex,
                    rolePredicate,
                    departmentPredicate,
                    positionPredicate,
                    sexPredicate,
                    namePredicate
                    );
                result.ForEach(x =>
                {
                    x.SEX = x.SEX == "0" ? "女" : (x.SEX == "1" ? "男" : "暂无");
                });
                data = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        protected void CellEditing_Edit(object sender,DirectEventArgs e)
        {

        }

        protected void CommandColumn_StatusChage_Command(object sender, DirectEventArgs e)
        {
            try
            {
                var id = JSON.Deserialize<Guid>(e.ExtraParams["id"]);
                var oldStatus = JSON.Deserialize<int>(e.ExtraParams["status"]);
                var newStatus = JSON.Deserialize<int>(e.ExtraParams["value"]);
                if (oldStatus == newStatus)
                {
                    if(oldStatus==0)
                        MessageBoxExt.ShowPrompt("当前用户已经禁用，不用再次禁用!");
                    if(oldStatus==1)
                        MessageBoxExt.ShowPrompt("当前用户已经启用，不用再次启用!");
                    return;
                }
                if (id == base.LOGIN_USER.ID && newStatus ==0)
                {
                    MessageBoxExt.ShowWarning("对不起，不能禁用当前登录的用户!");
                    return;
                }
                
                var bll = new BLL.OrganizationManagement.BLL_AddUser();
                var user = bll.QueryUser(id).ToJsonString().JsonToObject<T_USER>();
                user.USER_STATUS = newStatus;
                bll.UpdateUser(user);
                this.Store_GridPanel1.Reload();
            }
            catch(Exception ex)
            {
                base.WriteException("用户启用禁用异常", ex);
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected Field OnCreateFilterableField(object sender, ColumnBase column, Field defaultField)
        {
            if (column.DataIndex == "ID")
            {
                ((TextField)defaultField).Icon = Icon.Magnifier;
            }
            return defaultField;
        }
    }
}