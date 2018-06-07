using BLL.OrganizationManagement;
using COMMON;
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

namespace ExamOnLine.Pages.OrganizationManagement
{
    public partial class AddUser : TabBasePage
    {
        private BLL_AddUser BLLAddUser = new BLL_AddUser();

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                this.sltDegree.GetStore().DataSource = BLLAddUser.QueryDegree();

                string optype = Request.QueryString["optype"];
                switch (optype)
                {
                    case "a": // 添加用户
                        hidIsAdd.Value = true;
                        btnSave.Visible = true;
                        sltStatus.Select("1");
                        sltGender.Select("1");
                        break;

                    case "v": // 查看用户
                        btnSave.Visible = false;
                        txtLoginName.ReadOnly = true;
                        InitUserInfo(Request.QueryString["user_id"]);
                        break;

                    case "u": // 修改用户
                        hidIsAdd.Value = false;
                        btnSave.Visible = true;
                        txtLoginName.ReadOnly = true;
                        txtPwd.AllowBlank = true;
                        InitUserInfo(Request.QueryString["user_id"]);
                        break;

                    default:
                        break;
                }
            }
        }

        private void InitUserInfo(string userID)
        {
            hidUserID.Value = userID;
            var user = new V_USER_INFO();
            user = BLLAddUser.QueryUser(Guid.Parse(userID));
            txtLoginName.Text = user.LOGIN_NAME;
            //txtPwd.Text = user.USER_PWD;
            sltStatus.Select(user.USER_STATUS.ToString());
            txtUserName.Text = user.USER_NAME;
            sltGender.Select(user.SEX);
            hidDeptID.Value = user.DEPARTMENT_ID;
            txtDeptName.Text = user.DEPARTMENT_NAME;
            hidPosID.Value = user.POSITION_ID;
            txtPosName.Text = user.POSITION_NAME;
            rdoGpRole.Items.ForEach(a =>
            {
                var b = a as Radio;
                if (b.InputValue == user.ROLE_ID.ToString().ToUpper())
                {
                    (rdoGpRole.CheckedItems[0] as Radio).Checked = false;
                    b.Checked = true;                     
                }
            });
            txtIDCard.Text = user.IDENTITY_CARD_CODE;
            txtTel.Text = user.USER_TEL;
            txtMobile.Text = user.USER_MOBILE;
            txtContact.Text = user.USER_CONTACT;
            //sltDegree.Select(user.DEGREE_ID);
            var lstDegree = this.sltDegree.GetStore().DataSource as List<T_DEGREE>;

            lstDegree.ForEach(a => 
            {
                if (a.ID == user.DEGREE_ID)
                {
                    sltDegree.SelectedItems.Add(new Ext.Net.ListItem(user.DEGREE_NAME, user.DEGREE_ID));
                }
            });
            txtAddress.Text = user.ADDRESS;
            txtPostCode.Text = user.POSTCODE;
            imgPreView.ImageUrl = user.SERVER_FULL_PATH;
        }

        /// <summary>
        /// 部门列表弹出窗口
        /// </summary>
        protected void GetDept_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_DEPARTMENT>("DEPARTMENT_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                trpnlExt.Tree.SetdataSource(data);
                trpnlExt.SetWindowTitle("部门选择");
                trpnlExt.Show();
                hidIsDepartment.Value = true;
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 岗位列表弹出窗口
        /// </summary>
        protected void GetPos_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var data = new BLL.TreeData.BLL_QueryTreeData().QueryTreeDataList<T_POSITION>("POSITION_NAME", "ID", "PARENT_ID").Select(x => { return new NodeEx() { NodeID = x.ID, Text = x.Text, ParentNodeID = x.ParentID }; }).ToList();
                trpnlExt.Tree.SetdataSource(data);
                trpnlExt.SetWindowTitle("岗位选择");
                trpnlExt.Show();
                hidIsDepartment.Value = false;
            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 选择部门或岗位
        /// </summary>
        protected void trpnlExt_NodeClick(object sender, TreePanelNodeClickEventArgs e)
        {
            if (Convert.ToBoolean(hidIsDepartment.Value))
            {
                txtDeptName.Text   = e.NodeDbClick.Text;
                hidDeptID.Value     = e.NodeDbClick.NodeID;
            }
            else
            {
                txtPosName.Text = e.NodeDbClick.Text;
                hidPosID.Value = e.NodeDbClick.NodeID;
            }
        }

        /// <summary>
        /// 检测用户
        /// </summary>
        protected void CheckLoginName_Click(object sender, DirectEventArgs e)
        {
            if (BLLAddUser.CheckLoginName(txtLoginName.Text.Trim()))
            {
                MessageBoxExt.ShowPrompt("用户名可以使用!");
            }
            else
            {
                MessageBoxExt.ShowError("用户名已被使用,请更换用户名!");
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        protected void btnSave_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(hidIsAdd.Value))
                {
                    if (!BLLAddUser.CheckLoginName(txtLoginName.Text.Trim()))
                    {
                        MessageBoxExt.ShowError("用户名已被使用,请更换用户名!");
                        return;
                    }
                    DoAddUser();
                }
                else
                {
                    DoUpdateUser();
                }

            }
            catch (Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }

        private void DoAddUser()
        {
            #region 添加

            #region 用户基本信息
            var user = new T_USER();
            user.ID = Guid.NewGuid();
            user.LOGIN_NAME = txtLoginName.Text.Trim();
            if (!string.IsNullOrEmpty(txtPwd.Text.Trim()))
            {
                user.USER_PWD = Cipher.MD5Encrypt32(txtPwd.Text.Trim());
            }
            user.USER_STATUS = Convert.ToInt32(sltStatus.SelectedItem.Value);
            user.USER_NAME = txtUserName.Text.Trim();
            user.SEX = sltGender.SelectedItem.Value;
            Guid depID;
            if (Guid.TryParse(hidDeptID.Value.ToString(), out depID))
            {
                user.DEPARTMENT_ID = depID;
            }
            Guid posID;
            if (Guid.TryParse(hidPosID.Value.ToString(), out posID))
            {
                user.POSITION_ID = posID;
            }
            Guid roleID;
            if (rdoGpRole.CheckedItems.Count > 0)
            {
                if (Guid.TryParse(rdoGpRole.CheckedItems[0].InputValue, out roleID))
                {
                    user.ROLE_ID = roleID;
                }
            }
            
            user.IDENTITY_CARD_CODE = txtIDCard.Text.Trim();
            user.USER_TEL = txtTel.Text.Trim();
            user.USER_MOBILE = txtMobile.Text.Trim();
            user.USER_CONTACT = txtContact.Text.Trim();
            Guid degID;
            if (Guid.TryParse(sltDegree.SelectedItem.Value, out degID))
            {
                user.DEGREE_ID = degID;
            }
            user.ADDRESS = txtAddress.Text.Trim();
            user.POSTCODE = txtPostCode.Text.Trim();
            user.CREATE_USER_ID = base.LOGIN_USER.ID;
            user.CREATE_DATE = DateTime.Now;
            #endregion

            #region 保存头像
            if (Request.Files.Count > 0)
            {
                HttpPostedFile file = Request.Files[0];//读取文件对象

                var fileInfo = new T_SYS_FILE_INFO();
                fileInfo.ID = Guid.NewGuid();
                fileInfo.FILE_TYPE = "0";
                fileInfo.FILE_EXT_NAME = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1);
                fileInfo.FILE_NAME = user.ID.ToString() + "." + fileInfo.FILE_EXT_NAME;
                fileInfo.FILE_PATH_ID = Guid.Parse(@"69D96071-46B5-1879-E88F-156288FA42AF
");
                fileInfo.FILE_SIZE = file.ContentLength;
                fileInfo.UPLOAD_USER_ID = base.LOGIN_USER.ID;
                fileInfo.CREATE_DATE = DateTime.Now;
                fileInfo.UPDATE_DATETIME = DateTime.Now;
                fileInfo.UPDATE_USER_ID = base.LOGIN_USER.ID;

                string dir = BLLAddUser.QueryFilePath();
                string filename = dir + fileInfo.FILE_NAME;

                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }

                file.SaveAs(filename);

                if (BLLAddUser.AddSysFileInfo(fileInfo).Success)
                {
                    user.USER_PHOTO = fileInfo.ID;
                }
            }
            #endregion

            if (BLLAddUser.AddUser(user).Success)
            {
                MessageBoxExt.ShowPrompt("添加用户成功!");
            }
            else
            {
                MessageBoxExt.ShowError("添加用户失败!");
            }
            #endregion
        }

        private void DoUpdateUser()
        {
            #region 修改
            #region 用户基本信息
            var user = new T_USER();
            Guid id;
            if (Guid.TryParse(hidUserID.Value.ToString(), out id))
            {
                user.ID = id;
            }
            user.LOGIN_NAME = txtLoginName.Text.Trim();
            if (!string.IsNullOrEmpty(txtPwd.Text.Trim()))
            {
                user.USER_PWD = Cipher.MD5Encrypt32(txtPwd.Text.Trim());
            }            
            user.USER_STATUS = Convert.ToInt32(sltStatus.SelectedItem.Value);
            user.USER_NAME = txtUserName.Text.Trim();
            user.SEX = sltGender.SelectedItem.Value;
            Guid depID;
            if (Guid.TryParse(hidDeptID.Value.ToString(), out depID))
            {
                user.DEPARTMENT_ID = depID;
            }
            Guid posID;
            if (Guid.TryParse(hidPosID.Value.ToString(), out posID))
            {
                user.POSITION_ID = posID;
            }
            Guid roleID;
            if (rdoGpRole.CheckedItems.Count > 0)
            {
                if (Guid.TryParse(rdoGpRole.CheckedItems[0].InputValue, out roleID))
                {
                    user.ROLE_ID = roleID;
                }
            }
            user.IDENTITY_CARD_CODE = txtIDCard.Text.Trim();
            user.USER_TEL = txtTel.Text.Trim();
            user.USER_MOBILE = txtMobile.Text.Trim();
            user.USER_CONTACT = txtContact.Text.Trim();
            Guid degID;
            if (Guid.TryParse(sltDegree.SelectedItem.Value, out degID))
            {
                user.DEGREE_ID = degID;
            }
            user.ADDRESS = txtAddress.Text.Trim();
            user.POSTCODE = txtPostCode.Text.Trim();
            #endregion

            #region 保存头像
            if (Request.Files.Count > 0)
            {
                HttpPostedFile file = Request.Files[0];//读取文件对象

                var fileInfo = new T_SYS_FILE_INFO();
                fileInfo.FILE_TYPE = "0";
                fileInfo.FILE_EXT_NAME = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1);
                fileInfo.FILE_NAME = user.ID.ToString() + "." + fileInfo.FILE_EXT_NAME;
                fileInfo.FILE_SIZE = file.ContentLength;
                fileInfo.UPLOAD_USER_ID = user.ID;
                fileInfo.UPDATE_DATETIME = DateTime.Now;
                fileInfo.UPDATE_USER_ID = base.LOGIN_USER.ID;

                string dir = BLLAddUser.QueryFilePath();
                string filename = dir + "/" + fileInfo.FILE_NAME;

                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }

                file.SaveAs(filename);
                BLLAddUser.UpdateSysFileInfo(fileInfo);
            }
            #endregion

            if (BLLAddUser.UpdateUser(user).Success)
            {
                MessageBoxExt.ShowPrompt("修改用户成功!");
            }
            else
            {
                MessageBoxExt.ShowError("修改用户失败!");
            }
            #endregion
        }
    }
}