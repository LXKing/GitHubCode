using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using XCI.Component;
using XCI.Core;
using XCI.Extension;
using XCI.Helper;

namespace XCI.WinUtility
{
    public class XCIPopupTreeGrid : PopupContainerEdit
    {
        #region 字段
        private XCITreeGrid grid;
        private PopupContainerControl popupContainer;

        private bool isclickshow = true;
        private bool isfocusshow = true;
        private bool isMouseClickSelectAllText = true;
        private bool isAutoSelectFirst = true;
        private string gridid;

        private bool isClick;
        private object _setObject;
        private TreeListNode _setNode;
        private bool gridIsInit;
        public bool isSearch = true;

        #endregion

        public XCIPopupTreeGrid()
        {
            grid = new XCITreeGrid();
            popupContainer = new PopupContainerControl();
        }

        #region 属性


        [Browsable(false)]
        public XCITreeGrid Grid
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

        [Category("吕艳阳"), Description("自动选中所有上级")]
        public bool IsCheckParent { get; set; }

        [Category("吕艳阳"), Description("自动选中所有下级")]
        public bool IsCheckChild { get; set; }

        private Color _selectedColor = Color.FromArgb(255, 255, 192);

        [Category("吕艳阳"), Description("选中行背景色 只针对多选")]
        public Color SelectedColor
        {
            get { return _selectedColor; }
            set { _selectedColor = value; }
        }

        [Category("吕艳阳"), Description("是否允许多选")]
        public bool IsMultiSelect { get; set; }

        [Category("吕艳阳Size"), Description("Popup大小")]
        public Size PopupSize { get; set; }

        private string _splitSymbol = ",";

