<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPerformanceRule.aspx.cs" Inherits="ExamOnLine.Pages.ExaminationManagement.AddPerformanceRule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>添加考试结果规则</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/4_ExaminationManagement/AddPerformanceRule.js"></script>
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
                        <ext:TextField ID="txtRuleName"
                                    runat="server"
                                    LabelWidth="100"
                                    LabelAlign="Right"
                                    FieldLabel="规则名称"
                                    MaxLength="30"
                                    InputWidth="150"
                                    AllowBlank="false"
                                    StyleSpec="margin:15px 0px 0px 30px;">
                                </ext:TextField>

                        <ext:TextField ID="txtSequence"
                                    runat="server"
                                    LabelWidth="100"
                                    LabelAlign="Right"
                                    FieldLabel="规则顺序"
                                    MaxLength="30"
                                    InputWidth="150"
                                    StyleSpec="margin:15px 0px 0px 30px;">
                                </ext:TextField>

                        <ext:TextArea
                                runat="server"
                                ID="txtRuleDescription"
                                FieldLabel="规则说明"
                                LabelAlign="Right"
                                LabelWidth="100"
                                InputWidth="280"
                                Height="100"
                                MaxLength="200"
                                StyleSpec="margin:15px 0px 0px 30px;"
                            >
                        </ext:TextArea>

                        <ext:Container runat="server" StyleSpec="margin:15px 0px 0px 135px;">
                            <Items>
                                <ext:Button runat="server" ID="btnSave" Height="30" Width="120" Text="保存" StandOut="true">
                                    <DirectEvents>
                                        <Click Before="return btnSave_Click_Before();" After="" Success="" Failure="" Complete="btnSave_Click_Complete();" OnEvent="btnSave_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" ID="btnReturn" Height="30" Width="120" Text="返回" StyleSpec="margin-left:39px;">
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
