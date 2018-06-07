<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentManagement.aspx.cs" Inherits="ExamOnLine.Pages.OrganizationManagement.DepartmentManagement" %>
<%@ Register Assembly="Ext.Extension" Namespace="Ext.Extension.TreePanelEx" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>部门管理</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
   <%-- <script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
    <script src="../../Js/2_OrganizationManagement/DepartmentManagement.js"></script>
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
                                        <ucExt:TreePanelBaseExt
                                            runat="server"
                                            ID="departMent1"
                                            RootNodeID=""
                                            RootNodeText="Root"
                                            RootNodeVisible="false"
                                            OnNodeClick="departMent1_NodeClick"
                                            />
                                    </Content>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                        <ext:Panel runat="server" Region="North" Border="false" Height="35" Layout="ColumnLayout">
                            <Items>
                                <ext:ToolbarFill runat="server" Height="1" ColumnWidth="0.95" />
                                <ext:Button runat="server" Text="添加新部门" StyleSpec="margin-top:5px;" BaseCls="false" Cls="button_add_b">
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
                                        <ext:Container runat="server" AnchorHorizontal="100" Layout="ColumnLayout">
                                            <Items>
                                                <ext:Hidden ID="hidID" runat="server" />
                                                <ext:TextField ID="txtSearchDept" runat="server" FieldLabel="查询部门" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                                <ext:Button ID="btnSearchDept" runat="server" Text="查询" StyleSpec="margin-left:5px;" BaseCls="false" Cls="button_showsearch">
                                                    <DirectEvents>
                                                        <Click OnEvent="btnSearchDept_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                        </ext:Container>
                                        <ext:Hidden ID="hidParentID" runat="server" />
                                        <ext:TextField ID="txtDeptParent" runat="server" FieldLabel="所属部门" EmptyText="根节点" ReadOnly="true" Width="368" LabelAlign="Right" LabelStyle="color:#417ac1;" IndicatorIcon="Zoom">
                                            <DirectEvents>
                                                <IndicatorIconClick Before="" OnEvent="GetDepartment_Click"></IndicatorIconClick>
                                            </DirectEvents>
                                        </ext:TextField>
                                        <ext:TextField ID="txtDeptName" runat="server" FieldLabel="部门名称" AllowBlank="false" MaxLength="50" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                        <ext:TextField ID="txtFulPath" runat="server" FieldLabel="部门全路径" ReadOnly="true" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                        <ext:TextField ID="txtDeptSequence" runat="server" FieldLabel="排列顺序" MaxLength="30" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                                        <ext:TextArea ID="txtDeptDescript" runat="server" FieldLabel="部门描述" MaxLength="200" Height="150" Width="400" LabelAlign="Right" LabelStyle="color:#417ac1;" />
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
                                        <ucExt:WindowTreeBase runat="server" ID="departMent2"
                                            Hidden="true"
                                            Modal="true"
                                            RootNodeID=""
                                            RootNodeVisible="false" 
                                            OnNodeClick="departMent2_NodeClick"
                                            Title="" />
                                        </ucExt:WindowTreeBase>
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
