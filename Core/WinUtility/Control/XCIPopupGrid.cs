using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using XCI.Component;
using XCI.Core;
using XCI.Extension;
using XCI.Helper;

namespace XCI.WinUtility
{
    public class XCIPopupGrid : PopupContainerEdit
    {
        #region 字段
        private XCIGrid grid;
        private GridView view;
        private PopupContainerControl popupContainer;

        private bool isclickshow = true;
        private bool isfocusshow = true;
        private bool isMouseClickSelectAllText = true;
        private bool isAutoSelectFirst = true;
        private string gridid;

        private bool isClick;
        private object _setObject;
        private int _setIndex = -1;
        private bool gridIsInit;
        public bool isSearch = true;

        #endregion

        public XCIPopupGrid()
        {
            grid = new XCIGrid();
            view = new GridView();
            popupContainer = new PopupContainerControl();
        }

        #region 属性

        [Browsable(false)]
        public GridView View
        {
            get { return view; }
        }

        [Browsable(false)]
        public XCIGrid Grid
        {
            get { return grid; }
        }

        [Browsable(false)]
        public PopupContainerControl PopupContainer
        {
            get { return popupContainer; }
        }

        [Category("吕艳阳"), Description("鼠标单击时是否自动显示Popup")]
        public bool IsClickShow
        {
            get { return isclickshow; }
            set { isclickshow = value; }
        }


        [Category("吕艳阳"), Description("获得焦点后是否自动显示Popup")]
        public bool IsFocusShow
        {
            get { return isfocusshow; }
            set { isfocusshow = value; }
        }

        [Category("吕艳阳"), Description("弹出窗口时是否自动过滤")]
        public bool IsAutoFilter { get; set; }


        [Category("吕艳阳"), Description("鼠标选择方式")]
        public bool IsDoubleClickSelect { get; set; }


        [Category("吕艳阳"), Description("鼠标单击或者双击时选中全部文本")]
        public bool IsMouseClickSelectAllText
        {
            get { return isMouseClickSelectAllText; }
            set { isMouseClickSelectAllText = value; }
        }


        [Category("吕艳阳"), Description("自动选择第一行")]
        public bool IsAutoSelectFirst
        {
            get { return isAutoSelectFirst; }
            set { isAutoSelectFirst = value; }
        }


        [Category("吕艳阳Size"), Description("Popup大小")]
        public Size PopupSize { get; set; }

        [Category("吕艳阳Member"), Description("指示要为此控件中的项显示的属性名称")]
        public string DisplayMember { get; set; }

        [Category("吕艳阳Member"), Description("指示用作控件中项的实际值的属性名称")]
        public string ValueMember { get; set; }

        [Category("吕艳阳Member"), Description("指示要绑定实体的显示值属性名称")]
        public string BindDisplayMember { get; set; }

