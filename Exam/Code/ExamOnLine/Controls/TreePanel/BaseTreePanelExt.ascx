<%@ Control Language="C#" AutoEventWireup="true"  Inherits="Ext.Extension.UserControl.TreePanel.BaseTreePanelExt" %>
<ext:TreePanel 
            runat="server"
            ID="baseTreePanel_Main"
            IDMode="Static"
            AnchorHorizontal="100"
            AnchorVertical="100"
            MinHeight="300"
            MinWidth="150" 
            StyleSpec=""
            RootVisible="false"
            SingleExpand="false"
            AutoScroll="true"
            RenderXType="true" Border="false"
            >
            <Root>
                <ext:Node EmptyChildren="true" Icon="Folder"></ext:Node>
            </Root>
            <Store>
                <ext:TreeStore
                    runat="server"
                    ID="baseTreePanelStore_Main"
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