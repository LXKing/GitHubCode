﻿namespace DevLocalization
{
    using DevExpress.XtraGrid.Localization;
    using System;

    public class GridLocalizer_zhchs : GridResLocalizer
    {
        public override string GetLocalizedString(GridStringId id)
        {
            switch (id)
            {
                case GridStringId.FileIsNotFoundError:
                    return "文件{0}找不到";

                case GridStringId.ColumnViewExceptionMessage:
                    return " 要修正当前值吗?";

                case GridStringId.CustomizationCaption:
                    return "自定义";

                case GridStringId.CustomizationColumns:
                    return "列";

                case GridStringId.CustomizationBands:
                    return "带宽";

                case GridStringId.FilterPanelCustomizeButton:
                    return "自定义";

                case GridStringId.PopupFilterAll:
                    return "(全部)";

                case GridStringId.PopupFilterCustom:
                    return "(自定义)";

                case GridStringId.PopupFilterBlanks:
                    return "(空白)";

                case GridStringId.PopupFilterNonBlanks:
                    return "(无空白)";

                case GridStringId.CustomFilterDialogFormCaption:
                    return "用户自定义自动过滤器";

                case GridStringId.CustomFilterDialogCaption:
                    return "显示符合下列条件的行:";

                case GridStringId.CustomFilterDialogRadioAnd:
                    return "与(&A)";

                case GridStringId.CustomFilterDialogRadioOr:
                    return "或(&O)";

                case GridStringId.CustomFilterDialogOkButton:
                    return "确定(&O)";

                case GridStringId.CustomFilterDialogClearFilter:
                    return "清除过滤器(&L)";

                case GridStringId.CustomFilterDialog2FieldCheck:
                    return "字段";

                case GridStringId.CustomFilterDialogCancelButton:
                    return "取消(&C)";

                case GridStringId.CustomFilterDialogEmptyValue:
                    return "(输入一个值)";

                case GridStringId.CustomFilterDialogEmptyOperator:
                    return "(选择一个操作)";

                case GridStringId.CustomFilterDialogHint:
                    return "用 _ 替代一个单字符#用 % 替代其他任何类型的字符";

                case GridStringId.WindowErrorCaption:
                    return "错误";

                case GridStringId.MenuFooterSum:
                    return "和";

                case GridStringId.MenuFooterMin:
                    return "最小值";

                case GridStringId.MenuFooterMax:
                    return "最大值";

                case GridStringId.MenuFooterCount:
                    return "计数";

                case GridStringId.MenuFooterAverage:
                    return "平均值";

                case GridStringId.MenuFooterNone:
                    return "无";

                case GridStringId.MenuFooterSumFormat:
                    return "和={0:#.##}";

                case GridStringId.MenuFooterMinFormat:
                    return "最小值={0}";

                case GridStringId.MenuFooterMaxFormat:
                    return "最大值={0}";

                case GridStringId.MenuFooterCountFormat:
                    return "{0}";

                case GridStringId.MenuFooterAverageFormat:
                    return "平均={0:#.##}";

                case GridStringId.MenuColumnSortAscending:
                    return "升序排列";

                case GridStringId.MenuColumnSortDescending:
                    return "降序排列";

                case GridStringId.MenuColumnShowColumn:
                    return "显示列";

                case GridStringId.MenuColumnRemoveColumn:
                    return "移除列";

                case GridStringId.MenuColumnGroup:
                    return "根据此列分组";

                case GridStringId.MenuColumnUnGroup:
                    return "不分组";

                case GridStringId.MenuColumnColumnCustomization:
                    return "列选择";

                case GridStringId.MenuColumnBandCustomization:
                    return "显示/隐藏字段/区";

                case GridStringId.MenuColumnBestFit:
                    return "最佳匹配";

                case GridStringId.MenuColumnFilter:
                    return "允许筛选数据";

                case GridStringId.MenuColumnClearFilter:
                    return "清除过滤器";

                case GridStringId.MenuColumnBestFitAllColumns:
                    return "最佳匹配(所有列)";

                case GridStringId.MenuColumnResetGroupSummarySort:
                    return "清除摘要排序";

                case GridStringId.MenuColumnGroupSummarySortFormat:
                    return "{1} 依照 - '{0}' - {2}";

                case GridStringId.MenuColumnSumSummaryTypeDescription:
                    return "总计";

                case GridStringId.MenuColumnMinSummaryTypeDescription:
                    return "最小";

                case GridStringId.MenuColumnMaxSummaryTypeDescription:
                    return "最大";

                case GridStringId.MenuColumnCountSummaryTypeDescription:
                    return "计数";

                case GridStringId.MenuColumnAverageSummaryTypeDescription:
                    return "平均";

                case GridStringId.MenuColumnCustomSummaryTypeDescription:
                    return "自定义";

                case GridStringId.MenuColumnSortGroupBySummaryMenu:
                    return "按摘要排序";

                case GridStringId.MenuColumnGroupIntervalMenu:
                    return "分组的组段";

                case GridStringId.MenuColumnGroupIntervalNone:
                    return "无";

                case GridStringId.MenuColumnGroupIntervalDay:
                    return "天";

                case GridStringId.MenuColumnGroupIntervalMonth:
                    return "月";

                case GridStringId.MenuColumnGroupIntervalYear:
                    return "年";

                case GridStringId.MenuColumnGroupIntervalSmart:
                    return "灵巧";

                case GridStringId.MenuColumnGroupSummaryEditor:
                    return "分组汇总编辑器...";

                case GridStringId.MenuColumnExpressionEditor:
                    return "表达式编辑器";

                case GridStringId.MenuColumnFilterMode:
                    return "过滤模式";

                case GridStringId.MenuColumnFilterModeValue:
                    return "值";

                case GridStringId.MenuColumnFilterModeDisplayText:
                    return "显示文本";

                case GridStringId.MenuGroupPanelFullExpand:
                    return "全部展开";

                case GridStringId.MenuGroupPanelFullCollapse:
                    return "全部收合";

                case GridStringId.MenuGroupPanelClearGrouping:
                    return "清除分组";

                case GridStringId.MenuGroupPanelShow:
                    return "显示";

                case GridStringId.MenuGroupPanelHide:
                    return "隐藏";

                case GridStringId.PrintDesignerGridView:
                    return "打印设置(网格视图)";

                case GridStringId.PrintDesignerCardView:
                    return "打印设置(卡视图)";

                case GridStringId.PrintDesignerLayoutView:
                    return "打印设置(版面视图)";

                case GridStringId.PrintDesignerBandedView:
                    return "打印设置 (Banded View)";

                case GridStringId.PrintDesignerBandHeader:
                    return "起始带宽";

                case GridStringId.MenuColumnGroupBox:
                    return "分组依据框";

                case GridStringId.CardViewNewCard:
                    return "新建卡";

                case GridStringId.CardViewQuickCustomizationButton:
                    return "自定义";

                case GridStringId.CardViewQuickCustomizationButtonFilter:
                    return "过滤器　";

                case GridStringId.CardViewQuickCustomizationButtonSort:
                    return "排序方式:";

                case GridStringId.CardViewCaptionFormat:
                    return "记录 N {0}";

                case GridStringId.GridGroupPanelText:
                    return "拖动列标题至此,根据该列分组";

                case GridStringId.GridNewRowText:
                    return "在此处添加一行";

                case GridStringId.GridOutlookIntervals:
                    return "更早;上个月;本月初;三周之前;两周之前;上周;;;;;;;;昨天;今天;明天;;;;;;;;下周;两周后;三周后;本月底;下个月;一个月之后;";

                case GridStringId.PrintDesignerDescription:
                    return "为当前视图设置不同的打印选项";

                case GridStringId.MenuFooterCustomFormat:
                    return "统计值={0}";

                case GridStringId.MenuFooterCountGroupFormat:
                    return "计数={0}";

                case GridStringId.MenuColumnClearSorting:
                    return "清除排序设置";

                case GridStringId.MenuColumnFilterEditor:
                    return "设定数据筛选条件";

                case GridStringId.MenuColumnAutoFilterRowHide:
                    return "隐藏自动过滤行";

                case GridStringId.MenuColumnAutoFilterRowShow:
                    return "显示自动过滤行";

                case GridStringId.MenuColumnFindFilterHide:
                    return "隐藏查找面板";

                case GridStringId.MenuColumnFindFilterShow:
                    return "显示查找面板";

                case GridStringId.FilterBuilderOkButton:
                    return "确定(&O)";

                case GridStringId.FilterBuilderCancelButton:
                    return "取消(&C)";

                case GridStringId.FilterBuilderApplyButton:
                    return "应用(&A)";

                case GridStringId.FilterBuilderCaption:
                    return "数据筛选条件设定：";

                case GridStringId.CustomizationFormColumnHint:
                    return "在此拖拉列来定制布局";

                case GridStringId.CustomizationFormBandHint:
                    return "在此拖拉条来定制布局";

                case GridStringId.LayoutViewSingleModeBtnHint:
                    return "单卡";

                case GridStringId.LayoutViewRowModeBtnHint:
                    return "单行";

                case GridStringId.LayoutViewColumnModeBtnHint:
                    return "一个栏位";

                case GridStringId.LayoutViewMultiRowModeBtnHint:
                    return "多行";

                case GridStringId.LayoutViewMultiColumnModeBtnHint:
                    return "多列";

                case GridStringId.LayoutViewCarouselModeBtnHint:
                    return "旋转模式";

                case GridStringId.LayoutViewPanBtnHint:
                    return "面板";

                case GridStringId.LayoutViewCustomizeBtnHint:
                    return "自定义";

                case GridStringId.LayoutViewCloseZoomBtnHintClose:
                    return "还原视图";

                case GridStringId.LayoutViewCloseZoomBtnHintZoom:
                    return "最大化详细信息";

                case GridStringId.LayoutViewButtonApply:
                    return "应用(&A)";

                case GridStringId.LayoutViewButtonPreview:
                    return "显示更多卡(&M)";

                case GridStringId.LayoutViewButtonOk:
                    return "确定(&O)";

                case GridStringId.LayoutViewButtonCancel:
                    return "取消(&C)";

                case GridStringId.LayoutViewButtonSaveLayout:
                    return "保存版面...(&v)";

                case GridStringId.LayoutViewButtonLoadLayout:
                    return "加载面板(&L)...";

                case GridStringId.LayoutViewButtonCustomizeShow:
                    return "显示自定义(&S)";

                case GridStringId.LayoutViewButtonCustomizeHide:
                    return "隐藏自定义(&z)";

                case GridStringId.LayoutViewButtonReset:
                    return "重置卡模板(&R)";

                case GridStringId.LayoutViewButtonShrinkToMinimum:
                    return "收缩卡模板(&S)";

                case GridStringId.LayoutViewPageTemplateCard:
                    return "模板卡";

                case GridStringId.LayoutViewPageViewLayout:
                    return "查看版面";

                case GridStringId.LayoutViewGroupCustomization:
                    return "自定义";

                case GridStringId.LayoutViewGroupCaptions:
                    return "主题";

                case GridStringId.LayoutViewGroupIndents:
                    return "缩进";

                case GridStringId.LayoutViewGroupHiddenItems:
                    return "隐藏项";

                case GridStringId.LayoutViewGroupTreeStructure:
                    return "树形布局查看";

                case GridStringId.LayoutViewGroupPropertyGrid:
                    return "属性栅格";

                case GridStringId.LayoutViewLabelTextIndent:
                    return "文本缩进";

                case GridStringId.LayoutViewLabelPadding:
                    return "填充";

                case GridStringId.LayoutViewLabelSpacing:
                    return "间距";

                case GridStringId.LayoutViewLabelCaptionLocation:
                    return "区域主题位置";

                case GridStringId.LayoutViewLabelGroupCaptionLocation:
                    return "组标题位置:";

                case GridStringId.LayoutViewLabelTextAlignment:
                    return "文本对其方式:";

                case GridStringId.LayoutViewGroupView:
                    return "查看";

                case GridStringId.LayoutViewGroupLayout:
                    return "布局";

                case GridStringId.LayoutViewGroupCards:
                    return "卡";

                case GridStringId.LayoutViewGroupFields:
                    return "区域";

                case GridStringId.LayoutViewLabelShowLines:
                    return "显示线条";

                case GridStringId.LayoutViewLabelShowHeaderPanel:
                    return "显示表头面板";

                case GridStringId.LayoutViewLabelShowFilterPanel:
                    return "显示过滤面板";

                case GridStringId.LayoutViewLabelScrollVisibility:
                    return "滚动条可见:";

                case GridStringId.LayoutViewLabelViewMode:
                    return "查看模式";

                case GridStringId.LayoutViewLabelCardArrangeRule:
                    return "排列规则:";

                case GridStringId.LayoutViewLabelCardEdgeAlignment:
                    return "卡边缘对齐方式:";

                case GridStringId.LayoutViewGroupIntervals:
                    return "间隔";

                case GridStringId.LayoutViewLabelHorizontal:
                    return "水平间隔";

                case GridStringId.LayoutViewLabelVertical:
                    return "垂直间隔";

                case GridStringId.LayoutViewLabelShowCardCaption:
                    return "显示标题";

                case GridStringId.LayoutViewLabelShowCardExpandButton:
                    return "显示展开按钮";

                case GridStringId.LayoutViewLabelShowCardBorder:
                    return "显示边界";

                case GridStringId.LayoutViewLabelAllowFieldHotTracking:
                    return "允许热跟踪";

                case GridStringId.LayoutViewLabelShowFieldBorder:
                    return "显示边界";

                case GridStringId.LayoutViewLabelShowFieldHint:
                    return "显示提示";

                case GridStringId.LayoutViewCustomizationFormCaption:
                    return "自定义查看面板";

                case GridStringId.LayoutViewCustomizationFormDescription:
                    return "通过拖放自定义卡面板和菜单，并且可在查看面板中预览数据.";

                case GridStringId.LayoutModifiedWarning:
                    return "布局已被更改，确定要保存更改吗？";

                case GridStringId.LayoutViewCardCaptionFormat:
                    return "记录[{0} / {1}]";

                case GridStringId.LayoutViewFieldCaptionFormat:
                    return "{0}:";

                case GridStringId.GroupSummaryEditorFormCaption:
                    return "分组汇总编辑器";

                case GridStringId.GroupSummaryEditorFormOkButton:
                    return "确定";

                case GridStringId.GroupSummaryEditorFormCancelButton:
                    return "取消";

                case GridStringId.GroupSummaryEditorFormItemsTabCaption:
                    return "项";

                case GridStringId.GroupSummaryEditorFormOrderTabCaption:
                    return "排序";

                case GridStringId.GroupSummaryEditorSummaryMin:
                    return "最小";

                case GridStringId.GroupSummaryEditorSummaryMax:
                    return "最大";

                case GridStringId.GroupSummaryEditorSummaryAverage:
                    return "平均";

                case GridStringId.GroupSummaryEditorSummarySum:
                    return "总计";

                case GridStringId.GroupSummaryEditorSummaryCount:
                    return "个数";

                case GridStringId.FindControlFindButton:
                    return "搜索";

                case GridStringId.FindControlClearButton:
                    return "清除";

                case GridStringId.SearchLookUpMissingRows:
                    return "要显示所有的行，按“回车”(ENTER)键或单击“查找”(Find)。\r\n要搜索行，输入搜索字符串，并按下“回车”(ENTER)或单击“查找”(Find)。";

                case GridStringId.SearchLookUpAddNewButton:
                    return "新增";

                case GridStringId.MenuFooterAddSummaryItem:
                    return "新增总计";

                case GridStringId.MenuFooterClearSummaryItems:
                    return "清除总计项";

                case GridStringId.MenuShowSplitItem:
                    return "拆分";

                case GridStringId.MenuHideSplitItem:
                    return "删除拆分";

                case GridStringId.ServerRequestError:
                    return "服务器处理请求时发生错误 ({0}...)";
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

