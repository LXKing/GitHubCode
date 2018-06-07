using Ext.Extension.Message;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnLine.Pages.MyAccount
{
    public partial class ModifyPwd : TabBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(X.IsAjaxRequest)
            {

            }
            else
            {

            }
        }

        protected void btnSave_Click(object sender,DirectEventArgs e)
        {
            try
            {
                if(txtNewPwd.Text!=txtNewPwdRe.Text)
                {
                    throw new Exception("两次输入密码不一致!");
                }
                var result = new BLL.MyAccount.BLL_ModifyPwd().UpdatePwd(base.LOGIN_USER.ID,txtOldPwd.Text,txtNewPwd.Text);
                if (result.Success)
                    MessageBoxExt.NotifyPrompt(this.ResourceManager1, "用户密码修改成功!", true);
                else
                    MessageBoxExt.NotifyError(this.ResourceManager1, "用户密码修改失败:" + result.Message,true);
            }
            catch(Exception ex)
            {
                MessageBoxExt.ShowError(ex.Message);
            }
        }
    }
}