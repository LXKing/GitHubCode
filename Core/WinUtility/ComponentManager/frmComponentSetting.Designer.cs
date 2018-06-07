namespace XCI.WinUtility.ComponentManager.UI
{
    partial class frmComponentSetting
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
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.colName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colProvider = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colIsDefault = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colComment = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objectListView2 = new BrightIdeasSoftware.ObjectListView();
            this.colParamName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colParamValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colParamComment = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtInterfaceName = new System.Windows.Forms.TextBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnParamDelete = new System.Windows.Forms.Button();
            this.btnParamEdit = new System.Windows.Forms.Button();
            this.btnParamNew = new System.Windows.Forms.Button();
            this.txtSearch2 = new XCI.WinUtility.WatermarkTextBox();
            this.txtSearch1 = new XCI.WinUtility.WatermarkTextBox();
            this.lineControl1 = new XCI.WinUtility.LineControl();
            this.btnSetDefault = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView2)).BeginInit();
            this.SuspendLayout();
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.colName);
            this.objectListView1.AllColumns.Add(this.colProvider);
            this.objectListView1.AllColumns.Add(this.colIsDefault);
            this.objectListView1.AllColumns.Add(this.colComment);
            this.objectListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colProvider,
            this.colIsDefault,
            this.colComment});
            this.objectListView1.Location = new System.Drawing.Point(14, 97);
            this.objectListView1.Margin = new System.Windows.Forms.Padding(5);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.Size = new System.Drawing.Size(967, 190);
            this.objectListView1.TabIndex = 0;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            this.objectListView1.SelectedIndexChanged += new System.EventHandler(this.objectListView1_SelectedIndexChanged);
            this.objectListView1.DoubleClick += new System.EventHandler(this.objectListView1_DoubleClick);
            // 
            // colName
            // 
            this.colName.AspectName = "Name";
            this.colName.Text = "名称";
            this.colName.Width = 200;
            // 
            // colProvider
            // 
            this.colProvider.AspectName = "Provider";
            this.colProvider.Text = "实现类";
            this.colProvider.Width = 350;
            // 
            // colIsDefault
            // 
            this.colIsDefault.AspectName = "IsDefault";
            this.colIsDefault.Text = "默认";
            this.colIsDefault.Width = 100;
            // 
            // colComment
            // 
            this.colComment.AspectName = "Comment";
            this.colComment.Text = "描述";
            this.colComment.Width = 250;
            // 
            // objectListView2
            // 
            this.objectListView2.AllColumns.Add(this.colParamName);
            this.objectListView2.AllColumns.Add(this.colParamValue);
            this.objectListView2.AllColumns.Add(this.colParamComment);
            this.objectListView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colParamName,
            this.colParamValue,
            this.colParamComment});
            this.objectListView2.Location = new System.Drawing.Point(14, 332);
            this.objectListView2.Margin = new System.Windows.Forms.Padding(5);
            this.objectListView2.Name = "objectListView2";
            this.objectListView2.Size = new System.Drawing.Size(967, 191);
            this.objectListView2.TabIndex = 0;
            this.objectListView2.UseCompatibleStateImageBehavior = false;
            this.objectListView2.View = System.Windows.Forms.View.Details;
            this.objectListView2.SelectedIndexChanged += new System.EventHandler(this.objectListView2_SelectedIndexChanged);
            this.objectListView2.DoubleClick += new System.EventHandler(this.objectListView2_DoubleClick);
            // 
            // colParamName
            // 
            this.colParamName.AspectName = "Name";
            this.colParamName.Text = "参数名称";
            this.colParamName.Width = 200;
            // 
            // colParamValue
            // 
            this.colParamValue.AspectName = "Value";
            this.colParamValue.Text = "参数值";
            this.colParamValue.Width = 300;
            // 
            // colParamComment
            // 
            this.colParamComment.AspectName = "Comment";
            this.colParamComment.Text = "描述";
            this.colParamComment.Width = 300;
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.SystemColors.Control;
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTitle.Location = new System.Drawing.Point(14, 12);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(223, 22);
            this.txtTitle.TabIndex = 11;
            // 
            // txtInterfaceName
            // 
            this.txtInterfaceName.BackColor = System.Drawing.SystemColors.Control;
            this.txtInterfaceName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInterfaceName.Location = new System.Drawing.Point(257, 12);
            this.txtInterfaceName.Name = "txtInterfaceName";
            this.txtInterfaceName.Size = new System.Drawing.Size(514, 22);
            this.txtInterfaceName.TabIndex = 12;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(499, 56);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(87, 35);
            this.btnNew.TabIndex = 16;
            this.btnNew.Text = "新 增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(597, 56);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(87, 35);
            this.btnEdit.TabIndex = 17;
            this.btnEdit.Text = "编 辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(695, 56);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 35);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "删 除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnTest
            // 
            this.btnTest.Enabled = false;
            this.btnTest.Location = new System.Drawing.Point(891, 56);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(87, 35);
            this.btnTest.TabIndex = 19;
            this.btnTest.Text = "测 试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnParamDelete
            // 
            this.btnParamDelete.Enabled = false;
            this.btnParamDelete.Location = new System.Drawing.Point(695, 291);
            this.btnParamDelete.Name = "btnParamDelete";
            this.btnParamDelete.Size = new System.Drawing.Size(87, 35);
            this.btnParamDelete.TabIndex = 22;
            this.btnParamDelete.Text = "删 除";
            this.btnParamDelete.UseVisualStyleBackColor = true;
            this.btnParamDelete.Click += new System.EventHandler(this.btnParamDelete_Click);
            // 
            // btnParamEdit
            // 
            this.btnParamEdit.Enabled = false;
            this.btnParamEdit.Location = new System.Drawing.Point(597, 291);
            this.btnParamEdit.Name = "btnParamEdit";
            this.btnParamEdit.Size = new System.Drawing.Size(87, 35);
            this.btnParamEdit.TabIndex = 21;
            this.btnParamEdit.Text = "编 辑";
            this.btnParamEdit.UseVisualStyleBackColor = true;
            this.btnParamEdit.Click += new System.EventHandler(this.btnParamEdit_Click);
            // 
            // btnParamNew
            // 
            this.btnParamNew.Enabled = false;
            this.btnParamNew.Location = new System.Drawing.Point(499, 291);
            this.btnParamNew.Name = "btnParamNew";
            this.btnParamNew.Size = new System.Drawing.Size(87, 35);
            this.btnParamNew.TabIndex = 20;
            this.btnParamNew.Text = "新 增";
            this.btnParamNew.UseVisualStyleBackColor = true;
            this.btnParamNew.Click += new System.EventHandler(this.btnParamNew_Click);
            // 
            // txtSearch2
            // 
            this.txtSearch2.Location = new System.Drawing.Point(14, 295);
            this.txtSearch2.Name = "txtSearch2";
            this.txtSearch2.Size = new System.Drawing.Size(479, 29);
            this.txtSearch2.TabIndex = 15;
            this.txtSearch2.Watermark = "请输入搜索关键字";
            this.txtSearch2.TextChanged += new System.EventHandler(this.txtSearch2_TextChanged);
            // 
            // txtSearch1
            // 
            this.txtSearch1.Location = new System.Drawing.Point(14, 60);
            this.txtSearch1.Name = "txtSearch1";
            this.txtSearch1.Size = new System.Drawing.Size(479, 29);
            this.txtSearch1.TabIndex = 14;
            this.txtSearch1.Watermark = "请输入搜索关键字";
            this.txtSearch1.TextChanged += new System.EventHandler(this.txtSearch1_TextChanged);
            // 
            // lineControl1
            // 
            this.lineControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineControl1.Location = new System.Drawing.Point(14, 42);
            this.lineControl1.Margin = new System.Windows.Forms.Padding(5);
            this.lineControl1.Name = "lineControl1";
            this.lineControl1.Size = new System.Drawing.Size(967, 10);
            this.lineControl1.TabIndex = 10;
            this.lineControl1.Text = "lineControl1";
            // 
            // btnSetDefault
            // 
            this.btnSetDefault.Enabled = false;
            this.btnSetDefault.Location = new System.Drawing.Point(793, 56);
            this.btnSetDefault.Name = "btnSetDefault";
            this.btnSetDefault.Size = new System.Drawing.Size(87, 35);
            this.btnSetDefault.TabIndex = 23;
            this.btnSetDefault.Text = "设为默认";
            this.btnSetDefault.UseVisualStyleBackColor = true;
            this.btnSetDefault.Click += new System.EventHandler(this.btnSetDefault_Click);
            // 
            // frmComponentSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 537);
            this.Controls.Add(this.btnSetDefault);
            this.Controls.Add(this.btnParamDelete);
            this.Controls.Add(this.btnParamEdit);
            this.Controls.Add(this.btnParamNew);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.txtSearch2);
            this.Controls.Add(this.txtSearch1);
            this.Controls.Add(this.txtInterfaceName);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lineControl1);
            this.Controls.Add(this.objectListView2);
            this.Controls.Add(this.objectListView1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmComponentSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "组件配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmComponentSetting_FormClosing);
            this.Load += new System.EventHandler(this.frmComponentSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView objectListView1;
        private BrightIdeasSoftware.ObjectListView objectListView2;
        private BrightIdeasSoftware.OLVColumn colName;
        private BrightIdeasSoftware.OLVColumn colProvider;
        private BrightIdeasSoftware.OLVColumn colIsDefault;
        private BrightIdeasSoftware.OLVColumn colParamName;
        private BrightIdeasSoftware.OLVColumn colParamValue;
        private XCI.WinUtility.LineControl lineControl1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtInterfaceName;
        private XCI.WinUtility.WatermarkTextBox txtSearch1;
        private XCI.WinUtility.WatermarkTextBox txtSearch2;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnParamDelete;
        private System.Windows.Forms.Button btnParamEdit;
        private System.Windows.Forms.Button btnParamNew;
        private System.Windows.Forms.Button btnSetDefault;
        private BrightIdeasSoftware.OLVColumn colComment;
        private BrightIdeasSoftware.OLVColumn colParamComment;

    }
}