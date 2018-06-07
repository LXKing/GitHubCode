
namespace XCI.WinUtility.GridConfig
{
    partial class GridExpressionConditionsEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.editSearchBox = new XCI.WinUtility.XCIButtonEdit();
            this.gridExpression = new XCI.WinUtility.XCIGrid();
            this.gridViewExpression = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.comBoxColumn = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.propertyGrid1 = new XCI.WinUtility.XCIPropertyGrid();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.editSearchBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridExpression)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewExpression)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comBoxColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(722, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "É¾³ý...";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            this.editSearchBox.TabIndex = 18;
            this.editSearchBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.editSearchBox.WaterMarkFont = null;
            this.editSearchBox.WaterMarkText = "ÇëÊäÈëËÑË÷¹Ø¼ü×Ö";
            // 
            // gridExpression
            // 
            this.gridExpression.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridExpression.GridID = "2519a90512894a099bdc1f9074efe8e5";
            this.gridExpression.IsShowColumnHeardMenu = true;
            this.gridExpression.Location = new System.Drawing.Point(0, 0);
            this.gridExpression.MainView = this.gridViewExpression;
            this.gridExpression.Name = "gridExpression";
            this.gridExpression.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.comBoxColumn});
            this.gridExpression.Size = new System.Drawing.Size(510, 460);
            this.gridExpression.TabIndex = 13;
            this.gridExpression.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewExpression});
            // 
            // gridViewExpression
            // 
            this.gridViewExpression.GridControl = this.gridExpression;
            this.gridViewExpression.Name = "gridViewExpression";
            this.gridViewExpression.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewExpression_FocusedRowChanged);
            this.gridViewExpression.DoubleClick += new System.EventHandler(this.gridViewExpression_DoubleClick);
            // 
            // comBoxColumn
            // 
            this.comBoxColumn.AutoHeight = false;
            this.comBoxColumn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comBoxColumn.Name = "comBoxColumn";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(616, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 30);
            this.btnEdit.TabIndex = 14;
            this.btnEdit.Text = "±à¼­...";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Appearance.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertyGrid1.Appearance.Options.UseFont = true;
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(4);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.ShowCategories = false;
            this.propertyGrid1.Size = new System.Drawing.Size(385, 460);
            this.propertyGrid1.TabIndex = 15;
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(510, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 30);
            this.btnNew.TabIndex = 16;
            this.btnNew.Text = "ÐÂÔö...";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 40);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridExpression);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(900, 460);
            this.splitContainerControl1.SplitterPosition = 385;
            this.splitContainerControl1.TabIndex = 19;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // GridExpressionConditionsEditor
            // 
            this.Appearance.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.editSearchBox);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNew);
            this.Name = "GridExpressionConditionsEditor";
            this.Size = new System.Drawing.Size(900, 500);
            ((System.ComponentModel.ISupportInitialize)(this.editSearchBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridExpression)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewExpression)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comBoxColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private WinUtility.XCIButtonEdit editSearchBox;
        private WinUtility.XCIGrid gridExpression;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewExpression;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private WinUtility.XCIPropertyGrid propertyGrid1;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox comBoxColumn;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;

    }
}
