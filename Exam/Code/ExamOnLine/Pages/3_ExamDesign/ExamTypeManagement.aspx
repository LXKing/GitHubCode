<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamTypeManagement.aspx.cs" Inherits="ExamOnLine.Pages.ExamDesign.ExamTypeManagement" %>
<%@ Register Src="../../Controls/TreePanel/TreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<%@ Register Src="../../Controls/TreePanel/BaseTreePanelExt.ascx" TagName="BaseTreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考试分类管理</title>

    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/3_ExamDesign/ExamTypeManagement.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden runat="server" ID="hidden_ParentID"></ext:Hidden>
        <ext:Hidden runat="server" ID="hidden_AddOrUpdate" Text="a"></ext:Hidden>
        <ext:Hidden runat="server" ID="hidden_CurrentID"></ext:Hidden>
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
            <Items>
                <ext:Panel runat="server" ID="header" Title="▏考试类别管理" TitleAlign="Left" Height="55" Region="North" Layout="BorderLayout">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="toolBar" >
                            <Items>
                                <ext:ToolbarFill />
                                <ext:Button
                                    ID="Button2" 
                                    runat="server"
                                    Text="添加新类别"
                                    Height="22"
                                    Width="90"
                                    StyleSpec="margin-right:15px;"
                                    Region="East"
                                    StandOut="true"
                                    >
                                    <Listeners>
                                        <Click Handler="btnClear_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                </ext:Panel>

                <ext:Panel 
                    runat="server" 
                    Header="true"
                    Title="考试分类列表"
                    TitleAlign="Center"
                    ID="panelTree" 
                    Region="West" 
                    Width="200" 
                    AutoScroll="true"  
                    Layout="FitLayout" 
                    StyleSpec="">
                            <Content>
                                <ucExt:BaseTreePanelExt
                                    runat="server"
                                    ID="treePanelExamTypeList"
                                    RootNodeID=""
                                    RootNodeText="Root"
                                    RootNodeVisible="false"
                                    OnNodeClick="treePanelExamTypeList_NodeClick"
                                    StyleSpec="border-color: green;border-style: groove;background-color:green;"/>
                            </Content>
                        </ext:Panel>

                <ext:Panel runat="server" ID="panel1" Region="Center" Layout="FormLayout" StyleSpec="">
                    <Items>
                        <ext:TextField
                            runat="server"
                            ID="txtExamTypeParent"
                            FieldLabel="所属类别"
                            LabelAlign="Right"
                            LabelWidth="90"
                            InputWidth="180"
                            IndicatorIcon="Find"
                            ReadOnly="true"
                            StyleSpec="margin-top:10px;"
                            >
                            <DirectEvents>
                                <IndicatorIconClick Before="" After="" Success="" Failure="" Complete="" OnEvent="txtExamTypeParent_IndicatorIconClick"></IndicatorIconClick>
                            </DirectEvents>
                        </ext:TextField>
                        <ext:TextField
                            runat="server"
                            ID="txtExamTypeName"
                            FieldLabel="类别名称"
                            LabelAlign="Right"
                            LabelWidth="90"
                            InputWidth="180"
                            MaxLength="30"
                            AllowBlank="false"
                            StyleSpec="margin-top:10px;"
                            >
                        </ext:TextField>
                        <ext:TextField
                            runat="server"
                            ID="txtSequence"
                            FieldLabel="排列顺序"
                            LabelAlign="Right"
                            LabelWidth="90"
                            InputWidth="180"
                         MaxLength="30"
                            StyleSpec="margin-top:10px;"
                            >
                        </ext:TextField>
                        <ext:TextArea
                                runat="server"
                                ID="txtExamTypeDesc"
                                FieldLabel="类别描述"
                                LabelAlign="Right"
                                LabelWidth="90"
                                InputWidth="280"
                                Height="100"
                                MaxLength="200"
                                StyleSpec="margin-top:10px;"
                            >
                        </ext:TextArea>
                        <ext:Container runat="server" StyleSpec="margin-top:10px;">
                            <Items>
                                <ext:Button
                                    ID="btnSave" 
                                    runat="server"
                                    Text="保存"
                                    Height="22"
                                    Width="90"
                                     StyleSpec="margin-left:95px;"
                                    >
                                    <DirectEvents>
                                        <Click Before="return btnSave_Before();" After="" Success="" Failure="" Complete="" OnEvent="btnSave_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button
                                    ID="btnCancel" 
                                    runat="server"
                                    Text="删除"
                                    Height="22"
                                    Width="90"
                                     StyleSpec="margin-left:15px;"
                                    >
                                    <DirectEvents>
                                        <Click Before="return btnCancel_Before();" Success="" Failure="" Complete="" OnEvent="btnCancel_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button
                                    ID="btnClear" 
                                    runat="server"
                                    Text="清空"
                                    Height="22"
                                    Width="90"
                                     StyleSpec="margin-left:15px;"
                                    >
                                    <Listeners>
                                        <Click Handler="btnClear_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
        <ext:Container runat="server">
            <Content>
                <ucExt:TreePanelExt runat="server" 
                    ID="treePanelExamTypeWin"
                    Hidden="true"
                    Modal="true"
                    RootNodeID=""
                    RootNodeText="根节点"
                    Title="考试分类选择"
                    RootNodeVisible="false"
                    OnNodeClick="treePanelExamTypeWin_NodeClick"
                        />
            </Content>
        </ext:Container>
    </form>
</body>
</html>
