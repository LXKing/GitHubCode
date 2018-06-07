﻿namespace DevLocalization
{
    using DevExpress.XtraReports.Localization;
    using System;

    public class XtraReportsLocalizer_zhchs : ReportResLocalizer
    {
        public override string GetLocalizedString(ReportStringId id)
        {
            switch (id)
            {
                case ReportStringId.Dlg_SaveFile_Title:
                    return "保存 '{0}'";

                case ReportStringId.Msg_WarningFontNameCantBeEmpty:
                    return "字体名不能为空";

                case ReportStringId.Msg_FileNotFound:
                    return "文件找不到。";

                case ReportStringId.Msg_WrongReportClassName:
                    return "在逆序列化过程中发生错误－可能由于错误的报表类名";

                case ReportStringId.Msg_CreateReportInstance:
                    return "当前编辑的报表与你尝试打开的报表类型不同。 <br/>你想打开已经选中的报表吗？";

                case ReportStringId.Msg_FileCorrupted:
                    return "不能载入报表。该文件可能被损坏或者报表的部件已经丢失。";

                case ReportStringId.Msg_FileContentCorrupted:
                    return "不能载入报表的布局设计。该文件可能已经损坏或者包含错误信息。";

                case ReportStringId.Msg_IncorrectArgument:
                    return "错误的参数值";

                case ReportStringId.Msg_InvalidMethodCall:
                    return "对象的当前状态使得方法调用失败。";

                case ReportStringId.Msg_ScriptError:
                    return "脚本中的错误如下:\r\n{0}";

                case ReportStringId.Msg_ScriptExecutionError:
                    return "在程序{0}中的脚本执行时发生错误:\r\n{1}\r\nProcedure {0} 已经执行, 它不会被再调用。";

                case ReportStringId.Msg_ScriptCodeIsNotCorrect:
                    return "输入的代码是不正确的";

                case ReportStringId.Msg_InvalidReportSource:
                    return "不能作为当前报表的子报表";

                case ReportStringId.Msg_IncorrectBandType:
                    return "错误的带区类型";

                case ReportStringId.Msg_InvPropName:
                    return "无效的属性名称";

                //case ReportStringId.Msg_CantFitBarcodeToControlBounds:
                //    return "对条形码来说，控制边界太小。";

                //case ReportStringId.Msg_InvalidBarcodeText:
                //    return "文本中存在无效字符";

                //case ReportStringId.Msg_InvalidBarcodeTextFormat:
                //    return "无效的文本格式";

                case ReportStringId.Msg_CreateSomeInstance:
                    return "在一个窗体上不能创建两个类实例";

                case ReportStringId.Msg_DontSupportMulticolumn:
                    return "详细报表不支持多列。";

                case ReportStringId.Msg_FillDataError:
                    return "在组装数据源的时候发生错误。程序抛出以下异常:";

                //case ReportStringId.Msg_CyclicBoormarks:
                //    return "报表循环书签";

                case ReportStringId.Msg_CyclicBookmarks:
                    return "报表中有循环书签。";

                case ReportStringId.Msg_LargeText:
                    return "文本内容太大。";

                case ReportStringId.Msg_ScriptingPermissionErrorMessage:
                    return "你没有足够的权限来执行报表中的这段脚本。\r\n\r\n具体细节:\r\n\r\n{0}";

                case ReportStringId.Msg_ReportImporting:
                    return "正在载如报表布局.请等待...";

                case ReportStringId.Msg_IncorrectPadding:
                    return "输入值必须等于或大于0.";

                case ReportStringId.Msg_WarningControlsAreOverlapped:
                    return "输出通知: 以下空间出现重叠并且可能导致输出到HTML、XLS、和RTF不正确 - {0}.";

                case ReportStringId.Msg_WarningControlsAreOutOfMargin:
                    return "打印通知: 以下控件超出右页边距, 这将导致额外的页面被打印 - {0}.";

                case ReportStringId.Msg_WarningUnsavedReports:
                    return "打印警告: Save the following reports to preview subreports with recent changes applied - {0}.";

                case ReportStringId.Msg_ShapeRotationToolTip:
                    return "使用Ctrl和鼠标左键来旋转成型";

                case ReportStringId.Msg_ContainsIllegalSymbols:
                    return "输入包含非法字符的格式化字符串.";

                case ReportStringId.Msg_WarningRemoveCalculatedFields:
                    return "该操作将从所有数据表中删除所有计算字段。您是否继续?";

                case ReportStringId.Msg_WarningRemoveParameters:
                    return "该操作将删除所有参数.您是否继续?";

                //case ReportStringId.Msg_ScriptingErrorTitle:
                //    return "脚本错误";

                case ReportStringId.Msg_ErrorTitle:
                    return "错误";

                case ReportStringId.Msg_SerializationErrorTitle:
                    return "错误";

                case ReportStringId.Msg_InvalidExpression:
                    return "指定的表达式包含非法字符(行 {0}, 列 {1}).";

                case ReportStringId.Msg_InvalidCondition:
                    return "条件必须是布尔值";

                case ReportStringId.Msg_GroupSortWarning:
                    return "该组页眉或页脚要删除不为空。你想删除这个区连同其组件？";

                case ReportStringId.Msg_GroupSortNoDataSource:
                    return "要添加新的分组或分类一级，首先提供报表的数据源。";

                case ReportStringId.Msg_NotEnoughMemoryToPaint:
                    return "没有足够的内存,缩放级别将被复位。";

                case ReportStringId.Msg_Caption:
                    return "XtraReports";

                case ReportStringId.Cmd_InsertDetailReport:
                    return "插入详细的报表";

                case ReportStringId.Cmd_InsertUnboundDetailReport:
                    return "未绑定的";

                case ReportStringId.Cmd_ViewCode:
                    return "查看代码";

                case ReportStringId.Cmd_BringToFront:
                    return "置于顶层";

                case ReportStringId.Cmd_SendToBack:
                    return "置于底层";

                case ReportStringId.Cmd_AlignToGrid:
                    return "对齐表格";

                case ReportStringId.Cmd_TopMargin:
                    return "书眉";

                case ReportStringId.Cmd_BottomMargin:
                    return "底部边距";

                case ReportStringId.Cmd_ReportHeader:
                    return "报表头";

                case ReportStringId.Cmd_ReportFooter:
                    return "报表尾";

                case ReportStringId.Cmd_PageHeader:
                    return "页眉";

                case ReportStringId.Cmd_PageFooter:
                    return "页脚";

                case ReportStringId.Cmd_GroupHeader:
                    return "分组头";

                case ReportStringId.Cmd_GroupFooter:
                    return "分组尾";

                case ReportStringId.Cmd_Detail:
                    return "详细内容";

                case ReportStringId.Cmd_DetailReport:
                    return "详细报表";

                case ReportStringId.Cmd_RtfClear:
                    return "清除";

                case ReportStringId.Cmd_RtfLoad:
                    return "载入文件...";

                case ReportStringId.Cmd_TableInsert:
                    return "插入";

                case ReportStringId.Cmd_TableInsertRowAbove:
                    return "向上插入一行";

                case ReportStringId.Cmd_TableInsertRowBelow:
                    return "向下插入一行";

                case ReportStringId.Cmd_TableInsertColumnToLeft:
                    return "向左插入一列";

                case ReportStringId.Cmd_TableInsertColumnToRight:
                    return "向右插入一列";

                case ReportStringId.Cmd_TableInsertCell:
                    return "插入单元格";

                case ReportStringId.Cmd_TableDelete:
                    return "删除表";

                case ReportStringId.Cmd_TableDeleteRow:
                    return "删除行";

                case ReportStringId.Cmd_TableDeleteColumn:
                    return "删除列";

                case ReportStringId.Cmd_TableDeleteCell:
                    return "删除单元格";

                case ReportStringId.Cmd_Cut:
                    return "剪切";

                case ReportStringId.Cmd_Copy:
                    return "复制";

                case ReportStringId.Cmd_Paste:
                    return "粘贴";

                case ReportStringId.Cmd_Delete:
                    return "删除";

                case ReportStringId.Cmd_Properties:
                    return "属性";

                case ReportStringId.Cmd_InsertBand:
                    return "插入带区";

                case ReportStringId.Cmd_BandMoveUp:
                    return "下移动";

                case ReportStringId.Cmd_BandMoveDown:
                    return "上移";

                case ReportStringId.Cmd_AddCalculatedField:
                    return "增加计算字段";

                case ReportStringId.Cmd_EditCalculatedFields:
                    return "编辑计算字段...";

                case ReportStringId.Cmd_ClearCalculatedFields:
                    return "删除所有计算字段";

                case ReportStringId.Cmd_AddParameter:
                    return "增加参数";

                case ReportStringId.Cmd_EditParameters:
                    return "编辑参数...";

                case ReportStringId.Cmd_ClearParameters:
                    return "删除所有参数";

                case ReportStringId.Cmd_DeleteCalculatedField:
                    return "删除";

                case ReportStringId.Cmd_DeleteParameter:
                    return "删除";

                case ReportStringId.Cmd_EditExpression:
                    return "编辑表达式...";

                case ReportStringId.CatLayout:
                    return "布局";

                case ReportStringId.CatAppearance:
                    return "外观";

                case ReportStringId.CatData:
                    return "数据";

                case ReportStringId.CatBehavior:
                    return "行为";

                case ReportStringId.CatNavigation:
                    return "导航";

                case ReportStringId.CatPageSettings:
                    return "页面设置";

                case ReportStringId.CatUserDesigner:
                    return "用户设计器";

                case ReportStringId.CatDesign:
                    return "设计";

                case ReportStringId.CatParameters:
                    return "参数";

                case ReportStringId.CatStructure:
                    return "结构";

                case ReportStringId.CatPrinting:
                    return "打印";

                case ReportStringId.BandDsg_QuantityPerPage:
                    return "一个带区/每页";

                case ReportStringId.BandDsg_QuantityPerReport:
                    return "一个带区/每页";

                case ReportStringId.UD_ReportDesigner:
                    return "报表设计器";

                case ReportStringId.UD_Msg_ReportChanged:
                    return "报表已经发生修改。想要保存修改后的内容吗？";

                case ReportStringId.UD_Msg_MdiReportChanged:
                    return "\"{0}\"已更改，是否保存更改？";

                case ReportStringId.UD_TTip_FileOpen:
                    return "打开文件";

                case ReportStringId.UD_TTip_FileSave:
                    return "保存文件";

                case ReportStringId.UD_TTip_EditCut:
                    return "剪切";

                case ReportStringId.UD_TTip_EditCopy:
                    return "复制";

                case ReportStringId.UD_TTip_EditPaste:
                    return "粘贴";

                case ReportStringId.UD_TTip_Undo:
                    return "撤销";

                case ReportStringId.UD_TTip_Redo:
                    return "重做";

                case ReportStringId.UD_TTip_AlignToGrid:
                    return "使网格对齐";

                case ReportStringId.UD_TTip_AlignLeft:
                    return "左对齐";

                case ReportStringId.UD_TTip_AlignVerticalCenters:
                    return "中间对齐";

                case ReportStringId.UD_TTip_AlignRight:
                    return "右对齐";

                case ReportStringId.UD_TTip_AlignTop:
                    return "顶端对齐";

                case ReportStringId.UD_TTip_AlignHorizontalCenters:
                    return "中间对齐";

                case ReportStringId.UD_TTip_AlignBottom:
                    return "底端对齐";

                case ReportStringId.UD_TTip_SizeToControlWidth:
                    return "宽度相等";

                case ReportStringId.UD_TTip_SizeToGrid:
                    return "均匀排列";

                case ReportStringId.UD_TTip_SizeToControlHeight:
                    return "高度相同";

                case ReportStringId.UD_TTip_SizeToControl:
                    return "尺寸大小相等";

                case ReportStringId.UD_TTip_HorizSpaceMakeEqual:
                    return "水平间距相等";

                case ReportStringId.UD_TTip_HorizSpaceIncrease:
                    return "增加水平间距";

                case ReportStringId.UD_TTip_HorizSpaceDecrease:
                    return "减少水平间距";

                case ReportStringId.UD_TTip_HorizSpaceConcatenate:
                    return "删除垂直间距";

                case ReportStringId.UD_TTip_VertSpaceMakeEqual:
                    return "垂直间距相等";

                case ReportStringId.UD_TTip_VertSpaceIncrease:
                    return "增加垂直间距";

                case ReportStringId.UD_TTip_VertSpaceDecrease:
                    return "减少垂直间距";

                case ReportStringId.UD_TTip_VertSpaceConcatenate:
                    return "删除水平间距";

                case ReportStringId.UD_TTip_CenterHorizontally:
                    return "水平居中";

                case ReportStringId.UD_TTip_CenterVertically:
                    return "垂直居中";

                case ReportStringId.UD_TTip_BringToFront:
                    return "置于顶层";

                case ReportStringId.UD_TTip_SendToBack:
                    return "置于底层";

                case ReportStringId.UD_TTip_FormatBold:
                    return "加粗";

                case ReportStringId.UD_TTip_FormatItalic:
                    return "斜体";

                case ReportStringId.UD_TTip_FormatUnderline:
                    return "下划线";

                case ReportStringId.UD_TTip_FormatAlignLeft:
                    return "左对齐";

                case ReportStringId.UD_TTip_FormatCenter:
                    return "居中";

                case ReportStringId.UD_TTip_FormatAlignRight:
                    return "右对齐";

                case ReportStringId.UD_TTip_FormatFontName:
                    return "字体名称";

                case ReportStringId.UD_TTip_FormatFontSize:
                    return "字体大小";

                case ReportStringId.UD_TTip_FormatForeColor:
                    return "前景色";

                case ReportStringId.UD_TTip_FormatBackColor:
                    return "背景色";

                case ReportStringId.UD_TTip_FormatJustify:
                    return "两边对齐";

                case ReportStringId.UD_TTip_ItemDescription:
                    return "拖放对象来创建一个绑定控件;或者用鼠标右键或者SHIFT拖拽对象来从弹出菜单中选择一个绑定控件;或者使用上下文菜单来增加一个计算字段或者参数.";

                case ReportStringId.UD_TTip_TableDescription:
                    return "拖放对象来创建一个表;或者用鼠标右键或者SHIFT拖拽对象来创建一个\"头\"表;或者使用上下文菜单来增加一个计算字段或者参数.";

                case ReportStringId.UD_TTip_DataMemberDescription:
                    return "\r\n\r\n数据成员: {0}";

                case ReportStringId.UD_FormCaption:
                    return "报表设计者";

                case ReportStringId.UD_XtraReportsToolboxCategoryName:
                    return "标准控件";

                case ReportStringId.UD_XtraReportsPointerItemCaption:
                    return "指示器";

                case ReportStringId.Verb_EditBands:
                    return "编辑带区...";

                case ReportStringId.Verb_EditGroupFields:
                    return "编辑字段组...";

                case ReportStringId.Verb_Import:
                    return "导入...";

                case ReportStringId.Verb_Save:
                    return "存储...";

                case ReportStringId.Verb_About:
                    return "关于...";

                case ReportStringId.Verb_RTFClear:
                    return "清除";

                case ReportStringId.Verb_RTFLoad:
                    return "载入文件...";

                case ReportStringId.Verb_FormatString:
                    return "格式化字符串...";

                case ReportStringId.Verb_SummaryWizard:
                    return "摘要...";

                case ReportStringId.Verb_ReportWizard:
                    return "导航...";

                case ReportStringId.Verb_Insert:
                    return "插入...";

                case ReportStringId.Verb_Delete:
                    return "删除...";

                case ReportStringId.Verb_Bind:
                    return "绑定";

                case ReportStringId.Verb_EditText:
                    return "编辑内容";

                case ReportStringId.Verb_AddFieldToArea:
                    return "在区域中增加字段";

                case ReportStringId.Verb_RunDesigner:
                    return "设计器...";

                case ReportStringId.Verb_RemoveInvalidBindings:
                    return "移除无效的绑定";

                case ReportStringId.FSForm_Lbl_Category:
                    return "种类";

                case ReportStringId.FSForm_Lbl_Prefix:
                    return "前缀:";

                case ReportStringId.FSForm_Lbl_Suffix:
                    return "后缀:";

                case ReportStringId.FSForm_Lbl_CustomGeneral:
                    return "通用格式没有具体的数字格式";

                case ReportStringId.FSForm_GrBox_Sample:
                    return "示例";

                case ReportStringId.FSForm_Tab_StandardTypes:
                    return "标准类型";

                case ReportStringId.FSForm_Tab_Custom:
                    return "自定义";

                case ReportStringId.FSForm_Msg_BadSymbol:
                    return "错误: 非法符号";

                case ReportStringId.FSForm_Btn_Delete:
                    return "删除";

                case ReportStringId.FSForm_Btn_Ok:
                    return "确定";

                case ReportStringId.FSForm_Btn_Cancel:
                    return "取消";

                case ReportStringId.FSForm_Text:
                    return "格式化字符串编辑器";

                case ReportStringId.FSForm_Cat_DateTime:
                    return "日期";

                case ReportStringId.FSForm_Cat_Int32:
                    return "32位整型";

                case ReportStringId.FSForm_Cat_Number:
                    return "数字";

                case ReportStringId.FSForm_Cat_Percent:
                    return "百分比";

                case ReportStringId.FSForm_Cat_Currency:
                    return "货币";

                case ReportStringId.FSForm_Cat_Special:
                    return "特殊";

                case ReportStringId.FSForm_Cat_General:
                    return "常规";

                case ReportStringId.BCForm_Lbl_Property:
                    return "属性";

                case ReportStringId.BCForm_Lbl_Binding:
                    return "绑定";

                case ReportStringId.FRSForm_Caption:
                    return "格式页编辑器";

                case ReportStringId.FRSForm_Msg_NoRuleSelected:
                    return "没有格式规则被选";

                case ReportStringId.FRSForm_Msg_MoreThanOneRule:
                    return "需选一种以上的格式规则";

                case ReportStringId.FRSForm_TTip_AddRule:
                    return "添加格式规则";

                case ReportStringId.FRSForm_TTip_RemoveRule:
                    return "删除格式规则";

                case ReportStringId.FRSForm_TTip_ClearRules:
                    return "清除格式规则";

                case ReportStringId.FRSForm_TTip_PurgeRules:
                    return "删除未使用的格式规则";

                case ReportStringId.SSForm_Caption:
                    return "风格编辑器";

                case ReportStringId.SSForm_Btn_Close:
                    return "关闭";

                case ReportStringId.SSForm_Msg_NoStyleSelected:
                    return "没有选中风格";

                case ReportStringId.SSForm_Msg_MoreThanOneStyle:
                    return "你已经选中了一种和多种风格";

                case ReportStringId.SSForm_Msg_SelectedStylesText:
                    return " 选中的风格...";

                case ReportStringId.SSForm_Msg_StyleSheetError:
                    return "风格表单错误";

                case ReportStringId.SSForm_Msg_InvalidFileFormat:
                    return "无效的文件格式";

                case ReportStringId.SSForm_Msg_StyleNamePreviewPostfix:
                    return "风格";

                case ReportStringId.SSForm_Msg_FileFilter:
                    return "报表风格单文件 (*.repss)|*.repss|所有的文件(*.*)|*.*";

                case ReportStringId.SSForm_TTip_AddStyle:
                    return "添加风格";

                case ReportStringId.SSForm_TTip_RemoveStyle:
                    return "删除风格";

                case ReportStringId.SSForm_TTip_ClearStyles:
                    return "清除风格";

                case ReportStringId.SSForm_TTip_PurgeStyles:
                    return "删除未使用的风格";

                case ReportStringId.SSForm_TTip_SaveStyles:
                    return "将风格保存到文件";

                case ReportStringId.SSForm_TTip_LoadStyles:
                    return "从文件中导入风格";

                case ReportStringId.SR_Side_Margins:
                    return "边距";

                case ReportStringId.SR_Top_Margin:
                    return "上边距";

                case ReportStringId.SR_Vertical_Pitch:
                    return "垂直倾斜";

                case ReportStringId.SR_Horizontal_Pitch:
                    return "水平倾斜";

                case ReportStringId.SR_Width:
                    return "宽度";

                case ReportStringId.SR_Height:
                    return "高度";

                case ReportStringId.SR_Number_Down:
                    return "数字下降";

                case ReportStringId.SR_Number_Across:
                    return "数字交叉";

                case ReportStringId.ScriptEditor_ErrorDescription:
                    return "描述";

                case ReportStringId.ScriptEditor_ErrorLine:
                    return "线";

                case ReportStringId.ScriptEditor_ErrorColumn:
                    return "列";

                case ReportStringId.ScriptEditor_Validate:
                    return "验证";

                case ReportStringId.ScriptEditor_ScriptsAreValid:
                    return "所有的脚本都有效。";

                case ReportStringId.ScriptEditor_ScriptHasBeenChanged:
                    return "错误日志是实际的脚本无关，因为脚本已被更改后，其最后的验证。\r\n要看到实际的脚本错误，单击验证按钮";

                case ReportStringId.ScriptEditor_ClickValidate:
                    return "单击 \"验证\" 检验脚本。";

                case ReportStringId.ScriptEditor_NewString:
                    return "(New)";

                //case ReportStringId.FindForm_Msg_FinishedSearching:
                //    return "已经搜索完整个文档";

                //case ReportStringId.FindForm_Msg_TotalFound:
                //    return "总共发现有: ";

                case ReportStringId.RepTabCtl_HtmlView:
                    return "HTML视图";

                case ReportStringId.RepTabCtl_Preview:
                    return "预览";

                case ReportStringId.RepTabCtl_Designer:
                    return "设计视图";

                case ReportStringId.RepTabCtl_Scripts:
                    return "脚本";

                case ReportStringId.RepTabCtl_ReportStatus:
                    return "{0} {{ 纸张类型: {1} }}";

                case ReportStringId.PanelDesignMsg:
                    return "请在这里放置控件以便于拼装";

                case ReportStringId.MultiColumnDesignMsg1:
                    return "重复列的间距";

                case ReportStringId.MultiColumnDesignMsg2:
                    return "放置于此的控件不能正确打印";

                case ReportStringId.UD_Group_File:
                    return "文件";

                case ReportStringId.UD_Group_Edit:
                    return "编辑";

                case ReportStringId.UD_Group_View:
                    return "视图";

                case ReportStringId.UD_Group_Format:
                    return "格式化";

                case ReportStringId.UD_Group_Window:
                    return "窗口(&W)";

                case ReportStringId.UD_Group_Font:
                    return "字体";

                case ReportStringId.UD_Group_Justify:
                    return "左右对齐";

                case ReportStringId.UD_Group_Align:
                    return "对齐";

                case ReportStringId.UD_Group_MakeSameSize:
                    return "尺寸相等";

                case ReportStringId.UD_Group_HorizontalSpacing:
                    return "水平间距";

                case ReportStringId.UD_Group_VerticalSpacing:
                    return "垂直间距";

                case ReportStringId.UD_Group_CenterInForm:
                    return "居中";

                case ReportStringId.UD_Group_Order:
                    return "序号";

                case ReportStringId.UD_Group_ToolbarsList:
                    return "工具栏";

                case ReportStringId.UD_Group_DockPanelsList:
                    return "窗口";

                case ReportStringId.UD_Group_TabButtonsList:
                    return "标签按钮";

                case ReportStringId.UD_Capt_MainMenuName:
                    return "主菜单";

                case ReportStringId.UD_Capt_ToolbarName:
                    return "主工具栏";

                case ReportStringId.UD_Capt_LayoutToolbarName:
                    return "布局设计工具栏";

                case ReportStringId.UD_Capt_FormattingToolbarName:
                    return "格式化工具栏";

                case ReportStringId.UD_Capt_StatusBarName:
                    return "状态条";

                case ReportStringId.UD_Capt_ZoomToolbarName:
                    return "缩放工具";

                case ReportStringId.UD_Capt_NewReport:
                    return "创建新报表";

                case ReportStringId.UD_Capt_NewWizardReport:
                    return "报表导航...";

                case ReportStringId.UD_Capt_OpenFile:
                    return "打开文件...";

                case ReportStringId.UD_Capt_SaveFile:
                    return "保存";

                case ReportStringId.UD_Capt_SaveFileAs:
                    return "另存为...";

                case ReportStringId.UD_Capt_SaveAll:
                    return "保存所有(&L)";

                case ReportStringId.UD_Capt_Exit:
                    return "退出";

                case ReportStringId.UD_Capt_TabbedInterface:
                    return "标签式界面(&T)";

                case ReportStringId.UD_Capt_MdiCascade:
                    return "层叠(&C)";

                case ReportStringId.UD_Capt_MdiTileHorizontal:
                    return "水平平铺(&H)";

                case ReportStringId.UD_Capt_MdiTileVertical:
                    return "垂直平铺(&V)";

                case ReportStringId.UD_Capt_Close:
                    return "关闭(&C)";

                case ReportStringId.UD_Capt_Cut:
                    return "剪切";

                case ReportStringId.UD_Capt_Copy:
                    return "复制";

                case ReportStringId.UD_Capt_Paste:
                    return "粘贴";

                case ReportStringId.UD_Capt_Delete:
                    return "删除";

                case ReportStringId.UD_Capt_SelectAll:
                    return "选择所有";

                case ReportStringId.UD_Capt_Undo:
                    return "撤销";

                case ReportStringId.UD_Capt_Redo:
                    return "重做";

                case ReportStringId.UD_Capt_ForegroundColor:
                    return "前景色";

                case ReportStringId.UD_Capt_BackGroundColor:
                    return "背景色";

                case ReportStringId.UD_Capt_FontBold:
                    return "加粗";

                case ReportStringId.UD_Capt_FontItalic:
                    return "斜线";

                case ReportStringId.UD_Capt_FontUnderline:
                    return "下划线";

                case ReportStringId.UD_Capt_JustifyLeft:
                    return "左对齐";

                case ReportStringId.UD_Capt_JustifyCenter:
                    return "居中对齐";

                case ReportStringId.UD_Capt_JustifyRight:
                    return "右对齐";

                case ReportStringId.UD_Capt_JustifyJustify:
                    return "两边对齐";

                case ReportStringId.UD_Capt_AlignLefts:
                    return "左对齐";

                case ReportStringId.UD_Capt_AlignCenters:
                    return "居中对齐";

                case ReportStringId.UD_Capt_AlignRights:
                    return "右对齐";

                case ReportStringId.UD_Capt_AlignTops:
                    return "顶端对齐";

                case ReportStringId.UD_Capt_AlignMiddles:
                    return "中间对齐";

                case ReportStringId.UD_Capt_AlignBottoms:
                    return "底端对齐";

                case ReportStringId.UD_Capt_AlignToGrid:
                    return "对齐网格";

                case ReportStringId.UD_Capt_MakeSameSizeWidth:
                    return "宽度";

                case ReportStringId.UD_Capt_MakeSameSizeSizeToGrid:
                    return "均匀排列";

                case ReportStringId.UD_Capt_MakeSameSizeHeight:
                    return "高度";

                case ReportStringId.UD_Capt_MakeSameSizeBoth:
                    return "ReportStringId.UD_Capt_MakeSameS宽度/高度";

                case ReportStringId.UD_Capt_SpacingMakeEqual:
                    return "间距相等";

                case ReportStringId.UD_Capt_SpacingIncrease:
                    return "增加间距";

                case ReportStringId.UD_Capt_SpacingDecrease:
                    return "减少间距";

                case ReportStringId.UD_Capt_SpacingRemove:
                    return "删除间距";

                case ReportStringId.UD_Capt_CenterInFormHorizontally:
                    return "水平方向";

                case ReportStringId.UD_Capt_CenterInFormVertically:
                    return "垂直方向";

                case ReportStringId.UD_Capt_OrderBringToFront:
                    return "置于顶层";

                case ReportStringId.UD_Capt_OrderSendToBack:
                    return "置于底层";

                case ReportStringId.UD_Capt_Zoom:
                    return "缩放";

                case ReportStringId.UD_Capt_ZoomIn:
                    return "放大";

                case ReportStringId.UD_Capt_ZoomOut:
                    return "缩小";

                case ReportStringId.UD_Capt_ZoomFactor:
                    return "缩放比例: {0}%";

                case ReportStringId.UD_Hint_NewReport:
                    return "创建一个新的空报表";

                case ReportStringId.UD_Hint_NewWizardReport:
                    return "使用导航功能，创建一个新报表";

                case ReportStringId.UD_Hint_OpenFile:
                    return "打开报表";

                case ReportStringId.UD_Hint_SaveFile:
                    return "保存报表";

                case ReportStringId.UD_Hint_SaveFileAs:
                    return "用一个新名称来保存报表";

                case ReportStringId.UD_Hint_SaveAll:
                    return "保存所有报表";

                case ReportStringId.UD_Hint_Exit:
                    return "关闭设计器";

                case ReportStringId.UD_Hint_Close:
                    return "关闭报表";

                case ReportStringId.UD_Hint_TabbedInterface:
                    return "转换为多文档界面选项卡";

                case ReportStringId.UD_Hint_MdiCascade:
                    return "层叠排列全部打开的文档，使他们彼此重叠";

                case ReportStringId.UD_Hint_MdiTileHorizontal:
                    return "从上到下排列全部打开的文档";

                case ReportStringId.UD_Hint_MdiTileVertical:
                    return "从左到右排列全部打开的文档";

                case ReportStringId.UD_Hint_Cut:
                    return "删除该控件并将它拷贝到剪贴板";

                case ReportStringId.UD_Hint_Copy:
                    return "将控件拷贝到剪贴板";

                case ReportStringId.UD_Hint_Paste:
                    return "从剪贴板上添加一个控件";

                case ReportStringId.UD_Hint_Delete:
                    return "删除控件";

                case ReportStringId.UD_Hint_SelectAll:
                    return "选择文档里面的所有控件";

                case ReportStringId.UD_Hint_Undo:
                    return "撤销上一次操作";

                case ReportStringId.UD_Hint_Redo:
                    return "重做上一次操作";

                case ReportStringId.UD_Hint_ForegroundColor:
                    return "设置控件的前景色";

                case ReportStringId.UD_Hint_BackGroundColor:
                    return "设置控件的背景色";

                case ReportStringId.UD_Hint_FontBold:
                    return "使字体变粗";

                case ReportStringId.UD_Hint_FontItalic:
                    return "使字体变斜";

                case ReportStringId.UD_Hint_FontUnderline:
                    return "字体加下划线";

                case ReportStringId.UD_Hint_JustifyLeft:
                    return "控件内容左对齐";

                case ReportStringId.UD_Hint_JustifyCenter:
                    return "控件内容居中对齐";

                case ReportStringId.UD_Hint_JustifyRight:
                    return "控件内容右对齐";

                case ReportStringId.UD_Hint_JustifyJustify:
                    return "控件内容两边对齐";

                case ReportStringId.UD_Hint_AlignLefts:
                    return "将选择的控件左对齐";

                case ReportStringId.UD_Hint_AlignCenters:
                    return "将选择的控件垂直居中对齐";

                case ReportStringId.UD_Hint_AlignRights:
                    return "将选择的控件右对齐";

                case ReportStringId.UD_Hint_AlignTops:
                    return "将选择的控件顶端对齐";

                case ReportStringId.UD_Hint_AlignMiddles:
                    return "将选择的控件水平居中对齐";

                case ReportStringId.UD_Hint_AlignBottoms:
                    return "将选择的控件底端对齐";

                case ReportStringId.UD_Hint_AlignToGrid:
                    return "将选择的控件与网格对齐";

                case ReportStringId.UD_Hint_MakeSameSizeWidth:
                    return "使选择的控件宽度相等";

                case ReportStringId.UD_Hint_MakeSameSizeSizeToGrid:
                    return "调整选择的控件使网格对齐";

                case ReportStringId.UD_Hint_MakeSameSizeHeight:
                    return "使选择的控件高度相等";

                case ReportStringId.UD_Hint_MakeSameSizeBoth:
                    return "使选择的控件尺寸相等";

                case ReportStringId.UD_Hint_SpacingMakeEqual:
                    return "使选择的控件间距相等";

                case ReportStringId.UD_Hint_SpacingIncrease:
                    return "增加选择的控件间距";

                case ReportStringId.UD_Hint_SpacingDecrease:
                    return "减少选择的控件间距";

                case ReportStringId.UD_Hint_SpacingRemove:
                    return "删除选择的控件间距";

                case ReportStringId.UD_Hint_CenterInFormHorizontally:
                    return "在一个带区内使选择的控件水平居中";

                case ReportStringId.UD_Hint_CenterInFormVertically:
                    return "在一个带区内使选择的控件垂直居中";

                case ReportStringId.UD_Hint_OrderBringToFront:
                    return "使选择的控件置于顶层";

                case ReportStringId.UD_Hint_OrderSendToBack:
                    return "使选择的控件置于底层";

                case ReportStringId.UD_Hint_Zoom:
                    return "选择/输入 缩放率";

                case ReportStringId.UD_Hint_ZoomIn:
                    return "放大设计界面";

                case ReportStringId.UD_Hint_ZoomOut:
                    return "缩小设计界面";

                case ReportStringId.UD_Hint_ViewBars:
                    return "隐藏/显示 {0}";

                case ReportStringId.UD_Hint_ViewDockPanels:
                    return "隐藏/显示 {0} window";

                case ReportStringId.UD_Hint_ViewTabs:
                    return "切换到 {0} tab";

                case ReportStringId.UD_PropertyGrid_NotSetText:
                    return "(未设置)";

                case ReportStringId.UD_SaveFileDialog_DialogFilter:
                    return "报表文件 (*{0})|*{1}|所有文件 (*.*)|*.*";

                case ReportStringId.UD_Title_FieldList:
                    return "字段列表";

                case ReportStringId.UD_Title_FieldList_NonePickerNodeText:
                    return "无";

                case ReportStringId.UD_Title_FieldList_NoneNodeText:
                    return "(无)";

                case ReportStringId.UD_Title_FieldList_ProjectObjectsText:
                    return "工程对象";

                case ReportStringId.UD_Title_FieldList_AddNewDataSourceText:
                    return "添加新数据源";

                case ReportStringId.UD_Title_GroupAndSort:
                    return "分组和排序";

                case ReportStringId.UD_Title_ErrorList:
                    return "脚本错误";

                case ReportStringId.UD_Title_ReportExplorer:
                    return "报表导出工具";

                case ReportStringId.UD_Title_PropertyGrid:
                    return "属性";

                case ReportStringId.UD_Title_ToolBox:
                    return "工具箱";

                case ReportStringId.STag_Name_DataBinding:
                    return "数据绑定";

                case ReportStringId.STag_Name_FormatString:
                    return "格式化字符串";

                case ReportStringId.STag_Name_Checked:
                    return "选中";

                case ReportStringId.STag_Name_PreviewRowCount:
                    return "预览行数";

                case ReportStringId.STag_Name_Bands:
                    return "带区";

                case ReportStringId.STag_Name_Height:
                    return "高度";

                case ReportStringId.STag_Name_ColumnMode:
                    return "列模式";

                case ReportStringId.STag_Name_ColumnCount:
                    return "列数";

                case ReportStringId.STag_Name_ColumnWidth:
                    return "列宽";

                case ReportStringId.STag_Name_ColumnSpacing:
                    return "列间距";

                case ReportStringId.STag_Name_ColumnLayout:
                    return "多行";

                case ReportStringId.STag_Name_FieldArea:
                    return "新字段区域";

                case ReportStringId.STag_Capt_Tasks:
                    return "任务";

                case ReportStringId.STag_Capt_Format:
                    return "{0} {1}";

                case ReportStringId.RibbonXRDesign_PageText:
                    return "报表设计器";

                case ReportStringId.RibbonXRDesign_HtmlPageText:
                    return "HTML视图";

                case ReportStringId.RibbonXRDesign_StatusBar_HtmlProcessing:
                    return "处理中...";

                case ReportStringId.RibbonXRDesign_StatusBar_HtmlDone:
                    return "完成";

                case ReportStringId.RibbonXRDesign_PageGroup_Report:
                    return "报表";

                case ReportStringId.RibbonXRDesign_PageGroup_Edit:
                    return "编辑";

                case ReportStringId.RibbonXRDesign_PageGroup_Font:
                    return "字体";

                case ReportStringId.RibbonXRDesign_PageGroup_Alignment:
                    return "排列";

                case ReportStringId.RibbonXRDesign_PageGroup_SizeAndLayout:
                    return "布局";

                case ReportStringId.RibbonXRDesign_PageGroup_Zoom:
                    return "缩放";

                case ReportStringId.RibbonXRDesign_PageGroup_View:
                    return "视图";

                case ReportStringId.RibbonXRDesign_PageGroup_Scripts:
                    return "脚本";

                case ReportStringId.RibbonXRDesign_PageGroup_HtmlNavigation:
                    return "导航";

                case ReportStringId.RibbonXRDesign_AlignToGrid_Caption:
                    return "网格对齐";

                case ReportStringId.RibbonXRDesign_AlignLeft_Caption:
                    return "左对齐";

                case ReportStringId.RibbonXRDesign_AlignVerticalCenters_Caption:
                    return "居中对齐";

                case ReportStringId.RibbonXRDesign_AlignRight_Caption:
                    return "右对齐";

                case ReportStringId.RibbonXRDesign_AlignTop_Caption:
                    return "顶部对齐";

                case ReportStringId.RibbonXRDesign_AlignHorizontalCenters_Caption:
                    return "中间对齐";

                case ReportStringId.RibbonXRDesign_AlignBottom_Caption:
                    return "底部对齐";

                case ReportStringId.RibbonXRDesign_SizeToControlWidth_Caption:
                    return "置为相同宽度";

                case ReportStringId.RibbonXRDesign_SizeToGrid_Caption:
                    return "网格尺寸";

                case ReportStringId.RibbonXRDesign_SizeToControlHeight_Caption:
                    return "置为相同高度";

                case ReportStringId.RibbonXRDesign_SizeToControl_Caption:
                    return "置为相同大小";

                case ReportStringId.RibbonXRDesign_HorizSpaceMakeEqual_Caption:
                    return "水平间距等距";

                case ReportStringId.RibbonXRDesign_HorizSpaceIncrease_Caption:
                    return "增加水平间距";

                case ReportStringId.RibbonXRDesign_HorizSpaceDecrease_Caption:
                    return "减少水平间距";

                case ReportStringId.RibbonXRDesign_HorizSpaceConcatenate_Caption:
                    return "删除水平间距";

                case ReportStringId.RibbonXRDesign_VertSpaceMakeEqual_Caption:
                    return "垂直等距";

                case ReportStringId.RibbonXRDesign_VertSpaceIncrease_Caption:
                    return "增加垂直间距";

                case ReportStringId.RibbonXRDesign_VertSpaceDecrease_Caption:
                    return "减少垂直间距";

                case ReportStringId.RibbonXRDesign_VertSpaceConcatenate_Caption:
                    return "删除垂直间距";

                case ReportStringId.RibbonXRDesign_CenterHorizontally_Caption:
                    return "水平居中";

                case ReportStringId.RibbonXRDesign_CenterVertically_Caption:
                    return "垂直居中";

                case ReportStringId.RibbonXRDesign_BringToFront_Caption:
                    return "置于顶部";

                case ReportStringId.RibbonXRDesign_SendToBack_Caption:
                    return "置于底部";

                case ReportStringId.RibbonXRDesign_FontBold_Caption:
                    return "粗体";

                case ReportStringId.RibbonXRDesign_FontItalic_Caption:
                    return "斜体";

                case ReportStringId.RibbonXRDesign_FontUnderline_Caption:
                    return "下划线";

                case ReportStringId.RibbonXRDesign_ForeColor_Caption:
                    return "前景色";

                case ReportStringId.RibbonXRDesign_BackColor_Caption:
                    return "背景颜色";

                case ReportStringId.RibbonXRDesign_JustifyLeft_Caption:
                    return "左对齐文本";

                case ReportStringId.RibbonXRDesign_JustifyCenter_Caption:
                    return "文本居中";

                case ReportStringId.RibbonXRDesign_JustifyRight_Caption:
                    return "右对齐文本";

                case ReportStringId.RibbonXRDesign_JustifyJustify_Caption:
                    return "自适应";

                case ReportStringId.RibbonXRDesign_NewReport_Caption:
                    return "新建报表";

                case ReportStringId.RibbonXRDesign_NewReportWizard_Caption:
                    return "使用向导新建立报表...";

                case ReportStringId.RibbonXRDesign_OpenFile_Caption:
                    return "打开...";

                case ReportStringId.RibbonXRDesign_SaveFile_Caption:
                    return "保存";

                case ReportStringId.RibbonXRDesign_SaveFileAs_Caption:
                    return "另存为...";

                case ReportStringId.RibbonXRDesign_SaveAll_Caption:
                    return "保存所有";

                case ReportStringId.RibbonXRDesign_Cut_Caption:
                    return "剪切";

                case ReportStringId.RibbonXRDesign_Copy_Caption:
                    return "复制";

                case ReportStringId.RibbonXRDesign_Paste_Caption:
                    return "粘贴";

                case ReportStringId.RibbonXRDesign_Undo_Caption:
                    return "撤消";

                case ReportStringId.RibbonXRDesign_Redo_Caption:
                    return "重做";

                case ReportStringId.RibbonXRDesign_Exit_Caption:
                    return "退出";

                case ReportStringId.RibbonXRDesign_Close_Caption:
                    return "关闭";

                case ReportStringId.RibbonXRDesign_Zoom_Caption:
                    return "缩放";

                case ReportStringId.RibbonXRDesign_ZoomIn_Caption:
                    return "放大";

                case ReportStringId.RibbonXRDesign_ZoomOut_Caption:
                    return "缩小";

                case ReportStringId.RibbonXRDesign_ZoomExact_Caption:
                    return "要求:";

                case ReportStringId.RibbonXRDesign_Windows_Caption:
                    return "窗口";

                case ReportStringId.RibbonXRDesign_Scripts_Caption:
                    return "脚本";

                case ReportStringId.RibbonXRDesign_HtmlHome_Caption:
                    return "主页";

                case ReportStringId.RibbonXRDesign_HtmlBackward_Caption:
                    return "返回";

                case ReportStringId.RibbonXRDesign_HtmlForward_Caption:
                    return "下页";

                case ReportStringId.RibbonXRDesign_HtmlRefresh_Caption:
                    return "刷新";

                case ReportStringId.RibbonXRDesign_HtmlFind_Caption:
                    return "查找";

                case ReportStringId.RibbonXRDesign_AlignToGrid_STipTitle:
                    return "网格对齐";

                case ReportStringId.RibbonXRDesign_AlignLeft_STipTitle:
                    return "左对齐";

                case ReportStringId.RibbonXRDesign_AlignVerticalCenters_STipTitle:
                    return "居中对齐";

                case ReportStringId.RibbonXRDesign_AlignRight_STipTitle:
                    return "右对齐";

                case ReportStringId.RibbonXRDesign_AlignTop_STipTitle:
                    return "顶部对齐";

                case ReportStringId.RibbonXRDesign_AlignHorizontalCenters_STipTitle:
                    return "中间对齐";

                case ReportStringId.RibbonXRDesign_AlignBottom_STipTitle:
                    return "底部对齐";

                case ReportStringId.RibbonXRDesign_SizeToControlWidth_STipTitle:
                    return "置为相同宽度";

                case ReportStringId.RibbonXRDesign_SizeToGrid_STipTitle:
                    return "网格尺寸";

                case ReportStringId.RibbonXRDesign_SizeToControlHeight_STipTitle:
                    return "置为相同高度";

                case ReportStringId.RibbonXRDesign_SizeToControl_STipTitle:
                    return "置为相同大小";

                case ReportStringId.RibbonXRDesign_HorizSpaceMakeEqual_STipTitle:
                    return "水平间距等距";

                case ReportStringId.RibbonXRDesign_HorizSpaceIncrease_STipTitle:
                    return "增加水平间距";

                case ReportStringId.RibbonXRDesign_HorizSpaceDecrease_STipTitle:
                    return "减少水平间距";

                case ReportStringId.RibbonXRDesign_HorizSpaceConcatenate_STipTitle:
                    return "删除水平间距";

                case ReportStringId.RibbonXRDesign_VertSpaceMakeEqual_STipTitle:
                    return "垂直等距";

                case ReportStringId.RibbonXRDesign_VertSpaceIncrease_STipTitle:
                    return "增加垂直间距";

                case ReportStringId.RibbonXRDesign_VertSpaceDecrease_STipTitle:
                    return "减少垂直间距";

                case ReportStringId.RibbonXRDesign_VertSpaceConcatenate_STipTitle:
                    return "删除垂直间距";

                case ReportStringId.RibbonXRDesign_CenterHorizontally_STipTitle:
                    return "水平居中";

                case ReportStringId.RibbonXRDesign_CenterVertically_STipTitle:
                    return "垂直居中";

                case ReportStringId.RibbonXRDesign_BringToFront_STipTitle:
                    return "置于顶部";

                case ReportStringId.RibbonXRDesign_SendToBack_STipTitle:
                    return "置于底部";

                case ReportStringId.RibbonXRDesign_FontBold_STipTitle:
                    return "粗体";

                case ReportStringId.RibbonXRDesign_FontItalic_STipTitle:
                    return "斜体";

                case ReportStringId.RibbonXRDesign_FontUnderline_STipTitle:
                    return "下划线";

                case ReportStringId.RibbonXRDesign_ForeColor_STipTitle:
                    return "前景色";

                case ReportStringId.RibbonXRDesign_BackColor_STipTitle:
                    return "背景颜色";

                case ReportStringId.RibbonXRDesign_JustifyLeft_STipTitle:
                    return "左对齐文本";

                case ReportStringId.RibbonXRDesign_JustifyCenter_STipTitle:
                    return "文本居中";

                case ReportStringId.RibbonXRDesign_JustifyRight_STipTitle:
                    return "右对齐文本";

                case ReportStringId.RibbonXRDesign_JustifyJustify_STipTitle:
                    return "自适应";

                case ReportStringId.RibbonXRDesign_NewReport_STipTitle:
                    return "新建空白报表";

                case ReportStringId.RibbonXRDesign_NewReportWizard_STipTitle:
                    return "使用向导建立新报表";

                case ReportStringId.RibbonXRDesign_OpenFile_STipTitle:
                    return "打开报表";

                case ReportStringId.RibbonXRDesign_SaveFile_STipTitle:
                    return "保存报表";

                case ReportStringId.RibbonXRDesign_SaveFileAs_STipTitle:
                    return "报表另存为";

                case ReportStringId.RibbonXRDesign_SaveAll_STipTitle:
                    return "保存所有报表";

                case ReportStringId.RibbonXRDesign_Cut_STipTitle:
                    return "剪切";

                case ReportStringId.RibbonXRDesign_Copy_STipTitle:
                    return "复制";

                case ReportStringId.RibbonXRDesign_Paste_STipTitle:
                    return "粘贴";

                case ReportStringId.RibbonXRDesign_Undo_STipTitle:
                    return "撤消";

                case ReportStringId.RibbonXRDesign_Redo_STipTitle:
                    return "重做";

                case ReportStringId.RibbonXRDesign_Exit_STipTitle:
                    return "退出";

                case ReportStringId.RibbonXRDesign_Close_STipTitle:
                    return "关闭";

                case ReportStringId.RibbonXRDesign_Zoom_STipTitle:
                    return "缩放";

                case ReportStringId.RibbonXRDesign_ZoomIn_STipTitle:
                    return "放大";

                case ReportStringId.RibbonXRDesign_ZoomOut_STipTitle:
                    return "缩小";

                case ReportStringId.RibbonXRDesign_FontName_STipTitle:
                    return "字体";

                case ReportStringId.RibbonXRDesign_FontSize_STipTitle:
                    return "字体大小";

                case ReportStringId.RibbonXRDesign_Windows_STipTitle:
                    return "显示/隐藏窗口";

                case ReportStringId.RibbonXRDesign_Scripts_STipTitle:
                    return "显示/隐藏脚本";

                case ReportStringId.RibbonXRDesign_HtmlHome_STipTitle:
                    return "主页";

                case ReportStringId.RibbonXRDesign_HtmlBackward_STipTitle:
                    return "返回";

                case ReportStringId.RibbonXRDesign_HtmlForward_STipTitle:
                    return "下页";

                case ReportStringId.RibbonXRDesign_HtmlRefresh_STipTitle:
                    return "刷新";

                case ReportStringId.RibbonXRDesign_HtmlFind_STipTitle:
                    return "查找";

                case ReportStringId.RibbonXRDesign_AlignToGrid_STipContent:
                    return "网格对齐所选控件位置";

                case ReportStringId.RibbonXRDesign_AlignLeft_STipContent:
                    return "左对齐所选控件";

                case ReportStringId.RibbonXRDesign_AlignVerticalCenters_STipContent:
                    return "沿垂直方向居中对齐所选控件";

                case ReportStringId.RibbonXRDesign_AlignRight_STipContent:
                    return "右对齐所选控件";

                case ReportStringId.RibbonXRDesign_AlignTop_STipContent:
                    return "顶部对齐所选控件";

                case ReportStringId.RibbonXRDesign_AlignHorizontalCenters_STipContent:
                    return "沿水平方向中间对齐所选控件";

                case ReportStringId.RibbonXRDesign_AlignBottom_STipContent:
                    return "底部对齐所选控件";

                case ReportStringId.RibbonXRDesign_SizeToControlWidth_STipContent:
                    return "将所选控件置为相同宽度.";

                case ReportStringId.RibbonXRDesign_SizeToGrid_STipContent:
                    return "所选控件网格尺寸.";

                case ReportStringId.RibbonXRDesign_SizeToControlHeight_STipContent:
                    return "将所选控件置为相同高度.";

                case ReportStringId.RibbonXRDesign_SizeToControl_STipContent:
                    return "将所选控件置为相同大小.";

                case ReportStringId.RibbonXRDesign_HorizSpaceMakeEqual_STipContent:
                    return "使所选控件水平间距相等.";

                case ReportStringId.RibbonXRDesign_HorizSpaceIncrease_STipContent:
                    return "增加所选控件之间的水平间距.";

                case ReportStringId.RibbonXRDesign_HorizSpaceDecrease_STipContent:
                    return "减少所选控件之间的水平间距.";

                case ReportStringId.RibbonXRDesign_HorizSpaceConcatenate_STipContent:
                    return "删除所选控件之间的水平间距.";

                case ReportStringId.RibbonXRDesign_VertSpaceMakeEqual_STipContent:
                    return "使所选控件垂直等距.";

                case ReportStringId.RibbonXRDesign_VertSpaceIncrease_STipContent:
                    return "增加所选控件的垂直间距.";

                case ReportStringId.RibbonXRDesign_VertSpaceDecrease_STipContent:
                    return "减少所选控件的垂直间距.";

                case ReportStringId.RibbonXRDesign_VertSpaceConcatenate_STipContent:
                    return "删除所选控件的垂直间距.";

                case ReportStringId.RibbonXRDesign_CenterHorizontally_STipContent:
                    return "水平居中一个带区内所选控件.";

                case ReportStringId.RibbonXRDesign_CenterVertically_STipContent:
                    return "垂直居中一个带区内所选控件.";

                case ReportStringId.RibbonXRDesign_BringToFront_STipContent:
                    return "将所选控件置于顶部.";

                case ReportStringId.RibbonXRDesign_SendToBack_STipContent:
                    return "将所选控件置于底部.";

                case ReportStringId.RibbonXRDesign_FontBold_STipContent:
                    return "使所选文字为粗体.";

                case ReportStringId.RibbonXRDesign_FontItalic_STipContent:
                    return "使所选文字为斜体.";

                case ReportStringId.RibbonXRDesign_FontUnderline_STipContent:
                    return "使所选文字加下划线.";

                case ReportStringId.RibbonXRDesign_ForeColor_STipContent:
                    return "改变文字前景色.";

                case ReportStringId.RibbonXRDesign_BackColor_STipContent:
                    return "改变文字背景颜色.";

                case ReportStringId.RibbonXRDesign_JustifyLeft_STipContent:
                    return "左对齐文本.";

                case ReportStringId.RibbonXRDesign_JustifyCenter_STipContent:
                    return "文本居中.";

                case ReportStringId.RibbonXRDesign_JustifyRight_STipContent:
                    return "右对齐文本.";

                case ReportStringId.RibbonXRDesign_JustifyJustify_STipContent:
                    return "根据单词自动排列左右对齐.";

                case ReportStringId.RibbonXRDesign_Close_STipContent:
                    return "关闭当前报表。";

                case ReportStringId.RibbonXRDesign_Exit_STipContent:
                    return "关闭报表设计。";

                case ReportStringId.RibbonXRDesign_NewReport_STipContent:
                    return "创建一个新的空白报表,你可以插入字段和控件并设计报表.";

                case ReportStringId.RibbonXRDesign_NewReportWizard_STipContent:
                    return "启动报表向导帮助你创建简单的、自定义报表.";

                case ReportStringId.RibbonXRDesign_OpenFile_STipContent:
                    return "打开报表.";

                case ReportStringId.RibbonXRDesign_SaveFile_STipContent:
                    return "保存报表.";

                case ReportStringId.RibbonXRDesign_SaveFileAs_STipContent:
                    return "将报表保存为另外一个文件名.";

                case ReportStringId.RibbonXRDesign_SaveAll_STipContent:
                    return "保存报表的所有修改.";

                case ReportStringId.RibbonXRDesign_Cut_STipContent:
                    return "剪切报表中所选控件并放到剪贴板.";

                case ReportStringId.RibbonXRDesign_Copy_STipContent:
                    return "复制报表中所选控件到剪贴板.";

                case ReportStringId.RibbonXRDesign_Paste_STipContent:
                    return "粘贴剪贴板内容.";

                case ReportStringId.RibbonXRDesign_Undo_STipContent:
                    return "撤消最后一步操作.";

                case ReportStringId.RibbonXRDesign_Redo_STipContent:
                    return "重做最后一步操作.";

                case ReportStringId.RibbonXRDesign_Zoom_STipContent:
                    return "改变文档设计器的所放率.";

                case ReportStringId.RibbonXRDesign_ZoomIn_STipContent:
                    return "放大查看报表局部视图.";

                case ReportStringId.RibbonXRDesign_ZoomOut_STipContent:
                    return "缩小尺寸在查看更多的报表.";

                case ReportStringId.RibbonXRDesign_FontName_STipContent:
                    return "改变字体样式.";

                case ReportStringId.RibbonXRDesign_FontSize_STipContent:
                    return "改变字体大小.";

                case ReportStringId.RibbonXRDesign_Windows_STipContent:
                    return "显示/隐藏工具栏,报表浏览器,字段列表和属性窗口.";

                case ReportStringId.RibbonXRDesign_Scripts_STipContent:
                    return "显示或隐藏脚本编辑器。";

                case ReportStringId.RibbonXRDesign_HtmlHome_STipContent:
                    return "显示主页.";

                case ReportStringId.RibbonXRDesign_HtmlBackward_STipContent:
                    return "返回上页.";

                case ReportStringId.RibbonXRDesign_HtmlForward_STipContent:
                    return "移到下一页.";

                case ReportStringId.RibbonXRDesign_HtmlRefresh_STipContent:
                    return "刷新当前页.";

                case ReportStringId.RibbonXRDesign_HtmlFind_STipContent:
                    return "在该页上查找文本.";

                case ReportStringId.XRSubreport_NameInfo:
                    return "名称: {0}\r\n";

                case ReportStringId.XRSubreport_NullReportSourceInfo:
                    return "空";

                case ReportStringId.XRSubreport_ReportSourceInfo:
                    return "报表资源: {0}\r\n";

                case ReportStringId.XRSubreport_ReportSourceUrlInfo:
                    return "报表源URL: {0}\r\n";

                case ReportStringId.PivotGridForm_GroupMain_Caption:
                    return "主要的";

                case ReportStringId.PivotGridForm_GroupMain_Description:
                    return "重要设置(字段, 布局).";

                case ReportStringId.PivotGridForm_ItemFields_Caption:
                    return "字段";

                case ReportStringId.PivotGridForm_ItemFields_Description:
                    return "处理字段.";

                case ReportStringId.PivotGridForm_ItemLayout_Caption:
                    return "布局";

                case ReportStringId.PivotGridForm_ItemLayout_Description:
                    return "用户自定义当前XRPivotGrid的布局并预览数据.";

                case ReportStringId.PivotGridForm_GroupPrinting_Caption:
                    return "打印";

                case ReportStringId.PivotGridForm_GroupPrinting_Description:
                    return "当前XRPivotGrid打印选项处理.";

                case ReportStringId.PivotGridForm_ItemAppearances_Caption:
                    return "外观";

                case ReportStringId.PivotGridForm_ItemAppearances_Description:
                    return "调整当前XRPivotGrid的打印外观.";

                case ReportStringId.PivotGridForm_ItemSettings_Caption:
                    return "打印设置";

                case ReportStringId.PivotGridForm_ItemSettings_Description:
                    return "调整当前XRPivotGrid的打印设置.";

                case ReportStringId.PivotGridFrame_Fields_ColumnsText:
                    return "XRPivotGrid字段";

                case ReportStringId.PivotGridFrame_Fields_DescriptionText1:
                    return "你可以添加和删除XRPivotGrid字段并修改其设置.";

                case ReportStringId.PivotGridFrame_Fields_DescriptionText2:
                    return "选择并拖放字段到PivotGrid字段面板来创建PivotGrid字段.";

                case ReportStringId.PivotGridFrame_Layouts_DescriptionText:
                    return "修改XRPivotGrid的布局(排序设置,字段排列)并单击应用按钮来应用对当前XRPivotGrid的修改.你也可以保存布局到XML文件中(这使得其可以在设计时和运行时下加载并应用到其他视图).";

                case ReportStringId.PivotGridFrame_Layouts_SelectorCaption1:
                    return "隐藏字段选择器";

                case ReportStringId.PivotGridFrame_Layouts_SelectorCaption2:
                    return "显示字段选择器";

                case ReportStringId.PivotGridFrame_Appearances_DescriptionText:
                    return "选择一个或多个外观对象来自定义可见元素对应打印外观.";

                case ReportStringId.PropGrid_TTip_Alphabetical:
                    return "依字母顺序";

                case ReportStringId.PropGrid_TTip_Categorized:
                    return "分类";

                case ReportStringId.DesignerStatus_Location:
                    return "位置";

                case ReportStringId.DesignerStatus_Size:
                    return "大小";

                case ReportStringId.DesignerStatus_Height:
                    return "高";

                case ReportStringId.Wizard_PageChooseFields_Msg:
                    return "你必须选择报表字段，然后再继续";

                case ReportStringId.GroupSort_AddGroup:
                    return "增加组";

                case ReportStringId.GroupSort_AddSort:
                    return "增加排序";

                case ReportStringId.GroupSort_MoveUp:
                    return "往上移";

                case ReportStringId.GroupSort_MoveDown:
                    return "往下移";

                case ReportStringId.GroupSort_Delete:
                    return "删除";

                case ReportStringId.Parameter_Type_String:
                    return "String";

                case ReportStringId.Parameter_Type_DateTime:
                    return "DateTime";

                case ReportStringId.Parameter_Type_Int16:
                    return "Int16";

                case ReportStringId.Parameter_Type_Int32:
                    return "Int32";

                case ReportStringId.Parameter_Type_Float:
                    return "Float";

                case ReportStringId.Parameter_Type_Double:
                    return "Double";

                case ReportStringId.Parameter_Type_Decimal:
                    return "Decimal";

                case ReportStringId.Parameter_Type_Boolean:
                    return "Boolean";
            }
            return base.GetLocalizedString(id);
        }

        public override string Language
        {
            get
            {
                return "简体中文";
            }
        }
    }
}

