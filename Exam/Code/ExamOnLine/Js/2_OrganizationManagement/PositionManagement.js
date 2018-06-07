var isExpand = true;
function expandAll() {
    if (isExpand) {
        App.position1_TreePanel_Main.collapseAll();
        App.btnExpandAll.Text = "展开全部";
        isExpand = false;
    }
    else {
        App.position1_TreePanel_Main.expandAll();
        App.btnExpandAll.Text = "折叠全部";
        isExpand = true;
    }
}