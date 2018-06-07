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
点击查询
*/
function btnQuery_Click() {
    App.GridPanel1.store.load();
}

/*
点击增加新题型
*/
function btnAddQuestionType_Click()
{
    this.frameElement.src = getCurrentUrl() + "AddQuestionsType.aspx?optype=a&return=QuestionsTypeManagement.aspx";
}

/*
查看
*/
function CommandColumn_View_Command(item, command, record, recordIndex, cellIndex)
{
    var id = record.data.ID;
    this.frameElement.src = getCurrentUrl() + "AddQuestionsType.aspx?optype=v&return=QuestionsTypeManagement.aspx&id="+id;
}

/*
编辑
*/
function CommandColumn_Edit_Command(item, command, record, recordIndex, cellIndex) {
    var id = record.data.ID;
    this.frameElement.src = getCurrentUrl() + "AddQuestionsType.aspx?optype=u&return=QuestionsTypeManagement.aspx&id=" + id;
}