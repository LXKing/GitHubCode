/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />

/*
初始化
*/
$(document).ready(function () {

});
/*
保存前验证
*/
function btnSave_Before()
{
    var success0 = App.txtExamTypeName.validate();
    var success1 = App.txtSequence.validate();
    var success2 = App.txtExamTypeDesc.validate();
    var result = success0 && success1 && success2;
    if(result)
    {
        showLoading("Viewport1","正在保存数据......");
    }
    return result;
}

function btnCancel_Before() {
    if (App.hidden_AddOrUpdate.value != "u" || App.hidden_CurrentID.value.length == 0)
    {
        MessageBoxShowWarning(undefined,"请选择要删除的节点!",undefined);
        return false;
    }
    else
    {
        showLoading("Viewport1", "正在删除数据......");
        return true;
    }
    
}

function btnSave_Complete() {
    hideLoading("Viewport1");
}
function showLoadBeforeSave()
{
    showLoading("Viewport1", "正在保存数据......");
}
function showLoadBeforeCancel() {
    showLoading("Viewport1", "正在删除数据......");
}
function hideLoad()
{
    hideLoading("Viewport1");
}
/*
清空按钮
*/
function btnClear_Click()
{
    App.txtExamTypeParent.reset();
    App.txtExamTypeParent.inputEl.fadeOut();
    App.txtExamTypeParent.inputEl.fadeIn();

    App.txtExamTypeName.reset();
    App.txtExamTypeName.inputEl.fadeOut();
    App.txtExamTypeName.inputEl.fadeIn();

    App.txtSequence.reset();
    App.txtSequence.inputEl.fadeOut();
    App.txtSequence.inputEl.fadeIn();

    App.txtExamTypeDesc.reset();
    App.txtExamTypeDesc.inputEl.fadeOut();
    App.txtExamTypeDesc.inputEl.fadeIn();

    App.hidden_ParentID.reset();
    App.hidden_AddOrUpdate.setValue('a');
    App.hidden_CurrentID.reset();
}
