<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ExamOnLine.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>在线考试登录</title>
    <link href="Css/Login.css" rel="stylesheet" />
    <link href="Scripts/jquery.messi/messi.min.css" rel="stylesheet" />
    <link href="Scripts/jquery.toastmessage/jquery.toastmessage-min.css" rel="stylesheet" />

    <script src="Scripts/jquery-1.8.2.min.js"></script>
    <script src="Scripts/jquery.toastmessage/jquery.toastmessage-min.js"></script>
    <script src="Scripts/jquery.messi/messi.min.js"></script>
    <script src="Scripts/jsview/JsRender/jsrender.min.js"></script>
    <script src="Scripts/jquery.cookie.js"></script>
    <script src="Scripts/common.js"></script>
    <script src="Js/Login.js"></script>
    
</head>
<script id="LoadingTemplate" type="text/x-jsrender">
    <table style="width: 100%;">
        <tr>
            <td style="text-align: right; width: 10%; min-width: 32px;">
                <img src="Images/login/loading.gif" width="32" height="32" alt="" />
            </td>
            <td style="text-align: center; font-size: 14px; color: {{:color}}">{{:msg}}</td>
            <td style="width: 10%"></td>
        </tr>
    </table>
</script>
<body onload="document.onkeydown()">
    <form id="form1" runat="server">
        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td bgcolor="#e5f6cf">&nbsp;</td>
            </tr>
            <tr>
                <td height="608" background="Images/login/login_03.gif">
                    <table width="862" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="266" background="Images/login/login_04.gif">
                                <table height="266" width="100%">
                                    <tr>
                                        <td class="auto-style1">&nbsp;</td>
                                        <td id="tdmsg" style="color: red; vertical-align: bottom; font-size: 12px;"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="95">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="424" height="95" background="Images/login/login_06.gif">&nbsp;</td>
                                        <td width="183" background="Images/login/login_07.gif">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="21%" height="30">
                                                        <div align="center"><span class="STYLE3">用户</span></div>
                                                    </td>
                                                    <td width="79%" height="30">

                                                        <asp:TextBox ID="txtName" name="textfield" Style="height: 18px; width: 130px; border: solid 1px #cadcb2; font-size: 12px; color: #81b432;" runat="server"></asp:TextBox><span style="vertical-align: bottom; color: red; font-size: 12px;" id="spusername"></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                        <div align="center"><span class="STYLE3">密码</span></div>
                                                    </td>
                                                    <td height="30">
                                                        <asp:TextBox ID="txtPwd" name="textfield2" TextMode="Password" Style="height: 18px; width: 130px; border: solid 1px #cadcb2; font-size: 12px; color: #81b432;" runat="server"></asp:TextBox><span id="sppwd" style="vertical-align: bottom; color: red; font-size: 12px;"></span>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">&nbsp;</td>
                                                    <td height="30">
                                                        <a id="login" class="login" onclick="login_Click();">登录</a>
                                                        <a id="reset" class="login reset" onclick="reset_Click();">重置</a>
                                                        <%--<asp:ImageButton ID="imgBtnLogin" ImageUrl="~/Images/login/dl_01.png" runat="server" />
                                                        <asp:ImageButton ID="imgBtnReset" ImageUrl="~/Images/login/dl_02.png" CssClass="denglu" runat="server"/>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="255" background="Images/login/login_08.gif">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="247" valign="top" background="Images/login/login_09.gif">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="22%" height="30">&nbsp;</td>
                                        <td width="56%">&nbsp;</td>
                                        <td width="22%">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td height="30">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="44%" height="20">&nbsp;</td>
                                                    <td width="56%" class="STYLE4">版本 2015V1.0 </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#a2d962">&nbsp;</td>
            </tr>
        </table>

        <map name="Map">
            <area shape="rect" coords="3,3,36,19" href="#">
            <area shape="rect" coords="40,3,78,18" href="#">
        </map>
    </form>
</body>
</html>

