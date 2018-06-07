namespace XCI.WinUtility
{
    partial class XCIPropertyGrid
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
            this.components = new System.ComponentModel.Container();
            this.pnlHint = new DevExpress.Utils.Frames.NotePanelEx();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bMain = new DevExpress.XtraBars.Bar();
            this.bciCategories = new DevExpress.XtraBars.BarCheckItem();
            this.bciAlphabetical = new DevExpress.XtraBars.BarCheckItem();
            this.biDescription = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.lbCaption = new System.Windows.Forms.Label();
            this.pncDescription = new DevExpress.XtraEditors.PanelControl();
            this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pncDescription)).BeginInit();
            this.pncDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHint
            // 
            this.pnlHint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHint.ForeColor = System.Drawing.Color.Black;
            this.pnlHint.Location = new System.Drawing.Point(3, 58);
            this.pnlHint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlHint.MaxRows = 10;
            this.pnlHint.Name = "pnlHint";
            this.pnlHint.Size = new System.Drawing.Size(310, 23);
            this.pnlHint.TabIndex = 0;
            this.pnlHint.TabStop = false;
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bMain});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bciCategories,
            this.bciAlphabetical,
            this.biDescription});
            this.barManager1.MaxItemId = 3;
            this.barManager1.UseAltKeyForMenu = false;
            this.barManager1.UseF10KeyForMenu = false;
            // 
            // bMain
            // 
            this.bMain.BarName = "Main";
            this.bMain.DockCol = 0;
            this.bMain.DockRow = 0;
            this.bMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bMain.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bciCategories),
            new DevExpress.XtraBars.LinkPersistInfo(this.bciAlphabetical),
            new DevExpress.XtraBars.LinkPersistInfo(this.biDescription, true)});
            this.bMain.OptionsBar.AllowDelete = true;
            this.bMain.OptionsBar.AllowQuickCustomization = false;
            this.bMain.OptionsBar.DrawDragBorder = false;
            this.bMain.OptionsBar.UseWholeRow = true;
            this.bMain.Text = "Main";
            // 
            // bciCategories
            // 
            this.bciCategories.GroupIndex = 1;
            this.bciCategories.Hint = "Categorized";
            this.bciCategories.Id = 0;
            this.bciCategories.ImageIndex = 0;
            this.bciCategories.Name = "bciCategories";
            this.bciCategories.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bci_CheckedChanged);
            // 
            // bciAlphabetical
            // 
            this.bciAlphabetical.GroupIndex = 1;
            this.bciAlphabetical.Hint = "Alphabetic";
            this.bciAlphabetical.Id = 1;
            this.bciAlphabetical.ImageIndex = 1;
            this.bciAlphabetical.Name = "bciAlphabetical";
            this.bciAlphabetical.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bci_CheckedChanged);
            // 
            // biDescription
            // 
            this.biDescription.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.biDescription.Hint = "Show Description";
            this.biDescription.Id = 2;
            this.biDescription.ImageIndex = 2;
            this.biDescription.Name = "biDescription";
            this.biDescription.DownChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.biDescription_DownChanged);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlTop.Size = new System.Drawing.Size(316, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 500);
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlBottom.Size = new System.Drawing.Size(316, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 469);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(316, 31);
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 469);
            // 
            // lbCaption
            // 
            this.lbCaption.BackColor = System.Drawing.SystemColors.Info;
            this.lbCaption.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbCaption.Location = new System.Drawing.Point(3, 22);
            this.lbCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCaption.Name = "lbCaption";
            this.lbCaption.Size = new System.Drawing.Size(310, 36);
            this.lbCaption.TabIndex = 5;
            this.lbCaption.Text = "(PropertyName)";
            this.lbCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pncDescription
            // 
            this.pncDescription.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.pncDescription.Appearance.Options.UseBackColor = true;
            this.pncDescription.Controls.Add(this.lbCaption);
            this.pncDescription.Controls.Add(this.pnlHint);
            this.pncDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pncDescription.Location = new System.Drawing.Point(0, 416);
            this.pncDescription.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.pncDescription.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pncDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pncDescription.Name = "pncDescription";
            this.pncDescription.Size = new System.Drawing.Size(316, 84);
            this.pncDescription.TabIndex = 6;
            // 
            // propertyGridControl1
            // 
            this.propertyGridControl1.DefaultEditors.AddRange(new DevExpress.XtraVerticalGrid.Rows.DefaultEditor[] {
            new DevExpress.XtraVerticalGrid.Rows.DefaultEditor(typeof(bool), this.repositoryItemCheckEdit1),
            new DevExpress.XtraVerticalGrid.Rows.DefaultEditor(typeof(System.Drawing.Color), this.repositoryItemColorEdit1)});
            this.propertyGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControl1.Location = new System.Drawing.Point(0, 34);
            this.propertyGridControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemColorEdit1});
            this.propertyGridControl1.Size = new System.Drawing.Size(316, 379);
            this.propertyGridControl1.TabIndex = 7;
            this.propertyGridControl1.CustomPropertyDescriptors += new DevExpress.XtraVerticalGrid.Events.CustomPropertyDescriptorsEventHandler(this.propertyGridControl1_CustomPropertyDescriptors);
            this.propertyGridControl1.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(this.propertyGridControl1_FocusedRowChanged);
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            this.repositoryItemColorEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            // 
            // pnlTop
            // 
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 31);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(316, 3);
            this.pnlTop.TabIndex = 8;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 413);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(316, 3);
            this.pnlBottom.TabIndex = 9;
            // 
            // XCIPropertyGrid
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertyGridControl1);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pncDescription);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "XCIPropertyGrid";
            this.Size = new System.Drawing.Size(316, 500);
            this.Resize += new System.EventHandler(this.XtraPropertyGrid_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pncDescription)).EndInit();
            this.pncDescription.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Frames.NotePanelEx pnlHint;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Bar bMain;
        private System.Windows.Forms.Label lbCaption;
        private DevExpress.XtraEditors.PanelControl pncDescription;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBottom;
        private DevExpress.XtraBars.BarCheckItem bciCategories;
        private DevExpress.XtraBars.BarCheckItem bciAlphabetical;
        private DevExpress.XtraBars.BarButtonItem biDescription;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;

    }
}
