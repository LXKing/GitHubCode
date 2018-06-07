<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddExamsPlan.aspx.cs" Inherits="ExamOnLine.Pages.ExaminationManagement.AddExamsPlan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>添加考试安排</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <script src="../../Js/4_ExaminationManagement/AddExamsPlan.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden runat="server" ID="hidden_PaperID"></ext:Hidden>
        <ext:Hidden runat="server" ID="hidden_PerformanceRole"></ext:Hidden>
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
            <Items>
                <ext:Panel
                    ID="Panel1"
                    runat="server"
                    Layout="FormLayout"
                    Title="▏添加考试安排"
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
                                    Icon="WorldDawn"
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
                        <ext:TextField ID="txtExamPlanName"
                                                    runat="server"
                                                    LabelWidth="60"
                                                    LabelAlign="Right"
                                                    FieldLabel="安排名称"
                                                    InputWidth="400"
                                                    AllowBlank="false"
                                                    MaxLength="200"
                                                    StyleSpec="margin:5px 0px 0px 30px;"
                                                    >
                                                </ext:TextField>

                        <ext:Container runat="server" Layout="ColumnLayout" StyleSpec="margin:5px 0px 0px 30px;">
                            <Items>
                                <ext:NumberField 
                                    ID="numExamTime" 
                                    runat="server" 
                                    FieldLabel="考试时间"
                                    LabelAlign="Right"
                                    LabelWidth="60"
                                    InputWidth="80"
                                    Text="30"
                                    MinValue="30"
                                    MaxValue="1000"
                                    AllowDecimals="false"
                                    DecimalPrecision="1"
                                    Step="1"
                                    IndicatorIcon="Information"
                                    IndicatorTip="鼠标滚轮滚动可切换数字!"
                                />
                                <ext:NumberField 
                                    ID="numAllowJoinCounts" 
                                    runat="server" 
                                    FieldLabel="参加次数"
                                    LabelAlign="Right"
                                    LabelWidth="60"
                                    InputWidth="80"
                                    Text="1"
                                    MinValue="1"
                                    MaxValue="100"
                                    AllowDecimals="false"
                                    DecimalPrecision="1"
                                    Step="1"
                                    StyleSpec="margin-left:20px;"
                                    IndicatorIcon="Information"
                                    IndicatorTip="鼠标滚轮滚动可切换数字!"
                                />
                                <ext:NumberField 
                                    ID="numPaperTotalScore" 
                                    runat="server" 
                                    FieldLabel="试卷分数"
                                    LabelAlign="Right"
                                    LabelWidth="60"
                                    InputWidth="80"
                                    Text="100.00"
                                    MinValue="1"
                                    
                                    AllowDecimals="true"
                                    DecimalPrecision="2"
                                    Step="1"
                                    StyleSpec="margin-left:20px;"
                                    IndicatorIcon="Information"
                                    IndicatorTip="鼠标滚轮滚动可切换数字!">
                                    <Listeners>
                                        <Change Handler="numPaperTotalScore_Change(this, newValue, oldValue);"></Change>
                                    </Listeners>
                                </ext:NumberField>
                                <ext:NumberField 
                                    ID="numPassMinScore" 
                                    runat="server" 
                                    FieldLabel="通过分数"
                                    LabelAlign="Right"
                                    LabelWidth="60"
                                    InputWidth="80"
                                    Text="60"
                                    MinValue="1"
                                    AllowDecimals="true"
                                    DecimalPrecision="2"
                                    Step="1"
                                    StyleSpec="margin-left:20px;"
                                    IndicatorIcon="Information"
                                    IndicatorTip="鼠标滚轮滚动可切换数字!">
                                    <Listeners>
                                        <Change Handler="numPassMinScore_Change(this,  newValue,  oldValue);"></Change>
                                    </Listeners>
                                </ext:NumberField>
                            </Items>
                        </ext:Container>

                        <ext:Container runat="server" Layout="ColumnLayout" StyleSpec="margin:5px 0px 0px 30px;">
                            <Items>
                                <ext:DateField runat="server"
                                        ID="dateExamBegin"
                                        FieldLabel="起始时间"
                                        LabelWidth="60"
                                        LabelAlign="Right"
                                        AllowBlank="false"
                                        InputWidth="150"
                                        Format="yyyy-MM-dd HH:mm:ss"
                                    />
                                <ext:DateField runat="server"
                                        ID="dateExamEnd"
                                        FieldLabel="结束时间"
                                        LabelWidth="60"
                                        LabelAlign="Right"
                                        AllowBlank="false"
                                        InputWidth="150"
                                        Format="yyyy-MM-dd HH:mm:ss"
                                    StyleSpec="margin-left:151px;"
                                    />
                            </Items>
                        </ext:Container>

                        <ext:Container runat="server" Layout="ColumnLayout" StyleSpec="margin:5px 0px 0px 30px;">
                            <Items>
                                <ext:Label runat="server" ID="labArrangementOptions" MinWidth="60" Text="安排选项:" StyleSpec="margin-top:2px;margin-left:9px;"></ext:Label>
                                <ext:RadioGroup runat="server" ID="grpScorePublicType">
                                    <Items>
                                        <ext:Radio runat="server" ID="radShowScoreNow" FieldLabel="即时显示分数" LabelAlign="Right" LabelWidth="100" InputValue="0" BoxLabelAlign="Before" Checked="true"></ext:Radio>
                                        <ext:Radio runat="server" ID="radSettingPublicDate" FieldLabel="指定成绩发布时间" LabelAlign="Right" LabelWidth="100" InputValue="1" BoxLabelAlign="Before" StyleSpec="margin-left:50px;">
                                            <Listeners>
                                                <Change Handler="activate(this )"></Change>
                                            </Listeners>
                                        </ext:Radio>
                                        <ext:DateField runat="server"
                                        ID="dateScorePublic"
                                        AllowBlank="false"
                                        InputWidth="150"
                                        Format="yyyy-MM-dd HH:mm:ss"
                                        StyleSpec="margin-left:20px;"
                                         Disabled="true"
                                    />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:Container>

                        <ext:Container runat="server" Layout="ColumnLayout" StyleSpec="margin:5px 0px 0px 30px;">
                            <Items>
                                <ext:TextField ID="txtPerformanceRole"
                                        runat="server"
                                        LabelWidth="60"
                                        LabelAlign="Right"
                                        FieldLabel="成绩规则"
                                        InputWidth="400"
                                        AllowBlank="true"
                                        ReadOnly="true"
                                        StyleSpec="margin:5px 0px 0px 0px;"
                                        IndicatorIcon="ApplicationEdit"
                                        IndicatorTip="点击选择成绩规则，不选择不启用结果显示规则,双击清除选择!"
                                        >
                                <Listeners>
                                <IndicatorIconClick Handler="txtPerformanceRole_IndicatorIconClick();"></IndicatorIconClick>
                                </Listeners>
                                    </ext:TextField>
                                <ext:Button runat="server" ID="clearPerformanceRules" Text="清空" StyleSpec="margin-left:10px;margin-top:5px;">
                                    <Listeners>
                                        <Click Handler="clearPerformanceRules_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Container>
                        
                        <ext:Container runat="server" Layout="ColumnLayout" StyleSpec="margin:5px 0px 0px 30px;">
                            <Items>
                                <ext:Label runat="server" ID="lab" MinWidth="60" Text="试卷模式:" StyleSpec="margin-top:2px;margin-left:9px;"></ext:Label>
                                <ext:RadioGroup runat="server" ID="grpPaperModem">
                                    <Items>
                                        <ext:Radio runat="server" ID="radSingleModem" FieldLabel="单题模式" LabelAlign="Right" LabelWidth="60" InputValue="0" BoxLabelAlign="Before" Checked="true"></ext:Radio>
                                        <ext:Radio runat="server" ID="radWholeModem" FieldLabel="整卷模式" LabelAlign="Right" LabelWidth="70" InputValue="1" BoxLabelAlign="Before" StyleSpec="margin-left:50px;">
                                            <Listeners>
                                                <%--<Change Handler="activate(this )"></Change>--%>
                                            </Listeners>
                                        </ext:Radio>
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:Container>

                        <ext:Container runat="server" Layout="ColumnLayout" StyleSpec="margin:5px 0px 0px 10px;">
                            <Items>
                                <ext:Label runat="server" ID="labJudgeRule" MinWidth="80" Text="多选判分规则:" StyleSpec="margin-top:2px;margin-left:9px;"></ext:Label>
                                <ext:RadioGroup runat="server" ID="grpMultJudgeRules">
                                    <Items>
                                        <ext:Radio runat="server" ID="radAllRightRules" FieldLabel="全对给分" LabelAlign="Right" LabelWidth="60" InputValue="0" BoxLabelAlign="Before" Checked="true"></ext:Radio>
                                        <ext:Radio runat="server" ID="radAynRightRules" FieldLabel="按比例给分" LabelAlign="Right" LabelWidth="70" InputValue="1" BoxLabelAlign="Before" StyleSpec="margin-left:50px;">
                                            <Listeners>
                                                <%--<Change Handler="activate(this )"></Change>--%>
                                            </Listeners>
                                        </ext:Radio>
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:Container>

                        <ext:Container runat="server" Layout="ColumnLayout" StyleSpec="margin:5px 0px 0px 10px;">
                            <Items>
                                <ext:Label runat="server" ID="Label1" MinWidth="80" Text="考完能否阅卷:" StyleSpec="margin-top:2px;margin-left:9px;"></ext:Label>
                                <ext:RadioGroup runat="server"  ID="grpIsAllowedView">
                                    <Items>
                                        <ext:Radio runat="server" ID="radAllowView" FieldLabel="可以查看" LabelAlign="Right" LabelWidth="60" InputValue="0" BoxLabelAlign="Before" Checked="true"></ext:Radio>
                                        <ext:Radio runat="server" ID="radForbidView" FieldLabel="禁止查看" LabelAlign="Right" LabelWidth="70" InputValue="1" BoxLabelAlign="Before" StyleSpec="margin-left:50px;">
                                            <Listeners>
                                                <%--<Change Handler="activate(this )"></Change>--%>
                                            </Listeners>
                                        </ext:Radio>
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:Container>

                        <ext:TextField ID="txtPaperName"
                                        runat="server"
                                        LabelWidth="60"
                                        LabelAlign="Right"
                                        FieldLabel="选择试卷"
                                        InputWidth="400"
                                        AllowBlank="false"
                                        ReadOnly="true"
                                        StyleSpec="margin:5px 0px 0px 30px;"
                                        IndicatorIcon="ApplicationEdit"
                                        IndicatorTip="必须选择一套对应的试卷!"
                                        >
                                    <Listeners>
                                        <IndicatorIconClick Handler="txtPaperName_IndicatorIconClick();"></IndicatorIconClick>
                                    </Listeners>
                                    </ext:TextField>

                        <ext:HtmlEditor
                            ID="htmlRemark"
                            runat="server"
                            FieldLabel="备注"
                            LabelAlign="Right"
                            LabelWidth="60"
                            Width="600"
                            Height="110"
                            EnableAlignments="false"
                            EnableFontSize="false"
                            CreateLinkText="My CreateLinkText"
                            StyleSpec="margin:10px 0px 0px 30px;">
                            <ButtonTips>
                                <BackColor Text="My BackColor Tip" />
                                <Bold Text="My Bold Tip" />
                            </ButtonTips>
                        </ext:HtmlEditor>

                        <ext:Container runat="server" Layout="ColumnLayout" StyleSpec="margin:5px 0px 0px 75px;">
                            <Items>
                                <ext:Button runat="server" 
                                                    ID="btnSave"
                                                    Width="120"
                                                    Height="30"
                                                    Text="保存数据"
                                                    StyleSpec="margin:5px 0px 0px 20px;"
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
                </ext:Panel>
            </Items>
        </ext:Viewport>
        <ext:Window runat="server" 
            ID="winSelectPerformanceRole"
            Title = "选择考试结果规则"
            Modal ="true"
            AutoRender="true"
            Maximizable="true"
            Hidden="true"
            MinWidth="600"
            MinHeight="300"
         Layout="FitLayout"
            >
            <Loader runat="server"
                ID="loader1"
                Mode="Frame"
                Url=""
                >
                <LoadMask ShowMask="true" Msg="正在加载，请稍等......">
                </LoadMask>
            </Loader>
            <Listeners>
                <Resize Handler="winSelectPerformanceRole_Resize( item, adjWidth, adjHeight);"></Resize>
            </Listeners>
        </ext:Window>
        <ext:Window runat="server" 
            ID="winSelectPaperName"
            Title = "选择考试试卷"
            Modal ="true"
            AutoRender="true"
            Maximizable="true"
            Hidden="true"
            MinWidth="890"
            MinHeight="300"
         Layout="FitLayout"
            >
            <Loader runat="server"
                ID="loader2"
                Mode="Frame"
                Url=""
                >
                <LoadMask ShowMask="true" Msg="正在加载，请稍等......">
                </LoadMask>
            </Loader>
            <Listeners>
                <Resize Handler="winSelectPaperName_Resize( item, adjWidth, adjHeight);"></Resize>
            </Listeners>
        </ext:Window>
    </form>
</body>
</html>
