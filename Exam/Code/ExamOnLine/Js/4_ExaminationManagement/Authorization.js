/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />

/*
返回
*/
function goBack() {
    this.location = getCurrentUrl() + getQueryString("return");
}

/*
授权用户
*/
function GetUser_Click() {
    var url = "AddAuthorUser.aspx?optype=a&id=" + getQueryString("id");
    App.Window1.loader.url = url;
    App.Window1.loader.loadFrame();
    App.Window1.show();
}

/*
授权评卷人
*/
function GetReviwers_Click() {
    var url = "AddAuthorUser.aspx?optype=b&id=" + getQueryString("id");
    App.Window1.loader.url = url;
    App.Window1.loader.loadFrame();
    App.Window1.show();
}