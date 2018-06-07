using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Ext.Net.Utilities;
using MDL;
using Ext.Extension.Message;



using COMMON;
using BLL;


using System.Linq.Expressions;
using Ext.Extension.TreePanelEx;

namespace ExamOnLine.Pages.SystemManagement
{
    public partial class UserOnLineManagement : TabBasePage
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
        public void InitData()
        {
            //绑定角色数据
            InitRoleData();
        }
        public override void InitPermission()
        {
            //base.InitPermission();

        }

        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<V_USER_INFO> data = new List<V_USER_INFO>();
            int total = 0;
            try
            {
                var loginUserDic = (Dictionary<Guid, LoginUser>)Application[AppConst.Application_LoginUserDic];
                var result = QueryUserOnlineByConditions(extraParams);
                data = result.ToList();
                total = result.TotalCount;
            }
            catch (Exception ex)
            {
                base.WriteException("查询在线用户数据异常", ex);
                MessageBoxExt.ShowError(ex.Message, string.Empty);
            }
            return new { data, total };
        }

        /// <summary>
        /// 强制下线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommandColumn_ForcedOffline_Command(object sender, DirectEventArgs e)
        {
            try
            {
                var userIDStr = JSON.Deserialize<string>(e.ExtraParams["ID"]);
                Guid userID;
                if (Guid.TryParse(userIDStr, out userID))
                {
                    var success = RemoveLoginUser(userID);
                    if (!success)
                    {
                        MessageBoxExt.ShowError("强制下线失败，用户可能已经下线!");
                    }
                }
                else
                    MessageBoxExt.ShowError("错误的用户ID,无法强制用户下线!");
            }
            catch (Exception ex)
            {
                base.WriteException("强制用户下线异常", ex);
                MessageBoxExt.ShowError(ex.Message);
            }
        }


        #region 部门
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

                base.WriteException("树节点选择变化发生异常",ex);
            }
        }

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
                base.WriteException("获取角色数据异常",ex);
                MessageBoxExt.ShowError("获取角色数据异常!");
            }
        }

        protected void btnSearch_Click(object sender, DirectEventArgs e)
        {
            //js替代
        }

        private PagedList<V_USER_INFO> QueryUserOnlineByConditions(Dictionary<string, object> extraParams)
        {
            PagedList<V_USER_INFO> data = new PagedList<V_USER_INFO>();

            try
            {
                var loginUserDic = (Dictionary<Guid, LoginUser>)Application[AppConst.Application_LoginUserDic];
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
                if(!hidden_Department.Text.IsNullOrEmpty())
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
                if(cmbSex.SelectedItems.Count>0)
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
                var result = new BLL.SystemManagement.BLL_UserOnline().QueryUserOnlineByPaged(loginUserDic.Keys, pageSize, pageIndex,
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
    }
}
namespace ExamOnLine
{
    public enum TreeType
    {
        /// <summary>
        /// 部门
        /// </summary>
        Department,
        /// <summary>
        /// 职位
        /// </summary>
        Position
    }
}
