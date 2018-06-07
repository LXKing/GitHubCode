/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />
var returnUrl;
/*
初始化文
*/
$(document).ready(function () {

});

window.onload = function () {
    Init();
}
/*
初始化
*/
function Init() {
    returnUrl = getQueryString("return");
}
/*
添加新规则
*/
function btnAddSelectPerformanceRule_Click()
{
    this.frameElement.src = getCurrentUrl() + "AddPerformanceRule.aspx?optype=a&return1=PerformanceRulesManage.aspx&doc=" + Ext.Number.randomInt(1, 1000).toString() + "&return=" + returnUrl;
}
/*
返回
*/
function btnReturn_Click() {
    if (returnUrl != undefined) {
        this.frameElement.src = getCurrentUrl() + returnUrl;
    }
}

/*
查询
*/
function btnQuery_Click() {
    App.GridPanel1.store.load();
}
/*
清空
*/
function btbClearCodition_Click()
{
    App.txtRulesName.reset();
}

function CommandColumn_SubsectionManage_Command(item, command, record, recordIndex, cellIndex)
{
    this.frameElement.src = getCurrentUrl() + "SubsectionManagementRules.aspx?return1=PerformanceRulesManage.aspx&id=" + record.data.ID + "&_dc=" + Ext.Number.randomInt(1, 1000).toString() + "&return=" + returnUrl;
}
/*
查看
*/
function CommandColumn_View_Command(item, command, record, recordIndex, cellIndex)
{
    this.frameElement.src = getCurrentUrl() + "AddPerformanceRule.aspx?optype=v&return1=PerformanceRulesManage.aspx&id=" + record.data.ID + "&_dc=" + Ext.Number.randomInt(1, 1000).toString() + "&return=" + returnUrl;
}
/*
编辑
*/
function CommandColumn_Edit_Command(item, command, record, recordIndex, cellIndex)
{
    this.frameElement.src = getCurrentUrl() + "AddPerformanceRule.aspx?optype=u&return1=PerformanceRulesManage.aspx&id=" + record.data.ID + "&_dc=" + Ext.Number.randomInt(1, 1000).toString()+ "&return=" + returnUrl;
}
/*
删除前
*/
function CommandColumn_DeleteCommand_Before()
{
    showLoading("Viewport1", "正在删除数据，请稍等......");
}
/*
删除ajax完成后
*/
function CommandColumn_DeleteCommand_Complete()
{
    hideLoading("Viewport1");
}






