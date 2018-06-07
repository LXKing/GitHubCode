namespace XCI.WinUtility.ComponentManager.UI
{
    partial class frmParamEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtParamValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lineControl1 = new XCI.WinUtility.LineControl();
            this.label4 = new System.Windows.Forms.Label();
            this.txtParamComment = new System.Windows.Forms.TextBox();
            this.txtParamName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "参数名称";
            // 
            // txtParamValue
            // 
            this.txtParamValue.Location = new System.Drawing.Point(148, 128);
            this.txtParamValue.Multiline = true;
            this.txtParamValue.Name = "txtParamValue";
            this.txtParamValue.Size = new System.Drawing.Size(577, 125);
            this.txtParamValue.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "参数值";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(349, 351);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 35);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保 存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 31);
            this.label3.TabIndex = 7;
            this.label3.Text = "参数编辑";
            // 
            // lineControl1
            // 
            this.lineControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineControl1.Location = new System.Drawing.Point(14, 45);
            this.lineControl1.Margin = new System.Windows.Forms.Padding(5);
            this.lineControl1.Name = "lineControl1";
            this.lineControl1.Size = new System.Drawing.Size(756, 10);
            this.lineControl1.TabIndex = 8;
            this.lineControl1.Text = "lineControl1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "描述";
            // 
            // txtParamComment
            // 
            this.txtParamComment.Location = new System.Drawing.Point(148, 277);
            this.txtParamComment.Name = "txtParamComment";
            this.txtParamComment.Size = new System.Drawing.Size(577, 29);
            this.txtParamComment.TabIndex = 5;
            // 
            // txtParamName
            // 
            this.txtParamName.FormattingEnabled = true;
            this.txtParamName.Location = new System.Drawing.Point(148, 78);
            this.txtParamName.Name = "txtParamName";
            this.txtParamName.Size = new System.Drawing.Size(577, 29);
            this.txtParamName.TabIndex = 1;
            // 
            // frmParamEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 395);
            this.Controls.Add(this.txtParamName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lineControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtParamValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtParamComment);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmParamEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数编辑";
            this.Load += new System.EventHandler(this.frmParamEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtParamValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private XCI.WinUtility.LineControl lineControl1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtParamComment;
        private System.Windows.Forms.ComboBox txtParamName;
    }
}