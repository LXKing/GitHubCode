namespace XCI.Component
{
    partial class frmEncryptTest
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
            this.groupParams = new System.Windows.Forms.GroupBox();
            this.cbIsEncrypt = new System.Windows.Forms.CheckBox();
            this.txtInternalKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupTest = new System.Windows.Forms.GroupBox();
            this.txtEncryptText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPlainText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtAuthorInfo = new System.Windows.Forms.TextBox();
            this.groupParams.SuspendLayout();
            this.groupTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupParams
            // 
            this.groupParams.Controls.Add(this.cbIsEncrypt);
            this.groupParams.Controls.Add(this.txtInternalKey);
            this.groupParams.Controls.Add(this.label1);
            this.groupParams.Location = new System.Drawing.Point(20, 70);
            this.groupParams.Margin = new System.Windows.Forms.Padding(5);
            this.groupParams.Name = "groupParams";
            this.groupParams.Padding = new System.Windows.Forms.Padding(5);
            this.groupParams.Size = new System.Drawing.Size(684, 143);
            this.groupParams.TabIndex = 1;
            this.groupParams.TabStop = false;
            this.groupParams.Text = "参数";
            // 
            // cbIsEncrypt
            // 
            this.cbIsEncrypt.AutoSize = true;
            this.cbIsEncrypt.Location = new System.Drawing.Point(103, 71);
            this.cbIsEncrypt.Margin = new System.Windows.Forms.Padding(5);
            this.cbIsEncrypt.Name = "cbIsEncrypt";
            this.cbIsEncrypt.Size = new System.Drawing.Size(72, 16);
            this.cbIsEncrypt.TabIndex = 2;
            this.cbIsEncrypt.Text = "是否加密";
            this.cbIsEncrypt.UseVisualStyleBackColor = true;
            // 
            // txtInternalKey
            // 
            this.txtInternalKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInternalKey.Location = new System.Drawing.Point(103, 32);
            this.txtInternalKey.Margin = new System.Windows.Forms.Padding(5);
            this.txtInternalKey.Name = "txtInternalKey";
            this.txtInternalKey.Size = new System.Drawing.Size(540, 29);
            this.txtInternalKey.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "密钥";
            // 
            // groupTest
            // 
            this.groupTest.Controls.Add(this.txtEncryptText);
            this.groupTest.Controls.Add(this.label3);
            this.groupTest.Controls.Add(this.txtPlainText);
            this.groupTest.Controls.Add(this.label2);
            this.groupTest.Location = new System.Drawing.Point(20, 223);
            this.groupTest.Margin = new System.Windows.Forms.Padding(5);
            this.groupTest.Name = "groupTest";
            this.groupTest.Padding = new System.Windows.Forms.Padding(5);
            this.groupTest.Size = new System.Drawing.Size(684, 233);
            this.groupTest.TabIndex = 0;
            this.groupTest.TabStop = false;
            this.groupTest.Text = "测试";
            // 
            // txtEncryptText
            // 
            this.txtEncryptText.Location = new System.Drawing.Point(103, 129);
            this.txtEncryptText.Margin = new System.Windows.Forms.Padding(5);
            this.txtEncryptText.Multiline = true;
            this.txtEncryptText.Name = "txtEncryptText";
            this.txtEncryptText.Size = new System.Drawing.Size(540, 87);
            this.txtEncryptText.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 132);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "密文";
            // 
            // txtPlainText
            // 
            this.txtPlainText.Location = new System.Drawing.Point(103, 32);
            this.txtPlainText.Margin = new System.Windows.Forms.Padding(5);
            this.txtPlainText.Multiline = true;
            this.txtPlainText.Name = "txtPlainText";
            this.txtPlainText.Size = new System.Drawing.Size(540, 87);
            this.txtPlainText.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "明文";
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(266, 471);
            this.btnDecrypt.Margin = new System.Windows.Forms.Padding(5);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(125, 40);
            this.btnDecrypt.TabIndex = 3;
            this.btnDecrypt.Text = "解密";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(109, 471);
            this.btnEncrypt.Margin = new System.Windows.Forms.Padding(5);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(125, 40);
            this.btnEncrypt.TabIndex = 2;
            this.btnEncrypt.Text = "加密";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(423, 471);
            this.btnClear.Margin = new System.Windows.Forms.Padding(5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(125, 40);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(579, 471);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(5);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(125, 40);
            this.btnCopy.TabIndex = 4;
            this.btnCopy.Text = "复制密文";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(12, 12);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(50, 50);
            this.picLogo.TabIndex = 5;
            this.picLogo.TabStop = false;
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.SystemColors.Control;
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTitle.Location = new System.Drawing.Point(79, 12);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(625, 22);
            this.txtTitle.TabIndex = 13;
            // 
            // txtAuthorInfo
            // 
            this.txtAuthorInfo.BackColor = System.Drawing.SystemColors.Control;
            this.txtAuthorInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAuthorInfo.Location = new System.Drawing.Point(79, 40);
            this.txtAuthorInfo.Name = "txtAuthorInfo";
            this.txtAuthorInfo.Size = new System.Drawing.Size(625, 22);
            this.txtAuthorInfo.TabIndex = 13;
            // 
            // frmEncryptTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 525);
            this.Controls.Add(this.txtAuthorInfo);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.groupTest);
            this.Controls.Add(this.groupParams);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEncryptTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "加密模块测试";
            this.Load += new System.EventHandler(this.frmEncryptTest_Load);
            this.groupParams.ResumeLayout(false);
            this.groupParams.PerformLayout();
            this.groupTest.ResumeLayout(false);
            this.groupTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupParams;
        private System.Windows.Forms.GroupBox groupTest;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.TextBox txtInternalKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbIsEncrypt;
        private System.Windows.Forms.TextBox txtEncryptText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPlainText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthorInfo;
    }
}