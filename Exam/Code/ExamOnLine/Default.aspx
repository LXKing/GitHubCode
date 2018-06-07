<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExamOnLine.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>在线考试系统</title>
    <link href="Css/Default.css" rel="stylesheet" />

    <script src="Scripts/jquery-1.8.2.min.js"></script>
    <script src="Scripts/jquery.messi/messi.min.js"></script>
    <%--<script src="Scripts/jsview/JsRender/jsrender.min.js"></script>--%>
    <script src="Scripts/jquery.cookie.js"></script>
    <script src="Scripts/common.js"></script>
    <script src="Scripts/extjsEx.js"></script>
    <script src="Js/Deafult.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout" StyleSpec="width:100%;" AutoScroll="true">
            <Items>
                <ext:Panel ID="pnlTop" runat="server" Header="false" Region="North" Border="false"
                    Height="90" Layout="FitLayout" Margins="0 0 0 0" Split="false" Collapsible="false">
                    <Items>
                        <ext:Container runat="server" StyleSpec="background-image: url(Images/Default/BkgImage.png);">
                            <Items>
                                <ext:Container runat="server" Layout="ColumnLayout">
                                    <Items>
                                        <ext:Container runat="server" ColumnWidth="0.3" Height="90"  Layout="AnchorLayout">
                                            <Items>
                                                <ext:Image runat="server" AnchorHorizontal="50%" Height="50" StyleSpec="margin:10px 0px 0px 10px;" ImageUrl="Images/Default/logo.png" />
                                                <ext:Container runat="server" AnchorHorizontal="70%" Height="30" Layout="ColumnLayout">
                                                    <Items>
                                                       <%-- <ext:TagLabel runat="server">
                                                            <Tags>
                                                                <ext:Tag Text="刷新人数" Icon="UserTick" Style="cursor:pointer;"></ext:Tag>
                                                            </Tags>
                                                            <DirectEvents>
                                                                <Click OnEvent="RefreshUserCount_Click"></Click>
                                                            </DirectEvents>
                                                        </ext:TagLabel>--%>
                                                        <%--<ext:Label runat="server" ColumnWidth="0.5" Text="刷新人数" StyleSpec="margin:5px 0px 0px 20px;color: white;cursor: pointer;">
                                                            <DirectEvents>
                                                                
                                                            </DirectEvents>
                                                        </ext:Label>--%>
                                                        <ext:Label ID="lblUserCount" runat="server" ColumnWidth="0.5" StyleSpec="margin:5px 0px 0px 10px;color: white;" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" ColumnWidth="0.7" Height="90" Layout="AnchorLayout">
                                            <Items>
                                                <ext:Container runat="server" AnchorVertical="80%" Layout="ColumnLayout">
                                                    <Items>
                                                        <ext:Component runat="server" ColumnWidth="0.8" Height="30" />
                                                        <ext:Image runat="server" ID="img_Bug" Width="30" Height="30" Cls="imgButton" ImageUrl="Images/Default/bug.png"  />
                                                        <ext:Image runat="server" ID="img_Skin" Width="30" Height="30" Cls="imgButton" ImageUrl="Images/Default/pifu.png" >
                                                            <DirectEvents>
                                                                <Click Before="" After="" Success="" Failure="" OnEvent="img_Skin_Click"></Click>
                                                            </DirectEvents>
                                                        </ext:Image>
                                                        <ext:Image runat="server" ID="img_HideMenu" Width="30" Height="30" Cls="imgButton" ImageUrl="Images/Default/left.png">
                                                            <Listeners>
                                                                <Click Handler="shrinkMenu();"></Click>
                                                            </Listeners>
                                                        </ext:Image>
                                                        <ext:Image runat="server" ID="img_Refresh" Width="30" Height="30" Cls="imgButton" ImageUrl="Images/Default/refresh.png">
                                                            <Listeners>
                                                                <Click Handler="refreshPage();"></Click>
                                                            </Listeners>
                                                        </ext:Image>
                                                        <ext:Image runat="server" ID="img_Logout" Width="30" Height="30" Cls="imgButton"  ImageUrl="Images/Default/loginout.png" >
                                                            <DirectEvents>
                                                                <Click Before="img_Logout_BeforeClick();" After="" Success="" Complete="" OnEvent="img_Logout_Click"></Click>
                                                            </DirectEvents>
                                                            <Listeners>
                                                                <Click Handler="return img_Logout_Click()"></Click>
                                                            </Listeners>
                                                        </ext:Image>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container runat="server" AnchorVertical="20%" Layout="ColumnLayout" StyleSpec="margin:20px 0px 0px 0px;">
                                                    <Items>
                                                        <ext:Component runat="server" ColumnWidth="0.8" Height="2" />
                                                        <ext:Label ID="lblWelcome" runat="server" StyleSpec="color: white;" />
                                                    </Items>
                                                </ext:Container>                                                
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="pnlLeft" runat="server" 
                    Region="West" Width="220" 
                    MinWidth="140" MaxWidth="340" 
                    Margins="0 0 4 4" Split="true"
                    Header="true" Title="功能菜单" TitleAlign="Left" Icon="ApplicationStart"
                    Collapsible="true" Layout="AnchorLayout">
                    <Items>
                        <ext:Panel runat="server" Layout="ColumnLayout" AnchorVertical="70" StyleSpec="align:center;">
                            <Items>
                                <ext:Image ID="imgAvatar" runat="server" Width="60" Height="60" ImageUrl="Images/NoImage.png" StyleSpec="margin:5px 0 5px 5px;"></ext:Image>
                                <ext:Panel runat="server" Layout="BorderLayout" Height="70" ColumnWidth="1" Border="false" BodyStyle="background-color: white;">
                                    <Items>
                                        <ext:Label ID="lblUserName" runat="server" Margins="24px 0px 0px 10px" Region="Center"/>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="pnlMenu" runat="server" AnchorHorizontal="100%" AnchorVertical="88%" Layout="AccordionLayout" AutoScroll="true">
                            <Items>

                            </Items>
                            <Listeners>
                                <BeforeExpand Handler="this.setTitle(this.initialConfig.title);" />
                            </Listeners>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:TabPanel ID="ExampleTabs" runat="server" Region="Center" Margins="0 4 4 0" EnableTabScroll="true"
                    Cls="tabs" MinTabWidth="115">
                    <Items>
                        <ext:Panel ID="tabHome" runat="server" HideMode="Offsets" Icon="Application" Closable="false" Title="欢迎进入">
                            <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="true" Url="http://pic.4j4j.cn/upload/pic/20130712/8eeac9a188.jpg">
                                <LoadMask ShowMask="true" Msg="读取中" />
                            </Loader>
                        </ext:Panel>
                    </Items>
                    <Plugins>
                        <ext:TabCloseMenu ID="TabCloseMenu1" runat="server"
                        CloseTabText="关闭当前窗口"
                        CloseOthersTabsText="关闭其他窗口"
                        CloseAllTabsText="关闭所有窗口">
                    </ext:TabCloseMenu>
                    </Plugins>

                    <BottomBar>
                        <ext:StatusBar ID="StatusBar1" runat="server" StyleSpec="">
                            <Items>
                                <ext:Label ID="lblStatusTitle" runat="server" BaseCls="top-Label" StyleSpec="margin-left:280px;" />
                                <ext:Label ID="lblLoginMessage" runat="server" BaseCls="top-Label" />
                                <ext:Label ID="lblCurrentDate" runat="server" Text="" BaseCls="top-Label" StyleSpec="padding-left:50px;" />
                                <ext:ToolbarFill />
                            </Items>
                        </ext:StatusBar>
                    </BottomBar>
                </ext:TabPanel>
                <ext:Window runat="server" ID="themeWindow" Width="180" Height="110" Layout="FormLayout" Hidden="true" Modal="true">
                            <Items>
                                <ext:ComboBox runat="server" ID="cmbTheme" Editable="false"  Width="100" StyleSpec="margin-left:0px;margin-top:5px;" FieldLabel="皮肤" LabelAlign="Right" LabelWidth="50" InputWidth="80">
                                    <Items>
                                        <ext:ListItem Index="0" Text="Default" Value="0"></ext:ListItem>
                                        <ext:ListItem Index="1" Text="Gray" Value="1"></ext:ListItem>
                                        <ext:ListItem Index="2" Text="Access" Value="2"></ext:ListItem>
                                        <ext:ListItem Index="3" Text="Neptune" Value="4"></ext:ListItem>
                                    </Items>
                                </ext:ComboBox>

                                <ext:Button runat="server" ID="btnSaveTheme" Text="保存" Width="80" StyleSpec="margin-top:5px;margin-left:55px;">
                                    <DirectEvents>
                                        <Click OnEvent="btnSaveTheme_Click" Success ="btnSaveTheme_Click();"></Click>
                                    </DirectEvents>
                                    <Listeners>
                                        <Click Handler=""></Click>
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
