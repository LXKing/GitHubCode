/// <reference path="../../Scripts/jquery-1.8.2.min.js" />
/// <reference path="../../Scripts/jsview/JsRender/jsrender.min.js" />
/// <reference path="../../Scripts/jquery.messi/messi.min.js" />
/// <reference path="../../Scripts/jquery.cookie.js" />
/// <reference path="../../Scripts/common.js" />
/// <reference path="../../Scripts/extjsEx.js" />
var path = '../../Images/NoImage.png';
var fileUpload = 'FileUploadField1';

function viewImage() {
    var img = App.imgPreView.imgEl.dom.id;
    uploadImagePreview(img, fileUpload);  
}

function uploadImage() {
    $.ajax({
        contentType: false,
        processData: false,
        url: "../../Ashx/UploadImageHandler.ashx",
        type: 'post',
        async: false,
        data: document.forms.form1,
        success: function (data) {
            if (data.Message != "-1") {
                alert("图片上传成功!");
            }
            else {
                alert("图片上传失败!");
            }
        }
    });
}

function resetImage() {
    App.imgPreView.setImageUrl(path);
    var obj = document.getElementById(fileUpload);
    obj.outerHTML = obj.outerHTML;
}

function goBack() {
    this.location = getCurrentUrl() + getQueryString("return");
}