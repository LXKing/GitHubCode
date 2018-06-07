/// <reference path="../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../Scripts/jquery.cookie.js" />
/// <reference path="../Scripts/common.js" />

var loading;
//回车键事件
document.onkeydown = function mykeydown() { //网页内按下回车触发,设置[登录]图片按钮响应回车
    if (event.keyCode == 13) {
        //document.getElementById("imgBtnLogin").click();
        $("#login").click();
        return false;
    }
}

/*
页面加载完成
*/
window.onload = function () {

};

//html dom加载完成后
$(document).ready(function () {

});

//点击登录
function login_Click() {
    var name = $("#txtName").val();
    var pwd = $("#txtPwd").val();
    if (name.length == 0) {
        alert("用户名不能为空!");
        $("#txtName").focus();
        return;
    }
    if (pwd.length == 0) {
        alert("用户密码不能为空!");
        $("#txtPwd").focus();
        return;
    }
    loading = newMessi("LoadingTemplate", "正在验证用户......");
    login(name, pwd);
}

//登录验证
function login(name, pwd) {
    $.ajax({
        type: "post",
        url: "Ashx/Login.ashx",
        data: { name: name, pwd: pwd },
        dataType: "json",
        gloabl: false,
        async: true,
        beforeSend: function (XHR) {
            //;newMessi($.templates("#LoadingTemplate").render({ msg: "正在验证用户......" }));
            true;
        },
        success: function (data) {
            if (data.Success) {
                loading.setLoadingContent("LoadingTemplate", "验证成功,正在跳转......");
                window.location.href = getCurrentUrl().concat("Default.aspx");
            }
            else {
                alert(data.Message);
                if (loading != undefined) {
                    loading.hide();
                }
            }
        },
        error: function (ex) {
        },
        complete: function (http) {

        }
    });
}