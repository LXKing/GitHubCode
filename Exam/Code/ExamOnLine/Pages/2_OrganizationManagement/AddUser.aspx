<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="ExamOnLine.Pages.OrganizationManagement.AddUser" %>
<%@ Register Src="~/Controls/TreePanel/TreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户信息管理</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
    <script src="../../Js/2_OrganizationManagement/AddUser.js"></script>
    <script>

    </script>
</head>
<body>
    <form id="form1" method="post" enctype="multipart/form-data" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Hidden ID="hidIsAdd" runat="server" />
        <ext:Hidden ID="hidIsDepartment" runat="server" />
        <ext:Hidden ID="hidUserID" runat="server" />
        <ext:Hidden ID="hidDeptID" runat="server" />
        <ext:Hidden ID="hidPosID" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="formPanel" runat="server" AnchorHeight="100" AnchorHorizontal="100">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ToolbarFill runat="server" />
                                <ext:Button runat="server" Text="返回" BaseCls="false" Cls="button_goback">
                                    <Listeners>
                                        <Click Handler="goBack();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Defaults>
                        <ext:Parameter Name="style" Value="margin: 5px 0px 0px 0px;" Mode="Value" />
                    </Defaults>
                    <Items>
                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Items>
                                <ext:TextField ID="txtLoginName" runat="server" FieldLabel="用户名" AllowBlank="false" MaxLength="30" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                <ext:Button runat="server" Text="检测用户" StyleSpec="margin-left:5px;" BaseCls="false" Cls="button_yellow">
                                    <DirectEvents>
                                        <Click OnEvent="CheckLoginName_Click" />
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Container>
                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Items>
                                <ext:TextField ID="txtPwd" runat="server" InputType="Password" FieldLabel="密码" AllowBlank="false" Vtype="alphanum" MinLength="6" MaxLength="32" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                <ext:SelectBox ID="sltStatus" runat="server" FieldLabel="状态" LabelAlign="Right" LabelStyle="color:#417ac1;">
                                    <Items>
                                        <ext:ListItem Text="启用" Value="1" />
                                        <ext:ListItem Text="禁用" Value="0" />
                                    </Items>
                                </ext:SelectBox>
                            </Items>
                        </ext:Container>
                        
                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Items>
                                <ext:TextField ID="txtUserName" runat="server" FieldLabel="姓名" MaxLength="30" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                <ext:SelectBox ID="sltGender" runat="server" FieldLabel="姓别" LabelAlign="Right" LabelStyle="color:#417ac1;">
                                    <Items>
                                        <ext:ListItem Text="男" Value="1" />
                                        <ext:ListItem Text="女" Value="0" />
                                    </Items>
                                </ext:SelectBox>
                            </Items>
                        </ext:Container>
                        <%--                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Items>
                                <ext:TextField ID="txtEnglishName" runat="server" FieldLabel="英文名" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                <ext:TextField ID="txtNickName" runat="server" FieldLabel="昵称" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                            </Items>
                        </ext:Container>--%>
                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Items>
                                <ext:TextField ID="txtDeptName" runat="server" FieldLabel="部门" AllowBlank="false" IndicatorIcon="Zoom" ReadOnly="true" Width="368" LabelAlign="Right" LabelStyle="color:#417ac1;">
                                    <DirectEvents>
                                        <IndicatorIconClick OnEvent="GetDept_Click"></IndicatorIconClick>
                                    </DirectEvents>
                                </ext:TextField>
                                <ext:TextField ID="txtPosName" runat="server" FieldLabel="岗位" AllowBlank="false" IndicatorIcon="Zoom" ReadOnly="true" Width="350" LabelWidth="82" LabelAlign="Right" LabelStyle="color:#417ac1;">
                                    <DirectEvents>
                                        <IndicatorIconClick OnEvent="GetPos_Click"></IndicatorIconClick>
                                    </DirectEvents>
                                </ext:TextField>
                            </Items>
                        </ext:Container>
                        <ext:RadioGroup ID="rdoGpRole" runat="server" FieldLabel="角色" LabelAlign="Right" LabelStyle="color:#417ac1;" Layout="ColumnLayout">
                            <Items>
                                <ext:Radio ID="rdoStudent" runat="server" BoxLabel="考生" Checked="true" InputValue="7C10F51C-3011-B4E1-A786-C7747B3AA111" />
                                <ext:Radio ID="rdoSysManager" runat="server" BoxLabel="系统管理员" InputValue="9653EEBE-26DA-53A7-BC43-6FDDA23C50DE" />
                                <ext:Radio ID="rdoTeacher" runat="server" BoxLabel="监考老师" InputValue="3B62B902-14A2-F924-A867-07FA78D5AFC6" />
                                <ext:Radio ID="rdoManager" runat="server" BoxLabel="常规管理员" InputValue="378FC382-2A60-5623-8379-E604A82C891F" />
                            </Items>
                        </ext:RadioGroup>
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
<%--                                    <Items>
                                        <ext:ListItem Text="小学以下" Value="m" />
                                        <ext:ListItem Text="初中" Value="m" />
                                        <ext:ListItem Text="高中" Value="m" />
                                        <ext:ListItem Text="中专" Value="m" />
                                        <ext:ListItem Text="大专" Value="m" />
                                        <ext:ListItem Text="学士" Value="m" />
                                        <ext:ListItem Text="硕士" Value="m" />
                                        <ext:ListItem Text="博士" Value="m" />
                                        <ext:ListItem Text="博士后" Value="m" />
                                    </Items>--%>
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
                                <ext:Label runat="server" Text="当前照片:" Width="55" StyleSpec="margin-left:47px;color:#417ac1;" />
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
                                <ext:Button runat="server" Text="返回" BaseCls="false" Cls="button_goback">
                                    <Listeners>
                                        <Click Handler="goBack();" />
                                    </Listeners>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </Items>
                    <Content>
                        <ucExt:TreePanelExt runat="server" ID="trpnlExt"
                            Hidden="true"
                            Modal="true"
                            RootNodeID=""
                            RootNodeVisible="false" 
                            OnNodeClick="trpnlExt_NodeClick"
                            Title="" />
                    </Content>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
