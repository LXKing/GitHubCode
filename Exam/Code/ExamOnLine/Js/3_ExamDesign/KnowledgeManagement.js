/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />

var isExpand = true;
function expandAll() {
    if (isExpand) {
        App.departMent1_TreePanel_Main.collapseAll();
        App.btnExpandAll.Text = "展开全部";
        isExpand = false;
    }
    else {
        App.departMent1_TreePanel_Main.expandAll();
        App.btnExpandAll.Text = "折叠全部";
        isExpand = true;
    }
}