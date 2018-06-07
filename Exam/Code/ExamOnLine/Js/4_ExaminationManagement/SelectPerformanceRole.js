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
点击查询
*/
function btnQuery_Click() {
    App.GridPanel1.store.load();
}
/*
点击选择
*/
function CommandColumn_Select_Command(item, command, record, recordIndex, cellIndex)
{
    MessageBoxShowInfo(
        undefined,
        "已选择:" + record.data.PERFORMANCE_RULES_NAME + ".",
        function () {
            this.parent.App.winSelectPerformanceRole.close();
            this.parent.App.txtPerformanceRole.setValue(record.data.PERFORMANCE_RULES_NAME);
            this.parent.App.hidden_PerformanceRole.setValue(record.data.ID);
        }
    );
}