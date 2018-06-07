/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />

var fromPage=undefined;
var optype = undefined;
$(document).ready(function () {
    Init();
});
/*
初始化页面参数
*/
function Init()
{
    fromPage = getQueryString("return");
    optype = getQueryString("optype");
    var title = "";
    switch (optype) {
        case "a":
            title = "▏" + "增加题型";
            break;
        case "u":
            title = "▏" + "编辑题型";
            break;
        case "v":
            title = "▏" + "查看题型";
            break;
    }
    this.document.title = title;
}
/*
返回js
*/
function btnReturn_Click()
{
    
    if (fromPage != undefined)
        this.frameElement.src = getCurrentUrl() + fromPage;
}

/*
清空
*/
function btnClear_Click()
{
    App.cmbQuestionTypeTemplate.reset();
    App.txtQuestionTypeName.reset();
    App.txtSequence.reset();
    App.numScore.reset();
    App.numScore.setValue(0);
}