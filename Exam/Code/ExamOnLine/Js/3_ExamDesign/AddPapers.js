/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />

/*
初始化文本编辑器
*/
$(document).ready(function () {
    initEditor();
});

function initEditor() {
    UE.getEditor('editor', {
        autoHeightEnabled: false,
        initialFrameHeight: 200
    });
}

function goBack() {
    this.location = getCurrentUrl() + getQueryString("return");
}