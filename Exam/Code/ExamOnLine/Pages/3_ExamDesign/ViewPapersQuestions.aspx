<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPapersQuestions.aspx.cs" Inherits="ExamOnLine.Pages.ExamDesign.ViewPapersQuestions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看试题</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/3_ExamDesign/ViewPapersQuestions.js"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:Panel
                    runat="server"
                    Layout="FitLayout"
                    Title="▏试题信息"
                    AutoScroll="true">
                    <TopBar>
                        <ext:Toolbar runat="server" Height="35">
                            <Items>
                                <ext:ToolbarFill ID="ToolBarFill" />
                                <ext:Button runat="server"
                                    ID="Button1"
                                    Text="删除"
                                    BaseCls="false"
                                    Cls="button_delete">
                                    <Listeners>
                                        <Click></Click>
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
                                        <%--                                        <ext:PageProxy
                                            DirectFn="App.direct.BindData">
                                        </ext:PageProxy>--%>
                                    </Proxy>
                                    <Model>
                                        <ext:Model runat="server" IDProperty="ID">
                                            <Fields>
                                                <ext:ModelField Name="ID" Type="Object" />
                                                <ext:ModelField Name="KNOWLEDGE_ID" Type="Object" />
                                                <ext:ModelField Name="T_KNOWLEDGE.KNOWLEDGE_NAME" Type="String" />
                                                <ext:ModelField Name="QUESTION_TYPE_ID" Type="Object" />
                                                <ext:ModelField Name="T_QUESTION_TYPE.QUESTION_TYPE_NAME" Type="String" />
                                                <ext:ModelField Name="QUESTION_TYPE_NAME" Type="String" />
                                                <ext:ModelField Name="DIFFICULTY" Type="String" />
                                                <ext:ModelField Name="SHOW_IN_PRACTICE" Type="String" />
                                                <ext:ModelField Name="CREATE_DATE" Type="Date" DateWriteFormat="yyyy/MM/dd" />
                                                <ext:ModelField Name="QUESTION_CONTENT" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column ID="Column_ID" runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                                    <ext:Column ID="Column_KNOWLEDGE_NAME" runat="server" Text="知识点" Align="Center" DataIndex="T_KNOWLEDGE.KNOWLEDGE_NAME" Flex="1" />
                                    <ext:Column ID="Column_QUESTION_TYPE_NAME" runat="server" Text="难度" Align="Center" DataIndex="T_QUESTION_TYPE.QUESTION_TYPE_NAME" Flex="1" />
                                    <ext:Column ID="Column_DIFFICULTY" runat="server" Text="题目" Align="Center" DataIndex="DIFFICULTY" Flex="1" />
                                    <ext:Column ID="Column_SHOW_IN_PRACTICE" runat="server" Text="分数/题" Align="Center" DataIndex="SHOW_IN_PRACTICE" Flex="1" />
                                    <ext:Column ID="Column_CREATE_DATE" runat="server" Text="排序" Align="Center" DataIndex="CREATE_DATE" Flex="1" />

                                    <ext:CommandColumn ID="CommandColumn_Delete" runat="server" Text="删除" Align="Center" Flex="1">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Application" CommandName="questionDelete" MinWidth="22">
                                                <ToolTip Text="删除" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <DirectEvents>
                                            <Command Before="showLoading('GridPanel1','正在删除中......');" Complete="hideLoading('GridPanel1');">
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
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
