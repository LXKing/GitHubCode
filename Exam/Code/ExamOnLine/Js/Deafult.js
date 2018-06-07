/// <reference path="../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../Scripts/jquery.cookie.js" />
/// <reference path="../Scripts/common.js" />
/// <reference path="../Scripts/extjsEx.js" />

window.onbeforeunload = function (event) {
    return "";
}
window.onunload = function (event) {
    //$.removeCookie("loginCookie");
}
var expand = true;
function shrinkMenu() {
    var url = getCurrentUrl();
    if (expand) {
        Ext.getCmp('pnlLeft').collapse();
        $(App.img_HideMenu.imgEl.dom).attr("src", url.concat("Images/Default/right.png"));
        expand = false;
    }
    else {
        Ext.getCmp('pnlLeft').expand();
        $(App.img_HideMenu.imgEl.dom).attr("src", url.concat("Images/Default/left.png"));
        expand = true;
    }
}

/*
刷新内部选项卡页面
*/
function refreshPage() {
    //Ext.getCmp('ExampleTabs').getActiveTab().load();
    Ext.getCmp('ExampleTabs').getActiveTab().iframe.dom.src = Ext.getCmp('ExampleTabs').getActiveTab().iframe.dom.src;
};

function addTab(tabPanel, id, href, menuItem) {
    var tab = tabPanel.getComponent(id);
    loadExample(href, id);
}

function makeTab(href, id) {
    var tab,
        hostName,
        exampleName,
        node,
        tabTip;

    hostName = window.location.protocol + "//" + window.location.host;
    exampleName = href;

    tab = App.ExampleTabs.add(new Ext.Panel({
        id: id,
        title: id,
        closable: true,
        loader: {
            renderer: "frame",
            url: href,
            loadMask: {
                loadMask: true,
                msg: "Loading " + id + "...."
            }
        }
    }));

    App.ExampleTabs.setActiveTab(tab);
};

var lookup = {};

function loadExample(href, id) {
    var tab = App.ExampleTabs.getComponent(id),
        lObj = lookup[href];

    lookup[href] = id;

    if (tab) {
        App.ExampleTabs.setActiveTab(tab);
    } else {
        makeTab(href, id);
    }
};

function change(token) {
    if (token) {
        var url = token.split(',')[0];
        var id = token.split(',')[1];
        loadExample(url, id);
    } else {
        App.ExampleTabs.setActiveTab(0);
    }
};
/*
注销退出前
 */
function img_Logout_BeforeClick() {
    showLoading("Viewport1", "正在注销退出......");
};
/*
注销
*/
function img_Logout_Click()
{
    MessageBoxShowQuestion(
        undefined,
        "确定注销退出系统?",
        function () {
            Ext.net.directRequest({
                before: function (el, type, action, extraParams, o) {
                    img_Logout_BeforeClick();
                },
                control: Ext.getCmp("img_Logout")
            });
        },
    undefined);
    return false;
}
/*
关闭皮肤设置
*/
function btnSaveTheme_Click()
{
    App.themeWindow.close();
}