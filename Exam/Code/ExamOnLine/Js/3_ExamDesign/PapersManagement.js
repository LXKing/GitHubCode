/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />


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
查询
*/
function btnSearch_Click() {
    App.GridPanel1.store.load();
}
/*
清空条件
*/
function btnClear_Click() {
    App.txtKnowledge.reset();
    App.txtSubject.reset();
    App.cmbQuestionsType.reset();
    App.cmbDifficulty.reset();
    App.cmbStatus.reset();
    App.hidden_KnowledgeID.reset();
}
/*
添加试卷
*/
function btnAddPaper_Click() {
    this.frameElement.src = getCurrentUrl() + "AddPapers.aspx?optype=a&return=PapersManagement.aspx";
}

/*
题型管理
*/
function CommandColumn_Manage_Command(item, command, record, recordIndex, cellIndex) {
    var id = record.data.ID;
    this.frameElement.src = getCurrentUrl() + "PapersQuestionTypeManagement.aspx?optype=v&id=" + id + "&return=PapersManagement.aspx";
}

/*
查看
*/
function CommandColumn_View_Command(item, command, record, recordIndex, cellIndex) {
    var id = record.data.ID;
    this.frameElement.src = getCurrentUrl() + "AddPapers.aspx?optype=v&id=" + id + "&return=PapersManagement.aspx";
}
/*
修改
*/
function CommandColumn_Edit_Command(item, command, record, recordIndex, cellIndex) {
    var id = record.data.ID;
    this.frameElement.src = getCurrentUrl() + "AddPapers.aspx?optype=u&id=" + id + "&return=PapersManagement.aspx";
}