<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserOnLineManagement.aspx.cs" Inherits="ExamOnLine.Pages.SystemManagement.UserOnLineManagement" %>

<%@ Register Assembly="Ext.Extension" Namespace="Ext.Extension.TreePanelEx" TagPrefix="ucExt" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>在线用户管理</title>
    <link href="../../Css/Pages/1_SystemManagement/UserOnLineManagement.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/1_SystemManagement/UserOnLineManagement.js"></script>
</head>
<body>
    <form runat="server">
        <ext:Hidden runat="server" ID="hidden_TreeType"></ext:Hidden>
        <ext:Hidden runat="server" ID="hidden_Department"></ext:Hidden>
        <ext:Hidden runat="server" ID="hidden_Position"></ext:Hidden>
        <ext:ResourceManager runat="server" ID="ResourceManager1"/>
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:Panel runat="server" 
                    Layout="FitLayout"
                    Title="▏在线用户管理"
                    Region="Center"
                    AutoScroll="true"
                    Header="true"
                    >
                    <TopBar>
                        <ext:Toolbar runat="server" AnchorHorizontal="100" Height="30" Border="false">
                            <Items>
                                <ext:ToolbarFill />
                                <ext:Button runat="server"
                                    ID="btnRefreshUser"
                                    Icon="TableRefresh"
                                    Text="刷新人数"
                                    Width="90"
                                    Height="22"
                                    StandOut="true"
                                    ArrowAlign="Right"
                                    StyleSpec="margin-right:15px;"
                                    >
                                    <Listeners>
                                        <Click Handler="btnRefreshUser_Click();"></Click>
                                    </Listeners>
                                    <%--<DirectEvents>
                                        <Click Before="" After="" Success="" Failure="" Complete="" OnEvent="btnRefreshUser_Click"></Click>
                                    </DirectEvents>--%>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="btnShowQueryUI"
                                    Icon="Find"
                                    Text="显示查询"
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
                            Title="在线用户管理"
                            TitleAlign="Left"
                            Header="false"
                            DisableSelection="true"
                            AnchorHorizontal="100"
                            AutoScroll="true"
                            >
                            <TopBar>
                                <ext:Toolbar runat="server" AnchorHorizontal="100" ID="Toolbar_Search" Hidden="false" Layout="AnchorLayout">
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
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column ID="Column_ID"              runat="server" Text="ID"      Align="Center" DataIndex="ID" Flex="1" Visible="false" />
                                    <ext:Column ID="Column_LOGIN_NAME"      runat="server" Text="登录名"  Align="Center" DataIndex="LOGIN_NAME" Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_USER_NAME"       runat="server" Text="用户名"  Align="Center" DataIndex="USER_NAME" Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_SEX"             runat="server" Text="性别"   Align="Center" DataIndex="SEX" Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_DEPARTMENT_NAME" runat="server" Text="部门"   Align="Center" DataIndex="DEPARTMENT_NAME" Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_POSITION_NAME"   runat="server" Text="岗位"   Align="Center" DataIndex="POSITION_NAME" Flex="1" Visible="true"/>
                                    <ext:Column ID="ROLE_NAME"              runat="server" Text="角色"     Align="Center" DataIndex="ROLE_NAME"    Flex="1" Visible="true"/>
                                    <ext:CommandColumn ID="CommandColumn_ForcedOffline" runat="server" Text="强制下线" Align="Center" Flex="1" Visible="true">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Delete" CommandName="offlineCommand" MinWidth="22" Text="下线">
                                                <ToolTip Text="强制下线" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <DirectEvents>
                                            <Command Before="return CommandColumn_ForcedOffline_Click(item, command, record, recordIndex, cellIndex);" After="" Success="App.GridPanel1.store.load();" Failure="" Complete="hideLoading('Viewport1');" OnEvent="CommandColumn_ForcedOffline_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="ID" />
                                                </ExtraParams>
                                                <Confirmation Message="确定要强制下线该用户？" ConfirmRequest="true" Title="提示">
                                                </Confirmation>
                                            </Command>
                                        </DirectEvents>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel runat="server" Mode="Single"/>
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar
                                    runat="server"
                                    Dock="Bottom"
                                    DisplayInfo="true"
                                    DisplayMsg="数据显示: 第{0} - {1}条, 共 {2} 条"
                                    EmptyMsg="暂无数据"
                                    />
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                
                <ext:Container runat="server">
                    <Content>
                        <ucExt:WindowTreeBase runat="server" 
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
