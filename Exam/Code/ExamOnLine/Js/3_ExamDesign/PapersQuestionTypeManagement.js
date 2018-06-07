/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />


/*
添加题型
*/
function btnAddQuestionsType() {
    var url = "AddPapersQuestionType.aspx?optype=a&paperid=" + App.hidPaperID.value;
    App.Window1.loader.url = url;
    App.Window1.loader.loadFrame();
    App.Window1.show();
}

/*
返回
*/
function goBack() {
    this.location = getCurrentUrl() + getQueryString("return");
}

/*
查看试题
*/
function CommandColumn_ViewQuestion_Command(item, command, record, recordIndex, cellIndex) {
    var id = record.data.ID;
    var url = "QuestionsInfo.aspx?id=" + id;
    App.Window2.loader.url = url;
    App.Window2.loader.loadFrame();
    App.Window2.show();
}

/*
查看
*/
function CommandColumn_View_Command(item, command, record, recordIndex, cellIndex) {
    var id = record.data.ID;
    
    var url = "AddPapersQuestionType.aspx?optype=v&id=" + id;
    App.Window1.loader.url = url;
    App.Window1.loader.loadFrame();
    App.Window1.show();
}
/*
修改
*/
function CommandColumn_Edit_Command(item, command, record, recordIndex, cellIndex) {
    var id = record.data.ID;
    
    var url = "AddPapersQuestionType.aspx?optype=u&id=" + id;
    App.Window1.loader.url = url;
    App.Window1.loader.loadFrame();
    App.Window1.show();
}