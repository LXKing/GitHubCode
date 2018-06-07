using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using XCI.Component;
using XCI.Core;
using XCI.Helper;
using XCI.WinUtility.GridConfig;

namespace XCI.WinUtility
{
    public class XCIGrid : GridControl
    {
        #region 属性

        private bool isInit;

        private string _gridid;
        /// <summary>
        /// 表格ID
        /// </summary>
        [Browsable(false)]
        public string GridID
        {
            get { return _gridid ?? (_gridid = Guid.NewGuid().ToString("N")); }
            set { _gridid = value; }
        }

        /// <summary>
        /// 默认视图
        /// </summary>
        [Browsable(false)]
        public GridView View
        {
            get
            {
                GridView view = FocusedView as GridView;
                return view;
            }
        }

        private bool _isShowColumnHeardMenu = true;

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
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsConfigChange
        {
            get { return _isConfigChange; }
            set
            {
                _isConfigChange = value;
                if (value)
                {
                    OnGridConfigChangeNotify(EventArgs.Empty);
                }
            }
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


        /// <summary>
        /// 当前焦点是否是第一行
        /// </summary>
        [Browsable(false)]
        public bool IsFirstRow
        {
            get { return HasData && View.IsFirstRow; }
        }

        /// <summary>
        /// 当前焦点是否是最后一行
        /// </summary>
        [Browsable(false)]
        public bool IsLastRow
        {
            get { return HasData && View.IsLastRow; }
        }


        private bool _isStoreConfig = true;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStoreConfig
        {
            get { return _isStoreConfig; }
            set { _isStoreConfig = value; }
        }

        private bool _isShowConfigButton = true;
        private GridView gridView1;
    
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsShowConfigButton
        {
            get { return _isShowConfigButton; }
            set { _isShowConfigButton = value; }
        }

        private IList dataSourceData;
        /// <summary>
        /// 自定义的数据源 使用此数据源时表格的滚动条不会发生变化
        /// </summary>
        [Browsable(false)]
        public object SafeDataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                if (value is IList)
                {
                    var list = value as IList;
                    if (dataSourceData == null)
                    {
                        dataSourceData = list;
                        base.DataSource = dataSourceData;
                    }
                    else
                    {
                        dataSourceData.Clear();
                        foreach (object item in list)
                        {
                            dataSourceData.Add(item);
                        }
                        this.RefreshDataSource();
                    }
                }
                else
                {
                    base.DataSource = value;
                }
            }
        }

        #endregion


        #region 事件

        /// <summary>
        /// 表格配置通知
        /// </summary>
        public event EventHandler<EventArgs> GridConfigChangeNotify;

        /// <summary>
        /// 触发表格配置通知事件
        /// </summary>
        /// <param name="e">表格配置通知参数</param>
        public virtual void OnGridConfigChangeNotify(EventArgs e)
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

        #endregion

        /// <summary>
        /// 读取配置之前触发
        /// </summary>
        public event EventHandler<EventArgs> AfterLoadConfig;

