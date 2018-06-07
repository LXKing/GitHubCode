<%@ Control  Language="C#" AutoEventWireup="true"  Inherits="Ext.Extension.TreePanelEx.TreePanelBaseExt" %>
<script type="text/javascript">
    function BeforeClose()
    {
        App.treePanel_Main.store.reload();
    }
</script>
<ext:Window
        runat="server"
        ID="windowMain"
        IDMode="Static"
        MinWidth="200"
        MinHeight="300"
        Hidden="true"
        Layout="FitLayout"
        Modal="true"
    >
    <Items>
        <ext:TreePanel 
            runat="server"
            ID="treePanel_Main"
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
                    ID="treeStoreMain"
                    IDMode="Static"
                    BatchUpdateMode="Operation"
                    >
                </ext:TreeStore>
            </Store>
            <DirectEvents>
                <SelectionChange OnEvent="SelectionChange">
                    <ExtraParams>
                        <ext:StoreParameter Name="text" Value="selected[0].data.text" Mode="Raw" Encode="false" />
                        <ext:StoreParameter Name="id" Value="selected[0].data.id" Mode="Raw" Encode="false" />
                    </ExtraParams>
                </SelectionChange>
                <ItemClick OnEvent="ItemClick">
                     <ExtraParams>
                        <ext:StoreParameter Name="text" Value="record .data.text" Mode="Raw" Encode="false" />
                        <ext:StoreParameter Name="id" Value="record .data.id" Mode="Raw" Encode="false" />
                    </ExtraParams>
                </ItemClick>
            </DirectEvents>
        </ext:TreePanel>
    </Items>
    <Listeners>
        <BeforeClose Handler="BeforeClose();"></BeforeClose>
    </Listeners>
</ext:Window>

