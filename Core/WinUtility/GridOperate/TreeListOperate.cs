using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using XCI.Component;
using XCI.Core;
using XCI.Extension;
using XCI.Helper;
using XCI.WinUtility.Properties;

namespace XCI.WinUtility
{
    /// <summary>
    /// 数据列表操作
    /// </summary>
    /// <typeparam name="E">实体类型</typeparam>
    /// <typeparam name="S">接口类型</typeparam>
    public class TreeListOperate<E, S>
        where E : EntityBase
        where S : class,IEntityService<E>
    {

        #region 字段

        private EntityMetadata _metadata;
        private Dictionary<object, Dictionary<string, object>> _changedDic;
        private bool _isEnableContextMenu = true;
        private bool _isRecordChangedValue = true;

        #endregion

        #region 权限

        private bool _allowLoad = true;
        public bool AllowLoad
        {
            get { return _allowLoad; }
            set { _allowLoad = value; }
        }

        private bool _allowCreate = true;
        public bool AllowCreate
        {
            get { return _allowCreate; }
            set { _allowCreate = value; }
        }

        private bool _allowEdit = true;
        public bool AllowEdit
        {
            get { return _allowEdit; }
            set { _allowEdit = value; }
        }

        private bool _allowDelete = true;
        public bool AllowDelete
        {
            get { return _allowDelete; }
            set { _allowDelete = value; }
        }

        private bool _allowExport = true;
        public bool AllowExport
        {
            get { return _allowExport; }
            set { _allowExport = value; }
        }

        #endregion

        #region 属性

        private bool _isBindDoubleClick = true;
        public bool IsBindDoubleClick
        {
            get { return _isBindDoubleClick; }
            set { _isBindDoubleClick = value; }
        }


        private List<int> _copyUserIdList;
        protected List<int> CopyIDList
        {
            get { return _copyUserIdList ?? (_copyUserIdList = new List<int>()); }
        }

        private bool _isShowRecycleBinButton = true;

        /// <summary>
        /// 是否显示回收站按钮
        /// </summary>
        public bool IsShowRecycleBinButton
        {
            get { return _isShowRecycleBinButton; }
            set { _isShowRecycleBinButton = value; }
        }

        /// <summary>
        /// 是否自动选中父节点
        /// </summary>
        public bool IsAutoCheckParentNode { get; set; }

        /// <summary>
        /// 是否自动选中子节点
        /// </summary>
        public bool IsAutoCheckChildNode { get; set; }

        /// <summary>
        /// 表格控件
        /// </summary>
        public XCITreeGrid Grid { get; set; }

        /// <summary>
        /// 所属窗口
        /// </summary>
        public Form OwnerForm { get; set; }


        public Type SplashFormType { get; set; }

        /// <summary>
        /// 搜索框控件
        /// </summary>
        public XCIButtonEdit SearchEdit { get; set; }

        /// <summary>
        /// 新建按钮
        /// </summary>
        public SimpleButton NewButton { get; set; }

        /// <summary>
        /// 编辑按钮
        /// </summary>
        public SimpleButton EditButton { get; set; }

        /// <summary>
        /// 删除按钮
        /// </summary>
        public SimpleButton DeleteButton { get; set; }

        /// <summary>
        /// 导出按钮
        /// </summary>
        public SimpleButton ExportButton { get; set; }


        /// <summary>
        /// 全选复选框按钮
        /// </summary>
        public SimpleButton SelectAllButton { get; set; }

        /// <summary>
        /// 反选复选框按钮
        /// </summary>
        public SimpleButton SelectInverseButton { get; set; }

        /// <summary>
        /// 清除复选框按钮
        /// </summary>
        public SimpleButton SelectCleanButton { get; set; }

        /// <summary>
        /// 复制复选框ID按钮
        /// </summary>
        public SimpleButton CopySelectButton { get; set; }

        /// <summary>
        /// 粘贴复选框ID按钮
        /// </summary>
        public SimpleButton PasteSelectButton { get; set; }

        public BarButtonItem NewRootBarItemButton { get; set; }

        /// <summary>
        /// 右键新建按钮
        /// </summary>
        public BarButtonItem NewBarItemButton { get; set; }

        /// <summary>
        /// 右键编辑按钮
        /// </summary>
        public BarButtonItem EditBarItemButton { get; set; }

        /// <summary>
        /// 右键删除按钮
        /// </summary>
        public BarButtonItem DeleteBarItemButton { get; set; }

        /// <summary>
        /// 配置按钮
        /// </summary>
        public BarButtonItem ConfigBarItemButton { get; set; }

        public BarButtonItem ExpandBarItemButton { get; set; }

        public BarButtonItem ExpandAllBarItemButton { get; set; }

        public bool IsEditRoot { get; set; }

        /// <summary>
        /// 是否自动刷新
        /// </summary>
        public bool IsAutoRefresh { get; set; }

        /// <summary>
        /// 自动刷新间隔 单位毫秒 默认10分钟
        /// </summary>
        public int AutoRefreshInterval { get; set; }

        /// <summary>
        /// 是否启用右键菜单
        /// </summary>
        public bool IsEnableContextMenu
        {
            get { return _isEnableContextMenu; }
            set { _isEnableContextMenu = value; }
        }
        /// <summary>
        /// 是否记录改变的列值
        /// </summary>
        public bool IsRecordChangedValue
        {
            get { return _isRecordChangedValue; }
            set { _isRecordChangedValue = value; }
        }

        /// <summary>
        /// 组件是否初始化
        /// </summary>
        public bool IsInitialize { get; protected set; }


        /// <summary>
        /// 实体元数据
        /// </summary>
        public EntityMetadata Metadata
        {
            get { return _metadata ?? (_metadata = EntityMetadataFactory.Current.Get(typeof(E).FullName)); }
        }

        public BaseFactory<S> Factory { get; set; }

        /// <summary>
        /// 实体操作实现
        /// </summary>
        public IEntityService<E> Service
        {
            get { return Factory.Default; }
        }

        /// <summary>
        /// 改变的数据字典 (主键 (列名 列值))
        /// </summary>
        protected Dictionary<object, Dictionary<string, object>> ChangedDic
        {
            get
            {
                return _changedDic ??
                       (_changedDic = new Dictionary<object, Dictionary<string, object>>());
            }
        }


        /// <summary>
        /// 记录操作图片
        /// </summary>
        public ImageList RecordImgList { get; set; }

        /// <summary>
        /// 排序图片
        /// </summary>
        public ImageList SortImgList { get; set; }

        /// <summary>
        /// 选择图片
        /// </summary>
        public ImageList SelectImgList { get; set; }

        /// <summary>
        /// 右键图片
        /// </summary>
        public ImageList PopMenuImgList { get; set; }

        /// <summary>
        /// 获取实体主键函数
        /// </summary>
        public Func<E, object> GetEntityPKFunc { get; set; }


        private string _deleteConfirmMessage = "确定要删除选中的记录吗?";
        public string DeleteConfirmMessage
        {
            get { return _deleteConfirmMessage; }
            set { _deleteConfirmMessage = value; }
        }

        /// <summary>
        /// 检测权限
        /// </summary>
        public Action CheckAuthorized { get; set; }

        #endregion

        #region 事件

        #region 加载之前事件

        /// <summary>
        /// 数据加载之前
        /// </summary>
        public event EventHandler<ListLoadEventArgs> BeforeLoad;

        /// <summary>
        /// 触发数据加载之前事件
        /// </summary>
        /// <param name="e">数据加载之前事件参数</param>
        protected void OnBeforeLoad(ListLoadEventArgs e)
        {
            if (BeforeLoad != null)
            {
                BeforeLoad(this, e);
            }
        }

        #endregion

        #region 加载之后事件

        /// <summary>
        /// 数据加载之后
        /// </summary>
        public event EventHandler<ListLoadEventArgs> AfterLoad;

        /// <summary>
        /// 触发数据加载之后事件
        /// </summary>
        /// <param name="e">加载之后事件参数</param>
        protected void OnAfterLoad(ListLoadEventArgs e)
        {
            if (AfterLoad != null)
            {
                AfterLoad(this, e);
            }
        }

        #endregion

        #region 编辑之前事件(内部编辑器事件)

        /// <summary>
        /// 数据编辑之前(内部编辑器事件)
        /// </summary>
        public event EventHandler<ListEditEventArgs> BeforeEdit;

        /// <summary>
        /// 触发数据编辑之前事件
        /// </summary>
        /// <param name="e">数据编辑之前事件参数</param>
        protected void OnBeforeEdit(ListEditEventArgs e)
        {
            if (BeforeEdit != null)
            {
                BeforeEdit(this, e);
            }
        }

        #endregion

        #region 编辑之后事件(内部编辑器事件)

        /// <summary>
        /// 数据编辑之后(内部编辑器事件)
        /// </summary>
        public event EventHandler<ListEditEventArgs> AfterEdit;

        /// <summary>
        /// 触发数据编辑之后事件
        /// </summary>
        /// <param name="e">数据编辑之后事件参数</param>
        protected void OnAfterEdit(ListEditEventArgs e)
        {
            if (AfterEdit != null)
            {
                AfterEdit(this, e);
            }
        }

        #endregion

        #region 新建事件

        /// <summary>
        /// 数据新建
        /// </summary>
        public event EventHandler<ListEditEventArgs> New;

        /// <summary>
        /// 触发数据新建事件
        /// </summary>
        /// <param name="e">数据新建事件参数</param>
        protected void OnNew(ListEditEventArgs e)
        {
            if (New != null)
            {
                New(this, e);
            }
        }

        #endregion

        #region 编辑事件

        /// <summary>
        /// 数据编辑
        /// </summary>
        public event EventHandler<ListEditEventArgs> Edit;

        /// <summary>
        /// 触发数据编辑事件
        /// </summary>
        /// <param name="e">数据编辑事件参数</param>
        protected void OnEdit(ListEditEventArgs e)
        {
            if (Edit != null)
            {
                Edit(this, e);
            }
        }

        #endregion

        #region 显示编辑窗口事件

        /// <summary>
        /// 数据编辑
        /// </summary>
        public event EventHandler<ListEditEventArgs> ShowEdit;

        /// <summary>
        /// 触发数据编辑事件
        /// </summary>
        /// <param name="e">数据编辑事件参数</param>
        protected void OnShowEdit(ListEditEventArgs e)
        {
            if (ShowEdit != null)
            {
                ShowEdit(this, e);
            }
        }

        #endregion

        #region 删除之前事件

        /// <summary>
        /// 数据删除之前
        /// </summary>
        public event EventHandler<ListDeleteEventArgs> BeforeDelete;

        /// <summary>
        /// 触发数据删除之前事件
        /// </summary>
        /// <param name="e">删除之前事件参数</param>
        protected void OnBeforeDelete(ListDeleteEventArgs e)
        {
            if (BeforeDelete != null)
            {
                BeforeDelete(this, e);
            }
        }

        #endregion

        #region 删除之后事件

        /// <summary>
        /// 数据删除之后
        /// </summary>
        public event EventHandler<ListDeleteEventArgs> AfterDelete;

        /// <summary>
        /// 触发数据删除之后事件
        /// </summary>
        /// <param name="e">删除之后事件参数</param>
        protected void OnAfterDelete(ListDeleteEventArgs e)
        {
            if (AfterDelete != null)
            {
                AfterDelete(this, e);
            }
        }

        #endregion

        #region 选中事件

        /// <summary>
        /// 数据选中
        /// </summary>
        public event EventHandler<ListSelectedEventArgs> Selected;

        /// <summary>
        /// 触发数据选中事件
        /// </summary>
        /// <param name="e">选中事件参数</param>
        protected void OnSelected(ListSelectedEventArgs e)
        {
            if (Selected != null)
            {
                Selected(this, e);
            }
        }

        #endregion

        #region 选中之后事件

        /// <summary>
        /// 数据选中之后
        /// </summary>
        public event EventHandler<ListSelectedEventArgs> AfterSelected;

        /// <summary>
        /// 触发数据选中之后事件
        /// </summary>
        /// <param name="e">选中之后事件参数</param>
        protected void OnAfterSelected(ListSelectedEventArgs e)
        {
            if (AfterSelected != null)
            {
                AfterSelected(this, e);
            }
        }

        #endregion

        #region 列值变化之前事件

        /// <summary>
        /// 列值变化之前
        /// </summary>
        public event EventHandler<ListCellValueChanged> BeforeCellValueChanged;

        /// <summary>
        /// 触发列值变化之前
        /// </summary>
        /// <param name="e">列值变化之前参数</param>
        protected void OnBeforeCellValueChanged(ListCellValueChanged e)
        {
            if (BeforeCellValueChanged != null)
            {
                BeforeCellValueChanged(this, e);
            }
        }

        #endregion

        #region 列值变化之后事件

        /// <summary>
        /// 触发列值变化之后
        /// </summary>
        public event EventHandler<ListCellValueChanged> AfterCellValueChanged;

        /// <summary>
        /// 触发触发列值变化之后事件
        /// </summary>
        /// <param name="e">触发列值变化之后事件参数</param>
        protected void OnAfterCellValueChanged(ListCellValueChanged e)
        {
            if (AfterCellValueChanged != null)
            {
                AfterCellValueChanged(this, e);
            }
        }

        #endregion


        #region 列值变化即时事件

        /// <summary>
        /// 列值变化即时
        /// </summary>
        public event EventHandler<ListCellValueChanged> CellValueChanging;

        /// <summary>
        /// 触发列值变化即时
        /// </summary>
        /// <param name="e">列值变化即时参数</param>
        protected void OnCellValueChanging(ListCellValueChanged e)
        {
            if (CellValueChanging != null)
            {
                CellValueChanging(this, e);
            }
        }

        #endregion

        #region 复选框状态变化事件

        /// <summary>
        /// 复选框状态变化事件
        /// </summary>
        public event EventHandler<ListCheckedChangedEventArgs> CheckedChanged;

        /// <summary>
        /// 复选框状态变化事件
        /// </summary>
        /// <param name="e">复选框状态变化事件参数</param>
        protected void OnCheckedChanged(ListCheckedChangedEventArgs e)
        {
            if (CheckedChanged != null)
            {
                CheckedChanged(this, e);
            }
        }

        #endregion


        #region 上移之前事件

        /// <summary>
        /// 上移之前事件
        /// </summary>
        public event EventHandler<ListSelectedEventArgs> BeforeMoveUp;

        /// <summary>
        /// 上移之前事件
        /// </summary>
        /// <param name="e">上移之前事件参数</param>
        protected void OnBeforeMoveUp(ListSelectedEventArgs e)
        {
            if (BeforeMoveUp != null)
            {
                BeforeMoveUp(this, e);
            }
        }

        #endregion

        #region 下移之前事件

        /// <summary>
        /// 下移之前事件
        /// </summary>
        public event EventHandler<ListSelectedEventArgs> BeforeMoveDown;

        /// <summary>
        /// 下移之前事件
        /// </summary>
        /// <param name="e">下移之前事件参数</param>
        protected void OnBeforeMoveDown(ListSelectedEventArgs e)
        {
            if (BeforeMoveDown != null)
            {
                BeforeMoveDown(this, e);
            }
        }

        #endregion

        #region 列过滤事件

        /// <summary>
        /// 列过滤事件
        /// </summary>
        public event EventHandler<EventArgs> ColumnFilterChanged;

        /// <summary>
        /// 列过滤事件事件
        /// </summary>
        /// <param name="e">列过滤事件事件参数</param>
        protected void OnColumnFilterChanged(EventArgs e)
        {
            if (ColumnFilterChanged != null)
            {
                ColumnFilterChanged(this, e);
            }
        }

        #endregion

        #region 数据导出之前事件

        /// <summary>
        /// 数据导出之前
        /// </summary>
        public event EventHandler<ListOperateBaseEventArgs> BeforeExport;

        /// <summary>
        /// 触发数据导出之前事件
        /// </summary>
        /// <param name="e">导出之前事件参数</param>
        protected void OnBeforeExport(ListOperateBaseEventArgs e)
        {
            if (BeforeExport != null)
            {
                BeforeExport(this, e);
            }
        }

        #endregion


        #region 右键菜单显示之前事件

        /// <summary>
        /// 右键菜单显示之前事件
        /// </summary>
        public event EventHandler<EventArgs> BeforePopMenuShow;

        /// <summary>
        /// 右键菜单显示之前事件
        /// </summary>
        /// <param name="e">右键菜单显示之前事件参数</param>
        protected void OnBeforePopMenuShow(EventArgs e)
        {
            if (BeforePopMenuShow != null)
            {
                BeforePopMenuShow(this, e);
            }
        }

        #endregion

        #region 右键菜单显示之后事件

        /// <summary>
        /// 下移之前事件
        /// </summary>
        public event EventHandler<EventArgs> AfterPopMenuShow;

        /// <summary>
        /// 下移之前事件
        /// </summary>
        /// <param name="e">下移之前事件参数</param>
        protected void OnAfterPopMenuShow(EventArgs e)
        {
            if (AfterPopMenuShow != null)
            {
                AfterPopMenuShow(this, e);
            }
        }

        #endregion

        #endregion

        #region 保护方法

        /// <summary>
        /// 初始化右键菜单
        /// </summary>
        protected virtual void InitializePopupMenu()
        {
            if (IsEnableContextMenu)
            {
                PopMenuImgList = new ImageList();
                PopMenuImgList.ImageSize = new Size(16, 16);
                PopMenuImgList.Images.AddRange(new Image[]{
                    XCI.WinUtility.Properties.Resources.TreeAddRoot,
                    XCI.WinUtility.Properties.Resources.TreeAdd,
                    XCI.WinUtility.Properties.Resources.TreeEdit,
                    XCI.WinUtility.Properties.Resources.TreeDelete,
                    XCI.WinUtility.Properties.Resources.GridConfig
                });

                NewRootBarItemButton = Grid.CreateBarButtonItem("新建根纪录", PopMenuImgList.Images[0], NewRootMethod);
                NewBarItemButton = Grid.CreateBarButtonItem("新建新纪录", PopMenuImgList.Images[1], NewMethod);
                EditBarItemButton = Grid.CreateBarButtonItem("编辑纪录", PopMenuImgList.Images[2], EditMethod);
                DeleteBarItemButton = Grid.CreateBarButtonItem("删除纪录", PopMenuImgList.Images[3], DeleteMethod);

                NewRootBarItemButton.Enabled = false;
                NewBarItemButton.Enabled = false;
                NewBarItemButton.Enabled = false;
                DeleteBarItemButton.Enabled = false;

                Grid.AddPopMenuItem(NewRootBarItemButton, false);
                Grid.AddPopMenuItem(NewBarItemButton, true);
                Grid.AddPopMenuItem(EditBarItemButton, true);
                Grid.AddPopMenuItem(DeleteBarItemButton, true);


                ExpandBarItemButton = Grid.CreateBarButtonItem("展开/合上", null, Expand);
                ExpandAllBarItemButton = Grid.CreateBarButtonItem("展开全部", null, ExpandAll);

                Grid.AddPopMenuItem(ExpandBarItemButton, true);
                Grid.AddPopMenuItem(ExpandAllBarItemButton, false);

                if (Grid.IsShowConfigButton)
                {
                    ConfigBarItemButton = Grid.CreateBarButtonItem("自定义", PopMenuImgList.Images[4], Grid.ShowConfigForm);
                    Grid.AddPopMenuItem(ConfigBarItemButton, true);
                }
            }
        }

        protected void Expand()
        {
            if (this.Grid.FocusedNode != null)
            {
                this.Grid.FocusedNode.Expanded = !this.Grid.FocusedNode.Expanded;
            }
        }

        protected void ExpandAll()
        {
            if (this.Grid.FocusedNode != null)
            {
                this.Grid.FocusedNode.ExpandAll();
            }
        }

        /// <summary>
        /// 初始化表格
        /// </summary>
        protected void InitializeGrid()
        {
            if (IsBindDoubleClick) Grid.DoubleClick += View_DoubleClick;//双击行

            Grid.MouseUp += View_MouseUp; //鼠标放开时 右键菜单
            Grid.NodeCellStyle += Grid_NodeCellStyle;
            Grid.CellValueChanging += View_CellValueChanging; //列值发生变化
            Grid.CellValueChanged += View_CellValueChanged; //列值发生变化
            Grid.ShowingEditor += View_ShowingEditor;//显示行编辑器
            Grid.GridConfigChangeNotify += (o, e) => SetCommitButtonStatus(); //表格配置改变通知;
            Grid.BeforeSaveConfig += Grid_BeforeSaveConfig;

            Grid.AfterCheckNode += new NodeEventHandler(Grid_AfterCheckNode);
            Grid.BeforeDragNode += new BeforeDragNodeEventHandler(Grid_BeforeDragNode);
            Grid.AfterDragNode += XCIGrid_AfterDragNode;
            Grid.FocusedNodeChanged += XCIGrid_FocusedNodeChanged;
            if (Metadata != null)
            {
                Grid.KeyFieldName = Metadata.PrimaryKeyFieldName;
                Grid.ParentFieldName = Metadata.ParentFieldName;
            }

            if (Metadata != null && !string.IsNullOrEmpty(Metadata.DeleteFieldName))
            {
                Grid.PopupMenuShowing += Grid_PopupMenuShowing;
            }
        }

        void Grid_AfterCheckNode(object sender, NodeEventArgs e)
        {
            if (IsAutoCheckParentNode)
            {
                Grid.CheckParent(e.Node);
            }
            if (Grid.IsAutoDeleteChild || IsAutoCheckChildNode)
            {
                Grid.CheckChild(e.Node);
            }
        }


        void Grid_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            TreeListHitInfo hitInfo = Grid.CalcHitInfo(e.Point);
            if (IsShowRecycleBinButton && (hitInfo.HitInfoType == HitInfoType.Column
                || hitInfo.HitInfoType == HitInfoType.BehindColumn
                || hitInfo.HitInfoType == HitInfoType.RowIndicator))
            {
                Icon icon = new Icon(XCI.WinUtility.Properties.Resources.RecycleBin, new Size(16, 16));
                var image = icon.ToBitmap();
                DXMenuItem customItem = new DXMenuItem("回收站", delegate { ShowRecycleBinForm(); }, image);
                customItem.BeginGroup = true;
                e.Menu.Items.Add(customItem);
            }
        }

