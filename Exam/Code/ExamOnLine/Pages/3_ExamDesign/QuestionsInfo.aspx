<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionsInfo.aspx.cs" Inherits="ExamOnLine.Pages.ExamDesign.QuestionsInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>试题信息</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/3_ExamDesign/QuestionsInfo.js"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Hidden runat="server" ID="hidPaperQuestionTypeID" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:GridPanel
                    ID="GridPanel1"
                    runat="server"
                    AutoScroll="true">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ToolbarFill ID="ToolBarFill" />
<%--                                <ext:Button runat="server"
                                    ID="btnAddQuestion"
                                    Text="全选"
                                    BaseCls="false"
                                    Cls="button_blue_s">
                                    <Listeners>
                                        <Click></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="Button1"
                                    Text="反选"
                                    BaseCls="false"
                                    Cls="button_blue_s">
                                    <Listeners>
                                        <Click></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="btnExportUsers"
                                    Text="清空"
                                    BaseCls="false"
                                    Cls="button_green_s">
                                    <Listeners>
                                        <Click></Click>
                                    </Listeners>
                                </ext:Button>--%>
                                <ext:Button runat="server"
                                    ID="btnDownloadUsers"
                                    Text="批量删除"
                                    BaseCls="false"
                                    Cls="button_red">
                                    <DirectEvents>
                                        <Click Before="" After="" Success="App.GridPanel1.getSelectionModel().deselectAll();" Complete="" Failure="" OnEvent="btnDeleteMany_Click">
                                            <Confirmation Cancel="" BeforeConfirm="return App.GridPanel1.getSelectionModel().selected.keys.length>0;" Message="确定要删除选中的试题吗?" Title="询问" ConfirmRequest="true">
                                            </Confirmation>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
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
                                        <ext:ModelField Name="ID" Type="String" />
                                        <ext:ModelField Name="KNOWLEDGE_NAME" Type="String" />
                                        <ext:ModelField Name="DIFFICULTY" Type="String" />
                                        <ext:ModelField Name="QUESTION_CONTENT" Type="String" />
                                        <ext:ModelField Name="SCORE" Type="Float" />
                                        <ext:ModelField Name="SEQUENCE" Type="String" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:Column ID="Column_ID" runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                            <ext:Column ID="Column_KNOWLEDGE_NAME" runat="server" Text="知识点" Align="Center" DataIndex="KNOWLEDGE_NAME" Flex="1" />
                            <ext:Column ID="Column_DIFFICULTY" runat="server" Text="难度" Align="Center" DataIndex="DIFFICULTY" Flex="1" />
                            <ext:Column ID="Column_QUESTION_CONTENT" runat="server" Text="题目" Align="Center" DataIndex="QUESTION_CONTENT" Flex="1" />
                            <ext:Column ID="Column_SCORE" runat="server" Text="分数" Align="Center" DataIndex="SCORE" Flex="1" />
                            <ext:Column ID="Column_SEQUENCE" runat="server" Text="排序" Align="Center" DataIndex="SEQUENCE" Flex="1">
                                <Editor>
                                    <ext:TextField runat="server" />
                                </Editor>
                            </ext:Column>

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
                        <ext:CheckboxSelectionModel runat="server" Mode="Multi" />
                    </SelectionModel>
                    <Plugins>
                        <ext:CellEditing runat="server">
                            <Listeners>
                                <Edit Fn="edit" />
                            </Listeners>
                        </ext:CellEditing>
                    </Plugins>
                    <BottomBar>
                        <ext:PagingToolbar
                            runat="server"
                            DisplayInfo="true"
                            DisplayMsg="数据显示: 第{0} - {1}条, 共 {2} 条"
                            EmptyMsg="暂无数据" />
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
