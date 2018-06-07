/// <reference path="jquery-1.8.2.min.js" />
/// <reference path="jquery.messi/messi.min.js" />


//html获取地理位置
function getLocation(successHandle, errorHandle) {
    if (navigator.geolocation) {
        if (errorHandle == null) errorHandle = defaultErrorHandle;
        navigator.geolocation.getCurrentPosition(
                function (p) {
                    successHandle(p);
                },
                errorHandle,
                {
                    enableHighAccuracy: false,
                    timeout: 15000
                }//精确定位设置为true
            );
    }
    else {
        alert("浏览器不支持定位服务");
    }
}
function defaultErrorHandle(error) {
    switch (error.code) {
        case error.TIMEOUT:
            showMsg({ msg: "获取位置信息超时，请重试", title: "错误" });
            break;
        case error.PERMISSION_DENIED:
            showMsg({ msg: "您拒绝了使用位置共享服务", title: "错误" });
            break;
        case error.POSITION_UNAVAILABLE:
            showMsg({ msg: "获取位置信息失败，请重试", title: "错误" });
            break;
    }
}
//获取url参数
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        var v = r[2];
        return v;//unescape();
    }
    else
        return null;
}
//获取不包含文件名的url
function getCurrentUrl()
{
    var strUrl = window.location.href;
    var strPage = strUrl.substring(0, strUrl.lastIndexOf("/")).concat("/");
    return strPage;
}

//时间加减
Date.prototype.DateAdd = function (strInterval, Number) {
    var dtTmp = this;
    switch (strInterval) {
        case 's': return new Date(Date.parse(dtTmp) + (1000 * Number));
        case 'n': return new Date(Date.parse(dtTmp) + (60000 * Number));
        case 'h': return new Date(Date.parse(dtTmp) + (3600000 * Number));
        case 'd': return new Date(Date.parse(dtTmp) + (86400000 * Number));
        case 'w': return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));
        case 'q': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number * 3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'm': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'y': return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
    }
}
//时间格式化
Date.prototype.Format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month 
        "d+": this.getDate(), //day 
        "h+": this.getHours(), //hour 
        "m+": this.getMinutes(), //minute 
        "s+": this.getSeconds(), //second 
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
        "S": this.getMilliseconds() //millisecond 
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}
//求两个时间毫秒差
Date.prototype.DiffMilliseconds = function (date) {
    var date1 = this;
    if (date != undefined) {
        var milliseconds = Math.abs(date1.getTime() - Date.parse(date));
        return milliseconds;
    }
    else
        return null;
}
//求两个时间秒差
Date.prototype.DiffSeconds = function (date) {
    var date1 = this;
    if (date != undefined) {
        var milliseconds = date1.DiffMilliseconds(date);
        return milliseconds / 1000;//+ (milliseconds % 1000 > 0) ? 1 : 0
    }
    else
        return null;
}
//求两个时间分钟差
Date.prototype.DiffMinutes = function (date) {
    var date1 = this;
    if (date != undefined) {
        var milliseconds = date1.DiffMilliseconds(date);
        return milliseconds / (1000 * 60);//+ (milliseconds % (1000 * 60*60) > 0) ? 1 : 0
    }
    else
        return null;
}
//求两个时间小时差
Date.prototype.DiffHours = function (date) {
    var date1 = this;
    if (date != undefined) {
        var milliseconds = date1.DiffMilliseconds(date);
        return milliseconds / (1000 * 60 * 60);//+ (milliseconds % (1000 * 60 * 60*60) > 0) ? 1 : 0
    }
    else
        return null;
}
//求两个时间天数差
Date.prototype.DiffDays = function (date) {
    var date1 = this;
    if (date != undefined) {
        var milliseconds = date1.DiffMilliseconds(date);
        return milliseconds / (1000 * 60 * 60 * 24);//+ (milliseconds % (1000 * 60 * 60 * 60*24) > 0) ? 1 : 0
    }
    else
        return null;
}
//判断是否全部为数字
function isDigit(s) {
    var patrn = /^[0-9]{1,20}$/;
    if (!patrn.exec(s)) return false
    return true
}
//判断是否为IP地址
function isIP(s) //by zergling
{
    var patrn = /^[0-9.]{1,20}$/;
    if (!patrn.exec(s)) return false
    return true
}

