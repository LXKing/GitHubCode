<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAuthorUser.aspx.cs" Inherits="ExamOnLine.Pages.ExaminationManagement.AddAuthorUser" %>

<%@ Register Src="~/Controls/TreePanel/MulSelectTreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/4_ExaminationManagement/AddAuthorUser.js?sdf"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Hidden runat="server" ID="hidIsDept" />
        <ext:Hidden runat="server" ID="hidDeptID" />
        <ext:Hidden runat="server" ID="hidPosID" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:GridPanel ID="GridPanel1" runat="server" AutoScroll="true">
                    <TopBar>
                        <ext:Toolbar runat="server" Height="80" ID="Toolbar_Search" Hidden="false" Layout="FormLayout">
                            <Items>
                                <ext:Container runat="server"
                                    Height="35"
                                    Layout="ColumnLayout"
                                    StyleSpec="margin-top:0px;">
                                    <Items>
                                        <ext:Label runat="server"
                                            Text="快速筛选"
                                            Icon="Magnifier"
                                            StyleSpec="margin-left:5px;margin-top:5px;">
                                        </ext:Label>
                                        <ext:ComboBox runat="server"
                                            ID="cmbRole"
                                            FieldLabel="角色"
                                            LabelAlign="Right"
                                            DisplayField="NAME"
                                            ValueField="ID"
                                            LabelWidth="50"
                                            Width="200"
                                            AutoSelect="true"
                                            EmptyText="角色"
                                            StyleSpec="margin-top:5px;"
                                            Editable="false">
                                            <Items>
                                                <ext:ListItem Index="0" Text="常规管理员" Value="378FC382-2A60-5623-8379-E604A82C891F"></ext:ListItem>
                                                <ext:ListItem Index="1" Text="监考老师" Value="3B62B902-14A2-F924-A867-07FA78D5AFC6"></ext:ListItem>
                                                <ext:ListItem Index="2" Text="考生" Value="7C10F51C-3011-B4E1-A786-C7747B3AA111"></ext:ListItem>
                                                <ext:ListItem Index="3" Text="系统管理员" Value="9653EEBE-26DA-53A7-BC43-6FDDA23C50DE"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtDept"
                                            runat="server"
                                            LabelWidth="80"
                                            LabelAlign="Right"
                                            FieldLabel="部门"
                                            Width="250"
                                            ReadOnly="true"
                                            IndicatorIcon="Zoom"
                                            StyleSpec="margin-top:5px;">
                                            <DirectEvents>
                                                <IndicatorIconClick OnEvent="GetDepartment_Click"></IndicatorIconClick>
                                            </DirectEvents>
                                        </ext:TextField>
                                        <ext:TextField ID="txtPos"
                                            runat="server"
                                            LabelWidth="80"
                                            LabelAlign="Right"
                                            FieldLabel="岗位"
                                            Width="250"
                                            ReadOnly="true"
                                            IndicatorIcon="Zoom"
                                            StyleSpec="margin-top:5px;">
                                            <DirectEvents>
                                                <IndicatorIconClick OnEvent="GetPosition_Click"></IndicatorIconClick>
                                            </DirectEvents>
                                        </ext:TextField>
                                    </Items>
                                </ext:Container>

                                <ext:Container runat="server"
                                    Height="35"
                                    Layout="ColumnLayout"
                                    StyleSpec="margin-top:0px;">
                                    <Items>
                                        <ext:TextField ID="txtName"
                                            runat="server"
                                            LabelWidth="100"
                                            LabelAlign="Right"
                                            FieldLabel="用户名、姓名"
                                            Width="500"
                                            StyleSpec="margin-top:5px;">
                                        </ext:TextField>
                                        <ext:Button runat="server" Text="搜索"
                                            StyleSpec="margin-left:5px;margin-top:5px;"
                                            BaseCls="false"
                                            Cls="button_showsearch">
                                            <Listeners>
                                                <Click Handler="btnSearch_Click();"></Click>
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server" Text="确认选择"
                                            StyleSpec="margin-left:5px;margin-top:5px;"
                                            BaseCls="false"
                                            Cls="button_blue">
                                            <DirectEvents>
                                                <Click Before="" After="" Success="App.GridPanel1.getSelectionModel().deselectAll();" Complete="btnAuthorManyUser();" Failure="" OnEvent="btnAuthorManyUser_Click">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Store>
                        <ext:Store runat="server" PageSize="20">
                            <Proxy>
                                <ext:PageProxy
                                    DirectFn="App.direct.BindData">
                                </ext:PageProxy>
                            </Proxy>
                            <Model>
                                <ext:Model runat="server" IDProperty="ID">
                                    <Fields>
                                        <ext:ModelField Name="ID" Type="Object" />
                                        <ext:ModelField Name="LOGIN_NAME" Type="String" />
                                        <ext:ModelField Name="USER_NAME" Type="String" />
                                        <ext:ModelField Name="T_DEPARTMENT.DEPARTMENT_NAME" Type="String" />
                                        <ext:ModelField Name="T_POSITION.POSITION_NAME" Type="String" />
                                        <ext:ModelField Name="T_ROLE.ROLE_NAME" Type="String" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:Column ID="Column_ID" runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                            <ext:Column ID="Column_KNOWLEDGE_NAME" runat="server" Text="用户名" Align="Center" DataIndex="LOGIN_NAME" Flex="1" />
                            <ext:Column ID="Column_QUESTION_TYPE_NAME" runat="server" Text="姓名" Align="Center" DataIndex="USER_NAME" Flex="1" />
                            <ext:Column ID="Column_DIFFICULTY" runat="server" Text="部门" Align="Center" DataIndex="T_DEPARTMENT.DEPARTMENT_NAME" Flex="1" />
                            <ext:Column ID="Column_SHOW_IN_PRACTICE" runat="server" Text="岗位" Align="Center" DataIndex="T_POSITION.POSITION_NAME" Flex="1" />
                            <ext:Column ID="Column_CREATE_DATE" runat="server" Text="角色" Align="Center" DataIndex="T_ROLE.ROLE_NAME" Flex="1" />

                            <ext:CommandColumn ID="CommandColumn_Delete" runat="server" Text="选择" Align="Center" Flex="1">
                                <Commands>
                                    <ext:CommandFill></ext:CommandFill>
                                    <ext:GridCommand Icon="Application" CommandName="questionDelete" MinWidth="22">
                                        <ToolTip Text="选择" />
                                    </ext:GridCommand>
                                    <ext:CommandFill></ext:CommandFill>
                                </Commands>
                                <DirectEvents>
                                    <Command></Command>
                                </DirectEvents>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel runat="server" Mode="Simple" IgnoreRightMouseSelection="true" />
                    </SelectionModel>
                    <BottomBar>
                        <ext:PagingToolbar
                            runat="server"
                            DisplayInfo="true"
                            DisplayMsg="数据显示: 第{0} - {1}条, 共 {2} 条"
                            EmptyMsg="暂无数据" />
                    </BottomBar>
                </ext:GridPanel>
            </Items>
            <Content>
                <ucExt:TreePanelExt runat="server" ID="tplDepartment"
                    RootNodeID=""
                    RootNodeText="根节点"
                    Hidden="true"
                    Modal="true"
                    RootNodeVisible="false"
                    OnSubmittedNode="tplDepartment_SubmittedNode" />
            </Content>
        </ext:Viewport>
    </form>
</body>
</html>
