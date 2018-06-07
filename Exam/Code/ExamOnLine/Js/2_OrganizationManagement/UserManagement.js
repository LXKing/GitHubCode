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
function btnSearch_Click() {
    App.GridPanel1.store.load();
}
/*
查询成功
*/
function searchComplete() {
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
/*
转向添加用户
*/
function btnAddUser_Click()
{
    this.location = getCurrentUrl() + "AddUser.aspx?return=UserManagement.aspx&optype=a";
}

/*
绑定状态值
*/
function changeSelect(value) {
    var color="green";
    if(value==1 || value=="1")
    {
        value="启用";
        color="green";
    }
    else
    {
        value="禁用";
        color="red";
    }
    var format = "<span style='color:" + color + ";'>" + value + "</span>";
    return format;
}

function Refresh(store) {
    var data = store;
}