//去除字符串的首尾的空格
function trim(str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

// 判断输入是否是有效的电子邮件 
function isEmail(str) {
    var result = str.match(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/);
    if (result == null) return false;
    return true;
}
//匹配中国邮政编码(6位) 
function isPostcode(str) {
    var result = str.match(/[1-9]\d{5}(?!\d)/);
    if (result == null) return false;
    return true;
}
//匹配国内电话号码(0511-4405222 或 021-87888822) 
function isTel(str) {
    var result = str.match(/\d{3}-\d{8}|\d{4}-\d{7}/);
    if (result == null) return false;
    return true;
}
//校验手机号码：必须以数字开头，除数字外，可含有“-”
function isMobil(s) {
    var result = false;
    result = /^(1[0-9]{10})?$/.test(s);
    return result;
}
//匹配身份证(15位或18位) 
function isIDCard(str) {
    var result = str.match(/\d{15}|\d{18}/);
    if (result == null) return false;
    return true;
}

Messi.prototype.setLoadingContent = function(templateid, msg)
{
    var content = $.templates("#" + templateid).render({ msg: msg });
    this.setContent(content);
}
//创建loading遮罩层
var newMessi = function (templateid, msg) {
    return new Messi(
        //msg,
        $.templates("#" + templateid).render({ msg: msg }),
        {
            title: null,
            autoclose: null,
            callback: null,
            closeButton: false,
            titleClass: "anim",
            modalOpacity: 0.3,
            modal: true,
            width: document.documentElement.clientWidth * 0.3
        });
}
//获取浏览器县市区宽度
function getWidth() {
    return document.documentElement.clientWidth;
}
//获取浏览器县市区高度
function getHeight() {
    return document.documentElement.clientHeight;
}

//弹出提示窗口
function showMsg(titleText,msgText) {
    $.modaldialog.warning(
                msg,
                {
                    width: parseInt(getWidth() * 0.7),
                    title: title
                }
     );
}

//产生GUID
function newGuid() {
    var S4 = function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    };
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}

//(微信)关闭微信弹出的web窗体
function closeWechatWebWindow() {
    if (WeixinJSBridge != null)
        WeixinJSBridge.invoke('closeWindow', {}, function (res) {
        });
}
//(微信)隐藏右上角按钮
function hideWechatTopRightButton() {
    function onBridgeReady() {
        WeixinJSBridge.call('hideOptionMenu');
    }

    if (typeof WeixinJSBridge == "undefined") {
        if (document.addEventListener) {
            document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
        } else if (document.attachEvent) {
            document.attachEvent('WeixinJSBridgeReady', onBridgeReady);
            document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
        }
    } else {
        onBridgeReady();
    }
}
//(微信)隐藏微信底部按钮
function hideWechatBottomButton() {
    function onBridgeReady() {
        WeixinJSBridge.call('hideToolbar');
    }

    if (typeof WeixinJSBridge == "undefined") {
        if (document.addEventListener) {
            document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
        } else if (document.attachEvent) {
            document.attachEvent('WeixinJSBridgeReady', onBridgeReady);
            document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
        }
    } else {
        onBridgeReady();
    }
}

function uploadImagePreview(picId, fileId) {
    var pic = document.getElementById(picId);
    var file = document.getElementById(fileId);
    if (window.FileReader) {//chrome,firefox7+,opera,IE10,IE9，IE9也可以用滤镜来实现
        oFReader = new FileReader();
        oFReader.readAsDataURL(file.files[0]);
        oFReader.onload = function (oFREvent) { pic.src = oFREvent.target.result; };
    }
    else if (document.all) {//IE8-
        file.select();
        file.blur();
        var reallocalpath = document.selection.createRange().htmlText//IE下获取实际的本地文件路径
        if (window.ie6) pic.src = reallocalpath; //IE6浏览器设置img的src为本地路径可以直接显示图片
        else { //非IE6版本的IE由于安全问题直接设置img的src无法显示本地图片，但是可以通过滤镜来实现，IE10浏览器不支持滤镜，需要用FileReader来实现，所以注意判断FileReader先
            pic.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod='scale',src=\"" + reallocalpath + "\")";
            pic.src = 'data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==';//设置img的src为base64编码的透明图片，要不会显示红xx
        }
    }
    else if (file.files) {//firefox6-
        if (file.files.item(0)) {
            url = file.files.item(0).getAsDataURL();
            pic.src = url;
        }
    }
}
