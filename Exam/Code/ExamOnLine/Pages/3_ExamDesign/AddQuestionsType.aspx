<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuestionsType.aspx.cs" Inherits="ExamOnLine.Pages.ExamDesign.AddQuestionsType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>题型增加/编辑/查看</title>

    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/3_ExamDesign/AddQuestionsType.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden runat="server" ID="hiddenOptype"></ext:Hidden>
        <ext:Hidden runat="server" ID="hiddenReturn"></ext:Hidden>
        <ext:Hidden runat="server" ID="hiddenID"></ext:Hidden>
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:FormPanel
                    ID="Panel1"
                    runat="server"
                    Layout="FitLayout"
                    Title="▏题型增加/编辑/查看"
                    Region="Center"
                    AutoScroll="true"
                    Header="true"
                    >
                    <TopBar>
                        <ext:Toolbar runat="server" AnchorHorizontal="100" Height="30">
                            <Items>
                                <ext:ToolbarFill ID="ToolBarFill" />
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
                                    <%--<DirectEvents>
                                        <Click Before="" After="" Success="" Failure="" Complete="" OnEvent="btnRefreshUser_Click"></Click>
                                    </DirectEvents>--%>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Container runat="server" Layout="AnchorLayout">
                            <Items>
                                <ext:ComboBox runat="server"
                                    ID="cmbQuestionTypeTemplate"
                                    FieldLabel="选择模板"
                                    LabelAlign="Right"
                                    DisplayField="TEMPLATE_NAME"
                                    ValueField="ID"
                                    LabelWidth="100"
                                    StyleSpec="margin:15px 0px 0px 30px;"
                                    Width="250"
                                    Editable="false"
                                    QueryMode="Local"
                                    AllowBlank="false"
                                    >
                                    <%--<Items>
                                        <ext:ListItem Index="0" Mode="Value" Text="单项选择题" Value="单项选择题"></ext:ListItem>
                                        <ext:ListItem Index="1" Mode="Value" Text="多项选择题" Value="多项选择题"></ext:ListItem>
                                        <ext:ListItem Index="2" Mode="Value" Text="判断题" Value="判断题"></ext:ListItem>
                                        <ext:ListItem Index="3" Mode="Value" Text="填空题" Value="填空题"></ext:ListItem>
                                    </Items>--%>
                                    <Store>
                                        <ext:Store ID="Store1" runat="server">
                                            <Model>
                                                <ext:Model ID="Model1" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ID" />
                                                        <ext:ModelField Name="TEMPLATE_NAME" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                </ext:ComboBox>

                                <ext:TextField ID="txtQuestionTypeName"
                                    runat="server"
                                    LabelWidth="100"
                                    LabelAlign="Right"
                                    FieldLabel="题型名称"
                                    MaxLength="30"
                                    Width="300"
                                    AllowBlank="false"
                                    StyleSpec="margin:15px 0px 0px 30px;">
                                </ext:TextField>

                                <ext:TextField ID="txtSequence"
                                    runat="server"
                                    LabelWidth="100"
                                    LabelAlign="Right"
                                    FieldLabel="排列顺序"
                                    MaxLength="30"
                                    Width="300"
                                    StyleSpec="margin:15px 0px 0px 30px;">
                                </ext:TextField>

                                <ext:NumberField ID="numScore"
                                    runat="server"
                                    MouseWheelEnabled="true"
                                    AllowDecimals="true"
                                    DecimalPrecision="2"
                                    MinValue="0"
                                    LabelWidth="100"
                                    LabelAlign="Right"
                                    FieldLabel="练习考试分数"
                                    Width="200"
                                   EmptyNumber="0"
                                    Text="0"
                                    StyleSpec="margin:15px 0px 0px 30px;">
                                    <Listeners>
                                        
                                    </Listeners>
                                </ext:NumberField>

                                <ext:Container 
                                    runat="server"
                                    Layout="ColumnLayout"
                                    >
                                    <Items>
                                            <ext:Button 
                                            runat="server" 
                                            ID="btnSave" 
                                            Text="保存" 
                                            Icon="TableSave"
                                            Width="100"
                                            Height="30"
                                            StyleSpec="margin:15px 0px 0px 135px;">
                                            <DirectEvents>
                                                <Click Before="return App.Panel1.isValid();" OnEvent="btnSave_Click" ></Click>
                                            </DirectEvents>
                                        </ext:Button>

                                            <ext:Button 
                                            runat="server" 
                                            ID="btnClear" 
                                            Text="清空" 
                                            Icon="PageCancel"
                                            Width="100"
                                            Height="30"
                                            StyleSpec="margin:15px 0px 0px 25px;">
                                            <DirectEvents>
                                                <Click ></Click>
                                            </DirectEvents>
                                                <Listeners>
                                                    <Click Handler="btnClear_Click();"></Click>
                                                </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                        
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
