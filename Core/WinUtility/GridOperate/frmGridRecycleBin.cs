using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using XCI.Component;
using XCI.Core;
using XCI.Extension;

namespace XCI.WinUtility
{
    public partial class frmGridRecycleBin : FormBase
    {
        public frmGridRecycleBin()
        {
            InitializeComponent();
        }


        #region 属性

        public dynamic Operate { get; set; }

        #endregion


        #region 方法

        /// <summary>
        /// 绑定控件属性
        /// </summary>
        protected void BindControl()
        {
            gridControl.IsStoreConfig = false;
            gridControl.GridID = Operate.Grid.GridID;
            gridControl.LoadConfig(new Guid());
            gridControl.View.ClearGrouping();
            btnRestore.Enabled = btnClear.Enabled = btnDelete.Enabled = false;
        }

        /// <summary>
        /// 初始化控件属性
        /// </summary>
        protected void InitControl()
        {
            gridControl.DataSource = Operate.Factory.Default.GetDeleteList();
        }

        protected void ForeachSelectedList(Action<EntityBase> action)
        {
            if (action == null) return;
            var list = List.GetSelectedCheckboxList();
            for (int index = 0; index < list.Count; index++)
            {
                EntityBase item = (EntityBase)list[index];
                action(item);
            }
        }

        protected void Delete()
        {
            List<int> ids = new List<int>();
            ForeachSelectedList(p =>
            {
                ids.Add(p.ID);
                List.Grid.Delete(p);
            });
            string pkName = Operate.Metadata.PrimaryKeyFieldName;
            Query query = Operate.Factory.Default.CreateQuery();
            query.Where(pkName).In(ids);
            Operate.Factory.Default.Delete(query);

        }

        protected void Restore()
        {
            string deleteName = Operate.Metadata.DeleteFieldName;
            Dictionary<object, Dictionary<string, object>> dic = new Dictionary<object, Dictionary<string, object>>();
            ForeachSelectedList(p =>
            {
                var valueDic = new Dictionary<string, object>();
                valueDic.Add(deleteName, 0);
                dic.Add(p.ID, valueDic);
                List.Grid.Delete(p);
            });
            Operate.Factory.Default.BatchUpdateField(dic);
        }

        #endregion


        #region 操作对象

        private GridListOperate<EntityBase, IEntityService<EntityBase>> _list;
        /// <summary>
        /// 操作对象
        /// </summary>
        public GridListOperate<EntityBase, IEntityService<EntityBase>> List
        {
            get
            {
                if (_list == null)
                {
                    _list = new GridListOperate<EntityBase, IEntityService<EntityBase>>();
                    _list.OwnerForm = this;
                    _list.Grid = gridControl;
                    _list.Grid.MainBar = Operate.Grid.MainBar;
                    _list.SearchEdit = editSearchBox;
                    _list.DeleteButton = btnDelete;
                    _list.ExportButton = btnExport;
                    _list.SelectAllButton = btnSelectAll;
                    _list.SelectInverseButton = btnSelectInverse;
                    _list.IsEnableContextMenu = false;
                    _list.Grid.IsShowConfigButton = false;
                }
                return _list;
            }
        }
        #endregion


        private void frmGridRecycleBin_Load(object sender, EventArgs e)
        {
            BindControl();
            List.BeforeDelete += List_BeforeDelete;
            List.AfterSelected += List_AfterSelected;
            List.Initialize(false);
            InitControl();
        }

        void List_AfterSelected(object sender, ListSelectedEventArgs e)
        {
            if (e.Entity == null)
            {
                btnRestore.Enabled = btnClear.Enabled = btnDelete.Enabled = false;
            }
            else
            {
                btnRestore.Enabled = btnClear.Enabled = btnDelete.Enabled = true;
            }
        }

        void List_BeforeDelete(object sender, ListDeleteEventArgs e)
        {
            if (XtraMessageBoxHelper.ShowYesNoAndTips("确实要彻底删除选中的数据吗?删除后数据无法恢复") == System.Windows.Forms.DialogResult.Yes)
            {
                Delete();
            }
            e.IsSuccess = false;
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (XtraMessageBoxHelper.ShowYesNoAndTips("确实要还原选中的数据吗?") == System.Windows.Forms.DialogResult.Yes)
            {
                Restore();
                Operate.LoadMethod();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (XtraMessageBoxHelper.ShowYesNoAndTips("确实要清空当前数据列表吗?删除后数据无法恢复") == System.Windows.Forms.DialogResult.Yes)
            {
                List.SelectAllMethod();
                Delete();
            }
        }


    }
}