        /// <summary>
        /// 读取配置之前触发
        /// </summary>
        /// <param name="e">读取配置之前触发参数</param>
        public virtual void OnAfterLoadConfig(EventArgs e)
        {
            if (AfterLoadConfig != null)
            {
                AfterLoadConfig(this, e);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type EntityType { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object DataSource
        {
            get { return base.DataSource; }
            set
            {
                if (value != null && base.DataSource != null)
                {
                    if (base.DataSource is IList && View != null)
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
                    else if (base.DataSource is DataTable && View != null)
                    {
                        var index = View.FocusedRowHandle;
                        base.DataSource = value;
                        View.FocusedRowHandle = index;
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

        #region 实体操作

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
                return false;
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshData()
        {
            this.View.RefreshData();
        }


        public void RefreshRow(int rowIndex)
        {
            this.View.RefreshRow(rowIndex);
        }

        ///// <summary>
        ///// 插入数据
        ///// </summary>
        ///// <param name="rowHandle">表格行索引</param>
        ///// <param name="entity">数据</param>
        //public void Insert(int rowHandle, object entity)
        //{
        //    if (this.DataSource != null)
        //    {
        //        if (this.DataSource is IList)
        //        {
        //            ((IList)this.DataSource).Insert(rowHandle, entity);
        //            RefreshData();
        //        }
        //        else if (this.DataSource is DataTable)
        //        {
        //            ((DataTable)this.DataSource).Rows.InsertAt((DataRow)entity, rowHandle);
        //        }
        //    }
        //}


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
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Update(object entity)
        {
            if (this.DataSource != null && this.DataSource is IList)
            {
                int index = ListSource.IndexOf(entity);
                if (index >= 0)
                {
                    ListSource[index] = entity;
                    View.RefreshRow(View.GetRowHandle(index));
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">数据</param>
        public void Delete(object entity)
        {
            Delete(GetIndex(entity));
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="rowHandle">表格行索引</param>
        public void Delete(int rowHandle)
        {
            if (DataSource is IList)
            {
                this.View.DeleteRow(rowHandle);
            }
            if (DataSource is DataTable)
            {
                ((DataTable)this.DataSource).Rows.RemoveAt(this.View.GetDataSourceRowIndex(rowHandle));
            }
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
                    RefreshData();
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
        /// <returns>表格行索引</returns>
        public int GetIndex(object entity)
        {
            if (this.DataSource == null) return -1;
            var dataSourceIndex = -1;
            var source = this.DataSource as IList;
            if (source != null)
            {
                dataSourceIndex = (source).IndexOf(entity);
            }
            var dataTable = this.DataSource as DataTable;
            if (dataTable != null)
            {
                dataSourceIndex = (dataTable).Rows.IndexOf((DataRow)entity);
            }

            if (dataSourceIndex > -1)
            {
                return View.GetRowHandle(dataSourceIndex);
            }
            return -1;
        }

        /// <summary>
        /// 选中数据
        /// </summary>
        /// <param name="entity">数据</param>
        public void Select(object entity)
        {
            var rowHandle = GetIndex(entity);
            Select(rowHandle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowHandle">表格行索引</param>
        public void Select(int rowHandle)
        {
            if (rowHandle > -1)
            {
                this.View.FocusedRowHandle = rowHandle;
            }
        }

        #endregion


        #region 获取选中记录

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="rowHandle">表格行索引</param>
        /// <returns>数据</returns>
        public object Get(int rowHandle)
        {
            if (DataSource is IList)
            {
                return this.View.GetRow(rowHandle);
            }
            if (DataSource is DataTable)
            {
                return this.View.GetDataRow(rowHandle);
            }
            return null;
        }

        public T Get<T>(int rowHandle) where T : class
        {
            var obj = Get(rowHandle);
            if (obj == null)
            {
                return default(T);
            }
            return obj as T;
        }

        /// <summary>
        /// 获取选中数据
        /// </summary>
        /// <returns>数据</returns>
        public object GetSelected()
        {
            int[] ids = View.GetSelectedRows();
            if (ids.Length > 0)
            {
                return Get(ids[0]);
            }
            return null;
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

        /// <summary>
        /// 获取选中的数据列表
        /// </summary>
        /// <returns>数据列表</returns>
        public IList GetSelectedList()
        {
            int[] ids = View.GetSelectedRows();
            var list = new List<object>();
            foreach (int id in ids)
            {
                list.Add(Get(id));
            }
            return list;
        }

        public IList<T> GetSelectedList<T>() where T : class
        {
            int[] ids = View.GetSelectedRows();
            var list = new List<T>();
            foreach (int id in ids)
            {
                list.Add(Get<T>(id));
            }
            return list;
        }

        /// <summary>
        /// 获取选中的主键数组
        /// </summary>
        public int[] GetSelectedID(Func<object, int> getIDFunc)
        {
            int[] rows = View.GetSelectedRows();
            var list = new List<int>();
            foreach (int rowIndex in rows)
            {
                var obj = View.GetRow(rowIndex);
                var id = getIDFunc(obj);
                list.Add(id);
            }
            return list.ToArray();
        }

        #endregion

        #region 导航

        /// <summary>
        /// 移到首行
        /// </summary>
        public void MoveFirstRow(Action<int, int> updateSortCode = null)
        {
            if (this.View.SortedColumns.Count > 0) return;
            if (this.DataSource == null) return;
            if (IsFirstRow) return;

            int removeIndex = this.View.FocusedRowHandle;
            int insertIndex = 0;

            if (updateSortCode != null)
            {
                updateSortCode(removeIndex, insertIndex);
            }

            RemoveInsertRow(removeIndex, insertIndex);
        }


        /// <summary>
        /// 移到最后一行
        /// </summary>
        public void MoveLastRow(Action<int, int> updateSortCode = null)
        {
            if (this.View.SortedColumns.Count > 0) return;
            if (this.DataSource == null) return;
            if (IsLastRow) return;

            int removeIndex = this.View.FocusedRowHandle;
            int insertIndex = this.View.RowCount - 1;
            if (updateSortCode != null)
            {
                updateSortCode(removeIndex, insertIndex);
            }
            //RowCount Integer值，表示在视图可见行的数目
            //DataRowCount 一个整型值，在查看提供的数据行数计数。
            RemoveInsertRow(removeIndex, insertIndex);
        }

        /// <summary>
        /// 移到下一行
        /// </summary>
        public void MoveNextRow(Action<int, int> updateSortCode = null)
        {
            if (this.View.SortedColumns.Count > 0) return;
            if (this.DataSource == null) return;
            if (IsLastRow) return;

            int currentIndex = this.View.FocusedRowHandle;
            int targetIndex = currentIndex + 1;
            if (updateSortCode != null)
            {
                updateSortCode(currentIndex, targetIndex);
            }

            SwapRow(currentIndex, targetIndex);
        }

        /// <summary>
        /// 移到上一行
        /// </summary>
        public void MovePrevRow(Action<int, int> updateSortCode = null)
        {
            if (this.View.SortedColumns.Count > 0) return;
            if (this.DataSource == null) return;
            if (IsFirstRow) return;

            int currentIndex = this.View.FocusedRowHandle;
            int targetIndex = currentIndex - 1;

            if (updateSortCode != null)
            {
                updateSortCode(currentIndex, targetIndex);
            }
            SwapRow(currentIndex, targetIndex);

        }

        protected void RemoveInsertRow(int removeIndex, int insertIndex)
        {
            var source = this.DataSource as IList;
            if (source != null)
            {
                var obj = source[removeIndex];
                source.RemoveAt(removeIndex);
                source.Insert(insertIndex, obj);
                RefreshData();
                this.View.FocusedRowHandle = insertIndex;
            }
            var dataTable = this.DataSource as DataTable;
            if (dataTable != null)
            {
                var obj = DataTableHelper.CopyDataRow(dataTable.Rows[removeIndex]);
                dataTable.Rows.RemoveAt(removeIndex);
                dataTable.Rows.InsertAt(obj, insertIndex);
                this.View.FocusedRowHandle = insertIndex;
            }
        }

        protected void SwapRow(int currentIndex, int targetIndex)
        {
            var source = this.DataSource as IList;
            if (source != null)
            {
                var currentObj = source[currentIndex];
                var targetObj = source[targetIndex];
                source[currentIndex] = targetObj;
                source[targetIndex] = currentObj;
                this.View.RefreshRow(currentIndex);
                this.View.RefreshRow(targetIndex);
                this.View.FocusedRowHandle = targetIndex;
            }
            var dataTable = this.DataSource as DataTable;
            if (dataTable != null)
            {
                var currentObj = DataTableHelper.CopyDataRow(dataTable.Rows[currentIndex]);
                //var targetObj = dataTable.Rows[targetIndex];
                dataTable.Rows.RemoveAt(currentIndex);
                dataTable.Rows.InsertAt(currentObj, targetIndex);
                this.View.FocusedRowHandle = targetIndex;
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


        //protected override void CreateMainView()
        //{
        //    base.CreateMainView();
        //}

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (!isInit && !DesignMode)
            {
                isInit = true;
                if (IsStoreConfig) LoadConfig();
                OnAfterLoadConfig(EventArgs.Empty);
                this.View.PopupMenuShowing += new PopupMenuShowingEventHandler(View_PopupMenuShowing);
                this.View.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(View_CustomDrawRowIndicator);
                this.View.ColumnChanged += (o, a) => NoticeConfigChanged(); //列增减发生变化
                this.View.ColumnPositionChanged += (o, a) => NoticeConfigChanged();//列位置发生变化
                this.View.ColumnUnboundExpressionChanged += (o, a) => NoticeConfigChanged();//列绑定表达式变化
                this.View.ColumnWidthChanged += (o, a) => NoticeConfigChanged();//列宽度发生变化
            }
        }

        protected override void Dispose(bool disposing)
        {

            OnBeforeSaveConfig(EventArgs.Empty);
            if (View != null)
            {
                View.ClearColumnsFilter();
            }

            if (_isConfigChange && IsStoreConfig)
            {
                SaveConfig();
            }
            base.Dispose(disposing);
        }


        void View_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (View.OptionsView.ShowIndicator && e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                e.Info.ImageIndex = -1;
            }
        }

        private bool _ShowSelfConfig = true;
        /// <summary>
        /// 是否显示自定义按钮(初始化时使用)
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowSelfConfig
        {
            get { 
                return _ShowSelfConfig; 
            }
            set 
            { 
                _ShowSelfConfig = value; 
            }
        }
        void View_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (!IsShowColumnHeardMenu && e.Menu != null)
            {
                e.Menu.Items.Clear();
            }
            if (IsShowConfigButton && e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Column)
            {
                GridViewColumnMenu menu = e.Menu as GridViewColumnMenu;
                if (menu != null && ShowSelfConfig)
                {
                    DXMenuItem customItem = new DXMenuItem("自定义", (o, a) => ShowConfigForm(),
                        XCI.WinUtility.Properties.Resources.GridConfig);
                    customItem.BeginGroup = true;
                    menu.Items.Add(customItem);
                }
            }
        }

        public void ShowConfigForm()
        {
            frmGridConfig config = new frmGridConfig(this);
            config.ShowDialog();
            config.Dispose();
        }

        /// <summary>
        /// 通知配置发生改变
        /// </summary>
        protected void NoticeConfigChanged()
        {
            this.IsConfigChange = true;
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
            return AddPopMenuItem(CreateBarButtonItem(caption, image, action), isBeginGroup);
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

        private void InitializeComponent()
        {
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this;
            this.gridView1.Name = "gridView1";
            // 
            // XCIGrid
            // 
            this.MainView = this.gridView1;
            this.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

    }


    public class BooleanFormatter : IFormatProvider, ICustomFormatter
    {
        string trueString, falseString;
        public BooleanFormatter(string trueString, string falseString)
        {
            this.trueString = trueString;
            this.falseString = falseString;
        }
        public object GetFormat(System.Type type)
        {
            return this;
        }
        public string Format(string formatString, object arg, IFormatProvider formatProvider)
        {
            bool formatValue = Convert.ToBoolean(arg);
            if (formatValue)
                return trueString;
            return falseString;
        }
    }


}