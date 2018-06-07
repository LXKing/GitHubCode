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
    UE.getEditor('editor2', {
        autoHeightEnabled: false,
        initialFrameHeight: 200
    });
    UE.getEditor('editor3', {
        autoHeightEnabled: false,
        initialFrameHeight: 200
    });
}

function changeType() {
    var typeArray = App.sltType.value.toString().split(',');
    //App.hidQuestionTypeID.value = typeArray[0];
    //App.hidTemplateID.value = typeArray[1];

    switch (typeArray[1]) {
        case "5eb171e0-c68d-44cc-a371-c56c87079eb2":
            App.content.show();
            App.sltNum.show();
            App.sltNum.setValueAndFireSelect('4');
            App.rdoGpSingle.show();
            App.chbGpMultiple.hide();
            App.rdoGpJudge.hide();
            break;

        case "22361beb-d5c9-4a49-b14d-0385c5ceb4a4":
            App.content.show();
            App.sltNum.show();
            App.sltNum.setValueAndFireSelect('4');
            App.rdoGpSingle.hide();
            App.chbGpMultiple.show();
            App.rdoGpJudge.hide();
            break;

        case "3115bd1f-a88e-42f4-9117-787bccd5a73d":
            App.content.hide();
            App.sltNum.hide();
            App.rdoGpSingle.hide();
            App.chbGpMultiple.hide();
            App.rdoGpJudge.show();
            break;

        default:
            break;
    }
}

function goBack() {
    this.location = getCurrentUrl() + getQueryString("return");
}