        [Category("吕艳阳"), Description("多选时分隔符号")]
        public string SplitSymbol
        {
            get { return _splitSymbol; }
            set { _splitSymbol = value; }
        }

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
                        _displayGetAction = p => p.GetType().GetProperty(DisplayMember).GetValue(p,null).ToString();//((EntityBase)p).GetPropertyValue(DisplayMember).ToString();
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedObject
        {
            get { return Grid.GetSelected(); }
            set { SelectedValue = ValueGetAction(value); }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedValue
        {
            get
            {
                if (IsMultiSelect)
                {
                    return SelectedValues;
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
                    if (IsMultiSelect)
                    {
                        SelectedValues = value.ToString();
                    }
                    else
                    {
                        GetSelectedObjIndex(value);
                        isSearch = false;
                        Grid.ClearColumnsFilter();
                        //if (gridIsInit)
                        {
                            SetText(_setObject);
                            SelectRow(_setNode);
                        }
                        isSearch = true;
                    }

                }
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditText
        {
            get
            {
                if (IsMultiSelect)
                {
                    return SelectedTexts;
                }
                var obj = SelectedObject;
                if (obj != null)
                {
                    return DisplayGetAction(obj);
                }
                return string.Empty;
            }
            set { ; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedValues
        {
            get { return GetSplitValue(); }
            set
            {
                GetSelectedObj(value);
                isSearch = false;
                Grid.ClearColumnsFilter();
                //if (gridIsInit)
                {
                    SetTexts();
                    SelectRows(true);
                }
                isSearch = true;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedTexts
        {
            get { return GetSplitDisplay(); }
        }

        private XCIList<object> _selectedObjects;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public XCIList<object> SelectedObjects
        {
            get { return _selectedObjects ?? (_selectedObjects = new XCIList<object>()); }
            set
            {
                isSearch = false;
                _selectedObjects = value;
                Grid.ClearColumnsFilter();
                //if (gridIsInit)
                {
                    SetTexts();
                    SelectRows(true);
                }
                isSearch = true;
            }
        }

        public void BindSelectedObjects()
        {
            isSearch = false;
            Grid.ClearColumnsFilter();
            SetTexts();
            SelectRows(true);
            isSearch = true;
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
                if (PopupContainer != null) PopupContainer.Dispose();
            }
            base.Dispose(disposing);
        }

        protected virtual void Initialize()
        {
            if (!DesignMode)
            {
                #region Grid

                Grid.Dock = System.Windows.Forms.DockStyle.Fill;
                Grid.IsShowColumnHeardMenu = false;
                Grid.TabIndex = 0;
                Grid.TabStop = false;
                Grid.Font = this.Font;
                Grid.MenuManager = this.MenuManager;
                Grid.OptionsSelection.MultiSelect = true;
                Grid.OptionsBehavior.EnableFiltering = true;
                Grid.OptionsBehavior.Editable = false;
                Grid.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart;
                Grid.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
                Grid.OptionsView.ShowCheckBoxes = IsMultiSelect;
                Grid.SelectionChanged += (o, e) =>
                {
                    if (Grid.Selection.Count > 1)
                    {
                        SelectRow(Grid.FocusedNode);
                    }
                };

                Grid.AfterCheckNode += (o, e) =>
                {
                    Grid.SetFocusedNode(e.Node);
                    SwitchNodeStatusData(e.Node);
                };

                Grid.MouseUp += (o, e) =>
                {
                    TreeListHitInfo hi = Grid.CalcHitInfo(e.Location);
                    if (hi.HitInfoType == HitInfoType.Empty && e.Button == MouseButtons.Right)
                    {
                        Grid.ShowConfigPopMenu();
                    }
                };

                Grid.Click += (o, e) =>
                {
                    #region Click
                    if (!IsDoubleClickSelect)
                    {
                        ClosePopupCore();
                    }
                    #endregion
                };
                Grid.DoubleClick += (o, e) =>
                {
                    #region DoubleClick
                    if (IsDoubleClickSelect)
                    {
                        ClosePopupCore();
                    }
                    #endregion
                };
                Grid.KeyDown += (o, e) =>
                {
                    #region KeyDown

                    if (e.KeyCode == Keys.Enter)
                    {
                        ClosePopupCore();
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Space)
                    {
                        SwitchNodeStatus();
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

                Grid.NodeCellStyle += Grid_NodeCellStyle;
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

        void Grid_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Checked)
            {
                e.Appearance.BackColor = SelectedColor;
                //e.Appearance.ForeColor = Color.Green;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        protected override void DoShowPopup()
        {
            base.DoShowPopup();
            if (!gridIsInit)
            {
                gridIsInit = true;
                Grid.Selection.Clear();
                //Grid.ExpandAll();
                Grid.ExpandFirstNode();
                if (IsAutoSelectFirst && Grid.Nodes.Count > 0)
                {
                    SelectRow(Grid.Nodes[0]);
                }
                if (!IsMultiSelect) //单选
                {
                    if (_setObject != null && _setNode == null)
                    {
                        _setNode = Grid.FindNodeByFieldValue(ValueMember, ValueGetAction(_setObject));
                    }
                    if (_setObject != null && _setNode != null)
                    {
                        SelectRow(_setNode);
                    }
                }
                else //多选
                {
                    SelectRows(true);
                }
            }
            if (IsAutoFilter && !IsMultiSelect)
            {
                Filter();
            }
            else
            {
                if (Grid.Selection.Count > 0)
                {
                    Grid.MakeNodeVisible(Grid.FocusedNode);
                    //SelectRow(Grid.FocusedNode);
                }
            }
            this.Focus();
        }

        protected override void DoClosePopup(PopupCloseMode closeMode)
        {
            if (IsMultiSelect)
            {
                SetTexts();
            }
            base.DoClosePopup(closeMode);
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
            if (isSearch)
            {
                ShowPopup();
                Filter();
            }
            base.OnEditValueChanged();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)
            {
                SwitchNodeStatus();
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClosePopupCore();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (IsPopupOpen)
                {
                    if (Grid.Selection.Count == 0)
                    {
                        Grid.MoveFirst();
                        Grid.FocusedNode.Selected = true;
                    }
                    else
                    {
                        Grid.MovePrev();
                        Grid.FocusedNode.Selected = true;
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
                    if (Grid.Selection.Count == 0)
                    {
                        Grid.MoveFirst();
                        Grid.FocusedNode.Selected = true;
                    }
                    else
                    {
                        Grid.MoveNext();
                        Grid.FocusedNode.Selected = true;
                    }
                }
                else
                {
                    ShowPopup();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                ResetPopup();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (!IsMultiSelect)
                {
                    ResetPopup();
                }
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void Filter()
        {
            this.SelectionStart = this.Text.Length;
            //if (!IsMultiSelect)
            {
                Grid.Filter(this.Text.Trim());
            }

            Grid.Selection.Clear();
            if (Grid.VisibleNodesCount > 0 && IsAutoSelectFirst)
            {
                Grid.MoveFirst();
                Grid.FocusedNode.Selected = true;
            }
        }

        protected virtual string GetSplitDisplay()
        {
            StringBuilder sb = new StringBuilder();
            foreach (object item in SelectedObjects)
            {
                sb.AppendFormat("{0}{1}", DisplayGetAction(item), SplitSymbol);
            }
            return sb.ToString().TrimEnd(SplitSymbol.ToCharArray());
        }

        protected virtual string GetSplitValue()
        {
            StringBuilder sb = new StringBuilder();
            foreach (object item in SelectedObjects)
            {
                sb.AppendFormat("{0}{1}", ValueGetAction(item), SplitSymbol);
            }
            return sb.ToString().TrimEnd(SplitSymbol.ToCharArray());
        }

        private void SwitchNodeStatus()
        {
            if (!IsMultiSelect) return;

            Grid.FocusedNode.Checked = !Grid.FocusedNode.Checked;
            SwitchNodeStatusData(Grid.FocusedNode);
        }

        private void SwitchNodeStatusData(TreeListNode node)
        {
            Action<TreeListNode> action = p =>
            {
                if (p.Checked)
                {
                    SelectedObjects.AddOrUpdate(Grid.Get<EntityBase>(p));
                }
                else
                {
                    SelectedObjects.Remove(Grid.Get<EntityBase>(p));
                }
            };
            action(node);
            if (IsCheckParent)
            {
                Grid.CheckParent(node, action);
            }
            if (IsCheckChild)
            {
                Grid.CheckChild(node, action);
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

        private void GetSelectedObjIndex(object value)
        {
            if (value == null)
            {
                _setNode = null;
                _setObject = null;
                return;
            }

            var listDataSource = DataSource as IList;
            if (listDataSource != null)
            {
                foreach (object item in listDataSource)
                {
                    if (ValueGetAction(item).ToString().Equals(value.ToString()))
                    {
                        _setNode = Grid.FindNodeByFieldValue(ValueMember, ValueGetAction(item));
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
                            _setNode = Grid.FindNodeByFieldValue(ValueMember, ValueGetAction(item));
                            _setObject = item;
                            break;
                        }
                    }
                }
            }
        }

        private void GetSelectedObj(string values)
        {
            if (string.IsNullOrEmpty(values)) return;
            string[] sz = StringHelper.StringToArray(values, SplitSymbol);
            foreach (var v in sz)
            {
                var listDataSource = DataSource as IList;
                if (listDataSource != null)
                {
                    foreach (object item in listDataSource)
                    {
                        if (ValueGetAction(item).ToString().Equals(v))
                        {
                            SelectedObjects.AddOrUpdate(item);
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
                            if (ValueGetAction(item).ToString().Equals(values))
                            {
                                SelectedObjects.AddOrUpdate(item);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void SelectRow(TreeListNode node)
        {
            if (node == null) return;
            Grid.Selection.Clear();
            Grid.SetFocusedNode(node);
            node.Selected = true;
        }

        public void SelectRows(bool status)
        {
            if (SelectedObjects == null) return;
            foreach (var item in SelectedObjects)
            {
                var node = Grid.GetNode(item);
                if (node != null)
                {
                    node.Checked = status;
                }
            }
        }

        public void SetTexts()
        {
            isSearch = false;
            if (SelectedObjects == null)
            {
                this.Text = string.Empty;
            }
            else
            {
                this.Text = GetSplitDisplay();
                this.SelectionStart = this.Text.Length;
            }
            isSearch = true;
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
            this.Grid.MoveFirst();
            this.isClick = false;
            SetText(null);
            Grid.ClearColumnsFilter();
            Grid.Selection.Clear();
            SelectRows(false);
            SelectedObjects.Clear();
            isSearch = true;
        }

        private void ClosePopupCore()
        {
            isSearch = false;
            this.isClick = false;
            if (IsMultiSelect)
            {
                SetTexts();
            }
            else
            {
                SetText(SelectedObject);
            }
            if (!IsAutoFilter || IsMultiSelect)
            {
                Grid.Filter(string.Empty);
            }
            PopupSelectedEventArgs selectedArgs = new PopupSelectedEventArgs();
            selectedArgs.SelectedObject = SelectedObject;
            selectedArgs.Text = this.Text;
            selectedArgs.Value = SelectedValue;
            OnPopupSelected(selectedArgs);
            isSearch = true;
            this.ClosePopup();
        }

        /// <summary>
        /// 添加一个表格列
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="caption">列标题</param>
        /// <param name="width">宽度</param>
        public TreeListColumn AddColumn(string fieldName, string caption, int width)
        {
            TreeListColumn column = Grid.Columns.Add();
            column.Caption = caption;
            column.FieldName = fieldName;
            column.Width = width;
            column.Visible = true;
            column.VisibleIndex = 0;
            return column;
        }

        #endregion
    }
}