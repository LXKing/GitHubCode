namespace DevLocalization
{
    using DevExpress.XtraTreeList.Localization;
    using System;

    public class XtraTreeListLocalizer_zhchs : TreeListResLocalizer
    {
        public override string GetLocalizedString(TreeListStringId id)
        {
            switch (id)
            {
                case TreeListStringId.MenuFooterSum:
                    return "和";

                case TreeListStringId.MenuFooterMin:
                    return "最小值";

                case TreeListStringId.MenuFooterMax:
                    return "最大值";

                case TreeListStringId.MenuFooterCount:
                    return "计数";

                case TreeListStringId.MenuFooterAverage:
                    return "平均值";

                case TreeListStringId.MenuFooterNone:
                    return "无";

                case TreeListStringId.MenuFooterAllNodes:
                    return "所有节点";

                case TreeListStringId.MenuFooterSumFormat:
                    return "和={0:#.##}";

                case TreeListStringId.MenuFooterMinFormat:
                    return "最小值={0}";

                case TreeListStringId.MenuFooterMaxFormat:
                    return "最大值={0}";

                case TreeListStringId.MenuFooterCountFormat:
                    return "{0}";

                case TreeListStringId.MenuFooterAverageFormat:
                    return "平均值={0:#.##}";

                case TreeListStringId.MenuColumnSortAscending:
                    return "升序排列";

                case TreeListStringId.MenuColumnSortDescending:
                    return "降序排列";

                case TreeListStringId.MenuColumnColumnCustomization:
                    return "列选择";

                case TreeListStringId.MenuColumnBestFit:
                    return "最佳匹配";

                case TreeListStringId.MenuColumnBestFitAllColumns:
                    return "最佳匹配 (所有列)";

                case TreeListStringId.ColumnCustomizationText:
                    return "自定义";

                case TreeListStringId.ColumnNamePrefix:
                    return "列";

                case TreeListStringId.PrintDesignerHeader:
                    return "打印设置";

                case TreeListStringId.PrintDesignerDescription:
                    return "为当前的树状列表设置不同的打印选项";

                case TreeListStringId.InvalidNodeExceptionText:
                    return " 要修正当前值吗?";

                case TreeListStringId.MultiSelectMethodNotSupported:
                    return "OptionsBehavior.MultiSelect未激活时，指定方法不能工作.";

                case TreeListStringId.CustomizationFormColumnHint:
                    return "拖放列到这自定布局";

                case TreeListStringId.FilterPanelCustomizeButton:
                    return "编辑过滤器";

                case TreeListStringId.WindowErrorCaption:
                    return "错误";

                case TreeListStringId.FilterEditorOkButton:
                    return "确定(&)";

                case TreeListStringId.FilterEditorCancelButton:
                    return "取消(&)";

                case TreeListStringId.FilterEditorApplyButton:
                    return "应用(&)";

                case TreeListStringId.FilterEditorCaption:
                    return "过滤器编辑器";

                case TreeListStringId.MenuColumnAutoFilterRowHide:
                    return "隐藏自动过滤行";

                case TreeListStringId.MenuColumnAutoFilterRowShow:
                    return "显示自动过滤行";

                case TreeListStringId.MenuColumnFilterEditor:
                    return "过滤器编辑器...";

                case TreeListStringId.MenuColumnClearFilter:
                    return "清空过滤器";

                case TreeListStringId.PopupFilterAll:
                    return "(所有)";

                case TreeListStringId.PopupFilterBlanks:
                    return "(空白)";

                case TreeListStringId.PopupFilterNonBlanks:
                    return "(非空白)";
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

