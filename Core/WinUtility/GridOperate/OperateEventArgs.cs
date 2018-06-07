using System;
using System.Collections;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using XCI.Component;

namespace XCI.WinUtility
{
    #region 事件参数

    /// <summary>
    /// 表格操作事件参数基类
    /// </summary>
    public class ListOperateBaseEventArgs : EventArgs
    {
        private bool _isSuccess = true;
        
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; }
        }
    }

    /// <summary>
    /// 表格数据加载事件参数
    /// </summary>
    public class ListLoadEventArgs : ListOperateBaseEventArgs
    {
        /// <summary>
        /// 实体类型
        /// </summary>
        public Type EntityType { get; set; }


        /// <summary>
        /// 数据源
        /// </summary>
        public object DataSource { get; set; }

    }

    /// <summary>
    /// 表格数据选中事件参数
    /// </summary>
    public class ListSelectedEventArgs : ListOperateBaseEventArgs
    {
        /// <summary>
        /// 选中的行对象
        /// </summary>
        public object Entity { get; internal set; }
    }

    /// <summary>
    /// 表格复选框状态变化事件参数
    /// </summary>
    public class ListCheckedChangedEventArgs : ListSelectedEventArgs
    {
        public TreeListNode Node { get; set; }
        /// <summary>
        /// 是否选中复选框
        /// </summary>
        public bool Checked { get; set; }
    }

    /// <summary>
    /// 表格数据删除事件参数
    /// </summary>
    public class ListDeleteEventArgs : ListOperateBaseEventArgs
    {
        /// <summary>
        /// 实体列表
        /// </summary>
        public object EntityList { get; set; }
    }

    /// <summary>
    /// 表格数据编辑事件参数
    /// </summary>
    public class ListEditEventArgs : ListSelectedEventArgs
    {
        /// <summary>
        /// 上级对象(TreeList使用)
        /// </summary>
        public object ParentEntity { get; set; }

        /// <summary>
        /// 是否是新增
        /// </summary>
        public bool IsNew { get; internal set; }

    }

    /// <summary>
    /// 表格列值变化事件参数
    /// </summary>
    public class ListCellValueChanged : ListSelectedEventArgs
    {
        /// <summary>
        /// 实体主键
        /// </summary>
        public int EntityID { get; internal set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; internal set; }

        /// <summary>
        /// 列值
        /// </summary>
        public object ColumnValue { get; set; }

        /// <summary>
        /// 是否是复选框选择列
        /// </summary>
        public bool IsSelectColumn { get; set; }
    }
    
    /// <summary>
    /// 表格操作事件参数基类
    /// </summary>
    public class EditOperateBaseEventArgs : EventArgs
    {
        private bool _isSuccess = true;

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; }
        }
    }

    /// <summary>
    /// 新建事件参数
    /// </summary>
    public class NewEventArgs : EditEventArgs
    {
        /// <summary>
        /// 新实体对象
        /// </summary>
        public object NewEntity { get; set; }
    }

    /// <summary>
    /// 编辑事件参数
    /// </summary>
    public class EditEventArgs : EditOperateBaseEventArgs
    {
        /// <summary>
        /// 是否新建
        /// </summary>
        public bool IsNew { get; internal set; }

        /// <summary>
        /// 是否复制
        /// </summary>
        public bool IsCopy { get; internal set; }

        /// <summary>
        /// 选中的实体对象
        /// </summary>
        public object BindEntity { get;  set; }
    }

    /// <summary>
    /// 绑定事件参数
    /// </summary>
    public class BindEventArgs : EditEventArgs
    {

    }

    /// <summary>
    /// 复制事件参数
    /// </summary>
    public class CopyEventArgs : EditEventArgs
    {
        /// <summary>
        /// 复制的实体对象
        /// </summary>
        public object CopyEntity { get; set; }
    }

    /// <summary>
    /// 删除事件参数
    /// </summary>
    public class DeleteEventArgs : EditEventArgs
    {

    }

    /// <summary>
    /// 编辑值变化事件参数
    /// </summary>
    public class EditValueChangedEventArgs : EditEventArgs
    {

    }

    /// <summary>
    /// 保存事件参数
    /// </summary>
    public class SaveEventArgs : EditEventArgs
    {

    }

    /// <summary>
    /// 验证事件
    /// </summary>
    public class ValidateEventArgs : EditEventArgs
    {
        public BaseEdit Control { get; set; }
    }

    /// <summary>
    /// 关闭事件参数
    /// </summary>
    public class CloseEventArgs : EditEventArgs
    {

    }

    #endregion
}