        [Category("吕艳阳Member"), Description("指示要绑定实体的实际值的属性名称")]
        public string BindValueMember { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DataSource
        {
            get { return Grid.DataSource; }
            set { Grid.DataSource = value; }
        }


        /// <summary>
        /// 表格标识
        /// </summary>
        [Browsable(false)]
        public virtual string GridID
        {
            get { return gridid ?? (gridid = Guid.NewGuid().ToString("N")); }
            set { Grid.GridID = gridid = value; }
        }

        private Func<object, string> _displayGetAction;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<object, string> DisplayGetAction
        {
            get
            {
                if (_displayGetAction == null)
                {
                    if (DataSource is IList)
                    {
                        _displayGetAction = p => p.GetType().GetProperty(DisplayMember).GetValue(p, null).ToString();//((EntityBase)p).GetPropertyValue(DisplayMember).ToString();
                    }
                    else if (DataSource is DataTable)
                    {
                        _displayGetAction = p => ((DataRow)p)[DisplayMember].ToString();
                    }
                }
                return _displayGetAction;
            }
            set { _displayGetAction = value; }
        }

        private Func<object, object> _valueGetAction;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<object, object> ValueGetAction
        {
            get
            {
                if (_valueGetAction == null)
                {
                    if (DataSource is IList)
                    {
                        _valueGetAction = p => p.GetType().GetProperty(ValueMember).GetValue(p, null);//((EntityBase)p).GetPropertyValue(ValueMember);
                    }
                    else if (DataSource is DataTable)
                    {
                        _valueGetAction = p => ((DataRow)p)[ValueMember];
                    }
                }
                return _valueGetAction;
            }
            set { _valueGetAction = value; }
        }

        private object _SelectedObject;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedObject
        {
            get
            {
                
                var obj = Grid.GetSelected();
                return obj ?? _SelectedObject;
            }
            set
            {
                _SelectedObject = value;
                SelectedValue = ValueGetAction(value);
            }
        }

        /// <summary>
        /// 读取或者设置选中行索引
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (this.Text.Length == 0)
                {
                    return -1;
                }
                return _setIndex;
            }
            set
            {
                isSearch = false;
                View.ApplyFindFilter(string.Empty);
                _setIndex = value;
                _setObject = GetObject(_setIndex);
                //if (gridIsInit)
                {
                    SetText(_setObject);
                    SelectRow(_setIndex);
                }
                isSearch = true;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedValue
        {
            get
            {
                if (this.Text.Length == 0)
                {
                    return null;
                }
                var obj = SelectedObject;
                if (obj != null)
                {
                    return ValueGetAction(obj);
                }
                return null;
            }
            set
            {
                if (value == DBNull.Value)
                {
                    ResetPopup();
                }
                else
                {
                    GetSelectedObjIndex(value);
                    isSearch = false;
                    View.ApplyFindFilter(string.Empty);
                    //if (gridIsInit)
                    {
                        SetText(_setObject);
                        SelectRow(_setIndex);
                    }
                    isSearch = true;
                }
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditText
        {
            get
            {
                var obj = SelectedObject;
                if (obj != null)
                {
                    return DisplayGetAction(obj);
                }
                return string.Empty;
            }
            set { ; }
        }

        #endregion


        #region 选中事件

        private static readonly object PopupSelectedArgsObject = new object();
        [Category("吕艳阳"), Description("实体选中事件")]
        public event EventHandler<PopupSelectedEventArgs> PopupSelected
        {
            add { Events.AddHandler(PopupSelectedArgsObject, value); }
            remove { Events.RemoveHandler(PopupSelectedArgsObject, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnPopupSelected(PopupSelectedEventArgs e)
        {
            EventHandler<PopupSelectedEventArgs> handler = (EventHandler<PopupSelectedEventArgs>)Events[PopupSelectedArgsObject];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion


        #region 方法

        //private Color _waterMarkColor = Color.Gray;
        //[Category("吕艳阳"), Description("水印文本颜色")]
        //public Color WaterMarkColor
        //{
        //    get { return _waterMarkColor; }
        //    set { _waterMarkColor = value; }
        //}

        //[Browsable(true)]
        //[Category("吕艳阳"), Description("水印文本")]
        //public string WaterMarkText { get; set; }

        ///// <summary>
        ///// 初始化文本水印
        ///// </summary>
        //public void WaterMarkInit(bool isCleanText = true)
        //{
        //    XCITextBoxMaskBox maskBox = this.MaskBox as XCITextBoxMaskBox;
        //    if (maskBox != null)
        //    {
        //        maskBox.WaterMarkFont = new Font(this.Font.FontFamily,this.Font.Size, FontStyle.Regular);
        //        maskBox.WaterMarkColor = this.WaterMarkColor;
        //        maskBox.WaterMarkText = this.WaterMarkText;
        //        if (isCleanText)
        //        {
        //            this.Text = string.Empty;
        //        }
        //    }
        //}

        //protected override TextBoxMaskBox CreateMaskBoxInstance()
        //{
        //    return new XCITextBoxMaskBox(this);
        //}

        protected virtual void OnLoadData()
        {

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Grid != null) Grid.Dispose();
                if (View != null) View.Dispose();
                if (PopupContainer != null) PopupContainer.Dispose();
            }
            base.Dispose(disposing);
        }

        protected void Initialize()
        {
            if (!DesignMode)
            {
                Grid.MainView = View;
                Grid.ViewCollection.Add(View);
                view.GridControl = Grid;

                #region GridView

                View.OptionsSelection.EnableAppearanceHideSelection = false;
                View.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                View.OptionsView.ShowIndicator = false;
                View.OptionsSelection.MultiSelect = true;
                View.Appearance.Row.Font = this.Font;
                View.Appearance.HeaderPanel.Font = this.Font;
                View.OptionsView.ShowGroupPanel = false;
                View.OptionsBehavior.Editable = false;


                //View.OptionsCustomization.AllowFilter = false;
                //View.OptionsCustomization.AllowSort = false;
                //View.OptionsCustomization.AllowColumnMoving = false;
                //View.OptionsCustomization.AllowColumnResizing = false;
                //View.OptionsCustomization.AllowQuickHideColumns = false;
                //View.OptionsSelection.EnableAppearanceFocusedCell = false;
                //View.FocusRectStyle = DrawFocusRectStyle.None;

                View.Click += (o, e) =>
                {
                    #region Click
                    if (!IsDoubleClickSelect)
                    {
                        ClosePopupCore();
                    }
                    #endregion
                };

                View.MouseUp += (o, e) =>
                {
                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = View.CalcHitInfo(e.Location);
                    if (hi.HitTest == GridHitTest.EmptyRow && e.Button == MouseButtons.Right)
                    {
                        Grid.ShowConfigPopMenu();
                    }
                };

                View.DoubleClick += (o, e) =>
                {
                    #region DoubleClick
                    if (IsDoubleClickSelect)
                    {
                        ClosePopupCore();
                    }
                    #endregion
                };
                View.SelectionChanged += (o, e) =>
                {
                    if (View.SelectedRowsCount > 1)
                    {
                        View.ClearSelection();
                        SelectRow(View.FocusedRowHandle);
                    }
                };
                View.CustomDrawRowIndicator += (o, e) =>
                {
                    #region CustomDrawRowIndicator
                    if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                    {
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                        e.Info.ImageIndex = -1;
                    }
                    #endregion
                };

                #endregion

                #region Grid

                Grid.Dock = System.Windows.Forms.DockStyle.Fill;
                Grid.IsShowColumnHeardMenu = false;
                Grid.TabIndex = 0;
                Grid.TabStop = false;
                Grid.Font = this.Font;

                Grid.ProcessGridKey += (o, e) =>
                {
                    #region ProcessGridKey
                    if (View.IsGroupRow(View.FocusedRowHandle)) return;

                    if (e.KeyCode == Keys.Enter)
                    {
                        ClosePopupCore();
                        e.Handled = true;
                    }
                    else if ((e.KeyValue >= 65 && e.KeyValue <= 90)
                        || (e.KeyValue >= 48 && e.KeyValue <= 57))
                    {
                        this.Text += Convert.ToChar(e.KeyValue).ToString();
                        this.SelectionStart = this.Text.Length;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Delete && e.KeyCode == Keys.Back)
                    {
                        ResetPopup();
                        e.Handled = true;
                    }
                    #endregion
                };

                #endregion

                #region PopupContainer

                PopupContainer.TabIndex = 0;
                PopupContainer.TabStop = false;
                PopupContainer.Controls.Add(Grid);
                #endregion


                this.QueryCloseUp += (o, e) =>
                {
                    if (isClick) e.Cancel = true;
                };
                

                this.Properties.ShowPopupCloseButton = false;
                this.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                this.Properties.PopupControl = PopupContainer;

                SetPopupContainerSize();
                OnLoadData();
                this.Grid.ForceInitialize();
            }
        }

        protected override void DoShowPopup()
        {
            base.DoShowPopup();
            if (!gridIsInit)
            {
                gridIsInit = true;
                View.ClearSelection();
                if (_setObject != null && _setIndex > -1)
                {
                    SelectRow(_setIndex);
                }
                else if (IsAutoSelectFirst)
                {
                    SelectRow(0);
                }
            }
            if (IsAutoFilter)
            {
                Filter();
            }
            else
            {
                if (View.GetSelectedRows().Length > 0)
                {
                    View.MakeRowVisible(View.FocusedRowHandle);
                }
            }
            this.Focus();
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (IsFocusShow)
            {
                ShowPopup();
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.isClick = true;
            if (IsMouseClickSelectAllText)
            {
                this.SelectAll();
            }

            if (IsClickShow)
            {
                ShowPopup();
            }
        }

        protected override void OnEditValueChanged()
        {
            base.OnEditValueChanged();
            if (isSearch)
            {
                ShowPopup();
                Filter();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter)
            {
                ClosePopupCore();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (IsPopupOpen)
                {
                    if (View.GetSelectedRows().Length == 0)
                    {
                        View.MoveFirst();
                    }
                    else
                    {
                        View.MovePrev();
                    }
                }
                else
                {
                    ShowPopup();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (IsPopupOpen)
                {
                    if (View.GetSelectedRows().Length == 0)
                    {
                        View.MoveFirst();
                    }
                    else
                    {
                        View.MoveNext();
                    }
                }
                else
                {
                    ShowPopup();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                ResetPopup();
                e.Handled = true;
            }
        }

        private void Filter()
        {
            this.SelectionStart = this.Text.Length;
            View.ClearSelection();
            View.ApplyFindFilter(this.Text.Trim());
            if (View.RowCount > 0 && IsAutoSelectFirst)
            {
                View.MoveFirst();
            }
        }

        private void SetPopupContainerSize()
        {
            int width = this.Width - 4;
            int height = 200;
            if (PopupSize.Width > 0)
            {
                width = PopupSize.Width;
            }
            if (PopupSize.Height > 0)
            {
                height = PopupSize.Height;
            }
            PopupContainer.Size = new Size(width, height);
        }

        public void GetSelectedObjIndex(object value)
        {
            if (value == null)
            {
                _setIndex = -1;
                _setObject = null;
                return;
            }
            var listDataSource = DataSource as IList;
            if (listDataSource != null)
            {
                for (int i = 0; i < listDataSource.Count; i++)
                {
                    object item = listDataSource[i];
                    if (ValueGetAction(item).ToString().Equals(value.ToString()))
                    {
                        _setIndex = i;
                        _setObject = item;
                        break;
                    }
                }
            }
            else
            {
                var tableDataSource = DataSource as DataTable;
                if (tableDataSource != null)
                {
                    for (int i = 0; i < tableDataSource.Rows.Count; i++)
                    {
                        DataRow item = tableDataSource.Rows[i];
                        if (ValueGetAction(item).ToString().Equals(value.ToString()))
                        {
                            _setIndex = i;
                            _setObject = item;
                            break;
                        }
                    }
                }
            }
        }

        public void SelectRow(int index)
        {
            if (index < 0) return;
            View.ClearSelection();
            View.SelectRow(index);
            View.FocusedRowHandle = index;
            _setIndex = index;
            _SelectedObject = GetObject(index);
        }

        public object GetObject(int index)
        {
            object obj = null;
            var listDataSource = DataSource as IList;
            if (listDataSource != null)
            {
                obj = listDataSource[index];
            }
            else
            {
                var tableDataSource = DataSource as DataTable;

                if (tableDataSource != null)
                {
                    obj = tableDataSource.Rows[index];
                }
            }
            return obj;
        }

        public void SetText(object selectedObj)
        {
            if (selectedObj == null)
            {
                this.Text = string.Empty;
            }
            else
            {
                this.Text = DisplayGetAction(selectedObj);
                this.SelectionStart = this.Text.Length;
            }
        }

        public void ResetPopup()
        {
            isSearch = false;
            this.View.MoveFirst();
            this.isClick = false;
            SetText(null);
            this.Filter();
            View.FocusedRowHandle = -2147483648;
            View.ClearSelection();
            isSearch = true;
        }

        private void ClosePopupCore()
        {
            isSearch = false;
            this.isClick = false;
            SetText(SelectedObject);
            if (!IsAutoFilter)
            {
                View.ApplyFindFilter(string.Empty);
            }
            CFSelected();
            isSearch = true;
            this.ClosePopup();
        }

        private void CFSelected()
        {
            PopupSelectedEventArgs selectedArgs = new PopupSelectedEventArgs();
            selectedArgs.SelectedObject = SelectedObject;
            selectedArgs.Text = this.Text;
            selectedArgs.Value = SelectedValue;
            OnPopupSelected(selectedArgs);
        }

        /// <summary>
        /// 添加一个表格列
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="caption">列标题</param>
        /// <param name="width">宽度</param>
        public GridColumn AddColumn(string fieldName, string caption, int width)
        {
            GridColumn column = new GridColumn();
            column.Caption = caption;
            column.FieldName = fieldName;
            column.Width = width;
            column.Visible = true;
            column.VisibleIndex = 0;
            View.Columns.Add(column);
            return column;
        }

        #endregion

    }
}