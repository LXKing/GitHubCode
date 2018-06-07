using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using XCI.Helper;

namespace XCI.WinUtility
{
    /// <summary>
    /// 表格下拉控件
    /// </summary>
    [System.ComponentModel.DesignerCategoryAttribute("Code")]
    [Designer(typeof(PopupGridEditDesigner))]
    [ToolboxBitmap(typeof(XCIPopupGridEdit), "SearchLookUpEdit")]
    public class XCIPopupGridEdit : XCIButtonEdit
    {
        private XCIGrid gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private static readonly object EventSelectedIndexChanged = new object();
        private static readonly object EventDataSourceRefresh = new object();
        private static readonly object EventDataSourceAdd = new object();
        private Form ownerForm;

        private bool _isClickShowPopup = true;
        private bool _isFocusShowPopup = true;
        private bool _isKeyPressShowPopup = true;
        private bool _isAutoSelect = true;
        private bool _isDblClickSelected = true;
        private bool isFilter = true;
        private bool _isHidePopupSelected = false;

        private object dataSource;
        private CurrencyManager dataManager;
        private string displayMember;
        private string valueMember;
        private int selectedIndex = -1;


        static XCIPopupGridEdit() { RepositoryItemPopupGridEdit.RegisterPopupGridEdit(); }

        public XCIPopupGridEdit()
        {
            PopupPosition = Point.Empty;
            PopupSize = Size.Empty;
            InitializeGrid();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            OnLoadData();
        }

        /// <summary>
        /// <para>
        /// Gets the class name of the current editor.
        /// </para>
        /// </summary>
        /// <value>
        /// The string identifying the class name of the current editor.
        /// </value>
        public override string EditorTypeName
        {
            get { return RepositoryItemPopupGridEdit.EditName; }
        }

