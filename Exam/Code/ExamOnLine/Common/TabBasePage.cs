using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ext.Net;
using Ext.Extension.Message;
namespace ExamOnLine
{
    public class TabBasePage:BasePage
    {
        protected override void OnPreInit(EventArgs e)
        {
            //base.OnPreInit(e);
            var result = base.JudgeUserLoginState();
            switch(result)
            {
                case LoginState.Login:
                    break;
                case LoginState.NotLogin:
                    var jsContent= @"
                        alert('用户登录已失效，请重新登录!');
                        if(this.window.parent!=undefined) 
                            this.window.parent.location.reload();";
                    base.ResponseWriteJs(true,jsContent);
                    break;
                case LoginState.Off:
                    MessageBoxExt.ShowError("您已经被管理员强制下线或者已经在别的客户端登录，有问题请咨询管理人员!", "this.document.body.innerHTML='';");
                    break;
                default:
                    break;
            }
        }
        public override void InitPermission()
        {
            throw new NotImplementedException();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(X.IsAjaxRequest)
            {

            }
            else
            {

            }
        }
    }
}