/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />

/*
显示/隐藏查询
*/
function btnShowQueryUI_Click(btn)
{
    var toolBar = App.Toolbar_Search;
    if (toolBar.hidden)
    {
        toolBar.show();
        btn.setText("隐藏查询");
    }
    else {
        toolBar.hide();
        btn.setText("显示查询");
    }
}
/*
点击刷新人数
*/
function btnRefreshUser_Click()
{
    App.GridPanel1.store.load();
}
/*
强制下线
*/
function CommandColumn_ForcedOffline_Click(item, command, record, recordIndex, cellIndex)
{
    var id = $.cookie("idCookie");
    if(id==record.data.ID)
    {
        MessageBoxShow("提示", "用户不能强制自己下线,请正常退出系统!");
        return false;
    }
    showLoading('Viewport1', '正在操作中,请稍等......');
}
/*
点击查询
*/
function btnSearch_Click()
{
    App.GridPanel1.store.load();
}
/*
查询成功
*/
function searchBefore() {
    showLoading('Viewport1', '正在查询中,请稍等......');
}
/*
查询成功
*/
function searchComplete()
{
    hideLoading('Viewport1')
}
/*
清空查询条件
*/
function btbClearCodition_Click() {
    App.cmbRole.clearValue();

    App.hidden_Department.clear();
    App.txtDepartment.clear();

    App.hidden_Position.clear();
    App.txtPosition.clear();

    App.cmbSex.clear();

    App.txtName.clear();
}