<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuestions.aspx.cs" Inherits="ExamOnLine.Pages.ExamDesign.AddQuestions" %>
<%@ Register Src="~/Controls/TreePanel/TreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加试题</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="../../Scripts/jquery.messi/messi.min.js"></script>
    <script src="../../Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="../../Scripts/jquery.cookie.js"></script>
    <script src="../../Scripts/common.js"></script>
    <script src="../../Scripts/extjsEx.js"></script>
    <link href="../../Css/Button.css" rel="stylesheet" />
    <script type="text/javascript" charset="utf-8" src="../../ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../ueditor/ueditor.all.min.js"> </script>
    <!--建议手动加在语言，避免在ie下有时因为加载语言失败导致编辑器加载失败-->
    <!--这里加载的语言文件会覆盖你在配置项目里添加的语言类型，比如你在配置项目里配置的是英文，这里加载的中文，那最后就是中文-->
    <script type="text/javascript" charset="utf-8" src="../../ueditor/lang/zh-cn/zh-cn.js"></script>
    <script src="../../Js/3_ExamDesign/AddQuestions.js"></script>
</head>
<body>
    <form id="form1" method="post" enctype="multipart/form-data" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Hidden ID="hidIsAdd" runat="server" />
        <ext:Hidden ID="hidQuestionsID" runat="server" />
        <ext:Hidden ID="hidQuestionTypeID" runat="server" />
        <ext:Hidden ID="hidTemplateID" runat="server" />
        <ext:Hidden ID="hidKnowID" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout" AutoScroll="true">
            <Items>
                <ext:FormPanel ID="formPanel" runat="server" AutoScroll="true">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ToolbarFill runat="server" />
                                <ext:Button runat="server" Text="返回" BaseCls="false" Cls="button_goback">
                                    <Listeners>
                                        <Click Handler="goBack();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Defaults>
                        <ext:Parameter Name="style" Value="margin: 10px 0px 0px 0px;" Mode="Value" />
                        <ext:Parameter Name="LabelStyle" Value="color:#417ac1;" Mode="Value" />
                        <ext:Parameter Name="LabelAlign" Value="right" Mode="Value" />
                    </Defaults>
                    <Items>
                        <ext:TextField ID="txtKnow" runat="server" FieldLabel="知识体系" AllowBlank="false" IndicatorIcon="Zoom" ReadOnly="true" Width="368" >
                            <DirectEvents>
                                <IndicatorIconClick OnEvent="GetKnow_Click"></IndicatorIconClick>
                            </DirectEvents>
                        </ext:TextField>
                        <ext:Container runat="server" Layout="ColumnLayout">
                            <Defaults>
                                <ext:Parameter Name="LabelStyle" Value="color:#417ac1;" Mode="Value" />
                                <ext:Parameter Name="LabelAlign" Value="right" Mode="Value" />
                            </Defaults>
                            <Items>
                                <ext:SelectBox ID="sltType" runat="server" FieldLabel="试题类型" DisplayField="QUESTION_TYPE_NAME" ValueField="ID">
                                    <Listeners>
                                        <Change Handler="changeType();" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Change OnEvent="sltType_Change" />
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
                                </ext:SelectBox>
                                <ext:SelectBox ID="sltDifficulty" runat="server" FieldLabel="试题难度" >
                                    <Items>
                                        <ext:ListItem Text="低难度" Value="0" />
                                        <ext:ListItem Text="中难度" Value="1" />
                                        <ext:ListItem Text="高难度" Value="2" />
                                    </Items>
                                </ext:SelectBox>
                                <ext:SelectBox ID="sltDisplay" runat="server" FieldLabel="是否在练习中显示" LabelWidth="140">
                                    <Items>
                                        <ext:ListItem Text="显示" Value="1" />
                                        <ext:ListItem Text="不显示" Value="0" />
                                    </Items>
                                </ext:SelectBox>
                            </Items>
                        </ext:Container>
                        <ext:Container runat="server" StyleSpec="margin:10px 0px 0px 0px;" Width="815" Height="330" Layout="ColumnLayout">
                            <Items>
                                <ext:Label runat="server" Text="试题题目:" Width="100" StyleSpec="text-align:right;color:#417ac1;"  />
                                <ext:Container runat="server" Width="710" Height="330" StyleSpec="margin-left:5px;" >
                                    <Content>
                                        <script id="editor" name="content" type="text/plain" style="width: 700px; height: 330px;"><%=ques.QUESTION_CONTENT%></script>
                                    </Content>
                                </ext:Container>
                            </Items>                            
                        </ext:Container>
                        <ext:Container ID="content" runat="server" StyleSpec="margin:10px 0px 0px 0px;" Width="815" Height="330" Layout="ColumnLayout">
                            <Items>
                                <ext:Label runat="server" Text="选项内容:" Width="100" StyleSpec="text-align:right;color:#417ac1;"  />
                                <ext:Container runat="server" Width="710" Height="330" StyleSpec="margin-left:5px;" >
                                    <Content>
                                        <script id="editor2" name="option" type="text/plain" style="width: 700px; height: 330px;"><%=ques.QUESTION_OPTIONS%></script>
                                    </Content>
                                </ext:Container>
                            </Items>                            
                        </ext:Container>
                        <ext:SelectBox ID="sltNum" runat="server" FieldLabel="选项个数">
                            <Items>
                                <ext:ListItem Text="2" Value="2" />
                                <ext:ListItem Text="3" Value="3" />
                                <ext:ListItem Text="4" Value="4" />
                                <ext:ListItem Text="5" Value="5" />
                                <ext:ListItem Text="6" Value="6" />
                                <ext:ListItem Text="7" Value="7" />
                                <ext:ListItem Text="8" Value="8" />
                                <ext:ListItem Text="9" Value="9" />
                            </Items>
                        </ext:SelectBox>
                        <ext:RadioGroup ID="rdoGpSingle" runat="server" FieldLabel="试题答案" Layout="ColumnLayout">
                            <Items>
                                <ext:Radio ID="Radio1" runat="server" BoxLabel="(A)" Checked="true" />
                                <ext:Radio ID="Radio2" runat="server" BoxLabel="(B)" />
                                <ext:Radio ID="Radio3" runat="server" BoxLabel="(C)" />
                                <ext:Radio ID="Radio4" runat="server" BoxLabel="(D)" />
                                <ext:Radio ID="Radio7" runat="server" BoxLabel="(E)" />
                                <ext:Radio ID="Radio8" runat="server" BoxLabel="(F)" />
                                <ext:Radio ID="Radio9" runat="server" BoxLabel="(G)" />
                                <ext:Radio ID="Radio10" runat="server" BoxLabel="(H)" />
                                <ext:Radio ID="Radio11" runat="server" BoxLabel="(I)" />
                            </Items>
                        </ext:RadioGroup>
                        <ext:CheckboxGroup 
                            ID="chbGpMultiple" 
                            runat="server" 
                            FieldLabel="试题答案" 
                            Layout="ColumnLayout">
                            <Items>
                                <ext:Checkbox ID="Checkbox4" runat="server" BoxLabel="(A)" />
                                <ext:Checkbox ID="Checkbox5" runat="server" BoxLabel="(B)" />
                                <ext:Checkbox ID="Checkbox6" runat="server" BoxLabel="(C)" />
                                <ext:Checkbox ID="Checkbox7" runat="server" BoxLabel="(D)" />
                                <ext:Checkbox ID="Checkbox8" runat="server" BoxLabel="(E)" />
                                <ext:Checkbox ID="Checkbox1" runat="server" BoxLabel="(F)" />
                                <ext:Checkbox ID="Checkbox2" runat="server" BoxLabel="(G)" />
                                <ext:Checkbox ID="Checkbox3" runat="server" BoxLabel="(H)" />
                                <ext:Checkbox ID="Checkbox9" runat="server" BoxLabel="(I)" />
                            </Items>
                        </ext:CheckboxGroup> 
                        <ext:RadioGroup ID="rdoGpJudge" runat="server" FieldLabel="试题答案" Layout="ColumnLayout">
                            <Items>
                                <ext:Radio ID="Radio5" runat="server" BoxLabel="正确" Checked="true" />
                                <ext:Radio ID="Radio6" runat="server" BoxLabel="错误" />
                            </Items>
                        </ext:RadioGroup>
                        <ext:Container runat="server" StyleSpec="margin:10px 0px 0px 0px;" Width="815" Height="330" Layout="ColumnLayout">
                            <Items>
                                <ext:Label runat="server" Text="试题解析:" Width="100" StyleSpec="text-align:right;color:#417ac1;" />
                                <ext:Container runat="server" Width="710" Height="330" StyleSpec="margin-left:5px;">
                                    <Content>
                                        <script id="editor3" name="analyze" type="text/plain" style="width: 700px; height: 330px;"><%=ques.ANSWER_ANALYSIS%></script>
                                    </Content>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                        <ext:Panel runat="server" Border="false" StyleSpec="margin-left:100px;" ButtonAlign="Left">
                            <Buttons>
                                <ext:Button ID="btnSave" runat="server" Text="保存" BaseCls="false" Cls="button_infosave">
                                    <DirectEvents>
                                        <Click Before="#{formPanel}.getForm().isValid();" OnEvent="btnSave_click"/>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" Text="返回" BaseCls="false" Cls="button_goback">
                                    <Listeners>
                                        <Click Handler="goBack();" />
                                    </Listeners>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </Items>
                    <Content>
                        <ucExt:TreePanelExt 
                            runat="server" 
                            ID="trpnlExt"
                            Hidden="true"
                            Modal="true"
                            RootNodeID=""
                            RootNodeVisible="false" 
                            OnNodeClick="trpnlExt_NodeClick"
                            Title="" />
                    </Content>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
