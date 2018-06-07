
namespace XCI.WinUtility.GridConfig
{
    partial class GridTemplateEditor
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
            this.gridTemplate = new XCI.WinUtility.XCIGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnSaveAsTelplment = new DevExpress.XtraEditors.SimpleButton();
            this.btnTemplateApple = new DevExpress.XtraEditors.SimpleButton();
            this.btnTemplateCopy = new DevExpress.XtraEditors.SimpleButton();
            this.btnTemplateDelete = new DevExpress.XtraEditors.SimpleButton();
            this.editSearchBox = new XCI.WinUtility.XCIButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSearchBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTemplate
            // 
            this.gridTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTemplate.GridID = "58540c603651445d9335363741d7abb8";
            this.gridTemplate.IsShowColumnHeardMenu = true;
            this.gridTemplate.Location = new System.Drawing.Point(0, 40);
            this.gridTemplate.MainView = this.gridView1;
            this.gridTemplate.Name = "gridTemplate";
            this.gridTemplate.Size = new System.Drawing.Size(788, 464);
            this.gridTemplate.TabIndex = 96;
            this.gridTemplate.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridTemplate;
            this.gridView1.Name = "gridView1";
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // btnSaveAsTelplment
            // 
            this.btnSaveAsTelplment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAsTelplment.Location = new System.Drawing.Point(794, 40);
            this.btnSaveAsTelplment.Name = "btnSaveAsTelplment";
            this.btnSaveAsTelplment.Size = new System.Drawing.Size(100, 30);
            this.btnSaveAsTelplment.TabIndex = 97;
            this.btnSaveAsTelplment.Text = "存为模板";
            this.btnSaveAsTelplment.Click += new System.EventHandler(this.btnSaveAsTemplate_Click);
            // 
            // btnTemplateApple
            // 
            this.btnTemplateApple.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTemplateApple.Enabled = false;
            this.btnTemplateApple.Location = new System.Drawing.Point(794, 82);
            this.btnTemplateApple.Name = "btnTemplateApple";
            this.btnTemplateApple.Size = new System.Drawing.Size(100, 30);
            this.btnTemplateApple.TabIndex = 97;
            this.btnTemplateApple.Text = "应用模板";
            this.btnTemplateApple.Click += new System.EventHandler(this.btnTemplateApple_Click);
            // 
            // btnTemplateCopy
            // 
            this.btnTemplateCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTemplateCopy.Enabled = false;
            this.btnTemplateCopy.Location = new System.Drawing.Point(794, 124);
            this.btnTemplateCopy.Name = "btnTemplateCopy";
            this.btnTemplateCopy.Size = new System.Drawing.Size(100, 30);
            this.btnTemplateCopy.TabIndex = 97;
            this.btnTemplateCopy.Text = "复制模板";
            this.btnTemplateCopy.Click += new System.EventHandler(this.btnTemplateCopy_Click);
            // 
            // btnTemplateDelete
            // 
            this.btnTemplateDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTemplateDelete.Enabled = false;
            this.btnTemplateDelete.Location = new System.Drawing.Point(794, 166);
            this.btnTemplateDelete.Name = "btnTemplateDelete";
            this.btnTemplateDelete.Size = new System.Drawing.Size(100, 30);
            this.btnTemplateDelete.TabIndex = 97;
            this.btnTemplateDelete.Text = "删除模板";
            this.btnTemplateDelete.Click += new System.EventHandler(this.btnTemplateDelete_Click);
            // 
            // editSearchBox
            // 
            this.editSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editSearchBox.EnableChangerBackColor = false;
            this.editSearchBox.EnableZeroConvertEmpty = false;
            this.editSearchBox.Location = new System.Drawing.Point(0, 3);
            this.editSearchBox.Name = "editSearchBox";
            this.editSearchBox.Size = new System.Drawing.Size(788, 20);
            this.editSearchBox.TabIndex = 99;
            this.editSearchBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.editSearchBox.WaterMarkFont = null;
            this.editSearchBox.WaterMarkText = "请输入搜索关键字";
            this.editSearchBox.EditValueChanged += new System.EventHandler(this.editSearchBox_EditValueChanged);
            // 
            // GridTemplateEditor
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editSearchBox);
            this.Controls.Add(this.btnTemplateApple);
            this.Controls.Add(this.btnTemplateDelete);
            this.Controls.Add(this.btnTemplateCopy);
            this.Controls.Add(this.btnSaveAsTelplment);
            this.Controls.Add(this.gridTemplate);
            this.Name = "GridTemplateEditor";
            this.Size = new System.Drawing.Size(900, 500);
            ((System.ComponentModel.ISupportInitialize)(this.gridTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSearchBox.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private XCIGrid gridTemplate;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnSaveAsTelplment;
        private DevExpress.XtraEditors.SimpleButton btnTemplateApple;
        private DevExpress.XtraEditors.SimpleButton btnTemplateCopy;
        private DevExpress.XtraEditors.SimpleButton btnTemplateDelete;
        private XCIButtonEdit editSearchBox;
    }
}
