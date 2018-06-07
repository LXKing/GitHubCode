using BLL.ExaminationManagement;
using Ext.Extension.Message;
using Ext.Extension.TreePanelEx;
using Ext.Net;
using MDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnLine.Pages.ExaminationManagement
{
    public partial class AddAuthorUser : TabBasePage
    {
        BLL_AddAuthorUser BLLAddAuthorUser = new BLL_AddAuthorUser();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GetDepartment_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_DEPARTMENT>("DEPARTMENT_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID,Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                tplDepartment.Tree.SetdataSource(data);
                hidIsDept.Value = true;
                tplDepartment.SetWindowTitle("部门选择");
                tplDepartment.Show();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void GetPosition_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_POSITION>("POSITION_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                tplDepartment.Tree.SetdataSource(data);
                hidIsDept.Value = false;
                tplDepartment.SetWindowTitle("岗位选择");
                tplDepartment.Show();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void tplDepartment_SubmittedNode(object sender, TreePanelSubmittedNodeEventArgs e)
        {
            if (Convert.ToBoolean(hidIsDept.Value))
            {
                #region
                string dept = string.Empty;
                string deptID = string.Empty;

                foreach (var item in e.CheckedNodes)
                {
                    dept += item.Text + ";";
                    deptID += item.NodeID + ";";
                }

                dept = dept.Remove(dept.Length - 1, 1);
                txtDept.Text = dept;
                deptID = deptID.Remove(deptID.Length - 1, 1);
                hidDeptID.Value = deptID;
                #endregion
            }
            else
            {
                #region
                string pos = string.Empty;
                string posID = string.Empty;

                foreach (var item in e.CheckedNodes)
                {
                    pos += item.Text + ";";
                    posID += item.NodeID + ";";
                }

                pos = pos.Remove(pos.Length - 1, 1);
                txtPos.Text = pos;
                posID = posID.Remove(posID.Length - 1, 1);
                hidPosID.Value = posID;
                #endregion
            }
        }

        protected void btnAuthorManyUser_Click(object sender, DirectEventArgs e)
        {
            if (GridPanel1.SelectionModel.ToList().Count > 0)
            {
                if (Request.QueryString["optype"] == "a")
                {
                    #region 授权用户
                    try
                    {
                        var list = GridPanel1.GetSelectionModel() as RowSelectionModel;

                        List<T_AUTHORIZED_EXAM> data = new List<T_AUTHORIZED_EXAM>();

                        foreach (var item in list.SelectedRows.ToList())
                        {
                            var temp = new T_AUTHORIZED_EXAM();
                            temp.ID = Guid.NewGuid();
                            temp.EXAM_PLAN_ID = Guid.Parse(Request.QueryString["id"]);
                            temp.AUTHORIZED_LEVEL = "0";
                            temp.AUTHORIZED_LEVEL_ID = Guid.Parse(item.RecordID);
                            temp.CREATE_USER_ID = base.LOGIN_USER.ID;
                            temp.CREATE_DATE = DateTime.Now;
                            data.Add(temp);
                        }

                        var result = new BLL_Authorization().AddAuthors(data);

                        if (result.Success)
                        {
                            MessageBoxExt.ShowPrompt("添加用户授权成功!");
                        }
                        else
                        {
                            MessageBoxExt.ShowWarning("添加用户授权失败!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxExt.ShowError(ex.Message);
                    }
                    #endregion
                }
                else if (Request.QueryString["optype"] == "b")
                {
                    #region 授权评卷人
                    try
                    {
                        var list = GridPanel1.GetSelectionModel() as RowSelectionModel;

                        List<T_AUTHORIZED_READ_PAPERS_USER> data = new List<T_AUTHORIZED_READ_PAPERS_USER>();

                        foreach (var item in list.SelectedRows.ToList())
                        {
                            var temp = new T_AUTHORIZED_READ_PAPERS_USER();
                            temp.ID = Guid.NewGuid();
                            temp.EXAM_PLAN_ID = Guid.Parse(Request.QueryString["id"]);
                            temp.USE_ID = Guid.Parse(item.RecordID);
                            temp.CREATE_USER_ID = base.LOGIN_USER.ID;
                            temp.CREATE_DATE = DateTime.Now;
                            data.Add(temp);
                        }

                        var result = new BLL_Authorization().AddAuthorReviwers(data);

                        if (result.Success)
                        {
                            MessageBoxExt.ShowPrompt("添加评卷人授权成功!");
                        }
                        else
                        {
                            MessageBoxExt.ShowWarning("添加评卷人授权失败!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxExt.ShowError(ex.Message);
                    }
                    #endregion
                }
            }
        }


        [DirectMethod]
        public object BindData(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<T_USER> data = new List<T_USER>();
            int total = 0;
            var result = QueryPaperByConditions(extraParams);
            total = result.TotalCount;
            data = result.ToList();
            return new { data, total };
        }

        private PagedList<T_USER> QueryPaperByConditions(Dictionary<string, object> extraParams)
        {
            PagedList<T_USER> data = new PagedList<T_USER>();

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);
                int pageSize = Convert.ToInt32(extraParams["limit"]);

                Func<T_USER, bool> cmbRolePredicate = (x) => true;
                Func<T_USER, bool> txtDeptPredicate = (x) => true;
                Func<T_USER, bool> txtPosPredicate = (x) => true;
                Func<T_USER, bool> txtNamePredicate = (x) => true;

                #region 角色
                if (cmbRole.Value.NotNull() && cmbRole.Value.ToString().Length > 0)
                {
                    var id = Guid.Parse(cmbRole.Value.ToString());
                    cmbRolePredicate = (x) =>
                    {
                        return x.ROLE_ID == id;
                    };
                }
                #endregion

                #region 部门
                if (txtDept.Text.Length > 0 && hidDeptID.Value.NotNull() && hidDeptID.Value.ToString().Length > 0)
                {
                    var deptID = hidDeptID.Value.ToString().Split(';');
                    List<Guid?> IDs = new List<Guid?>();

                    foreach (var item in deptID)
                    {
                        IDs.Add(Guid.Parse(item));
                    }

                    txtDeptPredicate = (x) =>
                    {
                        return IDs.Contains(x.DEPARTMENT_ID);
                    };
                }
                #endregion

                #region 岗位
                if (txtPos.Text.Length > 0 && hidPosID.Value.NotNull() && hidPosID.Value.ToString().Length > 0)
                {
                    var posID = hidPosID.Value.ToString().Split(';');
                    List<Guid?> IDs = new List<Guid?>();

                    foreach (var item in posID)
                    {
                        IDs.Add(Guid.Parse(item));
                    }

                    txtPosPredicate = (x) =>
                    {
                        return IDs.Contains(x.POSITION_ID);
                    };
                }
                #endregion

                #region 用户名
                if (txtName.Text.Length > 0)
                {
                    txtNamePredicate = (x) =>
                    {
                        return x.LOGIN_NAME == txtName.Text || x.USER_NAME == txtName.Text;
                    };
                }
                #endregion

                var result = BLLAddAuthorUser.QueryPaperByPaged(pageSize, pageIndex,
                    cmbRolePredicate, txtDeptPredicate, txtPosPredicate, txtNamePredicate);
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