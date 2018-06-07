namespace XCI.WinUtility.ComponentManager.UI
{
    partial class frmComponentAbout
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
            this.btnOK = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.labName = new System.Windows.Forms.Label();
            this.labAuthor = new System.Windows.Forms.Label();
            this.labVersion = new System.Windows.Forms.Label();
            this.labCopyRight = new System.Windows.Forms.Label();
            this.labContact = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(391, 192);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(121, 138);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(250, 77);
            this.txtDescription.TabIndex = 3;
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.Location = new System.Drawing.Point(119, 24);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(29, 12);
            this.labName.TabIndex = 1;
            this.labName.Text = "名称";
            // 
            // labAuthor
            // 
            this.labAuthor.AutoSize = true;
            this.labAuthor.Location = new System.Drawing.Point(119, 68);
            this.labAuthor.Name = "labAuthor";
            this.labAuthor.Size = new System.Drawing.Size(29, 12);
            this.labAuthor.TabIndex = 1;
            this.labAuthor.Text = "作者";
            // 
            // labVersion
            // 
            this.labVersion.AutoSize = true;
            this.labVersion.Location = new System.Drawing.Point(119, 46);
            this.labVersion.Name = "labVersion";
            this.labVersion.Size = new System.Drawing.Size(29, 12);
            this.labVersion.TabIndex = 1;
            this.labVersion.Text = "版本";
            // 
            // labCopyRight
            // 
            this.labCopyRight.AutoSize = true;
            this.labCopyRight.Location = new System.Drawing.Point(119, 90);
            this.labCopyRight.Name = "labCopyRight";
            this.labCopyRight.Size = new System.Drawing.Size(29, 12);
            this.labCopyRight.TabIndex = 1;
            this.labCopyRight.Text = "版权";
            // 
            // labContact
            // 
            this.labContact.AutoSize = true;
            this.labContact.Location = new System.Drawing.Point(119, 112);
            this.labContact.Name = "labContact";
            this.labContact.Size = new System.Drawing.Size(29, 12);
            this.labContact.TabIndex = 4;
            this.labContact.TabStop = true;
            this.labContact.Text = "联系";
            this.labContact.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labContact_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmComponentAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 227);
            this.Controls.Add(this.labContact);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labCopyRight);
            this.Controls.Add(this.labVersion);
            this.Controls.Add(this.labAuthor);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmComponentAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "关于 控制台日志";
            this.Load += new System.EventHandler(this.frmConsoleAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Label labAuthor;
        private System.Windows.Forms.Label labVersion;
        private System.Windows.Forms.Label labCopyRight;
        private System.Windows.Forms.LinkLabel labContact;
    }
}