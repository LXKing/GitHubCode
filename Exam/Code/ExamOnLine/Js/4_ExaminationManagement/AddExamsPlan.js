/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />
var optype;
var returnUrl;
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

    if(optype=="a"){
        App.Panel1.setTitle("▏添加考试安排");
    }
    if (optype == "u") {
        App.Panel1.setTitle("▏修改考试安排");
    }
    if (optype == "v") {
        App.Panel1.setTitle("▏查看考试安排");

        ChangeState(true);
    }
}

function ChangeState(value)
{
    App.txtExamPlanName.setReadOnly(value);//App.txtExamPlanName.setDisabled(value);
    App.txtExamPlanName.allowBlank = true;

    App.numExamTime.setReadOnly(value);//App.numExamTime.setDisabled(value);
    App.numAllowJoinCounts.setReadOnly(value);//    App.numAllowJoinCounts.setDisabled(value);
    App.numPaperTotalScore.setReadOnly(value);//    App.numPaperTotalScore.setDisabled(value);
    App.numPassMinScore.setReadOnly(value);//App.numPassMinScore.setDisabled(value);

    App.dateExamBegin.setReadOnly(value);//App.dateExamBegin.setDisabled(value);
    App.dateExamBegin.allowBlank = true;
    App.dateExamEnd.setReadOnly(value);//App.dateExamEnd.setDisabled(value);
    App.dateExamEnd.allowBlank = true;

    //App.labArrangementOptions.setDisabled(value);


    App.radShowScoreNow.setReadOnly(value);//App.radShowScoreNow.setDisabled(value);
    App.radSettingPublicDate.setReadOnly(value);//App.radSettingPublicDate.setDisabled(value);
    App.dateScorePublic.setReadOnly(value);//App.dateScorePublic.setDisabled(value);

    App.txtPerformanceRole.setReadOnly(value);//App.txtPerformanceRole.setDisabled(value);
    App.clearPerformanceRules.setDisabled(value);//App.clearPerformanceRules.setDisabled(value);

    App.radSingleModem.setReadOnly(value);//App.radSingleModem.setDisabled(value);
    App.radWholeModem.setReadOnly(value);//App.radWholeModem.setDisabled(value);

    App.radAllRightRules.setReadOnly(value);//App.radAllRightRules.setDisabled(value);
    App.radAynRightRules.setReadOnly(value);//App.radAynRightRules.setDisabled(value);

    App.radAllRightRules.setReadOnly(value);//App.radAllRightRules.setDisabled(value);
    App.radAynRightRules.setReadOnly(value);//App.radAynRightRules.setDisabled(value);

    App.radAllowView.setReadOnly(value);//App.radAllowView.setDisabled(value);
    App.radForbidView.setReadOnly(value);//App.radForbidView.setDisabled(value);

    App.txtPaperName.setReadOnly(value);//App.txtPaperName.setDisabled(value);
    App.txtPaperName.allowBlank = true;

    App.htmlRemark.setReadOnly(value);//App.htmlRemark.setDisabled(value);

    App.btnSave.hidden = value;
}
function btnSave_Before()
{
    var v1 = App.txtExamPlanName.validate();
    v1 = v1 && App.dateExamBegin.validate();
    v1 = v1 && App.dateExamEnd.validate();
    if (App.radSettingPublicDate.checked)
    {
        v1 = v1 && App.dateScorePublic.validate();
    }
    v1 = v1 && App.txtPaperName.validate();
    if(App.htmlRemark.getValue().length > 200)
    {
        MessageBoxShowWarning(undefined, "备注长度不超过200个字符!");
        v1 = v1 && false;
    }
    if (v1)
    {
        showLoading("Viewport1","正在保存数据,请稍等......");
    }
    return v1;
}
function btnSave_Complete()
{
    hideLoading("Viewport1");
}
/*
返回
*/
function btnReturn_Click() {
    if (returnUrl != undefined)
    {
        this.frameElement.src = getCurrentUrl() + returnUrl;
    }
    
}

/*
判断试卷分数
*/
function numPaperTotalScore_Change(thisCmp, newValue, oldValue) {
    if (newValue < App.numPassMinScore.value) {
        MessageBoxShowWarning(undefined, "试卷分数不能小于通过分数!");
        App.numPaperTotalScore.setValue(oldValue);
    }
}
/*
判断通过分数
*/
function numPassMinScore_Change(thisCmp, newValue, oldValue)
{
    if(newValue>App.numPaperTotalScore.value)
    {
        MessageBoxShowWarning(undefined, "通过分数不能大于试卷总分数!");
        App.numPassMinScore.setValue(oldValue);
    }
}
/*
判断通过分数
*/
function activate(thisCmp)
{
    if (thisCmp.checked)
    {
        App.dateScorePublic.allowBlank = false;
        App.dateScorePublic.setDisabled(false);
        App.dateScorePublic.focus();
    }
    else
    {
        App.dateScorePublic.reset();
        App.dateScorePublic.allowBlank = true;
        App.dateScorePublic.setDisabled(true);
    }
}
/*
单击选择分数规则
*/
function txtPerformanceRole_IndicatorIconClick()
{
    if (optype == "v")
    {
        MessageBoxShowWarning(undefined,"查看模式下不能选择!",undefined);
        return;
    }
    App.winSelectPerformanceRole.loader.url = getCurrentUrl() + "SelectPerformanceRole.aspx";
    App.winSelectPerformanceRole.loader.loadFrame();
    App.winSelectPerformanceRole.show();
}
/*
大小变化
*/
function winSelectPerformanceRole_Resize(thisCmp,  width,  height)
{
   App.winSelectPerformanceRole.updateLayout();//App.winSelectPerformanceRole.loader.loadFrame();
}
/*
清空分数规则
*/
function clearPerformanceRules_Click()
{
    App.txtPerformanceRole.clear();
    App.hidden_PerformanceRole.clear();
}
/*
单击选择试卷
*/
function txtPaperName_IndicatorIconClick()
{
    if (optype == "v") {
        MessageBoxShowWarning(undefined, "查看模式下不能选择!", undefined);
        return;
    }
    App.winSelectPaperName.loader.url = getCurrentUrl() + "SelectPaper.aspx";
    App.winSelectPaperName.loader.loadFrame();
    App.winSelectPaperName.show();
}

/*
大小变化
*/
function winSelectPaperName_Resize(thisCmp, width, height) {
    App.winSelectPaperName.updateLayout();//App.winSelectPerformanceRole.loader.loadFrame();
}

