<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPaper.aspx.cs" Inherits="ExamOnLine.Pages.ExaminationManagement.SelectPaper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>选择试卷</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/4_ExaminationManagement/SelectPaper.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:GridPanel
                            ID="GridPanel1"
                            runat="server"
                            Title="选择考试试卷"
                            TitleAlign="Left"
                            Header="false"
                            DisableSelection="true"
                            AnchorHorizontal="100"
                            AutoScroll="true"
                            >
                            <TopBar>
                                <ext:Toolbar runat="server" AnchorHorizontal="100" Height="35" ID="Toolbar_Search" Hidden="false" Layout="ColumnLayout">
                                    <Items>
                                        <ext:Label runat="server" 
                                                    Text="快速筛选" 
                                                    Icon="Magnifier"
                                                    StyleSpec="margin-top:5px;
                                                    margin-left:5px;"
                                                    ></ext:Label>
                                        <ext:ComboBox ID="cmbMakeQuestionType"
                                                    runat="server"
                                                    LabelWidth="60"
                                                    InputWidth="130"
                                                    LabelAlign="Right"
                                                    FieldLabel="出题方式"
                                                    EmptyText="--出题方式--"
                                                    DisplayField="TEXT"
                                                    Editable="false"
                                                    ValueField="ID"
                                                    StyleSpec="margin:5px 0px 0px 5px;"
                                                    >
                                                    <Store>
                                                        <ext:Store runat="server">
                                                            <Model>
                                                                <ext:Model ID="Model3" runat="server" >
                                                                    <Fields>
                                                                        <ext:ModelField Name="ID" />
                                                                        <ext:ModelField Name="TEXT" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <%--<DirectEvents>
                                                        <IndicatorIconClick Before="" After="" Success="" Failure="" Complete="" OnEvent="Unnamed_Event"></IndicatorIconClick>
                                                    </DirectEvents>--%>
                                                </ext:ComboBox>
                                        <ext:ComboBox ID="cmbPaperType"
                                                    runat="server"
                                                    LabelWidth="60"
                                                    InputWidth="120"
                                                    LabelAlign="Right"
                                                    FieldLabel="试卷类型"
                                                    EmptyText="--试卷类型--"
                                                    DisplayField="TEXT"
                                                    Editable="false"
                                                    ValueField="ID"
                                                    StyleSpec="margin:5px 0px 0px 5px;"
                                                    >
                                                    <Store>
                                                        <ext:Store runat="server">
                                                            <Model>
                                                                <ext:Model ID="Model1" runat="server" >
                                                                    <Fields>
                                                                        <ext:ModelField Name="ID" />
                                                                        <ext:ModelField Name="TEXT" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <%--<DirectEvents>
                                                        <IndicatorIconClick Before="" After="" Success="" Failure="" Complete="" OnEvent="Unnamed_Event"></IndicatorIconClick>
                                                    </DirectEvents>--%>
                                                </ext:ComboBox>
                                        <ext:TextField ID="txtPaperName"
                                                    runat="server"
                                                    LabelWidth="60"
                                                    LabelAlign="Right"
                                                    FieldLabel="试卷名称"
                                                    InputWidth="120"
                                                    StyleSpec="margin:5px 0px 0px 5px;"
                                                    >
                                                </ext:TextField>
                                        <ext:Button runat="server" 
                                                    ID="btnQuery"
                                                    Width="80"
                                                    Text="搜索"
                                                    StyleSpec="margin:5px 0px 0px 20px;"
                                                    StandOut="true"
                                                    Icon="Zoom"
                                                    >
                                                    <Listeners>
                                                        <Click Handler="btnQuery_Click();"></Click>
                                                    </Listeners>
                                                </ext:Button>
                                        <ext:Button runat="server" 
                                                    ID="btnClear"
                                                    Width="80"
                                                    Text="清除"
                                                    StyleSpec="margin:5px 0px 0px 20px;"
                                                    StandOut="true"
                                                    Icon="Cancel"
                                                    >
                                                    <Listeners>
                                                        <Click Handler="btnClear_Click();"></Click>
                                                    </Listeners>
                                                </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store_GridPanel1" runat="server" PageSize="5">
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

                                                <ext:ModelField Name="PAPER_TYPE" Type="Object"/>
                                                <ext:ModelField Name="PAPER_TYPE_TEXT" Type="String"/>

                                                <ext:ModelField Name="MAKE_QUESTION_TYPE" Type="Object"/>
                                                <ext:ModelField Name="MAKE_QUESTION_TYPE_TEXT" Type="String"/>

                                                <ext:ModelField Name="PAPER_NAME" Type="String" />
                                                <ext:ModelField Name="CREATE_DATE" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column ID="Column_ID" runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                                    <ext:Column ID="Column_EXAM_TYPE_NAME"      runat="server" Text="考试领域" Align="Center" DataIndex="EXAM_TYPE_NAME" Flex="1"/>
                                    <ext:Column ID="Column_PAPER_TYPE_TEXT"       runat="server" Text="试卷类型" Align="Center" DataIndex="PAPER_TYPE_TEXT" Flex="1"/>
                                    <ext:Column ID="Column_MAKE_QUESTION_TYPE_TEXT"       runat="server" Text="出题方式" Align="Center" DataIndex="MAKE_QUESTION_TYPE_TEXT" Flex="1"/>
                                    <ext:Column ID="Column_PAPER_NAME"             runat="server" Text="试卷名称"   Align="Center" DataIndex="PAPER_NAME" Flex="1"/>
                                    <ext:CommandColumn ID="CommandColumn_Select" runat="server" Text="选择" Align="Center" Flex="1">
                                                <Commands>
                                                    <ext:CommandFill></ext:CommandFill>
                                                    <ext:GridCommand Icon="FlagChecked" CommandName="selectCommand" MinWidth="22">
                                                        <ToolTip Text="选择" />
                                                    </ext:GridCommand>
                                                    <ext:CommandFill></ext:CommandFill>
                                                </Commands>
                                                <Listeners>
                                                    <Command Handler="CommandColumn_Select_Command(item, command, record, recordIndex, cellIndex);">
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
        </ext:Viewport>
    </form>
</body>
</html>
