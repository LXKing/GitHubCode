namespace DevLocalization
{
    using DevExpress.Accessibility;
    using System;

    public class AccLocalizer_zhchs : EditResAccLocalizer
    {
        public override string GetLocalizedString(AccStringId id)
        {
            switch (id)
            {
                case AccStringId.ActionPress:
                    return "压力";

                case AccStringId.NameScroll:
                    return "滚动条";

                case AccStringId.NameScrollIndicator:
                    return "位置";

                case AccStringId.NameScrollLineUp:
                    return "上一行";

                case AccStringId.NameScrollLineDown:
                    return "向下一行";

                case AccStringId.NameScrollColumnLeft:
                    return "向左一列";

                case AccStringId.NameScrollColumnRight:
                    return "向右一列";

                case AccStringId.NameScrollAreaUp:
                    return "上一页";

                case AccStringId.NameScrollAreaDown:
                    return "下一页";

                case AccStringId.NameScrollAreaLeft:
                    return "向左一页";

                case AccStringId.NameScrollAreaRight:
                    return "向右一页";

                case AccStringId.DescScrollLineUp:
                    return "竖直位置上移一行";

                case AccStringId.DescScrollLineDown:
                    return "竖直位置下移一行";

                case AccStringId.DescScrollAreaUp:
                    return "竖直位置上移两行";

                case AccStringId.DescScrollAreaDown:
                    return "竖直位置下移两行";

                case AccStringId.DescScrollVertIndicator:
                    return "显示当前竖直位置，可以通过拖拽直接改变位置。";

                case AccStringId.DescScrollColumnLeft:
                    return "水平位置左移一列";

                case AccStringId.DescScrollColumnRight:
                    return "水平位置右移一列";

                case AccStringId.DescScrollAreaLeft:
                    return "水平位置左移两列";

                case AccStringId.DescScrollAreaRight:
                    return "水平位置右移两列";

                case AccStringId.DescScrollHorzIndicator:
                    return "显示当前水平位置，可以通过拖拽直接改变位置。";

                case AccStringId.ButtonPush:
                    return "压力";

                case AccStringId.ButtonOpen:
                    return "打开";

                case AccStringId.ButtonClose:
                    return "关闭";

                case AccStringId.MouseDoubleClick:
                    return "双击";

                case AccStringId.OpenKeyboardShortcut:
                    return "Alt+Down";

                case AccStringId.CheckEditCheck:
                    return "检查";

                case AccStringId.CheckEditUncheck:
                    return "未检查";

                case AccStringId.TabSwitch:
                    return "切换";

                case AccStringId.SpinBox:
                    return "旋转";

                case AccStringId.SpinUpButton:
                    return "向上";

                case AccStringId.SpinDownButton:
                    return "向下";

                case AccStringId.SpinLeftButton:
                    return "左";

                case AccStringId.SpinRightButton:
                    return "右";

                case AccStringId.GridNewItemRow:
                    return "新项目行";

                case AccStringId.GridFilterRow:
                    return "过滤行";

                case AccStringId.GridHeaderPanel:
                    return "表头面板";

                case AccStringId.GridDataPanel:
                    return "数据面板";

                case AccStringId.GridCell:
                    return "单元格";

                case AccStringId.GridRow:
                    return "行 {0}";

                case AccStringId.GridRowExpand:
                    return "扩展";

                case AccStringId.GridRowCollapse:
                    return "折叠";

                case AccStringId.GridCardExpand:
                    return "扩展";

                case AccStringId.GridCardCollapse:
                    return "折叠";

                case AccStringId.GridRowActivate:
                    return "激活";

                case AccStringId.GridCellEdit:
                    return "编辑";

                case AccStringId.GridCellFocus:
                    return "焦点";

                case AccStringId.GridDataRowExpand:
                    return "扩展细节";

                case AccStringId.GridDataRowCollapse:
                    return "折叠细节";

                case AccStringId.GridColumnSortAscending:
                    return "升序排列";

                case AccStringId.GridColumnSortDescending:
                    return "降序排列";

                case AccStringId.GridColumnSortNone:
                    return "删除排序";

                case AccStringId.BarLinkCaption:
                    return "项目";

                case AccStringId.BarLinkClick:
                    return "压力";

                case AccStringId.BarLinkMenuOpen:
                    return "打开";

                case AccStringId.BarLinkMenuClose:
                    return "关闭";

                case AccStringId.BarLinkStatic:
                    return "静态";

                case AccStringId.BarLinkEdit:
                    return "编辑";

                case AccStringId.BarDockControlTop:
                    return "靠顶部";

                case AccStringId.BarDockControlLeft:
                    return "靠左侧";

                case AccStringId.BarDockControlBottom:
                    return "靠底部";

                case AccStringId.BarDockControlRight:
                    return "靠右侧";

                case AccStringId.NavBarGroupExpand:
                    return "扩展";

                case AccStringId.NavBarGroupCollapse:
                    return "折叠";

                case AccStringId.NavBarItemClick:
                    return "压力";

                case AccStringId.NavBarScrollUp:
                    return "向上滚动";

                case AccStringId.NavBarScrollDown:
                    return "向下滚动";

                case AccStringId.TreeListNodeExpand:
                    return "展开";

                case AccStringId.TreeListNodeCollapse:
                    return "折叠";

                case AccStringId.TreeListNode:
                    return "节点";

                case AccStringId.TreeListNodeCell:
                    return "单元格";

                case AccStringId.TreeListColumnSortAscending:
                    return "升序排列";

                case AccStringId.TreeListColumnSortDescending:
                    return "降序排列";

                case AccStringId.TreeListColumnSortNone:
                    return "删除排序";

                case AccStringId.TreeListHeaderPanel:
                    return "标题面板";

                case AccStringId.TreeListDataPanel:
                    return "数据面板";

                case AccStringId.TreeListCellEdit:
                    return "编辑";

                case AccStringId.TreelistRowActivate:
                    return "活动的";

                case AccStringId.ScrollableControlDescription:
                    return "滚动";

                case AccStringId.ScrollableControlDefaultAction:
                    return "默认动作";
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

