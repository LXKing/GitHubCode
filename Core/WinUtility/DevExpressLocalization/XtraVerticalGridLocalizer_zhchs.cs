namespace DevLocalization
{
    using DevExpress.XtraVerticalGrid.Localization;
    using System;

    public class XtraVerticalGridLocalizer_zhchs : VGridResLocalizer
    {
        public override string GetLocalizedString(VGridStringId id)
        {
            switch (id)
            {
                case VGridStringId.RowCustomizationText:
                    return "定制";

                case VGridStringId.RowCustomizationNewCategoryFormText:
                    return "新增数据类别";

                case VGridStringId.RowCustomizationNewCategoryFormLabelText:
                    return "标题：";

                case VGridStringId.RowCustomizationNewCategoryText:
                    return "新增";

                case VGridStringId.RowCustomizationDeleteCategoryText:
                    return "删除";

                case VGridStringId.RowCustomizationTabPageCategoriesText:
                    return "分类数据";

                case VGridStringId.RowCustomizationTabPageRowsText:
                    return "数据列";

                case VGridStringId.InvalidRecordExceptionText:
                    return "是否要修改不正确的数据值？";

                case VGridStringId.StyleCreatorName:
                    return "风格定制器";

                case VGridStringId.MenuReset:
                    return "重置";

                case VGridStringId.MenuRowPropertiesExpressionEditor:
                    return "表达式编辑器...";
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

