namespace DevLocalization
{
    using DevExpress.XtraNavBar;
    using System;

    public class XtraNavBarLocalizer_zhchs : NavBarResLocalizer
    {
        public override string GetLocalizedString(NavBarStringId id)
        {
            switch (id)
            {
                case NavBarStringId.NavPaneMenuShowMoreButtons:
                    return "显示更多按钮（&M）";

                case NavBarStringId.NavPaneMenuShowFewerButtons:
                    return "显示少量按钮（&F）";

                case NavBarStringId.NavPaneMenuAddRemoveButtons:
                    return "添加或删除按钮（&A）";

                case NavBarStringId.NavPaneChevronHint:
                    return "配置按钮";
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

