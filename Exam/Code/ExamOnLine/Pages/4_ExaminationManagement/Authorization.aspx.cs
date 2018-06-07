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
    public partial class Authorization : TabBasePage
    {
        BLL_Authorization BLLAuthorization = new BLL_Authorization();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region 部门
        [DirectMethod]
        public object BindDataDept(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<V_AUTHOR_DEPARTMENT> data = new List<V_AUTHOR_DEPARTMENT>();
            int total = 0;
            var result = QueryDeptByConditions(extraParams);
            total = result.TotalCount;
            data = result.ToList();
            return new { data, total };
        }

        private PagedList<V_AUTHOR_DEPARTMENT> QueryDeptByConditions(Dictionary<string, object> extraParams)
        {
            PagedList<V_AUTHOR_DEPARTMENT> data = new PagedList<V_AUTHOR_DEPARTMENT>();

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);
                int pageSize = Convert.ToInt32(extraParams["limit"]);

                var result = BLLAuthorization.QueryAuthorDeptByPaged(Request.QueryString["id"], pageSize, pageIndex);
                data = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        protected void CommandColumn_DeleteDept_Command(object sender, DirectEventArgs e)
        {
            try
            {
                var id = JSON.Deserialize(e.ExtraParams["id"]).ToString();
                List<Guid> ids = new List<Guid>();
                ids.Add(Guid.Parse(id));
                var result = BLLAuthorization.DeleteAuthors(ids);

                if (result.Success)
                {
                    this.gpnlDepartment.GetStore().Reload();
                    MessageBoxExt.ShowPrompt("删除部门授权成功!");
                }
                else
                {
                    MessageBoxExt.ShowError("删除部门授权失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void btnDeleteManyDept_Click(object sender, DirectEventArgs e)
        {
            if (gpnlDepartment.SelectionModel.ToList().Count > 0)
            {
                try
                {
                    var list = gpnlDepartment.GetSelectionModel() as RowSelectionModel;
                    List<Guid> idList = new List<Guid>();
                    list.SelectedRows.ToList().ForEach(x =>
                    {
                        var id = Guid.Parse(x.RecordID);
                        idList.Add(id);
                    });
                    var result = BLLAuthorization.DeleteAuthors(idList);

                    if (result.Success)
                    {
                        MessageBoxExt.ShowPrompt("删除部门授权成功!");
                        this.gpnlDepartment.GetStore().Reload();
                    }
                    else
                    {
                        MessageBoxExt.ShowWarning("删除部门授权失败!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxExt.ShowError(ex.Message);
                }
            }
        }

        protected void GetDepartment_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_DEPARTMENT>("DEPARTMENT_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                tplDepartment.Tree.SetdataSource(data);
                hidIsDept.Value = true;
                tplDepartment.SetWindowTitle("选择要授权的部门");
                tplDepartment.Show();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void tplDepartment_SubmittedNode(object sender, Ext.Extension.TreePanelEx.TreePanelSubmittedNodeEventArgs e)
        {
            if (Convert.ToBoolean(hidIsDept.Value))
            {
                #region
                try
                {
                    List<T_AUTHORIZED_EXAM> data = new List<T_AUTHORIZED_EXAM>();

                    foreach (var item in e.CheckedNodes)
                    {
                        var temp = new T_AUTHORIZED_EXAM();
                        temp.ID = Guid.NewGuid();
                        temp.EXAM_PLAN_ID = Guid.Parse(Request.QueryString["id"]);
                        temp.AUTHORIZED_LEVEL = "2";
                        temp.AUTHORIZED_LEVEL_ID = Guid.Parse(item.NodeID);
                        temp.CREATE_USER_ID = Guid.NewGuid();
                        temp.CREATE_DATE = DateTime.Now;
                        data.Add(temp);
                    }

                    var result = BLLAuthorization.AddAuthors(data);

                    if (result.Success)
                    {
                        MessageBoxExt.ShowPrompt("添加部门授权成功!");
                        this.gpnlDepartment.GetStore().Reload();
                    }
                    else
                    {
                        MessageBoxExt.ShowWarning("添加部门授权失败!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxExt.ShowError(ex.Message);
                }
                #endregion
            }
            else
            {
                #region
                try
                {
                    List<T_AUTHORIZED_EXAM> data = new List<T_AUTHORIZED_EXAM>();

                    foreach (var item in e.CheckedNodes)
                    {
                        var temp = new T_AUTHORIZED_EXAM();
                        temp.ID = Guid.NewGuid();
                        temp.EXAM_PLAN_ID = Guid.Parse(Request.QueryString["id"]);
                        temp.AUTHORIZED_LEVEL = "1";
                        temp.AUTHORIZED_LEVEL_ID = Guid.Parse(item.NodeID);
                        temp.CREATE_USER_ID = Guid.NewGuid();
                        temp.CREATE_DATE = DateTime.Now;
                        data.Add(temp);
                    }

                    var result = BLLAuthorization.AddAuthors(data);

                    if (result.Success)
                    {
                        MessageBoxExt.ShowPrompt("添加岗位授权成功!");
                        this.gpnlPosition.GetStore().Reload();
                    }
                    else
                    {
                        MessageBoxExt.ShowWarning("添加岗位授权失败!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxExt.ShowError(ex.Message);
                }
                #endregion
            }

        }
        #endregion

        #region 岗位
        [DirectMethod]
        public object BindDataPos(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<V_AUTHOR_POSITION> data = new List<V_AUTHOR_POSITION>();
            int total = 0;
            var result = QueryPosByConditions(extraParams);
            total = result.TotalCount;
            data = result.ToList();
            return new { data, total };
        }

        private PagedList<V_AUTHOR_POSITION> QueryPosByConditions(Dictionary<string, object> extraParams)
        {
            PagedList<V_AUTHOR_POSITION> data = new PagedList<V_AUTHOR_POSITION>();

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);
                int pageSize = Convert.ToInt32(extraParams["limit"]);

                var result = BLLAuthorization.QueryAuthorPosByPaged(Request.QueryString["id"], pageSize, pageIndex);
                data = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        protected void CommandColumn_DeletePos_Command(object sender, DirectEventArgs e)
        {
            try
            {
                var id = JSON.Deserialize(e.ExtraParams["id"]).ToString();
                List<Guid> ids = new List<Guid>();
                ids.Add(Guid.Parse(id));
                var result = BLLAuthorization.DeleteAuthors(ids);

                if (result.Success)
                {
                    this.gpnlPosition.GetStore().Reload();
                    MessageBoxExt.ShowPrompt("删除岗位授权成功!");
                }
                else
                {
                    MessageBoxExt.ShowError("删除岗位授权失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void btnDeleteManyPos_Click(object sender, DirectEventArgs e)
        {
            if (gpnlPosition.SelectionModel.ToList().Count > 0)
            {
                try
                {
                    var list = gpnlPosition.GetSelectionModel() as RowSelectionModel;
                    List<Guid> idList = new List<Guid>();
                    list.SelectedRows.ToList().ForEach(x =>
                    {
                        var id = Guid.Parse(x.RecordID);
                        idList.Add(id);
                    });
                    var result = BLLAuthorization.DeleteAuthors(idList);

                    if (result.Success)
                    {
                        MessageBoxExt.ShowPrompt("删除岗位授权成功!");
                        this.gpnlPosition.GetStore().Reload();
                    }
                    else
                    {
                        MessageBoxExt.ShowWarning("删除岗位授权失败!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxExt.ShowError(ex.Message);
                }
            }
        }

        protected void GetPosition_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_POSITION>("POSITION_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                tplDepartment.Tree.SetdataSource(data);
                hidIsDept.Value = false;
                tplDepartment.SetWindowTitle("选择要授权的岗位");
                tplDepartment.Show();
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void tplPosition_SubmittedNode(object sender, Ext.Extension.TreePanelEx.TreePanelSubmittedNodeEventArgs e)
        {
            try
            {
                List<T_AUTHORIZED_EXAM> data = new List<T_AUTHORIZED_EXAM>();

                foreach (var item in e.CheckedNodes)
                {
                    var temp = new T_AUTHORIZED_EXAM();
                    temp.ID = Guid.NewGuid();
                    temp.EXAM_PLAN_ID = Guid.Parse(Request.QueryString["id"]);
                    temp.AUTHORIZED_LEVEL = "1";
                    temp.AUTHORIZED_LEVEL_ID = Guid.Parse(item.NodeID);
                    temp.CREATE_USER_ID = Guid.NewGuid();
                    temp.CREATE_DATE = DateTime.Now;
                    data.Add(temp);
                }

                var result = BLLAuthorization.AddAuthors(data);

                if (result.Success)
                {
                    MessageBoxExt.ShowPrompt("添加岗位授权成功!");
                    this.gpnlDepartment.GetStore().Reload();
                }
                else
                {
                    MessageBoxExt.ShowWarning("添加岗位授权失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
        #endregion

        #region 用户
        [DirectMethod]
        public object BindDataUser(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<V_AUTHOR_USER> data = new List<V_AUTHOR_USER>();
            int total = 0;
            var result = QueryUserByConditions(extraParams);
            total = result.TotalCount;
            data = result.ToList();
            return new { data, total };
        }

        private PagedList<V_AUTHOR_USER> QueryUserByConditions(Dictionary<string, object> extraParams)
        {
            PagedList<V_AUTHOR_USER> data = new PagedList<V_AUTHOR_USER>();

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);
                int pageSize = Convert.ToInt32(extraParams["limit"]);

                var result = BLLAuthorization.QueryAuthorUserByPaged(Request.QueryString["id"], pageSize, pageIndex);
                data = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        protected void CommandColumn_DeleteUser_Command(object sender, DirectEventArgs e)
        {
            try
            {
                var id = JSON.Deserialize(e.ExtraParams["id"]).ToString();
                List<Guid> ids = new List<Guid>();
                ids.Add(Guid.Parse(id));
                var result = BLLAuthorization.DeleteAuthors(ids);

                if (result.Success)
                {
                    this.gpnlUser.GetStore().Reload();
                    MessageBoxExt.ShowPrompt("删除用户授权成功!");
                }
                else
                {
                    MessageBoxExt.ShowError("删除用户授权失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void btnDeleteManyUser_Click(object sender, DirectEventArgs e)
        {
            if (gpnlUser.SelectionModel.ToList().Count > 0)
            {
                try
                {
                    var list = gpnlUser.GetSelectionModel() as RowSelectionModel;
                    List<Guid> idList = new List<Guid>();
                    list.SelectedRows.ToList().ForEach(x =>
                    {
                        var id = Guid.Parse(x.RecordID);
                        idList.Add(id);
                    });
                    var result = BLLAuthorization.DeleteAuthors(idList);

                    if (result.Success)
                    {
                        MessageBoxExt.ShowPrompt("删除用户授权成功!");
                        this.gpnlUser.GetStore().Reload();
                    }
                    else
                    {
                        MessageBoxExt.ShowWarning("删除用户授权失败!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxExt.ShowError(ex.Message);
                }
            }
        }

        #endregion

        #region 评卷人
        [DirectMethod]
        public object BindDataReviwers(string action, Dictionary<string, object> extraParams)
        {
            //System.Data.PagedList
            List<V_AUTHOR_REVIWERS> data = new List<V_AUTHOR_REVIWERS>();
            int total = 0;
            var result = QueryReviwersByConditions(extraParams);
            total = result.TotalCount;
            data = result.ToList();
            return new { data, total };
        }

        private PagedList<V_AUTHOR_REVIWERS> QueryReviwersByConditions(Dictionary<string, object> extraParams)
        {
            PagedList<V_AUTHOR_REVIWERS> data = new PagedList<V_AUTHOR_REVIWERS>();

            try
            {
                int pageIndex = Convert.ToInt32(extraParams["page"]);
                int pageSize = Convert.ToInt32(extraParams["limit"]);

                var result = BLLAuthorization.QueryAuthorReviwersByPaged(Request.QueryString["id"], pageSize, pageIndex);
                data = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        protected void CommandColumn_DeleteReviwers_Command(object sender, DirectEventArgs e)
        {
            try
            {
                var id = JSON.Deserialize(e.ExtraParams["id"]).ToString();
                List<Guid> ids = new List<Guid>();
                ids.Add(Guid.Parse(id));
                var result = BLLAuthorization.DeleteAuthorReviwers(ids);

                if (result.Success)
                {
                    this.gpnlReviwers.GetStore().Reload();
                    MessageBoxExt.ShowPrompt("删除评卷人授权成功!");
                }
                else
                {
                    MessageBoxExt.ShowError("删除评卷人授权失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        protected void btnDeleteManyReviwers_Click(object sender, DirectEventArgs e)
        {
            if (gpnlReviwers.SelectionModel.ToList().Count > 0)
            {
                try
                {
                    var list = gpnlReviwers.GetSelectionModel() as RowSelectionModel;
                    List<Guid> idList = new List<Guid>();
                    list.SelectedRows.ToList().ForEach(x =>
                    {
                        var id = Guid.Parse(x.RecordID);
                        idList.Add(id);
                    });
                    var result = BLLAuthorization.DeleteAuthorReviwers(idList);

                    if (result.Success)
                    {
                        MessageBoxExt.ShowPrompt("删除评卷人授权成功!");
                        this.gpnlReviwers.GetStore().Reload();
                    }
                    else
                    {
                        MessageBoxExt.ShowWarning("删除评卷人授权失败!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxExt.ShowError(ex.Message);
                }
            }
        }
        #endregion
    }
}