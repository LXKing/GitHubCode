<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="ExamOnLine.Pages.MyAccount.UserInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户资料管理</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
    <script src="../../Js/6_MyAccount/UserInfo.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="formPanel" runat="server">
                    <Defaults>
                        <ext:Parameter Name="style" Value="margin: 5px 0px 0px 0px;" Mode="Value" />
                    </Defaults>
                    <Items>
                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Items>
                                <ext:TextField ID="txtLoginName" runat="server" FieldLabel="用户名" ReadOnly="true" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                <ext:TextField ID="txtDeptName" runat="server" FieldLabel="部门" ReadOnly="true" Width="368" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                            </Items>
                        </ext:Container>
                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Items>
                                <ext:TextField ID="txtRole" runat="server" FieldLabel="角色" ReadOnly="true" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                <ext:TextField ID="txtPosName" runat="server" FieldLabel="岗位" ReadOnly="true" Width="368" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                            </Items>
                        </ext:Container>                        
                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Items>
                                <ext:TextField ID="txtUserName" runat="server" FieldLabel="姓名" AllowBlank="false" MaxLength="30" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                <ext:SelectBox ID="sltGender" runat="server" FieldLabel="姓别" LabelAlign="Right" LabelStyle="color:#417ac1;">
                                    <Items>
                                        <ext:ListItem Text="男" Value="1" />
                                        <ext:ListItem Text="女" Value="0" />
                                    </Items>
                                </ext:SelectBox>
                            </Items>
                        </ext:Container>
                        <ext:TextField ID="txtIDCard" runat="server" FieldLabel="身份证号" Vtype="idcard" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Items>
                                <ext:TextField ID="txtTel" runat="server" FieldLabel="联系电话" Vtype="phone" MaxLength="11" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                <ext:TextField ID="txtMobile" runat="server" FieldLabel="手机" Vtype="mobile" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                            </Items>
                        </ext:Container>
                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Items>
                                <ext:TextField ID="txtContact" runat="server" FieldLabel="其它联系" MaxLength="100" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                <ext:SelectBox ID="sltDegree" runat="server" FieldLabel="学历" LabelAlign="Right" LabelStyle="color:#417ac1;" DisplayField="DEGREE_NAME" ValueField="ID">
                                    <Store>
                                        <ext:Store runat="server">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ID" />
                                                        <ext:ModelField Name="DEGREE_NAME" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>  
                                </ext:SelectBox>
                            </Items>
                        </ext:Container>
                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Items>
                                <ext:TextField ID="txtAddress" runat="server" FieldLabel="联系地址" MaxLength="100" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                <ext:TextField ID="txtPostCode" runat="server" FieldLabel="邮编" Vtype="postcode" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                            </Items>
                        </ext:Container>                        
                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Items>
                                <ext:Label runat="server" Text="当前照片:" Width="60" StyleSpec="margin-left:45px;color:#417ac1;" />
                                <ext:Image ID="imgPreView" runat="server" StyleSpec="border: rgb(250, 250, 250);  border-width: 1px;  border-style: groove;" ImageUrl="../../Images/NoImage.png" Width="100" Height="120" />
                            </Items>
                        </ext:Container>
                        <ext:Container runat="server" StyleSpec="margin:7px 0px 0px 105px;">
                            <Content>
                                <input runat="server" type="file" id="FileUploadField1" onchange="viewImage();" />
                                <a onclick="resetImage();" href="#">重置照片</a>
                            </Content>
                        </ext:Container>
                        <ext:Panel runat="server" Border="false" StyleSpec="margin-left:100px;" ButtonAlign="Left">
                            <Buttons>
                                <ext:Button ID="btnSave" runat="server" Text="保存" BaseCls="false" Cls="button_infosave">
                                    <DirectEvents>
                                        <Click Before="return #{formPanel}.getForm().isValid();" OnEvent="btnSave_Click"/>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
