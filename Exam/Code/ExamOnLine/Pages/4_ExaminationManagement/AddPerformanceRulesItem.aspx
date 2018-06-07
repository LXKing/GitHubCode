<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPerformanceRulesItem.aspx.cs" Inherits="ExamOnLine.Pages.ExaminationManagement.AddPerformanceRulesItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/4_ExaminationManagement/AddPerformanceRulesItem.js"></script>
</head>
<body>
    <form id="form1" runat="server">
          <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:FormPanel
                    ID="Panel1"
                    runat="server"
                    Layout="FormLayout"
                    Title="▏考试规则增加/编辑/查看"
                    Region="Center"
                    AutoScroll="true"
                    Header="true"
                    >
                    <Items>
                        <ext:NumberField ID="numBeginScore"
                                    runat="server"
                                    LabelWidth="100"
                                    LabelAlign="Right"
                                    FieldLabel="起始分数"
                                    MaxLength="30"
                                    InputWidth="150"
                                    AllowBlank="false"
                                    DecimalPrecision="2"
                                    Text="0"
                                    MinValue="0"
                                    MaxValue="1000"
                                    StyleSpec="margin:15px 0px 0px 30px;">
                                    <Listeners>
                                        <Change Handler="numBeginScore_Change(this, newValue, oldValue) ;"></Change>
                                    </Listeners>
                                </ext:NumberField>

                        <ext:NumberField ID="numEndScore"
                                    runat="server"
                                    LabelWidth="100"
                                    LabelAlign="Right"
                                    FieldLabel="结束分数"
                                    MaxLength="30"
                                    InputWidth="150"
                                    AllowBlank="false"
                                    DecimalPrecision="2"
                                    Text="0"
                                    MinValue="0"
                                    MaxValue="1000"
                                    StyleSpec="margin:15px 0px 0px 30px;">
                                    <Listeners>
                                        <Change Handler="numEndScore_Change(this, newValue, oldValue) ;"></Change>
                                    </Listeners>
                                </ext:NumberField>

                        <ext:TextField ID="txtSequence"
                                    runat="server"
                                    LabelWidth="100"
                                    LabelAlign="Right"
                                    FieldLabel="排列顺序"
                                    MaxLength="30"
                                    InputWidth="150"
                                    AllowBlank="false"
                                    StyleSpec="margin:15px 0px 0px 30px;">
                                </ext:TextField>

                        <ext:HtmlEditor
                            ID="htmlDescription"
                            runat="server"
                            FieldLabel="备注"
                            LabelAlign="Right"
                            LabelWidth="100"
                            Width="600"
                            Height="200"
                            EnableAlignments="false"
                            EnableFontSize="false"
                            CreateLinkText="My CreateLinkText"
                            StyleSpec="margin:15px 0px 0px 30px;">
                            <ButtonTips>
                                <BackColor Text="My BackColor Tip" />
                                <Bold Text="My Bold Tip" />
                            </ButtonTips>
                        </ext:HtmlEditor>

                        <ext:Container runat="server" Layout="ColumnLayout" StyleSpec="margin:5px 0px 0px 130px;">
                            <Items>
                                <ext:Button runat="server" 
                                                    ID="btnSave"
                                                    Width="120"
                                                    Height="30"
                                                    Text="保存数据"
                                                    StyleSpec="margin:5px 0px 0px 0px;"
                                                    StandOut="true"
                                                    Icon="PageSave"
                                                    >
                                    <DirectEvents>
                                        <Click Before="return btnSave_Before();" After="" Success="" Failure="" Complete="btnSave_Complete();" OnEvent="btnSave_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" 
                                                    ID="btnReturn1"
                                                    Width="120"
                                                    Height="30"
                                                    Text="返回"
                                                    StyleSpec="margin:5px 0px 0px 20px;"
                                                    StandOut="false"
                                                    Icon="PageBack"
                                                    >
                                                    <Listeners>
                                                        <Click Handler="btnReturn_Click();"></Click>
                                                    </Listeners>
                                                </ext:Button>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
