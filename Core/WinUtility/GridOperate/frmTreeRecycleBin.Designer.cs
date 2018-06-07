namespace XCI.WinUtility
{
    partial class frmTreeRecycleBin
    {
        #region 清理
        /// <summary>
        /// 必需的设计器变量.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源.
        /// </summary>
        /// <param name="disposing">如果应释放托管资源 true,否则为false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region 窗口设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 这个使用代码编辑器方法的内容.
        /// </summary>
        private void InitializeComponent()
        {   
            this.panelCenter = new System.Windows.Forms.Panel();
            this.gridControl = new XCI.WinUtility.XCITreeGrid();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.editSearchBox = new XCI.WinUtility.XCIButtonEdit();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnSelectInverse = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnRestore = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSearchBox.Properties)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.gridControl);
            this.panelCenter.Controls.Add(this.editSearchBox);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 0);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(884, 417);
            this.panelCenter.TabIndex = 0;
            // 
            // gridControl
            // 
            this.gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl.GridID = "fa510f405d054324b1d82adc1e45ea9f";
            this.gridControl.IsShowColumnHeardMenu = true;
            this.gridControl.Location = new System.Drawing.Point(12, 43);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(860, 373);
            this.gridControl.TabIndex = 1;
            // 
            // editSearchBox
            // 
            this.editSearchBox.EnableChangerBackColor = false;
            this.editSearchBox.EnableZeroConvertEmpty = false;
            this.editSearchBox.Location = new System.Drawing.Point(12, 9);
            this.editSearchBox.Name = "editSearchBox";
            this.editSearchBox.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editSearchBox.Properties.Appearance.Options.UseFont = true;
            this.editSearchBox.Size = new System.Drawing.Size(440, 28);
            this.editSearchBox.TabIndex = 0;
            this.editSearchBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.editSearchBox.WaterMarkFont = null;
            this.editSearchBox.WaterMarkText = "请输入搜索关键字";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnSelectInverse);
            this.panelBottom.Controls.Add(this.btnSelectAll);
            this.panelBottom.Controls.Add(this.btnDelete);
            this.panelBottom.Controls.Add(this.btnRestore);
            this.panelBottom.Controls.Add(this.btnExport);
            this.panelBottom.Controls.Add(this.btnClear);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 417);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(884, 45);
            this.panelBottom.TabIndex = 1;
            // 
            // btnSelectInverse
            // 
            this.btnSelectInverse.Location = new System.Drawing.Point(43, 12);
            this.btnSelectInverse.Name = "btnSelectInverse";
            this.btnSelectInverse.Size = new System.Drawing.Size(25, 25);
            this.btnSelectInverse.TabIndex = 5;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(12, 12);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(25, 25);
            this.btnSelectAll.TabIndex = 4;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(666, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除(&D)...";
            this.btnDelete.ToolTip = "删除记录";
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.Location = new System.Drawing.Point(454, 6);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(100, 35);
            this.btnRestore.TabIndex = 0;
            this.btnRestore.Text = "还原";
            this.btnRestore.ToolTip = "新增记录";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(772, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 35);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "导出(&I)...";
            this.btnExport.ToolTip = "导出记录";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(560, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空";
            this.btnClear.ToolTip = "编辑记录";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmGridRecycleBin
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 462);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelBottom);
            this.KeyPreview = true;
            this.Name = "frmGridRecycleBin";
            this.Text = "回收站";
            this.Load += new System.EventHandler(this.frmGridRecycleBin_Load);
            this.panelCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSearchBox.Properties)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        #region 声明
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Panel panelBottom;
        private XCI.WinUtility.XCITreeGrid gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private XCI.WinUtility.XCIButtonEdit editSearchBox;
        private DevExpress.XtraEditors.SimpleButton btnRestore;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.SimpleButton btnSelectAll;
        private DevExpress.XtraEditors.SimpleButton btnSelectInverse;
        #endregion
        
    }
}
