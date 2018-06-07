<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyExams.aspx.cs" Inherits="ExamOnLine.Pages.MyExaminations.MyExams" %>
<%@ Register Src="../../Controls/TreePanel/TreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>我的考试</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/5_MyExaminations/MyExams.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden runat="server"  ID="hidden_ExamTypeID"/>
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:Panel 
                    runat="server"
                    Layout="FitLayout"
                    Title="▏参加考试"
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
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                         <ext:GridPanel
                            ID="GridPanel1"
                            runat="server"
                            Title="参加考试"
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

                                                <ext:TextField ID="txtExamType"
                                                    runat="server"
                                                    LabelWidth="60"
                                                    LabelAlign="Right"
                                                    FieldLabel="安排分类"
                                                    InputWidth="150"
                                                    IndicatorIcon="ApplicationForm"
                                                    IndicatorTip="选择安排分类"
                                                    StyleSpec="margin:5px 0px 0px 5px;"
                                                    >
                                                    <DirectEvents>
                                                        <IndicatorIconClick Before="" After="" Success="" Failure="" Complete="" OnEvent="txtExamType_IndicatorIconClick"></IndicatorIconClick>
                                                    </DirectEvents>
                                                    <Listeners>
                                                        <IndicatorIconClick Handler="txtExamType_IndicatorIconClick(item, indicatorEl);"></IndicatorIconClick>
                                                    </Listeners>
                                                </ext:TextField>

                                                <ext:TextField ID="txtExamName"
                                                    runat="server"
                                                    LabelWidth="60"
                                                    InputWidth="150"
                                                    LabelAlign="Right"
                                                    FieldLabel="安排名称"
                                                    StyleSpec="margin:5px 0px 0px 5px;"
                                                    ></ext:TextField>

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

                                                <ext:Button runat="server" 
                                                    ID="btnClearCodition"
                                                    Width="80"
                                                    Text="清空条件"
                                                    StyleSpec="margin:5px 0px 0px 40px;"
                                                    StandOut="true"
                                                    Icon="Cancel"
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
                                        <ext:PageProxy DirectFn="App.direct.BindData">
                                        </ext:PageProxy>
                                    </Proxy>                                    
                                    <Model>
                                        <ext:Model runat="server" IDProperty="ID">
                                            <Fields>
                                                <ext:ModelField Name="ID" Type="Object"/>

                                                <ext:ModelField Name="EXAM_TYPE_ID" Type="Object"/>

                                                <ext:ModelField Name="EXAM_TYPE_NAME" Type="String"/>

                                                <ext:ModelField Name="EXAM_PLAN_NAME" Type="String"/>

                                                <ext:ModelField Name="TOTAL_TIME" Type="Int"/>

                                                <ext:ModelField Name="MAKE_QUESTION_TYPE_ID" Type="Object"/>

                                                <ext:ModelField Name="MAKE_QUESTION_TYPE_TEXT" Type="String"/>

                                                <ext:ModelField Name="PAPER_MODEL_NAME" Type="String"/>

                                                <ext:ModelField Name="EXAM_TIME_PERIOD" Type="String"/>
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>

                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column ID="Column_ID" runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />

                                    <ext:Column ID="Column_EXAM_TYPE_NAME"      runat="server" Text="安排分类" Align="Center" DataIndex="EXAM_TYPE_NAME" Flex="1"/>

                                    <ext:Column ID="Column_EXAM_PLAN_NAME"       runat="server" Text="安排名称" Align="Center" DataIndex="EXAM_PLAN_NAME" Flex="1"/>

                                    <ext:Column ID="Column_TOTAL_TIME"       runat="server" Text="时间(分钟)" Align="Center" DataIndex="TOTAL_TIME" Flex="1"/>

                                    <%--<ext:Column ID="Column_MAKE_QUESTION_TYPE_TEXT"             runat="server" Text="出题方式"   Align="Center" DataIndex="MAKE_QUESTION_TYPE_TEXT" Flex="1"/>--%>
                                    <ext:Column ID="Column_PAPER_MODEL_NAME"             runat="server" Text="模式"   Align="Center" DataIndex="PAPER_MODEL_NAME" Flex="1"/>

                                    <ext:Column ID="Column_EXAM_TIME_PERIOD"             runat="server" Text="安排时间"   Align="Center" DataIndex="EXAM_TIME_PERIOD" Width="280"/>

                                    <ext:CommandColumn ID="CommandColumn_JoinExam" runat="server" Text="参加考试" Align="Center" Flex="1">
                                                <Commands>
                                                    <ext:CommandFill></ext:CommandFill>
                                                    <ext:GridCommand Icon="FlagChecked" CommandName="selectCommand" MinWidth="22">
                                                        <ToolTip Text="进入" />
                                                    </ext:GridCommand>
                                                    <ext:CommandFill></ext:CommandFill>
                                                </Commands>
                                                <Listeners>
                                                    <Command Handler="CommandColumn_JoinExam_Command(item, command, record, recordIndex, cellIndex);">
                                                    </Command>
                                                </Listeners>
                                            </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>

                            <SelectionModel>
                                <ext:RowSelectionModel runat="server" Mode="Single" />
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
         <ext:Container runat="server" ID="treePanelContainer">
            <Content>
                <ucExt:TreePanelExt runat="server" 
                    ID="treePanelExamTypeList"
                    Hidden="true"
                    Modal="true"
                    RootNodeID=""
                    RootNodeText="根节点"
                    Title="考试安排分类选择"
                    RootNodeVisible="false"
                    OnNodeClick="treePanelExamTypeList_NodeClick"
                        />
        </Content>
        </ext:Container>
    </form>
</body>
</html>
