namespace DevLocalization
{
    using DevExpress.XtraLayout.Localization;
    using System;

    public class LayoutLocalizer_zhchs : LayoutResLocalizer
    {
        public override string GetLocalizedString(LayoutStringId id)
        {
            switch (id)
            {
                //case LayoutStringId.CustomizationParentName:
                //    return "定制";

                case LayoutStringId.DefaultItemText:
                    return "项目";

                case LayoutStringId.DefaultActionText:
                    return "默认动作";

                case LayoutStringId.DefaultEmptyText:
                    return "无";

                case LayoutStringId.LayoutItemDescription:
                    return "版面设计控制器的项目元素";

                case LayoutStringId.LayoutGroupDescription:
                    return "版面设计控制器的群组元素";

                case LayoutStringId.TabbedGroupDescription:
                    return "版面控制器的群组标签页元素";

                case LayoutStringId.LayoutControlDescription:
                    return "版面控制";

                case LayoutStringId.CustomizationFormTitle:
                    return "定制";

                case LayoutStringId.HiddenItemsPageTitle:
                    return "隐藏项目";

                case LayoutStringId.HiddenItemsNodeText:
                    return "隐藏项目";

                case LayoutStringId.TreeViewPageTitle:
                    return "版面设计树状视图";

                case LayoutStringId.RenameSelected:
                    return "重命名";

                case LayoutStringId.HideItemMenutext:
                    return "隐藏项目";

                case LayoutStringId.LockItemSizeMenuText:
                    return "锁定项目大小";

                case LayoutStringId.UnLockItemSizeMenuText:
                    return "解除项目大小锁定";

                case LayoutStringId.GroupItemsMenuText:
                    return "群组";

                case LayoutStringId.UnGroupItemsMenuText:
                    return "解除群组设定";

                case LayoutStringId.CreateTabbedGroupMenuText:
                    return "创建群组标签页";

                case LayoutStringId.AddTabMenuText:
                    return "增加标签页";

                case LayoutStringId.UnGroupTabbedGroupMenuText:
                    return "解除群组标签页设定";

                case LayoutStringId.TreeViewRootNodeName:
                    return "最上层";

                case LayoutStringId.ShowCustomizationFormMenuText:
                    return "定制版面";

                case LayoutStringId.HideCustomizationFormMenuText:
                    return "隐藏定制表格";

                case LayoutStringId.EmptySpaceItemDefaultText:
                    return "空白区域项目";

                case LayoutStringId.SplitterItemDefaultText:
                    return "分隔器版面設計控制器的群組標籤頁項目";

                case LayoutStringId.SimpleLabelItemDefaultText:
                    return "标签";

                case LayoutStringId.SimpleSeparatorItemDefaultText:
                    return "分隔符";

                case LayoutStringId.ControlGroupDefaultText:
                    return "群组";

                case LayoutStringId.EmptyRootGroupText:
                    return "在这里放置控件";

                case LayoutStringId.EmptyTabbedGroupText:
                    return "将群组拖放到群组标签页区域";

                case LayoutStringId.ResetLayoutMenuText:
                    return "重设版面";

                case LayoutStringId.RenameMenuText:
                    return "重命名";

                case LayoutStringId.TextPositionMenuText:
                    return "文本位置";

                case LayoutStringId.TextPositionLeftMenuText:
                    return "左边";

                case LayoutStringId.TextPositionRightMenuText:
                    return "右边";

                case LayoutStringId.TextPositionTopMenuText:
                    return "上方";

                case LayoutStringId.TextPositionBottomMenuText:
                    return "下方";

                case LayoutStringId.ShowTextMenuItem:
                    return "显示文本";

                case LayoutStringId.HideTextMenuItem:
                    return "隐藏文本";

                case LayoutStringId.ShowSpaceMenuItem:
                    return "显示占位符";

                case LayoutStringId.HideSpaceMenuItem:
                    return "隐藏占位符";

                case LayoutStringId.LockSizeMenuItem:
                    return "锁定大小";

                case LayoutStringId.LockWidthMenuItem:
                    return "锁定宽度";

                case LayoutStringId.LockHeightMenuItem:
                    return "锁定高度";

                case LayoutStringId.LockMenuGroup:
                    return "强制限定大小";

                case LayoutStringId.ResetConstraintsToDefaultsMenuItem:
                    return "重设为默认值";

                case LayoutStringId.FreeSizingMenuItem:
                    return "允许改变大小";

                case LayoutStringId.CreateEmptySpaceItem:
                    return "创建空白区域项目";

                case LayoutStringId.UndoHintCaption:
                    return "撤消(Ctrl+Z)";

                case LayoutStringId.RedoHintCaption:
                    return "重复(Ctrl+Y)";

                case LayoutStringId.LoadHintCaption:
                    return "加载布局(Ctrl+O)";

                case LayoutStringId.SaveHintCaption:
                    return "保存布局(Ctrl+S)";

                case LayoutStringId.UndoButtonHintText:
                    return "撤消到最后动作";

                case LayoutStringId.RedoButtonHintText:
                    return "重复到最后动作";

                case LayoutStringId.SaveButtonHintText:
                    return "保存布局为XML文件";

                case LayoutStringId.LoadButtonHintText:
                    return "从XML文件加载布局";

                case LayoutStringId.LayoutResetConfirmationText:
                    return "你将要重置布局定制。是否继续？";

                case LayoutStringId.LayoutResetConfirmationDialogCaption:
                    return "布局重置确认";
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

