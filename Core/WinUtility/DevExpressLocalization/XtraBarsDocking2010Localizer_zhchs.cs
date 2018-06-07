namespace DevLocalization
{
    using DevExpress.XtraBars.Docking2010;
    using System;

    public class XtraBarsDocking2010Localizer_zhchs : DocumentManagerResXLocalizer
    {
        public override string GetLocalizedString(DocumentManagerStringId id)
        {
            switch (id)
            {
                case DocumentManagerStringId.CommandActivate:
                    return "启动";

                case DocumentManagerStringId.CommandClose:
                    return "关闭";

                case DocumentManagerStringId.CommandCloseAll:
                    return "全部关闭";

                case DocumentManagerStringId.CommandCloseAllButThis:
                    return "除此之外全部关闭";

                case DocumentManagerStringId.CommandFloat:
                    return "浮动";

                case DocumentManagerStringId.CommandDock:
                    return "选项卡式文档";

                case DocumentManagerStringId.CommandNewDocumentGroup:
                    return "新建选项卡组";

                case DocumentManagerStringId.CommandNewHorizontalDocumentGroup:
                    return "新建水平选项卡组";

                case DocumentManagerStringId.CommandNewVerticalDocumentGroup:
                    return "新建垂直选项卡组";

                case DocumentManagerStringId.CommandMoveToNextDocumentGroup:
                    return "移动到下一个选项卡组";

                case DocumentManagerStringId.CommandMoveToPrevDocumentGroup:
                    return "移动到上一个选项卡组";

                case DocumentManagerStringId.CommandVerticalOrientation:
                    return "垂直";

                case DocumentManagerStringId.CommandHorizontalOrientation:
                    return "水平";

                case DocumentManagerStringId.CommandCascade:
                    return "层叠窗口";

                case DocumentManagerStringId.CommandTileVertical:
                    return "并排显示窗口";

                case DocumentManagerStringId.CommandTileHorizontal:
                    return "堆叠显示窗口";

                case DocumentManagerStringId.CommandMinimizeAll:
                    return "最小化窗口";

                case DocumentManagerStringId.CommandArrangeIcons:
                    return "重排图标";

                case DocumentManagerStringId.CommandRestoreAll:
                    return "还原窗口";

                case DocumentManagerStringId.CommandBack:
                    return "后退";

                case DocumentManagerStringId.CommandMaximize:
                    return "最大化";

                case DocumentManagerStringId.CommandRestore:
                    return "还原";
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

