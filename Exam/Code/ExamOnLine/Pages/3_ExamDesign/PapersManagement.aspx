<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PapersManagement.aspx.cs" Inherits="ExamOnLine.Pages.ExamDesign.PapersManagement" %>
<%@ Register Src="~/Controls/TreePanel/TreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>试卷管理</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/3_ExamDesign/PapersManagement.js"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Hidden runat="server" ID="hidPaperFieldID" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:Panel 
                    runat="server"
                    Layout="FitLayout"
                    Title="▏试卷管理"
                    AutoScroll="true"
                    >
                    <TopBar>
                        <ext:Toolbar runat="server" Height="35">
                            <Items>
                                <ext:ToolbarFill ID="ToolBarFill" />
                                <ext:Button runat="server"
                                    ID="btnAddPaper"
                                    Text="添加试卷"
                                    BaseCls="false"
                                    Cls="button_add"  
                                    >
                                    <Listeners>
                                        <Click Handler="btnAddPaper_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
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
                                <ext:Toolbar runat="server" Height="35" ID="Toolbar_Search" Hidden="false" Layout="FormLayout">
                                    <Items>
                                        <ext:Container runat="server" 
                                            Layout="ColumnLayout">
                                            <Defaults>
                                                <ext:Parameter Name="LabelWidth" Value="80" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label runat="server" 
                                                    Text="快速筛选" 
                                                    Icon="Magnifier"
                                                    StyleSpec="margin-left:5px;"
                                                    ></ext:Label>
                                                <ext:ComboBox runat="server" 
                                                    ID="cmbPaperType" 
                                                    FieldLabel="试卷类型" 
                                                    LabelAlign="Right" 
                                                    DisplayField="NAME"
                                                    ValueField="ID"
                                                    Width="200"
                                                    AutoSelect="true"
                                                    EmptyText="试卷类型" 
                                                    Editable="false"
                                                    >
                                                    <Items>
                                                        <ext:ListItem Index="0" Text="在线考试" Value="39169d1e-0d06-67cd-f218-f94b29420af2"></ext:ListItem>
                                                        <ext:ListItem Index="1" Text="网上作业" Value="eb55a5f5-d471-a2a4-f4e9-6549052da334"></ext:ListItem>
                                                        <ext:ListItem Index="2" Text="在线练习" Value="59c4571d-bb96-b66a-d794-92358c14173f"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                                <ext:ComboBox runat="server" 
                                                    ID="cmbMakeWay" 
                                                    FieldLabel="出题方式" 
                                                    LabelAlign="Right" 
                                                    DisplayField="NAME" 
                                                    ValueField="ID"
                                                    Width="200"
                                                    AutoSelect="true"
                                                    EmptyText="出题方式" 
                                                    Editable="false"
                                                    >
                                                    <Items>
                                                        <ext:ListItem Index="0" Text="固定试卷(手工)" Value="5d9ce76c-5896-541f-b7b1-6b31ee04c1a2"></ext:ListItem>
                                                        <ext:ListItem Index="1" Text="固定试卷(随机)" Value="0785ce6b-3c78-4ac8-e292-a1bb251c0cf5"></ext:ListItem>
                                                        <ext:ListItem Index="2" Text="随机试卷" Value="997bded7-fb08-912b-adf7-b87f76db6d4a"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                                <ext:TextField ID="txtPaperField"
                                                    runat="server"
                                                    LabelAlign="Right"
                                                    FieldLabel="试卷分类"
                                                    Width="220"
                                                    ReadOnly="true"
                                                    IndicatorIcon="Zoom"
                                                    >
                                                    <DirectEvents>
                                                        <IndicatorIconClick Before="" OnEvent="GetPaperField_Click"></IndicatorIconClick>
                                                    </DirectEvents>
                                                </ext:TextField>
                                                <ext:TextField ID="txtPaperName"
                                                    runat="server"
                                                    LabelAlign="Right"
                                                    FieldLabel="试卷名称"
                                                    Width="220"
                                                    >
                                                </ext:TextField>
                                                <ext:Button runat="server" Text="搜索" 
                                                    StyleSpec="margin-left:5px;" 
                                                    BaseCls="false" 
                                                    Cls="button_showsearch">
                                                    <Listeners>
                                                        <Click Handler="btnSearch_Click();"></Click>
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
                                                <ext:ModelField Name="EXAM_TYPE_NAME" Type="String"/>
                                                <ext:ModelField Name="PAPER_TYPE_TEXT" Type="String" />
                                                <ext:ModelField Name="MAKE_QUESTION_TYPE_TEXT" Type="String" />
                                                <ext:ModelField Name="PAPER_NAME" Type="String" />
                                                <ext:ModelField Name="CREATE_DATE" Type="Date" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel runat="server">
                                <Columns>
                                    <ext:Column runat="server" Text="ID" Align="Center" DataIndex="ID" Visible="false" />
                                    <ext:Column runat="server" Text="考试类别" Align="Center" DataIndex="EXAM_TYPE_NAME" Flex="1"/>
                                    <ext:Column runat="server" Text="试卷类型" Align="Center" DataIndex="PAPER_TYPE_TEXT" Flex="1"/>
                                    <ext:Column runat="server" Text="出题方式"   Align="Center" DataIndex="MAKE_QUESTION_TYPE_TEXT" Flex="1"/>
                                    <ext:Column runat="server" Text="试卷名称"   Align="Center" DataIndex="PAPER_NAME" Flex="1"/>
                                    <ext:DateColumn runat="server" Text="创建时间"   Align="Center" DataIndex="CREATE_DATE" Flex="1" Format="Y-m-d h:m:s"/>
                                    
                                    <ext:CommandColumn ID="CommandColumn_Manage" runat="server" Text="题型管理" Align="Center" Flex="1">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Application" CommandName="viewQuestion" MinWidth="22">
                                                <ToolTip Text="题型管理" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <Listeners>
                                            <Command Handler="CommandColumn_Manage_Command(item, command, record, recordIndex, cellIndex);">
                                            </Command>
                                        </Listeners>
                                    </ext:CommandColumn>

                                    <ext:CommandColumn ID="CommandColumn2" runat="server" Text="导出试卷" Align="Center" Flex="1">
                                        <Commands>
                                            <ext:CommandFill></ext:CommandFill>
                                            <ext:GridCommand Icon="Application" CommandName="viewQuestion" MinWidth="22">
                                                <ToolTip Text="导出试卷" />
                                            </ext:GridCommand>
                                            <ext:CommandFill></ext:CommandFill>
                                        </Commands>
                                        <Listeners>
                                            <Command Handler="CommandColumn_View_Command(item, command, record, recordIndex, cellIndex);">
                                            </Command>
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
                <ext:Container runat="server">
                    <Content>
                        <ucExt:TreePanelExt runat="server" 
                            ID="trpnlPaperField"
                            Hidden="true"
                            Modal="true"
                            RootNodeID=""
                            RootNodeText="根节点"
                            Title="试卷涉及领域选择"
                            RootNodeVisible="false" OnSelectedChange="trpnlPaperField_SelectedChange"
                             />
                    </Content>
                </ext:Container>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
