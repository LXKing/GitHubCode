using BLL.MyAccount;
using BLL.OrganizationManagement;
using Ext.Extension.Message;
using Ext.Net;
using MDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnLine.Pages.MyAccount
{
    public partial class UserInfo : TabBasePage
    {
        private BLL_AddUser BLLAddUser = new BLL_AddUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                this.sltDegree.GetStore().DataSource = BLLAddUser.QueryDegree();
                this.InitUserInfo();
            }
        }

        private void InitUserInfo()
        {
            var user = new V_USER_INFO();
            user = BLLAddUser.QueryUser(base.LOGIN_USER.ID);
            txtLoginName.Text = user.LOGIN_NAME;
            txtDeptName.Text = user.DEPARTMENT_NAME;
            txtRole.Text = user.ROLE_NAME;
            txtPosName.Text = user.POSITION_NAME;
            txtUserName.Text = user.USER_NAME;
            sltGender.Select(user.SEX);
            txtIDCard.Text = user.IDENTITY_CARD_CODE;
            txtTel.Text = user.USER_TEL;
            txtMobile.Text = user.USER_MOBILE;
            txtContact.Text = user.USER_CONTACT;
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

        protected void btnSave_Click(object sender, DirectEventArgs e)
        {
            #region 用户基本信息
            var user = new T_USER();
            user.ID = base.LOGIN_USER.ID;

            user.USER_NAME = txtUserName.Text.Trim();
            user.SEX = sltGender.SelectedItem.Value;
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

            if (new BLL_UserInfo().UpdateUser(user).Success)
            {
                MessageBoxExt.ShowPrompt("修改用户成功!");
            }
            else
            {
                MessageBoxExt.ShowError("修改用户失败!");
            }
        }
    }
}