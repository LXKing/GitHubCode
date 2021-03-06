﻿namespace DevLocalization
{
    using DevExpress.XtraPrinting.Localization;
    using System;

    public class XtraPrintingLocalizer_zhchs : PreviewResLocalizer
    {
        public override string GetLocalizedString(PreviewStringId id)
        {
            switch (id)
            {
                case PreviewStringId.Button_Cancel:
                    return "取消";

                case PreviewStringId.Button_Ok:
                    return "确定";

                case PreviewStringId.Button_Help:
                    return "帮助";

                case PreviewStringId.Button_Apply:
                    return "应用";

                case PreviewStringId.PreviewForm_Caption:
                    return "预览";

                case PreviewStringId.TB_TTip_Customize:
                    return "定制";

                case PreviewStringId.TB_TTip_Print:
                    return "打印";

                case PreviewStringId.TB_TTip_PrintDirect:
                    return "快速打印";

                case PreviewStringId.TB_TTip_PageSetup:
                    return "页面调整";

                case PreviewStringId.TB_TTip_Magnifier:
                    return "放大镜";

                case PreviewStringId.TB_TTip_ZoomIn:
                    return "放大";

                case PreviewStringId.TB_TTip_ZoomOut:
                    return "缩小";

                case PreviewStringId.TB_TTip_Zoom:
                    return "缩放";

                case PreviewStringId.TB_TTip_Search:
                    return "搜索";

                case PreviewStringId.TB_TTip_FirstPage:
                    return "第一页";

                case PreviewStringId.TB_TTip_PreviousPage:
                    return "上一页";

                case PreviewStringId.TB_TTip_NextPage:
                    return "下一页";

                case PreviewStringId.TB_TTip_LastPage:
                    return "最后一页";

                case PreviewStringId.TB_TTip_MultiplePages:
                    return "显示多页";

                case PreviewStringId.TB_TTip_Backgr:
                    return "背景";

                case PreviewStringId.TB_TTip_Close:
                    return "关闭预览";

                case PreviewStringId.TB_TTip_EditPageHF:
                    return "页眉和页脚";

                case PreviewStringId.TB_TTip_HandTool:
                    return "抓取工具";

                case PreviewStringId.TB_TTip_Export:
                    return "输出文档...";

                case PreviewStringId.TB_TTip_Send:
                    return "通过电子邮件发送...";

                case PreviewStringId.TB_TTip_Map:
                    return "文档结构图";

                case PreviewStringId.TB_TTip_Parameters:
                    return "参数";

                case PreviewStringId.TB_TTip_Watermark:
                    return "水印";

                case PreviewStringId.TB_TTip_Scale:
                    return "比例";

                case PreviewStringId.TB_TTip_Open:
                    return "打开文件";

                case PreviewStringId.TB_TTip_Save:
                    return "保存文件";

                case PreviewStringId.MenuItem_PdfDocument:
                    return "PDF文件";

                case PreviewStringId.MenuItem_PageLayout:
                    return "页面布局";

                case PreviewStringId.MenuItem_TxtDocument:
                    return "文本文件";

                case PreviewStringId.MenuItem_GraphicDocument:
                    return "图象文件";

                case PreviewStringId.MenuItem_CsvDocument:
                    return "CSV文件";

                case PreviewStringId.MenuItem_MhtDocument:
                    return "MHT文件";

                case PreviewStringId.MenuItem_XlsDocument:
                    return "Excel文件";

                case PreviewStringId.MenuItem_XlsxDocument:
                    return "XLSX文件";

                case PreviewStringId.MenuItem_RtfDocument:
                    return "RTF文件";

                case PreviewStringId.MenuItem_HtmDocument:
                    return "HTML文件";

                case PreviewStringId.SaveDlg_FilterBmp:
                    return "BMP比特图格式";

                case PreviewStringId.SaveDlg_FilterGif:
                    return "GIF图形交换格式";

                case PreviewStringId.SaveDlg_FilterJpeg:
                    return "JPEG可交换文件格式";

                case PreviewStringId.SaveDlg_FilterPng:
                    return "PNG流式网络图形格式";

                case PreviewStringId.SaveDlg_FilterTiff:
                    return "TIFF标签图像文件格式";

                case PreviewStringId.SaveDlg_FilterEmf:
                    return "EMF 增强的视窗图元元件";

                case PreviewStringId.SaveDlg_FilterWmf:
                    return "WMF 视窗图元文件";

                //case PreviewStringId.SB_TotalPageNo:
                //    return "全部页码：";

                //case PreviewStringId.SB_CurrentPageNo:
                //    return "当前页码：";

                case PreviewStringId.SB_PageOfPages:
                    return "页码 {0} of {1}";

                case PreviewStringId.SB_ZoomFactor:
                    return "缩放比例：";

                case PreviewStringId.SB_PageNone:
                    return "空";

                case PreviewStringId.SB_PageInfo:
                    return "{0}在 {1}之中";

                case PreviewStringId.SB_TTip_Stop:
                    return "停止";

                case PreviewStringId.MPForm_Lbl_Pages:
                    return "页面";

                case PreviewStringId.Msg_EmptyDocument:
                    return "该文档不包含任何页。";

                case PreviewStringId.Msg_CreatingDocument:
                    return "建立文档...";

                case PreviewStringId.Msg_UnavailableNetPrinter:
                    return "网络打印机无法使用。";

                case PreviewStringId.Msg_NeedPrinter:
                    return "没有安装打印机。";

                case PreviewStringId.Msg_WrongPrinter:
                    return "打印机的名字是无效的。请检查打印机的设置。";

                case PreviewStringId.Msg_WrongPrinting:
                    return "打印错误.";

                case PreviewStringId.Msg_WrongPageSettings:
                    return "当前打印机不支持所选择页面大小。一定要继续吗？";

                case PreviewStringId.Msg_CustomDrawWarning:
                    return "警告！";

                case PreviewStringId.Msg_PageMarginsWarning:
                    return "一个或多个页边距被设置到也可打印的页面范围之外，是否要继续？";

                case PreviewStringId.Msg_IncorrectPageRange:
                    return "这不是有效的页面范围";

                case PreviewStringId.Msg_FontInvalidNumber:
                    return "字体大小不能设置为0或者负数。";

                case PreviewStringId.Msg_NotSupportedFont:
                    return "尚不支持该字体";

                case PreviewStringId.Msg_IncorrectZoomFactor:
                    return "数字大小必须界于{0}，{1}之间。";

                case PreviewStringId.Msg_InvalidMeasurement:
                    return "这不是一个有效的度量值。";

                case PreviewStringId.Msg_CannotAccessFile:
                    return "这个进程无法读取文件\"{0}\"，因为它正在被另一个进程使用。";

                case PreviewStringId.Msg_FileReadOnly:
                    return "文件\"{0}\"为只读，重试不同文件名";

                case PreviewStringId.Msg_OpenFileQuestion:
                    return "你想打开该文件吗？";

                case PreviewStringId.Msg_OpenFileQuestionCaption:
                    return "输出";

                case PreviewStringId.Msg_CantFitBarcodeToControlBounds:
                    return "对于条码来说控件的边界太小。";

                case PreviewStringId.Msg_InvalidBarcodeText:
                    return "文本中有无效字符。";

                case PreviewStringId.Msg_InvalidBarcodeTextFormat:
                    return "无效的文本格式";

                case PreviewStringId.Msg_InvalidBarcodeData:
                    return "二进制数据不能大于1033位";

                case PreviewStringId.Msg_SearchDialogFinishedSearching:
                    return "完成对整个文档的搜索";

                case PreviewStringId.Msg_SearchDialogTotalFound:
                    return "总共发现：";

                case PreviewStringId.Msg_SearchDialogReady:
                    return "准备就绪";

                case PreviewStringId.Msg_NoDifferentFilesInStream:
                    return "文件不能导出不同文件模式，请用单一文件模式或单页模式";

                case PreviewStringId.Msg_BigFileToCreate:
                    return "输出檔太大。设法减少页数，或分为若干檔。";

                case PreviewStringId.Msg_BigFileToCreateJPEG:
                    return "导出为JPEG文件太大，请选择其他导出格式";

                case PreviewStringId.Msg_BigBitmapToCreate:
                    return "导出文件太大，请缩小图象分辨率，\r\n或导出其他格式";

                case PreviewStringId.Msg_XlsMoreThanMaxRows:
                    return "导出的Excel文件太大，因为超过65536行。";

                case PreviewStringId.Msg_XlsMoreThanMaxColumns:
                    return "导出的Excel文件太大，因为超过256列。";

                case PreviewStringId.Msg_XlsxMoreThanMaxRows:
                    return "导出的Excel文件太大，因为超过1048576行。";

                case PreviewStringId.Msg_XlsxMoreThanMaxColumns:
                    return "导出的Excel文件太大，因为超过16348行。";

                case PreviewStringId.Msg_FileDoesNotHavePrnxExtention:
                    return "指定的文件不要有RPNX扩展，是否继续？";

                case PreviewStringId.Msg_FileDoesNotContainValidXml:
                    return "指定的文件不包含XML在PRNX格式，加载被停止。";

                case PreviewStringId.Msg_GoToNonExistentPage:
                    return "此文档没有{0}页";

                case PreviewStringId.Msg_Caption:
                    return "XtraPrinting";

                case PreviewStringId.Msg_PathTooLong:
                    return "此路径太长，请输入短名。";

                case PreviewStringId.Msg_CannotLoadDocument:
                    return "指定的文件不能被加载，因为它不包含有效的XML数据，或者超过了允许的大小。";

                case PreviewStringId.Msg_NoParameters:
                    return "指定的参数{0}不存在。";

                case PreviewStringId.Margin_Inch:
                    return "英寸";

                case PreviewStringId.Margin_Millimeter:
                    return "毫米";

                case PreviewStringId.Margin_TopMargin:
                    return "上页边距";

                case PreviewStringId.Margin_BottomMargin:
                    return "下页边距";

                case PreviewStringId.Margin_LeftMargin:
                    return "左页边距";

                case PreviewStringId.Margin_RightMargin:
                    return "右页边距";

                case PreviewStringId.Shapes_Rectangle:
                    return "矩形";

                case PreviewStringId.Shapes_Ellipse:
                    return "椭圆形";

                case PreviewStringId.Shapes_Arrow:
                    return "箭头";

                case PreviewStringId.Shapes_TopArrow:
                    return "上箭头";

                case PreviewStringId.Shapes_BottomArrow:
                    return "下箭头";

                case PreviewStringId.Shapes_LeftArrow:
                    return "左箭头";

                case PreviewStringId.Shapes_RightArrow:
                    return "右箭头";

                case PreviewStringId.Shapes_Polygon:
                    return "多边形";

                case PreviewStringId.Shapes_Triangle:
                    return "三角形";

                case PreviewStringId.Shapes_Square:
                    return "正方形";

                case PreviewStringId.Shapes_Pentagon:
                    return "五边形";

                case PreviewStringId.Shapes_Hexagon:
                    return "六边形";

                case PreviewStringId.Shapes_Octagon:
                    return "八边形";

                case PreviewStringId.Shapes_Star:
                    return "星形";

                case PreviewStringId.Shapes_ThreePointStar:
                    return "3点星";

                case PreviewStringId.Shapes_FourPointStar:
                    return "4点星";

                case PreviewStringId.Shapes_FivePointStar:
                    return "5点星";

                case PreviewStringId.Shapes_SixPointStar:
                    return "6点星";

                case PreviewStringId.Shapes_EightPointStar:
                    return "8点星";

                case PreviewStringId.Shapes_Line:
                    return "线";

                case PreviewStringId.Shapes_SlantLine:
                    return "斜线";

                case PreviewStringId.Shapes_BackslantLine:
                    return "反向斜线";

                case PreviewStringId.Shapes_HorizontalLine:
                    return "水平线";

                case PreviewStringId.Shapes_VerticalLine:
                    return "垂直线";

                case PreviewStringId.Shapes_Cross:
                    return "十字准线";

                case PreviewStringId.Shapes_Brace:
                    return "大括号";

                case PreviewStringId.Shapes_Bracket:
                    return "方括号";

                case PreviewStringId.ScrollingInfo_Page:
                    return "页面";

                case PreviewStringId.WMForm_PictureDlg_Title:
                    return "选择图片";

                case PreviewStringId.WMForm_ImageStretch:
                    return "拉伸";

                case PreviewStringId.WMForm_ImageClip:
                    return "裁剪";

                case PreviewStringId.WMForm_ImageZoom:
                    return "缩放";

                case PreviewStringId.WMForm_Watermark_Asap:
                    return "尽快";

                case PreviewStringId.WMForm_Watermark_Confidential:
                    return "机密";

                case PreviewStringId.WMForm_Watermark_Copy:
                    return "复制";

                case PreviewStringId.WMForm_Watermark_DoNotCopy:
                    return "不复制";

                case PreviewStringId.WMForm_Watermark_Draft:
                    return "草图";

                case PreviewStringId.WMForm_Watermark_Evaluation:
                    return "评价";

                case PreviewStringId.WMForm_Watermark_Original:
                    return "创新";

                case PreviewStringId.WMForm_Watermark_Personal:
                    return "个人";

                case PreviewStringId.WMForm_Watermark_Sample:
                    return "示例";

                case PreviewStringId.WMForm_Watermark_TopSecret:
                    return "最高机密";

                case PreviewStringId.WMForm_Watermark_Urgent:
                    return "紧迫";

                case PreviewStringId.WMForm_Direction_Horizontal:
                    return "水平的";

                case PreviewStringId.WMForm_Direction_Vertical:
                    return "垂直的";

                case PreviewStringId.WMForm_Direction_BackwardDiagonal:
                    return "后向倾斜";

                case PreviewStringId.WMForm_Direction_ForwardDiagonal:
                    return "前向倾斜";

                case PreviewStringId.WMForm_VertAlign_Bottom:
                    return "底部";

                case PreviewStringId.WMForm_VertAlign_Middle:
                    return "中间";

                case PreviewStringId.WMForm_VertAlign_Top:
                    return "上部";

                case PreviewStringId.WMForm_HorzAlign_Left:
                    return "左";

                case PreviewStringId.WMForm_HorzAlign_Center:
                    return "居中";

                case PreviewStringId.WMForm_HorzAlign_Right:
                    return "右";

                case PreviewStringId.WMForm_ZOrderRgrItem_InFront:
                    return "前方";

                case PreviewStringId.WMForm_ZOrderRgrItem_Behind:
                    return "后方";

                case PreviewStringId.WMForm_PageRangeRgrItem_All:
                    return "全部";

                case PreviewStringId.WMForm_PageRangeRgrItem_Pages:
                    return "页面";

                case PreviewStringId.SaveDlg_Title:
                    return "存储为";

                case PreviewStringId.SaveDlg_FilterPdf:
                    return "PDF文档";

                case PreviewStringId.SaveDlg_FilterHtm:
                    return "HTML文档";

                case PreviewStringId.SaveDlg_FilterMht:
                    return "MHT文档";

                case PreviewStringId.SaveDlg_FilterRtf:
                    return "多文本文档";

                case PreviewStringId.SaveDlg_FilterXls:
                    return "Excel文档";

                case PreviewStringId.SaveDlg_FilterXlsx:
                    return "XLSX文件";

                case PreviewStringId.SaveDlg_FilterCsv:
                    return "CSV文档";

                case PreviewStringId.SaveDlg_FilterTxt:
                    return "文本文档";

                case PreviewStringId.SaveDlg_FilterNativeFormat:
                    return "本地格式";

                case PreviewStringId.SaveDlg_FilterXps:
                    return "XPS文件";

                case PreviewStringId.MenuItem_File:
                    return "文件";

                case PreviewStringId.MenuItem_View:
                    return "视图";

                case PreviewStringId.MenuItem_Background:
                    return "背景...";

                case PreviewStringId.MenuItem_PageSetup:
                    return "页面调整";

                case PreviewStringId.MenuItem_Print:
                    return "打印...";

                case PreviewStringId.MenuItem_PrintDirect:
                    return "打印";

                case PreviewStringId.MenuItem_Export:
                    return "输出到";

                case PreviewStringId.MenuItem_Send:
                    return "以...格式发送";

                case PreviewStringId.MenuItem_Exit:
                    return "退出";

                case PreviewStringId.MenuItem_ViewToolbar:
                    return "工具条";

                case PreviewStringId.MenuItem_ViewStatusbar:
                    return "状态条";

                case PreviewStringId.MenuItem_ViewContinuous:
                    return "继续";

                case PreviewStringId.MenuItem_ViewFacing:
                    return "朝向";

                case PreviewStringId.MenuItem_BackgrColor:
                    return "颜色...";

                case PreviewStringId.MenuItem_Watermark:
                    return "水印...";

                case PreviewStringId.MenuItem_ZoomPageWidth:
                    return "页面宽度";

                case PreviewStringId.MenuItem_ZoomTextWidth:
                    return "文本宽度";

                case PreviewStringId.MenuItem_ZoomWholePage:
                    return "整个页面";

                case PreviewStringId.MenuItem_ZoomTwoPages:
                    return "两页";

                case PreviewStringId.PageInfo_PageNumber:
                    return "[页#]";

                case PreviewStringId.PageInfo_PageNumberOfTotal:
                    return "[页#，共#页]";

                case PreviewStringId.PageInfo_PageDate:
                    return "[已打印数据]";

                case PreviewStringId.PageInfo_PageTime:
                    return "[打印耗时]";

                case PreviewStringId.PageInfo_PageUserName:
                    return "[用户名]";

                case PreviewStringId.EMail_From:
                    return "来自";

                case PreviewStringId.BarText_Toolbar:
                    return "工具条";

                case PreviewStringId.BarText_MainMenu:
                    return "主菜单";

                case PreviewStringId.BarText_StatusBar:
                    return "状态条";

                case PreviewStringId.ScalePopup_GroupText:
                    return "缩放比例";

                case PreviewStringId.ScalePopup_AdjustTo:
                    return "调整至：";

                case PreviewStringId.ScalePopup_NormalSize:
                    return "%正常大小";

                case PreviewStringId.ScalePopup_FitTo:
                    return "调整到";

                case PreviewStringId.ScalePopup_PagesWide:
                    return "页面范围";

                case PreviewStringId.ExportOption_PdfPageRange:
                    return "页面范围：";

                case PreviewStringId.ExportOption_PdfConvertImagesToJpeg:
                    return "转换为jpeg";

                case PreviewStringId.ExportOption_PdfCompressed:
                    return "压缩";

                case PreviewStringId.ExportOption_PdfShowPrintDialogOnOpen:
                    return "显示打印对话框";

                case PreviewStringId.ExportOption_PdfNeverEmbeddedFonts:
                    return "不插入这些字体：";

                case PreviewStringId.ExportOption_PdfPasswordSecurityOptions:
                    return "密码安全:";

                case PreviewStringId.ExportOption_PdfImageQuality:
                    return "图象质量：";

                case PreviewStringId.ExportOption_PdfImageQuality_Lowest:
                    return "最低";

                case PreviewStringId.ExportOption_PdfImageQuality_Low:
                    return "低";

                case PreviewStringId.ExportOption_PdfImageQuality_Medium:
                    return "中等";

                case PreviewStringId.ExportOption_PdfImageQuality_High:
                    return "高";

                case PreviewStringId.ExportOption_PdfImageQuality_Highest:
                    return "最高";

                case PreviewStringId.ExportOption_PdfDocumentAuthor:
                    return "作者：";

                case PreviewStringId.ExportOption_PdfDocumentApplication:
                    return "应用：";

                case PreviewStringId.ExportOption_PdfDocumentTitle:
                    return "标题：";

                case PreviewStringId.ExportOption_PdfDocumentSubject:
                    return "主题：";

                case PreviewStringId.ExportOption_PdfDocumentKeywords:
                    return "关键字：";

                case PreviewStringId.ExportOption_PdfPrintingPermissions_None:
                    return "无";

                case PreviewStringId.ExportOption_PdfPrintingPermissions_LowResolution:
                    return "低分辨率(150dpi)";

                case PreviewStringId.ExportOption_PdfPrintingPermissions_HighResolution:
                    return "高分辨率";

                case PreviewStringId.ExportOption_PdfChangingPermissions_None:
                    return "无";

                case PreviewStringId.ExportOption_PdfChangingPermissions_InsertingDeletingRotating:
                    return "插入、删除和旋转页";

                case PreviewStringId.ExportOption_PdfChangingPermissions_FillingSigning:
                    return "从字段和签署现有的签名字段填充";

                case PreviewStringId.ExportOption_PdfChangingPermissions_CommentingFillingSigning:
                    return "从字段和签署现有的签名字段填充，批注。";

                case PreviewStringId.ExportOption_PdfChangingPermissions_AnyExceptExtractingPages:
                    return "除正在提取页外";

                case PreviewStringId.ExportOption_ConfirmOpenPasswordForm_Caption:
                    return "确认密码";

                case PreviewStringId.ExportOption_ConfirmOpenPasswordForm_Note:
                    return "请确认文件开启密码。一定要记下密码。这将需要打开该文档。";

                case PreviewStringId.ExportOption_ConfirmOpenPasswordForm_Name:
                    return "打开文件的密码(&A):";

                case PreviewStringId.ExportOption_ConfirmPermissionsPasswordForm_Caption:
                    return "确认密码";

                case PreviewStringId.ExportOption_ConfirmPermissionsPasswordForm_Note:
                    return "请确认权限密码。一定要记下密码。你将需要它在未来改变这些设置。";

                case PreviewStringId.ExportOption_ConfirmPermissionsPasswordForm_Name:
                    return "密码(&P)";

                case PreviewStringId.ExportOption_ConfirmationDoesNotMatchForm_Msg:
                    return "确认密码不匹配。请重新开始，并再次输入密码。";

                case PreviewStringId.ExportOption_HtmlExportMode:
                    return "输出模式：";

                case PreviewStringId.ExportOption_HtmlExportMode_SingleFile:
                    return "排成一列";

                case PreviewStringId.ExportOption_HtmlExportMode_SingleFilePageByPage:
                    return "逐页排成一列";

                case PreviewStringId.ExportOption_HtmlExportMode_DifferentFiles:
                    return "不同的文件";

                case PreviewStringId.ExportOption_HtmlCharacterSet:
                    return "个性化设置：";

                case PreviewStringId.ExportOption_HtmlTitle:
                    return "标题：";

                case PreviewStringId.ExportOption_HtmlRemoveSecondarySymbols:
                    return "删除回车";

                case PreviewStringId.ExportOption_HtmlEmbedImagesInHTML:
                    return "在HTML嵌入图像";

                case PreviewStringId.ExportOption_HtmlPageRange:
                    return "页面范围：";

                case PreviewStringId.ExportOption_HtmlPageBorderWidth:
                    return "页面边界宽度：";

                case PreviewStringId.ExportOption_HtmlPageBorderColor:
                    return "页面边界颜色：";

                case PreviewStringId.ExportOption_RtfExportMode:
                    return "导出方式:";

                case PreviewStringId.ExportOption_RtfExportMode_SingleFile:
                    return "单文件";

                case PreviewStringId.ExportOption_RtfExportMode_SingleFilePageByPage:
                    return "单文件页";

                case PreviewStringId.ExportOption_RtfPageRange:
                    return "页面范围:";

                case PreviewStringId.ExportOption_RtfExportWatermarks:
                    return "导出水印";

                case PreviewStringId.ExportOption_TextSeparator:
                    return "文本分隔器：";

                case PreviewStringId.ExportOption_TextSeparator_TabAlias:
                    return "TAB";

                case PreviewStringId.ExportOption_TextEncoding:
                    return "编码：";

                case PreviewStringId.ExportOption_TextQuoteStringsWithSeparators:
                    return "引用分隔符";

                case PreviewStringId.ExportOption_TextExportMode:
                    return "文本导出模式";

                case PreviewStringId.ExportOption_TextExportMode_Value:
                    return "值";

                case PreviewStringId.ExportOption_TextExportMode_Text:
                    return "文本";

                case PreviewStringId.ExportOption_XlsShowGridLines:
                    return "显示栅格线";

                case PreviewStringId.ExportOption_XlsUseNativeFormat:
                    return "以相应格式输出值";

                case PreviewStringId.ExportOption_XlsExportHyperlinks:
                    return "导出超链接";

                case PreviewStringId.ExportOption_XlsSheetName:
                    return "工作表名：";

                case PreviewStringId.ExportOption_XlsExportMode:
                    return "导出方式:";

                case PreviewStringId.ExportOption_XlsExportMode_SingleFile:
                    return "单文件";

                case PreviewStringId.ExportOption_XlsExportMode_DifferentFiles:
                    return "不同的文件";

                case PreviewStringId.ExportOption_XlsPageRange:
                    return "页面范围:";

                case PreviewStringId.ExportOption_XlsxExportMode:
                    return "导出方式:";

                case PreviewStringId.ExportOption_XlsxExportMode_SingleFile:
                    return "单文件";

                case PreviewStringId.ExportOption_XlsxExportMode_SingleFilePageByPage:
                    return "单文件页";

                case PreviewStringId.ExportOption_XlsxExportMode_DifferentFiles:
                    return "不同的文件";

                case PreviewStringId.ExportOption_XlsxPageRange:
                    return "页面范围:";

                case PreviewStringId.ExportOption_ImageExportMode:
                    return "导出方式:";

                case PreviewStringId.ExportOption_ImageExportMode_SingleFile:
                    return "单文件";

                case PreviewStringId.ExportOption_ImageExportMode_SingleFilePageByPage:
                    return "单文件页";

                case PreviewStringId.ExportOption_ImageExportMode_DifferentFiles:
                    return "不同的文件";

                case PreviewStringId.ExportOption_ImagePageRange:
                    return "页面范围:";

                case PreviewStringId.ExportOption_ImagePageBorderWidth:
                    return "页边框宽度:";

                case PreviewStringId.ExportOption_ImagePageBorderColor:
                    return "页边框颜色:";

                case PreviewStringId.ExportOption_ImageFormat:
                    return "图象格式：";

                case PreviewStringId.ExportOption_ImageResolution:
                    return "分辨率:";

                case PreviewStringId.ExportOption_NativeFormatCompressed:
                    return "压缩的";

                case PreviewStringId.ExportOption_XpsPageRange:
                    return "页面范围";

                case PreviewStringId.ExportOption_XpsCompression:
                    return "压缩:";

                case PreviewStringId.ExportOption_XpsCompression_NotCompressed:
                    return "不压缩";

                case PreviewStringId.ExportOption_XpsCompression_Normal:
                    return "常规";

                case PreviewStringId.ExportOption_XpsCompression_Maximum:
                    return "最大值";

                case PreviewStringId.ExportOption_XpsCompression_Fast:
                    return "快速";

                case PreviewStringId.ExportOption_XpsCompression_SuperFast:
                    return "超快速";

                case PreviewStringId.ExportOption_XpsDocumentCreator:
                    return "创建者:";

                case PreviewStringId.ExportOption_XpsDocumentCategory:
                    return "类别:";

                case PreviewStringId.ExportOption_XpsDocumentTitle:
                    return "标题:";

                case PreviewStringId.ExportOption_XpsDocumentSubject:
                    return "主题:";

                case PreviewStringId.ExportOption_XpsDocumentKeywords:
                    return "关键字:";

                case PreviewStringId.ExportOption_XpsDocumentVersion:
                    return "版本:";

                case PreviewStringId.ExportOption_XpsDocumentDescription:
                    return "描述:";

                case PreviewStringId.FolderBrowseDlg_ExportDirectory:
                    return "选择一个文件夹保存输出文档：";

                case PreviewStringId.ExportOptionsForm_CaptionPdf:
                    return "Pdf输出选项";

                case PreviewStringId.ExportOptionsForm_CaptionXls:
                    return "Xls输出选项";

                case PreviewStringId.ExportOptionsForm_CaptionXlsx:
                    return "导出XLSX选项";

                case PreviewStringId.ExportOptionsForm_CaptionTxt:
                    return "文本输出选项";

                case PreviewStringId.ExportOptionsForm_CaptionCsv:
                    return "Csv输出选项";

                case PreviewStringId.ExportOptionsForm_CaptionImage:
                    return "图象输出选项";

                case PreviewStringId.ExportOptionsForm_CaptionHtml:
                    return "Html输出选项";

                case PreviewStringId.ExportOptionsForm_CaptionMht:
                    return "Mht输出选项";

                case PreviewStringId.ExportOptionsForm_CaptionRtf:
                    return "Rtf输出选项";

                case PreviewStringId.ExportOptionsForm_CaptionNativeOptions:
                    return "本地格式选项";

                case PreviewStringId.ExportOptionsForm_CaptionXps:
                    return "导出XPS选项";

                case PreviewStringId.RibbonPreview_PageText:
                    return "打印预览";

                case PreviewStringId.RibbonPreview_PageGroup_Print:
                    return "打印";

                case PreviewStringId.RibbonPreview_PageGroup_PageSetup:
                    return "页面调整";

                case PreviewStringId.RibbonPreview_PageGroup_Navigation:
                    return "导航";

                case PreviewStringId.RibbonPreview_PageGroup_Zoom:
                    return "缩放";

                case PreviewStringId.RibbonPreview_PageGroup_Background:
                    return "页面背景";

                case PreviewStringId.RibbonPreview_PageGroup_Export:
                    return "输出";

                case PreviewStringId.RibbonPreview_PageGroup_Document:
                    return "文件";

                case PreviewStringId.RibbonPreview_DocumentMap_Caption:
                    return "书签";

                case PreviewStringId.RibbonPreview_Parameters_Caption:
                    return "参数";

                case PreviewStringId.RibbonPreview_Find_Caption:
                    return "查找";

                case PreviewStringId.RibbonPreview_Pointer_Caption:
                    return "指针";

                case PreviewStringId.RibbonPreview_HandTool_Caption:
                    return "抓取工具";

                case PreviewStringId.RibbonPreview_Customize_Caption:
                    return "选项";

                case PreviewStringId.RibbonPreview_Print_Caption:
                    return "打印";

                case PreviewStringId.RibbonPreview_PrintDirect_Caption:
                    return "快速打印";

                case PreviewStringId.RibbonPreview_PageSetup_Caption:
                    return "定制页边距...";

                case PreviewStringId.RibbonPreview_EditPageHF_Caption:
                    return "页眉／页脚";

                case PreviewStringId.RibbonPreview_Magnifier_Caption:
                    return "放大";

                case PreviewStringId.RibbonPreview_ZoomOut_Caption:
                    return "缩小";

                case PreviewStringId.RibbonPreview_ZoomExact_Caption:
                    return "精确度：";

                case PreviewStringId.RibbonPreview_ZoomIn_Caption:
                    return "放大";

                case PreviewStringId.RibbonPreview_ShowFirstPage_Caption:
                    return "第一页";

                case PreviewStringId.RibbonPreview_ShowPrevPage_Caption:
                    return "上一页";

                case PreviewStringId.RibbonPreview_ShowNextPage_Caption:
                    return "下一页";

                case PreviewStringId.RibbonPreview_ShowLastPage_Caption:
                    return "最后一页";

                case PreviewStringId.RibbonPreview_MultiplePages_Caption:
                    return "多页";

                case PreviewStringId.RibbonPreview_FillBackground_Caption:
                    return "页面颜色";

                case PreviewStringId.RibbonPreview_Watermark_Caption:
                    return "水印";

                case PreviewStringId.RibbonPreview_ExportFile_Caption:
                    return "输出";

                case PreviewStringId.RibbonPreview_SendFile_Caption:
                    return "在电子邮件中以...格式发送";

                case PreviewStringId.RibbonPreview_ClosePreview_Caption:
                    return "关闭打印预览";

                case PreviewStringId.RibbonPreview_Scale_Caption:
                    return "比例";

                case PreviewStringId.RibbonPreview_PageOrientation_Caption:
                    return "方位";

                case PreviewStringId.RibbonPreview_PaperSize_Caption:
                    return "大小";

                case PreviewStringId.RibbonPreview_PageMargins_Caption:
                    return "页边距";

                case PreviewStringId.RibbonPreview_Zoom_Caption:
                    return "缩放";

                case PreviewStringId.RibbonPreview_Save_Caption:
                    return "保存";

                case PreviewStringId.RibbonPreview_Open_Caption:
                    return "打开";

                case PreviewStringId.RibbonPreview_DocumentMap_STipTitle:
                    return "文档结构图";

                case PreviewStringId.RibbonPreview_Parameters_STipTitle:
                    return "参数";

                case PreviewStringId.RibbonPreview_Find_STipTitle:
                    return "查找";

                case PreviewStringId.RibbonPreview_Pointer_STipTitle:
                    return "鼠标指针";

                case PreviewStringId.RibbonPreview_HandTool_STipTitle:
                    return "抓取工具";

                case PreviewStringId.RibbonPreview_Customize_STipTitle:
                    return "选项";

                case PreviewStringId.RibbonPreview_Print_STipTitle:
                    return "打印(Ctrl+P)";

                case PreviewStringId.RibbonPreview_PrintDirect_STipTitle:
                    return "快速打印";

                case PreviewStringId.RibbonPreview_PageSetup_STipTitle:
                    return "页面调整";

                case PreviewStringId.RibbonPreview_EditPageHF_STipTitle:
                    return "页眉和页脚";

                case PreviewStringId.RibbonPreview_Magnifier_STipTitle:
                    return "放大镜";

                case PreviewStringId.RibbonPreview_ZoomOut_STipTitle:
                    return "缩小";

                case PreviewStringId.RibbonPreview_ZoomIn_STipTitle:
                    return "放大";

                case PreviewStringId.RibbonPreview_ShowFirstPage_STipTitle:
                    return "第一页(Ctrl+Home)";

                case PreviewStringId.RibbonPreview_ShowPrevPage_STipTitle:
                    return "上一页(PageUp)";

                case PreviewStringId.RibbonPreview_ShowNextPage_STipTitle:
                    return "下一页(PageDown)";

                case PreviewStringId.RibbonPreview_ShowLastPage_STipTitle:
                    return "最后一页(Ctrl+End)";

                case PreviewStringId.RibbonPreview_MultiplePages_STipTitle:
                    return "多页查看";

                case PreviewStringId.RibbonPreview_FillBackground_STipTitle:
                    return "背景颜色";

                case PreviewStringId.RibbonPreview_Watermark_STipTitle:
                    return "水印";

                case PreviewStringId.RibbonPreview_ExportFile_STipTitle:
                    return "输出...";

                case PreviewStringId.RibbonPreview_SendFile_STipTitle:
                    return "在电子邮件中以...格式发送";

                case PreviewStringId.RibbonPreview_ClosePreview_STipTitle:
                    return "关闭打印预览";

                case PreviewStringId.RibbonPreview_Scale_STipTitle:
                    return "比例";

                case PreviewStringId.RibbonPreview_PageOrientation_STipTitle:
                    return "页面方向";

                case PreviewStringId.RibbonPreview_PaperSize_STipTitle:
                    return "页面大小";

                case PreviewStringId.RibbonPreview_PageMargins_STipTitle:
                    return "页边距";

                case PreviewStringId.RibbonPreview_Zoom_STipTitle:
                    return "缩放";

                case PreviewStringId.RibbonPreview_PageGroup_PageSetup_STipTitle:
                    return "页面调整";

                case PreviewStringId.RibbonPreview_Save_STipTitle:
                    return "保存(Ctrl + S)";

                case PreviewStringId.RibbonPreview_Open_STipTitle:
                    return "打开(Ctrl + O)";

                case PreviewStringId.RibbonPreview_DocumentMap_STipContent:
                    return "打开文档结构图为你导航文档的结构。";

                case PreviewStringId.RibbonPreview_Parameters_STipContent:
                    return "打开参数窗格";

                case PreviewStringId.RibbonPreview_Find_STipContent:
                    return "显示查找对话框，查找文档中的文本。";

                case PreviewStringId.RibbonPreview_Pointer_STipContent:
                    return "显示鼠标指针。";

                case PreviewStringId.RibbonPreview_HandTool_STipContent:
                    return "调用抓取工具手动拖拽查看页面。";

                case PreviewStringId.RibbonPreview_Customize_STipContent:
                    return "打开可打印的组件编辑器对话框，并可以改变打印选项。";

                case PreviewStringId.RibbonPreview_Print_STipContent:
                    return "在打印前选择打印机，打印份数以及其他打印选项。";

                case PreviewStringId.RibbonPreview_PrintDirect_STipContent:
                    return "将文档不作任何修改直接送往默认打印机。";

                case PreviewStringId.RibbonPreview_PageSetup_STipContent:
                    return "显示页面调整对话框。";

                case PreviewStringId.RibbonPreview_EditPageHF_STipContent:
                    return "编辑该文档的页眉和页脚";

                case PreviewStringId.RibbonPreview_Magnifier_STipContent:
                    return "调用放大镜工具";

                case PreviewStringId.RibbonPreview_ZoomOut_STipContent:
                    return "缩小以便在一个减小的尺寸上看到页面的更多部分。";

                case PreviewStringId.RibbonPreview_ZoomIn_STipContent:
                    return "放大以便得到文档的近视图。";

                case PreviewStringId.RibbonPreview_ShowFirstPage_STipContent:
                    return "查看文档第一页。";

                case PreviewStringId.RibbonPreview_ShowPrevPage_STipContent:
                    return "查看文档上一页。";

                case PreviewStringId.RibbonPreview_ShowNextPage_STipContent:
                    return "查看文档下一页。";

                case PreviewStringId.RibbonPreview_ShowLastPage_STipContent:
                    return "查看文档最后一页。";

                case PreviewStringId.RibbonPreview_MultiplePages_STipContent:
                    return "选择页面布局以便在预览中排放文档页面。";

                case PreviewStringId.RibbonPreview_FillBackground_STipContent:
                    return "为文档页面背景选择颜色。";

                case PreviewStringId.RibbonPreview_Watermark_STipContent:
                    return "在页面的目录后插入文本或者图象的镜象。这通常用于指示一个文档被特殊处理过。";

                case PreviewStringId.RibbonPreview_ExportFile_STipContent:
                    return "将当前文档以一个可用的格式输出，并将其保存到磁盘文件上。";

                case PreviewStringId.RibbonPreview_SendFile_STipContent:
                    return "以一种可用格式输出当前文档，并且将其附到电子邮件中。";

                case PreviewStringId.RibbonPreview_ClosePreview_STipContent:
                    return "关闭该文档的打印预览";

                case PreviewStringId.RibbonPreview_Scale_STipContent:
                    return "按实际大小的百分比伸展或收缩打印输出。";

                case PreviewStringId.RibbonPreview_PageOrientation_STipContent:
                    return "在纵向和横向布局之间转换页面。";

                case PreviewStringId.RibbonPreview_PaperSize_STipContent:
                    return "选择文档的页面大小。";

                case PreviewStringId.RibbonPreview_PageMargins_STipContent:
                    return "为整个文档选择页边距大小。点击定制页边距为文档应用指定的页边距大小。";

                case PreviewStringId.RibbonPreview_Zoom_STipContent:
                    return "改变文档预览的缩放等级。";

                case PreviewStringId.RibbonPreview_PageGroup_PageSetup_STipContent:
                    return "显示页面调整对话框。";

                case PreviewStringId.RibbonPreview_Save_STipContent:
                    return "保存文件";

                case PreviewStringId.RibbonPreview_Open_STipContent:
                    return "打开文件";

                case PreviewStringId.RibbonPreview_ExportPdf_Caption:
                    return "PDF文件";

                case PreviewStringId.RibbonPreview_ExportHtm_Caption:
                    return "HTML文件";

                case PreviewStringId.RibbonPreview_ExportMht_Caption:
                    return "MHT文件";

                case PreviewStringId.RibbonPreview_ExportRtf_Caption:
                    return "RTF文件";

                case PreviewStringId.RibbonPreview_ExportXls_Caption:
                    return "Excel文件";

                case PreviewStringId.RibbonPreview_ExportXlsx_Caption:
                    return "XLSX文件";

                case PreviewStringId.RibbonPreview_ExportCsv_Caption:
                    return "CSV文件";

                case PreviewStringId.RibbonPreview_ExportTxt_Caption:
                    return "文本文件";

                case PreviewStringId.RibbonPreview_ExportGraphic_Caption:
                    return "图象文件";

                case PreviewStringId.RibbonPreview_ExportXps_Caption:
                    return "XPS文件";

                case PreviewStringId.RibbonPreview_SendPdf_Caption:
                    return "PDF文件";

                case PreviewStringId.RibbonPreview_SendMht_Caption:
                    return "MHT文件";

                case PreviewStringId.RibbonPreview_SendRtf_Caption:
                    return "RTF文件";

                case PreviewStringId.RibbonPreview_SendXls_Caption:
                    return "Excel文件";

                case PreviewStringId.RibbonPreview_SendXlsx_Caption:
                    return "XLSX文件";

                case PreviewStringId.RibbonPreview_SendCsv_Caption:
                    return "CSV文件";

                case PreviewStringId.RibbonPreview_SendTxt_Caption:
                    return "文本文件";

                case PreviewStringId.RibbonPreview_SendGraphic_Caption:
                    return "图象文件";

                case PreviewStringId.RibbonPreview_SendXps_Caption:
                    return "XPS文件";

                case PreviewStringId.RibbonPreview_ExportPdf_Description:
                    return "Adobe便携式文档格式";

                case PreviewStringId.RibbonPreview_ExportHtm_Description:
                    return "Web页面";

                case PreviewStringId.RibbonPreview_ExportTxt_Description:
                    return "纯文本";

                case PreviewStringId.RibbonPreview_ExportCsv_Description:
                    return "逗号分隔值文本";

                case PreviewStringId.RibbonPreview_ExportMht_Description:
                    return "单一文件的Web页";

                case PreviewStringId.RibbonPreview_ExportXls_Description:
                    return "Microsoft Excel工作薄";

                case PreviewStringId.RibbonPreview_ExportXlsx_Description:
                    return "Microsoft Excel 2007 Workbook";

                case PreviewStringId.RibbonPreview_ExportRtf_Description:
                    return "多本文格式";

                case PreviewStringId.RibbonPreview_ExportGraphic_Description:
                    return "BMP, GIF, JPEG, PNG, TIFF, EMF, WMF";

                case PreviewStringId.RibbonPreview_ExportXps_Description:
                    return "XPS";

                case PreviewStringId.RibbonPreview_SendPdf_Description:
                    return "Adobe便携式文档格式";

                case PreviewStringId.RibbonPreview_SendTxt_Description:
                    return "纯文本";

                case PreviewStringId.RibbonPreview_SendCsv_Description:
                    return "逗号分隔值文本";

                case PreviewStringId.RibbonPreview_SendMht_Description:
                    return "单文件网页";

                case PreviewStringId.RibbonPreview_SendXls_Description:
                    return "Microsoft Excel工作薄";

                case PreviewStringId.RibbonPreview_SendXlsx_Description:
                    return "Microsoft Excel 2007 Workbook";

                case PreviewStringId.RibbonPreview_SendRtf_Description:
                    return "多文本格式";

                case PreviewStringId.RibbonPreview_SendGraphic_Description:
                    return "BMP, GIF, JPEG, PNG, TIFF, EMF, WMF";

                case PreviewStringId.RibbonPreview_SendXps_Description:
                    return "XPS";

                case PreviewStringId.RibbonPreview_ExportPdf_STipTitle:
                    return "以PDF格式输出";

                case PreviewStringId.RibbonPreview_ExportHtm_STipTitle:
                    return "以HTML格式输出";

                case PreviewStringId.RibbonPreview_ExportTxt_STipTitle:
                    return "以文本格式输出";

                case PreviewStringId.RibbonPreview_ExportCsv_STipTitle:
                    return "以CSV格式输出";

                case PreviewStringId.RibbonPreview_ExportMht_STipTitle:
                    return "以MHT格式输出";

                case PreviewStringId.RibbonPreview_ExportXls_STipTitle:
                    return "以XLS格式输出";

                case PreviewStringId.RibbonPreview_ExportXlsx_STipTitle:
                    return "导出为XLSX";

                case PreviewStringId.RibbonPreview_ExportRtf_STipTitle:
                    return "以RTF格式输出";

                case PreviewStringId.RibbonPreview_ExportGraphic_STipTitle:
                    return "以图象格式输出";

                case PreviewStringId.RibbonPreview_SendPdf_STipTitle:
                    return "在电子邮件中以PDF格式发送";

                case PreviewStringId.RibbonPreview_SendTxt_STipTitle:
                    return "在电子邮件中以文本格式发送";

                case PreviewStringId.RibbonPreview_SendCsv_STipTitle:
                    return "在电子邮件中以CSV格式发送";

                case PreviewStringId.RibbonPreview_SendMht_STipTitle:
                    return "在电子邮件中以MHT格式发送";

                case PreviewStringId.RibbonPreview_SendXls_STipTitle:
                    return "在电子邮件中以XLS格式发送";

                case PreviewStringId.RibbonPreview_SendXlsx_STipTitle:
                    return "以XLSX发送E-Mail";

                case PreviewStringId.RibbonPreview_SendRtf_STipTitle:
                    return "在电子邮件中以RTF格式发送";

                case PreviewStringId.RibbonPreview_SendGraphic_STipTitle:
                    return "在电子邮件中以图象格式发送";

                case PreviewStringId.RibbonPreview_ExportPdf_STipContent:
                    return "将该文档以PDF格式输出并保存到磁盘文件上。";

                case PreviewStringId.RibbonPreview_ExportHtm_STipContent:
                    return "将该文档以HTML格式输出并保存到磁盘文件上。";

                case PreviewStringId.RibbonPreview_ExportTxt_STipContent:
                    return "将该文档以文本格式输出并保存到磁盘文件上。";

                case PreviewStringId.RibbonPreview_ExportCsv_STipContent:
                    return "将该文档以CSV格式输出并保存到磁盘文件上。";

                case PreviewStringId.RibbonPreview_ExportMht_STipContent:
                    return "将该文档以MHT格式输出并保存到磁盘文件上。";

                case PreviewStringId.RibbonPreview_ExportXls_STipContent:
                    return "将该文档以XLS格式输出并保存到磁盘文件上。";

                case PreviewStringId.RibbonPreview_ExportXlsx_STipContent:
                    return "导出XLSX文件并保存";

                case PreviewStringId.RibbonPreview_ExportRtf_STipContent:
                    return "将该文档以RTF格式输出并保存到磁盘文件上。";

                case PreviewStringId.RibbonPreview_ExportGraphic_STipContent:
                    return "将该文档以图象格式输出并保存到磁盘文件上。";

                case PreviewStringId.RibbonPreview_SendPdf_STipContent:
                    return "以PDF格式输出文档，并且将其附到电子邮件中。";

                case PreviewStringId.RibbonPreview_SendTxt_STipContent:
                    return "以文本格式输出文档，并且将其附到电子邮件中。";

                case PreviewStringId.RibbonPreview_SendCsv_STipContent:
                    return "以CSV格式输出文档，并且将其附到电子邮件中。";

                case PreviewStringId.RibbonPreview_SendMht_STipContent:
                    return "以MHT格式输出文档，并且将其附到电子邮件中。";

                case PreviewStringId.RibbonPreview_SendXls_STipContent:
                    return "以XLS格式输出文档，并且将其附到电子邮件中。";

                case PreviewStringId.RibbonPreview_SendXlsx_STipContent:
                    return "导出XLSX文件并发送e-mail";

                case PreviewStringId.RibbonPreview_SendRtf_STipContent:
                    return "以RTF格式输出文档，并且将其附到电子邮件中。";

                case PreviewStringId.RibbonPreview_SendGraphic_STipContent:
                    return "以图象格式输出文档，并且将其附到电子邮件中。";

                case PreviewStringId.RibbonPreview_GalleryItem_PageOrientationPortrait_Caption:
                    return "纵向";

                case PreviewStringId.RibbonPreview_GalleryItem_PageOrientationLandscape_Caption:
                    return "横向";

                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsNormal_Caption:
                    return "正常";

                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsNarrow_Caption:
                    return "窄";

                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsModerate_Caption:
                    return "中等";

                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsWide_Caption:
                    return "宽";

                case PreviewStringId.RibbonPreview_GalleryItem_PageOrientationPortrait_Description:
                    return " ";

                case PreviewStringId.RibbonPreview_GalleryItem_PageOrientationLandscape_Description:
                    return " ";

                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsNormal_Description:
                    return "正常";

                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsNarrow_Description:
                    return "窄";

                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsModerate_Description:
                    return "中等";

                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsWide_Description:
                    return "宽";

                case PreviewStringId.RibbonPreview_GalleryItem_PageMargins_Description:
                    return "上:\t\t{0}\t\t下:\t\t{1}\r\n左:\t\t {2}\t\t右:\t\t   {3}";

                case PreviewStringId.RibbonPreview_GalleryItem_PaperSize_Description:
                    return "{0} x {1}";

                case PreviewStringId.OpenFileDialog_Filter:
                    return "预览文件 (*{0})|*{0}|所有文件 (*.*)|*.*";

                case PreviewStringId.OpenFileDialog_Title:
                    return "打开";

                case PreviewStringId.ExportOption_PdfPasswordSecurityOptions_DocumentOpenPassword:
                    return "文件打开密码";

                case PreviewStringId.ExportOption_PdfPasswordSecurityOptions_Permissions:
                    return "权限";

                case PreviewStringId.ExportOption_PdfPasswordSecurityOptions_None:
                    return "(无)";

                case PreviewStringId.ParametersRequest_Submit:
                    return "提交";

                case PreviewStringId.ParametersRequest_Reset:
                    return "重置";

                case PreviewStringId.ParametersRequest_Caption:
                    return "参数";

                case PreviewStringId.NoneString:
                    return "(无)";

                case PreviewStringId.WatermarkTypePicture:
                    return "(图片)";

                case PreviewStringId.WatermarkTypeText:
                    return "(文本)";
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

