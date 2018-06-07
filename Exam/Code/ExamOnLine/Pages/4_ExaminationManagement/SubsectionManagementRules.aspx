<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubsectionManagementRules.aspx.cs" Inherits="ExamOnLine.Pages.ExaminationManagement.SubsectionManagementRules" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考试结果规则分段</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/4_ExaminationManagement/SubsectionManagementRules.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:Panel 
                    runat="server"
                    Layout="FitLayout"
                    Title="▏考试结果规则分段"
                    Region="Center"
                    AutoScroll="true"
                    Header="true"
                    >
                    <TopBar>
                        <ext:Toolbar runat="server" AnchorHorizontal="100" Height="30">
                            <Items>
                                <ext:ToolbarFill ID="ToolBarFill" />

                                <ext:Button runat="server"
                                    ID="btnAddPerformanceRuleItem"
                                    Icon="WorldAdd"
                                    Text="添加新规则段"
                                    Width="120"
                                    Height="22"
                                    StandOut="true"
                                    ArrowAlign="Right"
                                    StyleSpec="margin-right:15px;"
                                    >
                                    <Listeners>
                                        <Click Handler="btnAddPerformanceRuleItem_Click();"></Click>
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
                            Title="考试结果规则分段"
                            TitleAlign="Left"
                            Header="false"
                            DisableSelection="true"
                            AnchorHorizontal="100"
                            AutoScroll="true"
                            >
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

                                                <ext:ModelField Name="BEGIN_SCORE" Type="Float"/>

                                                <ext:ModelField Name="END_SCORE" Type="Float"/>

                                                <ext:ModelField Name="SEQUENCE" Type="String"/>

                                                <ext:ModelField Name="PERFORMANCE_RULES_ITEMS_DESC" Type="String"/>
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column ID="Column_ID" runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                                    <ext:Column ID="Column_BEGIN_SCORE"      runat="server" Text="起始分数" Align="Center" DataIndex="BEGIN_SCORE" Flex="1"/>
                                    <ext:Column ID="Column_END_SCORE"       runat="server" Text="结束分数" Align="Center" DataIndex="END_SCORE" Flex="1"/>
                                    <ext:Column ID="Column_SEQUENCE"       runat="server" Text="排列顺序" Align="Center" DataIndex="SEQUENCE" Flex="1"/>
                                    <ext:Column ID="Column_PERFORMANCE_RULES_ITEMS_DESC"   runat="server" Text="说明" Align="Center" DataIndex="PERFORMANCE_RULES_ITEMS_DESC" Flex="1"  />

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
                                <ext:CellSelectionModel runat="server" Mode="Single"></ext:CellSelectionModel>
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar
                                    runat="server"
                                    DisplayInfo="true"
                                    DisplayMsg="数据显示: 第{0} - {1}条, 共 {2} 条"
                                    EmptyMsg="暂无数据" />
                            </BottomBar>
                        </ext:GridPanel>
                        <ext:ToolTip
                            runat="server"
                            Target="={#{GridPanel1}.getView().el}"
                            Delegate=".x-grid-cell"
                            TrackMouse="true">
                            <Listeners>
                                <Show Handler="onShow(this, #{GridPanel1});" />
                            </Listeners>
                        </ext:ToolTip>
                    </Items>
            </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
