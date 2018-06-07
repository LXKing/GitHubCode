<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyPwd.aspx.cs" Inherits="ExamOnLine.Pages.MyAccount.ModifyPwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改密码</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/6_MyAccount/ModifyPwd.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
            <Items>
                <ext:Panel
                    ID="Panel1"
                    runat="server"
                    Layout="FormLayout"
                    Title="▏修改密码"
                    Region="Center"
                    AutoScroll="true"
                    Header="true"
                    >
                    <Items>
                        <ext:TextField ID="txtOldPwd"
                                                    runat="server"
                                                    LabelWidth="60"
                                                    LabelAlign="Right"
                                                    FieldLabel="新密码"
                                                    InputWidth="150"
                                                    AllowBlank="false"
                                                    InputType="Password"
                                                    MaxLength="20"
                                                    StyleSpec="margin:15px 0px 0px 30px;"
                                                    >
                                                </ext:TextField>
                        <ext:TextField ID="txtNewPwd"
                                                    runat="server"
                                                    LabelWidth="60"
                                                    LabelAlign="Right"
                                                    FieldLabel="旧密码"
                                                    InputWidth="150"
                                                    AllowBlank="false"
                                                    InputType="Password"
                                                    MaxLength="20"
                                                    StyleSpec="margin:15px 0px 0px 30px;"
                                                    ></ext:TextField>
                        <ext:TextField ID="txtNewPwdRe"
                                                    runat="server"
                                                    LabelWidth="60"
                                                    LabelAlign="Right"
                                                    FieldLabel="重复密码"
                                                    InputWidth="150"
                                                    AllowBlank="false"
                                                    InputType="Password"
                                                    MaxLength="20"
                                                    StyleSpec="margin:15px 0px 0px 30px;"
                                                    ></ext:TextField>
                        <ext:Button runat="server" 
                                                    ID="btnSave"
                                                    Width="120"
                                                    Height="30"
                                                    Text="保存数据"
                                                    StyleSpec="margin:15px 0px 0px 95px;"
                                                    StandOut="true"
                                                    Icon="PageSave"
                                                    >
                                    <DirectEvents>
                                        <Click Before="return btnSave_Before();" After="" Success="" Failure="" Complete="btnSave_Complete();" OnEvent="btnSave_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
