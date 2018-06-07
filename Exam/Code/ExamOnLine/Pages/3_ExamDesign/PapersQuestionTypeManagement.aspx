<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PapersQuestionTypeManagement.aspx.cs" Inherits="ExamOnLine.Pages.ExamDesign.PapersQuestionTypeManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>试卷题型管理</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/3_ExamDesign/PapersQuestionTypeManagement.js?124"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Hidden ID="hidPaperID" runat="server" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:Panel
                    runat="server"
                    Layout="FitLayout"
                    Title="▏试卷题型管理"
                    AutoScroll="true">
                    <TopBar>
                        <ext:Toolbar runat="server" Height="35">
                            <Items>
                                <ext:ToolbarFill ID="ToolBarFill" />
                                <ext:Button runat="server"
                                    ID="btnAddQuestionsType"
                                    Text="添加题型"
                                    BaseCls="false"
                                    Cls="button_add">
                                    <Listeners>
                                        <Click Handler="btnAddQuestionsType();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="btnViewPaper"
                                    Text="预览试卷"
                                    BaseCls="false"
                                    Cls="button_goback">
                                    <Listeners>
                                        <Click></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="Button1"
                                    Text="返回"
                                    BaseCls="false"
                                    Cls="button_goback">
                                    <Listeners>
                                        <Click Handler="goBack();"></Click>
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:GridPanel
                            ID="GridPanel1"
                            runat="server"
                            AutoScroll="true">
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
                                                <ext:ModelField Name="ID" Type="Object" />
                                                <ext:ModelField Name="QUESTION_TYPE_NAME" Type="String" />
                                                <ext:ModelField Name="TITLE" Type="String" />
                                                <ext:ModelField Name="SEQUENCE" Type="String" />
                                                <ext:ModelField Name="SCORE" Type="Float"/>
                                                <ext:ModelField Name="LOW_DIFFICULTY_QUESTIONS_COUNT" Type="String" />
                                                <ext:ModelField Name="MEDIUM_DIFFICULTY_QUESTIONS_COUNT" Type="String" />
                                                <ext:ModelField Name="HIGH_DIFFICULTY_QUESTIONS_COUNT" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column ID="Column_ID" runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                                    <ext:Column ID="Column_QUESTION_TYPE_NAME" runat="server" Text="题型" Align="Center" DataIndex="QUESTION_TYPE_NAME" Flex="1" />
                                    <ext:Column ID="Column_TITLE" runat="server" Text="大标题" Align="Center" DataIndex="TITLE" Flex="1" />
                                    <ext:Column ID="Column_SEQUENCE" runat="server" Text="序号" Align="Center" DataIndex="SEQUENCE" Flex="1" />
                                    <ext:Column ID="Column_SCORE" runat="server" Text="分数/题" Align="Center" DataIndex="SCORE" Flex="1" />
                                    <%--<ext:Column ID="Column_CREATE_DATE" runat="server" Text="题数" Align="Center" DataIndex="CREATE_DATE" Flex="1" />--%>
                                    <ext:Column ID="Column_LOW" runat="server" Text="低" Align="Center" DataIndex="LOW_DIFFICULTY_QUESTIONS_COUNT" Flex="1" />
                                    <ext:Column ID="Column_MEDIUM" runat="server" Text="中" Align="Center" DataIndex="MEDIUM_DIFFICULTY_QUESTIONS_COUNT" Flex="1" />
                                    <ext:Column ID="Column_HIGH" runat="server" Text="高" Align="Center" DataIndex="HIGH_DIFFICULTY_QUESTIONS_COUNT" Flex="1" />

                                    <ext:CommandColumn ID="CommandColumn2" runat="server" Text="查看试题" Align="Center" Flex="1">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Application" CommandName="viewQuestion" MinWidth="22">
                                                <ToolTip Text="查看试题" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <%--<DirectEvents>
                                            <Command Before="return CommandColumn_View_Command(item, command, record, recordIndex, cellIndex);" After="" Success="App.GridPanel1.store.load();" Failure="" Complete="hideLoading('Viewport1');" OnEvent="CommandColumn_View_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="ID" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>--%>
                                        <Listeners>
                                            <Command Handler="CommandColumn_ViewQuestion_Command(item, command, record, recordIndex, cellIndex);"></Command>
                                        </Listeners>
                                    </ext:CommandColumn>

                                    <ext:CommandColumn ID="CommandColumn_View" runat="server" Text="查看" Align="Center" Flex="1">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Application" CommandName="viewQuestion" MinWidth="22">
                                                <ToolTip Text="查看" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <Listeners>
                                            <Command Handler="CommandColumn_View_Command(item, command, record, recordIndex, cellIndex);"></Command>
                                        </Listeners>
                                    </ext:CommandColumn>

                                    <ext:CommandColumn ID="CommandColumn_Edit" runat="server" Text="修改" Align="Center" Flex="1">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="ApplicationEdit" CommandName="questionEditCommand" MinWidth="22">
                                                <ToolTip Text="修改" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <Listeners>
                                            <Command Handler="CommandColumn_Edit_Command(item, command, record, recordIndex, cellIndex);"></Command>
                                        </Listeners>
                                    </ext:CommandColumn>

                                    <ext:CommandColumn ID="CommandColumn_Delete" runat="server" Text="删除" Align="Center" Flex="1">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Delete" CommandName="questionDelete" MinWidth="22">
                                                <ToolTip Text="删除" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <DirectEvents>
                                            <Command Before="showLoading('GridPanel1','正在删除中......');" Complete="hideLoading('GridPanel1');" OnEvent="CommandColumn_Delete_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                </ExtraParams>
                                                <Confirmation Message="确定要删除该试题?" ConfirmRequest="true" Title="确认">
                                                </Confirmation>
                                            </Command>
                                        </DirectEvents>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel runat="server" Mode="Simple" IgnoreRightMouseSelection="true" />
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
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Title="管理题型信息"
            Height="505" 
            Width="520"
            BodyStyle="background-color: #fff;"
            BodyPadding="5"
            Modal="true" 
            Hidden="true" 
            Resizable="false">
            <Loader runat="server" Mode="Frame">
                <LoadMask ShowMask="true"></LoadMask>
            </Loader>
        </ext:Window>
        <ext:Window 
            ID="Window2" 
            runat="server" 
            Title="试题信息"
            Height="505" 
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
