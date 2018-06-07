using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Handler;
using DevExpress.XtraTreeList.Menu;
using DevExpress.XtraTreeList.Nodes;
using XCI.Component;
using XCI.Core;
using XCI.WinUtility.GridConfig;
using XCI.WinUtility.Properties;

namespace XCI.WinUtility
{
    public class XCITreeGrid : TreeList
    {
        #region 属性

        private bool isInit;
        private string _gridid;

        [Browsable(false)]
        public string GridID
        {
            get { return _gridid ?? (_gridid = Guid.NewGuid().ToString("N")); }
            set { _gridid = value; }
        }

        [Browsable(false)]
        public bool IsFirstNode
        {
            get
            {
                if (!HasData)
                {
                    return false;
                }
                if (FocusedNode != null)
                {
                    int index = GetNodeIndex(FocusedNode);
                    if (index == 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        [Browsable(false)]
        public bool IsLastNode
        {
            get
            {
                if (!HasData)
                {
                    return false;
                }
                if (FocusedNode != null)
                {
                    var node = FocusedNode;
                    int index = GetNodeIndex(node);
                    int count = GetNodeLevelNodeCount(node);
                    if (index == count - 1)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        [Browsable(false)]
        public bool IsFirstRow
        {
            get { return HasData && this.FocusedRowIndex == 0; }
        }

        [Browsable(false)]
        public bool IsLastRow
        {
            get { return HasData && this.FocusedRowIndex == this.RowCount - 1; }
        }

        private bool _isShowColumnHeardMenu = true;

        /// <summary>
        /// 是否自动删除下级(默认不允许)
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutoDeleteChild { get; set; }

        /// <summary>
        /// 是否显示列菜单
        /// </summary>
        [Browsable(true), Category("XCI"), Description("是否显示列菜单")]
        public bool IsShowColumnHeardMenu
        {
            get { return _isShowColumnHeardMenu; }
            set { _isShowColumnHeardMenu = value; }
        }

        private bool _isConfigChange;
        /// <summary>
        /// 配置是否发生变化 默认为false
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsConfigChange
        {
            get { return _isConfigChange; }
            set
            {
                _isConfigChange = value;
                OnGridConfigChangeNotify(EventArgs.Empty);
            }
        }

        private bool _isStoreConfig = true;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStoreConfig
        {
            get { return _isStoreConfig; }
            set { _isStoreConfig = value; }
        }

        private bool _isShowConfigButton = true;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsShowConfigButton
        {
            get { return _isShowConfigButton; }
            set { _isShowConfigButton = value; }
        }

        private Func<object, object> _getObjectId;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<object, object> GetObjectID
        {
            get
            {
                if (_getObjectId == null)
                {
                    if (DataSource is IList)
                    {
                        _getObjectId = p => p.GetType().GetProperty("ID").GetValue(p, null);//((EntityBase)p).ID;
                    }
                    else if (DataSource is DataTable)
                    {
                        _getObjectId = p => ((DataRow)p)[KeyFieldName];
                    }
                }
                return _getObjectId;
            }
            set { _getObjectId = value; }
        }

        private PopupMenu _popMenu;
        /// <summary>
        /// 右键菜单控件
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PopupMenu PopMenu
        {
            get
            {
                if (_popMenu == null)
                {
                    _popMenu = new PopupMenu();
                    _popMenu.Manager = MainBar;
                }
                return _popMenu;
            }
            set { _popMenu = value; }
        }

        /// <summary>
        /// 菜单管理器
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BarManager MainBar
        {
            get
            {
                if (MenuManager != null)
                {
                    return (BarManager)MenuManager;
                }
                return null;
            }
            set { MenuManager = value; }
        }

        #endregion

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type EntityType { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new object DataSource
        {
            get { return base.DataSource; }
            set
            {
                if (value != null && base.DataSource != null)
                {
                    if (base.DataSource is IList)
                    {
                        IList olddata = (IList)base.DataSource;
                        IList newData = (IList)value;
                        olddata.Clear();
                        foreach (var item in newData)
                        {
                            olddata.Add(item);
                        }
                        RefreshData();
                    }
                    else if (base.DataSource is DataTable)
                    {
                        if (GetObjectID != null)
                        {
                            var obj = Get(FocusedNode);
                            if (obj != null)
                            {
                                object ID = GetObjectID(obj);
                                base.DataSource = value;
                                var newNode = FindNodeByKeyID(ID);
                                if (newNode != null)
                                {
                                    this.SetFocusedNode(newNode);
                                }
                            }
                            else
                            {
                                base.DataSource = value;
                            }
                        }
                        else
                        {
                            base.DataSource = value;
                        }
                    }
                }
                else
                {
                    base.DataSource = value;
                }
            }
        }

        [Browsable(false)]
        public IList ListSource
        {
            get
            {
                if (this.DataSource != null && this.DataSource is IList)
                {
                    return (IList)this.DataSource;
                }
                return null;
            }
        }
        [Browsable(false)]
        public DataTable TableSource
        {
            get
            {
                if (this.DataSource != null && this.DataSource is DataTable)
                {
                    return (DataTable)this.DataSource;
                }
                return null;
            }
        }

        public TreeListHandler TreeHandler
        {
            get { return this.Handler; }
        }


        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshData()
        {
            this.RefreshDataSource();
        }

        /// <summary>
        /// 是否包含数据
        /// </summary>
        [Browsable(false)]
        public bool HasData
        {
            get
            {
                if (this.DataSource != null)
                {
                    var source = this.DataSource as IList;
                    if (source != null)
                    {
                        return (source).Count > 0;
                    }
                    var dataTable = this.DataSource as DataTable;
                    if (dataTable != null)
                    {
                        return (dataTable).Rows.Count > 0;
                    }
                }
                return Nodes.Count > 0;
            }
        }

        /// <summary>
        /// 获取指定节点的父节点的子节点个数
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int GetNodeLevelNodeCount(TreeListNode node)
        {
            int count = 0;
            if (node.ParentNode == null)
            {
                count = Nodes.Count;
            }
            else
            {
                count = FocusedNode.ParentNode.Nodes.Count;
            }
            return count;
        }

        #region 事件

        /// <summary>
        /// 表格配置通知
        /// </summary>
        public event EventHandler<EventArgs> GridConfigChangeNotify;

        /// <summary>
        /// 触发表格配置通知事件
        /// </summary>
        /// <param name="e">表格配置通知参数</param>
        protected virtual void OnGridConfigChangeNotify(EventArgs e)
        {
            if (GridConfigChangeNotify != null)
            {
                GridConfigChangeNotify(this, e);
            }
        }

        /// <summary>
        /// 保存配置之前触发
        /// </summary>
        public event EventHandler<EventArgs> BeforeSaveConfig;

        /// <summary>
        /// 保存配置之前触发
        /// </summary>
        /// <param name="e">保存配置之前触发参数</param>
        public virtual void OnBeforeSaveConfig(EventArgs e)
        {
            if (BeforeSaveConfig != null)
            {
                BeforeSaveConfig(this, e);
            }
        }

        /// <summary>
        /// 读取配置之后触发
        /// </summary>
        public event EventHandler<EventArgs> AfterLoadConfig;

        /// <summary>
        /// 读取配置之后触发
        /// </summary>
        /// <param name="e">读取配置之后触发参数</param>
        public virtual void OnAfterLoadConfig(EventArgs e)
        {
            if (AfterLoadConfig != null)
            {
                AfterLoadConfig(this, e);
            }
        }

        #endregion


        #region 实体操作

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">数据</param>
        public void Add(object entity)
        {
            if (this.DataSource != null)
            {
                if (this.DataSource is IList)
                {
                    ((IList)this.DataSource).Add(entity);
                    RefreshData();
                }
                else if (this.DataSource is DataTable)
                {
                    ((DataTable)this.DataSource).Rows.Add((DataRow)entity);
                }
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">数据</param>
        public void Update(object entity)
        {
            if (this.DataSource == null) return;

            var listSource = this.DataSource as IList;
            if (listSource != null)
            {
                var index = listSource.IndexOf(entity);
                listSource[index] = entity;
                this.RefreshNode(GetNode(entity));
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">数据</param>
        public void Delete(object entity)
        {
            Delete(GetNode(entity));
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void Delete(TreeListNode node)
        {
            if (!IsAutoDeleteChild && node.HasChildren) return;
            this.DeleteNode(node);
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void Clear()
        {
            if (this.DataSource != null)
            {
                if (this.DataSource is IList)
                {
                    ((IList)this.DataSource).Clear();
                    this.ClearNodes();
                }
                else if (this.DataSource is DataTable)
                {
                    ((DataTable)this.DataSource).Rows.Clear();
                }
            }
        }

        /// <summary>
        /// 获取数据所在的行索引
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TreeListNode GetNode(object entity)
        {
            if (GetObjectID == null) return null;
            if (this.DataSource == null) return null;
            return this.FindNodeByKeyID(GetObjectID(entity));
        }

        /// <summary>
        /// 选中数据
        /// </summary>
        /// <param name="entity">数据</param>
        public void Select(object entity)
        {
            var node = GetNode(entity);
            Select(node);
        }

        public void Select(TreeListNode node)
        {
            if (node != null)
            {
                this.SetFocusedNode(node);
                //this.MakeNodeVisible(node);
            }
        }

        #endregion


        #region 获取选中记录

        public object Get(TreeListNode node)
        {
            if (DataSource is IList)
            {
                return this.GetDataRecordByNode(node);
            }
            if (DataSource is DataTable)
            {
                return ((DataRowView)this.GetDataRecordByNode(node)).Row;
            }
            if (node != null && node.Tag != null)
            {
                return node.Tag;
            }
            return null;
        }

        public T Get<T>(TreeListNode node) where T : class
        {
            var obj = Get(node);
            if (obj == null)
            {
                return default(T);
            }
            return obj as T;
        }

        public object GetParent(TreeListNode node)
        {
            object result = null;
            if (node != null && node.ParentNode != null)
            {
                result = GetDataRecordByNode(node.ParentNode);
            }
            if (node != null && node.ParentNode != null && node.ParentNode.Tag != null)
            {
                result = node.ParentNode.Tag;
            }
            return result;
        }

        /// <summary>
        /// 获取选中行的父实体
        /// </summary>
        public object GetSelectedParent()
        {
            return GetParent(FocusedNode);
        }


        /// <summary>
        /// 获取选中数据
        /// </summary>
        /// <returns>数据</returns>
        public object GetSelected()
        {
            return Get(this.FocusedNode);
        }

        public T GetSelected<T>() where T : class
        {
            var obj = GetSelected();
            if (obj == null)
            {
                return default(T);
            }
            return obj as T;
        }

        ///// <summary>
        ///// 获取选中的数据列表
        ///// </summary>
        ///// <returns>数据列表</returns>
        //public IList GetSelectedList()
        //{
        //    throw new System.NotImplementedException();
        //}

        //public IList<T> GetSelectedList<T>() where T : class
        //{
        //    throw new System.NotImplementedException();
        //}

        ///// <summary>
        ///// 获取选中的主键数组
        ///// </summary>
        //public int[] GetSelectedID(Func<object, int> getIDFunc)
        //{
        //    throw new System.NotImplementedException();
        //}

        #endregion


        #region 导航
        /// <summary>
        /// 移到首行
        /// </summary>
        public void MoveFirstRow()
        {
            if (this.SortedColumnCount > 0) return;
            if (this.DataSource == null) return;
            if (IsFirstRow || IsFirstNode) return;

            var node = FocusedNode;
            int newIndex = 0;
            this.SetNodeIndex(node, newIndex);
            this.SetFocusedNode(node);
        }

        /// <summary>
        /// 移到最后一行
        /// </summary>
        public void MoveLastRow()
        {
            if (this.SortedColumnCount > 0) return;
            if (this.DataSource == null) return;
            if (IsLastRow || IsLastNode) return;

            var node = FocusedNode;
            int newIndex = GetNodeLevelNodeCount(node) - 1;
            this.SetNodeIndex(node, newIndex);
            this.SetFocusedNode(node);
        }

        /// <summary>
        /// 移到下一行
        /// </summary>
        public void MoveNextRow()
        {
            if (this.SortedColumnCount > 0) return;
            if (this.DataSource == null) return;
            if (IsLastRow || IsLastNode) return;

            var node = FocusedNode;
            int newIndex = GetNodeIndex(node) + 1;
            this.SetNodeIndex(node, newIndex);
            this.SetFocusedNode(node);
        }

        /// <summary>
        /// 移到上一行
        /// </summary>
        public void MovePrevRow()
        {
            if (this.SortedColumnCount > 0) return;
            if (this.DataSource == null) return;
            if (IsFirstRow || IsFirstNode) return;

            var node = FocusedNode;
            int newIndex = GetNodeIndex(node) - 1;
            this.SetNodeIndex(node, newIndex);
            this.SetFocusedNode(node);
        }

        #endregion


        #region 过滤

        public void FilterContainParentNode(string key, params string[] colNames)
        {
            if (key.Length > 0)
            {
                FilterContainParentNodeCore(key, this.Nodes, colNames);
            }
            else
            {
                RecursionTreeNode(this.Nodes, p => p.Visible = true);
            }
        }


        private bool FilterContainParentNodeCore(string key, TreeListNodes nodes, params string[] colNames)
        {
            bool result = false;
            foreach (TreeListNode item in nodes)
            {
                if (item.HasChildren)
                {
                    var ischeck = NodeMatch(key, item, colNames);
                    item.Visible = FilterContainParentNodeCore(key, item.Nodes, colNames) || ischeck;
                }
                else
                {
                    item.Visible = NodeMatch(key, item, colNames);
                }
                result = result || item.Visible;
            }
            return result;
        }

        /// <summary>
        /// 节点值匹配搜索值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <param name="colNames">搜索字段名称数组</param>
        /// <returns></returns>
        private bool NodeMatch(string key, TreeListNode node, params string[] colNames)
        {
            if (colNames.Length > 0)
            {
                foreach (string colName in colNames)
                {
                    string nodeText = node.GetDisplayText(Columns.ColumnByFieldName(colName));
                    bool isVisible = nodeText.IndexOf(key, StringComparison.CurrentCultureIgnoreCase) >= 0;
                    if (isVisible)
                    {
                        return true;
                    }
                }
                return false;
            }

            foreach (TreeListColumn column in Columns)
            {
                string nodeText = node.GetDisplayText(Columns.ColumnByFieldName(column.FieldName));
                bool isVisible = nodeText.IndexOf(key, StringComparison.CurrentCultureIgnoreCase) >= 0;
                if (isVisible)
                {
                    return true;
                }
            }
            return false;
        }
        public void ExpandFirstNode()
        {
            if (Nodes.Count > 0)
            {
                Nodes[0].Expanded = true;
            }
        }
        public void Filter(string key, params string[] colNames)
        {
            this.OptionsFilter.FilterMode = FilterMode.Smart;
            if (key.Length > 0)
            {
                this.ExpandAll();
                StringBuilder sb = new StringBuilder();
                if (colNames.Length > 0)
                {
                    sb.AppendFormat("Contains([{0}],'{1}')", colNames[0], key);
                    for (int index = 1; index < colNames.Length; index++)
                    {
                        string colName = colNames[index];
                        sb.AppendFormat("Or Contains([{0}],'{1}')", colName, key);
                    }
                }
                else if (this.Columns.Count > 0)
                {
                    sb.AppendFormat("Contains([{0}],'{1}')", this.Columns[0].FieldName, key);
                    for (int index = 1; index < this.Columns.Count; index++)
                    {
                        string colName = this.Columns[index].FieldName;
                        sb.AppendFormat("Or Contains([{0}],'{1}')", colName, key);
                    }
                }
                this.AddFilter(sb.ToString());
            }
            else
            {
                this.ClearColumnsFilter();
            }
        }

        #endregion


        #region 复选框

        /// <summary>
        /// 反选树节点
        /// </summary>
        public void InverseCheckAll()
        {
            RecursionTreeNode(Nodes, p => p.Checked = !p.Checked);
        }

        public void CheckParent(TreeListNode node)
        {
            CheckParent(node, null);
        }

        public void CheckParent(TreeListNode node, Action<TreeListNode> action)
        {
            RecursionParentNode(node, p =>
            {
                bool b = false;
                for (int i = 0; i < p.Nodes.Count; i++)
                {
                    CheckState state = p.Nodes[i].CheckState;
                    if (!node.CheckState.Equals(state))
                    {
                        b = true;
                        break;
                    }
                }
                p.CheckState = b ? CheckState.Checked : node.CheckState;
                if (action != null)
                {
                    action(p);
                }
            });
        }

        public void RecursionParentNode(TreeListNode node, Action<TreeListNode> action)
        {
            if (node.ParentNode == null) return;
            var parentNode = node.ParentNode;
            action(parentNode);
            RecursionParentNode(parentNode, action);
        }

        public void CheckChild(TreeListNode node)
        {
            CheckChild(node, null);
        }

        public void CheckChild(TreeListNode node, Action<TreeListNode> action)
        {
            RecursionChildNode(node, p =>
            {
                p.CheckState = node.CheckState;
                if (action != null)
                {
                    action(p);
                }
            });
        }

        public void RecursionChildNode(TreeListNode node, Action<TreeListNode> action)
        {
            if (!node.HasChildren) return;
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                var cnode = node.Nodes[i];
                action(cnode);
                RecursionChildNode(cnode, action);
            }
        }


        public void RecursionTreeNode(TreeListNodes nodes, Action<TreeListNode> action)
        {
            foreach (TreeListNode node in nodes)
            {
                action(node);
                if (node.HasChildren)
                {
                    RecursionTreeNode(node.Nodes, action);
                }
            }
        }

        #endregion


        #region 配置

        /// <summary>
        /// 保存表格配置
        /// </summary>
        public void SaveConfig()
        {
            SaveConfig(Guid.Empty);
        }

        /// <summary>
        /// 保存表格配置
        /// </summary>
        public void SaveConfig(Guid userID)
        {
            GridConfigFactory.Current.SaveConfig(this, userID);
        }

        /// <summary>
        /// 加载表格配置
        /// </summary>
        public void LoadConfig()
        {
            LoadConfig(Guid.Empty);
        }

        /// <summary>
        /// 加载表格配置
        /// </summary>
        public void LoadConfig(Guid userID)
        {
            GridConfigFactory.Current.LoadConfig(this, userID);
        }

        #endregion


        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (!isInit)
            {
                isInit = true;
                if (IsStoreConfig)
                {
                    this.Columns.Clear();
                    LoadConfig();
                    OnAfterLoadConfig(EventArgs.Empty);
                }
                this.OptionsFilter.FilterMode = FilterMode.Smart;
                this.PopupMenuShowing += XCITreeGrid_PopupMenuShowing; //自定义列菜单
                this.CustomDrawNodeIndicator += XCITreeGrid_CustomDrawNodeIndicator; //画行号
                this.ColumnChanged += (o, a) => NoticeConfigChanged(); //列增减发生变化
                this.ColumnWidthChanged += (o, a) => NoticeConfigChanged();//列宽度发生变化 
            }
        }

        void XCITreeGrid_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            if (e.IsNodeIndicator)
            {
                TreeList tree = (DevExpress.XtraTreeList.TreeList)sender;
                IndicatorObjectInfoArgs args = (IndicatorObjectInfoArgs)e.ObjectArgs;
                var index = tree.GetVisibleIndexByNode(e.Node);
                if (index < 0)
                {
                    args.DisplayText = string.Empty;
                }
                else
                {
                    args.DisplayText = (index + 1).ToString();
                }
                e.ImageIndex = -1;
            }
        }

        void XCITreeGrid_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (!IsShowColumnHeardMenu)
            {
                e.Menu.Items.Clear();
            }
            TreeListHitInfo hitInfo = CalcHitInfo(e.Point);
            if (hitInfo.HitInfoType == HitInfoType.Column
                || hitInfo.HitInfoType == HitInfoType.BehindColumn
                || hitInfo.HitInfoType == HitInfoType.RowIndicator)
            {
                AddColumnMenu(e.Menu, hitInfo.Column);
            }
        }

        protected override void Dispose(bool disposing)
        {
            OnBeforeSaveConfig(EventArgs.Empty);
            if (_isConfigChange || MRUFilters.Count > 0)
            {
                this.ClearColumnsFilter();
                this.MRUFilters.Clear();
                if (IsStoreConfig)
                {
                    SaveConfig();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 通知配置发生改变
        /// </summary>
        protected void NoticeConfigChanged()
        {
            this.IsConfigChange = true;
        }
        //private string _parentFieldName = "ParentID";
        //private bool _isShowTree = true;
        //private bool _isExpand = false;
        protected void AddColumnMenu(TreeListMenu menu, TreeListColumn column)
        {
            var item = new DXMenuItem("全部展开", (o, ex) => ExpandAll());
            item.BeginGroup = true;
            menu.Items.Add(item);
            item = new DXMenuItem("全部合上", (o, ex) => CollapseAll());
            menu.Items.Add(item);
            //item = new DXMenuItem("不显示层级", (o, ex) =>
            //{
            //    if (_isShowTree)
            //    {
            //        _isShowTree = false;
            //        _parentFieldName = ParentFieldName;
            //        ParentFieldName = "~";
            //        ((DXMenuItem)o).Caption = "显示层级";
            //    }
            //    else
            //    {
            //        _isShowTree = true;
            //        ParentFieldName = _parentFieldName;
            //        ((DXMenuItem)o).Caption = "不显示层级";
            //    }
            //    ClearSorting();
            //});
            //item.BeginGroup = true;
            //menu.Items.Add(item);

            if (column != null)
            {
                DXMenuItem sortItem = new DXMenuItem("清除排序设置", (oo, ee) =>
                {
                    this.IsConfigChange = true;
                    column.SortOrder = System.Windows.Forms.SortOrder.None;
                    RefreshDataSource();
                }, null);
                sortItem.Enabled = false;
                sortItem.BeginGroup = true;
                menu.Items.Add(sortItem);
                if (column.SortOrder != System.Windows.Forms.SortOrder.None)
                {
                    sortItem.Enabled = true;
                }
            }

            if (IsShowConfigButton)
            {
                DXMenuItem customItem = new DXMenuItem("自定义", delegate { ShowConfigForm(); }, Resources.GridConfig);
                customItem.BeginGroup = true;
                menu.Items.Add(customItem);
            }
        }

        public void ShowConfigForm()
        {
            frmTreeGridConfig config = new frmTreeGridConfig(this);
            config.ShowDialog();
            config.Dispose();
        }

        #region 右键菜单

        /// <summary>
        /// 显示配置右键菜单
        /// </summary>
        public void ShowConfigPopMenu()
        {
            if (MainBar == null) return;
            var _menu = new PopupMenu();
            _menu.Manager = MainBar;
            var configButton = CreateBarButtonItem("自定义",
                XCI.WinUtility.Properties.Resources.GridConfig, ShowConfigForm);
            MainBar.Items.Add(configButton);
            _menu.ItemLinks.Add(configButton, false);
            _menu.ShowPopup(Control.MousePosition);
        }


        /// <summary>
        /// 创建右键菜单项
        /// </summary>
        /// <param name="caption">文字</param>
        /// <param name="image">图片</param>
        /// <param name="action">动作</param>
        public BarButtonItem CreateBarButtonItem(string caption, Image image, Action action)
        {
            BarButtonItem item = new BarButtonItem();
            item.Caption = caption;
            item.Glyph = image;
            item.Name = Guid.NewGuid().ToString();
            if (action != null)
            {
                item.ItemClick += (o, e) => action();
            }
            return item;
        }

        /// <summary>
        /// 添加右键菜单项
        /// </summary>
        /// <param name="caption">文字</param>
        /// <param name="image">图片</param>
        /// <param name="action">动作</param>
        /// <param name="isBeginGroup">是否分组</param>
        public BarButtonItem AddPopMenuItem(string caption, Image image, Action action, bool isBeginGroup)
        {
            return AddPopMenuItem(CreateBarButtonItem(caption, null, action), isBeginGroup);
        }

        /// <summary>
        /// 添加右键菜单项
        /// </summary>
        /// <param name="item">右键菜单项</param>
        /// <param name="isBeginGroup">是否分组</param>
        public BarButtonItem AddPopMenuItem(BarButtonItem item, bool isBeginGroup)
        {
            if (MainBar != null)
            {
                MainBar.Items.Add(item);
                PopMenu.ItemLinks.Add(item, isBeginGroup);
            }
            return item;
        }

        public BarButtonItem InsertPopMenuItem(string caption, Image image, Action action, int index)
        {
            return InsertPopMenuItem(CreateBarButtonItem(caption, image, action), index);
        }


        public BarButtonItem InsertPopMenuItem(BarButtonItem item, int index)
        {
            if (MainBar != null)
            {
                MainBar.Items.Add(item);
                if (PopMenu != null)
                {
                    PopMenu.ItemLinks.Insert(index, item);
                }
                return item;
            }
            return null;
        }

        public void ClearPopMenu()
        {
            if (MainBar != null)
            {
                MainBar.Items.Clear();
                PopMenu.ItemLinks.Clear();
            }
        }

        #endregion
    }
}