        public void ShowRecycleBinForm()
        {
            frmTreeRecycleBin form = new frmTreeRecycleBin();
            form.Operate = this;
            form.ShowDialog();
            form.Dispose();
        }

        private TreeListNode dragNode;
        void Grid_BeforeDragNode(object sender, BeforeDragNodeEventArgs e)
        {
            if (!IsRecordChangedValue) return;
            dragNode = (TreeListNode)e.Node.Clone();
        }

        void XCIGrid_AfterDragNode(object sender, NodeEventArgs e)
        {
            if (!IsRecordChangedValue) return;
            string sortName = Metadata.SortCodeFieldName;
            if (e.Node != null && dragNode != null && !string.IsNullOrEmpty(sortName))
            {
                if (GetPKByNode(dragNode.ParentNode) != GetPKByNode(e.Node.ParentNode))
                {
                    TreeListNodes nodes;
                    if (dragNode.ParentNode == null)
                    {
                        nodes = Grid.Nodes;
                    }
                    else
                    {
                        nodes = dragNode.ParentNode.Nodes;
                    }

                    foreach (TreeListNode item in nodes)
                    {
                        AddOrUpdateChanged(GetPKByNode(item), sortName, Grid.GetNodeIndex(item));
                    }
                }
            }
            if (e.Node.ParentNode == null)
            {
                foreach (TreeListNode item in Grid.Nodes)
                {
                    AddOrUpdateChanged(GetPKByNode(item), sortName, Grid.GetNodeIndex(item));
                }
                if (GetPKByNode(dragNode.ParentNode) != GetPKByNode(e.Node.ParentNode))
                {
                    AddOrUpdateChanged(GetPKByNode(e.Node), Metadata.ParentFieldName, 0);
                }
            }
            else
            {
                foreach (TreeListNode item in e.Node.ParentNode.Nodes)
                {
                    AddOrUpdateChanged(GetPKByNode(item), sortName, Grid.GetNodeIndex(item));
                }
                if (GetPKByNode(dragNode.ParentNode) != GetPKByNode(e.Node.ParentNode))
                {
                    AddOrUpdateChanged(GetPKByNode(e.Node), Metadata.ParentFieldName, GetPKByNode(e.Node.ParentNode));
                }
            }

        }

