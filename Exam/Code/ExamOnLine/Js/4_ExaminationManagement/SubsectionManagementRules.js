/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />
var returnUrl;
var returnUrl1;
var id;
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
    returnUrl1 = getQueryString("return1");
    returnUrl = getQueryString("return");
    id = getQueryString("id");
}
/*
点击查询
*/
function btnQuery_Click() {
    App.GridPanel1.store.load();
}
/*
返回
*/
function btnReturn_Click() {
    if (returnUrl != undefined) {
        this.frameElement.src = getCurrentUrl() + returnUrl1 + "?return=" + returnUrl;
    }
}

function btnAddPerformanceRuleItem_Click()
{
    this.frameElement.src = getCurrentUrl() + "AddPerformanceRulesItem.aspx?optype=a&return2=SubsectionManagementRules.aspx&return=" + returnUrl + "&return1="+returnUrl1+"&id="+id;;
}
/*
删除前
*/
function CommandColumn_DeleteCommand_Before()
{
    showLoading("Viewport1", "正在删除数据,请稍等......");
}
/*
点击修改
*/
function CommandColumn_Edit_Command(item, command, record, recordIndex, cellIndex) {
    this.frameElement.src = getCurrentUrl() + "AddPerformanceRulesItem.aspx?optype=u&return2=SubsectionManagementRules.aspx&return=" + returnUrl + "&return1=" + returnUrl1 + "&id=" + id+"&id1="+record.data.ID;
}
/*
删除ajax完成
*/
function CommandColumn_DeleteCommand_Complete()
{
    hideLoading("Viewport1");
}

var onShow = function (toolTip, grid) {
    var view = grid.getView(),
        store = grid.getStore(),
        record = view.getRecord(view.findItemByChild(toolTip.triggerElement)),
        column = view.getHeaderByCell(toolTip.triggerElement),
        data = record.get(column.dataIndex);
    if (data != undefined && column.xtype == "gridcolumn")
    {
        toolTip.show();
        toolTip.update(data);
    }
    else
        toolTip.hide();
};
