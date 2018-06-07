namespace XCI.WinUtility.ComponentManager.UI
{
    partial class frmComponentManager
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.objectListView2 = new BrightIdeasSoftware.ObjectListView();
            this.colClassTitle = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colClassProvider = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.colTitle = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colProvider = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.lineControl2 = new XCI.WinUtility.LineControl();
            this.txtSearch2 = new XCI.WinUtility.WatermarkTextBox();
            this.txtSearch1 = new XCI.WinUtility.WatermarkTextBox();
            this.lineControl1 = new XCI.WinUtility.LineControl();
            this.btnAutoCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView2)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 31);
            this.label1.TabIndex = 8;
            this.label1.Text = "接口管理";
            // 
            // objectListView2
            // 
            this.objectListView2.AllColumns.Add(this.colClassTitle);
            this.objectListView2.AllColumns.Add(this.colClassProvider);
            this.objectListView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colClassTitle,
            this.colClassProvider});
            this.objectListView2.ContextMenuStrip = this.contextMenuStrip2;
            this.objectListView2.Location = new System.Drawing.Point(624, 100);
            this.objectListView2.Margin = new System.Windows.Forms.Padding(5);
            this.objectListView2.Name = "objectListView2";
            this.objectListView2.Size = new System.Drawing.Size(626, 616);
            this.objectListView2.TabIndex = 5;
            this.objectListView2.UseCompatibleStateImageBehavior = false;
            this.objectListView2.View = System.Windows.Forms.View.Details;
            this.objectListView2.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.objectListView2_FormatCell);
            this.objectListView2.DoubleClick += new System.EventHandler(this.objectListView2_DoubleClick);
            // 
            // colClassTitle
            // 
            this.colClassTitle.AspectName = "Title";
            this.colClassTitle.Text = "名称";
            this.colClassTitle.Width = 400;
            // 
            // colClassProvider
            // 
            this.colClassProvider.AspectName = "Provider";
            this.colClassProvider.Text = "实现类";
            this.colClassProvider.Width = 400;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAbout});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(101, 26);
            // 
            // btnAbout
            // 
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(100, 22);
            this.btnAbout.Text = "关于";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.colTitle);
            this.objectListView1.AllColumns.Add(this.colProvider);
            this.objectListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitle,
            this.colProvider});
            this.objectListView1.ContextMenuStrip = this.contextMenuStrip1;
            this.objectListView1.Location = new System.Drawing.Point(14, 100);
            this.objectListView1.Margin = new System.Windows.Forms.Padding(5);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.Size = new System.Drawing.Size(600, 616);
            this.objectListView1.TabIndex = 4;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            this.objectListView1.SelectedIndexChanged += new System.EventHandler(this.objectListView1_SelectedIndexChanged);
            this.objectListView1.DoubleClick += new System.EventHandler(this.objectListView1_DoubleClick);
            // 
            // colTitle
            // 
            this.colTitle.AspectName = "Title";
            this.colTitle.Text = "名称";
            this.colTitle.Width = 250;
            // 
            // colProvider
            // 
            this.colProvider.AspectName = "Provider";
            this.colProvider.Text = "接口";
            this.colProvider.Width = 400;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSetting,
            this.toolStripSeparator1,
            this.btnAutoCreate});
            this.contextMenuStrip1.Name = "contextMenuStrip2";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 76);
            // 
            // btnSetting
            // 
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(172, 22);
            this.btnSetting.Text = "配置";
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(618, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 31);
            this.label2.TabIndex = 12;
            this.label2.Text = "实现类管理";
            // 
            // lineControl2
            // 
            this.lineControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineControl2.Location = new System.Drawing.Point(624, 43);
            this.lineControl2.Name = "lineControl2";
            this.lineControl2.Size = new System.Drawing.Size(626, 10);
            this.lineControl2.TabIndex = 11;
            this.lineControl2.Text = "lineControl2";
            // 
            // txtSearch2
            // 
            this.txtSearch2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch2.Location = new System.Drawing.Point(624, 59);
            this.txtSearch2.Name = "txtSearch2";
            this.txtSearch2.Size = new System.Drawing.Size(626, 29);
            this.txtSearch2.TabIndex = 10;
            this.txtSearch2.Watermark = "请输入搜索关键字";
            this.txtSearch2.TextChanged += new System.EventHandler(this.txtSearch2_TextChanged);
            // 
            // txtSearch1
            // 
            this.txtSearch1.Location = new System.Drawing.Point(14, 59);
            this.txtSearch1.Name = "txtSearch1";
            this.txtSearch1.Size = new System.Drawing.Size(600, 29);
            this.txtSearch1.TabIndex = 9;
            this.txtSearch1.Watermark = "请输入搜索关键字";
            this.txtSearch1.TextChanged += new System.EventHandler(this.txtSearch1_TextChanged);
            // 
            // lineControl1
            // 
            this.lineControl1.Location = new System.Drawing.Point(14, 43);
            this.lineControl1.Name = "lineControl1";
            this.lineControl1.Size = new System.Drawing.Size(600, 10);
            this.lineControl1.TabIndex = 7;
            this.lineControl1.Text = "lineControl1";
            // 
            // btnAutoCreate
            // 
            this.btnAutoCreate.Name = "btnAutoCreate";
            this.btnAutoCreate.Size = new System.Drawing.Size(172, 22);
            this.btnAutoCreate.Text = "自动生成全部组件";
            this.btnAutoCreate.Click += new System.EventHandler(this.btnAutoCreate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // frmComponentManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 730);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lineControl2);
            this.Controls.Add(this.txtSearch2);
            this.Controls.Add(this.txtSearch1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lineControl1);
            this.Controls.Add(this.objectListView2);
            this.Controls.Add(this.objectListView1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmComponentManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "组件管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.objectListView2)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView objectListView1;
        private BrightIdeasSoftware.ObjectListView objectListView2;
        private XCI.WinUtility.LineControl lineControl1;
        private System.Windows.Forms.Label label1;
        private XCI.WinUtility.WatermarkTextBox txtSearch1;
        private XCI.WinUtility.WatermarkTextBox txtSearch2;
        private System.Windows.Forms.Label label2;
        private XCI.WinUtility.LineControl lineControl2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnSetting;
        private BrightIdeasSoftware.OLVColumn colTitle;
        private BrightIdeasSoftware.OLVColumn colProvider;
        private BrightIdeasSoftware.OLVColumn colClassTitle;
        private BrightIdeasSoftware.OLVColumn colClassProvider;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnAutoCreate;
    }
}