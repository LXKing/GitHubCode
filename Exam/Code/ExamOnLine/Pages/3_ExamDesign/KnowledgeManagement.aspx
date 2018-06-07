<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KnowledgeManagement.aspx.cs" Inherits="ExamOnLine.Pages.ExamDesign.KnowledgeManagement" %>
<%@ Register Src="~/Controls/TreePanel/TreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<%@ Register Src="../../Controls/TreePanel/BaseTreePanelExt.ascx" TagName="BaseTreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>知识点管理</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
    <script src="../../Js/3_ExamDesign/KnowledgeManagement.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Hidden ID="hidID" runat="server" />
        <ext:Hidden ID="hidIsAdd" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout" AutoScroll="true">
            <Items>
                <ext:Panel runat="server" Flex="1" Layout="BorderLayout">
                    <Items>
                        <ext:Panel runat="server" Region="West" Width="200" Layout="AnchorLayout" AutoScroll="true">
                            <Items>
                                <ext:Button ID="btnExpandAll" runat="server" Text="折叠全部" TextAlign="Left" AnchorHorizontal="100">
                                    <Listeners>
                                        <Click Handler="expandAll();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Panel ID="contain" runat="server" AnchorVertical="100" Layout="AnchorLayout" Border="false">
                                    <Content>
                                        <ucExt:BaseTreePanelExt
                                            runat="server"
                                            ID="knowledge1"
                                            RootNodeID=""
                                            RootNodeText="Root"
                                            RootNodeVisible="false"
                                            OnNodeClick="knowledge1_NodeClick"
                                             />
                                    </Content>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                        <ext:Panel runat="server" Region="North" Border="false" Height="35" Layout="ColumnLayout">
                            <Items>
                                <ext:ToolbarFill runat="server" Height="1" ColumnWidth="0.95" />
                                <ext:Button runat="server" Text="添加知识点" StyleSpec="margin-top:5px;" BaseCls="false" Cls="button_add_b">
                                    <DirectEvents>
                                        <Click OnEvent="Add_Click" />
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Panel>
                        <ext:Panel runat="server" Region="Center" Layout="FitLayout">
                            <Items>
                                <ext:FormPanel ID="formPanel" runat="server">
                                    <Defaults>
                                        <ext:Parameter Name="style" Value="margin: 10px 0px 0px 0px;" Mode="Value" />
                                    </Defaults>
                                    <Items>
                                        <ext:Hidden ID="hidParentID" runat="server" />
                                        <ext:TextField ID="txtKnowParent" runat="server" FieldLabel="所属知识体系" EmptyText="根节点" ReadOnly="true" Width="368" LabelAlign="Right" LabelStyle="color:#417ac1;" IndicatorIcon="Zoom">
                                            <DirectEvents>
                                                <IndicatorIconClick Before="" OnEvent="GetKnowledge_Click"></IndicatorIconClick>
                                            </DirectEvents>
                                        </ext:TextField>
                                        <ext:TextField ID="txtKnowName" runat="server" FieldLabel="知识体系名称" AllowBlank="false" MaxLength="30" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                        <ext:TextField ID="txtKnowSequence" runat="server" FieldLabel="排列顺序" MaxLength="30" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                        <ext:TextArea ID="txtKnowDescript" runat="server" FieldLabel="知识体系描述" MaxLength="200" Height="150" Width="400" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                        <ext:Panel runat="server" Border="false" ButtonAlign="Left">
                                            <Buttons>
                                                <ext:Button runat="server" Text="保存" StyleSpec="margin-left: 100px;" BaseCls="false" Cls="button_infosave">
                                                    <DirectEvents>
                                                        <Click Before="return #{formPanel}.getForm().isValid();" OnEvent="Save_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Button ID="btnDelete" runat="server" Text="删除" BaseCls="false" Cls="button_delete">
                                                    <DirectEvents>
                                                        <Click OnEvent="Delete_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Buttons>
                                        </ext:Panel>
                                    </Items>
                                    <Content>
                                        <ucExt:TreePanelExt runat="server" ID="knowledge2"
                                            Hidden="true"
                                            Modal="true"
                                            RootNodeID=""
                                            RootNodeVisible="false" 
                                         OnNodeClick="knowledge2_NodeClick"
                                            Title="" />
                                    </Content>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
