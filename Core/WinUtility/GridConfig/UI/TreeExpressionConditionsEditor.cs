using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Design;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.StyleFormatConditions;
using XCI.Helper;

namespace XCI.WinUtility.GridConfig
{
    public partial class TreeExpressionConditionsEditor : UserControlBase
    {
        /// <summary>
        /// 目标表格
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public XCITreeGrid TargetGrid { get; set; }

        /// <summary>
        /// 表达式表格
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected XCIGrid Grid { get { return gridExpression; } }

        public TreeExpressionConditionsEditor()
        {
            InitializeComponent();
            propertyGrid1.PropertyGrid.OptionsView.ShowRootCategories = false;
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="grid">目标表格</param>
        public void Initialize(XCITreeGrid grid)
        {
            this.TargetGrid = grid;
            Grid.MainBar = TargetGrid.MainBar;
            LoadGridColumn();
            InitData();
        }

        protected void InitData()
        {
            Grid.DataSource = TargetGrid.FormatConditions;
        }

        protected void LoadGridColumn()
        {
            foreach(TreeListColumn col in TargetGrid.Columns)
            {
                comBoxColumn.Items.Add(new ImageComboBoxItem(col.Caption, col, -1));
            }
        }
        
        /// <summary>
        /// 显示表达式编辑器
        /// </summary>
        /// <param name="condition">条件表达式</param>
        protected void ShowEditor(object condition)
        {
            using (ExpressionEditorForm form = new ConditionExpressionEditorForm(condition, null))
            {
                form.StartPosition = FormStartPosition.CenterParent;
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    ObjectHelper.SetObjectProperty(condition, "Expression", form.Expression.Replace(" ", ""));
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            StyleFormatCondition condition = new StyleFormatCondition();
            condition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            TargetGrid.FormatConditions.Add(condition);
            ShowEditor(condition);
            if (string.IsNullOrEmpty(condition.Expression))
            {
                TargetGrid.FormatConditions.Remove(condition);
            }
            Grid.RefreshData();
            Grid.View.MoveLastVisible();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var condition = Grid.GetSelected();
            if (condition != null)
            {
                ShowEditor(condition);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            XtraMessageBoxHelper.ShowYesNoAndWarning("确定要删除选中的数据吗?", d =>
            {
                var entity = Grid.GetSelected();
                if (entity != null)
                {
                    Grid.Delete(entity);
                }
            });
        }


        private void gridViewExpression_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var entity = Grid.GetSelected();
            if (entity != null)
            {
                propertyGrid1.SetObject(ObjectHelper.GetObjectProperty(entity, "Appearance"));
                propertyGrid1.Enabled = true;
                btnEdit.Enabled = btnDelete.Enabled = true;
            }
            else
            {
                propertyGrid1.PropertyGrid.SelectedObject = null;
                propertyGrid1.Enabled = false;
                btnEdit.Enabled = btnDelete.Enabled = false;
            }
        }

        private void gridViewExpression_DoubleClick(object sender, EventArgs e)
        {
            btnEdit.PerformClick();
        }
        
    }
}
