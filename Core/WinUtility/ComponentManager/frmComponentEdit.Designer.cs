namespace XCI.WinUtility.ComponentManager.UI
{
    partial class frmComponentEdit
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
            this.label3 = new System.Windows.Forms.Label();
            this.lineControl1 = new XCI.WinUtility.LineControl();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProvider = new System.Windows.Forms.ComboBox();
            this.txtIsDefault = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 31);
            this.label3.TabIndex = 8;
            this.label3.Text = "组件编辑";
            // 
            // lineControl1
            // 
            this.lineControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineControl1.Location = new System.Drawing.Point(14, 45);
            this.lineControl1.Margin = new System.Windows.Forms.Padding(5);
            this.lineControl1.Name = "lineControl1";
            this.lineControl1.Size = new System.Drawing.Size(756, 10);
            this.lineControl1.TabIndex = 9;
            this.lineControl1.Text = "lineControl1";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(349, 248);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 35);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保 存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "实现";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(124, 78);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(602, 29);
            this.txtName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            // 
            // txtProvider
            // 
            this.txtProvider.FormattingEnabled = true;
            this.txtProvider.Location = new System.Drawing.Point(124, 125);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.Size = new System.Drawing.Size(602, 29);
            this.txtProvider.TabIndex = 3;
            this.txtProvider.SelectedIndexChanged += new System.EventHandler(this.txtProvider_SelectedIndexChanged);
            // 
            // txtIsDefault
            // 
            this.txtIsDefault.AutoSize = true;
            this.txtIsDefault.Location = new System.Drawing.Point(124, 219);
            this.txtIsDefault.Name = "txtIsDefault";
            this.txtIsDefault.Size = new System.Drawing.Size(61, 25);
            this.txtIsDefault.TabIndex = 6;
            this.txtIsDefault.Text = "默认";
            this.txtIsDefault.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "描述";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(124, 172);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(602, 29);
            this.txtComment.TabIndex = 5;
            // 
            // frmComponentEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 296);
            this.Controls.Add(this.txtIsDefault);
            this.Controls.Add(this.txtProvider);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lineControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmComponentEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "组件编辑";
            this.Load += new System.EventHandler(this.frmComponentEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private XCI.WinUtility.LineControl lineControl1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox txtProvider;
        private System.Windows.Forms.CheckBox txtIsDefault;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtComment;
    }
}