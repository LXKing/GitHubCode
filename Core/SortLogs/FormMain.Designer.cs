namespace SortLogs
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.skinTabControl1 = new CCWin.SkinControl.SkinTabControl();
            this.skinTabPage1 = new CCWin.SkinControl.SkinTabPage();
            this.skinPanel1 = new CCWin.SkinControl.SkinPanel();
            this.btnSort1 = new CCWin.SkinControl.SkinButton();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.skinTabPage2 = new CCWin.SkinControl.SkinTabPage();
            this.skinPanel2 = new CCWin.SkinControl.SkinPanel();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.btnSort2 = new CCWin.SkinControl.SkinButton();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.skinPanel3 = new CCWin.SkinControl.SkinPanel();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtPath1 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.skinProgressBar1 = new CCWin.SkinControl.SkinProgressBar();
            this.skinTabControl1.SuspendLayout();
            this.skinTabPage1.SuspendLayout();
            this.skinPanel1.SuspendLayout();
            this.skinTabPage2.SuspendLayout();
            this.skinPanel2.SuspendLayout();
            this.skinPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinTabControl1
            // 
            this.skinTabControl1.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.skinTabControl1.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.skinTabControl1.Controls.Add(this.skinTabPage1);
            this.skinTabControl1.Controls.Add(this.skinTabPage2);
            this.skinTabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.skinTabControl1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.skinTabControl1.HeadBack = null;
            this.skinTabControl1.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.skinTabControl1.ItemSize = new System.Drawing.Size(70, 36);
            this.skinTabControl1.Location = new System.Drawing.Point(4, 85);
            this.skinTabControl1.Name = "skinTabControl1";
            this.skinTabControl1.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowDown")));
            this.skinTabControl1.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowHover")));
            this.skinTabControl1.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseHover")));
            this.skinTabControl1.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseNormal")));
            this.skinTabControl1.PageDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageDown")));
            this.skinTabControl1.PageHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageHover")));
            this.skinTabControl1.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.skinTabControl1.PageNorml = null;
            this.skinTabControl1.SelectedIndex = 1;
            this.skinTabControl1.Size = new System.Drawing.Size(866, 90);
            this.skinTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.skinTabControl1.TabIndex = 0;
            // 
            // skinTabPage1
            // 
            this.skinTabPage1.BackColor = System.Drawing.Color.White;
            this.skinTabPage1.Controls.Add(this.skinPanel1);
            this.skinTabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPage1.Location = new System.Drawing.Point(0, 36);
            this.skinTabPage1.Name = "skinTabPage1";
            this.skinTabPage1.Size = new System.Drawing.Size(866, 54);
            this.skinTabPage1.TabIndex = 1;
            this.skinTabPage1.TabItemImage = null;
            this.skinTabPage1.Text = "日志整理1";
            // 
            // skinPanel1
            // 
            this.skinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel1.Controls.Add(this.btnSort1);
            this.skinPanel1.Controls.Add(this.skinLabel1);
            this.skinPanel1.Controls.Add(this.dateTimePicker1);
            this.skinPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(0, 0);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Size = new System.Drawing.Size(866, 54);
            this.skinPanel1.TabIndex = 1;
            // 
            // btnSort1
            // 
            this.btnSort1.BackColor = System.Drawing.Color.Transparent;
            this.btnSort1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSort1.DownBack = null;
            this.btnSort1.Location = new System.Drawing.Point(362, 3);
            this.btnSort1.MouseBack = null;
            this.btnSort1.Name = "btnSort1";
            this.btnSort1.NormlBack = null;
            this.btnSort1.Size = new System.Drawing.Size(96, 36);
            this.btnSort1.TabIndex = 3;
            this.btnSort1.Text = "开始处理";
            this.btnSort1.UseVisualStyleBackColor = false;
            this.btnSort1.Click += new System.EventHandler(this.btnSort1_Click);
            // 
            // skinLabel1
            // 
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.skinLabel1.Location = new System.Drawing.Point(21, 17);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(73, 20);
            this.skinLabel1.TabIndex = 2;
            this.skinLabel1.Text = "处理日期:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(100, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(104, 27);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // skinTabPage2
            // 
            this.skinTabPage2.BackColor = System.Drawing.Color.White;
            this.skinTabPage2.Controls.Add(this.skinPanel2);
            this.skinTabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPage2.Location = new System.Drawing.Point(0, 36);
            this.skinTabPage2.Name = "skinTabPage2";
            this.skinTabPage2.Size = new System.Drawing.Size(866, 54);
            this.skinTabPage2.TabIndex = 2;
            this.skinTabPage2.TabItemImage = null;
            this.skinTabPage2.Text = "日志整理2";
            // 
            // skinPanel2
            // 
            this.skinPanel2.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel2.Controls.Add(this.skinLabel5);
            this.skinPanel2.Controls.Add(this.dtEnd);
            this.skinPanel2.Controls.Add(this.btnSort2);
            this.skinPanel2.Controls.Add(this.skinLabel4);
            this.skinPanel2.Controls.Add(this.dtBegin);
            this.skinPanel2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinPanel2.DownBack = null;
            this.skinPanel2.Location = new System.Drawing.Point(0, 0);
            this.skinPanel2.MouseBack = null;
            this.skinPanel2.Name = "skinPanel2";
            this.skinPanel2.NormlBack = null;
            this.skinPanel2.Size = new System.Drawing.Size(866, 54);
            this.skinPanel2.TabIndex = 6;
            // 
            // skinLabel5
            // 
            this.skinLabel5.AutoSize = true;
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.skinLabel5.Location = new System.Drawing.Point(283, 17);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(73, 20);
            this.skinLabel5.TabIndex = 8;
            this.skinLabel5.Text = "截止日期:";
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(362, 12);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(104, 27);
            this.dtEnd.TabIndex = 7;
            // 
            // btnSort2
            // 
            this.btnSort2.BackColor = System.Drawing.Color.Transparent;
            this.btnSort2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSort2.DownBack = null;
            this.btnSort2.Location = new System.Drawing.Point(538, 3);
            this.btnSort2.MouseBack = null;
            this.btnSort2.Name = "btnSort2";
            this.btnSort2.NormlBack = null;
            this.btnSort2.Size = new System.Drawing.Size(96, 36);
            this.btnSort2.TabIndex = 3;
            this.btnSort2.Text = "开始处理";
            this.btnSort2.UseVisualStyleBackColor = false;
            this.btnSort2.Click += new System.EventHandler(this.btnSort2_Click);
            // 
            // skinLabel4
            // 
            this.skinLabel4.AutoSize = true;
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.skinLabel4.Location = new System.Drawing.Point(21, 17);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(73, 20);
            this.skinLabel4.TabIndex = 2;
            this.skinLabel4.Text = "起始日期:";
            // 
            // dtBegin
            // 
            this.dtBegin.Location = new System.Drawing.Point(100, 12);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(104, 27);
            this.dtBegin.TabIndex = 1;
            // 
            // skinPanel3
            // 
            this.skinPanel3.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel3.Controls.Add(this.skinButton1);
            this.skinPanel3.Controls.Add(this.skinLabel2);
            this.skinPanel3.Controls.Add(this.txtPath1);
            this.skinPanel3.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.skinPanel3.DownBack = null;
            this.skinPanel3.Location = new System.Drawing.Point(4, 28);
            this.skinPanel3.MouseBack = null;
            this.skinPanel3.Name = "skinPanel3";
            this.skinPanel3.NormlBack = null;
            this.skinPanel3.Size = new System.Drawing.Size(866, 57);
            this.skinPanel3.TabIndex = 7;
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.Location = new System.Drawing.Point(362, 15);
            this.skinButton1.MouseBack = null;
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Size = new System.Drawing.Size(96, 36);
            this.skinButton1.TabIndex = 9;
            this.skinButton1.Text = "浏览...";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // skinLabel2
            // 
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.skinLabel2.Location = new System.Drawing.Point(21, 23);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(73, 20);
            this.skinLabel2.TabIndex = 8;
            this.skinLabel2.Text = "日志路径:";
            // 
            // txtPath1
            // 
            this.txtPath1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SortLogs.Properties.Settings.Default, "Path", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPath1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txtPath1.Location = new System.Drawing.Point(100, 20);
            this.txtPath1.Name = "txtPath1";
            this.txtPath1.ReadOnly = true;
            this.txtPath1.Size = new System.Drawing.Size(259, 27);
            this.txtPath1.TabIndex = 7;
            this.txtPath1.Text = global::SortLogs.Properties.Settings.Default.Path;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.richTextBox1.Location = new System.Drawing.Point(4, 210);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(866, 425);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // skinProgressBar1
            // 
            this.skinProgressBar1.Back = null;
            this.skinProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.skinProgressBar1.BarBack = null;
            this.skinProgressBar1.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinProgressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.skinProgressBar1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.skinProgressBar1.ForeColor = System.Drawing.Color.Red;
            this.skinProgressBar1.Location = new System.Drawing.Point(4, 175);
            this.skinProgressBar1.Name = "skinProgressBar1";
            this.skinProgressBar1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinProgressBar1.Size = new System.Drawing.Size(866, 35);
            this.skinProgressBar1.TabIndex = 10;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("微软雅黑", 11F);
            this.ClientSize = new System.Drawing.Size(874, 639);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.skinProgressBar1);
            this.Controls.Add(this.skinTabControl1);
            this.Controls.Add(this.skinPanel3);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "日志归档工具";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.skinTabControl1.ResumeLayout(false);
            this.skinTabPage1.ResumeLayout(false);
            this.skinPanel1.ResumeLayout(false);
            this.skinPanel1.PerformLayout();
            this.skinTabPage2.ResumeLayout(false);
            this.skinPanel2.ResumeLayout(false);
            this.skinPanel2.PerformLayout();
            this.skinPanel3.ResumeLayout(false);
            this.skinPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinTabControl skinTabControl1;
        private CCWin.SkinControl.SkinTabPage skinTabPage1;
        private CCWin.SkinControl.SkinPanel skinPanel1;
        private CCWin.SkinControl.SkinButton btnSort1;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private CCWin.SkinControl.SkinTabPage skinTabPage2;
        private CCWin.SkinControl.SkinPanel skinPanel2;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private CCWin.SkinControl.SkinButton btnSort2;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private CCWin.SkinControl.SkinPanel skinPanel3;
        private CCWin.SkinControl.SkinButton skinButton1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private System.Windows.Forms.TextBox txtPath1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private CCWin.SkinControl.SkinProgressBar skinProgressBar1;
    }
}

