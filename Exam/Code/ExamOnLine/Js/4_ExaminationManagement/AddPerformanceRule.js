/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />
var optype;
var returnUrl;
var returnUrl1;
/*
初始化文
*/
$(document).ready(function () {

});

window.onload = function () {
    Init();
}

function Init()
{
    optype = getQueryString("optype");
    returnUrl1 = getQueryString("return1");
    returnUrl = getQueryString("return");

    if (optype == "a") {
        App.Panel1.setTitle("▏添加考试结果规则");
    }
    if (optype == "u") {
        App.Panel1.setTitle("▏修改考试结果规则");
    }
    if (optype == "v") {
        App.Panel1.setTitle("▏查看考试结果规则");
        App.txtRuleName.setReadOnly(true);
        App.txtSequence.setReadOnly(true);
        App.txtRuleDescription.setReadOnly(true);
        App.btnSave.hide();
    }
}

function btnSave_Click_Before()
{
    var v1 = App.txtRuleName.validate();
    v1 = v1 && App.txtSequence.validate();
    v1 = v1 && App.txtRuleDescription.validate();
    if(v1)
    {
        showLoading("Viewport1", "正在保存数据......");
    }
    return v1;
}

function btnSave_Click_Complete()
{
    hideLoading("Viewport1");
}

function btnReturn_Click()
{
    if (returnUrl != undefined) {
        this.frameElement.src = getCurrentUrl() + returnUrl1+"?return="+returnUrl;
    }
}