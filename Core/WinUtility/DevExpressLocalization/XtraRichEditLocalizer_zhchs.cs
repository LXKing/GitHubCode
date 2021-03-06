﻿namespace DevLocalization
{
    using DevExpress.XtraRichEdit.Localization;
    using System;

    public class XtraRichEditLocalizer_zhchs : XtraRichEditResLocalizer
    {
        public override string GetLocalizedString(XtraRichEditStringId id)
        {
            switch (id)
            {
                case XtraRichEditStringId.Msg_IsNotValid:
                    return "'{0}' 不是 '{1}'的有效值";

                case XtraRichEditStringId.Msg_UnsupportedDocVersion:
                    return "只支持Microsoft Word 97或更高版本的Word";

                case XtraRichEditStringId.Msg_EncryptedDocFile:
                    return "目前不支持加密文件";

                case XtraRichEditStringId.Msg_MagicNumberNotFound:
                    return "你试图打开该文件不同格式的指定文件扩展名。";

                case XtraRichEditStringId.Msg_InternalError:
                    return "内部错误！";

                case XtraRichEditStringId.Msg_UseDeletedStyleError:
                    return "错误:使用已删除的式样";

                case XtraRichEditStringId.Msg_UseDeletedParagraphError:
                    return "错误：使用已删除的段落";

                case XtraRichEditStringId.Msg_UseDeletedFieldError:
                    return "错误：域段落";

                case XtraRichEditStringId.Msg_UseDeletedSectionError:
                    return "错误：使用已删除的段";

                case XtraRichEditStringId.Msg_UseDeletedBookmarkError:
                    return "错误:使用已删除的书签";

                case XtraRichEditStringId.Msg_UseDeletedHyperlinkError:
                    return "错误:使用已删除的超链接";

                case XtraRichEditStringId.Msg_UseDeletedTableError:
                    return "错误：使用已删除的表";

                case XtraRichEditStringId.Msg_UseDeletedTableRowError:
                    return "错误：使用删除表中的行";

                case XtraRichEditStringId.Msg_UseDeletedTableCellError:
                    return "错误：使用删除表中的单元格";

                case XtraRichEditStringId.Msg_UseInvalidParagraphProperties:
                    return "错误：段落属性不再有效";

                case XtraRichEditStringId.Msg_UseInvalidCharacterProperties:
                    return "错误：字符属性不再有效";

                case XtraRichEditStringId.Msg_DocumentPositionDoesntMatchDocument:
                    return "错误:指定的文档位置或范围是另一个文档的";

                case XtraRichEditStringId.Msg_UseInvalidDocument:
                    return "错误:此文档不再有效";

                case XtraRichEditStringId.Msg_UnsupportedFormatException:
                    return "文件格式不支持";

                case XtraRichEditStringId.Msg_PrintingUnavailable:
                    return "打印是不可用的。检查是否下列组件安装:\r\n{0}\r\n{1}";

                case XtraRichEditStringId.Msg_CreateHyperlinkError:
                    return "不能创建超链接。超链接在指定的范围已经存在。";

                case XtraRichEditStringId.Msg_SelectBookmarkError:
                    return "不能选择处于非活动状态的子文档中的书签。";

                case XtraRichEditStringId.Msg_IncorrectBookmarkName:
                    return "书签名应以字母开头和字母数字和下划线";

                case XtraRichEditStringId.Msg_InvalidNavigateUri:
                    return "不能访问指定的地址";

                case XtraRichEditStringId.Msg_IncorrectPattern:
                    return "不正确的模式";

                case XtraRichEditStringId.Msg_DocumentProtectionInvalidPassword:
                    return "密码错误！";

                case XtraRichEditStringId.Msg_DocumentProtectionInvalidPasswordConfirmation:
                    return "密码不匹配。";

                case XtraRichEditStringId.MenuCmd_SimpleView:
                    return "单一视图";

                case XtraRichEditStringId.MenuCmd_DraftView:
                    return "草稿视图";

                case XtraRichEditStringId.MenuCmd_ReadingView:
                    return "阅读视图";

                case XtraRichEditStringId.MenuCmd_PrintView:
                    return "打印视图";

                case XtraRichEditStringId.MenuCmd_SimpleViewDescription:
                    return "单一视图";

                case XtraRichEditStringId.MenuCmd_DraftViewDescription:
                    return "草稿视图";

                case XtraRichEditStringId.MenuCmd_ReadingViewDescription:
                    return "阅读视图";

                case XtraRichEditStringId.MenuCmd_PrintViewDescription:
                    return "打印视图";

                case XtraRichEditStringId.MenuCmd_Zoom:
                    return "缩放";

                case XtraRichEditStringId.MenuCmd_ZoomDescription:
                    return "缩放";

                case XtraRichEditStringId.MenuCmd_ZoomIn:
                    return "放大";

                case XtraRichEditStringId.MenuCmd_ZoomInDescription:
                    return "放大";

                case XtraRichEditStringId.MenuCmd_ZoomOut:
                    return "缩小";

                case XtraRichEditStringId.MenuCmd_ZoomOutDescription:
                    return "缩小";

                case XtraRichEditStringId.MenuCmd_Undo:
                    return "复原";

                case XtraRichEditStringId.MenuCmd_UndoDescription:
                    return "复原";

                case XtraRichEditStringId.MenuCmd_Redo:
                    return "取消复原";

                case XtraRichEditStringId.MenuCmd_RedoDescription:
                    return "取消复原";

                case XtraRichEditStringId.MenuCmd_ClearUndo:
                    return "清除复原";

                case XtraRichEditStringId.MenuCmd_ClearUndoDescription:
                    return "清除复原缓冲区";

                case XtraRichEditStringId.MenuCmd_ScrollDown:
                    return "向下卷动";

                case XtraRichEditStringId.MenuCmd_ScrollDownDescription:
                    return "向下卷动";

                case XtraRichEditStringId.MenuCmd_EnsureCaretVisibleVertically:
                    return "确保插入符号垂直显示";

                case XtraRichEditStringId.MenuCmd_EnsureCaretVisibleVerticallyDescription:
                    return "确保插入符号垂直显示";

                case XtraRichEditStringId.MenuCmd_EnsureCaretVisibleHorizontally:
                    return "确保插入符号水平显示";

                case XtraRichEditStringId.MenuCmd_EnsureCaretVisibleHorizontallyDescription:
                    return "确保插入符号水平显示";

                case XtraRichEditStringId.MenuCmd_MoveForward:
                    return "移动到前面";

                case XtraRichEditStringId.MenuCmd_MoveForwardDescription:
                    return "移动到前面";

                case XtraRichEditStringId.MenuCmd_MoveBackward:
                    return "移动到后面";

                case XtraRichEditStringId.MenuCmd_MoveBackwardDescription:
                    return "移动到后面";

                case XtraRichEditStringId.MenuCmd_MoveToStartOfLine:
                    return "移动到开始行";

                case XtraRichEditStringId.MenuCmd_MoveToStartOfLineDescription:
                    return "移动到开始行";

                case XtraRichEditStringId.MenuCmd_MoveToEndOfLine:
                    return "移动到结束行";

                case XtraRichEditStringId.MenuCmd_MoveToEndOfLineDescription:
                    return "移动到结束行";

                case XtraRichEditStringId.MenuCmd_MoveLineUp:
                    return "移动到上一行";

                case XtraRichEditStringId.MenuCmd_MoveLineUpDescription:
                    return "移动到上一行";

                case XtraRichEditStringId.MenuCmd_MoveLineDown:
                    return "移动到下一行";

                case XtraRichEditStringId.MenuCmd_MoveLineDownDescription:
                    return "移动到下一行";

                case XtraRichEditStringId.MenuCmd_MovePreviousParagraph:
                    return "移动到上一段";

                case XtraRichEditStringId.MenuCmd_MovePreviousParagraphDescription:
                    return "移动到上一段";

                case XtraRichEditStringId.MenuCmd_MoveNextParagraph:
                    return "移动到下一段";

                case XtraRichEditStringId.MenuCmd_MoveNextParagraphDescription:
                    return "移动到下一段";

                case XtraRichEditStringId.MenuCmd_MovePreviousWord:
                    return "移动到上一字";

                case XtraRichEditStringId.MenuCmd_MovePreviousWordDescription:
                    return "移动到上一字";

                case XtraRichEditStringId.MenuCmd_MoveNextWord:
                    return "移动到下一字";

                case XtraRichEditStringId.MenuCmd_MoveNextWordDescription:
                    return "移动到下一字";

                case XtraRichEditStringId.MenuCmd_MovePreviousPage:
                    return "移动到上一页";

                case XtraRichEditStringId.MenuCmd_MovePreviousPageDescription:
                    return "移动到上一页";

                case XtraRichEditStringId.MenuCmd_MoveNextPage:
                    return "移动到下一页";

                case XtraRichEditStringId.MenuCmd_MoveNextPageDescription:
                    return "移动到下一页";

                case XtraRichEditStringId.MenuCmd_MoveToBeginOfDocument:
                    return "移动到文檔开始";

                case XtraRichEditStringId.MenuCmd_MoveToBeginOfDocumentDescription:
                    return "移动到文檔开始";

                case XtraRichEditStringId.MenuCmd_MoveToEndOfDocument:
                    return "移动到文檔结束";

                case XtraRichEditStringId.MenuCmd_MoveToEndOfDocumentDescription:
                    return "移动到文檔结束";

                case XtraRichEditStringId.MenuCmd_MoveScreenUp:
                    return "移动到上一屏";

                case XtraRichEditStringId.MenuCmd_MoveScreenUpDescription:
                    return "移动到上一屏";

                case XtraRichEditStringId.MenuCmd_MoveScreenDown:
                    return "移动到下一屏";

                case XtraRichEditStringId.MenuCmd_MoveScreenDownDescription:
                    return "移动到下一屏";

                case XtraRichEditStringId.MenuCmd_SelectAll:
                    return "全选";

                case XtraRichEditStringId.MenuCmd_SelectAllDescription:
                    return "全选";

                case XtraRichEditStringId.MenuCmd_DeselectAll:
                    return "取消全选";

                case XtraRichEditStringId.MenuCmd_DeselectAllDescription:
                    return "重置选择";

                case XtraRichEditStringId.MenuCmd_InsertTableElement:
                    return "插入";

                case XtraRichEditStringId.MenuCmd_InsertParagraph:
                    return "插入段落";

                case XtraRichEditStringId.MenuCmd_InsertParagraphDescription:
                    return "插入段落";

                case XtraRichEditStringId.MenuCmd_InsertLineBreak:
                    return "插入行分隔符";

                case XtraRichEditStringId.MenuCmd_InsertLineBreakDescription:
                    return "插入行分隔符";

                case XtraRichEditStringId.MenuCmd_InsertText:
                    return "插入文本";

                case XtraRichEditStringId.MenuCmd_InsertTextDescription:
                    return "插入文本";

                case XtraRichEditStringId.MenuCmd_InsertBulletList:
                    return "项目符号";

                case XtraRichEditStringId.MenuCmd_InsertBulletListDescription:
                    return "开始项目符号列表";

                case XtraRichEditStringId.MenuCmd_InsertMultilevelList:
                    return "插入多级列表";

                case XtraRichEditStringId.MenuCmd_InsertMultilevelListDescription:
                    return "开始多级列表";

                case XtraRichEditStringId.MenuCmd_InsertSimpleList:
                    return "编号";

                case XtraRichEditStringId.MenuCmd_InsertSimpleListDescription:
                    return "开始编号列表";

                case XtraRichEditStringId.MenuCmd_InsertField:
                    return "插入域";

                case XtraRichEditStringId.MenuCmd_InsertFieldDescription:
                    return "插入域";

                case XtraRichEditStringId.MenuCmd_InsertPageNumberField:
                    return "页码";

                case XtraRichEditStringId.MenuCmd_InsertPageNumberFieldDescription:
                    return "插入页码";

                case XtraRichEditStringId.MenuCmd_InsertPageCountField:
                    return "总页数";

                case XtraRichEditStringId.MenuCmd_InsertPageCountFieldDescription:
                    return "插入总页数";

                case XtraRichEditStringId.MenuCmd_InsertMergeField:
                    return "插入合并域";

                case XtraRichEditStringId.MenuCmd_InsertMergeFieldDescription:
                    return "插入合并域";

                case XtraRichEditStringId.MenuCmd_InsertTabToParagraph:
                    return "插入制表符到段落";

                case XtraRichEditStringId.MenuCmd_InsertTabToParagraphDescription:
                    return "插入制表符到段落";

                case XtraRichEditStringId.MenuCmd_InsertTable:
                    return "表格";

                case XtraRichEditStringId.MenuCmd_InsertTableDescription:
                    return "插入表格";

                case XtraRichEditStringId.MenuCmd_InsertTableRowAbove:
                    return "在上方插入行";

                case XtraRichEditStringId.MenuCmd_InsertTableRowAboveDescription:
                    return "在所选行的上方直接增加一行。";

                case XtraRichEditStringId.MenuCmd_InsertTableRowBelow:
                    return "在下方插入行";

                case XtraRichEditStringId.MenuCmd_InsertTableRowBelowDescription:
                    return "在所选行的下方直接增加一行。";

                case XtraRichEditStringId.MenuCmd_InsertTableCells:
                    return "插入列";

                case XtraRichEditStringId.MenuCmd_InsertTableCellsDescription:
                    return "插入列";

                case XtraRichEditStringId.MenuCmd_DeleteTableCells:
                    return "删除单元格";

                case XtraRichEditStringId.MenuCmd_DeleteTableCellsMenuItem:
                    return "删除单元格";

                case XtraRichEditStringId.MenuCmd_DeleteTableCellsDescription:
                    return "删除行、列或单元格";

                case XtraRichEditStringId.MenuCmd_DeleteTableRows:
                    return "删除行";

                case XtraRichEditStringId.MenuCmd_DeleteTableRowsDescription:
                    return "删除行";

                case XtraRichEditStringId.MenuCmd_DeleteTable:
                    return "删除表格";

                case XtraRichEditStringId.MenuCmd_DeleteTableDescription:
                    return "删除整个表格";

                case XtraRichEditStringId.MenuCmd_DeleteTableElements:
                    return "删除";

                case XtraRichEditStringId.MenuCmd_DeleteTableElementsDescription:
                    return "删除行、列、单元格或整个表格";

                case XtraRichEditStringId.MenuCmd_DeleteTableColumns:
                    return "删除列";

                case XtraRichEditStringId.MenuCmd_DeleteTableColumnsDescription:
                    return "删除列";

                case XtraRichEditStringId.MenuCmd_ShowInsertMergeFieldForm:
                    return "插入合并域";

                case XtraRichEditStringId.MenuCmd_ShowInsertMergeFieldFormDescription:
                    return "添加一个域从列表中的收件人或数据表的文件";

                case XtraRichEditStringId.MenuCmd_DeleteNumerationFromParagraph:
                    return "从段落删除编号";

                case XtraRichEditStringId.MenuCmd_DeleteNumerationFromParagraphDescription:
                    return "从段落删除编号";

                case XtraRichEditStringId.MenuCmd_IncrementNumerationFromParagraph:
                    return "从段落增加编号";

                case XtraRichEditStringId.MenuCmd_IncrementNumerationFromParagraphDescription:
                    return "从段落增加编号";

                case XtraRichEditStringId.MenuCmd_DecrementNumerationFromParagraph:
                    return "从段落缩减编号";

                case XtraRichEditStringId.MenuCmd_DecrementNumerationFromParagraphDescription:
                    return "从段落缩减编号";

                case XtraRichEditStringId.MenuCmd_TabKey:
                    return "Tab键";

                case XtraRichEditStringId.MenuCmd_TabKeyDescription:
                    return "Tab键";

                case XtraRichEditStringId.MenuCmd_ShiftTabKey:
                    return "ShiftTab键";

                case XtraRichEditStringId.MenuCmd_ShiftTabKeyDescription:
                    return "ShiftTab键";

                case XtraRichEditStringId.MenuCmd_BackSpaceKey:
                    return "退格键";

                case XtraRichEditStringId.MenuCmd_BackSpaceKeyDescription:
                    return "退格键";

                case XtraRichEditStringId.MenuCmd_InsertTab:
                    return "插入Tab";

                case XtraRichEditStringId.MenuCmd_InsertTabDescription:
                    return "插入Tab";

                case XtraRichEditStringId.MenuCmd_InsertPageBreak:
                    return "插入页面分隔符";

                case XtraRichEditStringId.MenuCmd_InsertPageBreakDescription:
                    return "插入页面分隔符";

                case XtraRichEditStringId.MenuCmd_InsertNonBreakingSpace:
                    return "插入非中断空格";

                case XtraRichEditStringId.MenuCmd_InsertNonBreakingSpaceDescription:
                    return "插入非中断空格";

                case XtraRichEditStringId.MenuCmd_InsertColumnBreak:
                    return "插入列分隔符";

                case XtraRichEditStringId.MenuCmd_InsertColumnBreakDescription:
                    return "插入列分隔符";

                case XtraRichEditStringId.MenuCmd_InsertEnDash:
                    return "插入半破折号";

                case XtraRichEditStringId.MenuCmd_InsertEnDashDescription:
                    return "插入半破折号";

                case XtraRichEditStringId.MenuCmd_InsertEmDash:
                    return "插入全破折号";

                case XtraRichEditStringId.MenuCmd_InsertEmDashDescription:
                    return "插入全破折号";

                case XtraRichEditStringId.MenuCmd_InsertCopyrightSymbol:
                    return "插入版权信息";

                case XtraRichEditStringId.MenuCmd_InsertCopyrightSymbolDescription:
                    return "插入版权信息";

                case XtraRichEditStringId.MenuCmd_InsertRegisteredTrademarkSymbol:
                    return "插入注册商标";

                case XtraRichEditStringId.MenuCmd_InsertRegisteredTrademarkSymbolDescription:
                    return "插入注册商标";

                case XtraRichEditStringId.MenuCmd_InsertTrademarkSymbol:
                    return "插入商标";

                case XtraRichEditStringId.MenuCmd_InsertTrademarkSymbolDescription:
                    return "插入商标";

                case XtraRichEditStringId.MenuCmd_InsertEllipsis:
                    return "插入省略号";

                case XtraRichEditStringId.MenuCmd_InsertEllipsisDescription:
                    return "插入省略号";

                case XtraRichEditStringId.MenuCmd_InsertOpeningSingleQuotationMark:
                    return "插入开始的单引号";

                case XtraRichEditStringId.MenuCmd_InsertOpeningSingleQuotationMarkDescription:
                    return "插入开始的单引号";

                case XtraRichEditStringId.MenuCmd_InsertClosingSingleQuotationMark:
                    return "插入结束的单引号";

                case XtraRichEditStringId.MenuCmd_InsertClosingSingleQuotationMarkDescription:
                    return "插入结束的单引号";

                case XtraRichEditStringId.MenuCmd_InsertOpeningDoubleQuotationMark:
                    return "插入开始的双引号";

                case XtraRichEditStringId.MenuCmd_InsertOpeningDoubleQuotationMarkDescription:
                    return "插入开始的双引号";

                case XtraRichEditStringId.MenuCmd_InsertClosingDoubleQuotationMark:
                    return "插入结束的双引号";

                case XtraRichEditStringId.MenuCmd_InsertClosingDoubleQuotationMarkDescription:
                    return "插入结束的双引号";

                case XtraRichEditStringId.MenuCmd_InsertSymbol:
                    return "插入符号";

                case XtraRichEditStringId.MenuCmd_InsertSymbolDescription:
                    return "不在键盘的符号，例如版权信息、商标、段落标记和双字节字符。";

                case XtraRichEditStringId.MenuCmd_InsertPicture:
                    return "插入图片";

                case XtraRichEditStringId.MenuCmd_InsertPictureDescription:
                    return "插入图片";

                case XtraRichEditStringId.MenuCmd_InsertBreak:
                    return "分隔符";

                case XtraRichEditStringId.MenuCmd_InsertBreakDescription:
                    return "在文档中添加分页符、分节符或分栏符。";

                case XtraRichEditStringId.MenuCmd_InsertSectionBreakNextPage:
                    return "分节符(下一页)";

                case XtraRichEditStringId.MenuCmd_InsertSectionBreakNextPageDescription:
                    return "插入分节符并在下一页上开始新节。";

                case XtraRichEditStringId.MenuCmd_InsertSectionBreakOddPage:
                    return "分节符(奇数页)";

                case XtraRichEditStringId.MenuCmd_InsertSectionBreakOddPageDescription:
                    return "插入分节符并在下一奇数页上开始新节。";

                case XtraRichEditStringId.MenuCmd_InsertSectionBreakEvenPage:
                    return "分节符(偶数页)";

                case XtraRichEditStringId.MenuCmd_InsertSectionBreakEvenPageDescription:
                    return "插入分节符并在下一偶数页上开始新节。";

                case XtraRichEditStringId.MenuCmd_InsertSectionBreakContinuous:
                    return "分节符(连续)";

                case XtraRichEditStringId.MenuCmd_InsertSectionBreakContinuousDescription:
                    return "插入分节符并在同一页上开始新节。";

                case XtraRichEditStringId.MenuCmd_ToggleFontBold:
                    return "粗体";

                case XtraRichEditStringId.MenuCmd_ToggleFontBoldDescription:
                    return "使选中的文本为粗体";

                case XtraRichEditStringId.MenuCmd_ToggleFontItalic:
                    return "斜体";

                case XtraRichEditStringId.MenuCmd_ToggleFontItalicDescription:
                    return "斜体选中的文本";

                case XtraRichEditStringId.MenuCmd_ToggleHiddenText:
                    return "隐藏";

                case XtraRichEditStringId.MenuCmd_ToggleHiddenTextDescription:
                    return "隐藏";

                case XtraRichEditStringId.MenuCmd_ToggleFontUnderline:
                    return "底线";

                case XtraRichEditStringId.MenuCmd_ToggleFontUnderlineDescription:
                    return "选中的文本下划线";

                case XtraRichEditStringId.MenuCmd_ToggleFontDoubleUnderline:
                    return "双下划线";

                case XtraRichEditStringId.MenuCmd_ToggleFontDoubleUnderlineDescription:
                    return "选中的文本双下划线";

                case XtraRichEditStringId.MenuCmd_ToggleFontStrikeout:
                    return "删除线";

                case XtraRichEditStringId.MenuCmd_ToggleFontStrikeoutDescription:
                    return "选中的文本中间一条线";

                case XtraRichEditStringId.MenuCmd_ToggleFontDoubleStrikeout:
                    return "双删除线";

                case XtraRichEditStringId.MenuCmd_ToggleFontDoubleStrikeoutDescription:
                    return "选中的文本中间二条线";

                case XtraRichEditStringId.MenuCmd_IncrementFontSize:
                    return "加大字体大小";

                case XtraRichEditStringId.MenuCmd_IncrementFontSizeDescription:
                    return "加大字体大小";

                case XtraRichEditStringId.MenuCmd_DecrementFontSize:
                    return "减少字体大小";

                case XtraRichEditStringId.MenuCmd_DecrementFontSizeDescription:
                    return "减少字体大小";

                case XtraRichEditStringId.MenuCmd_ChangeFontColor:
                    return "字体颜色";

                case XtraRichEditStringId.MenuCmd_ChangeFontColorDescription:
                    return "改变字体颜色";

                case XtraRichEditStringId.MenuCmd_HighlightText:
                    return "文本增强颜色";

                case XtraRichEditStringId.MenuCmd_HighlightTextDescription:
                    return "要让文字看起来就像是一个荧光笔";

                case XtraRichEditStringId.MenuCmd_ChangeFontName:
                    return "字体";

                case XtraRichEditStringId.MenuCmd_ChangeFontNameDescription:
                    return "改变字体外形";

                case XtraRichEditStringId.MenuCmd_ChangeFontSize:
                    return "字体大小";

                case XtraRichEditStringId.MenuCmd_ChangeFontSizeDescription:
                    return "改变字体大小";

                case XtraRichEditStringId.MenuCmd_ChangeStyle:
                    return "式样";

                case XtraRichEditStringId.MenuCmd_ChangeStyleDescription:
                    return "式样";

                case XtraRichEditStringId.MenuCmd_ChangeColumnCount:
                    return "列";

                case XtraRichEditStringId.MenuCmd_ChangeColumnCountDescription:
                    return "列";

                case XtraRichEditStringId.MenuCmd_IncreaseFontSize:
                    return "加大字体大小";

                case XtraRichEditStringId.MenuCmd_IncreaseFontSizeDescription:
                    return "加大字体大小";

                case XtraRichEditStringId.MenuCmd_DecreaseFontSize:
                    return "减少字体大小";

                case XtraRichEditStringId.MenuCmd_DecreaseFontSizeDescription:
                    return "减少字体大小";

                case XtraRichEditStringId.MenuCmd_FontSuperscript:
                    return "上标";

                case XtraRichEditStringId.MenuCmd_FontSuperscriptDescription:
                    return "在文本右上角创建小字";

                case XtraRichEditStringId.MenuCmd_FontSubscript:
                    return "下标";

                case XtraRichEditStringId.MenuCmd_FontSubscriptDescription:
                    return "基于文本右下角创建小字";

                case XtraRichEditStringId.MenuCmd_ParagraphAlignmentLeft:
                    return "左对齐";

                case XtraRichEditStringId.MenuCmd_ParagraphAlignmentLeftDescription:
                    return "调整文本到左边";

                case XtraRichEditStringId.MenuCmd_ParagraphAlignmentCenter:
                    return "居中对齐";

                case XtraRichEditStringId.MenuCmd_ParagraphAlignmentCenterDescription:
                    return "文本居中";

                case XtraRichEditStringId.MenuCmd_ParagraphAlignmentRight:
                    return "右对齐";

                case XtraRichEditStringId.MenuCmd_ParagraphAlignmentRightDescription:
                    return "调整文本到右边";

                case XtraRichEditStringId.MenuCmd_ParagraphAlignmentJustify:
                    return "两端对齐";

                case XtraRichEditStringId.MenuCmd_ParagraphAlignmentJustifyDescription:
                    return "两端对齐";

                case XtraRichEditStringId.MenuCmd_ChangeParagraphBackColor:
                    return "底纹";

                case XtraRichEditStringId.MenuCmd_ChangeParagraphBackColorDescription:
                    return "所选段落的背景色。";

                case XtraRichEditStringId.MenuCmd_SetSingleParagraphSpacing:
                    return "1.0";

                case XtraRichEditStringId.MenuCmd_SetSingleParagraphSpacingDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_SetSesquialteralParagraphSpacing:
                    return "1.5";

                case XtraRichEditStringId.MenuCmd_SetSesquialteralParagraphSpacingDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_SetDoubleParagraphSpacing:
                    return "2.0";

                case XtraRichEditStringId.MenuCmd_SetDoubleParagraphSpacingDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_AddSpacingBeforeParagraph:
                    return "在段落前增加空格";

                case XtraRichEditStringId.MenuCmd_AddSpacingBeforeParagraphDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_AddSpacingAfterParagraph:
                    return "在段落后增加空格";

                case XtraRichEditStringId.MenuCmd_AddSpacingAfterParagraphDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_RemoveSpacingBeforeParagraph:
                    return "删除段落前空格";

                case XtraRichEditStringId.MenuCmd_RemoveSpacingBeforeParagraphDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_RemoveSpacingAfterParagraph:
                    return "删除段落后空格";

                case XtraRichEditStringId.MenuCmd_RemoveSpacingAfterParagraphDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_ChangeTableCellShading:
                    return "底纹";

                case XtraRichEditStringId.MenuCmd_ChangeTableCellShadingDescription:
                    return "所选单元格的背景色。";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsAllBorders:
                    return "全部边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsAllBordersDescription:
                    return "自定义所选单元格的边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsOutsideBorder:
                    return "外边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsOutsideBorderDescription:
                    return "自定义所选单元格的边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsInsideBorder:
                    return "内边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsInsideBorderDescription:
                    return "自定义所选单元格的边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsTopBorder:
                    return "上边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsTopBorderDescription:
                    return "自定义所选单元格的边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsBottomBorder:
                    return "下边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsBottomBorderDescription:
                    return "自定义所选单元格的边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsLeftBorder:
                    return "左边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsLeftBorderDescription:
                    return "自定义所选单元格的边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsRightBorder:
                    return "右边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsRightBorderDescription:
                    return "自定义所选单元格的边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsInsideHorizontalBorder:
                    return "插入横向边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsInsideHorizontalBorderDescription:
                    return "自定义所选单元格的边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsInsideVerticalBorder:
                    return "插入垂直边框";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsInsideVerticalBorderDescription:
                    return "自定义所选单元格的边框";

                case XtraRichEditStringId.MenuCmd_ResetTableCellsBorders:
                    return "无";

                case XtraRichEditStringId.MenuCmd_ResetTableCellsBordersDescription:
                    return "自定义所选单元格的边框";

                case XtraRichEditStringId.MenuCmd_ChangeCurrentBorderRepositoryItemLineStyle:
                    return "线型";

                case XtraRichEditStringId.MenuCmd_ChangeCurrentBorderRepositoryItemLineStyleDescription:
                    return "边框线的式样";

                case XtraRichEditStringId.MenuCmd_ChangeCurrentBorderRepositoryItemLineThickness:
                    return "宽度";

                case XtraRichEditStringId.MenuCmd_ChangeCurrentBorderRepositoryItemLineThicknessDescription:
                    return "边框线的宽度";

                case XtraRichEditStringId.MenuCmd_ChangeCurrentBorderRepositoryItemColor:
                    return "颜色";

                case XtraRichEditStringId.MenuCmd_ChangeCurrentBorderRepositoryItemColorDescription:
                    return "边框线的颜色";

                case XtraRichEditStringId.MenuCmd_ChangeTableBorders:
                    return "边框";

                case XtraRichEditStringId.MenuCmd_ChangeTableBordersDescription:
                    return "自定义所选单元格的边框";

                case XtraRichEditStringId.MenuCmd_ChangeTableCellsContentAlignment:
                    return "单元格对齐";

                case XtraRichEditStringId.MenuCmd_ChangeTableCellsContentAlignmentDescription:
                    return "单元格对齐";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsTopLeftAlignment:
                    return "靠上两端对齐";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsTopLeftAlignmentDescription:
                    return "文字在单元格的左上角";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsTopCenterAlignment:
                    return "靠上居中";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsTopCenterAlignmentDescription:
                    return "文字在单元格的上方中间";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsTopRightAlignment:
                    return "靠上右对齐";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsTopRightAlignmentDescription:
                    return "文字在单元格的右上角";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsMiddleLeftAlignment:
                    return "中部两端对齐";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsMiddleLeftAlignmentDescription:
                    return "文字在单元格的中间左边";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsMiddleCenterAlignment:
                    return "中部居中";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsMiddleCenterAlignmentDescription:
                    return "文字在单元格的正中间";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsMiddleRightAlignment:
                    return "中部右对齐";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsMiddleRightAlignmentDescription:
                    return "文字在单元格的中间右边";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsBottomLeftAlignment:
                    return "靠下两端对齐";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsBottomLeftAlignmentDescription:
                    return "文字在单元格的左下角";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsBottomCenterAlignment:
                    return "靠下居中";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsBottomCenterAlignmentDescription:
                    return "文字在单元格的下方中间";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsBottomRightAlignment:
                    return "靠下右对齐";

                case XtraRichEditStringId.MenuCmd_ToggleTableCellsBottomRightAlignmentDescription:
                    return "文字在单元格右下角";

                case XtraRichEditStringId.MenuCmd_ToggleTableAutoFitPlaceholder:
                    return "自动调整";

                case XtraRichEditStringId.MenuCmd_ToggleTableAutoFitPlaceholderDescription:
                    return "根据列中文字的大小自动调整列宽。\r\n\r\n可以根据窗口大小设置表格宽度,或者将其恢复为使用固定列宽。";

                case XtraRichEditStringId.MenuCmd_ToggleTableAutoFitContents:
                    return "根据内容自动调整表格";

                case XtraRichEditStringId.MenuCmd_ToggleTableAutoFitContentsDescription:
                    return "根据内容自动调整表格.";

                case XtraRichEditStringId.MenuCmd_ToggleTableAutoFitWindow:
                    return "根据窗口自动调整表格";

                case XtraRichEditStringId.MenuCmd_ToggleTableAutoFitWindowDescription:
                    return "根据窗口自动调整表格.";

                case XtraRichEditStringId.MenuCmd_ToggleTableFixedColumnWidth:
                    return "固定列宽";

                case XtraRichEditStringId.MenuCmd_ToggleTableFixedColumnWidthDescription:
                    return "设置表格的固定宽度.";

                case XtraRichEditStringId.MenuCmd_Delete:
                    return "删除";

                case XtraRichEditStringId.MenuCmd_DeleteDescription:
                    return "删除";

                case XtraRichEditStringId.MenuCmd_DeleteCore:
                    return "删除核心";

                case XtraRichEditStringId.MenuCmd_DeleteCoreDescription:
                    return "删除核心";

                case XtraRichEditStringId.MenuCmd_DeleteBack:
                    return "删除退格";

                case XtraRichEditStringId.MenuCmd_DeleteBackDescription:
                    return "删除退格";

                case XtraRichEditStringId.MenuCmd_DeleteBackCore:
                    return "刪除退格核心";

                case XtraRichEditStringId.MenuCmd_DeleteBackCoreDescription:
                    return "刪除退格核心";

                case XtraRichEditStringId.MenuCmd_DeleteWord:
                    return "删除文字";

                case XtraRichEditStringId.MenuCmd_DeleteWordDescription:
                    return "删除文字";

                case XtraRichEditStringId.MenuCmd_DeleteWordCore:
                    return "删除文字核心";

                case XtraRichEditStringId.MenuCmd_DeleteWordCoreDescription:
                    return "删除文字核心";

                case XtraRichEditStringId.MenuCmd_DeleteWordBack:
                    return "删除文字退格";

                case XtraRichEditStringId.MenuCmd_DeleteWordBackDescription:
                    return "删除文字退格";

                case XtraRichEditStringId.MenuCmd_DeleteWordBackCore:
                    return "删除文字核心退格";

                case XtraRichEditStringId.MenuCmd_DeleteWordBackCoreDescription:
                    return "删除文字核心退格";

                case XtraRichEditStringId.MenuCmd_CopySelection:
                    return "复制";

                case XtraRichEditStringId.MenuCmd_CopySelectionDescription:
                    return "把所选择的放在剪贴板";

                case XtraRichEditStringId.MenuCmd_Paste:
                    return "粘贴";

                case XtraRichEditStringId.MenuCmd_PasteDescription:
                    return "粘贴剪贴板的内容";

                case XtraRichEditStringId.MenuCmd_PastePlainText:
                    return "无格式文本";

                case XtraRichEditStringId.MenuCmd_PastePlainTextDescription:
                    return "以\"不带任何格式的文字\"的形式插入\"剪贴板\"的内容.";

                case XtraRichEditStringId.MenuCmd_PasteRtfText:
                    return "带格式文本(RTF)";

                case XtraRichEditStringId.MenuCmd_PasteRtfTextDescription:
                    return "以\"带有字体和表格格式的文字\"的形式插入\"剪贴板\"的内容.";

                case XtraRichEditStringId.MenuCmd_PasteSilverlightXamlText:
                    return "带格式文本(XAML)";

                case XtraRichEditStringId.MenuCmd_PasteSilverlightXamlTextDescription:
                    return "以\"带有字体格式的文字\"的形式插入\"剪贴板\"的内容.";

                case XtraRichEditStringId.MenuCmd_PasteHtmlText:
                    return "HTML格式";

                case XtraRichEditStringId.MenuCmd_PasteHtmlTextDescription:
                    return "以\"HTML格式\"的形式插入\"剪贴板\"的内容.";

                case XtraRichEditStringId.MenuCmd_PasteImage:
                    return "图片";

                case XtraRichEditStringId.MenuCmd_PasteImageDescription:
                    return "以\"图片文件\"的形式插入\"剪贴板\"的内容.";

                case XtraRichEditStringId.MenuCmd_PasteMetafileImage:
                    return "图元文件";

                case XtraRichEditStringId.MenuCmd_PasteMetafileImageDescription:
                    return "以\"增强型图元文件\"的形式插入\"剪贴板\"的内容.";

                case XtraRichEditStringId.MenuCmd_PasteFiles:
                    return "文件";

                case XtraRichEditStringId.MenuCmd_PasteFilesDescription:
                    return "以\"嵌入的文件\"的形式插入\"剪贴板\"的内容.";

                case XtraRichEditStringId.MenuCmd_ShowPasteSpecialForm:
                    return "选择性粘贴";

                case XtraRichEditStringId.MenuCmd_ShowPasteSpecialFormDescription:
                    return "选择性粘贴";

                case XtraRichEditStringId.MenuCmd_CutSelection:
                    return "剪切";

                case XtraRichEditStringId.MenuCmd_CutSelectionDescription:
                    return "把所选择的剪切放在剪贴板";

                case XtraRichEditStringId.MenuCmd_ToggleWhitespace:
                    return "显示/隐藏 \x00b6";

                case XtraRichEditStringId.MenuCmd_ToggleWhitespaceDescription:
                    return "显示段落标记和隐藏格式符号。";

                case XtraRichEditStringId.MenuCmd_ToggleShowTableGridLines:
                    return "网格线";

                case XtraRichEditStringId.MenuCmd_ToggleShowTableGridLinesDescription:
                    return "显示或隐藏表格的网格线。";

                case XtraRichEditStringId.MenuCmd_ToggleShowHorizontalRuler:
                    return "水平标尺";

                case XtraRichEditStringId.MenuCmd_ToggleShowHorizontalRulerDescription:
                    return "水平标尺用来衡量和排队的对象";

                case XtraRichEditStringId.MenuCmd_ToggleShowVerticalRuler:
                    return "垂直标尺";

                case XtraRichEditStringId.MenuCmd_ToggleShowVerticalRulerDescription:
                    return "垂直标尺用来衡量和排队的对象";

                case XtraRichEditStringId.MenuCmd_FindAndSelectForward:
                    return "向上查找";

                case XtraRichEditStringId.MenuCmd_FindAndSelectForwardDescription:
                    return "向上查找";

                case XtraRichEditStringId.MenuCmd_FindAndSelectBackward:
                    return "向下查找";

                case XtraRichEditStringId.MenuCmd_FindAndSelectBackwardDescription:
                    return "向下查找";

                case XtraRichEditStringId.MenuCmd_ReplaceForward:
                    return "向上替换";

                case XtraRichEditStringId.MenuCmd_ReplaceForwardDescription:
                    return "向上替换";

                case XtraRichEditStringId.MenuCmd_ReplaceBackward:
                    return "向下替换";

                case XtraRichEditStringId.MenuCmd_ReplaceBackwardDescription:
                    return "向下替换";

                case XtraRichEditStringId.MenuCmd_FindNext:
                    return "查找下一个";

                case XtraRichEditStringId.MenuCmd_FindNextDescription:
                    return "查找下一个";

                case XtraRichEditStringId.MenuCmd_FindPrev:
                    return "查找上一个";

                case XtraRichEditStringId.MenuCmd_FindPrevDescription:
                    return "查找上一个";

                case XtraRichEditStringId.MenuCmd_NewEmptyDocument:
                    return "新建";

                case XtraRichEditStringId.MenuCmd_NewEmptyDocumentDescription:
                    return "新建";

                case XtraRichEditStringId.MenuCmd_LoadDocument:
                    return "打开";

                case XtraRichEditStringId.MenuCmd_LoadDocumentDescription:
                    return "打开";

                case XtraRichEditStringId.MenuCmd_SaveDocument:
                    return "保存";

                case XtraRichEditStringId.MenuCmd_SaveDocumentDescription:
                    return "保存";

                case XtraRichEditStringId.MenuCmd_SaveDocumentAs:
                    return "另存为";

                case XtraRichEditStringId.MenuCmd_SaveDocumentAsDescription:
                    return "另存为";

                case XtraRichEditStringId.MenuCmd_ShowFontForm:
                    return "字体";

                case XtraRichEditStringId.MenuCmd_ShowFontFormDescription:
                    return "显示字体对话框";

                case XtraRichEditStringId.MenuCmd_ShowParagraphForm:
                    return "段落";

                case XtraRichEditStringId.MenuCmd_ShowParagraphFormDescription:
                    return "显示段落对话框";

                case XtraRichEditStringId.MenuCmd_ShowEditStyleForm:
                    return "修改式样";

                case XtraRichEditStringId.MenuCmd_ShowEditStyleFormDescription:
                    return "显示编辑式样对话框";

                case XtraRichEditStringId.MenuCmd_ShowTabsForm:
                    return "制表符";

                case XtraRichEditStringId.MenuCmd_ShowTabsFormDescription:
                    return "制表符";

                case XtraRichEditStringId.MenuCmd_ShowLineSpacingForm:
                    return "行间距选项...";

                case XtraRichEditStringId.MenuCmd_ShowLineSpacingFormDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_EnterKey:
                    return "回车符";

                case XtraRichEditStringId.MenuCmd_EnterKeyDescription:
                    return "回车符";

                case XtraRichEditStringId.MenuCmd_ShowNumberingList:
                    return "项目符号和编号";

                case XtraRichEditStringId.MenuCmd_ShowNumberingListDescription:
                    return "显示项目符号和编号列表对话框";

                case XtraRichEditStringId.MenuCmd_ShowFloatingObjectLayoutOptionsForm:
                    return "布局";

                case XtraRichEditStringId.MenuCmd_ShowFloatingObjectLayoutOptionsFormDescription:
                    return "显示布局对话框";

                case XtraRichEditStringId.MenuCmd_ShowSymbol:
                    return "符号";

                case XtraRichEditStringId.MenuCmd_ShowSymbolDescription:
                    return "显示符号的对话框。";

                case XtraRichEditStringId.MenuCmd_ShowBookmarkForm:
                    return "书签";

                case XtraRichEditStringId.MenuCmd_ShowBookmarkFormDescription:
                    return "创建书签";

                case XtraRichEditStringId.MenuCmd_ShowHyperlinkForm:
                    return "超链接";

                case XtraRichEditStringId.MenuCmd_ShowHyperlinkFormDescription:
                    return "创建一个网页、图片、e-mail或应用程序的链接";

                case XtraRichEditStringId.MenuCmd_ShowRangeEditingPermissionsForm:
                    return "编辑权限范围";

                case XtraRichEditStringId.MenuCmd_ShowRangeEditingPermissionsFormDescription:
                    return "编辑权限范围";

                case XtraRichEditStringId.MenuCmd_Hyperlink:
                    return "超链接...";

                case XtraRichEditStringId.MenuCmd_HyperlinkDescription:
                    return "超链接...";

                case XtraRichEditStringId.MenuCmd_Bookmark:
                    return "书签...";

                case XtraRichEditStringId.MenuCmd_BookmarkDescription:
                    return "书签...";

                case XtraRichEditStringId.MenuCmd_CheckSpelling:
                    return "拼写检查";

                case XtraRichEditStringId.MenuCmd_CheckSpellingDescription:
                    return "拼写检查";

                case XtraRichEditStringId.MenuCmd_IgnoreMistakenWord:
                    return "忽略";

                case XtraRichEditStringId.MenuCmd_IgnoreMistakenWordDescription:
                    return "忽略";

                case XtraRichEditStringId.MenuCmd_IgnoreAllMistakenWords:
                    return "全部忽略";

                case XtraRichEditStringId.MenuCmd_IgnoreAllMistakenWordsDescription:
                    return "全部忽略";

                case XtraRichEditStringId.MenuCmd_AddWordToDictionary:
                    return "添加到词典";

                case XtraRichEditStringId.MenuCmd_AddWordToDictionaryDescription:
                    return "添加到词典";

                case XtraRichEditStringId.MenuCmd_DeleteRepeatedWord:
                    return "删除重复单词";

                case XtraRichEditStringId.MenuCmd_DeleteRepeatedWordDescription:
                    return "删除重复单词";

                case XtraRichEditStringId.MenuCmd_ToggleSpellCheckAsYouType:
                    return "输入时检查拼写";

                case XtraRichEditStringId.MenuCmd_ToggleSpellCheckAsYouTypeDescription:
                    return "输入时检查拼写";

                case XtraRichEditStringId.MenuCmd_ChangeColumnSize:
                    return "改变栏大小";

                case XtraRichEditStringId.MenuCmd_ChangeColumnSizeDescription:
                    return "改变栏大小";

                case XtraRichEditStringId.MenuCmd_ChangeIndent:
                    return "改变缩进";

                case XtraRichEditStringId.MenuCmd_ChangeIndentDescription:
                    return "改变缩进";

                case XtraRichEditStringId.MenuCmd_ChangeParagraphLeftIndent:
                    return "改变段落的左缩进";

                case XtraRichEditStringId.MenuCmd_ChangeParagraphLeftIndentDescription:
                    return "改变段落的左缩进";

                case XtraRichEditStringId.MenuCmd_ChangeParagraphRightIndent:
                    return "改变段落的右缩进";

                case XtraRichEditStringId.MenuCmd_ChangeParagraphRightIndentDescription:
                    return "改变段落的右缩进";

                case XtraRichEditStringId.MenuCmd_ChangeParagraphFirstLineIndent:
                    return "增加段落的第一行缩进";

                case XtraRichEditStringId.MenuCmd_ChangeParagraphFirstLineIndentDescription:
                    return "增加段落的第一行缩进";

                case XtraRichEditStringId.MenuCmd_IncrementParagraphLeftIndent:
                    return "增加段落缩排";

                case XtraRichEditStringId.MenuCmd_IncrementParagraphLeftIndentDescription:
                    return "增加段落缩排";

                case XtraRichEditStringId.MenuCmd_IncrementIndent:
                    return "增加缩排";

                case XtraRichEditStringId.MenuCmd_IncrementIndentDescription:
                    return "增加缩排";

                case XtraRichEditStringId.MenuCmd_DecrementParagraphLeftIndent:
                    return "减少段落缩排";

                case XtraRichEditStringId.MenuCmd_DecrementParagraphLeftIndentDescription:
                    return "减少段落缩排";

                case XtraRichEditStringId.MenuCmd_DecrementIndent:
                    return "减少缩排";

                case XtraRichEditStringId.MenuCmd_DecrementIndentDescription:
                    return "减少缩排";

                case XtraRichEditStringId.MenuCmd_ChangeParagraphStyle:
                    return "改变段落式样";

                case XtraRichEditStringId.MenuCmd_ChangeParagraphStyleDescription:
                    return "改变段落式样";

                case XtraRichEditStringId.MenuCmd_PlaceCaretToPhysicalPoint:
                    return "插入";

                case XtraRichEditStringId.MenuCmd_PlaceCaretToPhysicalPointDescription:
                    return "插入";

                case XtraRichEditStringId.MenuCmd_ChangeCharacterStyle:
                    return "改变文字式样";

                case XtraRichEditStringId.MenuCmd_ChangeCharacterStyleDescription:
                    return "改变文字式样";

                case XtraRichEditStringId.MenuCmd_Find:
                    return "查找";

                case XtraRichEditStringId.MenuCmd_FindDescription:
                    return "在文本中查找文字";

                case XtraRichEditStringId.MenuCmd_Replace:
                    return "替换";

                case XtraRichEditStringId.MenuCmd_ReplaceDescription:
                    return "在文本中替换文字";

                case XtraRichEditStringId.MenuCmd_ReplaceText:
                    return "替换文本";

                case XtraRichEditStringId.MenuCmd_ReplaceTextDescription:
                    return "替换文本";

                case XtraRichEditStringId.MenuCmd_ReplaceAllForward:
                    return "向上替換所有";

                case XtraRichEditStringId.MenuCmd_ReplaceAllForwardDescription:
                    return "向上替換所有";

                case XtraRichEditStringId.MenuCmd_ReplaceAllBackward:
                    return "向下替換所有";

                case XtraRichEditStringId.MenuCmd_ReplaceAllBackwardDescription:
                    return "向下替換所有";

                case XtraRichEditStringId.MenuCmd_ScrollToPage:
                    return "移动到上一页";

                case XtraRichEditStringId.MenuCmd_ScrollToPageDescription:
                    return "移动到上一页";

                case XtraRichEditStringId.MenuCmd_Print:
                    return "打印(&P)";

                case XtraRichEditStringId.MenuCmd_PrintDescription:
                    return "选择打印机，页数，和其它在打印前的打印选项";

                case XtraRichEditStringId.MenuCmd_QuickPrint:
                    return "快速打印(&Q)";

                case XtraRichEditStringId.MenuCmd_QuickPrintDescription:
                    return "使用默认的打印机打印文檔";

                case XtraRichEditStringId.MenuCmd_PrintPreview:
                    return "打印预览(&V)";

                case XtraRichEditStringId.MenuCmd_PrintPreviewDescription:
                    return "在打印之前预览";

                case XtraRichEditStringId.MenuCmd_BrowserPrint:
                    return "浏览器打印";

                case XtraRichEditStringId.MenuCmd_BrowserPrintDescription:
                    return "在浏览器中打印文档。";

                case XtraRichEditStringId.MenuCmd_BrowserPrintPreview:
                    return "在浏览器中打印预览";

                case XtraRichEditStringId.MenuCmd_BrowserPrintPreviewDescription:
                    return "在浏览器中打印之前浏览页。";

                case XtraRichEditStringId.MenuCmd_ChangeMistakenWord:
                    return "(无拼写建议)";

                case XtraRichEditStringId.MenuCmd_ChangeMistakenWordDescription:
                    return "(无拼写建议)";

                case XtraRichEditStringId.MenuCmd_ClearFormatting:
                    return "清除格式";

                case XtraRichEditStringId.MenuCmd_ClearFormattingDescription:
                    return "清除格式";

                case XtraRichEditStringId.MenuCmd_CreateField:
                    return "创建域";

                case XtraRichEditStringId.MenuCmd_CreateFieldDescription:
                    return "创建域";

                case XtraRichEditStringId.MenuCmd_CreateBookmark:
                    return "创建书签";

                case XtraRichEditStringId.MenuCmd_CreateBookmarkDescription:
                    return "创建书签";

                case XtraRichEditStringId.MenuCmd_SelectBookmark:
                    return "选择书签";

                case XtraRichEditStringId.MenuCmd_SelectBookmarkDescription:
                    return "选择书签";

                case XtraRichEditStringId.MenuCmd_DeleteBookmark:
                    return "删除书签";

                case XtraRichEditStringId.MenuCmd_DeleteBookmarkDescription:
                    return "删除书签";

                case XtraRichEditStringId.MenuCmd_UpdateField:
                    return "更新域";

                case XtraRichEditStringId.MenuCmd_UpdateFieldDescription:
                    return "更新域";

                case XtraRichEditStringId.MenuCmd_UpdateFields:
                    return "更新域";

                case XtraRichEditStringId.MenuCmd_UpdateFieldsDescription:
                    return "更新域";

                case XtraRichEditStringId.MenuCmd_ToggleFieldCodes:
                    return "切换域代码";

                case XtraRichEditStringId.MenuCmd_ToggleFieldCodesDescription:
                    return "切换域代码";

                case XtraRichEditStringId.MenuCmd_ShowAllFieldResults:
                    return "显示所有域结果";

                case XtraRichEditStringId.MenuCmd_ShowAllFieldResultsDescription:
                    return "显示所有域结果";

                case XtraRichEditStringId.MenuCmd_ShowAllFieldCodes:
                    return "显示所有域代码";

                case XtraRichEditStringId.MenuCmd_ShowAllFieldCodesDescription:
                    return "显示所有域代码";

                case XtraRichEditStringId.MenuCmd_ToggleViewMergedData:
                    return "查看合并的数据";

                case XtraRichEditStringId.MenuCmd_ToggleViewMergedDataDescription:
                    return "查看合并的数据";

                case XtraRichEditStringId.MenuCmd_SelectFieldNextToCaret:
                    return "向下插入符号";

                case XtraRichEditStringId.MenuCmd_SelectFieldNextToCaretDescription:
                    return "向下插入符号";

                case XtraRichEditStringId.MenuCmd_SelectFieldPrevToCaret:
                    return "向上插入符号";

                case XtraRichEditStringId.MenuCmd_SelectFieldPrevToCaretDescription:
                    return "向下插入符号";

                case XtraRichEditStringId.MenuCmd_CreateHyperlink:
                    return "创建超链接";

                case XtraRichEditStringId.MenuCmd_CreateHyperlinkDescription:
                    return "创建超链接";

                case XtraRichEditStringId.MenuCmd_InsertHyperlink:
                    return "插入超链接";

                case XtraRichEditStringId.MenuCmd_InsertHyperlinkDescription:
                    return "插入超链接";

                case XtraRichEditStringId.MenuCmd_SwitchToDraftView:
                    return "草稿视图";

                case XtraRichEditStringId.MenuCmd_SwitchToDraftViewDescription:
                    return "草稿视图是快速编辑文本文件，某些元素是看不到的，如页眉和页脚。";

                case XtraRichEditStringId.MenuCmd_SwitchToPrintLayoutView:
                    return "打印视图";

                case XtraRichEditStringId.MenuCmd_SwitchToPrintLayoutViewDescription:
                    return "将在打印页显示文档";

                case XtraRichEditStringId.MenuCmd_SwitchToSimpleView:
                    return "简易视图";

                case XtraRichEditStringId.MenuCmd_SwitchToSimpleViewDescription:
                    return "简单的备忘录文档";

                case XtraRichEditStringId.MenuCmd_OpenHyperlink:
                    return "打开超链接";

                case XtraRichEditStringId.MenuCmd_OpenHyperlinkDescription:
                    return "打开超链接";

                case XtraRichEditStringId.MenuCmd_RemoveHyperlink:
                    return "取消超链接";

                case XtraRichEditStringId.MenuCmd_RemoveHyperlinkDescription:
                    return "取消超链接";

                case XtraRichEditStringId.MenuCmd_ModifyHyperlink:
                    return "修改超链接";

                case XtraRichEditStringId.MenuCmd_ModifyHyperlinkDescription:
                    return "修改超链接";

                case XtraRichEditStringId.MenuCmd_MergeTableCells:
                    return "合并单元格";

                case XtraRichEditStringId.MenuCmd_MergeTableCellsDescription:
                    return "所选的单元格合并成一个单元格。";

                case XtraRichEditStringId.MenuCmd_SplitTable:
                    return "拆分表格";

                case XtraRichEditStringId.MenuCmd_SplitTableDescription:
                    return "拆分表格为2个表格，选定的行会成为新表的第一行。";

                case XtraRichEditStringId.MenuCmd_SplitTableCells:
                    return "拆分单元格";

                case XtraRichEditStringId.MenuCmd_SplitTableCellsMenuItem:
                    return "拆分单元格...";

                case XtraRichEditStringId.MenuCmd_SplitTableCellsDescription:
                    return "拆分选定的单元格分成多个新行";

                case XtraRichEditStringId.MenuCmd_InsertTableColumnToTheLeft:
                    return "在左侧插入列";

                case XtraRichEditStringId.MenuCmd_InsertTableColumnToTheLeftDescription:
                    return "在所选列的左边插入新列";

                case XtraRichEditStringId.MenuCmd_InsertTableColumnToTheRight:
                    return "在右侧插入列";

                case XtraRichEditStringId.MenuCmd_InsertTableColumnToTheRightDescription:
                    return "在所选列的右边插入新列";

                case XtraRichEditStringId.MenuCmd_OpenHyperlinkAtCaretPosition:
                    return "在插入符号位置打开超链接";

                case XtraRichEditStringId.MenuCmd_OpenHyperlinkAtCaretPositionDescription:
                    return "在插入符号位置打开超链接";

                case XtraRichEditStringId.MenuCmd_EditHyperlink:
                    return "编辑超链接";

                case XtraRichEditStringId.MenuCmd_EditHyperlinkDescription:
                    return "编辑超链接";

                case XtraRichEditStringId.MenuCmd_EditPageHeader:
                    return "页眉";

                case XtraRichEditStringId.MenuCmd_EditPageHeaderDescription:
                    return "编辑页眉";

                case XtraRichEditStringId.MenuCmd_EditPageFooter:
                    return "页脚";

                case XtraRichEditStringId.MenuCmd_EditPageFooterDescription:
                    return "编辑页脚";

                case XtraRichEditStringId.MenuCmd_ClosePageHeaderFooter:
                    return "关闭页眉和页脚";

                case XtraRichEditStringId.MenuCmd_ClosePageHeaderFooterDescription:
                    return "关闭页眉和页脚";

                case XtraRichEditStringId.MenuCmd_GoToPage:
                    return "到页面";

                case XtraRichEditStringId.MenuCmd_GoToPageDescription:
                    return "到页面";

                case XtraRichEditStringId.MenuCmd_GoToPageHeader:
                    return "到页眉";

                case XtraRichEditStringId.MenuCmd_GoToPageHeaderDescription:
                    return "到此页的页眉进行编辑";

                case XtraRichEditStringId.MenuCmd_GoToPageFooter:
                    return "到页脚";

                case XtraRichEditStringId.MenuCmd_GoToPageFooterDescription:
                    return "到此页的页脚进行编辑";

                case XtraRichEditStringId.MenuCmd_ToggleHeaderFooterLinkToPrevious:
                    return "链接到上一个";

                case XtraRichEditStringId.MenuCmd_ToggleHeaderFooterLinkToPreviousDescription:
                    return "链接到上一个";

                case XtraRichEditStringId.MenuCmd_GoToPreviousHeaderFooter:
                    return "显示上一个";

                case XtraRichEditStringId.MenuCmd_GoToPreviousHeaderFooterDescription:
                    return "定位到上一节的页眉和页脚";

                case XtraRichEditStringId.MenuCmd_GoToNextHeaderFooter:
                    return "显示下一个";

                case XtraRichEditStringId.MenuCmd_GoToNextHeaderFooterDescription:
                    return "定位到下一节的页眉和页脚";

                case XtraRichEditStringId.MenuCmd_ToggleDifferentFirstPage:
                    return "首页不同";

                case XtraRichEditStringId.MenuCmd_ToggleDifferentFirstPageDescription:
                    return "指定第一页一个唯一的页眉和页脚";

                case XtraRichEditStringId.MenuCmd_ToggleDifferentOddAndEvenPages:
                    return "奇偶页不同";

                case XtraRichEditStringId.MenuCmd_ToggleDifferentOddAndEvenPagesDescription:
                    return "指定奇偶数也不同的页眉和页脚";

                case XtraRichEditStringId.MenuCmd_ChangeParagraphLineSpacing:
                    return "行间距";

                case XtraRichEditStringId.MenuCmd_ChangeParagraphLineSpacingDescription:
                    return "行间距";

                case XtraRichEditStringId.MenuCmd_ChangeSectionPageOrientation:
                    return "方向";

                case XtraRichEditStringId.MenuCmd_ChangeSectionPageOrientationDescription:
                    return "改变页面是横向还是纵向";

                case XtraRichEditStringId.MenuCmd_ChangeSectionPagePaperKind:
                    return "大小";

                case XtraRichEditStringId.MenuCmd_ChangeSectionPagePaperKindDescription:
                    return "改变当前节的大小";

                case XtraRichEditStringId.MenuCmd_SetSectionOneColumn:
                    return "1";

                case XtraRichEditStringId.MenuCmd_SetSectionOneColumnDescription:
                    return "1栏";

                case XtraRichEditStringId.MenuCmd_SetSectionTwoColumns:
                    return "2";

                case XtraRichEditStringId.MenuCmd_SetSectionTwoColumnsDescription:
                    return "2栏";

                case XtraRichEditStringId.MenuCmd_SetSectionThreeColumns:
                    return "3";

                case XtraRichEditStringId.MenuCmd_SetSectionThreeColumnsDescription:
                    return "3栏";

                case XtraRichEditStringId.MenuCmd_SetSectionColumns:
                    return "分栏";

                case XtraRichEditStringId.MenuCmd_SetSectionColumnsDescription:
                    return "把文字分成多少栏";

                case XtraRichEditStringId.MenuCmd_SetLandscapePageOrientation:
                    return "横向";

                case XtraRichEditStringId.MenuCmd_SetLandscapePageOrientationDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_SetPortraitPageOrientation:
                    return "纵向";

                case XtraRichEditStringId.MenuCmd_SetPortraitPageOrientationDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_ChangeSectionPageMargins:
                    return "页边距";

                case XtraRichEditStringId.MenuCmd_ChangeSectionPageMarginsDescription:
                    return "页边距大小";

                case XtraRichEditStringId.MenuCmd_SetNormalSectionPageMargins:
                    return "常规\r\n上:\t{1,10}\t下:\t{3,10}\r\n左:\t{0,10}\t右:\t\t{2,10}";

                case XtraRichEditStringId.MenuCmd_SetNormalSectionPageMarginsDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_SetNarrowSectionPageMargins:
                    return "窄\r\n上:\t{1,10}\t下:\t{3,10}\r\n左:\t{0,10}\t右:\t\t{2,10}";

                case XtraRichEditStringId.MenuCmd_SetNarrowSectionPageMarginsDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_SetModerateSectionPageMargins:
                    return "中等\r\n上:\t{1,10}\t下:\t{3,10}\r\n左:\t{0,10}\t右:\t\t{2,10}";

                case XtraRichEditStringId.MenuCmd_SetModerateSectionPageMarginsDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_SetWideSectionPageMargins:
                    return "宽\r\n上:\t{1,10}\t下:\t{3,10}\r\n左:\t{0,10}\t右:\t\t{2,10}";

                case XtraRichEditStringId.MenuCmd_SetWideSectionPageMarginsDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_NextDataRecord:
                    return "下一个记录";

                case XtraRichEditStringId.MenuCmd_NextDataRecordDescription:
                    return "下一个记录";

                case XtraRichEditStringId.MenuCmd_PreviousDataRecord:
                    return "上一个记录";

                case XtraRichEditStringId.MenuCmd_PreviousDataRecordDescription:
                    return "上一个记录";

                case XtraRichEditStringId.MenuCmd_LastDataRecord:
                    return "最后一个记录";

                case XtraRichEditStringId.MenuCmd_LastDataRecordDescription:
                    return "最后一个记录";

                case XtraRichEditStringId.MenuCmd_FirstDataRecord:
                    return "第一个记录";

                case XtraRichEditStringId.MenuCmd_FirstDataRecordDescription:
                    return "第一个记录";

                case XtraRichEditStringId.MenuCmd_MailMergeSaveDocumentAsCommand:
                    return "邮件合并";

                case XtraRichEditStringId.MenuCmd_MailMergeSaveDocumentAsCommandDescription:
                    return "邮件合并";

                case XtraRichEditStringId.MenuCmd_ResetCharacterFormatting:
                    return "重置字符格式";

                case XtraRichEditStringId.MenuCmd_ResetCharacterFormattingDescription:
                    return "使所选的式样为默认的字符格式";

                case XtraRichEditStringId.MenuCmd_SelectTableColumns:
                    return "选择列";

                case XtraRichEditStringId.MenuCmd_SelectTableColumnsDescription:
                    return "选择列";

                case XtraRichEditStringId.MenuCmd_SelectTableCell:
                    return "选择单元格";

                case XtraRichEditStringId.MenuCmd_SelectTableCellDescription:
                    return "选择单元格";

                case XtraRichEditStringId.MenuCmd_SelectTableRow:
                    return "选择行";

                case XtraRichEditStringId.MenuCmd_SelectTableRowDescription:
                    return "选择行";

                case XtraRichEditStringId.MenuCmd_SelectTable:
                    return "选择表格";

                case XtraRichEditStringId.MenuCmd_SelectTableDescription:
                    return "选择表格";

                case XtraRichEditStringId.MenuCmd_SelectTableElements:
                    return "选择";

                case XtraRichEditStringId.MenuCmd_SelectTableElementsDescription:
                    return "选择当前单元格、行、列或整个表格。";

                case XtraRichEditStringId.MenuCmd_ProtectDocument:
                    return "保护文档";

                case XtraRichEditStringId.MenuCmd_ProtectDocumentDescription:
                    return "编辑文档时输入密码";

                case XtraRichEditStringId.MenuCmd_UnprotectDocument:
                    return "撤销保护";

                case XtraRichEditStringId.MenuCmd_UnprotectDocumentDescription:
                    return "撤销保护";

                case XtraRichEditStringId.MenuCmd_MakeTextUpperCase:
                    return "大写";

                case XtraRichEditStringId.MenuCmd_MakeTextUpperCaseDescription:
                    return "所选文本全部转换成大写。";

                case XtraRichEditStringId.MenuCmd_MakeTextLowerCase:
                    return "小写";

                case XtraRichEditStringId.MenuCmd_MakeTextLowerCaseDescription:
                    return "所选文本全部转换成小写";

                case XtraRichEditStringId.MenuCmd_ToggleTextCase:
                    return "切换大小写";

                case XtraRichEditStringId.MenuCmd_ToggleTextCaseDescription:
                    return "切换大小写";

                case XtraRichEditStringId.MenuCmd_ChangeTextCase:
                    return "变更大小写";

                case XtraRichEditStringId.MenuCmd_ChangeTextCaseDescription:
                    return "所选文本全部转换成大写、小写或其他常规以大写字母开头";

                case XtraRichEditStringId.MenuCmd_ChangeSectionLineNumbering:
                    return "行号";

                case XtraRichEditStringId.MenuCmd_ChangeSectionLineNumberingDescription:
                    return "在文档每一行旁边的边距中增加行号.";

                case XtraRichEditStringId.MenuCmd_SetSectionLineNumberingNone:
                    return "无";

                case XtraRichEditStringId.MenuCmd_SetSectionLineNumberingNoneDescription:
                    return "不显示行号";

                case XtraRichEditStringId.MenuCmd_SetSectionLineNumberingContinuous:
                    return "连续";

                case XtraRichEditStringId.MenuCmd_SetSectionLineNumberingContinuousDescription:
                    return "连续";

                case XtraRichEditStringId.MenuCmd_SetSectionLineNumberingRestartNewPage:
                    return "每页重编行号";

                case XtraRichEditStringId.MenuCmd_SetSectionLineNumberingRestartNewPageDescription:
                    return "每页重编行号";

                case XtraRichEditStringId.MenuCmd_SetSectionLineNumberingRestartNewSection:
                    return "每节重编行号";

                case XtraRichEditStringId.MenuCmd_SetSectionLineNumberingRestartNewSectionDescription:
                    return "每节重编行号";

                case XtraRichEditStringId.MenuCmd_ParagraphSuppressLineNumbers:
                    return "静止行号用于当前段落";

                case XtraRichEditStringId.MenuCmd_ParagraphSuppressLineNumbersDescription:
                    return "静止行号用于当前段落";

                case XtraRichEditStringId.MenuCmd_ParagraphSuppressHyphenation:
                    return "静止断字用于当前段落";

                case XtraRichEditStringId.MenuCmd_ParagraphSuppressHyphenationDescription:
                    return "静止断字用于当前段落";

                case XtraRichEditStringId.MenuCmd_ShowLineNumberingForm:
                    return "行编号选项";

                case XtraRichEditStringId.MenuCmd_ShowLineNumberingFormDescription:
                    return "行编号选项";

                case XtraRichEditStringId.MenuCmd_ShowPageSetupForm:
                    return "页面设置";

                case XtraRichEditStringId.MenuCmd_ShowPageSetupFormDescription:
                    return "显示页面设置对话框";

                case XtraRichEditStringId.MenuCmd_ShowColumnsSetupForm:
                    return "其他列";

                case XtraRichEditStringId.MenuCmd_ShowColumnsSetupFormDescription:
                    return "显示自定义列宽对话框";

                case XtraRichEditStringId.MenuCmd_ShowTablePropertiesForm:
                    return "属性";

                case XtraRichEditStringId.MenuCmd_ShowTablePropertiesFormDescription:
                    return "显示表格属性对话框，更改高级表格属性，如缩进和文字环绕选项。";

                case XtraRichEditStringId.MenuCmd_ShowTablePropertiesFormMenuItem:
                    return "表格属性...";

                case XtraRichEditStringId.MenuCmd_ShowTablePropertiesFormDescriptionMenuItem:
                    return "显示表格属性对话框。";

                case XtraRichEditStringId.MenuCmd_IncrementParagraphOutlineLevel:
                    return "增加大纲级别";

                case XtraRichEditStringId.MenuCmd_IncrementParagraphOutlineLevelDescription:
                    return "增加大纲级别";

                case XtraRichEditStringId.MenuCmd_DecrementParagraphOutlineLevel:
                    return "缩减大纲级别";

                case XtraRichEditStringId.MenuCmd_DecrementParagraphOutlineLevelDescription:
                    return "缩减大纲级别";

                case XtraRichEditStringId.MenuCmd_SetParagraphBodyTextLevel:
                    return "不在目录中显示";

                case XtraRichEditStringId.MenuCmd_SetParagraphBodyTextLevelDescription:
                    return "不在目录中显示";

                case XtraRichEditStringId.MenuCmd_SetParagraphHeadingLevel:
                    return "{0} 级";

                case XtraRichEditStringId.MenuCmd_SetParagraphHeadingLevelDescription:
                    return "{0} 级";

                case XtraRichEditStringId.MenuCmd_AddParagraphsToTableOfContents:
                    return "添加文字";

                case XtraRichEditStringId.MenuCmd_AddParagraphsToTableOfContentsDescription:
                    return "将当前段落添加为目录的条目.";

                case XtraRichEditStringId.MenuCmd_InsertTableOfContents:
                    return "目录";

                case XtraRichEditStringId.MenuCmd_InsertTableOfContentsDescription:
                    return "在文档中添加目录.\n\n添加目录后，即可单击添加文字按钮，在表中添加条目。";

                case XtraRichEditStringId.MenuCmd_InsertTableOfEquations:
                    return "公式目录";

                case XtraRichEditStringId.MenuCmd_InsertTableOfEquationsDescription:
                    return "将公式目录插入文档。\n\n公式目录包括文档中所有公式列表。";

                case XtraRichEditStringId.MenuCmd_InsertTableOfFigures:
                    return "图表目录";

                case XtraRichEditStringId.MenuCmd_InsertTableOfFiguresDescription:
                    return "将图表目录插入文档。\n\n图表目录包括文档中所有图表列表。";

                case XtraRichEditStringId.MenuCmd_InsertTableOfTables:
                    return "表格目录";

                case XtraRichEditStringId.MenuCmd_InsertTableOfTablesDescription:
                    return "将表格目录插入文档。\n\n表格目录包括文档中所有表格列表。";

                case XtraRichEditStringId.MenuCmd_InsertTableOfFiguresPlaceholder:
                    return "插入图表目录";

                case XtraRichEditStringId.MenuCmd_InsertTableOfFiguresPlaceholderDescription:
                    return "将图表目录插入文档。\n\n图表目录包括文档中所有图表、表格和公式的列表。";

                case XtraRichEditStringId.MenuCmd_InsertEquationsCaption:
                    return "公式题注";

                case XtraRichEditStringId.MenuCmd_InsertEquationsCaptionDescription:
                    return "添加公式题注";

                case XtraRichEditStringId.MenuCmd_InsertFiguresCaption:
                    return "图表题注";

                case XtraRichEditStringId.MenuCmd_InsertFiguresCaptionDescription:
                    return "添加图表题注";

                case XtraRichEditStringId.MenuCmd_InsertTablesCaption:
                    return "表格题注";

                case XtraRichEditStringId.MenuCmd_InsertTablesCaptionDescription:
                    return "添加表格题注";

                case XtraRichEditStringId.MenuCmd_InsertCaptionPlaceholder:
                    return "插入题注";

                case XtraRichEditStringId.MenuCmd_InsertCaptionPlaceholderDescription:
                    return "为图片或其他图像添加题注。\n\n题注是对象下文显示的一行文字,用来描述该对象.";

                case XtraRichEditStringId.MenuCmd_UpdateTableOfContents:
                    return "更新目录";

                case XtraRichEditStringId.MenuCmd_UpdateTableOfContentsDescription:
                    return "更新目录,使其包含文档中所有条目.";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectSquareTextWrapType:
                    return "外形轮廓";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectSquareTextWrapTypeDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectBehindTextWrapType:
                    return "在文本后面";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectBehindTextWrapTypeDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectInFrontOfTextWrapType:
                    return "在前面的文本";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectInFrontOfTextWrapTypeDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectThroughTextWrapType:
                    return "通过";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectThroughTextWrapTypeDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectTightTextWrapType:
                    return "紧凑";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectTightTextWrapTypeDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectTopAndBottomTextWrapType:
                    return "上部底部";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectTopAndBottomTextWrapTypeDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectTopLeftAlignment:
                    return "上部左部";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectTopLeftAlignmentDescription:
                    return "文字环绕外形轮廓的位置在左上";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectTopCenterAlignment:
                    return "上部中央";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectTopCenterAlignmentDescription:
                    return "在上部中心位置文字的外形轮廓";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectTopRightAlignment:
                    return "上部右部";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectTopRightAlignmentDescription:
                    return "文字的形状轮廓的位置在右上";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectMiddleLeftAlignment:
                    return "中心左部";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectMiddleLeftAlignmentDescription:
                    return "在中间位置，左文字设置形状轮廓";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectMiddleCenterAlignment:
                    return "中心";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectMiddleCenterAlignmentDescription:
                    return "在在中东文字设置形状轮廓中心的位置";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectMiddleRightAlignment:
                    return "中间偏右";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectMiddleRightAlignmentDescription:
                    return "在中右文字色设置形状轮廓的位置";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectBottomLeftAlignment:
                    return "左下角";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectBottomLeftAlignmentDescription:
                    return "在底部的位置左边文字设置形状轮廓";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectBottomCenterAlignment:
                    return "底部居中";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectBottomCenterAlignmentDescription:
                    return "形状轮廓的文字设置在底部中心位置";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectBottomRightAlignment:
                    return "右下";

                case XtraRichEditStringId.MenuCmd_SetFloatingObjectBottomRightAlignmentDescription:
                    return "在右下的文字设置形状轮廓的位置";

                case XtraRichEditStringId.MenuCmd_FloatingObjectBringForward:
                    return "上移一层";

                case XtraRichEditStringId.MenuCmd_FloatingObjectBringForwardDescription:
                    return "带在前面的所有其他对象的选定对象，所以没有它的背后隐藏另一个对象的一部分.";

                case XtraRichEditStringId.MenuCmd_FloatingObjectBringToFront:
                    return "置于顶层";

                case XtraRichEditStringId.MenuCmd_FloatingObjectBringToFrontDescription:
                    return "把它的一部分，所以没有在前面的所有其他对象的选定对象的背后隐藏另一个对象。.";

                case XtraRichEditStringId.MenuCmd_FloatingObjectBringInFrontOfText:
                    return "浮于文字上方";

                case XtraRichEditStringId.MenuCmd_FloatingObjectBringInFrontOfTextDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_FloatingObjectSendBackward:
                    return "下移一层";

                case XtraRichEditStringId.MenuCmd_FloatingObjectSendBackwardDescription:
                    return "发送所选对象的落后，因此，它是隐藏在它前面的对象.";

                case XtraRichEditStringId.MenuCmd_FloatingObjectSendToBack:
                    return "返回";

                case XtraRichEditStringId.MenuCmd_FloatingObjectSendToBackDescription:
                    return "发送背后的所有其他对象的选定对象.";

                case XtraRichEditStringId.MenuCmd_FloatingObjectSendBehindText:
                    return "设置衬于文字下方";

                case XtraRichEditStringId.MenuCmd_FloatingObjectSendBehindTextDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_ChangeFloatingObjectTextWrapType:
                    return "自动换行";

                case XtraRichEditStringId.MenuCmd_ChangeFloatingObjectTextWrapTypeDescription:
                    return "更改文字环绕选定对象的包装方式。要配置对象，以便它随着移动文本周围的文本，选择\"与文字排列\"";

                case XtraRichEditStringId.MenuCmd_ChangeFloatingObjectAlignment:
                    return "位置";

                case XtraRichEditStringId.MenuCmd_ChangeFloatingObjectAlignmentDescription:
                    return "向前带来选定的对象，因此，它是由更少的对象，在它前面的是隐藏";

                case XtraRichEditStringId.MenuCmd_FloatingObjectBringForwardPlaceholder:
                    return "放在前一层";

                case XtraRichEditStringId.MenuCmd_FloatingObjectBringForwardPlaceholderDescription:
                    return "Bring the selected object forward so that it is hidden by fewer object that are in front of it.";

                case XtraRichEditStringId.MenuCmd_FloatingObjectSendBackwardPlaceholder:
                    return "Send Backward";

                case XtraRichEditStringId.MenuCmd_FloatingObjectSendBackwardPlaceholderDescription:
                    return "发送所选对象的落后，因此，它是隐藏在它前面的对象";

                case XtraRichEditStringId.MenuCmd_ShowPageMarginsSetupForm:
                    return "自定义边距";

                case XtraRichEditStringId.MenuCmd_ShowPageMarginsSetupFormDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_ShowPagePaperSetupForm:
                    return "更多的边距大小";

                case XtraRichEditStringId.MenuCmd_ShowPagePaperSetupFormDescription:
                    return " ";

                case XtraRichEditStringId.MenuCmd_ChangeFloatingObjectFillColor:
                    return "形状轮廓填充";

                case XtraRichEditStringId.MenuCmd_ChangeFloatingObjectFillColorDescription:
                    return "Fill the selected shape with a solid color.";

                case XtraRichEditStringId.MenuCmd_ChangeFloatingObjectOutlineColor:
                    return "形状轮廓";

                case XtraRichEditStringId.MenuCmd_ChangeFloatingObjectOutlineColorDescription:
                    return "自定义选中形状轮廓颜色.";

                case XtraRichEditStringId.MenuCmd_ChangeFloatingObjectOutlineWidth:
                    return "形状轮廓边框";

                case XtraRichEditStringId.MenuCmd_ChangeFloatingObjectOutlineWidthDescription:
                    return "为选定形状的自定义宽度.";

                case XtraRichEditStringId.MenuCmd_InsertTextBox:
                    return "文本框";

                case XtraRichEditStringId.MenuCmd_InsertTextBoxDescription:
                    return "插入文本到文档中不";

                case XtraRichEditStringId.MenuCmd_InsertFloatingObjectPicture:
                    return "图片";

                case XtraRichEditStringId.MenuCmd_InsertFloatingObjectPictureDescription:
                    return "插一张图片到文件中";

                case XtraRichEditStringId.Msg_InvalidBeginInit:
                    return "错误：在BeginUpdate内部调用BeginInit";

                case XtraRichEditStringId.Msg_InvalidEndInit:
                    return "错误：没有进行BeginInit或者在BeginUpdate内部调用EndInit/CancelInit";

                case XtraRichEditStringId.Msg_InvalidBeginUpdate:
                    return "错误：在BeginInit内部调用BeginUpdate";

                case XtraRichEditStringId.Msg_InvalidEndUpdate:
                    return "错误：没有进行BeginInit或者在BeginUpdate内部调用EndInit/CancelInit";

                case XtraRichEditStringId.Msg_InvalidSetCharacterProperties:
                    return "没有进行BeginInit或者没有增加对象到文档，而尝试设置属性";

                case XtraRichEditStringId.Msg_InvalidSetParagraphProperties:
                    return "没有进行BeginInit或者没有增加对象到文档，而尝试设置属性";

                case XtraRichEditStringId.Msg_InvalidParentStyle:
                    return "父级格式赋值无效：循环引用";

                case XtraRichEditStringId.Msg_InvalidDocumentModel:
                    return "文档管理不相等";

                case XtraRichEditStringId.Msg_TableIntegrityError:
                    return "无效的完整表格";

                case XtraRichEditStringId.Msg_InvalidParagraphContainNumbering:
                    return "错误：段落已经包含编号";

                case XtraRichEditStringId.Msg_InvalidCopyFromDocumentModel:
                    return "无效的复制：不同的源和目标文文件模式";

                case XtraRichEditStringId.Msg_InvalidNumber:
                    return "这是无效的数字";

                case XtraRichEditStringId.Msg_InvalidFontSize:
                    return "此数字必须是{0}到{1}之间";

                case XtraRichEditStringId.Msg_InvalidValueRange:
                    return "此值必须是{0}到{1}之间";

                case XtraRichEditStringId.Msg_InvalidDivisor:
                    return "数字必须是{0}的除数";

                case XtraRichEditStringId.Msg_UsedWrongUnit:
                    return "测量单位指定不正确";

                case XtraRichEditStringId.Msg_InvalidTabStop:
                    return "这不是有效的标签停止";

                case XtraRichEditStringId.Msg_VariableDeletedOrMissed:
                    return "错误：文档变量丢失或删除";

                case XtraRichEditStringId.Msg_SearchComplete:
                    return "查找完成。";

                case XtraRichEditStringId.Msg_SearchInForwardDirectionComplete:
                    return "结束的文檔已达到";

                case XtraRichEditStringId.Msg_SearchInBackwardDirectionComplete:
                    return "开始的文檔已达到";

                case XtraRichEditStringId.Msg_StyleAlreadyLinked:
                    return "错误：一个或多个样式已经连接";

                case XtraRichEditStringId.Msg_ErrorLinkDeletedStyle:
                    return "错误：不能删除链接的样式";

                case XtraRichEditStringId.Msg_SearchItemNotFound:
                    return "查找未找到";

                case XtraRichEditStringId.Msg_SearchInSelectionComplete:
                    return "在选定的范围中查找完成。";

                case XtraRichEditStringId.Msg_ContinueSearchFromBeginningQuestion:
                    return "你想从此文檔的开头开始查找吗？";

                case XtraRichEditStringId.Msg_ContinueSearchFromEndQuestion:
                    return "你想从此文檔的结束开始查找吗？";

                case XtraRichEditStringId.Msg_ContinueSearchInRemainderQuestion:
                    return "你想查找此文檔的剩余部分吗？";

                case XtraRichEditStringId.Msg_ReplacementsCount:
                    return "{0}替换已完成";

                case XtraRichEditStringId.Msg_InvalidStyleName:
                    return "无效的样式名";

                case XtraRichEditStringId.Msg_IncorrectNumericFieldFormat:
                    return "錯誤！數字不能代表在指定格式";

                case XtraRichEditStringId.Msg_SyntaxErrorInFieldPattern:
                    return "！語法錯誤，{0}";

                case XtraRichEditStringId.Msg_UnmatchedQuotesInFieldPattern:
                    return "錯誤！模式串包含不匹配的引號";

                case XtraRichEditStringId.Msg_UnknownSwitchArgument:
                    return "错误！未知的转换参数";

                case XtraRichEditStringId.Msg_UnexpectedEndOfFormula:
                    return "！异常的公式结尾";

                case XtraRichEditStringId.Msg_MissingOperator:
                    return "！缺少操作符";

                case XtraRichEditStringId.Msg_ZeroDivide:
                    return "！除以0";

                case XtraRichEditStringId.Msg_ClickToFollowHyperlink:
                    return "单击跟踪连接";

                case XtraRichEditStringId.Msg_BookmarkCreationFailing:
                    return "书签已经存在。是否替换？";

                case XtraRichEditStringId.FileFilterDescription_AllFiles:
                    return "所有文件";

                case XtraRichEditStringId.FileFilterDescription_AllSupportedFiles:
                    return "所有支持的文件";

                case XtraRichEditStringId.FileFilterDescription_DocFiles:
                    return "Microsoft Word Document";

                case XtraRichEditStringId.FileFilterDescription_HtmlFiles:
                    return "HTML";

                case XtraRichEditStringId.FileFilterDescription_MhtFiles:
                    return "单个文件网页";

                case XtraRichEditStringId.FileFilterDescription_RtfFiles:
                    return "Rich 文本格式";

                case XtraRichEditStringId.FileFilterDescription_TextFiles:
                    return "文本文件";

                case XtraRichEditStringId.FileFilterDescription_OpenXmlFiles:
                    return "Word 2007文件";

                case XtraRichEditStringId.FileFilterDescription_OpenDocumentFiles:
                    return "OpenDocument Text Document";

                case XtraRichEditStringId.FileFilterDescription_WordMLFiles:
                    return "XML 文件";

                case XtraRichEditStringId.FileFilterDescription_XamlFiles:
                    return "Xaml 文件";

                case XtraRichEditStringId.FileFilterDescription_ePubFiles:
                    return "电子出版物";

                case XtraRichEditStringId.FileFilterDescription_PDFFiles:
                    return "PDF";

                case XtraRichEditStringId.FileFilterDescription_BitmapFiles:
                    return "Bit";

                case XtraRichEditStringId.FileFilterDescription_JPEGFiles:
                    return "JPEG";

                case XtraRichEditStringId.FileFilterDescription_PNGFiles:
                    return "PNG";

                case XtraRichEditStringId.FileFilterDescription_GifFiles:
                    return "gif";

                case XtraRichEditStringId.FileFilterDescription_TiffFiles:
                    return "tiff";

                case XtraRichEditStringId.FileFilterDescription_EmfFiles:
                    return "Microsoft Enhanced Metafile";

                case XtraRichEditStringId.FileFilterDescription_WmfFiles:
                    return "Windows Metafile";

                case XtraRichEditStringId.DefaultStyleName_Normal:
                    return "正文";

                case XtraRichEditStringId.DefaultStyleName_heading1:
                    return "标题 1";

                case XtraRichEditStringId.DefaultStyleName_heading2:
                    return "标题 2";

                case XtraRichEditStringId.DefaultStyleName_heading3:
                    return "标题 3";

                case XtraRichEditStringId.DefaultStyleName_heading4:
                    return "标题 4";

                case XtraRichEditStringId.DefaultStyleName_heading5:
                    return "标题 5";

                case XtraRichEditStringId.DefaultStyleName_heading6:
                    return "标题 6";

                case XtraRichEditStringId.DefaultStyleName_heading7:
                    return "标题 7";

                case XtraRichEditStringId.DefaultStyleName_heading8:
                    return "标题 8";

                case XtraRichEditStringId.DefaultStyleName_heading9:
                    return "标题 9";

                case XtraRichEditStringId.DefaultStyleName_index1:
                    return "索引 1";

                case XtraRichEditStringId.DefaultStyleName_index2:
                    return "索引 2";

                case XtraRichEditStringId.DefaultStyleName_index3:
                    return "索引 3";

                case XtraRichEditStringId.DefaultStyleName_index4:
                    return "索引 4";

                case XtraRichEditStringId.DefaultStyleName_index5:
                    return "索引 5";

                case XtraRichEditStringId.DefaultStyleName_index6:
                    return "索引 6";

                case XtraRichEditStringId.DefaultStyleName_index7:
                    return "索引 7";

                case XtraRichEditStringId.DefaultStyleName_index8:
                    return "索引 8";

                case XtraRichEditStringId.DefaultStyleName_index9:
                    return "索引 9";

                case XtraRichEditStringId.DefaultStyleName_toc1:
                    return "目录 1";

                case XtraRichEditStringId.DefaultStyleName_toc2:
                    return "目录 2";

                case XtraRichEditStringId.DefaultStyleName_toc3:
                    return "目录 3";

                case XtraRichEditStringId.DefaultStyleName_toc4:
                    return "目录 4";

                case XtraRichEditStringId.DefaultStyleName_toc5:
                    return "目录 5";

                case XtraRichEditStringId.DefaultStyleName_toc6:
                    return "目录 6";

                case XtraRichEditStringId.DefaultStyleName_toc7:
                    return "目录 7";

                case XtraRichEditStringId.DefaultStyleName_toc8:
                    return "目录 8";

                case XtraRichEditStringId.DefaultStyleName_toc9:
                    return "目录 9";

                case XtraRichEditStringId.DefaultStyleName_NormalIndent:
                    return "正文缩进";

                case XtraRichEditStringId.DefaultStyleName_footnotetext:
                    return "脚注文本";

                case XtraRichEditStringId.DefaultStyleName_annotationtext:
                    return "批注文本";

                case XtraRichEditStringId.DefaultStyleName_header:
                    return "页眉";

                case XtraRichEditStringId.DefaultStyleName_footer:
                    return "页脚";

                case XtraRichEditStringId.DefaultStyleName_indexheading:
                    return "索引标题";

                case XtraRichEditStringId.DefaultStyleName_caption:
                    return "标题";

                case XtraRichEditStringId.DefaultStyleName_tableoffigures:
                    return "图表";

                case XtraRichEditStringId.DefaultStyleName_envelopeaddress:
                    return "收信人地址";

                case XtraRichEditStringId.DefaultStyleName_envelopereturn:
                    return "寄信人地址";

                case XtraRichEditStringId.DefaultStyleName_footnotereference:
                    return "脚注参考";

                case XtraRichEditStringId.DefaultStyleName_annotationreference:
                    return "批注参考";

                case XtraRichEditStringId.DefaultStyleName_linenumber:
                    return "行号";

                case XtraRichEditStringId.DefaultStyleName_pagenumber:
                    return "页号";

                case XtraRichEditStringId.DefaultStyleName_endnotereference:
                    return "尾注参考";

                case XtraRichEditStringId.DefaultStyleName_endnotetext:
                    return "尾注文本";

                case XtraRichEditStringId.DefaultStyleName_tableofauthorities:
                    return "授权";

                case XtraRichEditStringId.DefaultStyleName_macrotoaheading:
                    return "宏标题";

                case XtraRichEditStringId.DefaultStyleName_List:
                    return "列表";

                case XtraRichEditStringId.DefaultStyleName_List2:
                    return "列表 2";

                case XtraRichEditStringId.DefaultStyleName_List3:
                    return "列表 3";

                case XtraRichEditStringId.DefaultStyleName_List4:
                    return "列表 4";

                case XtraRichEditStringId.DefaultStyleName_List5:
                    return "列表 5";

                case XtraRichEditStringId.DefaultStyleName_ListBullet:
                    return "列表项目符号";

                case XtraRichEditStringId.DefaultStyleName_ListBullet2:
                    return "列表项目符号 2";

                case XtraRichEditStringId.DefaultStyleName_ListBullet3:
                    return "列表项目符号 3";

                case XtraRichEditStringId.DefaultStyleName_ListBullet4:
                    return "列表项目符号 4";

                case XtraRichEditStringId.DefaultStyleName_ListBullet5:
                    return "列表项目符号 5";

                case XtraRichEditStringId.DefaultStyleName_ListNumber:
                    return "列表编号";

                case XtraRichEditStringId.DefaultStyleName_ListNumber2:
                    return "列表编号 2";

                case XtraRichEditStringId.DefaultStyleName_ListNumber3:
                    return "列表编号 3";

                case XtraRichEditStringId.DefaultStyleName_ListNumber4:
                    return "列表编号 4";

                case XtraRichEditStringId.DefaultStyleName_ListNumber5:
                    return "列表编号 5";

                case XtraRichEditStringId.DefaultStyleName_Title:
                    return "标题";

                case XtraRichEditStringId.DefaultStyleName_Closing:
                    return "结束语";

                case XtraRichEditStringId.DefaultStyleName_Signature:
                    return "签名";

                case XtraRichEditStringId.DefaultStyleName_DefaultParagraphFont:
                    return "默认段落字体";

                case XtraRichEditStringId.DefaultStyleName_BodyText:
                    return "正文";

                case XtraRichEditStringId.DefaultStyleName_ListContinue:
                    return "列表接续";

                case XtraRichEditStringId.DefaultStyleName_ListContinue2:
                    return "列表接续 2";

                case XtraRichEditStringId.DefaultStyleName_ListContinue3:
                    return "列表接续 3";

                case XtraRichEditStringId.DefaultStyleName_ListContinue4:
                    return "列表接续 4";

                case XtraRichEditStringId.DefaultStyleName_ListContinue5:
                    return "列表接续 5";

                case XtraRichEditStringId.DefaultStyleName_MessageHeader:
                    return "信息标题";

                case XtraRichEditStringId.DefaultStyleName_Salutation:
                    return "称呼";

                case XtraRichEditStringId.DefaultStyleName_Date:
                    return "日期";

                case XtraRichEditStringId.DefaultStyleName_BodyTextFirstIndent:
                    return "正文首行缩进";

                case XtraRichEditStringId.DefaultStyleName_BodyTextFirstIndent2:
                    return "正文首行缩进 2";

                case XtraRichEditStringId.DefaultStyleName_NoteHeading:
                    return "题注";

                case XtraRichEditStringId.DefaultStyleName_Subtitle:
                    return "副标题";

                case XtraRichEditStringId.DefaultStyleName_BodyText2:
                    return "正文文本 2";

                case XtraRichEditStringId.DefaultStyleName_BodyText3:
                    return "正文文本 3";

                case XtraRichEditStringId.DefaultStyleName_BodyTextIndent2:
                    return "正文文本缩进 2";

                case XtraRichEditStringId.DefaultStyleName_BodyTextIndent3:
                    return "正文文本缩进 3";

                case XtraRichEditStringId.DefaultStyleName_BlockText:
                    return "文本块";

                case XtraRichEditStringId.DefaultStyleName_HyperlinkFollowed:
                    return "超链接跟踪";

                case XtraRichEditStringId.DefaultStyleName_HyperlinkStrongEmphasis:
                    return "超链接增强";

                case XtraRichEditStringId.DefaultStyleName_DocumentMap:
                    return "文檔图";

                case XtraRichEditStringId.DefaultStyleName_PlainText:
                    return "纯文本";

                case XtraRichEditStringId.DefaultStyleName_EmailSignature:
                    return "E-mail 署名";

                case XtraRichEditStringId.DefaultStyleName_HTMLTopofForm:
                    return "HTML顶部";

                case XtraRichEditStringId.DefaultStyleName_HTMLBottomofForm:
                    return "HTML底部";

                case XtraRichEditStringId.DefaultStyleName_NormalWeb:
                    return "普通(Web)";

                case XtraRichEditStringId.DefaultStyleName_HTMLAcronym:
                    return "HTML首字母缩写";

                case XtraRichEditStringId.DefaultStyleName_HTMLAddress:
                    return "HTML地址";

                case XtraRichEditStringId.DefaultStyleName_HTMLCite:
                    return "HTML引用";

                case XtraRichEditStringId.DefaultStyleName_HTMLCode:
                    return "HTML编码";

                case XtraRichEditStringId.DefaultStyleName_HTMLDefinition:
                    return "HTML清晰度";

                case XtraRichEditStringId.DefaultStyleName_HTMLKeyboard:
                    return "HTML键盘";

                case XtraRichEditStringId.DefaultStyleName_HTMLPreformatted:
                    return "HTML预先格式";

                case XtraRichEditStringId.DefaultStyleName_HTMLSample:
                    return "HTML样本";

                case XtraRichEditStringId.DefaultStyleName_HTMLTypewriter:
                    return "HTML打字机";

                case XtraRichEditStringId.DefaultStyleName_HTMLVariable:
                    return "HTML变量";

                case XtraRichEditStringId.DefaultStyleName_NormalTable:
                    return "普通表格";

                case XtraRichEditStringId.DefaultStyleName_annotationsubject:
                    return "批注主题";

                case XtraRichEditStringId.DefaultStyleName_NoList:
                    return "没有列表";

                case XtraRichEditStringId.DefaultStyleName_OutlineList1:
                    return "大纲列表 1";

                case XtraRichEditStringId.DefaultStyleName_OutlineList2:
                    return "大纲列表 2";

                case XtraRichEditStringId.DefaultStyleName_OutlineList3:
                    return "大纲列表 3";

                case XtraRichEditStringId.DefaultStyleName_TableSimple1:
                    return "简明型表格 1";

                case XtraRichEditStringId.DefaultStyleName_TableSimple2:
                    return "简明型表格 2";

                case XtraRichEditStringId.DefaultStyleName_TableSimple3:
                    return "简明型表格 3";

                case XtraRichEditStringId.DefaultStyleName_TableClassic1:
                    return "古典型表格 1";

                case XtraRichEditStringId.DefaultStyleName_TableClassic2:
                    return "古典型表格 2";

                case XtraRichEditStringId.DefaultStyleName_TableClassic3:
                    return "古典型表格 3";

                case XtraRichEditStringId.DefaultStyleName_TableClassic4:
                    return "古典型表格 4";

                case XtraRichEditStringId.DefaultStyleName_TableColorful1:
                    return "彩色型表格 1";

                case XtraRichEditStringId.DefaultStyleName_TableColorful2:
                    return "彩色型表格 2";

                case XtraRichEditStringId.DefaultStyleName_TableColorful3:
                    return "彩色型表格 3";

                case XtraRichEditStringId.DefaultStyleName_TableColumns1:
                    return "竖列型表格 1";

                case XtraRichEditStringId.DefaultStyleName_TableColumns2:
                    return "竖列型表格 2";

                case XtraRichEditStringId.DefaultStyleName_TableColumns3:
                    return "竖列型表格 3";

                case XtraRichEditStringId.DefaultStyleName_TableColumns4:
                    return "竖列型表格 4";

                case XtraRichEditStringId.DefaultStyleName_TableColumns5:
                    return "竖列型表格 5";

                case XtraRichEditStringId.DefaultStyleName_TableGrid1:
                    return "网格型表格 1";

                case XtraRichEditStringId.DefaultStyleName_TableGrid2:
                    return "网格型表格 2";

                case XtraRichEditStringId.DefaultStyleName_TableGrid3:
                    return "网格型表格 3";

                case XtraRichEditStringId.DefaultStyleName_TableGrid4:
                    return "网格型表格 4";

                case XtraRichEditStringId.DefaultStyleName_TableGrid5:
                    return "网格型表格 5";

                case XtraRichEditStringId.DefaultStyleName_TableGrid6:
                    return "网格型表格 6";

                case XtraRichEditStringId.DefaultStyleName_TableGrid7:
                    return "网格型表格 7";

                case XtraRichEditStringId.DefaultStyleName_TableGrid8:
                    return "网格型表格 8";

                case XtraRichEditStringId.DefaultStyleName_TableList1:
                    return "列表型表格 1";

                case XtraRichEditStringId.DefaultStyleName_TableList2:
                    return "列表型表格 2";

                case XtraRichEditStringId.DefaultStyleName_TableList3:
                    return "列表型表格 3";

                case XtraRichEditStringId.DefaultStyleName_TableList4:
                    return "列表型表格 4";

                case XtraRichEditStringId.DefaultStyleName_TableList5:
                    return "列表型表格 5";

                case XtraRichEditStringId.DefaultStyleName_TableList6:
                    return "列表型表格 6";

                case XtraRichEditStringId.DefaultStyleName_TableList7:
                    return "列表型表格 7";

                case XtraRichEditStringId.DefaultStyleName_TableList8:
                    return "列表型表格 8";

                case XtraRichEditStringId.DefaultStyleName_Table3Deffects1:
                    return "立体型表格 1";

                case XtraRichEditStringId.DefaultStyleName_Table3Deffects2:
                    return "立体型表格 2";

                case XtraRichEditStringId.DefaultStyleName_Table3Deffects3:
                    return "立体型表格 3";

                case XtraRichEditStringId.DefaultStyleName_TableContemporary:
                    return "流行型表格";

                case XtraRichEditStringId.DefaultStyleName_TableElegant:
                    return "典雅型表格";

                case XtraRichEditStringId.DefaultStyleName_TableProfessional:
                    return "专业型表格";

                case XtraRichEditStringId.DefaultStyleName_TableSubtle1:
                    return "精巧型表格 1";

                case XtraRichEditStringId.DefaultStyleName_TableSubtle2:
                    return "精巧型表格 2";

                case XtraRichEditStringId.DefaultStyleName_TableWeb1:
                    return "网页型表格 1";

                case XtraRichEditStringId.DefaultStyleName_TableWeb2:
                    return "网页型表格 2";

                case XtraRichEditStringId.DefaultStyleName_TableWeb3:
                    return "网页型表格 3";

                case XtraRichEditStringId.DefaultStyleName_BalloonText:
                    return "气泡文本";

                case XtraRichEditStringId.DefaultStyleName_TableGrid:
                    return "网格型表格";

                case XtraRichEditStringId.LinkedCharacterStyleFormatString:
                    return "{0}字符";

                case XtraRichEditStringId.ClearFormatting:
                    return "清除格式";

                case XtraRichEditStringId.FontStyle_Bold:
                    return "粗体";

                case XtraRichEditStringId.FontStyle_Italic:
                    return "斜体";

                case XtraRichEditStringId.FontStyle_BoldItalic:
                    return "粗斜体";

                case XtraRichEditStringId.FontStyle_Regular:
                    return "常规";

                case XtraRichEditStringId.FontStyle_Strikeout:
                    return "删除线";

                case XtraRichEditStringId.FontStyle_Underline:
                    return "下划线";

                case XtraRichEditStringId.Caption_AlignmentCenter:
                    return "居中对齐";

                case XtraRichEditStringId.Caption_AlignmentJustify:
                    return "两端对齐";

                case XtraRichEditStringId.Caption_AlignmentLeft:
                    return "左对齐";

                case XtraRichEditStringId.Caption_AlignmentRight:
                    return "右对齐";

                case XtraRichEditStringId.Caption_LineSpacingSingle:
                    return "单一";

                case XtraRichEditStringId.Caption_LineSpacingSesquialteral:
                    return "1.5倍的线";

                case XtraRichEditStringId.Caption_LineSpacingDouble:
                    return "双";

                case XtraRichEditStringId.Caption_LineSpacingMultiple:
                    return "多";

                case XtraRichEditStringId.Caption_LineSpacingExactly:
                    return "正确地";

                case XtraRichEditStringId.Caption_LineSpacingAtLeast:
                    return "至少";

                case XtraRichEditStringId.Caption_FirstLineIndentNone:
                    return "(无)";

                case XtraRichEditStringId.Caption_FirstLineIndentIndented:
                    return "首行缩进";

                case XtraRichEditStringId.Caption_FirstLineIndentHanging:
                    return "顶端对齐";

                case XtraRichEditStringId.Caption_OutlineLevelBody:
                    return "正文文本";

                case XtraRichEditStringId.Caption_OutlineLevel1:
                    return "1 级";

                case XtraRichEditStringId.Caption_OutlineLevel2:
                    return "2 级";

                case XtraRichEditStringId.Caption_OutlineLevel3:
                    return "3 级";

                case XtraRichEditStringId.Caption_OutlineLevel4:
                    return "4 级";

                case XtraRichEditStringId.Caption_OutlineLevel5:
                    return "5 级";

                case XtraRichEditStringId.Caption_OutlineLevel6:
                    return "6 级";

                case XtraRichEditStringId.Caption_OutlineLevel7:
                    return "7 级";

                case XtraRichEditStringId.Caption_OutlineLevel8:
                    return "8 级";

                case XtraRichEditStringId.Caption_OutlineLevel9:
                    return "9 级";

                case XtraRichEditStringId.HyperlinkForm_SelectionInDocument:
                    return "所选范围";

                case XtraRichEditStringId.HyperlinkForm_InsertHyperlinkTitle:
                    return "插入超链接";

                case XtraRichEditStringId.HyperlinkForm_EditHyperlinkTitle:
                    return "编辑超链接";

                case XtraRichEditStringId.HyperlinkForm_SelectedBookmarkNone:
                    return "<无>";

                case XtraRichEditStringId.DialogCaption_InsertSymbol:
                    return "符号";

                case XtraRichEditStringId.TabForm_All:
                    return "所有";

                case XtraRichEditStringId.UnderlineNone:
                    return "(无)";

                case XtraRichEditStringId.ColorAuto:
                    return "自动";

                case XtraRichEditStringId.UnitAbbreviation_Inch:
                    return "\"";

                case XtraRichEditStringId.UnitAbbreviation_Centimeter:
                    return " cm";

                case XtraRichEditStringId.UnitAbbreviation_Millimeter:
                    return " mm";

                case XtraRichEditStringId.UnitAbbreviation_Pica:
                    return " pc";

                case XtraRichEditStringId.UnitAbbreviation_Point:
                    return " pt";

                case XtraRichEditStringId.UnitAbbreviation_Percent:
                    return "%";

                case XtraRichEditStringId.FindAndReplaceForm_AnySingleCharacter:
                    return "任何字符";

                case XtraRichEditStringId.FindAndReplaceForm_ZeroOrMore:
                    return "零或更多";

                case XtraRichEditStringId.FindAndReplaceForm_OneOrMore:
                    return "一或更多";

                case XtraRichEditStringId.FindAndReplaceForm_BeginningOfLine:
                    return "开始于段落";

                case XtraRichEditStringId.FindAndReplaceForm_EndOfLine:
                    return "结束于段落";

                case XtraRichEditStringId.FindAndReplaceForm_BeginningOfWord:
                    return "开始于单词";

                case XtraRichEditStringId.FindAndReplaceForm_EndOfWord:
                    return "结束于单词";

                case XtraRichEditStringId.FindAndReplaceForm_AnyOneCharacterInTheSet:
                    return "重置任何一个字符";

                case XtraRichEditStringId.FindAndReplaceForm_AnyOneCharacterNotInTheSet:
                    return "不重置任何一个字符";

                case XtraRichEditStringId.FindAndReplaceForm_Or:
                    return "或";

                case XtraRichEditStringId.FindAndReplaceForm_EscapeSpecialCharacter:
                    return "转义字符";

                case XtraRichEditStringId.FindAndReplaceForm_TagExpression:
                    return "标签表达式";

                case XtraRichEditStringId.FindAndReplaceForm_WordCharacter:
                    return "单词";

                case XtraRichEditStringId.FindAndReplaceForm_SpaceOrTab:
                    return "空格或制表符";

                case XtraRichEditStringId.FindAndReplaceForm_Integer:
                    return "整数";

                case XtraRichEditStringId.FindAndReplaceForm_TaggedExpression:
                    return "标签表达式 {0}";

                case XtraRichEditStringId.FieldMapUniqueId:
                    return "唯一标识符";

                case XtraRichEditStringId.FieldMapTitle:
                    return "惯例标题";

                case XtraRichEditStringId.FieldMapFirstName:
                    return "名";

                case XtraRichEditStringId.FieldMapMiddleName:
                    return "名";

                case XtraRichEditStringId.FieldLastName:
                    return "姓";

                case XtraRichEditStringId.FieldMapSuffix:
                    return "后缀";

                case XtraRichEditStringId.FieldMapNickName:
                    return "昵称";

                case XtraRichEditStringId.FieldMapJobTitle:
                    return "职称";

                case XtraRichEditStringId.FieldMapCompany:
                    return "公司";

                case XtraRichEditStringId.FieldMapAddress1:
                    return "地址1";

                case XtraRichEditStringId.FieldMapAddress2:
                    return "地址2";

                case XtraRichEditStringId.FieldMapCity:
                    return "城市";

                case XtraRichEditStringId.FieldMapState:
                    return "省份";

                case XtraRichEditStringId.FieldMapPostalCode:
                    return "邮政编码";

                case XtraRichEditStringId.FieldMapCountry:
                    return "国家或地区";

                case XtraRichEditStringId.FieldMapBusinessPhone:
                    return "公司电话";

                case XtraRichEditStringId.FieldMapBusinessFax:
                    return "公司传真";

                case XtraRichEditStringId.FieldMapHomePhone:
                    return "家庭电话";

                case XtraRichEditStringId.FieldMapHomeFax:
                    return "家庭传真";

                case XtraRichEditStringId.FieldMapEMailAddress:
                    return "E-mail地址";

                case XtraRichEditStringId.FieldMapWebPage:
                    return "主页";

                case XtraRichEditStringId.FieldMapPartnerTitle:
                    return "配偶/合作伙伴";

                case XtraRichEditStringId.FieldMapPartnerFirstName:
                    return "配偶/合作伙伴名";

                case XtraRichEditStringId.FieldMapPartnerMiddleName:
                    return "配偶/合作伙伴名";

                case XtraRichEditStringId.FieldMapPartnerLastName:
                    return "配偶/合作伙伴姓";

                case XtraRichEditStringId.FieldMapPartnerNickName:
                    return "配偶/合作伙伴昵称";

                case XtraRichEditStringId.FieldMapPhoneticGuideFirstName:
                    return "名的拼音";

                case XtraRichEditStringId.FieldMapPhoneticGuideLastName:
                    return "姓的拼音";

                case XtraRichEditStringId.FieldMapAddress3:
                    return "地址3";

                case XtraRichEditStringId.FieldMapDepartment:
                    return "部门";

                case XtraRichEditStringId.TargetFrameDescription_Self:
                    return "同级框架";

                case XtraRichEditStringId.TargetFrameDescription_Blank:
                    return "新建窗口";

                case XtraRichEditStringId.TargetFrameDescription_Top:
                    return "整页";

                case XtraRichEditStringId.TargetFrameDescription_Parent:
                    return "上层框架";

                case XtraRichEditStringId.KeyName_Control:
                    return "Ctrl";

                case XtraRichEditStringId.KeyName_Shift:
                    return "Shift";

                case XtraRichEditStringId.KeyName_Alt:
                    return "Alt";

                case XtraRichEditStringId.Caption_NumberingListBoxNone:
                    return "无";

                case XtraRichEditStringId.Caption_PageHeader:
                    return "页眉";

                case XtraRichEditStringId.Caption_FirstPageHeader:
                    return "首页页眉";

                case XtraRichEditStringId.Caption_OddPageHeader:
                    return "奇数页页眉";

                case XtraRichEditStringId.Caption_EvenPageHeader:
                    return "偶数页页眉";

                case XtraRichEditStringId.Caption_PageFooter:
                    return "页脚";

                case XtraRichEditStringId.Caption_FirstPageFooter:
                    return "首页页脚";

                case XtraRichEditStringId.Caption_OddPageFooter:
                    return "奇数页页脚";

                case XtraRichEditStringId.Caption_EvenPageFooter:
                    return "偶数页页脚";

                case XtraRichEditStringId.Caption_SameAsPrevious:
                    return "与前一个相同";

                case XtraRichEditStringId.Caption_SectionPropertiesApplyToWholeDocument:
                    return "整篇文档";

                case XtraRichEditStringId.Caption_SectionPropertiesApplyToCurrentSection:
                    return "当前段落";

                case XtraRichEditStringId.Caption_SectionPropertiesApplyToSelectedSections:
                    return "所选段落";

                case XtraRichEditStringId.Caption_SectionPropertiesApplyThisPointForward:
                    return "插入点之前";

                case XtraRichEditStringId.Caption_PageSetupSectionStartContinuous:
                    return "接续本页";

                case XtraRichEditStringId.Caption_PageSetupSectionStartColumn:
                    return "新建栏";

                case XtraRichEditStringId.Caption_PageSetupSectionStartNextPage:
                    return "新建页";

                case XtraRichEditStringId.Caption_PageSetupSectionStartOddPage:
                    return "奇数页";

                case XtraRichEditStringId.Caption_PageSetupSectionStartEvenPage:
                    return "偶数页";

                case XtraRichEditStringId.Caption_HeightTypeExact:
                    return "固定值";

                case XtraRichEditStringId.Caption_HeightTypeMinimum:
                    return "最小值";

                case XtraRichEditStringId.Caption_UnitPercent:
                    return "百分比";

                case XtraRichEditStringId.Caption_UnitInches:
                    return "英寸";

                case XtraRichEditStringId.Caption_UnitCentimeters:
                    return "厘米";

                case XtraRichEditStringId.Caption_UnitMillimeters:
                    return "毫米";

                case XtraRichEditStringId.Caption_UnitPoints:
                    return "磅";

                case XtraRichEditStringId.Caption_CaptionPrefixEquation:
                    return "公式";

                case XtraRichEditStringId.Caption_CaptionPrefixFigure:
                    return "图表";

                case XtraRichEditStringId.Caption_CaptionPrefixTable:
                    return "表格";

                case XtraRichEditStringId.Caption_CurrentDocumentHyperlinkTooltip:
                    return "当前文档";

                case XtraRichEditStringId.Msg_InvalidNumberingListStartAtValue:
                    return "开始值必须在{0}和{1}之间";

                case XtraRichEditStringId.Msg_Loading:
                    return "加载...";

                case XtraRichEditStringId.Msg_Saving:
                    return "保存...";

                case XtraRichEditStringId.Msg_DuplicateBookmark:
                    return "书签名已经存在";

                case XtraRichEditStringId.Msg_NoDefaultTabs:
                    return "不能设置默认制表位。";

                case XtraRichEditStringId.Msg_CantResetDefaultProperties:
                    return "无法复位后的默认样式设置。";

                case XtraRichEditStringId.Msg_CantDeleteDefaultStyle:
                    return "不能删除默认的样式。";

                case XtraRichEditStringId.Msg_NoTocEntriesFound:
                    return "没有找到目录项。";

                case XtraRichEditStringId.Msg_NumberingListNotInListCollection:
                    return "不能使用编号列表。编号列表中必须添加到Document.NumberingLists集合";

                case XtraRichEditStringId.Msg_ParagraphStyleNameAlreadyExists:
                    return "此样式名称已经存在。";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_HorizontalPositionTypeMargin:
                    return "页边距";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_HorizontalPositionTypeCharacter:
                    return "字符";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_HorizontalPositionTypeColumn:
                    return "列";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_HorizontalPositionTypeInsideMargin:
                    return "内边距";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_HorizontalPositionTypeLeftMargin:
                    return "左边距";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_HorizontalPositionTypeOutsideMargin:
                    return "外边距";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_HorizontalPositionTypePage:
                    return "页";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_HorizontalPositionTypeRightMargin:
                    return "右边距";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_HorizontalPositionAlignmentCenter:
                    return "居中";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_HorizontalPositionAlignmentLeft:
                    return "左";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_HorizontalPositionAlignmentRight:
                    return "右";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionTypeMargin:
                    return "页边距";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionTypePage:
                    return "页";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionTypeLine:
                    return "线";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionTypeTopMargin:
                    return "上边距";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionTypeBottomMargin:
                    return "下边距";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionTypeInsideMargin:
                    return "内边距";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionTypeOutsideMargin:
                    return "外边距";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionTypeParagraph:
                    return "段落";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionAlignmentTop:
                    return "上";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionAlignmentCenter:
                    return "居中";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionAlignmentBottom:
                    return "下";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionAlignmentInside:
                    return "内";

                case XtraRichEditStringId.FloatingObjectLayoutOptionsForm_VerticalPositionAlignmentOutside:
                    return "外";

                case XtraRichEditStringId.Caption_PreviousParagraphText:
                    return "以上段落";

                case XtraRichEditStringId.Caption_CurrentParagraphText:
                    return "单一文本";

                case XtraRichEditStringId.Caption_FollowingParagraphText:
                    return "以下段落";

                case XtraRichEditStringId.Caption_EmptyParentStyle:
                    return "(含蓄的属性)";

                case XtraRichEditStringId.Caption_ParagraphAlignment_Left:
                    return "左对齐";

                case XtraRichEditStringId.Caption_ParagraphAlignment_Right:
                    return "右对齐";

                case XtraRichEditStringId.Caption_ParagraphAlignment_Center:
                    return "居中";

                case XtraRichEditStringId.Caption_ParagraphAlignment_Justify:
                    return "两端对齐";

                case XtraRichEditStringId.Caption_ParagraphFirstLineIndent_None:
                    return "无";

                case XtraRichEditStringId.Caption_ParagraphFirstLineIndent_Indented:
                    return "首行缩进";

                case XtraRichEditStringId.Caption_ParagraphFirstLineIndent_Hanging:
                    return "悬挂缩进";

                case XtraRichEditStringId.Caption_ParagraphLineSpacing_Single:
                    return "单倍行距";

                case XtraRichEditStringId.Caption_ParagraphLineSpacing_Sesquialteral:
                    return "1.5倍行距";

                case XtraRichEditStringId.Caption_ParagraphLineSpacing_Double:
                    return "2倍行距";

                case XtraRichEditStringId.Caption_ParagraphLineSpacing_Multiple:
                    return "多倍行距";

                case XtraRichEditStringId.Caption_ParagraphLineSpacing_Exactly:
                    return "固定值";

                case XtraRichEditStringId.Caption_ParagraphLineSpacing_AtLeast:
                    return "最小值";
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

