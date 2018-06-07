
namespace XCI.WinUtility.GridConfig
{
    partial class frmTreeGridConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTreeGridConfig));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnDefault = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pageAdvanced = new DevExpress.XtraTab.XtraTabPage();
            this.pageTemplate = new DevExpress.XtraTab.XtraTabPage();
            this.pageGridStyle = new DevExpress.XtraTab.XtraTabPage();
            this.treeExpressionConditionsEditor1 = new TreeExpressionConditionsEditor();
            this.pageAppearance = new DevExpress.XtraTab.XtraTabPage();
            this.pageGridColumn = new DevExpress.XtraTab.XtraTabPage();
            this.treeColumnEditor1 = new TreeColumnEditor();
            this.pageGeneral = new DevExpress.XtraTab.XtraTabPage();
            this.treeGeneralEditor1 = new TreeGeneralEditor();
            this.tabControlProperty = new DevExpress.XtraTab.XtraTabControl();
            this.gridAppearanceEditor1 = new GridAppearanceEditor();
            this.gridTemplateEditor1 = new GridTemplateEditor();
            this.gridAdvancedEditor1 = new GridAdvancedEditor();
            this.pageAdvanced.SuspendLayout();
            this.pageTemplate.SuspendLayout();
            this.pageGridStyle.SuspendLayout();
            this.pageAppearance.SuspendLayout();
            this.pageGridColumn.SuspendLayout();
            this.pageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlProperty)).BeginInit();
            this.tabControlProperty.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(821, 557);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "确定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefault.Location = new System.Drawing.Point(715, 557);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(100, 30);
            this.btnDefault.TabIndex = 2;
            this.btnDefault.Text = "恢复默认";
            this.btnDefault.Click += new System.EventHandler(this.btnGetAsDefault_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(927, 557);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pageAdvanced
            // 
            this.pageAdvanced.Controls.Add(this.gridAdvancedEditor1);
            this.pageAdvanced.Name = "pageAdvanced";
            this.pageAdvanced.Size = new System.Drawing.Size(1026, 515);
            this.pageAdvanced.Text = "高级";
            // 
            // pageTemplate
            // 
            this.pageTemplate.Controls.Add(this.gridTemplateEditor1);
            this.pageTemplate.Name = "pageTemplate";
            this.pageTemplate.Size = new System.Drawing.Size(1026, 515);
            this.pageTemplate.Text = "模板";
            // 
            // pageGridStyle
            // 
            this.pageGridStyle.Controls.Add(this.treeExpressionConditionsEditor1);
            this.pageGridStyle.Name = "pageGridStyle";
            this.pageGridStyle.Size = new System.Drawing.Size(1026, 515);
            this.pageGridStyle.Text = "条件样式";
            // 
            // treeExpressionConditionsEditor1
            // 
            this.treeExpressionConditionsEditor1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeExpressionConditionsEditor1.Appearance.Options.UseFont = true;
            this.treeExpressionConditionsEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeExpressionConditionsEditor1.Location = new System.Drawing.Point(0, 0);
            this.treeExpressionConditionsEditor1.Name = "treeExpressionConditionsEditor1";
            this.treeExpressionConditionsEditor1.Size = new System.Drawing.Size(1026, 515);
            this.treeExpressionConditionsEditor1.TabIndex = 0;
            // 
            // pageAppearance
            // 
            this.pageAppearance.Controls.Add(this.gridAppearanceEditor1);
            this.pageAppearance.Name = "pageAppearance";
            this.pageAppearance.Size = new System.Drawing.Size(1026, 515);
            this.pageAppearance.Text = "外观";
            // 
            // pageGridColumn
            // 
            this.pageGridColumn.Controls.Add(this.treeColumnEditor1);
            this.pageGridColumn.Name = "pageGridColumn";
            this.pageGridColumn.Size = new System.Drawing.Size(1026, 515);
            this.pageGridColumn.Text = "表格列";
            // 
            // treeColumnEditor1
            // 
            this.treeColumnEditor1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeColumnEditor1.Appearance.Options.UseFont = true;
            this.treeColumnEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeColumnEditor1.Location = new System.Drawing.Point(0, 0);
            this.treeColumnEditor1.Name = "treeColumnEditor1";
            this.treeColumnEditor1.Size = new System.Drawing.Size(1026, 515);
            this.treeColumnEditor1.TabIndex = 0;
            // 
            // pageGeneral
            // 
            this.pageGeneral.Controls.Add(this.treeGeneralEditor1);
            this.pageGeneral.Name = "pageGeneral";
            this.pageGeneral.Size = new System.Drawing.Size(1026, 515);
            this.pageGeneral.Text = "常用";
            // 
            // treeGeneralEditor1
            // 
            this.treeGeneralEditor1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeGeneralEditor1.Appearance.Options.UseFont = true;
            this.treeGeneralEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeGeneralEditor1.Location = new System.Drawing.Point(0, 0);
            this.treeGeneralEditor1.Name = "treeGeneralEditor1";
            this.treeGeneralEditor1.Size = new System.Drawing.Size(1026, 515);
            this.treeGeneralEditor1.TabIndex = 0;
            // 
            // tabControlProperty
            // 
            this.tabControlProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperty.Location = new System.Drawing.Point(0, 2);
            this.tabControlProperty.Name = "tabControlProperty";
            this.tabControlProperty.SelectedTabPage = this.pageGeneral;
            this.tabControlProperty.Size = new System.Drawing.Size(1032, 544);
            this.tabControlProperty.TabIndex = 3;
            this.tabControlProperty.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageGeneral,
            this.pageGridColumn,
            this.pageAppearance,
            this.pageGridStyle,
            this.pageTemplate,
            this.pageAdvanced});
            // 
            // gridAppearanceEditor1
            // 
            this.gridAppearanceEditor1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridAppearanceEditor1.Appearance.Options.UseFont = true;
            this.gridAppearanceEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAppearanceEditor1.Location = new System.Drawing.Point(0, 0);
            this.gridAppearanceEditor1.Name = "gridAppearanceEditor1";
            this.gridAppearanceEditor1.Size = new System.Drawing.Size(1026, 515);
            this.gridAppearanceEditor1.TabIndex = 0;
            // 
            // gridTemplateEditor1
            // 
            this.gridTemplateEditor1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridTemplateEditor1.Appearance.Options.UseFont = true;
            this.gridTemplateEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTemplateEditor1.Location = new System.Drawing.Point(0, 0);
            this.gridTemplateEditor1.Name = "gridTemplateEditor1";
            this.gridTemplateEditor1.Size = new System.Drawing.Size(1026, 515);
            this.gridTemplateEditor1.TabIndex = 0;
            // 
            // gridAdvancedEditor1
            // 
            this.gridAdvancedEditor1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridAdvancedEditor1.Appearance.Options.UseFont = true;
            this.gridAdvancedEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAdvancedEditor1.Location = new System.Drawing.Point(0, 0);
            this.gridAdvancedEditor1.Name = "gridAdvancedEditor1";
            this.gridAdvancedEditor1.Size = new System.Drawing.Size(1026, 515);
            this.gridAdvancedEditor1.TabIndex = 0;
            // 
            // frmTreeGridConfig
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 599);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.tabControlProperty);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTreeGridConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义表格属性";
            this.Load += new System.EventHandler(this.frmGridConfig_Load);
            this.pageAdvanced.ResumeLayout(false);
            this.pageTemplate.ResumeLayout(false);
            this.pageGridStyle.ResumeLayout(false);
            this.pageAppearance.ResumeLayout(false);
            this.pageGridColumn.ResumeLayout(false);
            this.pageGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlProperty)).EndInit();
            this.tabControlProperty.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnDefault;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraTab.XtraTabPage pageAdvanced;
        private DevExpress.XtraTab.XtraTabPage pageTemplate;
        private DevExpress.XtraTab.XtraTabPage pageGridStyle;
        private DevExpress.XtraTab.XtraTabPage pageAppearance;
        private DevExpress.XtraTab.XtraTabPage pageGridColumn;
        private DevExpress.XtraTab.XtraTabPage pageGeneral;
        private DevExpress.XtraTab.XtraTabControl tabControlProperty;
        private TreeGeneralEditor treeGeneralEditor1;
        private TreeExpressionConditionsEditor treeExpressionConditionsEditor1;
        private TreeColumnEditor treeColumnEditor1;
        private GridAdvancedEditor gridAdvancedEditor1;
        private GridTemplateEditor gridTemplateEditor1;
        private GridAppearanceEditor gridAppearanceEditor1;
    }
}