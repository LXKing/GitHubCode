<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Authorization.aspx.cs" Inherits="ExamOnLine.Pages.ExaminationManagement.Authorization" %>
<%@ Register Src="~/Controls/TreePanel/MulSelectTreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>考试授权</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/4_ExaminationManagement/Authorization.js"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Hidden runat="server" ID="hidIsDept" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:Panel runat="server" Layout="AnchorLayout">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:TabStrip runat="server">
                                    <Items>
                                        <ext:Tab ActionItemID="gpnlDepartment" Text="部门授权" />
                                        <ext:Tab ActionItemID="gpnlPosition" Text="岗位授权" />
                                        <ext:Tab ActionItemID="gpnlUser" Text="用户授权" />
                                        <ext:Tab ActionItemID="gpnlReviwers" Text="评卷人授权" />
                                    </Items>
                                </ext:TabStrip>
                                <ext:ToolbarFill />
                                <ext:Button runat="server"
                                    Text="返回"
                                    BaseCls="false"
                                    Cls="button_goback">
                                    <Listeners>
                                        <Click Handler="goBack();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:GridPanel
                            ID="gpnlDepartment"
                            runat="server" AnchorHorizontal="60%" AnchorVertical="100%"
                            AutoScroll="true">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:ToolbarFill />
                                        <ext:Button runat="server"
                                            Text="授权部门"
                                            BaseCls="false"
                                            Cls="button_red_b">
                                            <DirectEvents>
                                                <Click OnEvent="GetDepartment_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button runat="server"
                                            Text="批量删除"
                                            BaseCls="false"
                                            Cls="button_red">
                                            <DirectEvents>
                                                <Click Before="return App.gpnlUser.getSelectionModel().selected.keys.length>0;" After="" Success="App.gpnlDepartment.getSelectionModel().deselectAll();" Complete="" Failure="" OnEvent="btnDeleteManyDept_Click">
                                                    <Confirmation Cancel="" BeforeConfirm="return App.gpnlDepartment.getSelectionModel().selected.keys.length>0;" Message="确定要删除选中的部门吗?" Title="询问" ConfirmRequest="true">
                                                    </Confirmation>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store runat="server" PageSize="20">
                                    <Proxy>
                                        <ext:PageProxy
                                            DirectFn="App.direct.BindDataDept">
                                        </ext:PageProxy>
                                    </Proxy>
                                    <Model>
                                        <ext:Model runat="server" IDProperty="ID">
                                            <Fields>
                                                <ext:ModelField Name="ID" Type="String" />
                                                <ext:ModelField Name="DEPARTMENT_NAME" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                                    <ext:Column runat="server" Text="部门授权(部门名称)" Align="Center" DataIndex="DEPARTMENT_NAME" Flex="1" />
                                    <ext:CommandColumn ID="CommandColumn_DeleteDept" runat="server" Text="删除" Align="Center" Width="80">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Delete" MinWidth="22" CommandName="DepartmentDelete">
                                                <ToolTip Text="删除" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <DirectEvents>
                                            <Command Before="showLoading('gpnlDepartment','正在删除中......');" Complete="hideLoading('gpnlDepartment');" OnEvent="CommandColumn_DeleteDept_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                </ExtraParams>
                                                <Confirmation Message="确定要删除该部门?" ConfirmRequest="true" Title="确认">
                                                </Confirmation>
                                            </Command>
                                        </DirectEvents>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:CheckboxSelectionModel runat="server" Mode="Multi" />
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar
                                    runat="server"
                                    DisplayInfo="true"
                                    DisplayMsg="数据显示: 第{0} - {1}条, 共 {2} 条"
                                    EmptyMsg="暂无数据" />
                            </BottomBar>
                        </ext:GridPanel>
                        <ext:GridPanel
                            ID="gpnlPosition"
                            runat="server" AnchorHorizontal="60%" AnchorVertical="100%"
                            AutoScroll="true">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:ToolbarFill />
                                        <ext:Button runat="server"
                                            Text="授权岗位"
                                            BaseCls="false"
                                            Cls="button_yellow_b">
                                            <DirectEvents>
                                                <Click OnEvent="GetPosition_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button runat="server"
                                            Text="批量删除"
                                            BaseCls="false"
                                            Cls="button_red">
                                            <DirectEvents>
                                                <Click Before="return App.gpnlUser.getSelectionModel().selected.keys.length>0;" After="" Success="App.gpnlPosition.getSelectionModel().deselectAll();" Complete="" Failure="" OnEvent="btnDeleteManyPos_Click">
                                                    <Confirmation Cancel="" BeforeConfirm="return App.gpnlPosition.getSelectionModel().selected.keys.length>0;" Message="确定要删除选中的岗位吗?" Title="询问" ConfirmRequest="true">
                                                    </Confirmation>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store runat="server" PageSize="20">
                                    <Proxy>
                                        <ext:PageProxy
                                            DirectFn="App.direct.BindDataPos">
                                        </ext:PageProxy>
                                    </Proxy>
                                    <Model>
                                        <ext:Model runat="server" IDProperty="ID">
                                            <Fields>
                                                <ext:ModelField Name="ID" Type="String" />
                                                <ext:ModelField Name="POSITION_NAME" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                                    <ext:Column runat="server" Text="岗位授权(岗位名称)" Align="Center" DataIndex="POSITION_NAME" Flex="1" />
                                    <ext:CommandColumn ID="CommandColumn_DeletePos" runat="server" Text="删除" Align="Center" Width="80">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Delete" MinWidth="22" CommandName="PositionDelete">
                                                <ToolTip Text="删除" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <DirectEvents>
                                            <Command Before="showLoading('gpnlPosition','正在删除中......');" Complete="hideLoading('gpnlPosition');" OnEvent="CommandColumn_DeletePos_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                </ExtraParams>
                                                <Confirmation Message="确定要删除该岗位?" ConfirmRequest="true" Title="确认">
                                                </Confirmation>
                                            </Command>
                                        </DirectEvents>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:CheckboxSelectionModel runat="server" Mode="Multi" />
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar
                                    runat="server"
                                    DisplayInfo="true"
                                    DisplayMsg="数据显示: 第{0} - {1}条, 共 {2} 条"
                                    EmptyMsg="暂无数据" />
                            </BottomBar>
                        </ext:GridPanel>
                        <ext:GridPanel
                            ID="gpnlUser"
                            runat="server" AnchorHorizontal="60%" AnchorVertical="100%"
                            AutoScroll="true">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:ToolbarFill />
                                        <ext:Button runat="server"
                                            Text="授权用户"
                                            BaseCls="false"
                                            Cls="button_green_b">
                                            <Listeners>
                                                <Click Handler="GetUser_Click();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server"
                                            Text="批量删除"
                                            BaseCls="false"
                                            Cls="button_red">
                                            <DirectEvents>
                                                <Click Before="return App.gpnlUser.getSelectionModel().selected.keys.length>0;" After="" Success="App.gpnlUser.getSelectionModel().deselectAll();" Complete="" Failure="" OnEvent="btnDeleteManyUser_Click">
                                                    <Confirmation Cancel="" BeforeConfirm="return App.gpnlUser.getSelectionModel().selected.keys.length>0;" Message="确定要删除选中的用户吗?" Title="询问" ConfirmRequest="true">
                                                    </Confirmation>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store runat="server" PageSize="20">
                                    <Proxy>
                                        <ext:PageProxy
                                            DirectFn="App.direct.BindDataUser">
                                        </ext:PageProxy>
                                    </Proxy>
                                    <Model>
                                        <ext:Model runat="server" IDProperty="ID">
                                            <Fields>
                                                <ext:ModelField Name="ID" Type="String" />
                                                <ext:ModelField Name="USER_NAME" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                                    <ext:Column runat="server" Text="用户授权(用户名)" Align="Center" DataIndex="USER_NAME" Flex="1" />
                                    <ext:CommandColumn ID="CommandColumn_DeleteUser" runat="server" Text="删除" Align="Center" Width="80">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>                                            
                                            <ext:GridCommand Icon="Delete" MinWidth="22" CommandName="UserDelete">
                                                <ToolTip Text="删除" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <DirectEvents>
                                            <Command Before="showLoading('gpnlUser','正在删除中......');" Complete="hideLoading('gpnlUser');" OnEvent="CommandColumn_DeleteUser_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                </ExtraParams>
                                                <Confirmation Message="确定要删除该用户?" ConfirmRequest="true" Title="确认">
                                                </Confirmation>
                                            </Command>
                                        </DirectEvents>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:CheckboxSelectionModel runat="server" Mode="Multi" />
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar
                                    runat="server"
                                    DisplayInfo="true"
                                    DisplayMsg="数据显示: 第{0} - {1}条, 共 {2} 条"
                                    EmptyMsg="暂无数据" />
                            </BottomBar>
                        </ext:GridPanel>
                        <ext:GridPanel
                            ID="gpnlReviwers"
                            runat="server" AnchorHorizontal="60%" AnchorVertical="100%"
                            AutoScroll="true">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:ToolbarFill />
                                        <ext:Button runat="server"
                                            Text="授权评卷人"
                                            BaseCls="false"
                                            Cls="button_blue_b">
                                            <Listeners>
                                                <Click Handler="GetReviwers_Click();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server"
                                            Text="批量删除"
                                            BaseCls="false"
                                            Cls="button_red">
                                            <DirectEvents>
                                                <Click Before="return App.gpnlReviwers.getSelectionModel().selected.keys.length>0;" After="" Success="App.gpnlReviwers.getSelectionModel().deselectAll();" Complete="" Failure="" OnEvent="btnDeleteManyReviwers_Click">
                                                    <Confirmation Cancel="" BeforeConfirm="return App.gpnlReviwers.getSelectionModel().selected.keys.length>0;" Message="确定要删除选中的评卷人吗?" Title="询问" ConfirmRequest="true">
                                                    </Confirmation>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store runat="server" PageSize="20">
                                    <Proxy>
                                        <ext:PageProxy
                                            DirectFn="App.direct.BindDataReviwers">
                                        </ext:PageProxy>
                                    </Proxy>
                                    <Model>
                                        <ext:Model runat="server" IDProperty="ID">
                                            <Fields>
                                                <ext:ModelField Name="ID" Type="String" />
                                                <ext:ModelField Name="USER_NAME" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                                    <ext:Column runat="server" Text="评卷人授权(用户名称)" Align="Center" DataIndex="USER_NAME" Flex="1" />
                                    <ext:CommandColumn runat="server" Text="删除" Align="Center" Width="80">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Delete" MinWidth="22" CommandName="ReviwersDelete">
                                                <ToolTip Text="删除" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <DirectEvents>
                                            <Command Before="" Complete="hideLoading('gpnlReviwers');" OnEvent="CommandColumn_DeleteReviwers_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                </ExtraParams>
                                                <Confirmation Message="确定要删除该评卷人?" ConfirmRequest="true" Title="确认">
                                                </Confirmation>
                                            </Command>
                                        </DirectEvents>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:CheckboxSelectionModel runat="server" Mode="Multi" />
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
                        <%--                        <ucExt:TreePanelExt runat="server" ID="tplPosition"
                            RootNodeID=""
                            RootNodeText ="根节点"
                            Hidden="true"
                            Modal="true"
                            Title="选择要授权的岗位"
                            RootNodeVisible="false"
                            OnSubmittedNode="tplPosition_SubmittedNode" />--%>
                    </Content>
                </ext:Panel>
            </Items>            
        </ext:Viewport>
        <ext:Window 
            ID="Window1" 
            runat="server"
            Height="605" 
            Width="820"
            BodyStyle="background-color: #fff;"
            BodyPadding="5"
            Modal="true" 
            Hidden="true" 
            Resizable="false">
            <Loader runat="server" Mode="Frame">
                <LoadMask ShowMask="true"></LoadMask>
            </Loader>
        </ext:Window>
    </form>
</body>
</html>
