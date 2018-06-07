
namespace XCI.WinUtility.GridConfig
{
    partial class GridAppearanceEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.xciPropertyGrid1 = new XCI.WinUtility.XCIPropertyGrid();
            this.xciGridControl1 = new XCI.WinUtility.XCIGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.editSearchBox = new XCI.WinUtility.XCIButtonEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.xciGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSearchBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xciPropertyGrid1
            // 
            this.xciPropertyGrid1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xciPropertyGrid1.Appearance.Options.UseFont = true;
            this.xciPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xciPropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.xciPropertyGrid1.Margin = new System.Windows.Forms.Padding(4);
            this.xciPropertyGrid1.Name = "xciPropertyGrid1";
            this.xciPropertyGrid1.ShowCategories = false;
            this.xciPropertyGrid1.Size = new System.Drawing.Size(390, 471);
            this.xciPropertyGrid1.TabIndex = 1;
            // 
            // xciGridControl1
            // 
            this.xciGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xciGridControl1.GridID = "d345a68ed09f4447b19171a1a60fcff7";
            this.xciGridControl1.IsShowColumnHeardMenu = true;
            this.xciGridControl1.Location = new System.Drawing.Point(0, 0);
            this.xciGridControl1.MainView = this.gridView1;
            this.xciGridControl1.Name = "xciGridControl1";
            this.xciGridControl1.Size = new System.Drawing.Size(505, 471);
            this.xciGridControl1.TabIndex = 2;
            this.xciGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.xciGridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // editSearchBox
            // 
            this.editSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editSearchBox.EnableChangerBackColor = false;
            this.editSearchBox.EnableZeroConvertEmpty = false;
            this.editSearchBox.Location = new System.Drawing.Point(0, 3);
            this.editSearchBox.Name = "editSearchBox";
            this.editSearchBox.Size = new System.Drawing.Size(503, 20);
            this.editSearchBox.TabIndex = 7;
            this.editSearchBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.editSearchBox.WaterMarkFont = null;
            this.editSearchBox.WaterMarkText = "请输入搜索关键字";
            this.editSearchBox.EditValueChanged += new System.EventHandler(this.editSearchBox_EditValueChanged);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 29);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.xciGridControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xciPropertyGrid1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(900, 471);
            this.splitContainerControl1.SplitterPosition = 390;
            this.splitContainerControl1.TabIndex = 8;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // GridAppearanceEditor
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.editSearchBox);
            this.Name = "GridAppearanceEditor";
            this.Size = new System.Drawing.Size(900, 500);
            ((System.ComponentModel.ISupportInitialize)(this.xciGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSearchBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private XCIPropertyGrid xciPropertyGrid1;
        private XCIGrid xciGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private XCIButtonEdit editSearchBox;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    }
}
