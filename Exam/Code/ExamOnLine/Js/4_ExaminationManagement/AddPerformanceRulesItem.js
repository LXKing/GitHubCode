/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />
var optype;
var returnUrl;
var returnUrl1;
var returnUrl2;
var id,id1;
/*
初始化文
*/
$(document).ready(function () {

});

window.onload = function () {
    Init();
}

function Init() {
    optype = getQueryString("optype");
    returnUrl = getQueryString("return");
    returnUrl1 = getQueryString("return1");
    returnUrl2 = getQueryString("return2");
    id = getQueryString("id");
    if (optype == "a") {
        App.Panel1.setTitle("▏添加考试成绩规则段");
    }
    if (optype == "u") {
        App.Panel1.setTitle("▏修改考试成绩规则段");
        id1 = getQueryString("id1");
    }
    if (optype == "v") {
        App.Panel1.setTitle("▏查看考试成绩规则段");
        id1 = getQueryString("id1");
    }
}
/*
保存前
*/
function btnSave_Before() {
    var v1 = App.txtSequence.validate();
    if (App.htmlDescription.getValue().length > 200) {
        MessageBoxShowWarning(undefined, "备注长度不超过200个字符!");
        v1 = v1 && false;
    }
    if (v1) {
        showLoading("Viewport1", "正在保存数据,请稍等......");
    }
    return v1;
}
/*
保存ajax完成后
*/
function btnSave_Complete() {
    hideLoading("Viewport1");
}
/*
返回
*/
function btnReturn_Click() {
    if (returnUrl != undefined) {
        this.frameElement.src = getCurrentUrl() + returnUrl2+"?return="+returnUrl+"&return1="+returnUrl1+"&id="+id;
    }

}
/*
判断起始分数
*/
function numBeginScore_Change(thisCmp, newValue, oldValue) {
    if (newValue > App.numEndScore.value) {
        MessageBoxShowWarning(undefined,
            "起始分数不能大于结束分数!",
            function () {
                App.numBeginScore.setValue(oldValue);
            }
            );
        
    }
}
/*
判断结束分数
*/
function numEndScore_Change(thisCmp, newValue, oldValue) {
    if (newValue < App.numBeginScore.value) {
        MessageBoxShowWarning(undefined,
            "结束分数不能小于起始分数!",
            function () {
            App.numEndScore.setValue(oldValue);
        });
        
    }
}