        [Category(CategoryName.Properties)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemPopupGridEdit Properties
        {
            get { return base.Properties as RepositoryItemPopupGridEdit; }
        }

        [Category("吕艳阳Member"), Description("指示要绑定实体的显示值属性名称")]
        public string BindDisplayMember { get; set; }

        [Category("吕艳阳Member"), Description("指示要绑定实体的实际值的属性名称")]
        public string BindValueMember { get; set; }

        /// <summary>
        /// 指示要为此控件中的项显示的属性
        /// </summary>
        [Category("XCI数据"), Description("指示要为此控件中的项显示的属性")]
        public string DisplayMember
        {
            get { return displayMember; }
            set { displayMember = value; }
        }

        /// <summary>
        /// 指示用作控件中项的实际值的属性
        /// </summary>
        [Category("XCI数据"), Description("指示用作控件中项的实际值的属性")]
        public string ValueMember
        {
            get { return valueMember; }
            set { valueMember = value; }
        }


        /// <summary>
        /// 是否打开弹出框
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPopupOpen { get; private set; }

        /// <summary>
        /// 控件所在窗口
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Form OwnerForm
        {
            get { return ownerForm ?? (ownerForm = FindForm()); }
        }

        /// <summary>
        /// 表格
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public XCIGrid Grid { get { return gridControl; } }

        private string gridid;
        /// <summary>
        /// 表格标识
        /// </summary>
        [Browsable(false)]
        public string GridID
        {
            get { return gridid ?? (gridid = Guid.NewGuid().ToString("N")); }
            set { Grid.GridID = gridid = value; }
        }

        /// <summary>
        /// 表格默认视图
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridView View { get { return gridView; } }
        
        /// <summary>
        /// 表格数据源
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DataSource
        {
            get { return dataSource; }
            set
            {
                if (value != null && !(value is IList || value is IListSource))
                    throw new ArgumentException("无效数据源");
                if (dataSource == value) return;
                try
                {
                    dataSource = value;
                    if (((dataSource != null) && (BindingContext != null)) && (dataSource != Convert.DBNull))
                    {
                        dataManager = (CurrencyManager)this.BindingContext[dataSource];
                        this.isFilter = false;
                        UpdateText();
                        this.isFilter = true;
                    }
                    gridControl.DataSource = dataSource;
                    if (!OwnerForm.Controls.Contains(gridControl))
                    {
                        OwnerForm.Controls.Add(gridControl);
                    }
                }
                catch
                {
                }
            }
        }

        [Bindable(false), Browsable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [Category("Data"), Description("获取或设置指定当前选定值。")]
        public new object EditValue
        {
            get { return SelectedValue; }
            set { SelectedValue = value; }
        }

        /// <summary>
        /// 获取或设置当前选定的项
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get { return CurrencyManagerHelper.GetItem(dataManager, selectedIndex); }
            set { this.SelectedValue = CurrencyManagerHelper.GetValue(dataManager, value, valueMember); }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedValue
        {
            get
            {
                object currentItem = CurrencyManagerHelper.GetItem(dataManager, selectedIndex);
                return CurrencyManagerHelper.GetValue(dataManager, currentItem, valueMember);
            }
            set
            {
                if (value == null) this.SelectedIndex = -1;

                if (dataManager != null)
                {
                    string propertyName = valueMember;
                    if (string.IsNullOrEmpty(propertyName))
                        throw new InvalidOperationException("请设置ValueMember");
                    if (this.DataBindings.Count > 0)
                    {
                        ObjectHelper.SetObjectProperty(this.DataBindings[0].DataSource, BindValueMember, value);
                    }
                    this.SelectedIndex = CurrencyManagerHelper.GetIndex(dataManager,propertyName, value);
                    
                }
            }
        }

        /// <summary>
        /// 获取或设置指定当前选定项的索引。(数据源中的行索引,不是控件中的顺序)
        /// </summary>
        /// <remarks>当前选定项的从零开始的索引。如果未选定任何项，则返回值为负一 (-1)。</remarks>
        [Category("Data"), Description("获取或设置指定当前选定项的索引。")]
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;

                    this.isFilter = false;
                    UpdateText();
                    this.isFilter = true;

                    OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 单击文本框显示弹出框
        /// </summary>
        [Category("XCI显示框"), Description("单击文本框显示弹出框")]
        public bool IsClickShowPopup
        {
            get { return _isClickShowPopup; }
            set { _isClickShowPopup = value; }
        }

        /// <summary>
        /// 文本框获得焦点显示弹出框
        /// </summary>
        [Category("XCI显示框"), Description("文本框获得焦点显示弹出框")]
        public bool IsFocusShowPopup
        {
            get { return _isFocusShowPopup; }
            set { _isFocusShowPopup = value; }
        }

        /// <summary>
        /// 文本框按下键盘按键显示弹出框
        /// </summary>
        [Category("XCI显示框"), Description("文本框按下键盘按键显示弹出框")]
        public bool IsKeyPressShowPopup
        {
            get { return _isKeyPressShowPopup; }
            set { _isKeyPressShowPopup = value; }
        }

        /// <summary>
        /// 自动选中首行
        /// </summary>
        [Category("XCI选择"), Description("自动选中首行")]
        public bool IsAutoSelectFirst
        {
            get { return _isAutoSelect; }
            set { _isAutoSelect = value; }
        }

        /// <summary>
        /// 循环选择(下翻到最后一行时自动回到首行,上翻到首行时自动回到末行)
        /// </summary>
        [Category("XCI选择"), Description("循环选择(下翻到最后一行时自动回到首行,上翻到首行时自动回到末行)")]
        public bool IsLoopSelection { get; set; }

        /// <summary>
        /// 点击表格行确认选择
        /// </summary>
        [Category("XCI选择"), Description("点击表格行确认选择")]
        public bool IsClickSelected { get; set; }

        /// <summary>
        /// 击双表格行确认选择
        /// </summary>
        [Category("XCI选择"), Description("击双表格行确认选择")]
        public bool IsDblClickSelected
        {
            get { return _isDblClickSelected; }
            set { _isDblClickSelected = value; }
        }

        /// <summary>
        /// 隐藏弹出框时自动确认选择
        /// </summary>
        [Category("XCI选择"), Description("隐藏弹出框时自动确认选择")]
        public bool IsHidePopupSelected
        {
            get { return _isHidePopupSelected; }
            set { _isHidePopupSelected = value; }
        }

        /// <summary>
        /// 获取或设置弹出框大小(默认高度200,宽度与文本框一样)
        /// </summary>
        [Category("XCI布局"), Description("弹出框大小(默认高度200,宽度与文本框一样)")]
        public Size PopupSize { get; set; }

        /// <summary>
        /// 获取或设置弹出框显示位置(默认为文本框下面)
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point PopupPosition { get; set; }

        /// <summary>
        /// SelectedIndex 属性值更改时发生
        /// </summary>
        [Category("XCI"), Description("SelectedIndex 属性值更改时发生")]
        public event EventHandler SelectedIndexChanged
        {
            add { Events.AddHandler(EventSelectedIndexChanged, value); }
            remove { Events.RemoveHandler(EventSelectedIndexChanged, value); }
        }

        public event EventHandler DataSourceRefresh
        {
            add { Events.AddHandler(EventDataSourceRefresh, value); }
            remove { Events.RemoveHandler(EventDataSourceRefresh, value); }
        }

        public event EventHandler DataSourceAdd
        {
            add { Events.AddHandler(EventDataSourceAdd, value); }
            remove { Events.RemoveHandler(EventDataSourceAdd, value); }
        }


        /// <summary>
        /// 触发SelectedIndexChanged 事件。
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnSelectedIndexChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventSelectedIndexChanged];
            if (handler != null) handler(this, e);

            if (dataManager != null && dataManager.Position != selectedIndex)
            {
                if (selectedIndex != -1)
                {
                    this.dataManager.Position = selectedIndex;
                }
            }
        }

