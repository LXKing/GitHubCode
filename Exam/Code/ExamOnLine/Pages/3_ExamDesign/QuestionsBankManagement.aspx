<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionsBankManagement.aspx.cs" Inherits="ExamOnLine.Pages.ExamDesign.QuestionsBankManagement" %>
<%@ Register Src="~/Controls/TreePanel/TreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>题库管理</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/3_ExamDesign/QuestionsBankManagement.js"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden runat="server" ID="hidden_TreeType"></ext:Hidden>
        <ext:Hidden runat="server" ID="hidden_KnowledgeID"></ext:Hidden>
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:Panel 
                    runat="server"
                    Layout="FitLayout"
                    Title="▏题库管理"
                    AutoScroll="true"
                    >
                    <TopBar>
                        <ext:Toolbar runat="server" Height="35">
                            <Items>
                                <ext:ToolbarFill ID="ToolBarFill" />
                                <ext:Button runat="server"
                                    ID="btnShowQueryUI"
                                    Text="隐藏查询"
                                    BaseCls="false"
                                    Cls="button_showsearch"
                                    >
                                    <Listeners>
                                        <Click Handler="btnShowQueryUI_Click(this);"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="btnAddQuestion"
                                    Text="添加试题"
                                    BaseCls="false"
                                    Cls="button_add"  
                                    >
                                    <Listeners>
                                        <Click Handler="btnAddQuestion_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="Button1"
                                    Text="试题预览"
                                    BaseCls="false"
                                    Cls="button_preview" 
                                    >
                                    <Listeners>
                                        <Click Handler="btnExportUsers_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="btnExportUsers"
                                    Text="导出数据"
                                    BaseCls="false"
                                    Cls="button_exportinfo" 
                                    >
                                    <Listeners>
                                        <Click Handler="btnExportUsers_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="btnDownloadUsers"
                                    Text="下载数据"
                                    BaseCls="false"
                                    Cls="button_down" 
                                    >
                                    <Listeners>
                                        <Click Handler="btnDownloadUsers_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="btnImportUsers"
                                    Text="导入试题"
                                    BaseCls="false"
                                    Cls="button_down"
                                    >
                                    <Listeners>
                                        <Click Handler="btnImportUsers_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="btnShowError"
                                    Text="错误信息"
                                    BaseCls="false"
                                    Cls="button_downerror"
                                    >
                                    <Listeners>
                                        <Click Handler="btnShowError_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server"
                                    ID="btnDeleteMany"
                                    Text="批量删除"
                                    BaseCls="false"
                                    Cls="button_batchdelete"
                                    >
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
                    <Items>
                        <ext:GridPanel
                            ID="GridPanel1"
                            runat="server"
                            AutoScroll="true"
                            >
                            <TopBar>
                                <ext:Toolbar runat="server" Height="80" ID="Toolbar_Search" Hidden="false" Layout="FormLayout">
                                    <Items>
                                        <ext:Container runat="server" 
                                            Layout="ColumnLayout"
                                            Height="35"
                                            StyleSpec="margin-top:0px;">
                                            <Items>
                                                <ext:Label runat="server" 
                                                    Text="题库搜索" 
                                                    Icon="Magnifier"
                                                    StyleSpec="margin-left:5px;margin-top:5px;"
                                                    ></ext:Label>
                                                <ext:TextField ID="txtKnowledge"
                                                    runat="server"                                                    
                                                    LabelWidth="80"
                                                    LabelAlign="Right"
                                                    FieldLabel="知识体系"
                                                    Width="250"
                                                    ReadOnly="true"
                                                    IndicatorIcon="Zoom"
                                                    StyleSpec="margin-top:5px;"
                                                    >
                                                    <DirectEvents>
                                                        <IndicatorIconClick OnEvent="Knowledge_IndicatorIconClick"></IndicatorIconClick>
                                                    </DirectEvents>
                                                    <Listeners>
                                                        <IndicatorIconClick Handler="Knowledge_IndicatorIconClick();"></IndicatorIconClick>
                                                    </Listeners>
                                                </ext:TextField>
                                                <ext:TextField ID="txtSubject"
                                                    runat="server"
                                                    LabelWidth="50"
                                                    LabelAlign="Right"
                                                    FieldLabel="题目"
                                                    Width="400"
                                                    StyleSpec="margin-top:5px;"
                                                    >
                                                </ext:TextField>
                                                <ext:Button runat="server" Text="搜索" 
                                                    StyleSpec="margin-left:5px;margin-top:5px;" 
                                                    BaseCls="false" 
                                                    Cls="button_showsearch">
                                                    <Listeners>
                                                        <Click Handler="btnSearch_Click();"></Click>
                                                    </Listeners>
                                                </ext:Button>                                    
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server"
                                            Height="35"
                                            Layout="ColumnLayout"
                                            StyleSpec="margin-top:0px;">
                                            <Items>
                                                <ext:Label runat="server" 
                                                    Text="快速筛选" 
                                                    Icon="Magnifier"
                                                    StyleSpec="margin-left:5px;">
                                                </ext:Label>
                                                <ext:ComboBox runat="server" 
                                                    ID="cmbQuestionsType" 
                                                    FieldLabel="题型" 
                                                    LabelWidth="80"
                                                    LabelAlign="Right" 
                                                    DisplayField="QUESTION_TYPE_NAME" 
                                                    ValueField="ID"
                                                    Width="250"
                                                    AutoSelect="true"
                                                    EmptyText="题型" 
                                                    Editable="false"
                                                    >
                                                    <Store>
                                                        <ext:Store ID="Store1" runat="server">
                                                            <Model>
                                                                <ext:Model ID="Model1" runat="server" >
                                                                    <Fields>
                                                                        <ext:ModelField Name="ID" />
                                                                        <ext:ModelField Name="QUESTION_TYPE_NAME" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                </ext:ComboBox>
                                                <ext:ComboBox runat="server" 
                                                    ID="cmbDifficulty" 
                                                    FieldLabel="难度" 
                                                    LabelAlign="Right" 
                                                    DisplayField="NAME" 
                                                    ValueField="ID" 
                                                    LabelWidth="50"
                                                    Width="200"
                                                    AutoSelect="true"
                                                    EmptyText="难度" 
                                                    Editable="false"
                                                    >
                                                    <Items>
                                                        <ext:ListItem Index="0" Text="低难度" Value="0"></ext:ListItem>
                                                        <ext:ListItem Index="1" Text="中难度" Value="1"></ext:ListItem>
                                                        <ext:ListItem Index="2" Text="高难度" Value="2"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                                <ext:ComboBox runat="server" 
                                                    ID="cmbStatus" 
                                                    FieldLabel="状态" 
                                                    LabelAlign="Right" 
                                                    DisplayField="NAME" 
                                                    ValueField="ID" 
                                                    LabelWidth="50"
                                                    Width="200"
                                                    AutoSelect="true"
                                                    EmptyText="状态" 
                                                    Editable="false"
                                                    >
                                                    <Items>
                                                        <ext:ListItem Index="0" Text="不显示" Value="1"></ext:ListItem>
                                                        <ext:ListItem Index="1" Text="显示" Value="0"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                                <ext:Button runat="server" ID="btnClear" Text="清空" StyleSpec="margin-left:5px;" BaseCls="false" Cls="button_exportinfo">
                                                    <Listeners>
                                                        <Click Handler="btnClear_Click();"></Click>
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
                                                <ext:ModelField Name="KNOWLEDGE_ID" Type="Object"/>
                                                <ext:ModelField Name="KNOWLEDGE_NAME" Type="String" />
                                                <ext:ModelField Name="QUESTION_TYPE_ID" Type="Object" />
                                                <ext:ModelField Name="QUESTION_TYPE_NAME" Type="String" />
                                                <ext:ModelField Name="DIFFICULTY" Type="String" />
                                                <ext:ModelField Name="SHOW_IN_PRACTICE" Type="String" />
                                                <ext:ModelField Name="CREATE_DATE" Type="Date" DateWriteFormat="yyyy/MM/dd" />
                                                <ext:ModelField Name="QUESTION_CONTENT" Type="String"/>
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column ID="Column_ID"              runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                                    <ext:Column ID="Column_KNOWLEDGE_NAME"      runat="server" Text="知识体系" Align="Center" DataIndex="KNOWLEDGE_NAME" Flex="1"/>
                                    <ext:Column ID="Column_QUESTION_TYPE_NAME"       runat="server" Text="题型" Align="Center" DataIndex="QUESTION_TYPE_NAME" Flex="1"/>
                                    <ext:Column ID="Column_DIFFICULTY"             runat="server" Text="难度"   Align="Center" DataIndex="DIFFICULTY" Flex="1"/>
                                    <ext:Column ID="Column_SHOW_IN_PRACTICE" runat="server" Text="状态"   Align="Center" DataIndex="SHOW_IN_PRACTICE" Flex="1"/>
                                    <ext:Column ID="Column_QUESTION_CONTENT"       runat="server" Text="题目"   Align="Center" DataIndex="QUESTION_CONTENT" Flex="1"/>
                                    <ext:Column ID="Column_CREATE_DATE"   runat="server" Text="日期"   Align="Center" DataIndex="CREATE_DATE" Flex="1"/>
                                    

                                    <ext:CommandColumn ID="CommandColumn_View" runat="server" Text="查看" Align="Center" Flex="1">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Application" CommandName="viewQuestion" MinWidth="22">
                                                <ToolTip Text="查看" />
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
                                            <Command Handler="CommandColumn_View_Command(item, command, record, recordIndex, cellIndex);">
                                            </Command>
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
                                        <%--<DirectEvents>
                                            <Command OnEvent="CommandColumn_Edit_Command">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>--%>
                                        <Listeners>
                                            <Command Handler="CommandColumn_Edit_Command(item, command, record, recordIndex, cellIndex);">
                                            </Command>
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
                </ext:Panel>
                <ext:Container runat="server">
                    <Content>
                        <ucExt:TreePanelExt runat="server" 
                            ID="treePanelKnowledge"
                            Hidden="true"
                            Modal="true"
                            RootNodeID=""
                            RootNodeText="根节点"
                            Title="知识体系选择"
                            RootNodeVisible="false"
                            OnNodeClick="treePanelKnowledge_NodeClick"
                             />
                    </Content>
                </ext:Container>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
