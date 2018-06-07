namespace DevLocalization
{
    using DevExpress.XtraPivotGrid.Localization;
    using System;

    public class XtraPivotGridLocalizer_zhchs : PivotGridResLocalizer
    {
        public override string GetLocalizedString(PivotGridStringId id)
        {
            switch (id)
            {
                case PivotGridStringId.RowHeadersCustomization:
                    return "将行域放在这里";

                case PivotGridStringId.ColumnHeadersCustomization:
                    return "将列域放在这里";

                case PivotGridStringId.FilterHeadersCustomization:
                    return "将数据筛选条件域放在这里";

                case PivotGridStringId.DataHeadersCustomization:
                    return "将数据项目放在这里";

                case PivotGridStringId.RowArea:
                    return "行区域";

                case PivotGridStringId.ColumnArea:
                    return "列区域";

                case PivotGridStringId.FilterArea:
                    return "数据筛选区域";

                case PivotGridStringId.DataArea:
                    return "数据区域";

                case PivotGridStringId.FilterShowAll:
                    return "(全部显示)";

                case PivotGridStringId.FilterOk:
                    return "确定";

                case PivotGridStringId.FilterCancel:
                    return "取消";

                case PivotGridStringId.FilterBlank:
                    return "(空白)";

                case PivotGridStringId.FilterShowBlanks:
                    return "显示空白";

                case PivotGridStringId.FilterInvert:
                    return "反转过滤";

                case PivotGridStringId.CustomizationFormCaption:
                    return "PivotGrid标题列表";

                case PivotGridStringId.CustomizationFormText:
                    return "将项目放到PivotGrid中";

                case PivotGridStringId.CustomizationFormAddTo:
                    return "增加到";

                case PivotGridStringId.CustomizationFormHint:
                    return "拖动字段到区域下";

                case PivotGridStringId.CustomizationFormDeferLayoutUpdate:
                    return "延迟布局更新";

                case PivotGridStringId.CustomizationFormUpdate:
                    return "更新";

                case PivotGridStringId.CustomizationFormListBoxText:
                    return "拖动字段至此自定义布局";

                case PivotGridStringId.CustomizationFormHiddenFields:
                    return "隐藏字段";

                case PivotGridStringId.Total:
                    return "合计";

                case PivotGridStringId.GrandTotal:
                    return "总计";

                case PivotGridStringId.TotalFormat:
                    return "{0}合计";

                case PivotGridStringId.TotalFormatCount:
                    return "{0}数据数";

                case PivotGridStringId.TotalFormatSum:
                    return "{0}小计";

                case PivotGridStringId.TotalFormatMin:
                    return "{0} 最小值";

                case PivotGridStringId.TotalFormatMax:
                    return "{0} 最大值";

                case PivotGridStringId.TotalFormatAverage:
                    return "{0}平均";

                case PivotGridStringId.TotalFormatStdDev:
                    return "{0}标准差";

                case PivotGridStringId.TotalFormatStdDevp:
                    return "{0}总体标准差";

                case PivotGridStringId.TotalFormatVar:
                    return "{0}样本方差";

                case PivotGridStringId.TotalFormatVarp:
                    return "{0}总体方差";

                case PivotGridStringId.TotalFormatCustom:
                    return "{0}统计值";

                case PivotGridStringId.PrintDesigner:
                    return "打印设计";

                case PivotGridStringId.PrintDesignerPageOptions:
                    return "选项";

                case PivotGridStringId.PrintDesignerPageBehavior:
                    return "执行";

                case PivotGridStringId.PrintDesignerCategoryDefault:
                    return "默认值";

                case PivotGridStringId.PrintDesignerCategoryLines:
                    return "格线";

                case PivotGridStringId.PrintDesignerCategoryHeaders:
                    return "标题";

                case PivotGridStringId.PrintDesignerCategoryFieldValues:
                    return "字段值";

                case PivotGridStringId.PrintDesignerHorizontalLines:
                    return "水平格线";

                case PivotGridStringId.PrintDesignerVerticalLines:
                    return "垂直格线";

                case PivotGridStringId.PrintDesignerFilterHeaders:
                    return "数据筛选条件标题";

                case PivotGridStringId.PrintDesignerDataHeaders:
                    return "数据标题";

                case PivotGridStringId.PrintDesignerColumnHeaders:
                    return "列标题";

                case PivotGridStringId.PrintDesignerRowHeaders:
                    return "行标题";

                case PivotGridStringId.PrintDesignerHeadersOnEveryPage:
                    return "标题在每一个页面";

                case PivotGridStringId.PrintDesignerUnusedFilterFields:
                    return "未使用过滤字段";

                case PivotGridStringId.PrintDesignerMergeColumnFieldValues:
                    return "合并列字段值";

                case PivotGridStringId.PrintDesignerMergeRowFieldValues:
                    return "合并行字段值";

                case PivotGridStringId.PrintDesignerUsePrintAppearance:
                    return "使用打印外观";

                case PivotGridStringId.PopupMenuRefreshData:
                    return "刷新数据";

                case PivotGridStringId.PopupMenuSortAscending:
                    return "升序";

                case PivotGridStringId.PopupMenuSortDescending:
                    return "降序";

                case PivotGridStringId.PopupMenuClearSorting:
                    return "清除排序";

                case PivotGridStringId.PopupMenuShowExpression:
                    return "表达式编辑器...";

                case PivotGridStringId.PopupMenuHideField:
                    return "隐藏";

                case PivotGridStringId.PopupMenuShowFieldList:
                    return "显示数据栏清单";

                case PivotGridStringId.PopupMenuHideFieldList:
                    return "隐藏数据栏清单";

                case PivotGridStringId.PopupMenuFieldOrder:
                    return "排序";

                case PivotGridStringId.PopupMenuMovetoBeginning:
                    return "移到开头";

                case PivotGridStringId.PopupMenuMovetoLeft:
                    return "移到左边";

                case PivotGridStringId.PopupMenuMovetoRight:
                    return "移到右边";

                case PivotGridStringId.PopupMenuMovetoEnd:
                    return "移到最后";

                case PivotGridStringId.PopupMenuCollapse:
                    return "收合";

                case PivotGridStringId.PopupMenuExpand:
                    return "展开";

                case PivotGridStringId.PopupMenuCollapseAll:
                    return "全部收合";

                case PivotGridStringId.PopupMenuExpandAll:
                    return "全部展开";

                case PivotGridStringId.PopupMenuShowPrefilter:
                    return "显示过滤器";

                case PivotGridStringId.PopupMenuHidePrefilter:
                    return "隐藏过滤器";

                case PivotGridStringId.PopupMenuSortFieldByColumn:
                    return "按列\"{0}\"排序";

                case PivotGridStringId.PopupMenuSortFieldByRow:
                    return "按行\"{0}\"排序";

                case PivotGridStringId.PopupMenuRemoveAllSortByColumn:
                    return "清除所有排序";

                case PivotGridStringId.DataFieldCaption:
                    return "数据";

                case PivotGridStringId.TopValueOthersRow:
                    return "其他";

                case PivotGridStringId.CellError:
                    return "错误";

                case PivotGridStringId.ValueError:
                    return "错误";

                case PivotGridStringId.CannotCopyMultipleSelections:
                    return "此命令不能多次选择";

                case PivotGridStringId.PrefilterInvalidProperty:
                    return "(无效属性)";

                case PivotGridStringId.PrefilterInvalidCriteria:
                    return "错误发生在滤器条件。请检测无效的属性标题和条件操作，并更正或删除。";

                case PivotGridStringId.PrefilterFormCaption:
                    return "PivotGrid过滤器";

                case PivotGridStringId.EditPrefilter:
                    return "编辑过虑器";

                case PivotGridStringId.OLAPMeasuresCaption:
                    return "量度";

                case PivotGridStringId.OLAPDrillDownFilterException:
                    return "当在一个报表的筛选字段中选择了多个项目时显示明细命令将无法执行。在执行之前请在报表筛选区域为每一个筛选选择一个单独项目。";

                case PivotGridStringId.OLAPNoOleDbProvidersMessage:
                    return "为了使用的PivotGrid OLAP功能，你应该在系统上安装一个MS OLAP OLEDB提供程序。\r\n您可以在这里下载:";

                case PivotGridStringId.TrendGoingUp:
                    return "上升";

                case PivotGridStringId.TrendGoingDown:
                    return "下沉";

                case PivotGridStringId.TrendNoChange:
                    return "不改变";

                case PivotGridStringId.StatusBad:
                    return "坏";

                case PivotGridStringId.StatusNeutral:
                    return "中立";

                case PivotGridStringId.StatusGood:
                    return "好";

                case PivotGridStringId.SummaryCount:
                    return "计数";

                case PivotGridStringId.SummarySum:
                    return "总计";

                case PivotGridStringId.SummaryMin:
                    return "最小";

                case PivotGridStringId.SummaryMax:
                    return "最大";

                case PivotGridStringId.SummaryAverage:
                    return "平均";

                case PivotGridStringId.SummaryStdDev:
                    return "标准差估计";

                case PivotGridStringId.SummaryStdDevp:
                    return "扩展标准差";

                case PivotGridStringId.SummaryVar:
                    return "变异数估计";

                case PivotGridStringId.SummaryVarp:
                    return "扩展变异数";

                case PivotGridStringId.SummaryCustom:
                    return "自定";

                case PivotGridStringId.CustomizationFormStackedDefault:
                    return "字段和区层叠";

                case PivotGridStringId.CustomizationFormStackedSideBySide:
                    return "字段和区并排";

                case PivotGridStringId.CustomizationFormTopPanelOnly:
                    return "只有字段";

                case PivotGridStringId.CustomizationFormBottomPanelOnly2by2:
                    return "只有2区中的2区";

                case PivotGridStringId.CustomizationFormBottomPanelOnly1by4:
                    return "只有4区中的1区";

                case PivotGridStringId.CustomizationFormLayoutButtonTooltip:
                    return "自定布局";

                case PivotGridStringId.FilterPopupToolbarShowOnlyAvailableItems:
                    return "显示可用项";

                case PivotGridStringId.FilterPopupToolbarShowNewValues:
                    return "显示新的字段值";

                case PivotGridStringId.FilterPopupToolbarIncrementalSearch:
                    return "增加搜索";

                case PivotGridStringId.FilterPopupToolbarMultiSelection:
                    return "多选";

                case PivotGridStringId.FilterPopupToolbarRadioMode:
                    return "单选";

                case PivotGridStringId.FilterPopupToolbarInvertFilter:
                    return "反向过滤";

                case PivotGridStringId.Alt_Expand:
                    return "[展开]";

                case PivotGridStringId.Alt_Collapse:
                    return "[折迭]";

                case PivotGridStringId.Alt_SortedAscending:
                    return "(升序)";

                case PivotGridStringId.Alt_SortedDescending:
                    return "(降序)";

                case PivotGridStringId.Alt_FilterWindowSizeGrip:
                    return "[调整大小]";

                case PivotGridStringId.Alt_FilterButton:
                    return "[过滤器]";

                case PivotGridStringId.Alt_FilterButtonActive:
                    return "[已过滤]";

                case PivotGridStringId.Alt_DragHideField:
                    return "隐藏";

                case PivotGridStringId.Alt_FilterAreaHeaders:
                    return "[过滤区标题]";

                case PivotGridStringId.Alt_ColumnAreaHeaders:
                    return "[列区标题]";

                case PivotGridStringId.Alt_RowAreaHeaders:
                    return "[行区标题]";

                case PivotGridStringId.Alt_DataAreaHeaders:
                    return "[数据区标题]";

                case PivotGridStringId.Alt_FieldListHeaders:
                    return "[隐藏字段标题]";

                case PivotGridStringId.Alt_LayoutButton:
                    return "[布局按钮]";

                case PivotGridStringId.Alt_StackedDefaultLayout:
                    return "[堆叠默认布局]";

                case PivotGridStringId.Alt_StackedSideBySideLayout:
                    return "[堆叠并排布局]";

                case PivotGridStringId.Alt_TopPanelOnlyLayout:
                    return "[只上面板布局]";

                case PivotGridStringId.Alt_BottomPanelOnly2by2Layout:
                    return "[底部面板只2区中的2区布局]";

                case PivotGridStringId.Alt_BottomPanelOnly1by4Layout:
                    return "[底部面板只4区中的1区布局]";

                case PivotGridStringId.SearchBoxText:
                    return "搜索";
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

