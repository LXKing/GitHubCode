namespace DevLocalization
{
    using DevExpress.XtraBars.Docking;
    using System;

    public class XtraBarsDockingLocalizer_zhchs : DockManagerResXLocalizer
    {
        public override string GetLocalizedString(DockManagerStringId id)
        {
            switch (id)
            {
                case DockManagerStringId.CommandActivate:
                    return "启动";

                case DockManagerStringId.CommandClose:
                    return "关闭";

                case DockManagerStringId.CommandFloat:
                    return "浮动";

                case DockManagerStringId.CommandDock:
                    return "选项卡";

                case DockManagerStringId.CommandDockAsTabbedDocument:
                    return "选项卡式文档";

                case DockManagerStringId.CommandAutoHide:
                    return "自动隐藏";

                case DockManagerStringId.CommandMaximize:
                    return "最大化";

                case DockManagerStringId.CommandRestore:
                    return "还原";

                case DockManagerStringId.MessageFormPropertyChangedCaption:
                    return "警告";

                case DockManagerStringId.MessageFormPropertyChangedText:
                    return "如果你改变窗体属性，所有的布局将会丢失。是否继续？";
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

