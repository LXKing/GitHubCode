/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />


/*
初始化文
*/
$(document).ready(function () {

});

window.onload = function () {
    
}
/*
保存前检查
*/
function btnSave_Before()
{
    var v1 = App.txtOldPwd.validate();
    v1 = v1 && App.txtNewPwd.validate();
    v1 = v1 && App.txtNewPwdRe.validate();
    if(App.txtNewPwd.getValue() != App.txtNewPwdRe.getValue())
    {
        MessageBoxShowWarning(undefined, "两次新密码输入不一致!", function () {
            App.txtNewPwd.setActiveError("密码不一致!");
            App.txtNewPwd.doComponentLayout();

            App.txtNewPwdRe.setActiveError("密码不一致!");
            App.txtNewPwdRe.doComponentLayout();
        });
        v1 = v1 && false;
    }
    if (v1)
    {
        showLoading("Viewport1","正在保存数据......");
    }
    return v1;
}
/*
保存ajax完成
*/
function btnSave_Complete()
{
    hideLoading("Viewport1");
}