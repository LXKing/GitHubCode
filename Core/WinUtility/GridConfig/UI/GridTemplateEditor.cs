using System;
using System.ComponentModel;
using XCI.Component;

namespace XCI.WinUtility.GridConfig
{
    public partial class GridTemplateEditor : UserControlBase
    {
        /// <summary>
        /// 目标表格
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public dynamic TargetGrid { get; set; }

        /// <summary>
        /// 模板表格
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public XCIGrid Grid { get { return gridTemplate; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GridTemplateEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="grid">目标表格</param>
        public void Initialize(dynamic grid)
        {
            this.TargetGrid = grid;
            InitData();
        }

        /// <summary>
        /// 加载模板数据
        /// </summary>
        protected void InitData()
        {
            var data = GridConfigTemplateFactory.Current.GetTemplateList(TargetGrid.GridID);
            Grid.MainBar = TargetGrid.MainBar;
            Grid.DataSource = data;
        }

        private void btnSaveAsTemplate_Click(object sender, EventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("请输入模板名称 ", "操作提示 ");
            if (!string.IsNullOrEmpty(name))
            {
                GridConfigTemplateFactory.Current.SaveConfig(TargetGrid, name);
                InitData();
                this.Grid.View.MoveLastVisible();
            }
        }

        private void btnTemplateApple_Click(object sender, EventArgs e)
        {
            var entity = Grid.GetSelected<GridConfigTemplateEntity>();
            if (entity != null)
            {
                XtraMessageBoxHelper.ShowYesNoAndTips("确定要应用模板吗?", p => GridConfigTemplateFactory.Current.LoadConfig(TargetGrid, entity.Name));
            }
        }

        private void btnTemplateCopy_Click(object sender, EventArgs e)
        {
            var entity = Grid.GetSelected<GridConfigTemplateEntity>();
            if (entity != null)
            {
                string name = Microsoft.VisualBasic.Interaction.InputBox("请输入模板新名称 ", "操作提示 ");
                if (!string.IsNullOrEmpty(name))
                {
                    GridConfigTemplateFactory.Current.CopyConfig(entity.ID, name);
                    InitData();
                    this.Grid.View.MoveLastVisible();
                }
            }
        }

        private void btnTemplateDelete_Click(object sender, EventArgs e)
        {
            var entity = Grid.GetSelected<GridConfigTemplateEntity>();
            if (entity != null)
            {
                XtraMessageBoxHelper.ShowYesNoAndTips("确定要删除模板吗?", p =>
                {
                    GridConfigTemplateFactory.Current.DeleteConfig(entity.ID);
                    InitData();
                    this.Grid.View.MoveLastVisible();
                });
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var entity = Grid.GetSelected<GridConfigTemplateEntity>();
            if (entity != null)
            {
                btnTemplateApple.Enabled = true;
                btnTemplateCopy.Enabled = true;
                btnTemplateDelete.Enabled = true;
            }
            else
            {
                btnTemplateApple.Enabled = false;
                btnTemplateCopy.Enabled = true;
                btnTemplateDelete.Enabled = true;
            }
        }

        private void editSearchBox_EditValueChanged(object sender, EventArgs e)
        {
            Grid.View.ApplyFindFilter(((XCIButtonEdit)sender).Text.Trim());
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("Name"))
            {
                var entity = Grid.GetSelected<GridConfigTemplateEntity>();
                if (entity != null)
                {
                    GridConfigTemplateFactory.Current.RenameConfig(entity.ID, e.Value.ToString());
                }
            }
        }

    }
}
