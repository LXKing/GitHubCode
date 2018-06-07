<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionsTypeManagement.aspx.cs" Inherits="ExamOnLine.Pages.ExamDesign.QuestionsTypeManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>题型管理</title>

    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/3_ExamDesign/QuestionsTypeManagement.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:Panel 
                    runat="server"
                    Layout="FitLayout"
                    Title="▏题型管理"
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
                                    ID="btnAddQuestionType"
                                    Icon="TableRefresh"
                                    Text="添加新题型"
                                    Width="90"
                                    Height="22"
                                    StandOut="true"
                                    ArrowAlign="Right"
                                    StyleSpec="margin-right:15px;"
                                    >
                                    <Listeners>
                                        <Click Handler="btnAddQuestionType_Click();"></Click>
                                    </Listeners>
                                    <%--<DirectEvents>
                                        <Click Before="" After="" Success="" Failure="" Complete="" OnEvent="btnRefreshUser_Click"></Click>
                                    </DirectEvents>--%>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:GridPanel
                            ID="GridPanel1"
                            runat="server"
                            Title="题型管理"
                            TitleAlign="Left"
                            Header="false"
                            DisableSelection="true"
                            AnchorHorizontal="100"
                            AutoScroll="true"
                            >
                            <TopBar>
                                <ext:Toolbar runat="server" AnchorHorizontal="100" Height="35" ID="Toolbar_Search" Hidden="false" Layout="AnchorLayout">
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
                                                <ext:TextField ID="txtQuestionTypeName"
                                                    runat="server"
                                                    LabelWidth="150"
                                                    LabelAlign="Right"
                                                    FieldLabel="题型名称/模板名称"
                                                    Width="350"
                                                    StyleSpec="margin:5px 0px 0px 5px;"
                                                    >
                                                    <%--<DirectEvents>
                                                        <IndicatorIconClick Before="" After="" Success="" Failure="" Complete="" OnEvent="Unnamed_Event"></IndicatorIconClick>
                                                    </DirectEvents>--%>
                                                </ext:TextField>
                                                <ext:Button runat="server" 
                                                    ID="btnQuery"
                                                    Width="80"
                                                    Text="搜索"
                                                    StyleSpec="margin:5px 0px 0px 40px;"
                                                    StandOut="true"
                                                    Icon="Zoom"
                                                    >
                                                    <Listeners>
                                                        <Click Handler="btnQuery_Click();"></Click>
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
                                                <ext:ModelField Name="QUESTION_TYPE_NAME" Type="String"/>
                                                <ext:ModelField Name="SCORE" Type="Float" DateFormat="0.00"/>
                                                <ext:ModelField Name="T_QUESTION_TEMPLATE.TEMPLATE_NAME" Type="String" />
                                                <ext:ModelField Name="SEQUENCE" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column ID="Column_ID"              runat="server" Text="ID"     Width="50" Align="Center" DataIndex="ID" Flex="1" Visible="false" />
                                    <ext:Column ID="Column_QUESTION_TYPE_NAME"      runat="server" Text="题型" Width="50" Align="Center" DataIndex="QUESTION_TYPE_NAME" Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_SCORE"       runat="server" Text="分数" Width="50" Align="Center" DataIndex="SCORE" Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_TEMPLATE_NAME"             runat="server" Text="模板名称"   Align="Center" DataIndex="T_QUESTION_TEMPLATE.TEMPLATE_NAME" Flex="1" Visible="true"/>
                                    <ext:Column ID="Column_SEQUENCE"             runat="server" Text="排序"   Align="Center" DataIndex="SEQUENCE" Flex="1" Visible="true"/>

                                    <ext:CommandColumn ID="CommandColumn_View" runat="server" Width="100" Text="查看" Align="Center">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Application" CommandName="questionTypeViewCommand" MinWidth="22">
                                                <ToolTip Text="查看" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <%--<DirectEvents>
                                            <Command OnEvent="CommandColumn_View_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>--%>
                                        <Listeners>
                                            <Command Handler=" CommandColumn_View_Command(item, command, record, recordIndex, cellIndex);"></Command>
                                        </Listeners>
                                    </ext:CommandColumn>

                                    <ext:CommandColumn ID="CommandColumn_Edit" runat="server" Width="100" Text="修改" Align="Center">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="ApplicationEdit" CommandName="questionTypeEditCommand" MinWidth="22">
                                                <ToolTip Text="修改" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <%--<DirectEvents>
                                            <Command OnEvent="CommandColumn_Edit_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>--%>
                                        <Listeners>
                                            <Command Handler=" CommandColumn_Edit_Command(item, command, record, recordIndex, cellIndex);"></Command>
                                        </Listeners>
                                    </ext:CommandColumn>

                                    <ext:CommandColumn ID="CommandColumn_Delete" runat="server" Width="100" Text="删除" Align="Center">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Delete" CommandName="questionTypeDeleteCommand" MinWidth="22">
                                                <ToolTip Text="删除" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <DirectEvents>
                                            <Command Before="showLoading('GridPanel1','正在删除中......');" Complete="hideLoading('GridPanel1');" OnEvent="CommandColumn_Delete_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                </ExtraParams>
                                                <Confirmation Message="确定要删除该题型?" ConfirmRequest="true" Title="确认">
                                                </Confirmation>
                                            </Command>
                                        </DirectEvents>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
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
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
