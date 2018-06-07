
namespace XCI.WinUtility.GridConfig
{
    partial class GridAdvancedEditor
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
            this.btnResetCurrentControlDefault = new DevExpress.XtraEditors.SimpleButton();
            this.btnResetAllControlDefault = new DevExpress.XtraEditors.SimpleButton();
            this.btnResetAllControlAllUser = new DevExpress.XtraEditors.SimpleButton();
            this.btnResetCurrentControlAllUser = new DevExpress.XtraEditors.SimpleButton();
            this.btnSetAsDefault = new DevExpress.XtraEditors.SimpleButton();
            this.propertyGridControl = new XCI.WinUtility.XCIPropertyGrid();
            this.SuspendLayout();
            // 
            // btnResetCurrentControlDefault
            // 
            this.btnResetCurrentControlDefault.Location = new System.Drawing.Point(672, 144);
            this.btnResetCurrentControlDefault.Name = "btnResetCurrentControlDefault";
            this.btnResetCurrentControlDefault.Size = new System.Drawing.Size(225, 30);
            this.btnResetCurrentControlDefault.TabIndex = 13;
            this.btnResetCurrentControlDefault.Text = "重置本控件";
            this.btnResetCurrentControlDefault.Click += new System.EventHandler(this.btnResetCurrentControlDefault_Click);
            // 
            // btnResetAllControlDefault
            // 
            this.btnResetAllControlDefault.Location = new System.Drawing.Point(672, 108);
            this.btnResetAllControlDefault.Name = "btnResetAllControlDefault";
            this.btnResetAllControlDefault.Size = new System.Drawing.Size(225, 30);
            this.btnResetAllControlDefault.TabIndex = 14;
            this.btnResetAllControlDefault.Text = "重置全部控件";
            this.btnResetAllControlDefault.Click += new System.EventHandler(this.btnResetAllControlDefault_Click);
            // 
            // btnResetAllControlAllUser
            // 
            this.btnResetAllControlAllUser.Location = new System.Drawing.Point(672, 72);
            this.btnResetAllControlAllUser.Name = "btnResetAllControlAllUser";
            this.btnResetAllControlAllUser.Size = new System.Drawing.Size(225, 30);
            this.btnResetAllControlAllUser.TabIndex = 12;
            this.btnResetAllControlAllUser.Text = "重置全部控件全部用户配置";
            this.btnResetAllControlAllUser.Click += new System.EventHandler(this.btnResetAllControlAllUser_Click);
            // 
            // btnResetCurrentControlAllUser
            // 
            this.btnResetCurrentControlAllUser.Location = new System.Drawing.Point(672, 36);
            this.btnResetCurrentControlAllUser.Name = "btnResetCurrentControlAllUser";
            this.btnResetCurrentControlAllUser.Size = new System.Drawing.Size(225, 30);
            this.btnResetCurrentControlAllUser.TabIndex = 10;
            this.btnResetCurrentControlAllUser.Text = "重置本控件全部用户配置";
            this.btnResetCurrentControlAllUser.Click += new System.EventHandler(this.btnResetCurrentControlAllUser_Click);
            // 
            // btnSetAsDefault
            // 
            this.btnSetAsDefault.Location = new System.Drawing.Point(672, 0);
            this.btnSetAsDefault.Name = "btnSetAsDefault";
            this.btnSetAsDefault.Size = new System.Drawing.Size(225, 30);
            this.btnSetAsDefault.TabIndex = 11;
            this.btnSetAsDefault.Text = "设为默认值";
            this.btnSetAsDefault.Click += new System.EventHandler(this.btnSetAsDefault_Click);
            // 
            // propertyGridControl
            // 
            this.propertyGridControl.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertyGridControl.Appearance.Options.UseFont = true;
            this.propertyGridControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.propertyGridControl.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControl.Margin = new System.Windows.Forms.Padding(4);
            this.propertyGridControl.Name = "propertyGridControl";
            this.propertyGridControl.Size = new System.Drawing.Size(665, 500);
            this.propertyGridControl.TabIndex = 1;
            // 
            // GridAdvancedEditor
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnResetCurrentControlDefault);
            this.Controls.Add(this.btnResetAllControlDefault);
            this.Controls.Add(this.btnResetAllControlAllUser);
            this.Controls.Add(this.btnResetCurrentControlAllUser);
            this.Controls.Add(this.btnSetAsDefault);
            this.Controls.Add(this.propertyGridControl);
            this.Name = "GridAdvancedEditor";
            this.Size = new System.Drawing.Size(900, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private XCIPropertyGrid propertyGridControl;
        private DevExpress.XtraEditors.SimpleButton btnResetCurrentControlDefault;
        private DevExpress.XtraEditors.SimpleButton btnResetAllControlDefault;
        private DevExpress.XtraEditors.SimpleButton btnResetAllControlAllUser;
        private DevExpress.XtraEditors.SimpleButton btnResetCurrentControlAllUser;
        private DevExpress.XtraEditors.SimpleButton btnSetAsDefault;
    }
}