        private void OnDataSourceRefresh(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventDataSourceRefresh];
            if (handler != null) handler(this, e);
        }

        private void OnDataSourceAdd(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventDataSourceAdd];
            if (handler != null) handler(this, e);
        }

        protected virtual void OnLoadData()
        {

        }

        private void InitializeGrid()
        {
            this.gridControl = new XCIGrid();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            // 
            // gridControl
            // 
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.TabStop = false;
            this.gridControl.ViewCollection.Add(this.gridView);
            this.gridControl.ProcessGridKey += this.gridControl_ProcessGridKey;
            this.gridControl.Location = new Point(-500, -500);
            this.gridControl.Size = new Size(100, 100);
            //this.gridControl.Load += gridControl_Load;
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.SelectionChanged += this.gridView_SelectionChanged;
            this.gridView.Click += this.gridView_Click;
            this.gridView.DoubleClick += this.gridView_DoubleClick;
            this.gridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.gridView.OptionsSelection.MultiSelect = true;
            this.ButtonPressed += PopupEdit_ButtonPressed;

            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();

            ApplyFont();
        }

        private void UpdateText()
        {
            object item = CurrencyManagerHelper.GetItem(dataManager, selectedIndex);
            if (item == null)
            {
                Text = string.Empty;
            }
            else
            {
                var value = CurrencyManagerHelper.GetValue(dataManager, item, displayMember);
                Text = value == null ? string.Empty : value.ToString();
            }
        }

        /// <summary>
        /// 显示弹出框
        /// </summary>
        public void ShowPopup()
        {
            if (IsPopupOpen) return;
            if (OwnerForm == null) return;
            if (dataManager == null) return;

            if (DataSource == null) OnDataSourceRefresh(EventArgs.Empty);

            gridControl.Size = PopupSize;
            gridControl.Width = gridControl.Width <= 0 ? this.Width : gridControl.Width;
            gridControl.Height = gridControl.Height <= 0 ? 200 : gridControl.Height;
            //bool isCreateGrid = false;
            //if (!OwnerForm.Controls.Contains(gridControl))
            //{
            //    OwnerForm.Controls.Add(gridControl);
            //    isCreateGrid = true;
            //}
            if (PopupPosition == Point.Empty)
            {
                //Point pointInfo = OwnerForm.PointToScreen(new Point(OwnerForm.Width, OwnerForm.Height));
                //if ((p.X + gridControl.Width) > pointInfo.X)
                //{
                //    p.X = PointToScreen(new Point(Width, Height - 1)).X;
                //}
                //if ((p.Y + gridControl.Height) > pointInfo.Y)
                //{
                //    p.Y = PointToScreen(new Point(Width, 0)).Y - gridControl.Height;
                //}
                Point p = PointToScreen(new Point(0, Height - 1));
                gridControl.Location = OwnerForm.PointToClient(p);
            }
            else
            {
                gridControl.Location = PopupPosition;
            }

            gridControl.Visible = true;
            gridControl.BringToFront();

            this.AddMouseDownEvent(OwnerForm);
            //if (!isCreateGrid)
            {
                SelectRow();
            }
            IsPopupOpen = true;
        }

        /// <summary>
        /// 隐藏弹出框
        /// </summary>
        public void HidePopup()
        {
            RemoveMouseDownEvent(OwnerForm);
            if (IsHidePopupSelected)
            {
                CompleteSelected();
            }
            gridControl.Visible = false;
            IsPopupOpen = false;
        }

        /// <summary>
        /// 重置弹出框
        /// </summary>
        public void ResetPopup()
        {
            this.isFilter = false;
            gridView.ClearSelection();
            gridView.MakeRowVisible(0);
            if (IsAutoSelectFirst)
            {
                this.SelectedIndex = 0;
            }
            this.isFilter = true;
        }

        private void CleanStatus()
        {
            this.isFilter = false;
            gridView.ClearSelection();
            gridView.MakeRowVisible(0);
            this.SelectedIndex = -1;
            //this.HidePopup();
            this.isFilter = true;
        }

        

        private void SelectRow()
        {
            gridView.ClearSelection();
            if (selectedIndex > -1)
            {
                int dataIndex = gridView.GetRowHandle(selectedIndex);
                gridView.SelectRow(dataIndex);
                gridView.FocusedRowHandle = dataIndex;
            }
            else if (IsAutoSelectFirst)
            {
                gridView.SelectRow(0);
                gridView.FocusedRowHandle = 0;
            }
        }

        private void AddMouseDownEvent(Control control)
        {
            if ((control == null) || (control == this) || (control == gridControl)) return;
            control.MouseDown += Control_MouseDown;
            foreach (Control c in control.Controls)
            {
                this.AddMouseDownEvent(c);
            }
        }

        private void RemoveMouseDownEvent(Control control)
        {
            if ((control == null) || (control == this) || (control == gridControl)) return;
            control.MouseDown -= Control_MouseDown;
            foreach (Control c in control.Controls)
            {
                this.RemoveMouseDownEvent(c);
            }
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            this.HidePopup();
        }

        private void CompleteSelected()
        {
            var rows = gridView.GetSelectedRows();
            if (rows.Length > 0)
            {
                this.SelectedItem = gridView.GetRow(rows[0]);
            }
            else
            {
                this.SelectedIndex = -1;
            }
        }

        private void ApplyFont()
        {
            gridView.Appearance.Row.Font
                = gridView.Appearance.HeaderPanel.Font
                = this.Font;
        }

        private void Filter()
        {
            int length = this.Text.Length;
            this.SelectionStart = length;
            gridView.ApplyFindFilter(this.Text.Trim());
            if (length > 0 && gridView.RowCount > 0)
            {
                gridView.MoveFirst();
            }
            else
            {
                CleanStatus();
            }
        }

        private void ProcessKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CompleteSelected();
                HidePopup();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                ShowPopup();
                if (IsLoopSelection && gridView.FocusedRowHandle == 0)
                {
                    gridView.MoveLastVisible();
                }
                else if (gridView.GetSelectedRows().Length == 0)
                {
                    gridView.MoveFirst();
                }
                else
                {
                    gridView.MovePrev();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                ShowPopup();
                if (IsLoopSelection && (gridView.RowCount - 1 == gridView.FocusedRowHandle))
                {
                    gridView.MoveFirst();
                }
                else if (gridView.GetSelectedRows().Length == 0)
                {
                    gridView.MoveFirst();
                }
                else
                {
                    gridView.MoveNext();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                CleanStatus();
                e.Handled = true;
            }
        }

        #region 事件响应

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (IsFocusShowPopup)
            {
                ShowPopup();
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (IsClickShowPopup)
            {
                ShowPopup();
            }
        }

        protected override void OnEditValueChanged()
        {
            base.OnEditValueChanged();
            if (isFilter)
            {
                Filter();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            ProcessKey(e);
            base.OnKeyDown(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            ApplyFont();
            base.OnFontChanged(e);
        }

        private void gridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (gridView.IsGroupRow(gridView.FocusedRowHandle)) return;
            ProcessKey(e);
            if ((e.KeyValue >= 65 && e.KeyValue <= 90)
                || (e.KeyValue >= 48 && e.KeyValue <= 57))
            {
                this.Text += Convert.ToChar(e.KeyValue).ToString().ToLower();
                this.SelectionStart = this.Text.Length;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                int textLen = this.Text.Length;
                if (textLen > 0)
                {
                    this.Text = this.Text.Substring(0, textLen - 1);
                    this.SelectionStart = textLen;
                }
                e.Handled = true;
            }
        }

        //private void gridControl_Load(object sender, EventArgs e)
        //{
        //    //Debug.WriteLine("xx-gridLoad");
        //    SelectRow();
        //}

        private void gridView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (gridView.SelectedRowsCount > 1)
            {
                gridView.ClearSelection();
                gridView.SelectRow(gridView.FocusedRowHandle);
            }
        }

        private void gridView_Click(object sender, EventArgs e)
        {
            if (IsClickSelected)
            {
                CompleteSelected();
                HidePopup();
            }
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if (IsDblClickSelected)
            {
                CompleteSelected();
                HidePopup();
            }
        }

        private void PopupEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                ShowPopup();
            }
            else if (e.Button.Index == 1)
            {
                OnDataSourceRefresh(EventArgs.Empty);
            }
            else if (e.Button.Index == 2)
            {
                OnDataSourceAdd(EventArgs.Empty);
            }
        }

        #endregion

    }


    [UserRepositoryItem("RegisterPopupGridEdit")]
    public class RepositoryItemPopupGridEdit : RepositoryItemButtonEdit
    {
        //The static constructor which calls the registration method
        static RepositoryItemPopupGridEdit() { RegisterPopupGridEdit(); }

        //Initialize new properties
        public RepositoryItemPopupGridEdit()
        {

        }

        //The unique name for the custom editor
        public const string EditName = "PopupGridEdit";

        //Return the unique name
        public override string EditorTypeName { get { return EditName; } }

        //Register the editor
        public static void RegisterPopupGridEdit()
        {
            //Icon representing the editor within a container editor's Designer
            Bitmap img = null;
            try
            {
                var stream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("XCI.WinUtility.Images.SearchLookUpEdit.bmp");
                if (stream != null)
                {
                    img = (Bitmap)Image.FromStream(stream, true);

                    img.MakeTransparent(Color.Magenta);
                }
            }
            catch
            {

            }
            EditorRegistrationInfo.Default.Editors.Add(
                new EditorClassInfo(EditName,
              typeof(XCIPopupGridEdit), typeof(RepositoryItemPopupGridEdit),
              typeof(ButtonEditViewInfo), new ButtonEditPainter(), true, img));
        }

        //A custom property
        private bool _isShowDown = true;
        private bool _isShowRefresh = true;
        private bool _isShowPlus = true;

        public bool IsShowDown
        {
            get { return _isShowDown; }
            set
            {
                if (_isShowDown != value)
                {
                    _isShowDown = value;
                    OnPropertiesChanged();
                }
            }
        }

        public bool IsShowRefresh
        {
            get { return _isShowRefresh; }
            set
            {
                if (_isShowRefresh != value)
                {
                    _isShowRefresh = value;
                    OnPropertiesChanged();
                }
            }
        }

        public bool IsShowPlus
        {
            get { return _isShowPlus; }
            set
            {
                if (_isShowPlus != value)
                {
                    _isShowPlus = value;
                    OnPropertiesChanged();
                }
            }
        }

        //Override the Assign method
        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemPopupGridEdit source = item as RepositoryItemPopupGridEdit;
                if (source == null) return;
                _isShowRefresh = source.IsShowRefresh;
                _isShowDown = source.IsShowDown;
                _isShowPlus = source.IsShowPlus;
            }
            finally
            {
                EndUpdate();
            }
        }

        protected override void OnPropertiesChanged()
        {
            base.OnPropertiesChanged();

            if (Buttons.Count >= 1)
            {
                Buttons[0].Visible = _isShowDown;
            }
            if (Buttons.Count >= 2)
            {
                Buttons[1].Visible = _isShowRefresh;
            }
            if (Buttons.Count >= 3)
            {
                Buttons[2].Visible = _isShowPlus;
            }
        }

        public override void CreateDefaultButton()
        {
            Buttons.AddRange(new[]
                {
                    new DevExpress.XtraEditors.Controls.EditorButton(
                                 DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
                    new DevExpress.XtraEditors.Controls.EditorButton(
                                 DevExpress.XtraEditors.Controls.ButtonPredefines.Redo),
                    new DevExpress.XtraEditors.Controls.EditorButton(
                                 DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
                });

        }

    }

    /// <summary>
    /// 报表控件设计时类
    /// </summary>
    public class PopupGridEditDesigner : ControlDesigner
    {
        /// <summary>
        /// 创建一个自定义操作列表集合
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                DesignerActionListCollection actionLists = new DesignerActionListCollection();
                actionLists.Add(new PopupGridEditDesignerActionList(this));
                return actionLists;
            }
        }

        public override SelectionRules SelectionRules
        {
            get
            {
                SelectionRules selectionRules = base.SelectionRules;
                selectionRules |= SelectionRules.AllSizeable;
                selectionRules &= ~(SelectionRules.BottomSizeable | SelectionRules.TopSizeable);
                return selectionRules;
            }
        }

        /// <summary>
        /// 控件设计时行为自定义
        /// </summary>
        public class PopupGridEditDesignerActionList : DesignerActionList
        {
            private readonly PopupGridEditDesigner popupdesigner;
            private DesignerActionItemCollection items;

            public PopupGridEditDesignerActionList(PopupGridEditDesigner designer)
                : base(designer.Component)
            {
                this.popupdesigner = designer;
            }

            private XCIPopupGridEdit Editor { get { return popupdesigner.Component as XCIPopupGridEdit; } }

            protected void SetPropertyValue(string name, object value)
            {
                PropertyDescriptor propDesc = TypeDescriptor.GetProperties(popupdesigner.Component)[name];
                propDesc.SetValue(popupdesigner.Component, value);
            }

            protected void SetPropertyValue(object obj, string name, object value)
            {
                PropertyDescriptor propDesc = TypeDescriptor.GetProperties(obj)[name];
                propDesc.SetValue(obj, value);
            }

            public string Name
            {
                get { return Editor.Name; }
                set { SetPropertyValue("Name", value); }
            }

            public bool IsFocusShowPopup
            {
                get { return Editor.IsFocusShowPopup; }
                set { SetPropertyValue("IsFocusShowPopup", value); }
            }

            public bool IsClickShowPopup
            {
                get { return Editor.IsClickShowPopup; }
                set { SetPropertyValue("IsClickShowPopup", value); }
            }

            public bool IsKeyPressShowPopup
            {
                get { return Editor.IsKeyPressShowPopup; }
                set { SetPropertyValue("IsKeyPressShowPopup", value); }
            }


            public bool IsAutoSelectFirst
            {
                get { return Editor.IsAutoSelectFirst; }
                set { SetPropertyValue("IsAutoSelectFirst", value); }
            }

            public bool IsLoopSelection
            {
                get { return Editor.IsLoopSelection; }
                set { SetPropertyValue("IsLoopSelection", value); }
            }

            public bool IsClickSelected
            {
                get { return Editor.IsClickSelected; }
                set { SetPropertyValue("IsClickSelected", value); }
            }
            public bool IsDblClickSelected
            {
                get { return Editor.IsDblClickSelected; }
                set { SetPropertyValue("IsDblClickSelected", value); }
            }
            public bool IsHidePopupSelected
            {
                get { return Editor.IsHidePopupSelected; }
                set { SetPropertyValue("IsHidePopupSelected", value); }
            }

            public string DisplayMember
            {
                get { return Editor.DisplayMember; }
                set { SetPropertyValue("DisplayMember", value); }
            }

            public string ValueMember
            {
                get { return Editor.ValueMember; }
                set { SetPropertyValue("ValueMember", value); }
            }

            public bool IsShowDown
            {
                get { return Editor.Properties.IsShowDown; }
                set { SetPropertyValue(Editor.Properties, "IsShowDown", value); }
            }
            public bool IsShowRefresh
            {
                get { return Editor.Properties.IsShowRefresh; }
                set { SetPropertyValue(Editor.Properties, "IsShowRefresh", value); }
            }
            public bool IsShowPlus
            {
                get { return Editor.Properties.IsShowPlus; }
                set { SetPropertyValue(Editor.Properties, "IsShowPlus", value); }
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                if (items == null)
                {
                    items = new DesignerActionItemCollection();

                    items.Add(new DesignerActionPropertyItem("Name", "Name"));
                    items.Add(new DesignerActionHeaderItem("数据绑定"));
                    items.Add(new DesignerActionPropertyItem("ValueMember", "值成员", "数据绑定"));
                    items.Add(new DesignerActionPropertyItem("DisplayMember", "显示成员", "数据绑定"));

                    items.Add(new DesignerActionHeaderItem("显示框"));
                    items.Add(new DesignerActionPropertyItem("IsFocusShowPopup", "文本框焦点显示", "显示框"));
                    items.Add(new DesignerActionPropertyItem("IsClickShowPopup", "文本框单击显示", "显示框"));
                    items.Add(new DesignerActionPropertyItem("IsKeyPressShowPopup", "文本框搜索显示", "显示框"));

                    items.Add(new DesignerActionHeaderItem("选择"));
                    items.Add(new DesignerActionPropertyItem("IsAutoSelectFirst", "自动选中首行", "选择"));
                    items.Add(new DesignerActionPropertyItem("IsLoopSelection", "循环移动焦点", "选择"));
                    items.Add(new DesignerActionPropertyItem("IsClickSelected", "点击确认选择", "选择"));
                    items.Add(new DesignerActionPropertyItem("IsDblClickSelected", "击双确认选择", "选择"));
                    items.Add(new DesignerActionPropertyItem("IsHidePopupSelected", "隐藏弹出框确认选择", "选择"));

                    items.Add(new DesignerActionHeaderItem("按钮显示"));
                    items.Add(new DesignerActionPropertyItem("IsShowDown", "显示下拉按钮", "按钮显示"));
                    items.Add(new DesignerActionPropertyItem("IsShowRefresh", "显示刷新按钮", "按钮显示"));
                    items.Add(new DesignerActionPropertyItem("IsShowPlus", "显示添加按钮", "按钮显示"));

                }
                return items;
            }
        }
    }
}
