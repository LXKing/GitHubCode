<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ExamOnLine.Report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>报表设计</title>
    <script type="text/javascript">
        function checkBoxAfterRender(obj) {
            
            //obj.bodyEl.setStyle('border-width', 0);
            //obj.bodyEl.setStyle('width', 32);
            
        }

        function boxReady(obj, width, height) {
            obj.labelCell.setStyle('border-width', 0);
            obj.bodyEl.dom.setAttribute('style', 'border-width:0px;height:13px;width:13px;');
            obj.indicatorEl.dom.parentNode.parentNode.setAttribute('style', 'border-width:0px;width:0px;');
            //obj.indicatorEl.dom.parentNode.parentNode.remove();
            //obj.setWidth(obj.getWidth() - 37); 
            //obj.container.setWidth(obj.container.getWidth() - 37);
            //obj.ownerCt.setWidth(obj.ownerCt.getWidth() - 37);
            //obj.container.dom.parentElement.parentElement.parentNode.setAttribute('style', 'width:' + (obj.container.dom.parentElement.parentElement.parentNode.clientWidth - 50) + 'px');
            }
        function GridPanel(grid) {
            ;
        }
        //数字控件样式
        function afterrender(obj) {
            obj.inputCell.setStyle("border-width", 0);
            obj.triggerCell.setStyle("border-top-width", 0);
            obj.triggerCell.setStyle("border-bottom-width", 0);
            obj.setHideTrigger(true);
            obj.setValue(0.00);
        }
        function ShowMenuAdd(item,e)
        {
            App.GridMenuAdd.showAt(e.getPoint());
            e.preventDefault();
        }

        function ShowMenuRemove(item, record, node, index, e) {
            App.GridMenuRemove.showAt(e.getPoint());
            e.preventDefault();
            rowIndex = index;
        }
        function RemoveRow()
        {
            var grid = App.GridPanel1,
                sm = grid.getSelectionModel(),
                store = grid.getStore();

            grid.editingPlugin.cancelEdit();
            store.remove(sm.getSelection());

            if (store.getCount() > 0) {
                sm.select(0);
            }
        }

        function AddNewRow()
        {
            var grid = App.GridPanel1;
            
            store = grid.getStore();
            grid.editingPlugin.cancelEdit();
            //store.getSorters().removeAll(); // We have to remove sorting to avoid auto-sorting on insert
            //grid.getView().headerCt.setSortState(); // To update columns sort UI
            
            store.insert(0, {
                ID: 1,
                HWXingHao:'',
                HWName: '',
                HWCount: 0,
                HWBaoZhuang: '',
                HWWeight:0.00,
            });
            grid.editingPlugin.startEdit(0, 0);
        }
        var change = function (value, meta) {
            //meta.style = Ext.String.format(template, (value > 0) ? "green" : "red");
            return value;
        };

        var pctChange = function (value, meta) {
            //meta.style = Ext.String.format(template, (value > 0) ? "green" : "red");
            return value + "%";
        };

        var edit = function (editor, e) {
            /*
                "e" is an edit event with the following properties:

                    grid - The grid
                    record - The record that was edited
                    field - The field name that was edited
                    value - The value being set
                    originalValue - The original value for the field, before the edit.
                    row - The grid table row
                    column - The grid Column defining the column that was edited.
                    rowIdx - The row index that was edited
                    colIdx - The column index that was edited
            */

            // Call DirectMethod
            //if (!(e.value === e.originalValue || (Ext.isDate(e.value) && Ext.Date.isEqual(e.value, e.originalValue)))) {
            //    CompanyX.Edit(e.record.data.ID, e.field, e.originalValue, e.value, e.record.data);
            //}
        };
    </script>
    <style>
        .incicator
        {
            border-top-width: 0px;
border-bottom-width: 0;
border-left-width: 0;
border-right-width: 0;
        }
        .extcolor{
            color:#417ac1
        }

        html, body {
            height: 100%;
            padding: 0;
            margin: 0;
        }

        body {
            font-size: 9pt;
            color: #000;
            font-family: 微软雅黑,宋体,Arial,Helvetica,Verdana,sans-serif;
        }

        .wrapper {
            padding: 15px 45px 15px 23px;
            width: 1048px;
            height: 485px;
            border: 1px solid #cfcfcf;
            background: #fff;
            box-shadow: 0 1px 3px rgba(0,0,0,0.2);
            margin-left: -559px;
            margin-top: -259px;
            position: absolute;
            top: 50%;
            left: 50%;
        }

        .wrapper table {
                width: 100%;
            }

        .voucher {
            border: solid 2px #666;
            border-width: 2px;
            border-collapse: collapse;
            line-height: 35px;
        }

            .voucher th, .voucher td {
                border: 1px solid #666;
            }

            .voucher th {
                /*height: 48px;*/
                color: #555;
                font-size: 14px;
                text-align: center;
                font-weight: bold;
                overflow: hidden;
            }

        .voucher-tit {
            display: inline;
            font: 28px/1.8 "Microsoft Yahei";
            text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.2);
        }

        .ui-input {
            border: 1px solid #ddd;
            border-left: none;
            border-right: none;
            border-top: none;
            background-color: white;
            text-align: center;
            vertical-align: middle;
            /*color: #555;*/
            color: blue;
            height: 12px;
            line-height: 12px;
            outline: 0 none;
            padding: 6px 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <HtmlBin>
                <div>
                    <div class="wrapper">
                        <%-- 头部 --%>
                        <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;" Height="50">
                                    <Items>
                                        <ext:Container runat="server" Layout="HBoxLayout" BorderSpec="width:0px;" Width="392" StyleSpec="margin-top:20px;">
                                            <Items>
                                                <ext:TextField ID="txtStatrStation" runat="server" AllowBlank="false" Width="80" LabelAlign="Right" LabelWidth="0" EmptyText="起运站" LabelStyle="color:#417ac1;" />
                                                <ext:TextField ID="txtEndStation" runat="server" AllowBlank="false" Width="130" FieldLabel="至&nbsp;&nbsp;&nbsp;&nbsp;" LabelSeparator="" LabelAlign="Right" LabelWidth="50" EmptyText="目的站" LabelStyle="color:#417ac1;"/>
                                                <ext:TextField ID="txtStationName" runat="server" AllowBlank="false" Width="120" FieldLabel="(" LabelSeparator="" LabelAlign="Right" LabelWidth="30" EmptyText="当前网点" IndicatorText="&nbsp;)" IndicatorCls="extcolor" LabelStyle="color:#417ac1;"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                            <Items>
                                                <ext:Label runat="server"
                                                    ID="labDanJuTitile"
                                                    Text="托运凭证"
                                                    StyleSpec="font-family: 'Microsoft Yahei';font-size: 32px;Yaheitext-shadow: 1px 1px 1px rgba(0, 0, 0, 0.2);"
                                                    >
                                                </ext:Label>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container runat="server" Layout="HBoxLayout" BorderSpec="width:0px;" Width="450" StyleSpec="margin-top:20px;">
                                            <Items>
                                                <ext:Label runat="server"
                                                    ID="Label1"
                                                    Text="NO:&nbsp;"
                                                    StyleSpec="margin-left:20px;margin-top:3px;font-weight: 700;"
                                                    >
                                                </ext:Label>
                                                <ext:Label runat="server"
                                                    ID="Label2"
                                                    Width="240"
                                                    Text="G798989898HJKUY"
                                                    StyleSpec="margin-left:0px;margin-top:3px;color: rgb(24, 33, 139);">
                                                </ext:Label>
                                                <ext:DateField runat="server" ID="dtYWDate" EmptyText="请选择日期" FieldLabel="业务日期" LabelAlign="Right" LabelWidth="60" InputWidth="100"
                                                    StyleSpec="margin-right:0px;">
                                                    <Listeners>
                                                        <AfterRender Handler="App.dtYWDate.setValue(new Date());"></AfterRender>
                                                    </Listeners>
                                                </ext:DateField>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                        <%-- 单据 --%>
                    <table class="voucher">
                        <tr>
                            <th style="width:6.66%" class="k-header">发货方</th>
                            <td colspan="3" style="width:19.98%">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                    <Items>
                                        <ext:TextField ID="txtFHF" 
                                            runat="server" 
                                            AllowBlank="false"   
                                            InputWidth="185"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="发货方" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                             />
                                    </Items>
                                </ext:Container>
                            </td>
                            <th style="width:6.66%" class="k-header">手机</th>
                            <td colspan="2" style="width:13.32%;border-right:none;">
                                 <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                    <Items>
                                        <ext:TextField ID="txtPhone" 
                                            runat="server" 
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="手机号码" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                             />
                                    </Items>
                                </ext:Container>
                            </td>
                            <th style="width:6.66%" class="k-header">电话</th>
                            <td colspan="2" style="width:13.32%;border-right:none;">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                    <Items>
                                        <ext:TextField ID="txtTel" 
                                            runat="server" 
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="电话号码" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                             />
                                    </Items>
                                </ext:Container>
                            </td>
                            <th style="width:6.66%" class="k-header">地址</th>
                            <td colspan="4" style="width:26.65%">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                    <Items>
                                        <ext:TextArea ID="txtAddress" 
                                            runat="server"
                                            Rows="1"
                                            AllowBlank="false"   
                                            InputWidth="250"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="地址" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                             />
                                    </Items>
                                </ext:Container>
                            </td>
                        </tr>
                        <tr>
                            <th style="width:6.66%" class="k-header">收货方</th>
                                <td colspan="3" style="width:19.98%">
                                    <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                        <Items>
                                            <ext:TextField ID="txtSHF" 
                                            runat="server" 
                                            AllowBlank="false"   
                                            InputWidth="185"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="收货方" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                             />
                                        </Items>
                                    </ext:Container>
                            </td>
                            <th style="width:6.66%" class="k-header">手机</th>
                            <td colspan="2" style="width:13.32%;border-right:none;">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                    <Items>
                                        <ext:TextField ID="txtPhoneSHF" 
                                            runat="server" 
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="手机号码" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                             />
                                    </Items>
                                </ext:Container>
                            </td>
                            <th style="width:6.66%" class="k-header">电话</th>
                            <td colspan="2" style="width:13.32%;border-right:none;">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                    <Items>
                                        <ext:TextField ID="txtTelSHF" 
                                            runat="server" 
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="手机号码" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                             />
                                    </Items>
                                </ext:Container>
                            </td>
                            <th style="width:6.66%" class="k-header">地址</th>
                            <td colspan="4" style="width:26.65%">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                    <Items>
                                        <ext:TextArea ID="txtAddressSHF" 
                                            runat="server"
                                            Rows="1"
                                            AllowBlank="false"   
                                            InputWidth="250"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="地址" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                             />
                                    </Items>
                                </ext:Container>
                            </td>
                        </tr>
                        <tr style="height:200px;">
                            <td colspan="15">
                                <%--<div id="grid"></div>--%>
                                <ext:Container runat="server" Layout="FitLayout" BorderSpec="width:0px;" Width="972">
                                    <Items>
                                        <ext:GridPanel
                                            ID="GridPanel1"
                                            runat="server"
                                            Title="Editable GridPanel"
                                            Header="false"
                                            MaxWidth="972"
                                            Height="200">
                                            <Store>
                                                <ext:Store runat="server">
                                                    <Model>
                                                        <ext:Model runat="server" IDProperty="HWName">
                                                            <Fields>
                                                                <ext:ModelField Name="ID" Type="Int" />
                                                                <ext:ModelField Name="HWName" />
                                                                <ext:ModelField Name="HWXingHao" />
                                                                <ext:ModelField Name="HWCount" Type="Int" />
                                                                <ext:ModelField Name="HWBaoZhuang" />
                                                                <ext:ModelField Name="HWWeight" Type="Float" />
                                                                <ext:ModelField Name="HWVolume" Type="Float" />
                                                                <ext:ModelField Name="HWPrice" Type="Float" />
                                                                <ext:ModelField Name="HWYF" Type="Float" />


                                                                <ext:ModelField Name="HWOtherFY" Type="Float" />
                                                                <ext:ModelField Name="HWValue" Type="Float" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel runat="server" >
                                                <Columns>
                                                    <ext:Column runat="server" Text="ID" DataIndex="ID" Width="35" Visible="false"/>
                                                    <ext:Column runat="server" Text="货&nbsp;名" DataIndex="HWName" Flex="1" Align="Center">
                                                        <Editor>
                                                            <ext:TextField runat="server" />
                                                        </Editor>
                                                    </ext:Column>
                                                    <ext:Column runat="server" Text="型&nbsp;号" DataIndex="HWXingHao" Flex="1" Align="Center">
                                                        <Editor>
                                                            <ext:TextField runat="server" />
                                                        </Editor>
                                                    </ext:Column>
                                                    <ext:Column runat="server" Text="件&nbsp;数" DataIndex="HWCount" Flex="1" Align="Center">
                                                        <Renderer Format="UsMoney" />
                                                        <Editor>
                                                            <ext:NumberField runat="server"  DecimalPrecision="0"/>
                                                        </Editor>
                                                    </ext:Column>
                                                    <ext:Column runat="server" Text="包&nbsp;装" DataIndex="HWBaoZhuang" Flex="1" Align="Center">
                                                        <Renderer Fn="change" />
                                                        <Editor>
                                                            <ext:TextField runat="server" />
                                                        </Editor>
                                                    </ext:Column>
                                                     <ext:Column runat="server" Text="重&nbsp;量(吨)" DataIndex="HWWeight" Flex="1" Align="Center">
                                                        <Renderer Fn="change" />
                                                        <Editor>
                                                            <ext:NumberField runat="server" DecimalPrecision="2"/>
                                                        </Editor>
                                                    </ext:Column>
                                                    <ext:Column runat="server" Text="体&nbsp;积(立方)" DataIndex="HWVolume" Flex="1" Align="Center">
                                                        <Renderer Fn="change" />
                                                        <Editor>
                                                            <ext:NumberField runat="server" DecimalPrecision="2"/>
                                                        </Editor>
                                                    </ext:Column>
                                                    <ext:Column runat="server" Text="单&nbsp;价" DataIndex="HWPrice" Flex="1" Align="Center">
                                                        <Renderer Fn="change" />
                                                        <Editor>
                                                            <ext:NumberField runat="server" DecimalPrecision="2"/>
                                                        </Editor>
                                                    </ext:Column>
                                                    <ext:Column runat="server" Text="运&nbsp;费" DataIndex="HWYF" Flex="1" Align="Center">
                                                        <Renderer Fn="change" />
                                                        <Editor>
                                                            <ext:NumberField runat="server" DecimalPrecision="2"/>
                                                        </Editor>
                                                    </ext:Column>
                                                    <ext:Column runat="server" Text="其它费用" DataIndex="HWOtherFY" Flex="1" Align="Center">
                                                        <Renderer Fn="change" />
                                                        <Editor>
                                                            <ext:NumberField runat="server" DecimalPrecision="2"/>
                                                        </Editor>
                                                    </ext:Column>
                                                    <ext:Column runat="server" Text="声明价值" DataIndex="HWValue" Flex="1" Align="Center">
                                                        <Renderer Fn="change" />
                                                        <Editor>
                                                            <ext:NumberField runat="server" DecimalPrecision="2"/>
                                                        </Editor>
                                                    </ext:Column>
                                                </Columns>
                                            </ColumnModel>
                                            <SelectionModel>
                                                <%--<ext:RowSelectionModel runat="server" Mode="Single"></ext:RowSelectionModel>--%>
                                                <ext:CellSelectionModel runat="server"></ext:CellSelectionModel>
                                            </SelectionModel>
                                            <Plugins>
                                                <ext:RowEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" />
                                            </Plugins>
                                            <Listeners>
                                                <ContainerContextMenu Handler="ShowMenuAdd(this, e);">
                                                </ContainerContextMenu>
                                                <ItemContextMenu Handler="ShowMenuRemove(this, record, node, index, e);">
                                                </ItemContextMenu>
                                            </Listeners>
                                            <Listeners>
                                                <AfterRender Handler="GridPanel(this);"></AfterRender>
                                            </Listeners>
                                        </ext:GridPanel>
                                        <ext:Menu ID="GridMenuAdd" runat="server">
                                                    <Items>
                                                        <ext:MenuItem ID="MenuItemAdd" runat="server" Text="添加货物" Handler="AddNewRow();" IconCls="diy_add">
                                                        </ext:MenuItem>
                                                    </Items>
                                                </ext:Menu>
                                        <ext:Menu ID="GridMenuRemove" runat="server">
                                                    <Items>
                                                        <ext:MenuItem ID="MenuItem1" runat="server" Text="移除货物" Handler="RemoveRow();" IconCls="diy_add">
                                                        </ext:MenuItem>
                                                    </Items>
                                                </ext:Menu>
                                    </Items>
                                </ext:Container>
                            </td>
                        </tr>
                        <tr>
                            <th class="k-header">结算方式</th>
                            <td colspan="2" style="width:13.32%">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                        <Items>
                                            <ext:TextField ID="txtJSModel" 
                                            runat="server" 
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="结算方式" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                             />
                                        </Items>
                                    </ext:Container>
                            </td>
                            <th style="width:6.66%" class="k-header">现付</th>
                            <td colspan="2" style="width:13.32%">
                               <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                        <Items>
                                            <ext:NumberField runat="server"
                                            ID="txtXF"
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="现付" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                            MinValue="0"
                                            TriggerCls="incicator"
                                            DecimalPrecision="2"
                                            >
                                                <Listeners>
                                                    <AfterRender Handler="afterrender(this);"></AfterRender>
                                                </Listeners>
                                            </ext:NumberField>
                                            
                                        </Items>
                                    </ext:Container>
                            </td>
                            <th style="width:6.66%" class="k-header">到付</th>
                            <td colspan="2" style="width:13.32%">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                        <Items>
                                            <ext:NumberField runat="server"
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="到付" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                            MinValue="0"
                                            DecimalPrecision="2">
                                                <Listeners>
                                                    <AfterRender Handler="afterrender(this);"></AfterRender>
                                                </Listeners>
                                            </ext:NumberField>
                                        </Items>
                                    </ext:Container>
                            </td>
                            <th style="width:6.66%" class="k-header">回单付</th>
                            <td colspan="2" style="width:13.32%">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                        <Items>
                                            <ext:NumberField runat="server"
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="回单付" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                            MinValue="0"
                                            DecimalPrecision="2"
                                             >
                                                <Listeners>
                                                    <AfterRender Handler="afterrender(this);"></AfterRender>
                                                </Listeners>
                                            </ext:NumberField>
                                        </Items>
                                    </ext:Container>
                            </td>
                            <th style="width:6.66%" class="k-header">月结</th>
                            <td colspan="2" style="width:13.42%">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                        <Items>
                                            <ext:NumberField runat="server"
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="月结"
                                             HideTrigger="true"
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                            MinValue="0"
                                            DecimalPrecision="2"
                                             >
                                                <Listeners>
                                                    <AfterRender Handler="afterrender(this);"></AfterRender>
                                                </Listeners>
                                            </ext:NumberField>
                                        </Items>
                                    </ext:Container>
                            </td>
                        </tr>
                        <tr>
                            <th class="k-header">短欠</th>
                            <td colspan="2">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                        <Items>
                                            <ext:NumberField runat="server"
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="短欠"
                                             HideTrigger="true"
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                            MinValue="0"
                                            DecimalPrecision="2"
                                             >
                                                <Listeners>
                                                    <AfterRender Handler="afterrender(this);"></AfterRender>
                                                </Listeners>
                                            </ext:NumberField>
                                        </Items>
                                    </ext:Container>
                            </td>
                            <th class="k-header">信息费</th>
                            <td colspan="2">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                        <Items>
                                            <ext:NumberField runat="server"
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="信息费"
                                             HideTrigger="true"
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                            MinValue="0"
                                            DecimalPrecision="2"
                                             >
                                                <Listeners>
                                                    <AfterRender Handler="afterrender(this);"></AfterRender>
                                                </Listeners>
                                            </ext:NumberField>
                                        </Items>
                                    </ext:Container>
                            </td>
                            <th class="k-header">交货方式</th>
                            <td colspan="2">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                    <Items>
                                        <ext:TextField ID="TextField1" 
                                            runat="server" 
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="交货方式" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                             />
                                    </Items>
                                </ext:Container>
                            </td>
                            <th class="k-header">备注</th>
                            <td rowspan="2" colspan="5">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                    <Items>
                                        <ext:TextArea ID="TextArea1" 
                                            runat="server"
                                            Rows="4"
                                            AllowBlank="false"   
                                            InputWidth="315"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="备注" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                             />
                                    </Items>
                                </ext:Container>
                            </td>
                        </tr>
                        <tr>
                            
                            <th class="k-header">回单要求</th>
                            <td colspan="2">
                                <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                    <Items>
                                        <ext:TextField ID="TextField2" 
                                            runat="server" 
                                            AllowBlank="false"   
                                            InputWidth="120"
                                            LabelAlign="Right" 
                                            LabelWidth="0" 
                                            EmptyText="交货方式" 
                                            LabelStyle="color:#417ac1;"
                                            FieldStyle="border-width:0px;border-color:#b5b8c8;"
                                             />
                                    </Items>
                                </ext:Container>
                            </td>
                            <td colspan="7" style="border:none; text-align:center">
                                
                                 <ext:Container runat="server" Layout="ColumnLayout" BorderSpec="width:0px;">
                                     <Items>
                                         <ext:Checkbox runat="server"
                                             ID="checkBoxFHMessage"
                                             FieldLabel="等通知放货"  
                                             LabelWidth="70" 
                                             LabelAlign="Right"
                                             Width="88"
                                             >
                                             <Listeners>
                                                 <%--<AfterRender Handler="checkBoxAfterRender(this);"></AfterRender>--%>
                                                 <BoxReady Handler="boxReady(this,width,height);"></BoxReady>
                                             </Listeners>
                                         </ext:Checkbox>

                                         <ext:Checkbox runat="server" ID="checkBoxFSMessage" 
                                             FieldLabel="发送短信(发货方)" 
                                             LabelWidth="100"
                                             Width="138"
                                              LabelStyle="margin-left:20px;"
                                             LabelAlign="Right">
                                             <Listeners>
                                                 <AfterRender Handler="checkBoxAfterRender(this);"></AfterRender>
                                                 <BoxReady Handler="boxReady(this,width,height);"></BoxReady>
                                             </Listeners>
                                         </ext:Checkbox>
                                         <ext:Checkbox runat="server" ID="checkBox1" 
                                             FieldLabel="收送短信(发货方)" 
                                             LabelWidth="100"
                                             Width="138"
                                              LabelStyle="margin-left:20px;"
                                             LabelAlign="Right">
                                             <Listeners>
                                                 <AfterRender Handler="checkBoxAfterRender(this);"></AfterRender>
                                                 <BoxReady Handler="boxReady(this,width,height);"></BoxReady>
                                             </Listeners>
                                         </ext:Checkbox>
                                     </Items>
                                 </ext:Container>
                                
                                <%--<input type="checkbox" title="控货" />等通知放货--%>
                            </td>
                            
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <th style="width:10%; padding-left:23px;">
                                开单网点</th>
                            <td style="width:10%;">
                                <ext:Label runat ="server" EmptyText="暂无" Width="200"></ext:Label>
                            </td>
                            <td style="width:60%;"></td>
                            <th style="width:10%;">业务员</th>
                            <td style="width:20%;">
                                <ext:Label runat ="server" EmptyText="暂无" Width="200"></ext:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                </div>
                
            </HtmlBin>
        </ext:Viewport>
        
    </form>
</body>
</html>
