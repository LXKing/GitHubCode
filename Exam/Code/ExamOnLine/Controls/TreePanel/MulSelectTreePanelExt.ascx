<%@ Control Language="C#" AutoEventWireup="true"  Inherits="Ext.Extension.TreePanelEx.TreePanelBaseExt" %>
<script type="text/javascript">
    function checkChange(item, checked)
    {
        var i = item;
        var ch = checked;
        //alert("");
    }

    function btnConfirmSelect_Before()
    {
        var selected = App.mulSelectTreePanel_Main.getCheckedNodes().length > 0;
        return selected;
    }
        
    //全选
    function btnSelectAll_Click()
    {
        App.mulSelectTreePanel_Main.setAllChecked();
    }

    //取消全选
    function btnClearSelect_Click()
    {
        App.mulSelectTreePanel_Main.clearChecked();
    }

    //取消
    function btnCancel_Click()
    {
        App.mulSelectWindow_Main.close();
    }
</script>
<asp:HiddenField runat="server" ClientIDMode="Static" ID="hidden_SelectedID" />
<asp:HiddenField runat="server" ClientIDMode="Static" ID="Hidden_SelectedText" />
<ext:Window
        runat="server"
        ID="mulSelectWindow_Main"
        IDMode="Static"
        MinWidth="350"
        MinHeight="400"
        Hidden="true"
        Layout="FitLayout"
        Modal="true"
    >
    <Items>
        <ext:TreePanel 
            runat="server"
            ID="mulSelectTreePanel_Main"
            IDMode="Static"
            AnchorHorizontal="100"
            AnchorVertical="100"
            ActiveIndex="0"
            StyleSpec=""
            RootVisible="false"
            SingleExpand="false"
            AutoScroll="true"
            RenderXType="true"
            >
            <Root>
                <ext:Node EmptyChildren="true" Icon="Folder"></ext:Node>
            </Root>
            <Store>
                <ext:TreeStore
                    runat="server"
                    ID="mulSelectTreeStore_Main"
                    IDMode="Static"
                    BatchUpdateMode="Operation"
                    >
                    <Listeners>
                    </Listeners>
                </ext:TreeStore>
            </Store>
            <DirectEvents>
                <SelectionChange OnEvent="SelectionChange">
                    <ExtraParams>
                        <ext:StoreParameter Name="text" Value="selected[0].data.text" Mode="Raw" Encode="false" />
                        <ext:StoreParameter Name="id" Value="selected[0].data.id" Mode="Raw" Encode="false" />
                    </ExtraParams>
                </SelectionChange>
            </DirectEvents>
            <Listeners>
                <CheckChange Handler="checkChange(item, checked);"></CheckChange>
            </Listeners>
            <BottomBar>
                <ext:Toolbar runat="server" Layout="FitLayout">
                    <Items>
                        <ext:ButtonGroup runat="server" ID="btnGroups" IDMode="Static">
                            <Buttons>
                                <ext:Button runat="server" ID="btnConfirmSelect" IDMode="Static" Height="25" Width="50" Text="确认选择">
                                    <DirectEvents>
                                        <Click Before="return btnConfirmSelect_Before();" After="" Success="btnCancel_Click();" Complete="" Failure="" OnEvent="btnConfirmSelect_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" ID="btnSelectAll" IDMode="Static" Height="25" Width="40" Text="全选">
                                    <Listeners>
                                        <Click Handler="btnSelectAll_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" ID="btnClearSelect" IDMode="Static" Height="25" Width="40" Text="清除选择">
                                    <Listeners>
                                        <Click Handler="btnClearSelect_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" ID="btnCancel" IDMode="Static" Height="25" Width="40" Text="取消">
                                    <Listeners>
                                        <Click Handler="btnCancel_Click();"></Click>
                                    </Listeners>
                                </ext:Button>
                            </Buttons>
                        </ext:ButtonGroup>
                    </Items>
                </ext:Toolbar>
            </BottomBar>
        </ext:TreePanel>
        
    </Items>
    <Listeners>
        <%--<Hide Handler="App.panel2_TreePanel_Main.selectionModelField.destroy();"></Hide>
        <Close Handler="App.panel2_TreePanel_Main.selectionModelField.destroy();"></Close>--%>
    </Listeners>
</ext:Window>