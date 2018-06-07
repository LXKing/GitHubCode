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

/*
显示/隐藏查询
*/
function btnShowQueryUI_Click(btn) {
    var toolBar = App.Toolbar_Search;
    if (toolBar.hidden) {
        toolBar.show();
        btn.setText("隐藏查询");
    }
    else {
        toolBar.hide();
        btn.setText("显示查询");
    }
}
/*
添加考试安排
*/
function btnAddExamPlan_Click()
{
    this.frameElement.src = getCurrentUrl() + "AddExamsPlan.aspx?optype=a&return=ExamsPlansManagement.aspx";
}

/*
查看过期考试安排
*/
function btnExpiredExamPlan_Click() {
    this.frameElement.src = getCurrentUrl() + "ExpiredExamPlans.aspx?return=ExamsPlansManagement.aspx";
}
/*
点击查询
*/
function btnQuery_Click() {
    App.GridPanel1.store.load();
}

/*
清空查询条件
*/
function btbClearCodition_Click() {
    App.cmbMakeQuestionType.clear();
    App.txtExamType.clear();
    App.txtExamName.clear();
    App.hidden_ExamTypeID.clear();
}
/*
删除前
*/
function CommandColumn_Delete_Before()
{
    return true;
}
/*
编辑
*/
function CommandColumn_View_Command(item, command, record, recordIndex, cellIndex)
{
    this.frameElement.src = getCurrentUrl() + "AddExamsPlan.aspx?optype=v&id=" + record.data.ID + "&return=ExamsPlansManagement.aspx";
}
/*
编辑
*/
function CommandColumn_Edit_Command(item, command, record, recordIndex, cellIndex)
{
    this.frameElement.src = getCurrentUrl() + "AddExamsPlan.aspx?optype=u&id="+record.data.ID+"&return=ExamsPlansManagement.aspx";
}
/*
规则
*/
function CommandColumn_Rules_Command(item, command, record, recordIndex, cellIndex)
{
    this.frameElement.src = getCurrentUrl() + "PerformanceRulesManage.aspx?return=ExamsPlansManagement.aspx";
}
/*
授权
*/
function CommandColumn_Select_Command(item, command, record, recordIndex, cellIndex) {
    this.frameElement.src = getCurrentUrl() + "Authorization.aspx?id=" + record.data.ID + "&return=ExamsPlansManagement.aspx";
}
