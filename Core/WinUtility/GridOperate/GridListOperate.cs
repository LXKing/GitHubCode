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
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraSplashScreen;
using XCI.Component;
using XCI.Core;
using XCI.Extension;
using XCI.Helper;

namespace XCI.WinUtility
{
    /// <summary>
    /// 数据列表操作
    /// </summary>
    /// <typeparam name="E">实体类型</typeparam>
    /// <typeparam name="S">接口类型</typeparam>
    public class GridListOperate<E, S>:IDisposable
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
        /// 表格控件
        /// </summary>
        public XCIGrid Grid { get; set; }

        public GridView View
        {
            get { return Grid.View; }
        }
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
        /// 移到最上按钮
        /// </summary>
        public SimpleButton MoveTopButton { get; set; }

        /// <summary>
        /// 上移按钮
        /// </summary>
        public SimpleButton MoveUpButton { get; set; }

        /// <summary>
        /// 下移按钮
        /// </summary>
        public SimpleButton MoveDownButton { get; set; }

        /// <summary>
        /// 移到最下按钮
        /// </summary>
        public SimpleButton MoveBottomButton { get; set; }

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

        public const string SelectedFieldName = "Selected";

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
                    XCI.WinUtility.Properties.Resources.RecordAdd,
                    XCI.WinUtility.Properties.Resources.RecordEdit,
                    XCI.WinUtility.Properties.Resources.RecordDelete,
                    XCI.WinUtility.Properties.Resources.GridConfig
                });

                NewBarItemButton = Grid.CreateBarButtonItem("新建纪录", PopMenuImgList.Images[0], NewMethod);
                EditBarItemButton = Grid.CreateBarButtonItem("编辑纪录", PopMenuImgList.Images[1], EditMethod);
                DeleteBarItemButton = Grid.CreateBarButtonItem("删除纪录", PopMenuImgList.Images[2], DeleteMethod);

                NewBarItemButton.Enabled = false;
                NewBarItemButton.Enabled = false;
                DeleteBarItemButton.Enabled = false;

                Grid.AddPopMenuItem(NewBarItemButton, false);
                Grid.AddPopMenuItem(EditBarItemButton, true);
                Grid.AddPopMenuItem(DeleteBarItemButton, true);

                if (Grid.IsShowConfigButton)
                {
                    ConfigBarItemButton = Grid.CreateBarButtonItem("自定义", PopMenuImgList.Images[3], Grid.ShowConfigForm);
                    Grid.AddPopMenuItem(ConfigBarItemButton, true);
                }
            }
        }

        /// <summary>
        /// 初始化表格
        /// </summary>
        protected void InitializeGrid()
        {
            if (IsBindDoubleClick) View.DoubleClick += View_DoubleClick;//双击行

            View.MouseUp += View_MouseUp; //鼠标放开时 右键菜单
            View.ColumnFilterChanged += View_ColumnFilterChanged;
            View.FocusedRowChanged += View_FocusedRowChanged; //选中行发生变化
            View.CellValueChanging += View_CellValueChanging; //列值发生变化
            View.CellValueChanged += View_CellValueChanged; //列值发生变化
            View.ShowingEditor += View_ShowingEditor;//显示行编辑器

            Grid.ProcessGridKey += Grid_ProcessGridKey;//表格键盘处理
            Grid.GridConfigChangeNotify += (o, e) => SetCommitButtonStatus(); //表格配置改变通知;
            Grid.BeforeSaveConfig += Grid_BeforeSaveConfig;

            if (Metadata != null && !string.IsNullOrEmpty(Metadata.DeleteFieldName))
            {
                View.PopupMenuShowing += View_PopupMenuShowing;
            }
        }

        void View_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (IsShowRecycleBinButton && e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Column)
            {
                GridViewColumnMenu menu = e.Menu as GridViewColumnMenu;
                if (menu != null)
                {
                    Icon icon = new Icon(XCI.WinUtility.Properties.Resources.RecycleBin, new Size(16, 16));
                    var image = icon.ToBitmap();
                    DXMenuItem customItem = new DXMenuItem("回收站",
                        (o, a) => ShowRecycleBinForm(), image);
                    customItem.BeginGroup = true;
                    menu.Items.Add(customItem);
                }
            }
        }

        public void ShowRecycleBinForm()
        {
            frmGridRecycleBin form = new frmGridRecycleBin();
            form.Operate = this;
            form.ShowDialog();
            form.Dispose();
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
            var ev = e as DXMouseEventArgs;
            if (ev != null)
            {
                if (ev.Clicks == 2 &&
                    View.CalcHitInfo(ev.Location).HitTest == GridHitTest.ColumnEdge)
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
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = View.CalcHitInfo(e.Location);

            if (IsEnableContextMenu && e.Button == MouseButtons.Right)
            {
                //if (hi.InRow)
                //{
                //    //EditBarItemButton.Enabled = true;
                //    //DeleteBarItemButton.Enabled = true;
                //}
                if (hi.HitTest == GridHitTest.EmptyRow)
                {
                    EditBarItemButton.Enabled = false;
                    DeleteBarItemButton.Enabled = false;
                }
                SetButtonStatus();
                if (hi.InRow || hi.HitTest == GridHitTest.EmptyRow)
                {
                    Grid.PopMenu.ShowPopup(Control.MousePosition);
                }
            }
            if (!IsEnableContextMenu && hi.HitTest == GridHitTest.EmptyRow && e.Button == MouseButtons.Right)
            {
                Grid.ShowConfigPopMenu();
            }
        }

        void View_ColumnFilterChanged(object sender, EventArgs e)
        {
            OnColumnFilterChanged(e);
            SetButtonStatus();
        }

        /// <summary>
        /// 选中行发生变化
        /// </summary>
        void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
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
        void View_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column.FieldName.Equals(SelectedFieldName))
            {
                View.SetRowCellValue(e.RowHandle, SelectedFieldName, e.Value);
            }

            if (IsRecordChangedValue)
            {
                object entity = Grid.Get(e.RowHandle);
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

                bool isSelectColumn = IsSelectColumn(columnName);
                ListCellValueChanged args = new ListCellValueChanged();
                args.ColumnName = columnName;
                args.ColumnValue = columnValue;
                args.Entity = entity;
                args.EntityID = ID;
                args.IsSelectColumn = isSelectColumn;
                OnCellValueChanging(args);
            }
        }

        /// <summary>
        /// 列值发生变化
        /// </summary>
        void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (IsRecordChangedValue && e.RowHandle >= 0)
            {
                object entity = Grid.Get(e.RowHandle);
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

                bool isSelectColumn = IsSelectColumn(columnName);
                ListCellValueChanged beforeArgs = new ListCellValueChanged();
                beforeArgs.ColumnName = columnName;
                beforeArgs.ColumnValue = columnValue;
                beforeArgs.Entity = entity;
                beforeArgs.EntityID = ID;
                beforeArgs.IsSelectColumn = isSelectColumn;
                OnBeforeCellValueChanged(beforeArgs);//触发列值变化前事件
                if (!beforeArgs.IsSuccess) //事件拦截
                {
                    return;
                }

                if (isSelectColumn)
                {
                    ListCheckedChangedEventArgs checkArgs = new ListCheckedChangedEventArgs();
                    checkArgs.Checked = e.Value.ToBool();
                    checkArgs.Entity = entity;
                    OnCheckedChanged(checkArgs);
                }
                else
                {
                    AddOrUpdateChanged(ID, columnName, columnValue);
                }

                ListCellValueChanged afterArgs = new ListCellValueChanged();
                afterArgs.ColumnName = columnName;
                afterArgs.ColumnValue = columnValue;
                afterArgs.Entity = entity;
                afterArgs.EntityID = ID;
                afterArgs.IsSelectColumn = isSelectColumn;
                OnAfterCellValueChanged(afterArgs);
            }
        }


        /// <summary>
        /// 显示行编辑器
        /// </summary>
        void View_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Metadata != null && View.FocusedColumn.FieldName.Equals(Metadata.PrimaryKeyFieldName))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 表格键盘处理
        /// </summary>
        void Grid_ProcessGridKey(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.GridControl gc = (DevExpress.XtraGrid.GridControl)sender;
            DevExpress.XtraGrid.Views.Grid.GridView gv = (DevExpress.XtraGrid.Views.Grid.GridView)gc.FocusedView;

            #region Tab 最后一列时 自动跳转到下一行列首
            if (!e.Shift && (e.KeyCode == System.Windows.Forms.Keys.Tab)
                && (gv.FocusedColumn == gv.GetVisibleColumn(gv.VisibleColumns.Count - 1)))
            {
                gv.FocusedColumn = gv.GetVisibleColumn(0);
                if (gv.FocusedRowHandle != gv.RowCount - 1)
                {
                    gv.FocusedRowHandle += 1;
                }
                gv.ShowEditor();
                e.Handled = true;
            }

            if (e.Shift && (e.KeyCode == System.Windows.Forms.Keys.Tab)
                && (gv.FocusedColumn == gv.GetVisibleColumn(0)))
            {
                gv.FocusedColumn = gv.GetVisibleColumn(gv.VisibleColumns.Count - 1);
                if (gv.FocusedRowHandle != 0)
                {
                    gv.FocusedRowHandle -= 1;
                }
                gv.ShowEditor();
                e.Handled = true;
            }
            #endregion

            if (!View.IsLastRow)
            {
                #region 回车到下一行 Esc到上一行 列不变
                if (!e.Shift && (e.KeyCode == System.Windows.Forms.Keys.Enter))
                {
                    if (gv.FocusedRowHandle == gv.RowCount - 1)
                    {
                        gv.CloseEditor();
                    }
                    else
                    {
                        if (gv.FocusedRowHandle >= 0)
                        {
                            gv.FocusedRowHandle += 1;
                        }
                        gv.ShowEditor();
                    }
                    e.Handled = true;
                }
                if (e.KeyCode == System.Windows.Forms.Keys.Escape)
                {
                    if (gv.FocusedRowHandle == 0)
                    {
                        gv.CloseEditor();
                    }
                    else
                    {
                        gv.FocusedRowHandle -= 1;
                        gv.ShowEditor();
                    }
                    e.Handled = true;
                }
                #endregion
            }
            else
            {
                #region 回车到下一列 Esc到上一列
                if (!e.Shift && (e.KeyCode == System.Windows.Forms.Keys.Enter))
                {
                    if (gv.FocusedColumn == gv.GetVisibleColumn(gv.VisibleColumns.Count - 1))
                    {
                        gv.FocusedColumn = gv.GetVisibleColumn(0);
                        gv.ShowEditor();
                    }
                    else
                    {
                        gv.FocusedColumn = gv.GetVisibleColumn(gv.FocusedColumn.VisibleIndex + 1);
                        gv.ShowEditor();
                    }
                    e.Handled = true;
                }
                if (e.KeyCode == System.Windows.Forms.Keys.Escape)
                {
                    if (gv.FocusedColumn == gv.GetVisibleColumn(0))
                    {
                        gv.FocusedColumn = gv.GetVisibleColumn(gv.VisibleColumns.Count - 1);
                        gv.ShowEditor();
                    }
                    else
                    {
                        gv.FocusedColumn = gv.GetVisibleColumn(gv.FocusedColumn.VisibleIndex - 1);
                        gv.ShowEditor();
                    }
                    e.Handled = true;
                }
                #endregion
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
                SearchEdit.EditValueChanged += (o, e) => View.ApplyFindFilter(SearchEdit.Text.Trim());
            }
        }

        void SearchEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                Grid.View.MovePrev();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                Grid.View.MoveNext();
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
                //NewButton.Enabled = false;
            }
            if (EditButton != null)
            {
                EditButton.Click += (o, e) => EditMethod();
                EditButton.ImageList = RecordImgList;
                EditButton.ImageIndex = 1;
                EditButton.ToolTip = "编辑记录";
                //EditButton.Enabled = false;
            }
            if (DeleteButton != null)
            {
                DeleteButton.Click += (o, e) => DeleteMethod();
                DeleteButton.ImageList = RecordImgList;
                DeleteButton.ImageIndex = 2;
                DeleteButton.ToolTip = "删除记录";
                //DeleteButton.Enabled = false;
            }
            if (ExportButton != null)
            {
                ExportButton.Click += (o, e) => ExportMethod();
                ExportButton.ImageList = RecordImgList;
                ExportButton.ImageIndex = 3;
                ExportButton.ToolTip = "导出记录";
                //ExportButton.Enabled = false;
            }
            if (MoveTopButton != null)
            {
                MoveTopButton.Click += (o, e) => MoveFirstMethod();
                MoveTopButton.ImageList = SortImgList;
                MoveTopButton.ImageIndex = 0;
                MoveTopButton.ToolTip = "移到最上";
                //MoveTopButton.Enabled = false;
            }
            if (MoveUpButton != null)
            {
                MoveUpButton.Click += (o, e) => MovePrevMethod();
                MoveUpButton.ImageList = SortImgList;
                MoveUpButton.ImageIndex = 1;
                MoveUpButton.ToolTip = "上移";
                //MoveUpButton.Enabled = false;
            }
            if (MoveDownButton != null)
            {
                MoveDownButton.Click += (o, e) => MoveNextMethod();
                MoveDownButton.ImageList = SortImgList;
                MoveDownButton.ImageIndex = 2;
                MoveDownButton.ToolTip = "下移";
                //MoveDownButton.Enabled = false;
            }
            if (MoveBottomButton != null)
            {
                MoveBottomButton.Click += (o, e) => MoveLastMethod();
                MoveBottomButton.ImageList = SortImgList;
                MoveBottomButton.ImageIndex = 3;
                MoveBottomButton.ToolTip = "移到最下";
                //MoveBottomButton.Enabled = false;
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

        private Timer timer;
        /// <summary>
        /// 初始化自动刷新数据
        /// </summary>
        protected void InitializeAutoRefresh()
        {
            if (IsAutoRefresh)
            {
                const int interval = 1000 * 60 * 10; //10分钟
                timer = new System.Windows.Forms.Timer();
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
        protected virtual void SetButtonStatus()
        {
            if (NewButton != null) NewButton.Enabled = AllowCreate;
            if (NewBarItemButton != null) NewBarItemButton.Enabled = AllowCreate;
            var entity = Grid.GetSelected();
            if (entity != null)
            {
                if (EditButton != null && AllowEdit) EditButton.Enabled = true;
                if (EditBarItemButton != null && AllowEdit) EditBarItemButton.Enabled = true;
                if (DeleteButton != null && AllowDelete) DeleteButton.Enabled = true;
                if (DeleteBarItemButton != null && AllowDelete) DeleteBarItemButton.Enabled = true;
                if (ExportButton != null && AllowExport) ExportButton.Enabled = true;

                if (SelectAllButton != null) SelectAllButton.Enabled = true;
                if (SelectInverseButton != null) SelectInverseButton.Enabled = true;

                #region 序号

                if (MoveTopButton != null && AllowEdit) MoveTopButton.Enabled = true;
                if (MoveUpButton != null && AllowEdit) MoveUpButton.Enabled = true;
                if (MoveDownButton != null && AllowEdit) MoveDownButton.Enabled = true;
                if (MoveBottomButton != null && AllowEdit) MoveBottomButton.Enabled = true;

                if (Grid.IsFirstRow)
                {
                    if (MoveTopButton != null) MoveTopButton.Enabled = false;
                    if (MoveUpButton != null) MoveUpButton.Enabled = false;
                }
                if (Grid.IsLastRow)
                {
                    if (MoveDownButton != null) MoveDownButton.Enabled = false;
                    if (MoveBottomButton != null) MoveBottomButton.Enabled = false;
                }

                #endregion

            }
            else
            {
                if (EditButton != null) EditButton.Enabled = false;
                if (DeleteButton != null) DeleteButton.Enabled = false;
                if (ExportButton != null) ExportButton.Enabled = false;
                if (DeleteBarItemButton != null) DeleteBarItemButton.Enabled = false;
                if (EditBarItemButton != null) EditBarItemButton.Enabled = false;

                if (MoveTopButton != null) MoveTopButton.Enabled = false;
                if (MoveUpButton != null) MoveUpButton.Enabled = false;
                if (MoveDownButton != null) MoveDownButton.Enabled = false;
                if (MoveBottomButton != null) MoveBottomButton.Enabled = false;

                if (SelectAllButton != null) SelectAllButton.Enabled = false;
                if (SelectInverseButton != null) SelectInverseButton.Enabled = false;

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
                SearchEdit.Properties.Buttons[1].Enabled = ChangedDic.Count > 0;// || this.Grid.IsConfigChange;
            }
        }

        /// <summary>
        /// 是否是复选框选择列
        /// </summary>
        /// <param name="columnName">列名</param>
        public bool IsSelectColumn(string columnName)
        {
            return columnName.Equals(SelectedFieldName);
        }


        /// <summary>
        /// 排序码刷新
        /// </summary>
        protected virtual void RefreshSortCode()
        {
            //Grid.RefreshData();
            SetButtonStatus();
            SetCommitButtonStatus();
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
                    var tableSource = _dataSource as DataTable;
                    if (tableSource != null)
                    {
                        var col = new DataColumn("Selected", typeof(bool));
                        col.DefaultValue = false;
                        tableSource.Columns.Add(col);
                    }
                    Grid.DataSource = _dataSource;

                    SetButtonStatus();
                    ListLoadEventArgs afterArgs = new ListLoadEventArgs();
                    afterArgs.EntityType = entityType;
                    OnAfterLoad(afterArgs);
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
                if (Grid.DataSource is DataTable && Factory != null)
                {
                    var row = Grid.GetSelected<DataRow>();
                    return Factory.Default.MapToEntity(row);
                }
            }
            return null;
        }

        /// <summary>
        /// 新建方法
        /// </summary>
        public virtual void NewMethod()
        {
            var entity = GetSelectEntity();
            ListEditEventArgs args = new ListEditEventArgs();
            args.Entity = entity;
            args.IsNew = true;
            OnNew(args);
            if (args.IsSuccess)
            {
                ShowEditMethod(args.IsNew, entity);
            }
        }

        /// <summary>
        /// 编辑方法
        /// </summary>
        public virtual void EditMethod()
        {
            if (Grid.DataSource == null) return;

            var entity = GetSelectEntity();
            if (entity != null)
            {
                ListEditEventArgs args = new ListEditEventArgs();
                args.Entity = entity;
                args.IsNew = false;
                OnEdit(args);
                if (args.IsSuccess)
                {
                    ShowEditMethod(args.IsNew, entity);
                }
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
        /// <param name="entity">实体对象</param>
        protected virtual void ShowEditMethod(bool isNew, EntityBase entity)
        {
            ListEditEventArgs args = new ListEditEventArgs();
            args.Entity = entity;
            args.IsNew = isNew;
            OnShowEdit(args);
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        public void DeleteMethod()
        {
            var entityList = GetSelectedCheckboxList();

            ListDeleteEventArgs beforeArgs = new ListDeleteEventArgs();
            beforeArgs.EntityList = entityList;
            OnBeforeDelete(beforeArgs);
            if (!beforeArgs.IsSuccess) return;

            XtraMessageBoxHelper.ShowYesNoAndWarning(DeleteConfirmMessage, d =>
            {
                if (entityList.Count > 0)
                {
                    try
                    {
                        foreach (object item in entityList)
                        {
                            DataRow dr = item as DataRow;
                            if (dr != null)
                            {
                                Service.Delete(Factory.Default.MapToEntity(dr));
                            }
                            else
                            {
                                Service.Delete((E)item);
                            }
                            Grid.Delete(item);
                        }

                        ListDeleteEventArgs afterArgs = new ListDeleteEventArgs();
                        beforeArgs.EntityList = entityList;
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

        public List<object> GetSelectedCheckboxList()
        {
            List<object> list = new List<object>();
            for (int i = 0; i < View.RowCount; i++)
            {
                var selected = View.GetRowCellValue(i, SelectedFieldName).ToBool();
                if (selected)
                {
                    list.Add(Grid.Get(i));
                }
            }
            if (list.Count == 0)
            {
                list.Add(Grid.GetSelected());
            }
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
        /// 移到顶部
        /// </summary>
        public void MoveFirstMethod()
        {
            Action<int, int> action = (a, b) =>
            {
                if (string.IsNullOrEmpty(Metadata.SortCodeFieldName)) return;
                var newCode = SequenceFactory.Current.GetReduction(Metadata.TableName);
                object id = null;
                if (this.Grid.DataSource is IList)
                {
                    var obja = this.Grid.Get<E>(a);
                    id = obja.ID;
                    obja.SetPropertyValue(Metadata.SortCodeFieldName, newCode);
                }
                else if (this.Grid.DataSource is DataTable)
                {
                    var obja = this.Grid.Get<DataRow>(a);
                    id = obja[Metadata.PrimaryKeyFieldName];
                    obja[Metadata.SortCodeFieldName] = newCode;
                }
                AddOrUpdateChanged(id, Metadata.SortCodeFieldName, newCode);
            };
            this.Grid.MoveFirstRow(action);
            RefreshSortCode();
        }

        /// <summary>
        /// 上移
        /// </summary>
        public void MovePrevMethod()
        {
            Action<int, int> action = (a, b) =>
            {
                if (string.IsNullOrEmpty(Metadata.SortCodeFieldName)) return;
                if (this.Grid.DataSource is IList)
                {
                    var obja = this.Grid.Get<E>(a);
                    var objb = this.Grid.Get<E>(b);
                    var fasta = obja.GetFastProperty(Metadata.SortCodeFieldName);
                    var fastb = objb.GetFastProperty(Metadata.SortCodeFieldName);
                    var sorta = fasta.Get(obja);
                    var sortb = fastb.Get(objb);
                    fasta.Set(obja, sortb);
                    fastb.Set(objb, sorta);
                    AddOrUpdateChanged(obja.ID, Metadata.SortCodeFieldName, sortb);
                    AddOrUpdateChanged(objb.ID, Metadata.SortCodeFieldName, sorta);
                }
                else if (this.Grid.DataSource is DataTable)
                {
                    var obja = this.Grid.Get<DataRow>(a);
                    var objb = this.Grid.Get<DataRow>(b);
                    var sorta = obja[Metadata.SortCodeFieldName];
                    var sortb = objb[Metadata.SortCodeFieldName];
                    obja[Metadata.SortCodeFieldName] = sortb;
                    objb[Metadata.SortCodeFieldName] = sorta;
                    AddOrUpdateChanged(obja[Metadata.PrimaryKeyFieldName], Metadata.SortCodeFieldName, sortb);
                    AddOrUpdateChanged(objb[Metadata.PrimaryKeyFieldName], Metadata.SortCodeFieldName, sorta);
                }

            };
            this.Grid.MovePrevRow(action);
            RefreshSortCode();
        }

        /// <summary>
        /// 下移
        /// </summary>
        public void MoveNextMethod()
        {
            Action<int, int> action = (a, b) =>
            {
                if (string.IsNullOrEmpty(Metadata.SortCodeFieldName)) return;

                if (this.Grid.DataSource is IList)
                {
                    var obja = this.Grid.Get<E>(a);
                    var objb = this.Grid.Get<E>(b);
                    var fasta = obja.GetFastProperty(Metadata.SortCodeFieldName);
                    var fastb = objb.GetFastProperty(Metadata.SortCodeFieldName);
                    var sorta = fasta.Get(obja);
                    var sortb = fastb.Get(objb);
                    fasta.Set(obja, sortb);
                    fastb.Set(objb, sorta);
                    AddOrUpdateChanged(obja.ID, Metadata.SortCodeFieldName, sortb);
                    AddOrUpdateChanged(objb.ID, Metadata.SortCodeFieldName, sorta);
                }
                else if (this.Grid.DataSource is DataTable)
                {
                    var obja = this.Grid.Get<DataRow>(a);
                    var objb = this.Grid.Get<DataRow>(b);
                    var sorta = obja[Metadata.SortCodeFieldName];
                    var sortb = objb[Metadata.SortCodeFieldName];
                    obja[Metadata.SortCodeFieldName] = sortb;
                    objb[Metadata.SortCodeFieldName] = sorta;
                    AddOrUpdateChanged(obja[Metadata.PrimaryKeyFieldName], Metadata.SortCodeFieldName, sortb);
                    AddOrUpdateChanged(objb[Metadata.PrimaryKeyFieldName], Metadata.SortCodeFieldName, sorta);
                }
            };
            this.Grid.MoveNextRow(action);
            RefreshSortCode();
        }

        /// <summary>
        /// 移到底部
        /// </summary>
        public void MoveLastMethod()
        {
            Action<int, int> action = (a, b) =>
            {
                if (string.IsNullOrEmpty(Metadata.SortCodeFieldName)) return;
                var newCode = SequenceFactory.Current.GetSequence(Metadata.TableName);
                object id = null;
                if (this.Grid.DataSource is IList)
                {
                    var obja = this.Grid.Get<E>(a);
                    id = obja.ID;
                    obja.SetPropertyValue(Metadata.SortCodeFieldName, newCode);
                }
                else if (this.Grid.DataSource is DataTable)
                {
                    var obja = this.Grid.Get<DataRow>(a);
                    id = obja[Metadata.PrimaryKeyFieldName];
                    obja[Metadata.SortCodeFieldName] = newCode;
                }
                AddOrUpdateChanged(id, Metadata.SortCodeFieldName, newCode);
            };
            this.Grid.MoveLastRow(action);
            RefreshSortCode();
        }

        /// <summary>
        /// 清空复选框(不触发列值变化事件)
        /// </summary>
        public void SelectCleanAllMethod()
        {
            for (int i = 0; i < View.RowCount; i++)
            {
                View.SetRowCellValue(i, SelectedFieldName, false);
            }
        }

        /// <summary>
        /// 全选复选框(不触发列值变化事件)
        /// </summary>
        public void SelectAllMethod()
        {
            for (int i = 0; i < View.RowCount; i++)
            {
                View.SetRowCellValue(i, SelectedFieldName, true);
            }
        }

        /// <summary>
        /// 反选复选框(不触发列值变化事件)
        /// </summary>
        public void SelectInverseMethod()
        {
            for (int i = 0; i < View.RowCount; i++)
            {
                bool value = View.GetRowCellValue(i, SelectedFieldName).ToBool();
                View.SetRowCellValue(i, SelectedFieldName, !value);
            }
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

        public void Dispose()
        {
            if (timer!=null)
            {
                timer.Stop();
                timer.Dispose();
            }
        }
    }
}