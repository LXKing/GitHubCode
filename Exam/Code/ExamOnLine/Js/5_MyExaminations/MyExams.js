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
点击查询
*/
function btnQuery_Click() {
    App.GridPanel1.store.load();
}

function txtExamType_IndicatorIconClick(item, indicatorEl)
{
    App.windowMain.show();
}

/*
清空查询条件
*/
function btbClearCodition_Click() {
    App.txtExamType.clear();
    App.txtExamName.clear();
}