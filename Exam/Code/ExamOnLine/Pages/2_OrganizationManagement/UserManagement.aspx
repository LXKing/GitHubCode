<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="ExamOnLine.Pages.OrganizationManagement.UserManagement" %>
<%@ Register Src="~/Controls/TreePanel/TreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户管理</title>

    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/2_OrganizationManagement/UserManagement.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden runat="server" ID="hidden_TreeType"></ext:Hidden>
        <ext:Hidden runat="server" ID="hidden_Department"></ext:Hidden>
        <ext:Hidden runat="server" ID="hidden_Position"></ext:Hidden>
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:Panel 
                    runat="server"
                    Layout="FitLayout"
                    Title="▏用户管理"
                    Region="Center"
                    AutoScroll="true"
                    Header="true"
                    >
                    <TopBar>
                        <ext:Toolbar runat="server" AnchorHorizontal="100" Height="30">
                            <Items>
                                <ext:ToolbarFill ID="ToolBarFill" />
                                <ext:Button runat="server"
                                    ID="btnShowQuery"
                                    Icon="TableRefresh"
                                    Text="隐藏查询"
                                    Width="90"
                                    Height="22"
                                    StandOut="true"
                                    ArrowAlign="Right"
                                    StyleSpec="margin-right:15px;"
                                    >
                                    <Listeners>
                                        <Click Handler="btnShowQueryUI_Click(this);"></Click>
                                    </Listeners>
                                    <%--<DirectEvents>
                                        <Click Before="" After="" Success="" Failure="" Complete="" OnEvent="btnRefreshUser_Click"></Click>
                                    </DirectEvents>--%>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="btnAddUser"
                                    Icon="TableRefresh"
                                    Text="添加用户"
                                    Width="90"
                                    Height="22"
                                    StandOut="true"
                                    ArrowAlign="Right"
                                    StyleSpec="margin-right:15px;"
                                    >
                                    <Listeners>
                                        <Click Handler="btnAddUser_Click();"></Click>
                                    </Listeners>
                                    <%--<DirectEvents>
                                        <Click Before="" After="" Success="" Failure="" Complete="" OnEvent="btnRefreshUser_Click"></Click>
                                    </DirectEvents>--%>
                                </ext:Button>
                                <%--<ext:Button runat="server"
                                    ID="btnExportUsers"
                                    Icon="TableRefresh"
                                    Text="导出数据"
                                    Width="90"
                                    Height="22"
                                    StandOut="true"
                                    ArrowAlign="Right"
                                    StyleSpec="margin-right:15px;"
                                    >
                                    <Listeners>
                                        <Click Handler="btnExportUsers_Click();"></Click>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click Before="" After="" Success="" Failure="" Complete="" OnEvent="btnRefreshUser_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>--%>
                                <%--<ext:Button runat="server"
                                    ID="btnDownloadUsers"
                                    Icon="TableRefresh"
                                    Text="下载数据"
                                    Width="90"
                                    Height="22"
                                    StandOut="true"
                                    ArrowAlign="Right"
                                    StyleSpec="margin-right:15px;"
                                    >
                                    <Listeners>
                                        <Click Handler="btnDownloadUsers_Click();"></Click>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click Before="" After="" Success="" Failure="" Complete="" OnEvent="btnRefreshUser_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>--%>
                                <ext:Button runat="server"
                                    ID="btnImportUsers"
                                    Icon="TableRefresh"
                                    Text="导入用户"
                                    Width="90"
                                    Height="22"
                                    StandOut="true"
                                    ArrowAlign="Right"
                                    StyleSpec="margin-right:15px;"
                                    >
                                    <Listeners>
                                        <Click Handler="btnImportUsers_Click();"></Click>
                                    </Listeners>
                                    <%--<DirectEvents>
                                        <Click Before="" After="" Success="" Failure="" Complete="" OnEvent="btnRefreshUser_Click"></Click>
                                    </DirectEvents>--%>
                                </ext:Button>
                                <%--<ext:Button runat="server"
                                    ID="btnDeleteMany"
                                    Icon="TableRefresh"
                                    Text="批量删除"
                                    Width="90"
                                    Height="22"
                                    StandOut="true"
                                    ArrowAlign="Right"
                                    StyleSpec="margin-right:15px;"
                                    >
                                    <Listeners>
                                        <Click Handler="btnDeleteMany_Click();"></Click>
                                    </Listeners>
                                    <DirectEvents>
                                        <Click Before="" After="" Success="" Failure="" Complete="" OnEvent="btnRefreshUser_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>--%>
                                <ext:Button runat="server"
                                    ID="btnShowError"
                                    Icon="Find"
                                    Text="错误信息"
                                    Width="90"
                                    Height="22"
                                    StandOut="true"
                                    ArrowAlign="Right"
                                    StyleSpec="margin-right:15px;"
                                    >

                                    <Listeners>
                                        <Click Handler="btnShowError_Click();"></Click>
                                    </Listeners>
                                    <%--<DirectEvents>
                                        <Click Before="" After="" Success="" Failure="" Complete="" OnEvent=""></Click>
                                    </DirectEvents>--%>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:GridPanel
                            ID="GridPanel1"
                            runat="server"
                            Title="用户管理"
                            TitleAlign="Left"
                            Header="false"
                            DisableSelection="true"
                            AnchorHorizontal="100"
                            AutoScroll="true"
                            >
                            <TopBar>
                                <ext:Toolbar runat="server" AnchorHorizontal="100" Height="70" ID="Toolbar_Search" Hidden="false" Layout="AnchorLayout">
                                    <Items>
                                        <ext:Container runat="server" 
                                            Height="33"
                                            Layout="ColumnLayout"
                                            StyleSpec="">
                                            <Items>
                                                <ext:Label runat="server" 
                                                    Text="快速筛选" 
                                                    Icon="Magnifier"
                                                    StyleSpec="margin-top:5px;
                                                    margin-left:5px;"
                                                    ></ext:Label>
                                                <ext:ComboBox  runat="server" 
                                                    ID="cmbRole" 
                                                    FieldLabel="角色" 
                                                    LabelAlign="Right" 
                                                    DisplayField="ROLE_NAME" 
                                                    ValueField="ID" 
                                                    LabelWidth="50"
                                                    StyleSpec="margin:5px 0px 0px 5px;"
                                                    Width="200"
                                                    MultiSelect="true"
                                                    QueryMode="Local"
                                                    >
                                                    <Store>
                                                        <ext:Store ID="Store4" runat="server">
                                                            <Model>
                                                                <ext:Model ID="Model3" runat="server" >
                                                                    <Fields>
                                                                        <ext:ModelField Name="ID" />
                                                                        <ext:ModelField Name="ROLE_NAME" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                </ext:ComboBox>
                                                <ext:TextField ID="txtDepartment"
                                                    runat="server"
                                                    LabelWidth="50"
                                                    LabelAlign="Right"
                                                    FieldLabel="部门"
                                                    Width="200"
                                                    ReadOnly="true"
                                                    IndicatorIcon="Zoom"
                                                    StyleSpec="margin:5px 0px 0px 5px;"
                                                    >
                                                    <DirectEvents>
                                                        <IndicatorIconClick Before="" After="" Success="" Failure="" Complete="" OnEvent="txtDepartment_IndicatorIconClick"></IndicatorIconClick>
                                                    </DirectEvents>
                                                </ext:TextField>
                                                <ext:TextField ID="txtPosition"
                                                    runat="server"
                                                    LabelWidth="50"
                                                    LabelAlign="Right"
                                                    FieldLabel="岗位"
                                                    Width="200"
                                                    ReadOnly="true"
                                                    IndicatorIcon="Zoom"
                                                    StyleSpec="margin:5px 0px 0px 5px;"
                                                    >
                                                    <DirectEvents>
                                                        <IndicatorIconClick Before="" After="" Success="" Failure="" Complete="" OnEvent="txtPosition_IndicatorIconClick"></IndicatorIconClick>
                                                    </DirectEvents>
                                                </ext:TextField>
                                                
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" 
                                            Height="33"
                                            Layout="ColumnLayout"
                                            StyleSpec="">
                                            <Items>
                                                <ext:Label runat="server" 
                                                    Text="快速筛选" 
                                                    Icon="Magnifier"
                                                    StyleSpec="margin-top:5px;
                                                    margin-left:5px;">
                                                </ext:Label>
                                                <ext:ComboBox runat="server" 
                                                    ID="cmbSex" 
                                                    FieldLabel="性别" 
                                                    LabelAlign="Right" 
                                                    DisplayField="NAME" 
                                                    ValueField="ID" 
                                                    LabelWidth="50"
                                                    StyleSpec="margin:5px 0px 0px 5px;"
                                                    Width="110"
                                                    AutoSelect="true"
                                                    EmptyText="性别"
                                                    >
                                                    <Items>
                                                        <ext:ListItem Index="0" Text="男" Value="1"></ext:ListItem>
                                                        <ext:ListItem Index="1" Text="女" Value="0"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                                <ext:TextField ID="txtName"
                                                    runat="server"
                                                    LabelWidth="140"
                                                    LabelAlign="Right"
                                                    FieldLabel="登录名、用户名"
                                                    Width="272"
                                                    MaxLength="30"
                                                    StyleSpec="margin:5px 0px 0px 5px;"
                                                    >
                                                </ext:TextField>
                                                <ext:ImageButton runat="server"
                                                    ID="btnSearch"
                                                    Align="AbsBottom"
                                                    Width="127"
                                                    Height="22"
                                                    ImageUrl="../../Images/Pages/1_SystemManagement/UserOnLineManagement/search1.png"
                                                    OverImageUrl="../../Images/Pages/1_SystemManagement/UserOnLineManagement/search2.png"
                                                    PressedImageUrl="../../Images/Pages/1_SystemManagement/UserOnLineManagement/search1.png"
                                                    StyleSpec="margin:5px 0px 0px 78px;background-color: #ed6d48;"
                                                    >
                                                    <%--<DirectEvents>
                                                        <Click Before="searchBefore();" After="" Success="" Failure="" Complete="searchComplete();" OnEvent="btnSearch_Click"></Click>
                                                    </DirectEvents>--%>
                                                    <Listeners>
                                                        <Click Handler="btnSearch_Click();"></Click>
                                                    </Listeners>
                                                </ext:ImageButton>
                                                <ext:Button runat="server"
                                                    ID="btnClearCodition"
                                                    Text="清空条件"
                                                    Height="22"
                                                    Width="100"
                                                    StyleSpec="margin:5px 0px 0px 78px;"
                                                    >
                                                    <Listeners>
                                                        <Click Handler="btbClearCodition_Click();"></Click>
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store_GridPanel1" runat="server" PageSize="20">
                                    <Proxy>
                                        <ext:PageProxy
                                            DirectFn="App.direct.BindData">
                                        </ext:PageProxy>
                                    </Proxy>
                                    
                                    <Model>
                                        <ext:Model runat="server" IDProperty="ID">
                                            <Fields>
                                                <ext:ModelField Name="ID" Type="Object"/>
                                                <ext:ModelField Name="LOGIN_NAME" Type="String"/>
                                                <ext:ModelField Name="USER_NAME" Type="String" />
                                                <ext:ModelField Name="SEX" Type="String" />
                                                <ext:ModelField Name="DEPARTMENT_NAME" Type="String" />
                                                <ext:ModelField Name="POSITION_NAME" Type="String"/>
                                                <ext:ModelField Name="ROLE_NAME" Type="String"/>
                                                <ext:ModelField Name="USER_STATUS" Type="Int"/>
                                            </Fields>
                                        </ext:Model>
                                    </Model>

                                    <Listeners>
                                        <Refresh Handler="Refresh(this);"></Refresh>
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column ID="Column_ID"              runat="server" Text="ID"     Width="50" Align="Center" DataIndex="ID" Flex="1" Visible="false" />
                                    <ext:Column ID="Column_LOGIN_NAME"      runat="server" Text="登录名" Width="50" Align="Center" DataIndex="LOGIN_NAME" Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_USER_NAME"       runat="server" Text="用户名" Width="50" Align="Center" DataIndex="USER_NAME" Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_SEX"             runat="server" Text="性别"   Align="Center" DataIndex="SEX" Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_DEPARTMENT_NAME" runat="server" Text="部门"   Align="Center" DataIndex="DEPARTMENT_NAME" Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_POSITION_NAME"   runat="server" Text="岗位"   Align="Center" DataIndex="POSITION_NAME" Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_ROLE_NAME"       runat="server" Text="角色"   Align="Center" DataIndex="ROLE_NAME"    Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_USER_STATUS"     runat="server" Text="状态"   Align="Center" DataIndex="USER_STATUS"  Visible="true" Width="80">
                                        <Renderer Fn="changeSelect"></Renderer>
                                    </ext:Column>

                                    <ext:CommandColumn ID="CommandColumn_Change_Stutas" runat="server" Width="100" Text="禁用/启用" Align="Center">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Delete" CommandName="0" MinWidth="22">
                                                <ToolTip Text="禁用" />
                                            </ext:GridCommand>
                                            <ext:GridCommand Icon="Accept" CommandName="1" MinWidth="22">
                                                <ToolTip Text="启用" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="CommandColumn_StatusChage_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.USER_STATUS" Name="status" />
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="command" Name="value" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:CommandColumn>

                                    <ext:CommandColumn ID="CommandColumn_View" runat="server" Width="100" Text="查看" Align="Center">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Application" CommandName="viewUser" MinWidth="22">
                                                <ToolTip Text="查看" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="CommandColumn_View_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                        <%--<DirectEvents>
                                            <Command Before="return CommandColumn_ForcedOffline_Click(item, command, record, recordIndex, cellIndex);" After="" Success="App.GridPanel1.store.load();" Failure="" Complete="hideLoading('Viewport1');" OnEvent="CommandColumn_View_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="ID" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>--%>
                                    </ext:CommandColumn>

                                    <ext:CommandColumn ID="CommandColumn_Edit" runat="server" Width="100" Text="修改" Align="Center">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="ApplicationEdit" CommandName="userEditCommand" MinWidth="22">
                                                <ToolTip Text="修改" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="CommandColumn_Edit_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:CommandColumn>

                                </Columns>
                            </ColumnModel>

                            <Plugins>
                                <ext:FilterHeader runat="server" OnCreateFilterableField="OnCreateFilterableField"/>
                            </Plugins>
                            <SelectionModel>
                                <ext:RowSelectionModel runat="server" Mode="Simple"  IgnoreRightMouseSelection="true"/>
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
                </ext:Panel>
                <ext:Container runat="server">
                    <Items>
                        <ext:Window 
                            runat="server"
                            ID="change_Status"
                            Width="250"
                            Height="150"
                            Hidden="true"
                            Modal="true"
                            Layout="FormLayout"
                            >
                            <Items>
                                <ext:ComboBox runat="server" 
                                    FieldLabel="状态" 
                                    LabelAlign="Right" 
                                    LabelWidth="70" 
                                    Width="200" 
                                    StyleSpec="margin-top:20px;">
                                    <Items>
                                        <ext:ListItem Index="0" Mode="Value" Text="禁用" Value="0"></ext:ListItem>
                                        <ext:ListItem Index="1" Mode="Value" Text="启用" Value="1"></ext:ListItem>
                                    </Items>
                                </ext:ComboBox>
                                <ext:ButtonGroup runat="server" Layout="ColumnLayout" ButtonAlign="Center" StyleSpec="margin-bottom:0px;margin-top:33px;">
                                    <Buttons>
                                        <ext:Button runat="server" Text="确定" Width="80"></ext:Button>
                                        <ext:Button runat="server" Text="取消" Width="80"></ext:Button>
                                    </Buttons>
                                </ext:ButtonGroup>
                            </Items>
                        </ext:Window>
                    </Items>
                </ext:Container>
                <ext:Container runat="server">
                    <Content>
                        <ucExt:TreePanelExt runat="server" 
                            ID="treePanel"
                            Hidden="true"
                            Modal="true"
                            RootNodeID=""
                            RootNodeText="根节点"
                            Title=""
                            RootNodeVisible="false"
                            OnNodeClick="treePanel_NodeClick"
                             />
                    </Content>
                </ext:Container>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
