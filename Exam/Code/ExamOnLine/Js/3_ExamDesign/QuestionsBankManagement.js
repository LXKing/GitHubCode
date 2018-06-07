/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />

$(document).ready(function () {
    Select();
});
function Select()
{
    Ext.selection.CheckboxModel.override({
        selectAll: function (suppressEvent) {
            var me = this,
                selections = me.store.getAllRange(), // instead of the getRange call
                i = 0,
                len = selections.length,
                start = me.getSelection().length;

            me.suspendChanges();

            for (; i < len; i++) {
                me.doSelect(selections[i], true, suppressEvent);
            }

            me.resumeChanges();
            if (!suppressEvent) {
                me.maybeFireSelectionChange(me.getSelection().length !== start);
            }
        },

        deselectAll: Ext.Function.createSequence(
            Ext.selection.CheckboxModel.prototype.deselectAll,
            function () {
                this.view.panel.getSelectionMemory().clearMemory();
            }
        ),

        updateHeaderState: function () {
            var me = this,
                store = me.store,
                storeCount = store.getTotalCount(),
                views = me.views,
                hdSelectStatus = false,
                selectedCount = 0,
                selected, len, i;

            //if (!store.buffered && storeCount > 0) {
            //    selected = me.view.panel.getSelectionMemory().selectedIds;
            //    hdSelectStatus = true;
            //    for (s in selected) {
            //        ++selectedCount;
            //    }

            //    hdSelectStatus = storeCount === selectedCount;
            //}
            
            if (me.selected.items.length > 0 && (me.store.pageSize == me.selected.items.length || me.selected.items.length == storeCount)) {//views && views.length
                me.toggleUiHeader(true);
            }
            else
                me.toggleUiHeader(false);
        }
    });
}
function Knowledge_IndicatorIconClick()
{
    //App.treePanelKnowledge.show();
}

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
function btnSearch_Click()
{
    App.GridPanel1.store.load();
}
/*
清空条件
*/
function btnClear_Click()
{
    App.txtKnowledge.reset();
    App.txtSubject.reset();
    App.cmbQuestionsType.reset();
    App.cmbDifficulty.reset();
    App.cmbStatus.reset();
    App.hidden_KnowledgeID.reset();
}
/*
增加新题
*/
function btnAddQuestion_Click() {
    this.frameElement.src = getCurrentUrl() + "AddQuestions.aspx?optype=a&return=QuestionsBankManagement.aspx";
}

/*
查看
*/
function CommandColumn_View_Command(item, command, record, recordIndex, cellIndex)
{
    var id=record.data.ID;
    this.frameElement.src = getCurrentUrl() + "AddQuestions.aspx?optype=v&id="+id+"&return=QuestionsBankManagement.aspx";
}
/*
修改
*/
function CommandColumn_Edit_Command(item, command, record, recordIndex, cellIndex) {
    var id = record.data.ID;
    this.frameElement.src = getCurrentUrl() + "AddQuestions.aspx?optype=u&id=" + id + "&return=QuestionsBankManagement.aspx";
}

