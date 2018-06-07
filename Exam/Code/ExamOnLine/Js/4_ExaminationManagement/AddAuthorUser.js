/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />

/*
查询
*/
function btnSearch_Click() {
    App.GridPanel1.store.load();
}

function btnAuthorManyUser() {
    if (getQueryString('optype') == 'a') {
        this.parent.App.gpnlUser.store.load();
    }
    else if (getQueryString('optype') == 'b') {
        this.parent.App.gpnlReviwers.store.load();
    }
}