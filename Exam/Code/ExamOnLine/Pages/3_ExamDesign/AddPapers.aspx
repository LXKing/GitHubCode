<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPapers.aspx.cs" Inherits="ExamOnLine.Pages.ExamDesign.AddPapers" %>

<%@ Register Src="~/Controls/TreePanel/TreePanelExt.ascx" TagName="TreePanelExt" TagPrefix="ucExt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加试卷</title>
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
    <script src="../../Js/3_ExamDesign/AddPapers.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" ID="ResourceManager1" />
        <ext:Hidden runat="server" ID="hidIsAdd" />
        <ext:Hidden runat="server" ID="hidPapersID" />
        <ext:Hidden runat="server" ID="hidPaperFieldID" />
        <ext:Viewport runat="server" ID="Viewport1" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="formPanel" runat="server">
                    <Defaults>
                        <ext:Parameter Name="style" Value="margin: 10px 0px 0px 0px;" Mode="Value" />
                        <ext:Parameter Name="LabelStyle" Value="color:#417ac1;" Mode="Value" />
                        <ext:Parameter Name="LabelAlign" Value="right" Mode="Value" />
                    </Defaults>
                    <Items>                        
                        <ext:TextField ID="txtPaperName" runat="server" FieldLabel="试卷名称" AllowBlank="false" MaxLength="30" Width="350" />
                        <ext:TextField ID="txtPaperField" runat="server" FieldLabel="试卷领域" ReadOnly="true" Width="368" IndicatorIcon="Zoom">
                            <DirectEvents>
                                <IndicatorIconClick Before="" OnEvent="GetPaperField_Click"></IndicatorIconClick>
                            </DirectEvents>
                        </ext:TextField>
                        <ext:ComboBox ID="cmbPaperType" runat="server" FieldLabel="试卷类型" Editable="false" Width="350">
                            <Items>
                                <ext:ListItem Index="0" Text="在线考试" Value="39169d1e-0d06-67cd-f218-f94b29420af2"></ext:ListItem>
                                <ext:ListItem Index="1" Text="网上作业" Value="eb55a5f5-d471-a2a4-f4e9-6549052da334"></ext:ListItem>
                                <ext:ListItem Index="2" Text="在线练习" Value="59c4571d-bb96-b66a-d794-92358c14173f"></ext:ListItem>
                            </Items>
                        </ext:ComboBox>
                        <ext:ComboBox ID="cmbMakeWay" runat="server" FieldLabel="出题方式" Editable="false" Width="350">
                            <Items>
                                <ext:ListItem Index="0" Text="固定试卷(手工)" Value="5d9ce76c-5896-541f-b7b1-6b31ee04c1a2"></ext:ListItem>
                                <ext:ListItem Index="1" Text="固定试卷(随机)" Value="0785ce6b-3c78-4ac8-e292-a1bb251c0cf5"></ext:ListItem>
                                <ext:ListItem Index="2" Text="随机试卷" Value="997bded7-fb08-912b-adf7-b87f76db6d4a"></ext:ListItem>
                            </Items>
                        </ext:ComboBox>
                        <ext:Container runat="server" Width="815" Height="330" Layout="ColumnLayout">
                            <Items>
                                <ext:Label runat="server" Text="试卷说明:" Width="100" StyleSpec="text-align:right;color:#417ac1;"  />
                                <ext:Container runat="server" Width="710" Height="330" StyleSpec="margin-left:5px;" >
                                    <Content>
                                        <script id="editor" name="content" type="text/plain" style="width: 700px; height: 330px;"><%=pap.PAPER_DESC%></script>
                                    </Content>
                                </ext:Container>
                            </Items>                            
                        </ext:Container>
                        <ext:Panel runat="server" Border="false" ButtonAlign="Left" StyleSpec="margin-left: 100px;" >
                            <Buttons>
                                <ext:Button ID="btnSave" runat="server" Text="保存" BaseCls="false" Cls="button_infosave">
                                    <DirectEvents>
                                        <Click Before="return #{formPanel}.getForm().isValid();" OnEvent="btnSave_click" />
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
                        <ucExt:TreePanelExt runat="server" ID="trpnlPaperField"
                            Hidden="true"
                            Modal="true"
                            RootNodeID=""
                            RootNodeVisible="false" 
                            OnNodeClick="trpnlPaperField_NodeClick"
                            Title="选择试卷分类" />
                    </Content>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