        object GetPKByNode(TreeListNode node)
        {
            var entity = Grid.Get(node);
            if (entity != null)
            {
                E entityBase = entity as E;
                if (entityBase != null)
                {
                    return entityBase.ID;
                }
                DataRow dr = entity as DataRow;
                if (dr != null)
                {
                    return dr[Metadata.PrimaryKeyFieldName];
                }
            }
            return -1;
        }

        public TreeListNode GetParnentNode(bool isNew)
        {
            TreeListNode parentNode = null;
            if (IsEditRoot)
            {
                parentNode = null;
            }
            else if (isNew)
            {
                parentNode = Grid.FocusedNode;
            }
            else
            {
                parentNode = Grid.FocusedNode.ParentNode;
            }
            return parentNode;
        }

        void Grid_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Checked)
            {
                e.Appearance.BackColor = Color.FromArgb(255, 255, 192);
                e.Appearance.ForeColor = Color.Black;
                //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        void Grid_BeforeSaveConfig(object sender, EventArgs e)
        {
            if (OwnerForm != null && SplashFormType != null)
            {
                SplashScreenManager.ShowForm(OwnerForm, SplashFormType, true, true, false);
                SplashScreenManager.Default.SetWaitFormDescription("正在保存配置...");
            }

            this.SaveData();

            if (OwnerForm != null && SplashFormType != null)
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        void View_DoubleClick(object sender, EventArgs e)
        {
            var ev = e as MouseEventArgs;
            if (ev != null)
            {
                if (ev.Clicks == 2
                    &&
                    (Grid.CalcHitInfo(ev.Location).HitInfoType == HitInfoType.ColumnEdge
                     || Grid.CalcHitInfo(ev.Location).HitInfoType == HitInfoType.Button
                     || Grid.CalcHitInfo(ev.Location).HitInfoType == HitInfoType.Empty
                    ))
                {
                    return;
                }
            }
            EditMethod();
        }

        /// <summary>
        /// 鼠标放开时 关联右键菜单
        /// </summary>
        void View_MouseUp(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hi = Grid.CalcHitInfo(e.Location);
            if (IsEnableContextMenu && e.Button == MouseButtons.Right)
            {
                OnBeforePopMenuShow(EventArgs.Empty);
                if (hi.HitInfoType == HitInfoType.Cell)
                {
                    if (hi.Node != null)
                    {
                        Grid.FocusedNode = hi.Node;
                    }
                }
                if (hi.HitInfoType == HitInfoType.Empty)
                {
                    EditBarItemButton.Enabled = false;
                    DeleteBarItemButton.Enabled = false;
                }
                if (!Grid.IsAutoDeleteChild && Grid.FocusedNode != null && Grid.FocusedNode.HasChildren)
                {
                    if (DeleteBarItemButton != null) DeleteBarItemButton.Enabled = false;
                    if (DeleteButton != null) DeleteButton.Enabled = false;
                }
                if (Grid.FocusedNode != null && Grid.FocusedNode.HasChildren)
                {
                    if (ExpandBarItemButton != null) ExpandBarItemButton.Enabled = true;
                    if (ExpandAllBarItemButton != null) ExpandAllBarItemButton.Enabled = true;
                }
                else
                {
                    if (ExpandBarItemButton != null) ExpandBarItemButton.Enabled = true;
                    if (ExpandAllBarItemButton != null) ExpandAllBarItemButton.Enabled = true;
                }
                SetButtonStatus();
                if (hi.HitInfoType == HitInfoType.Cell || hi.HitInfoType == HitInfoType.Empty)
                {
                    Grid.PopMenu.ShowPopup(Control.MousePosition);
                    OnAfterPopMenuShow(EventArgs.Empty);
                }
            }

            if (!IsEnableContextMenu && hi.HitInfoType == HitInfoType.Empty && e.Button == MouseButtons.Right)
            {
                Grid.ShowConfigPopMenu();
            }
        }

        void XCIGrid_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            ListSelectedEventArgs args = new ListSelectedEventArgs();
            args.Entity = GetSelectEntity();
            OnSelected(args);
            if (args.IsSuccess)
            {
                SetButtonStatus();

                ListSelectedEventArgs afterArgs = new ListSelectedEventArgs();
                afterArgs.Entity = GetSelectEntity();
                OnAfterSelected(afterArgs);
            }
        }
        /// <summary>
        /// 列值发生变化
        /// </summary>
        void View_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Node == null) return;
            //if (e.Column.FieldName.Equals(SelectedFieldName))
            //{
            //    View.SetRowCellValue(e.RowHandle, SelectedFieldName, e.Value);
            //}

            if (IsRecordChangedValue)
            {
                object entity = Grid.Get(e.Node);
                int ID = -1;
                if (entity is DataRow)
                {
                    ID = (entity as DataRow)[Metadata.PrimaryKeyFieldName].ToInt();
                }
                else if (entity is EntityBase)
                {
                    ID = (entity as EntityBase).ID;
                }
                string columnName = e.Column.FieldName;
                object columnValue = e.Value;

                //bool isSelectColumn = IsSelectColumn(columnName);
                ListCellValueChanged args = new ListCellValueChanged();
                args.ColumnName = columnName;
                args.ColumnValue = columnValue;
                args.Entity = entity;
                args.EntityID = ID;
                //args.IsSelectColumn = isSelectColumn;
                OnCellValueChanging(args);
            }
        }

        /// <summary>
        /// 列值发生变化
        /// </summary>
        void View_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (IsRecordChangedValue && e.Node != null)
            {
                object entity = Grid.Get(e.Node);
                int ID = -1;
                if (entity is DataRow)
                {
                    ID = (entity as DataRow)[Metadata.PrimaryKeyFieldName].ToInt();
                }
                else if (entity is EntityBase)
                {
                    ID = (entity as EntityBase).ID;
                }
                string columnName = e.Column.FieldName;
                object columnValue = e.Value;

                //bool isSelectColumn = IsSelectColumn(columnName);
                ListCellValueChanged beforeArgs = new ListCellValueChanged();
                beforeArgs.ColumnName = columnName;
                beforeArgs.ColumnValue = columnValue;
                beforeArgs.Entity = entity;
                beforeArgs.EntityID = ID;
                //beforeArgs.IsSelectColumn = isSelectColumn;
                OnBeforeCellValueChanged(beforeArgs);//触发列值变化前事件
                if (!beforeArgs.IsSuccess) //事件拦截
                {
                    return;
                }

                //if (isSelectColumn)
                //{
                //    ListCheckedChangedEventArgs checkArgs = new ListCheckedChangedEventArgs();
                //    checkArgs.Checked = e.Value.ToBool();
                //    checkArgs.Entity = entity;
                //    OnCheckedChanged(checkArgs);
                //}
                //else
                {
                    AddOrUpdateChanged(ID, columnName, columnValue);
                }

                ListCellValueChanged afterArgs = new ListCellValueChanged();
                afterArgs.ColumnName = columnName;
                afterArgs.ColumnValue = columnValue;
                afterArgs.Entity = entity;
                afterArgs.EntityID = ID;
                //afterArgs.IsSelectColumn = isSelectColumn;
                OnAfterCellValueChanged(afterArgs);
            }
        }


        /// <summary>
        /// 显示行编辑器
        /// </summary>
        void View_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Metadata != null && Grid.FocusedColumn.FieldName.Equals(Metadata.PrimaryKeyFieldName))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 初始化搜索框控件
        /// </summary>
        protected virtual void InitializeSearchEdit()
        {
            if (SearchEdit != null)
            {
                SearchEdit.AllowHtmlTextInToolTip = DefaultBoolean.True;
                SearchEdit.ToolTip = "请输入关键字进行搜索 语法如下:<br> 1 指定列 列名:关键字<br> 2 并且关系+ 排除-<br> 3 搜索带空格时加引号";
                SearchEdit.Properties.Buttons.Clear();
                SearchEdit.Properties.Buttons.AddRange(
                new[] {
                    new EditorButton(ButtonPredefines.Glyph, "清空", -1, true, true, false, ImageLocation.MiddleCenter, Properties.Resources.SearchClean, new KeyShortcut(Keys.None), "清空搜索框"),
                    new EditorButton(ButtonPredefines.Glyph, "提交", -1, false, true, false, ImageLocation.MiddleCenter, Properties.Resources.SearchCommit,  new KeyShortcut(Keys.None), "提交改变的数据"),
                    new EditorButton(ButtonPredefines.Glyph, "刷新", -1, true, true, false, ImageLocation.MiddleCenter, Properties.Resources.SearchFind,  new KeyShortcut(Keys.None), "刷新当前表格数据")
                });

                if (IsAutoRefresh)
                {
                    SearchEdit.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Glyph, "自动刷新", -1, true, true,
                                                                       false, ImageLocation.MiddleCenter,
                                                                       Properties.Resources.SearchCommitRefresh,
                                                                       new KeyShortcut(Keys.None), "自动刷新数据 当前没有开启自动刷新"));
                }
                SearchEdit.WaterMarkInit();
                SearchEdit.ButtonPressed += SearchEdit_ButtonPressed;
                SearchEdit.KeyDown += SearchEdit_KeyDown;
                SearchEdit.EditValueChanged += (o, e) => Grid.Filter(SearchEdit.Text.Trim());
            }
        }

        void SearchEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                Grid.MovePrev();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                Grid.MoveNext();
                e.Handled = true;
            }
        }

        /// <summary>
        /// 初始化编辑按钮事件
        /// </summary>
        protected void InitializeButtonEvent()
        {
            RecordImgList = new ImageList();
            RecordImgList.ImageSize = new Size(16, 16);
            RecordImgList.Images.AddRange(new Image[]{
                    XCI.WinUtility.Properties.Resources.RecordAdd,
                    XCI.WinUtility.Properties.Resources.RecordEdit,
                    XCI.WinUtility.Properties.Resources.RecordDelete,
                    XCI.WinUtility.Properties.Resources.RecordExport
                });

            SortImgList = new ImageList();
            SortImgList.ImageSize = new Size(16, 16);
            SortImgList.Images.AddRange(new Image[]{
                XCI.WinUtility.Properties.Resources.goTop,
                XCI.WinUtility.Properties.Resources.goUp,
                XCI.WinUtility.Properties.Resources.goDown,
                XCI.WinUtility.Properties.Resources.goBottom
            });

            SelectImgList = new ImageList();
            SelectImgList.ImageSize = new Size(16, 16);
            SelectImgList.Images.AddRange(new Image[]{
                XCI.WinUtility.Properties.Resources.SelectAll,
                XCI.WinUtility.Properties.Resources.SelectInverse
            });

            if (NewButton != null)
            {
                NewButton.Click += (o, e) => NewMethod();
                NewButton.ImageList = RecordImgList;
                NewButton.ImageIndex = 0;
                NewButton.ToolTip = "新增记录";
                NewButton.Enabled = false;
            }
            if (EditButton != null)
            {
                EditButton.Click += (o, e) => EditMethod();
                EditButton.ImageList = RecordImgList;
                EditButton.ImageIndex = 1;
                EditButton.ToolTip = "编辑记录";
                EditButton.Enabled = false;
            }
            if (DeleteButton != null)
            {
                DeleteButton.Click += (o, e) => DeleteMethod();
                DeleteButton.ImageList = RecordImgList;
                DeleteButton.ImageIndex = 2;
                DeleteButton.ToolTip = "删除记录";
                DeleteButton.Enabled = false;
            }
            if (ExportButton != null)
            {
                ExportButton.Click += (o, e) => ExportMethod();
                ExportButton.ImageList = RecordImgList;
                ExportButton.ImageIndex = 3;
                ExportButton.ToolTip = "导出记录";
                ExportButton.Enabled = false;
            }

            if (SelectCleanButton != null)
            {
                SelectCleanButton.Click += (o, e) => SelectCleanAllMethod();
            }
            if (SelectAllButton != null)
            {
                SelectAllButton.Click += (o, e) => SelectAllMethod();
                SelectAllButton.ImageList = SelectImgList;
                SelectAllButton.ImageIndex = 0;
                SelectAllButton.ToolTip = "全选";
            }
            if (SelectInverseButton != null)
            {
                SelectInverseButton.Click += (o, e) => SelectInverseMethod();
                SelectInverseButton.ImageList = SelectImgList;
                SelectInverseButton.ImageIndex = 1;
                SelectInverseButton.ToolTip = "反选";
            }

        }

        /// <summary>
        /// 初始化自动刷新数据
        /// </summary>
        protected void InitializeAutoRefresh()
        {
            if (IsAutoRefresh)
            {
                const int interval = 1000 * 60 * 10; //10分钟
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Enabled = true;
                timer.Interval = AutoRefreshInterval == 0 ? interval : AutoRefreshInterval;
                timer.Tick += (o, e) =>
                {
                    timer.Stop();
                    LoadMethod();
                    timer.Start();
                };
            }
        }

        /// <summary>
        /// 搜索框按钮响应
        /// </summary>
        protected virtual void SearchEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var index = e.Button.Index;
            if (e.Button.Index == 0)
            {
                SearchEdit.Text = string.Empty;
            }
            else if (index == 1)
            {
                SaveData();
            }
            else if (index == 2)
            {
                e.Button.Enabled = false;
                LoadMethod();
                //十秒后自动回复按钮状态
                ThreadHelper.CountDownAction(
                    () =>
                    {
                        if (SearchEdit.IsHandleCreated)
                        {
                            SearchEdit.Invoke(new Action(() => { e.Button.Enabled = true; }));
                        }
                    },
                    s =>
                    {
                        if (SearchEdit.IsHandleCreated)
                        {
                            SearchEdit.Invoke(new Action(() =>
                            {
                                if (s <= 0)
                                {
                                    e.Button.ToolTip = "刷新当前表格数据";
                                }
                                else
                                {
                                    e.Button.ToolTip = "{0}秒后恢复".FS(s);
                                }
                            }));
                        }
                    }, 10);
            }
            else if (index == 3)
            {
                this.IsAutoRefresh = !this.IsAutoRefresh;
                if (this.IsAutoRefresh)
                {
                    e.Button.ToolTip = "{0}秒自动刷新一次数据".FS(this.AutoRefreshInterval / 1000);
                }
                else
                {
                    e.Button.ToolTip = "自动刷新数据 当前没有开启自动刷新";
                }
            }
        }

        /// <summary>
        /// 添加改变的列值字典
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="columnName">列名</param>
        /// <param name="columnValue">列值</param>
        public void AddOrUpdateChanged(object id, string columnName, object columnValue)
        {
            if (!ChangedDic.ContainsKey(id))
            {
                ChangedDic.Add(id, new Dictionary<string, object>());
            }
            ChangedDic[id].AddOrUpdate(columnName, columnValue);
            SetCommitButtonStatus();
        }

        /// <summary>
        /// 设置按钮状态
        /// </summary>
        public virtual void SetButtonStatus()
        {
            if (NewButton != null) NewButton.Enabled = AllowCreate;
            if (NewBarItemButton != null) NewBarItemButton.Enabled = AllowCreate;
            if (NewRootBarItemButton != null) NewRootBarItemButton.Enabled = AllowCreate;
            var entity = Grid.FocusedNode;
            if (entity != null)
            {
                if (EditButton != null && AllowEdit) EditButton.Enabled = true;
                if (EditBarItemButton != null && AllowEdit) EditBarItemButton.Enabled = true;
                if (DeleteButton != null && AllowDelete) DeleteButton.Enabled = true;
                if (DeleteBarItemButton != null && AllowDelete) DeleteBarItemButton.Enabled = true;
                if (ExportButton != null && AllowExport) ExportButton.Enabled = true;

                if (SelectAllButton != null) SelectAllButton.Enabled = true;
                if (SelectInverseButton != null) SelectInverseButton.Enabled = true;

            }
            else
            {
                if (EditButton != null) EditButton.Enabled = false;
                if (DeleteButton != null) DeleteButton.Enabled = false;
                if (ExportButton != null) ExportButton.Enabled = false;
                if (DeleteBarItemButton != null) DeleteBarItemButton.Enabled = false;
                if (EditBarItemButton != null) EditBarItemButton.Enabled = false;

                if (SelectAllButton != null) SelectAllButton.Enabled = false;
                if (SelectInverseButton != null) SelectInverseButton.Enabled = false;
            }

            if (!Grid.IsAutoDeleteChild && Grid.FocusedNode != null && Grid.FocusedNode.HasChildren)
            {
                if (DeleteBarItemButton != null) DeleteBarItemButton.Enabled = false;
                if (DeleteButton != null) DeleteButton.Enabled = false;
            }

            if (CheckAuthorized != null) CheckAuthorized();
        }

        /// <summary>
        /// 设置提交按钮状态
        /// </summary>
        protected void SetCommitButtonStatus()
        {
            if (SearchEdit != null && SearchEdit.Properties.Buttons.VisibleCount >= 1)
            {
                SearchEdit.Properties.Buttons[1].Enabled = ChangedDic.Count > 0;//|| this.Grid.IsConfigChange;
            }
        }

        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            Initialize(true);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="isLoadData">是否自动加载数据</param>
        public void Initialize(bool isLoadData)
        {
            if (IsInitialize) return;

            InitializePopupMenu();
            InitializeGrid();
            InitializeSearchEdit();
            InitializeButtonEvent();
            InitializeAutoRefresh();

            if (isLoadData) LoadMethod();
            if (CheckAuthorized != null) CheckAuthorized();
            IsInitialize = true;
        }


        /// <summary>
        /// 加载数据方法
        /// </summary>
        public virtual void LoadMethod()
        {
            if (!AllowLoad) return;
            var entityType = typeof(E);
            ListLoadEventArgs beforeArgs = new ListLoadEventArgs();
            beforeArgs.EntityType = entityType;

            try
            {
                OnBeforeLoad(beforeArgs);
                if (beforeArgs.IsSuccess)
                {
                    Grid.EntityType = beforeArgs.EntityType;
                    object _dataSource = null;
                    if (beforeArgs.DataSource != null)
                    {
                        _dataSource = beforeArgs.DataSource;
                    }
                    else
                    {
                        if (Factory != null && Service != null)
                        {
                            _dataSource = Service.GetList(true, false, null);
                        }
                    }

                    Grid.DataSource = _dataSource;

                    SetButtonStatus();
                    ListLoadEventArgs afterArgs = new ListLoadEventArgs();
                    afterArgs.EntityType = entityType;
                    OnAfterLoad(afterArgs);
                    Grid.ExpandFirstNode();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBoxHelper.ShowError(ex.Message);
            }
        }

        public EntityBase GetSelectEntity()
        {
            if (Grid.DataSource != null)
            {
                if (Grid.DataSource is IList)
                {
                    return Grid.GetSelected<E>();
                }
                if (Grid.DataSource is DataTable)
                {
                    var row = Grid.GetSelected<DataRow>();
                    return Factory.Default.MapToEntity(row);
                }
            }
            return null;
        }

        /// <summary>
        /// 获取父实体
        /// </summary>
        public E GetParentEntity(bool isNew)
        {
            if (IsEditRoot) return null;

            if (isNew)
            {
                var obj = Grid.GetSelected();
                if (Grid.DataSource is IList)
                {
                    return (E)obj;
                }
                if (Grid.DataSource is DataTable)
                {
                    return Factory.Default.MapToEntity((DataRow)obj);
                }
                return null;
            }

            if (Grid.FocusedNode != null)
            {
                var obj = Grid.GetSelectedParent();
                if (Grid.DataSource is IList)
                {
                    return (E)obj;
                }
                if (Grid.DataSource is DataTable)
                {
                    return Factory.Default.MapToEntity((DataRow)obj);
                }
                return null;
            }
            return null;
        }

        public void NewRootMethod()
        {
            IsEditRoot = true;
            ShowEditMethod(true);
        }

        /// <summary>
        /// 新建方法
        /// </summary>
        public virtual void NewMethod()
        {
            IsEditRoot = false;
            var entity = GetSelectEntity();
            ListEditEventArgs args = new ListEditEventArgs();
            args.Entity = entity;
            args.IsNew = true;
            OnNew(args);
            if (args.IsSuccess)
            {
                ShowEditMethod(args.IsNew);
            }
        }

        /// <summary>
        /// 编辑方法
        /// </summary>
        public virtual void EditMethod()
        {
            IsEditRoot = false;
            var entity = GetSelectEntity();
            ListEditEventArgs args = new ListEditEventArgs();
            args.Entity = entity;
            args.IsNew = false;
            OnEdit(args);
            if (!args.IsSuccess) return;
            if (Grid.DataSource == null) return;

            if (entity != null)
            {
                ShowEditMethod(args.IsNew);
            }
            else
            {
                XtraMessageBoxHelper.ShowError("请选择一条数据");
            }
        }

        /// <summary>
        /// 显示编辑窗口
        /// </summary>
        /// <param name="isNew">是否新建</param>
        public virtual void ShowEditMethod(bool isNew)
        {
            ListEditEventArgs args = new ListEditEventArgs();
            args.IsNew = isNew;
            OnShowEdit(args);
        }

        public void CleanNotAllowNode(List<TreeListNode> nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                TreeListNode node = nodes[i];
                if (!Grid.IsAutoDeleteChild && node.HasChildren)
                {
                    nodes.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        public void DeleteMethod()
        {
            var nodeList = GetSelectedCheckboxList();
            CleanNotAllowNode(nodeList);
            ListDeleteEventArgs beforeArgs = new ListDeleteEventArgs();
            beforeArgs.EntityList = nodeList;
            OnBeforeDelete(beforeArgs);
            if (!beforeArgs.IsSuccess) return;

            XtraMessageBoxHelper.ShowYesNoAndWarning(DeleteConfirmMessage, d =>
            {
                if (nodeList.Count > 0)
                {
                    try
                    {
                        Grid.BeginUpdate();
                        foreach (TreeListNode item in nodeList)
                        {

                            var obj = Grid.Get(item);
                            DataRow dr = obj as DataRow;
                            if (dr != null)
                            {
                                Service.Delete(Factory.Default.MapToEntity(dr));
                            }
                            else
                            {
                                Service.Delete((E)obj);
                            }
                            Grid.Delete(item);
                        }
                        Grid.EndUpdate();
                        ListDeleteEventArgs afterArgs = new ListDeleteEventArgs();
                        beforeArgs.EntityList = nodeList;
                        OnAfterDelete(afterArgs);
                        if (afterArgs.IsSuccess)
                        {
                            SetButtonStatus();
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBoxHelper.ShowError(ex.Message);
                    }
                }
            });
        }

        public List<TreeListNode> GetSelectedCheckboxList()
        {
            var list = GetSelectedCheckboxListNoCurrentNode();
            if (list.Count == 0)
            {
                list.Add(Grid.FocusedNode);
            }
            return list;
        }

        public List<TreeListNode> GetSelectedCheckboxListNoCurrentNode()
        {
            List<TreeListNode> list = new List<TreeListNode>();
            Grid.RecursionTreeNode(Grid.Nodes, p =>
            {
                if (p.Checked)
                {
                    list.Add(p);
                }
            });
            return list;
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        public void ExportMethod()
        {
            ListOperateBaseEventArgs args = new ListOperateBaseEventArgs();
            OnBeforeExport(args);
            if (args.IsSuccess)
            {
                Grid.ShowPrintPreview();
            }
        }

        /// <summary>
        /// 清空复选框(不触发列值变化事件)
        /// </summary>
        public void SelectCleanAllMethod()
        {
            Grid.UncheckAll();
        }

        /// <summary>
        /// 全选复选框(不触发列值变化事件)
        /// </summary>
        public void SelectAllMethod()
        {
            Grid.CheckAll();
        }

        /// <summary>
        /// 反选复选框(不触发列值变化事件)
        /// </summary>
        public void SelectInverseMethod()
        {
            Grid.RecursionTreeNode(Grid.Nodes, p => p.Checked = !p.Checked);
        }

        /// <summary>
        /// 保存配置和数据
        /// </summary>
        public void SaveData()
        {
            if (ChangedDic.Count > 0)
            {
                try
                {
                    Service.BatchUpdateField(ChangedDic);
                    ChangedDic.Clear();
                }
                catch (Exception ex)
                {
                    XtraMessageBoxHelper.ShowError(ex.Message);
                }
            }
            //if (Grid.IsConfigChange)
            //{
            //    Grid.SaveConfig();
            //    Grid.IsConfigChange = false;
            //}
            SetCommitButtonStatus();
        }

        #endregion

    }
}