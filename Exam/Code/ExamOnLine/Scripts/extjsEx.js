

/*
显示loading
*/
function showLoading(id, msg)
{
    var v = Ext.getCmp(id);
    if(v!=undefined)
    {
        v.setLoading(msg)
    }
}
/*
隐藏loading
*/
function hideLoading(id)
{
    var v = Ext.getCmp(id);
    if (v != undefined) {
        v.loadMask.hide();
    }
}


/*
弹出提示对话框
*/
function MessageBoxShow(strTitle, strMsg, clickAction)
{
    //Ext.Msg.show({ "title": "提示", "buttons": { "ok": "确定" }, "icon": Ext.Msg.INFO, "msg": "考试类型更新成功!" });
    Ext.Msg.show(
        {
            "title": strTitle,
            "buttons": { "ok": "确定" },
            "icon": Ext.Msg.INFO,
            "msg": strMsg,
            "fn": clickAction
        });
    //Ext.MessageBox.alert(strTitle, strMsg, clickfunName);
}
/*
弹出提示
*/
function MessageBoxShowInfo(strTitle, strMsg,clickAction)
{
    if (strTitle == undefined)
        strTitle = "提示";
    Ext.Msg.show(
        {
            "title": strTitle,
            "buttons": { "ok": "确定" },
            "icon": Ext.Msg.INFO,
            "msg": strMsg,
            "fn": clickAction
        });
}
/*
弹出警告
*/
function MessageBoxShowWarning(strTitle, strMsg,clickAction)
{
    if (strTitle == undefined)
        strTitle = "警告";
    Ext.Msg.show(
        {
            "title": strTitle,
            "buttons": { "ok": "确定" },
            "icon": Ext.Msg.WARNING,
            "msg": strMsg,
            "fn": clickAction
        });
}
/*
弹出错误
*/
function MessageBoxShowError(strTitle, strMsg, clickAction)
{
    if (strTitle == undefined)
        strTitle = "错误";
    Ext.Msg.show(
        {
        "title": strTitle,
        "buttons": { "ok": "确定" },
        "icon": Ext.Msg.ERROR,
        "msg": strMsg,
        "fn": clickAction
    });
}
/*
弹出询问
*/
function MessageBoxShowQuestion(strTitle, strMsg, clickOkAction,clickCancelAction)
{
    if (strTitle == undefined)
        strTitle = "询问";
    Ext.Msg.show(
        {
            "title": strTitle,
            "buttons": { "ok": "确定", "cancel": "取消" },
            "icon": Ext.Msg.QUESTION,
            "msg": strMsg,
            "fn": function (buttonId, text, opt) {
                switch(buttonId)
                {
                    case "ok":
                        if (clickOkAction != undefined)
                            clickOkAction();
                        break;
                    case "cancel":
                        if (clickCancelAction != undefined)
                            clickCancelAction();
                        break;
                }
            }
        });
}


/*
自定义身份证验证
*/
Ext.apply(Ext.form.VTypes, {
    idcard: function (pId, field) {
        var arrVerifyCode = [1, 0, "x", 9, 8, 7, 6, 5, 4, 3, 2];
        var Wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
        var Checker = [1, 9, 8, 7, 6, 5, 4, 3, 2, 1, 1];
        if (pId.length != 15 && pId.length != 18) {
            return false;
        }
        var Ai = pId.length == 18 ? pId.substring(0, 17) : pId.slice(0, 6) + "19" + pId.slice(6, 16);
        if (!/^\d+$/.test(Ai)) {
            return false;
        }
        var yyyy = Ai.slice(6, 10), mm = Ai.slice(10, 12) - 1, dd = Ai.slice(12, 14);
        var d = new Date(yyyy, mm, dd), now = new Date();
        var year = d.getFullYear(), mon = d.getMonth(), day = d.getDate();
        if (year != yyyy || mon != mm || day != dd || d > now || year < 1940) {
            return false;
        }
        for (var i = 0, ret = 0; i < 17; i++) ret += Ai.charAt(i) * Wi[i];
        Ai += arrVerifyCode[ret %= 11];
        return pId.length == 18 && pId != Ai ? false : true;
    },
    idcardText: '身份证号错误'
});

/*
自定义电话验证
*/
Ext.apply(Ext.form.VTypes, { 
    phone: function (val, field) {
        var reg = /^\d{3,4}-?\d{6,8}$/;
        return reg.test(val) == false ? false : true;
    },
    phoneText: "电话号码错误"
});

/*
自定义手机验证
*/
Ext.apply(Ext.form.VTypes, {
    mobile: function (val, field) {
        var reg = /^1[358]\d{9}$/;
        return reg.test(val) == false ? false : true;
    },
    mobileText: "手机号码错误"
});

/*
自定义邮编验证
*/
Ext.apply(Ext.form.VTypes, {
    postcode: function (val, field) {
        var reg = /^\d{6}$/;
        return reg.test(val) == false ? false : true;
    },
    postcodeText: "邮编错误"
});