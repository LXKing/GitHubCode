<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PositionManagement.aspx.cs" Inherits="ExamOnLine.Pages.OrganizationManagement.PositionManagement" %>
<%@ Register Src="~/Controls/TreePanel/TreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<%@ Register Src="../../Controls/TreePanel/BaseTreePanelExt.ascx" TagName="BaseTreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>岗位管理</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
    <script src="../../Js/2_OrganizationManagement/PositionManagement.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
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
                                <ext:Panel ID="contain" runat="server" AnchorVertical="100" Layout="FitLayout" Border="false">
                                    <Content>
                                        <ucExt:BaseTreePanelExt
                                            runat="server"
                                            ID="position1"
                                            RootNodeID=""
                                            RootNodeText="Root"
                                            RootNodeVisible="false"
                                            OnNodeClick="position1_NodeClick"
                                            />
                                    </Content>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                        <ext:Panel runat="server" Region="North" Border="false" Height="35" Layout="ColumnLayout">
                            <Items>
                                <ext:ToolbarFill runat="server" Height="1" ColumnWidth="0.95" />
                                <ext:Button runat="server" Text="添加新岗位" StyleSpec="margin-top:5px;" BaseCls="false" Cls="button_add_b">
                                    <DirectEvents>
                                        <Click OnEvent="Add_Click" />
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Panel>
                        <ext:Panel runat="server" Region="Center" Layout="FitLayout">
                            <Items>
                                <ext:FormPanel ID="formPanel" runat="server" Layout="FormLayout">
                                    <Defaults>
                                        <ext:Parameter Name="style" Value="margin: 10px 0px 0px 0px;" Mode="Value" />
                                    </Defaults>
                                    <Items>
                                        <ext:Container runat="server" AnchorHorizontal="100" Layout="ColumnLayout">
                                            <Items>
                                                <ext:Hidden ID="hidID" runat="server" />
                                                <ext:TextField ID="txtSearchPos" runat="server" FieldLabel="查询岗位" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                                <ext:Button ID="btnSearchPos" runat="server" Text="查询" StyleSpec="margin-left:5px;" BaseCls="false" Cls="button_showsearch">
                                                    <DirectEvents>
                                                        <Click OnEvent="btnSearchPos_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                        </ext:Container>
                                        <ext:Hidden ID="hidParentID" runat="server" />
                                        <ext:TextField ID="txtPosParent" runat="server" FieldLabel="所属分组" EmptyText="根节点" ReadOnly="true" Width="368" LabelAlign="Right" LabelStyle="color:#417ac1;" IndicatorIcon="Zoom">
                                            <DirectEvents>
                                                <IndicatorIconClick Before="" OnEvent="GetPosition_Click"></IndicatorIconClick>
                                            </DirectEvents>
                                        </ext:TextField>
                                        <ext:TextField ID="txtPosName" runat="server" FieldLabel="岗位名称" AllowBlank="false" MaxLength="30" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                        <ext:TextField ID="txtFulPath" runat="server" FieldLabel="岗位全路径" ReadOnly="true" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                        <ext:TextField ID="txtPosSequence" runat="server" FieldLabel="排列顺序" MaxLength="30" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                        <ext:TextArea ID="txtPosDescript" runat="server" FieldLabel="岗位描述" MaxLength="200" Height="150" Width="400" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                        <ext:Panel runat="server" Border="false" ButtonAlign="Left">
                                            <Buttons>
                                                <ext:Button runat="server" Text="保存" StyleSpec="margin-left: 100px;" BaseCls="false" Cls="button_infosave">
                                                    <DirectEvents>
                                                        <Click Before="return #{formPanel}.getForm().isValid();" OnEvent="Save_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                                <%--<ext:Button runat="server" Text="新增" BaseCls="false" Cls="button_add">
                                                    <DirectEvents>
                                                        <Click OnEvent="Add_Click" />
                                                    </DirectEvents>
                                                </ext:Button>--%>
                                                <ext:Button ID="btnDelete" runat="server" Text="删除" BaseCls="false" Cls="button_delete">
                                                    <DirectEvents>
                                                        <Click OnEvent="Delete_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Buttons>
                                        </ext:Panel>
                                    </Items>
                                    <Content>
                                        <ucExt:TreePanelExt runat="server" ID="position2"
                                            Hidden="true"
                                            Modal="true"
                                            RootNodeID=""
                                            RootNodeVisible="true" 
                                            OnNodeClick="position2_NodeClick"
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
