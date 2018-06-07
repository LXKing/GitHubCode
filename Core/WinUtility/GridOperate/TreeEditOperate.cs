using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using XCI.Component;
using XCI.Core;
using XCI.Extension;
using XCI.Helper;

namespace XCI.WinUtility
{
    /// <summary>
    /// 数据编辑操作
    /// </summary>
    /// <typeparam name="E">实体类型</typeparam>
    /// <typeparam name="S">接口类型</typeparam>
    public class TreeEditOperate<E, S>
        where E : EntityBase, new()
        where S : class,IEntityService<E>
    {
        #region 字段

        private EntityMetadata _metadata;
        private bool _isBindKeyShortcut = true;

        #endregion

        #region 权限

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


        #endregion

        #region 属性

        /// <summary>
        /// 表格控件
        /// </summary>
        public XCITreeGrid Grid { get; set; }

        /// <summary>
        /// 所属窗口
        /// </summary>
        public Form OwnerForm { get; set; }

        /// <summary>
        /// 编辑面板
        /// </summary>
        public Panel EditPanel { get; set; }

        /// <summary>
        /// 状态面板
        /// </summary>
        public Panel StatusPanel { get; set; }

        /// <summary>
        /// 第一个控件
        /// </summary>
        public BaseEdit FirstControl { get; set; }

        /// <summary>
        /// 最后一个控件
        /// </summary>
        public BaseEdit LastControl { get; set; }

        /// <summary>
        /// 新建按钮
        /// </summary>
        public SimpleButton NewButton { get; set; }

        /// <summary>
        /// 删除按钮
        /// </summary>
        public SimpleButton DeleteButton { get; set; }

        /// <summary>
        /// 复制按钮
        /// </summary>
        public SimpleButton CopyButton { get; set; }

        /// <summary>
        /// 保存按钮
        /// </summary>
        public SimpleButton SaveButton { get; set; }

        /// <summary>
        /// 保存并新增按钮
        /// </summary>
        public SimpleButton SaveNewButton { get; set; }

        /// <summary>
        /// 保存并关闭按钮
        /// </summary>
        public SimpleButton SaveCloseButton { get; set; }

        /// <summary>
        /// 移到首条按钮
        /// </summary>
        public SimpleButton MoveFirstButton { get; set; }

        /// <summary>
        /// 移到上一条按钮
        /// </summary>
        public SimpleButton MovePreviousButton { get; set; }

        /// <summary>
        /// 移到下一条按钮
        /// </summary>
        public SimpleButton MoveNextButton { get; set; }

        /// <summary>
        /// 移到最后一条按钮
        /// </summary>
        public SimpleButton MoveLastButton { get; set; }


        private string _deleteConfirmMessage = "确定要删除当前记录吗?";
        public string DeleteConfirmMessage
        {
            get { return _deleteConfirmMessage; }
            set { _deleteConfirmMessage = value; }
        }

        /// <summary>
        /// 是否绑定窗口快捷键
        /// </summary>
        public bool IsBindKeyShortcut
        {
            get { return _isBindKeyShortcut; }
            set { _isBindKeyShortcut = value; }
        }

        /// <summary>
        /// 是否初始化
        /// </summary>
        public bool IsInitialization { get; protected set; }

        /// <summary>
        /// 当前操作实体
        /// </summary>
        public E CurrentEntity { get; set; }

        /// <summary>
        /// 当前操作实体ID
        /// </summary>
        public int CurrentID
        {
            get
            {
                if (CurrentEntity != null)
                {
                    return CurrentEntity.ID;
                }
                return 0;
            }
        }

        /// <summary>
        /// 表格选中实体副本
        /// </summary>
        protected E SelectedEntityCopy
        {
            get
            {
                if (Grid == null) return null;
                if (Grid.DataSource != null)
                {
                    if (Grid.DataSource is IList)
                    {
                        return (E)Grid.GetSelected<E>().Clone();
                    }
                    if (Grid.DataSource is DataTable)
                    {
                        var copyRow = DataTableHelper.CopyDataRow(Grid.GetSelected<DataRow>());
                        return Factory.Default.MapToEntity(copyRow);
                    }
                    return null;
                }
                return (E)Grid.GetSelected<E>().Clone();
            }
        }

        /// <summary>
        /// 当前是否是新建
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// 控件值是否发生变化
        /// </summary>
        protected bool IsEditValueChanged { get; set; }

        /// <summary>
        /// 是否是内部赋值
        /// </summary>
        protected bool IsInnerSetValue { get; set; }


        //private bool _isSaveSelectRecord = true;

        ///// <summary>
        ///// 保存后选中记录
        ///// </summary>
        //public virtual bool IsSaveSelectRecord
        //{
        //    get { return _isSaveSelectRecord; }
        //    set { _isSaveSelectRecord = value; }
        //}

        /// <summary>
        /// 实体元数据
        /// </summary>
        protected EntityMetadata Metadata
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
        /// 记录操作图片
        /// </summary>
        public ImageList RecordImgList { get; set; }

        /// <summary>
        /// 导航图片
        /// </summary>
        public ImageList NavigateImgList { get; set; }

        /// <summary>
        /// 验证规则
        /// </summary>
        protected IList<ValidateRule> ValidateRules { get; set; }

        /// <summary>
        /// 检测权限
        /// </summary>
        public Action CheckAuthorized { get; set; }

        #endregion

        #region 事件

        #region 数据新增之前事件

        /// <summary>
        /// 数据新增之前事件
        /// </summary>
        public event EventHandler<NewEventArgs> BeforeNew;

        /// <summary>
        /// 触发数据新增之前
        /// </summary>
        /// <param name="e">新增之前事件参数</param>
        protected void OnBeforeNew(NewEventArgs e)
        {
            if (BeforeNew != null)
            {
                BeforeNew(this, e);
            }
        }

        #endregion

        #region 数据新增之后事件

        /// <summary>
        /// 数据新增之后事件
        /// </summary>
        public event EventHandler<NewEventArgs> AfterNew;

        /// <summary>
        /// 触发数据新增之后
        /// </summary>
        /// <param name="e">新增之后事件参数</param>
        protected void OnAfterNew(NewEventArgs e)
        {
            if (AfterNew != null)
            {
                AfterNew(this, e);
            }
        }

        #endregion

        #region 数据删除之前事件

        /// <summary>
        /// 数据删除之前事件
        /// </summary>
        public event EventHandler<DeleteEventArgs> BeforeDelete;

        /// <summary>
        /// 触发数据删除之前
        /// </summary>
        /// <param name="e">删除之前事件参数</param>
        protected void OnBeforeDelete(DeleteEventArgs e)
        {
            if (BeforeDelete != null)
            {
                BeforeDelete(this, e);
            }
        }

        #endregion

        #region 数据删除之后事件

        /// <summary>
        /// 数据删除之后事件
        /// </summary>
        public event EventHandler<DeleteEventArgs> AfterDelete;

        /// <summary>
        /// 触发数据删除之后
        /// </summary>
        /// <param name="e">删除之后事件参数</param>
        protected void OnAfterDelete(DeleteEventArgs e)
        {
            if (AfterDelete != null)
            {
                AfterDelete(this, e);
            }
        }

        #endregion

        #region 数据绑定事件

        /// <summary>
        /// 数据绑定事件
        /// </summary>
        public event EventHandler<BindEventArgs> Bind;

        /// <summary>
        /// 触发数据绑定
        /// </summary>
        /// <param name="e">绑定事件参数</param>
        protected void OnBind(BindEventArgs e)
        {
            if (Bind != null)
            {
                Bind(this, e);
            }
        }

        #endregion

        #region 关闭事件

        /// <summary>
        /// 关闭事件
        /// </summary>
        public event EventHandler<CloseEventArgs> Close;

        /// <summary>
        /// 触发关闭事件
        /// </summary>
        /// <param name="e">关闭事件参数</param>
        protected void OnClose(CloseEventArgs e)
        {
            if (Close != null)
            {
                Close(this, e);
            }
        }

        #endregion

        #region 数据验证事件

        /// <summary>
        /// 数据验证事件
        /// </summary>
        public event EventHandler<ValidateEventArgs> Validate;

        /// <summary>
        /// 触发数据验证
        /// </summary>
        /// <param name="e">验证事件参数</param>
        protected void OnValidate(ValidateEventArgs e)
        {
            if (Validate != null)
            {
                Validate(this, e);
            }
        }

        #endregion

        #region 数据保存前事件

        /// <summary>
        /// 数据保存前事件
        /// </summary>
        public event EventHandler<SaveEventArgs> BeforeSave;

        /// <summary>
        /// 触发数据保存前
        /// </summary>
        /// <param name="e">保存前事件参数</param>
        protected void OnBeforeSave(SaveEventArgs e)
        {
            if (BeforeSave != null)
            {
                BeforeSave(this, e);
            }
        }

        #endregion

        #region 数据保存后事件

        /// <summary>
        /// 数据保存后事件
        /// </summary>
        public event EventHandler<SaveEventArgs> AfterSave;

        /// <summary>
        /// 触发数据保存后
        /// </summary>
        /// <param name="e">保存后事件参数</param>
        protected void OnAfterSave(SaveEventArgs e)
        {
            if (AfterSave != null)
            {
                AfterSave(this, e);
            }
        }

        #endregion

        #region 数据复制之前事件

        /// <summary>
        /// 数据复制之前事件
        /// </summary>
        public event EventHandler<CopyEventArgs> BeforeCopy;

        /// <summary>
        /// 触发数据复制之前
        /// </summary>
        /// <param name="e">复制之前事件参数</param>
        protected void OnBeforeCopy(CopyEventArgs e)
        {
            if (BeforeCopy != null)
            {
                BeforeCopy(this, e);
            }
        }

        #endregion

        #region 数据复制之后事件

        /// <summary>
        /// 数据复制之后事件
        /// </summary>
        public event EventHandler<CopyEventArgs> AfterCopy;

        /// <summary>
        /// 触发数据复制之后
        /// </summary>
        /// <param name="e">数据复制之后事件参数</param>
        protected void OnAfterCopy(CopyEventArgs e)
        {
            if (AfterCopy != null)
            {
                AfterCopy(this, e);
            }
        }

        #endregion

        #region 控件值变化之前事件

        /// <summary>
        /// 控件值变化之前事件
        /// </summary>
        public event EventHandler<EditValueChangedEventArgs> BeforeEditValueChanged;

        /// <summary>
        /// 触发控件值变化之前
        /// </summary>
        /// <param name="e">控件值变化之前参数</param>
        protected void OnBeforeEditValueChanged(EditValueChangedEventArgs e)
        {
            if (BeforeEditValueChanged != null)
            {
                BeforeEditValueChanged(this, e);
            }
        }

        #endregion

        #region 控件值变化之后事件

        /// <summary>
        /// 控件值变化之后事件
        /// </summary>
        public event EventHandler<EditValueChangedEventArgs> AfterEditValueChanged;

        /// <summary>
        /// 触发控件值变化之后
        /// </summary>
        /// <param name="e">控件值变化之后参数</param>
        protected void OnAfterEditValueChanged(EditValueChangedEventArgs e)
        {
            if (AfterEditValueChanged != null)
            {
                AfterEditValueChanged(this, e);
            }
        }

        #endregion

        #endregion

        #region 数据验证

        /// <summary>
        /// 注册不能为空的验证规则
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="errorMessage">为空时的错误消息</param>
        public void RegisterNotEmptyRule(BaseEdit control, string errorMessage)
        {
            Func<bool> validateFun = () => control.Text.Length > 0;
            RegisterRule(control, validateFun, errorMessage, true);
        }

        /// <summary>
        /// 注册验证规则
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="validateFunc">验证函数 验证通过返回True</param>
        /// <param name="errorMessage">错误消息</param>
        public void RegisterRule(BaseEdit control, Func<bool> validateFunc, string errorMessage)
        {
            RegisterRule(control, validateFunc, errorMessage, true);
        }

        /// <summary>
        /// 注册验证规则
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="validateFunc">验证函数</param>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="isFocusControl">错误时是否激活控件焦点</param>
        public void RegisterRule(BaseEdit control, Func<bool> validateFunc, string errorMessage, bool isFocusControl)
        {
            ValidateRule item = new ValidateRule();
            item.Control = control;
            item.IsFocusControl = isFocusControl;
            item.ValidateFun = validateFunc;
            item.Message = errorMessage;

            if (ValidateRules == null)
            {
                ValidateRules = new List<ValidateRule>();
            }
            ValidateRules.Add(item);
        }


        /// <summary>
        /// 验证控件 如果控件为空验证全部规则
        /// </summary>
        /// <param name="control">控件</param>
        /// <returns>验证成功返回true</returns>
        public bool ValidateControl(BaseEdit control)
        {
            ValidateRule rule = GetFirstValidateRule(control);
            if (rule != null)
            {
                if (rule.Message.IsNotEmpty())
                {
                    XtraMessageBoxHelper.ShowWarning(rule.Message);
                }
                if (rule.IsFocusControl)
                {
                    rule.Control.SelectAll();
                    rule.Control.Select();
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取第一个没有通过的验证规则 如果指定控件那么查找与控件相关的规则 否则查找验证全部规则
        /// </summary>
        /// <param name="control">控件</param>
        /// <returns>返回第一个没有通过的规则</returns>
        protected ValidateRule GetFirstValidateRule(BaseEdit control)
        {
            if (ValidateRules != null && ValidateRules.Count > 0)
            {
                if (control != null)
                {
                    foreach (ValidateRule rule in ValidateRules)
                    {
                        if (rule.Control.Name.Equals(control.Name))
                        {
                            if (!rule.Validate())
                            {
                                return rule;
                            }
                        }
                    }
                    return null;
                }

                foreach (ValidateRule rule in ValidateRules)
                {
                    if (!rule.Validate())
                    {
                        return rule;
                    }
                }
            }
            return null;
        }

        #endregion

        #region 保护方法

        /// <summary>
        /// 初始化窗口
        /// </summary>
        protected void InitializeForm()
        {
            if (OwnerForm != null)
            {
                if (IsBindKeyShortcut)
                {
                    OwnerForm.KeyPreview = true;
                    OwnerForm.KeyUp += new KeyEventHandler(OwnerForm_KeyUp);
                }
                OwnerForm.FormClosing += OwnerForm_FormClosing;
            }
        }

        /// <summary>
        /// 初始化编辑面板
        /// </summary>
        protected void InitializationEditPanel()
        {
            if (EditPanel != null)
            {
                FormHelper.BindControlEnterEvent(EditPanel, GetActiveControlFunc, FirstControl, LastControl, LastControlAction, EditValueChangedNotifyAction, EnterValidateFunc);
            }
        }

        /// <summary>
        /// 初始化状态面板
        /// </summary>
        protected void InitializationStatusPanel()
        {
            if (StatusPanel != null)
            {
                FormHelper.ReadonlyControl(StatusPanel);
            }
        }

        /// <summary>
        /// 初始化按钮
        /// </summary>
        protected void InitializationButtonEvent()
        {
            RecordImgList = new ImageList();
            RecordImgList.ImageSize = new Size(16, 16);
            RecordImgList.Images.AddRange(new Image[]{
                    XCI.WinUtility.Properties.Resources.RecordSave,
                    XCI.WinUtility.Properties.Resources.RecordSaveNew,
                    XCI.WinUtility.Properties.Resources.RecordSaveClose,
                    XCI.WinUtility.Properties.Resources.RecordCopy,
                    XCI.WinUtility.Properties.Resources.RecordAdd,
                    XCI.WinUtility.Properties.Resources.RecordDelete
                });

            NavigateImgList = new ImageList();
            NavigateImgList.ImageSize = new Size(16, 16);
            NavigateImgList.Images.AddRange(new Image[]{
                XCI.WinUtility.Properties.Resources.goFirst,
                XCI.WinUtility.Properties.Resources.goPrevious,
                XCI.WinUtility.Properties.Resources.goNext,
                XCI.WinUtility.Properties.Resources.goLast
            });

            if (SaveButton != null)
            {
                SaveButton.Click += (o, e) => SaveMethod();
                SaveButton.ImageList = RecordImgList;
                SaveButton.ImageIndex = 0;
                SaveButton.ToolTip = "保存记录";
                SaveButton.Enabled = false;
            }
            if (SaveNewButton != null)
            {
                SaveNewButton.Click += (o, e) => SaveNewMethod();
                SaveNewButton.ImageList = RecordImgList;
                SaveNewButton.ImageIndex = 1;
                SaveNewButton.ToolTip = "保存并新增";
                SaveNewButton.Enabled = false;
            }
            if (SaveCloseButton != null)
            {
                SaveCloseButton.Click += (o, e) => SaveCloseMethod();
                SaveCloseButton.ImageList = RecordImgList;
                SaveCloseButton.ImageIndex = 2;
                SaveCloseButton.ToolTip = "保存并关闭";
                SaveCloseButton.Enabled = false;
            }
            if (CopyButton != null)
            {
                CopyButton.Click += (o, e) => CopyMethod();
                CopyButton.ImageList = RecordImgList;
                CopyButton.ImageIndex = 3;
                CopyButton.ToolTip = "复制记录";
                CopyButton.Enabled = false;
            }
            if (NewButton != null)
            {
                NewButton.Click += (o, e) => NewMethod();
                NewButton.ImageList = RecordImgList;
                NewButton.ImageIndex = 4;
                NewButton.ToolTip = "新增记录";
                NewButton.Enabled = false;
            }

            if (DeleteButton != null)
            {
                DeleteButton.Click += (o, e) => DeleteMethod();
                DeleteButton.ImageList = RecordImgList;
                DeleteButton.ImageIndex = 5;
                DeleteButton.ToolTip = "删除记录";
                DeleteButton.Enabled = false;
            }


            if (MoveFirstButton != null)
            {
                MoveFirstButton.Click += (o, e) => MoveFirstMethod();
                MoveFirstButton.ImageList = NavigateImgList;
                MoveFirstButton.ImageIndex = 0;
                MoveFirstButton.ToolTip = "移到最前";
            }
            if (MovePreviousButton != null)
            {
                MovePreviousButton.Click += (o, e) => MovePreviousMethod();
                MovePreviousButton.ImageList = NavigateImgList;
                MovePreviousButton.ImageIndex = 1;
                MovePreviousButton.ToolTip = "前移";
            }
            if (MoveNextButton != null)
            {
                MoveNextButton.Click += (o, e) => MoveNextMethod();
                MoveNextButton.ImageList = NavigateImgList;
                MoveNextButton.ImageIndex = 2;
                MoveNextButton.ToolTip = "后移";
            }
            if (MoveLastButton != null)
            {
                MoveLastButton.Click += (o, e) => MoveLastMethod();
                MoveLastButton.ImageList = NavigateImgList;
                MoveLastButton.ImageIndex = 3;
                MoveLastButton.ToolTip = "移到最后";
            }

        }


        /// <summary>
        /// 窗口键盘事件
        /// </summary>
        void OwnerForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Right)
            {
                e.Handled = true;
                MoveNextMethod();
            }
            else if (e.Control && e.KeyCode == Keys.Left)
            {
                e.Handled = true;
                MovePreviousMethod();
            }
            else if (e.Control && e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                MoveFirstMethod();
            }
            else if (e.Control && e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                MoveLastMethod();
            }
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        void OwnerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseEventArgs args = new CloseEventArgs();
            SetEditEventArgs(args);
            OnClose(args);
            e.Cancel = !args.IsSuccess;
        }

        /// <summary>
        /// 焦点第一个编辑控件
        /// </summary>
        protected void SelectFirstControl()
        {
            if (FirstControl != null)
            {
                FirstControl.Select();
            }
        }

        /// <summary>
        /// 设置按钮状态
        /// </summary>
        protected virtual void SetButtonStatus()
        {
            if (MoveFirstButton != null) MoveFirstButton.Enabled = !Grid.IsFirstNode;
            if (MovePreviousButton != null) MovePreviousButton.Enabled = !Grid.IsFirstNode;
            if (MoveNextButton != null) MoveNextButton.Enabled = !Grid.IsLastNode;
            if (MoveLastButton != null) MoveLastButton.Enabled = !Grid.IsLastNode;

            if (NewButton != null) NewButton.Enabled = AllowCreate;
            if (IsNew)
            {
                if (SaveButton != null && AllowCreate) SaveButton.Enabled = true;
                if (SaveCloseButton != null && AllowCreate) SaveCloseButton.Enabled = true;
                if (SaveNewButton != null && AllowCreate) SaveNewButton.Enabled = true;
                if (CopyButton != null) CopyButton.Enabled = false;
                if (DeleteButton != null) DeleteButton.Enabled = false;
            }
            else
            {
                if (CurrentEntity != null)
                {
                    if (CopyButton != null && AllowCreate) CopyButton.Enabled = true;
                    if (DeleteButton != null && AllowDelete) DeleteButton.Enabled = true;

                }
                else
                {
                    if (CopyButton != null) CopyButton.Enabled = false;
                    if (DeleteButton != null) DeleteButton.Enabled = false;
                }

                if (SaveButton != null) SaveButton.Enabled = false;
                if (SaveCloseButton != null) SaveCloseButton.Enabled = false;
                if (SaveNewButton != null) SaveNewButton.Enabled = false;
            }
            if (!Grid.IsAutoDeleteChild && Grid.FocusedNode != null && Grid.FocusedNode.HasChildren)
            {
                if (DeleteButton != null) DeleteButton.Enabled = false;
            }
            if (CheckAuthorized != null) CheckAuthorized();
        }

        /// <summary>
        /// 设置编辑事件参数
        /// </summary>
        /// <param name="args">编辑事件参数</param>
        protected void SetEditEventArgs(EditEventArgs args)
        {
            args.IsNew = IsNew;
            args.BindEntity = CurrentEntity;
        }

        /// <summary>
        /// 通知控件值发生改变
        /// </summary>
        protected void EditValueChangedNotifyAction()
        {
            if (IsInnerSetValue) return;

            EditValueChangedEventArgs beforeArgs = new EditValueChangedEventArgs();
            SetEditEventArgs(beforeArgs);
            OnBeforeEditValueChanged(beforeArgs);
            if (!beforeArgs.IsSuccess) return;

            IsEditValueChanged = true;
            bool isEdit = false;
            if (IsNew)
            {
                isEdit = AllowCreate;
            }
            else
            {
                isEdit = AllowEdit;
            }
            if (SaveButton != null && isEdit) SaveButton.Enabled = true;
            if (SaveCloseButton != null && isEdit) SaveCloseButton.Enabled = true;
            if (SaveNewButton != null && isEdit) SaveNewButton.Enabled = true;

            if (CheckAuthorized != null) CheckAuthorized();

            EditValueChangedEventArgs afterArgs = new EditValueChangedEventArgs();
            SetEditEventArgs(afterArgs);
            OnAfterEditValueChanged(afterArgs);
        }

        /// <summary>
        /// 获取激活的控件
        /// </summary>
        protected Control GetActiveControlFunc()
        {
            if (OwnerForm != null)
            {
                return OwnerForm.ActiveControl;
            }
            return null;
        }

        /// <summary>
        /// 最后一个控件回车时的回调
        /// </summary>
        protected void LastControlAction()
        {
            if (IsNew)
            {
                if (SaveNewButton != null && SaveNewButton.Enabled)
                {
                    SaveNewButton.Select();
                    SaveNewButton.PerformClick();
                }
                else
                {
                    SelectFirstControl();
                }
            }
            else
            {
                if (SaveButton != null && SaveButton.Enabled)
                {
                    SaveButton.Select();
                    SaveButton.PerformClick();
                }
                else
                {
                    SelectFirstControl();
                }
            }
        }

        /// <summary>
        /// 焦点移动时验证控件
        /// </summary>
        /// <param name="control">控件</param>
        protected bool EnterValidateFunc(BaseEdit control)
        {
            if (ValidateControl(control))
            {
                ValidateEventArgs args = new ValidateEventArgs();
                SetEditEventArgs(args);
                args.Control = control;
                OnValidate(args);
                return args.IsSuccess;
            }
            return false;
        }

        /// <summary>
        /// 保存数据具体实现
        /// </summary>
        /// <returns>成功返回True</returns>
        protected bool SaveCore()
        {
            try
            {
                if (!ValidateControl(null)) return false;
                SaveEventArgs beforeargs = new SaveEventArgs();
                beforeargs.IsNew = IsNew;
                beforeargs.BindEntity = CurrentEntity;
                OnBeforeSave(beforeargs);
                if (!beforeargs.IsSuccess) return false;

                if (CurrentEntity == null) return false;

                if (IsNew)
                {
                    int sortCode = 0;
                    if (ParentNode == null && Grid.Nodes.Count > 0)
                    {
                        sortCode = Grid.Nodes.Count - 1;
                    }
                    else if (ParentNode != null)
                    {
                        var pNode = Grid.FindNodeByID(ParentNode.Id);
                        if (pNode!=null)
                        {
                            sortCode = pNode.Nodes.Count;
                        }
                    }
                    CurrentEntity.SetPropertyValue(Metadata.SortCodeFieldName, sortCode);
                    Service.Create(CurrentEntity);
                    if (Grid.DataSource != null)
                    {
                        if (Grid.DataSource is IList)
                        {
                            Grid.Add(CurrentEntity);
                        }
                        else if (Grid.DataSource is DataTable)
                        {
                            var row = Factory.Default.MapToDataRow(Grid.TableSource, CurrentEntity);
                            Grid.Add(row);
                        }
                    }
                }
                else
                {
                    Service.Update(CurrentEntity);
                    if (Grid.DataSource is IList)
                    {
                        Grid.Update(CurrentEntity);
                    }
                    else if (Grid.DataSource is DataTable)
                    {
                        var index = Grid.TableSource.Rows.IndexOf(Grid.GetSelected<DataRow>());
                        if (index > -1)
                        {
                            var row = Grid.TableSource.Rows[index];
                            Factory.Default.UpdateDataRow(row, CurrentEntity);
                        }
                    }
                }

                SaveEventArgs afterArgs = new SaveEventArgs();
                afterArgs.IsNew = IsNew;
                afterArgs.BindEntity = CurrentEntity;
                OnAfterSave(afterArgs);
                if (afterArgs.IsSuccess)
                {
                    SetButtonStatus();
                }
                IsNew = false;
                IsEditValueChanged = false;
                return true;
            }
            catch (VersionException ex)
            {
                //LogManager.Error(ex.Message, "AppUser", ex);
                var newEntity = (E)ex.NewEntity;
                BindMethod(newEntity);
                //Grid.UpdateEntity(newEntity);
                XtraMessageBoxHelper.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                XtraMessageBoxHelper.ShowError(ex.Message);
                //LogManager.Error(ex.Message, "AppUser", ex);
            }
            return false;
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
        /// <param name="isBindData">是否自动绑定数据</param>
        public void Initialize(bool isBindData)
        {
            if (IsInitialization) return;

            InitializationEditPanel();
            InitializationStatusPanel();
            InitializationButtonEvent();
            InitializeForm();


            if (isBindData) BindData();
            if (CheckAuthorized != null) CheckAuthorized();
            IsInitialization = true;
        }

        /// <summary>
        /// 绑定数据 新增时调用新增方法 编辑时调用编辑方法
        /// </summary>
        public void BindData()
        {
            if (IsNew)
            {
                NewMethod();
            }
            else
            {
                EditMethod();
            }
        }

        /// <summary>
        /// 绑定实体数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isCopy">是否复制</param>
        public virtual void BindMethod(E entity, bool isCopy = false)
        {
            IsInnerSetValue = true;
            CurrentEntity = entity;
            BindEventArgs args = new BindEventArgs();
            args.IsNew = IsNew;
            args.IsCopy = isCopy;
            args.BindEntity = entity;
            OnBind(args);
            if (args.IsSuccess)
            {
                if (IsNew && Metadata != null && Grid != null && Metadata.ParentFieldName.IsNotEmpty())
                {
                    int parentID = 0;
                    if (ParentEntity != null)
                    {
                        parentID = ParentEntity.ID;
                    }
                    CurrentEntity.SetPropertyValue(Metadata.ParentFieldName, parentID);
                }

                if (EditPanel != null) FormHelper.BindControlValue(EditPanel, entity);

                if (StatusPanel != null) FormHelper.BindControlValue(StatusPanel, entity);
                SetButtonStatus();

            }
            IsInnerSetValue = false;
        }

        public TreeListNode ParentNode { get; set; }
        public E ParentEntity
        {
            get
            {
                if (ParentNode != null)
                {
                    return Grid.Get<E>(ParentNode);
                }
                return null;
            }
        }

        private void SetParentObject()
        {
            ParentNode = Grid.FocusedNode.ParentNode;
        }

        //public TreeListNode ParentNode
        //{
        //    get
        //    {
        //        TreeListNode obj = null;
        //        if (IsEditRoot)
        //        {
        //            obj = null;
        //        }
        //        else if (IsNew)
        //        {
        //            obj = Grid.FocusedNode.ParentNode;
        //        }
        //        else
        //        {
        //            obj = Grid.FocusedNode;
        //        }
        //        return obj;
        //    }
        //}
        //public E ParentEntity
        //{
        //    get
        //    {
        //        object obj = null;
        //        if (IsEditRoot)
        //        {
        //            obj = null;
        //        }
        //        else if (IsNew)
        //        {
        //            obj = Grid.GetSelectedParent();
        //        }
        //        else
        //        {
        //            obj = Grid.GetSelected();
        //        }

        //        return obj as E;
        //    }
        //}

        /// <summary>
        /// 新增方法
        /// </summary>
        public void NewMethod()
        {
            IsNew = true;

            NewEventArgs beforeArgs = new NewEventArgs();
            beforeArgs.IsNew = IsNew;
            OnBeforeNew(beforeArgs);
            if (!beforeArgs.IsSuccess) return;

            if (beforeArgs.NewEntity == null)
            {
                beforeArgs.NewEntity = new E();
            }
            if (beforeArgs.NewEntity != null)
            {
                E entity = (E)beforeArgs.NewEntity;
                BindMethod(entity);
                SelectFirstControl();
                NewEventArgs afterArgs = new NewEventArgs();
                afterArgs.IsNew = IsNew;
                afterArgs.BindEntity = entity;
                OnAfterNew(afterArgs);
            }
        }

        /// <summary>
        /// 编辑方法
        /// </summary>
        public void EditMethod()
        {
            BindMethod(SelectedEntityCopy);
            SelectFirstControl();
        }

        /// <summary>
        /// 复制方法
        /// </summary>
        public void CopyMethod()
        {
            IsNew = true;

            CopyEventArgs beforeArgs = new CopyEventArgs();
            SetEditEventArgs(beforeArgs);
            beforeArgs.IsCopy = true;
            OnBeforeCopy(beforeArgs);
            if (!beforeArgs.IsSuccess) return;
            if (beforeArgs.CopyEntity == null)
            {
                beforeArgs.CopyEntity = CurrentEntity.Clone();
            }

            if (beforeArgs.CopyEntity != null)
            {
                E entity = (E)beforeArgs.CopyEntity;
                entity.ID = 0;
                BindMethod(entity, true);
                SelectFirstControl();

                CopyEventArgs afterArgs = new CopyEventArgs();
                SetEditEventArgs(afterArgs);
                afterArgs.IsCopy = true;
                OnAfterCopy(afterArgs);
            }
        }

        /// <summary>
        /// 关闭方法
        /// </summary>
        public void CloseMethod()
        {
            //if (IsSaveSelectRecord)
            //{
            TreeListNode node = Grid.FindNodeByKeyID(CurrentEntity.ID);
            if (node != null)
            {
                Grid.Select(node);
            }
            //}
            if (OwnerForm != null)
            {
                OwnerForm.Close();
            }
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        public void SaveMethod()
        {
            if (SaveCore())
            {
                if (Grid != null)
                {
                    var node = Grid.FindNodeByKeyID(CurrentEntity.ID);
                    Grid.SetFocusedNode(node);
                }

                BindMethod(SelectedEntityCopy);
                SelectFirstControl();
            }
        }

        /// <summary>
        /// 保存关闭方法
        /// </summary>
        public void SaveCloseMethod()
        {
            if (SaveCore())
            {
                CloseMethod();
            }
        }

        /// <summary>
        /// 保存新建方法
        /// </summary>
        public void SaveNewMethod()
        {
            if (SaveCore())
            {
                NewMethod();
            }
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        public void DeleteMethod()
        {
            DeleteEventArgs beforeArgs = new DeleteEventArgs();
            SetEditEventArgs(beforeArgs);
            OnBeforeDelete(beforeArgs);
            if (!beforeArgs.IsSuccess) return;
            if (CurrentEntity == null) return;

            XtraMessageBoxHelper.ShowYesNoAndWarning(DeleteConfirmMessage, d =>
            {
                try
                {
                    if (!Grid.IsAutoDeleteChild && Grid.FocusedNode.HasChildren)
                    {
                        XtraMessageBoxHelper.ShowWarning("此节点有下级 不允许删除");
                        return;
                    }
                    Service.Delete(CurrentEntity);
                    Grid.Delete(Grid.GetSelected());

                    DeleteEventArgs afterArgs = new DeleteEventArgs();
                    SetEditEventArgs(afterArgs);
                    OnAfterDelete(afterArgs);
                    if (afterArgs.IsSuccess)
                    {
                        BindMethod(SelectedEntityCopy);
                        SetButtonStatus();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBoxHelper.ShowError(ex.Message);
                }
            });
        }

        /// <summary>
        /// 移动到第一条
        /// </summary>
        public virtual void MoveFirstMethod()
        {
            IsNew = false;
            Grid.MoveFirst();
            SetParentObject();
            BindMethod(SelectedEntityCopy);
        }

        /// <summary>
        /// 移动到上一条
        /// </summary>
        public virtual void MovePreviousMethod()
        {
            IsNew = false;
            Grid.MovePrev();
            SetParentObject();
            BindMethod(SelectedEntityCopy);
        }

        /// <summary>
        /// 移动到下一条
        /// </summary>
        public virtual void MoveNextMethod()
        {
            IsNew = false;
            Grid.MoveNext();
            SetParentObject();
            BindMethod(SelectedEntityCopy);
        }

        /// <summary>
        /// 移动到最后一条
        /// </summary>
        public virtual void MoveLastMethod()
        {
            IsNew = false;
            Grid.MoveLastVisible();
            SetParentObject();
            BindMethod(SelectedEntityCopy);
        }

        #endregion
    }

}