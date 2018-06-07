<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPapersQuestionType.aspx.cs" Inherits="ExamOnLine.Pages.ExamDesign.AddPapersQuestionType" %>

<%@ Register Src="~/Controls/TreePanel/MulSelectTreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加题型</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
    <script src="../../Js/3_ExamDesign/AddPapersQuestionType.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" />
        <ext:Hidden ID="hidIsAdd" runat="server" />
        <ext:Hidden ID="hidPaperID" runat="server" />
        <ext:Hidden ID="hidPaperQuetionTypeID" runat="server" />
        <ext:Hidden ID="hidQuestionTypeID" runat="server" />
        <ext:Hidden ID="hidKnowledgeID" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout" AutoScroll="true">
            <Items>
                <ext:FormPanel ID="formPanel" runat="server">
                    <Defaults>
                        <ext:Parameter Name="style" Value="margin: 10px 0px 0px 0px;" Mode="Value" />
                    </Defaults>
                    <Items>
                        <ext:ComboBox ID="cmbType" runat="server" FieldLabel="题型" Editable="false" DisplayField="QUESTION_TYPE_NAME" ValueField="ID" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;">
                            <DirectEvents>
                                <Change OnEvent="cmbType_Change" />
                            </DirectEvents>
                            <Store>
                                <ext:Store runat="server">
                                    <Model>
                                        <ext:Model runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ID" />
                                                <ext:ModelField Name="QUESTION_TYPE_NAME" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                        </ext:ComboBox>
                        <ext:TextField ID="txthead" runat="server" FieldLabel="标题" AllowBlank="false" MaxLength="50" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                        <ext:TextArea ID="txtsubhead" runat="server" FieldLabel="副标题" MaxLength="200" Height="100" Width="450" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                        <ext:TextField ID="txtSequence" runat="server" FieldLabel="排序序号" MaxLength="50" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                        <ext:NumberField  ID="txtScore" runat="server" AllowDecimals="true" DecimalPrecision="2" MinValue="0" MaxValue="100" AllowBlank="false" FieldLabel="每题分数" Width="350" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                        <ext:TextField ID="txtKnowledge" runat="server" FieldLabel="抽题领域" AllowBlank="false" EmptyText="抽题领域" ReadOnly="true" Width="368" LabelAlign="Right" LabelStyle="color:#417ac1;" IndicatorIcon="Zoom">
                            <DirectEvents>
                                <IndicatorIconClick Before="" OnEvent="GetKnowledge_Click"></IndicatorIconClick>
                            </DirectEvents>
                        </ext:TextField>
                        <ext:Container runat="server" AnchorHorizontal="100" Layout="ColumnLayout">
                            <Items>
                                <ext:Label runat="server" Text="难度分布:" StyleSpec="margin-left:47px;color:#417ac1;" />
                                <ext:NumberField ID="txtLow" runat="server" MinValue="0" FieldLabel="低" LabelWidth="20" Width="80" LabelAlign="Right" />
                                <ext:Label runat="server" Text="/" />
                                <ext:Label ID="lblLow" runat="server" Text="0" StyleSpec="color:red;" />
                                <ext:NumberField ID="txtMiddle" runat="server" MinValue="0" FieldLabel="中" LabelWidth="20" Width="80" LabelAlign="Right" />
                                <ext:Label runat="server" Text="/" />
                                <ext:Label ID="lblMiddle" runat="server" Text="0" StyleSpec="color:red;" />
                                <ext:NumberField ID="txtHight" runat="server" MinValue="0" FieldLabel="高" LabelWidth="20" Width="80" LabelAlign="Right" />
                                <ext:Label runat="server" Text="/" />
                                <ext:Label ID="lblHight" runat="server" Text="0" StyleSpec="color:red;" />
                            </Items>
                        </ext:Container>
                        <ext:TextArea ID="txtDescript" runat="server" FieldLabel="说明" MaxLength="200" Height="100" Width="450" LabelAlign="Right" LabelStyle="color:#417ac1;" />
                        <ext:Panel runat="server" Border="false" ButtonAlign="Left">
                            <Buttons>
                                <ext:Button ID="btnSave" runat="server" Text="保存数据" StyleSpec="margin-left: 100px;" BaseCls="false" Cls="button_infosave">
                                    <DirectEvents>
                                        <Click Before="return #{formPanel}.getForm().isValid();" OnEvent="btnSave_Click" Complete="refreshData();" />
                                    </DirectEvents>
                                </ext:Button>
<%--                                <ext:Button ID="btnDelete" runat="server" Text="取消" BaseCls="false" Cls="button_delete">
                                    <DirectEvents>
                                        <Click />
                                    </DirectEvents>
                                </ext:Button>--%>
                            </Buttons>
                        </ext:Panel>
                    </Items>
                    <Content>
                        <ucExt:TreePanelExt runat="server" ID="tplKnowledge"
                            RootNodeID=""
                            RootNodeText ="根节点"
                            Hidden="true"
                            Modal="true"
                            Title="管理题型信息"
                            RootNodeVisible="false"
                            OnSubmittedNode="tplKnowledge_SubmittedNode" />
                    </Content>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
