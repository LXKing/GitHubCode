using System;
using System.ComponentModel;
using System.Windows.Forms;
using XCI.Component;
using XCI.Core;

namespace XCI.WinUtility.GridConfig
{
    public partial class GridAdvancedEditor : UserControlBase
    {
        /// <summary>
        /// 目标表格
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public dynamic TargetGrid { get; set; }

        public GridAdvancedEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="grid">目标表格</param>
        /// <param name="targetObject">绑定的对象</param>
        public void Initialize(dynamic grid, object targetObject)
        {
            this.TargetGrid = grid;
            propertyGridControl.SetObject(targetObject);
        }

        /// <summary>
        /// 设为默认设置
        /// </summary>
        private void btnSetAsDefault_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBoxHelper.ShowYesNoAndTips("您确定要把当前设置存为默认设置吗?"))
            {
                TargetGrid.SaveConfig(Guid.Empty);
            }
        }


        private void btnResetAllControlAllUser_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBoxHelper.ShowYesNoAndTips("您确定要清除全部控件全部用户配置?"))
            {
                GridConfigFactory.Current.DeleteAllControlAllUserConfig();
            }
        }

        private void btnResetAllControlDefault_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBoxHelper.ShowYesNoAndTips("您确定要清除全部控件配置?"))
            {
                GridConfigFactory.Current.DeleteAllControlConfig();
            }
        }

        private void btnResetCurrentControlAllUser_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBoxHelper.ShowYesNoAndTips("您确定要清除本控件全部用户配置?"))
            {
                GridConfigFactory.Current.DeleteCurrentControlAllUserConfig(TargetGrid.GridID);
            }
        }

        private void btnResetCurrentControlDefault_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBoxHelper.ShowYesNoAndTips("您确定要清除本控件配置?"))
            {
                GridConfigFactory.Current.DeleteCurrentControlConfig(TargetGrid.GridID);
            }
        }

    }
}
