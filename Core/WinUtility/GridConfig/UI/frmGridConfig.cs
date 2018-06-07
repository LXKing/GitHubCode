using System;
using System.Windows.Forms;
using XCI.Component;
using XCI.Core;

namespace XCI.WinUtility.GridConfig
{
    public partial class frmGridConfig : FormBase
    {
        #region 属性

        /// <summary>
        /// 目标表格控件
        /// </summary>
        public XCIGrid TargetGrid { get; set; }
        
        private TabLoadHelper _tabLoad;
        /// <summary>
        /// 选项卡按需加载
        /// </summary>
        public TabLoadHelper TabLoad
        {
            get { return _tabLoad ?? (_tabLoad = new TabLoadHelper(tabControlProperty, PageLoadAction)); }
        }


        #endregion

        public frmGridConfig()
        {
            
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="grid">目标表格控件</param>
        public frmGridConfig(XCIGrid grid)
        {
            InitializeComponent();
            this.TargetGrid = grid;
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        private void frmGridConfig_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                TabLoad.LoadPage(tabControlProperty.SelectedTabPage);
            }
        }

        /// <summary>
        /// 选项卡加载处理函数
        /// </summary>
        /// <param name="name">选项卡名称</param>
        protected virtual void PageLoadAction(string name)
        {
            var grid = TargetGrid;
            switch (name)
            {
                case "pageGeneral":
                    gridGeneralEditor1.Initialize(grid, grid.View);
                    break;
                case "pageGridColumn":
                    gridColumnEditor1.Initialize(grid);
                    break;
                case "pageAppearance":
                    gridAppearanceEditor1.Initialize(grid.View.Appearance);
                    break;
                case "pageGridStyle":
                    gridExpressionConditionsEditor1.Initialize(grid);
                    break;
                case "pageAdvanced":
                    gridAdvancedEditor1.Initialize(grid, grid.View);
                    break;
                case "pageTemplate":
                    gridTemplateEditor1.Initialize(grid);
                    break;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// 设为默认
        /// </summary>
        private void btnGetAsDefault_Click(object sender, EventArgs e)
        {
            AsDefault();
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        protected virtual void Save()
        {
            TargetGrid.IsConfigChange = true;
            Cancel();
        }

        protected virtual void Cancel()
        {
            this.Close();
        }

        protected virtual void AsDefault()
        {
            if (DialogResult.Yes == XtraMessageBoxHelper.ShowYesNoAndTips("您确定要恢复默认设置吗?"))
            {
                GridConfigFactory.Current.DeleteConfig(TargetGrid.GridID, Guid.Empty);
                TargetGrid.LoadConfig();
                TargetGrid.IsConfigChange = false;
                Cancel();
            }
        }
    }
}
