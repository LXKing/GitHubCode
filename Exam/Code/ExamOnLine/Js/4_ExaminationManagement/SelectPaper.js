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
function CommandColumn_Select_Command(item, command, record, recordIndex, cellIndex) {
    MessageBoxShowInfo(
        undefined,
        "已选择:" + record.data.PAPER_NAME + ".",
        function () {
            this.parent.App.winSelectPaperName.close();
            this.parent.App.txtPaperName.setValue(record.data.PAPER_NAME);
            this.parent.App.hidden_PaperID.setValue(record.data.ID);
        }
    );
}
/*
清除条件
*/
function btnClear_Click()
{
    App.cmbMakeQuestionType.reset();
    App.cmbPaperType.reset();
    App.txtPaperName.reset();
}