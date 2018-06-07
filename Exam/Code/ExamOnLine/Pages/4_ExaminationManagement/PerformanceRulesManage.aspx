<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformanceRulesManage.aspx.cs" Inherits="ExamOnLine.Pages.ExaminationManagement.PerformanceRulesManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考试结果规则</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/4_ExaminationManagement/PerformanceRulesManage.js"></script>
</head>
<body>
    <form id="form1" runat="server">
         <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:Panel 
                    runat="server"
                    Layout="FitLayout"
                    Title="▏考试结果规则"
                    Region="Center"
                    AutoScroll="true"
                    Header="true"
                    >
                    <TopBar>
                        <ext:Toolbar runat="server" AnchorHorizontal="100" Height="30">
                            <Items>
                                <ext:ToolbarFill ID="ToolBarFill" />

                                <ext:Button runat="server"
                                    ID="btnAddSelectPerformanceRule"
                                    Icon="WorldAdd"
                                    Text="添加新规则"
                                    Width="100"
                                    Height="22"
                                    StandOut="true"
                                    ArrowAlign="Right"
                                    StyleSpec="margin-right:15px;"
                                    >
                                    <Listeners>
                                        <Click Handler="btnAddSelectPerformanceRule_Click();"></Click>
                                    </Listeners>
                                </ext:Button>

                                <ext:Button runat="server"
                                    ID="btnReturn"
                                    Icon="PageBack"
                                    Text="返回"
                                    Width="90"
                                    Height="22"
                                    StandOut="true"
                                    ArrowAlign="Right"
                                    StyleSpec="margin-right:15px;"
                                    >
                                    <Listeners>
                                        <Click Handler="btnReturn_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>

                    <Items>
                        <ext:GridPanel
                            ID="GridPanel1"
                            runat="server"
                            Title="考试结果规则"
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
                                                
                                                <ext:TextField ID="txtRulesName"
                                                    runat="server"
                                                    LabelWidth="60"
                                                    LabelAlign="Right"
                                                    FieldLabel="规则名称"
                                                    InputWidth="150"
                                                    StyleSpec="margin:5px 0px 0px 5px;"
                                                    >
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

                                                <ext:ModelField Name="PERFORMANCE_RULES_NAME" Type="String"/>

                                                <ext:ModelField Name="SEQUENCE" Type="String"/>

                                                <ext:ModelField Name="REMARK" Type="String"/>
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column ID="Column_ID" runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                                    <ext:Column ID="Column_PERFORMANCE_RULES_NAME"      runat="server" Text="规则名称" Align="Center" DataIndex="PERFORMANCE_RULES_NAME" Flex="1"/>
                                    <ext:Column ID="Column_SEQUENCE"       runat="server" Text="规则顺序" Align="Center" DataIndex="SEQUENCE" Flex="1"/>
                                    <ext:Column ID="Column_REMARK"       runat="server" Text="备注" Align="Center" DataIndex="REMARK" Flex="1"/>
                                    <ext:CommandColumn ID="CommandColumn_SubsectionManage" runat="server" Text="分段管理" Align="Center" Flex="1">
                                                <Commands>
                                                    <ext:CommandFill></ext:CommandFill>
                                                    <ext:GridCommand Icon="ApplicationOsx" CommandName="subsectionManageCommand" MinWidth="22">
                                                        <ToolTip Text="分段管理" />
                                                    </ext:GridCommand>
                                                    <ext:CommandFill></ext:CommandFill>
                                                </Commands>
                                                <Listeners>
                                                    <Command Handler="CommandColumn_SubsectionManage_Command(item, command, record, recordIndex, cellIndex);">
                                                    </Command>
                                                </Listeners>
                                            </ext:CommandColumn>

                                    <ext:CommandColumn ID="CommandColumn_View" runat="server" Text="查看" Align="Center" Flex="1">
                                                <Commands>
                                                    <ext:CommandFill></ext:CommandFill>
                                                    <ext:GridCommand Icon="ApplicationDouble" CommandName="viewCommand" MinWidth="22">
                                                        <ToolTip Text="查看" />
                                                    </ext:GridCommand>
                                                    <ext:CommandFill></ext:CommandFill>
                                                </Commands>
                                                <Listeners>
                                                    <Command Handler="CommandColumn_View_Command(item, command, record, recordIndex, cellIndex);">
                                                    </Command>
                                                </Listeners>
                                            </ext:CommandColumn>

                                    <ext:CommandColumn ID="CommandColumn_Edit" runat="server" Text="修改" Align="Center" Flex="1">
                                                <Commands>
                                                    <ext:CommandFill></ext:CommandFill>
                                                    <ext:GridCommand Icon="ApplicationFormEdit" CommandName="editCommand" MinWidth="22">
                                                        <ToolTip Text="修改" />
                                                    </ext:GridCommand>
                                                    <ext:CommandFill></ext:CommandFill>
                                                </Commands>
                                                <Listeners>
                                                    <Command Handler="CommandColumn_Edit_Command(item, command, record, recordIndex, cellIndex);">
                                                    </Command>
                                                </Listeners>
                                            </ext:CommandColumn>

                                    <ext:CommandColumn ID="CommandColumn_Delete" runat="server" Text="删除" Align="Center" Flex="1">
                                                <Commands>
                                                    <ext:CommandFill></ext:CommandFill>
                                                    <ext:GridCommand Icon="Cancel" CommandName="deleteCommand" MinWidth="22">
                                                        <ToolTip Text="删除" />
                                                    </ext:GridCommand>
                                                    <ext:CommandFill></ext:CommandFill>
                                                </Commands>
                                                <DirectEvents>
                                            <Command Before="CommandColumn_DeleteCommand_Before();" After="" Success="btnQuery_Click();" Failure="" Complete="CommandColumn_DeleteCommand_Complete();" OnEvent="CommandColumn_DeleteCommand">
                                                <ExtraParams>
                                                    <ext:Parameter Mode="Raw" Encode="true" Value="record.data.ID" Name="id" />
                                                </ExtraParams>
                                                <Confirmation Message="确定要删除该考试成绩规则吗?" ConfirmRequest="true" Title="确认">
                                                </Confirmation>
                                            </Command>
                                        </DirectEvents>
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
    </form>
</body>
</html>
