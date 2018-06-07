namespace DevComponents.DotNetBar
{
    partial class PageBarEx
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageBarEx));
            this.btnFirstPage = new System.Windows.Forms.PictureBox();
            this.btnPreviousPage = new System.Windows.Forms.PictureBox();
            this.btnNextPage = new System.Windows.Forms.PictureBox();
            this.btnLastPage = new System.Windows.Forms.PictureBox();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.line2 = new DevComponents.DotNetBar.Controls.Line();
            this.labPageInfo = new DevComponents.DotNetBar.LabelX();
            this.balloonTip1 = new DevComponents.DotNetBar.BalloonTip();
            this.txtCurrentPageIndex = new DevComponents.Editors.IntegerInput();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            ((System.ComponentModel.ISupportInitialize)(this.btnFirstPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPreviousPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNextPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLastPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentPageIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFirstPage
            // 
            this.balloonTip1.SetBalloonCaption(this.btnFirstPage, null);
            this.balloonTip1.SetBalloonText(this.btnFirstPage, "首页");
            this.btnFirstPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFirstPage.Image = global::DevComponents.Properties.Resources.previous_first;
            this.btnFirstPage.Location = new System.Drawing.Point(25, 7);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(19, 19);
            this.btnFirstPage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnFirstPage.TabIndex = 0;
            this.btnFirstPage.TabStop = false;
            this.btnFirstPage.Click += new System.EventHandler(this.btnFirstPage_Click);
            // 
            // btnPreviousPage
            // 
            this.balloonTip1.SetBalloonCaption(this.btnPreviousPage, null);
            this.balloonTip1.SetBalloonText(this.btnPreviousPage, "前一页");
            this.btnPreviousPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviousPage.Image")));
            this.btnPreviousPage.Location = new System.Drawing.Point(84, 7);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(19, 19);
            this.btnPreviousPage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPreviousPage.TabIndex = 1;
            this.btnPreviousPage.TabStop = false;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // btnNextPage
            // 
            this.balloonTip1.SetBalloonCaption(this.btnNextPage, null);
            this.balloonTip1.SetBalloonText(this.btnNextPage, "下一页");
            this.btnNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextPage.Image = ((System.Drawing.Image)(resources.GetObject("btnNextPage.Image")));
            this.btnNextPage.Location = new System.Drawing.Point(303, 7);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(19, 19);
            this.btnNextPage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnNextPage.TabIndex = 2;
            this.btnNextPage.TabStop = false;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnLastPage
            // 
            this.balloonTip1.SetBalloonCaption(this.btnLastPage, null);
            this.balloonTip1.SetBalloonText(this.btnLastPage, "最后一页");
            this.btnLastPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLastPage.Image = ((System.Drawing.Image)(resources.GetObject("btnLastPage.Image")));
            this.btnLastPage.Location = new System.Drawing.Point(360, 7);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(19, 19);
            this.btnLastPage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnLastPage.TabIndex = 3;
            this.btnLastPage.TabStop = false;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // line1
            // 
            this.line1.Location = new System.Drawing.Point(84, 5);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(75, 24);
            this.line1.TabIndex = 4;
            this.line1.VerticalLine = true;
            // 
            // line2
            // 
            this.line2.Location = new System.Drawing.Point(255, 5);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(75, 24);
            this.line2.TabIndex = 6;
            this.line2.Text = "line2";
            this.line2.VerticalLine = true;
            // 
            // labPageInfo
            // 
            // 
            // 
            // 
            this.labPageInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labPageInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.labPageInfo.Location = new System.Drawing.Point(208, 7);
            this.labPageInfo.Name = "labPageInfo";
            this.labPageInfo.Size = new System.Drawing.Size(80, 23);
            this.labPageInfo.TabIndex = 7;
            this.labPageInfo.Text = "of {0}";
            this.labPageInfo.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // balloonTip1
            // 
            this.balloonTip1.DefaultBalloonWidth = 50;
            this.balloonTip1.InitialDelay = 100;
            this.balloonTip1.MinimumBalloonWidth = 30;
            this.balloonTip1.ShowBalloonOnFocus = true;
            this.balloonTip1.ShowCloseButton = false;
            // 
            // txtCurrentPageIndex
            // 
            this.txtCurrentPageIndex.AllowEmptyState = false;
            // 
            // 
            // 
            this.txtCurrentPageIndex.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtCurrentPageIndex.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCurrentPageIndex.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtCurrentPageIndex.FieldNavigation = DevComponents.Editors.eInputFieldNavigation.Enter;
            this.txtCurrentPageIndex.InputMouseWheelEnabled = false;
            this.txtCurrentPageIndex.Location = new System.Drawing.Point(127, 6);
            this.txtCurrentPageIndex.MinValue = 0;
            this.txtCurrentPageIndex.Name = "txtCurrentPageIndex";
            this.txtCurrentPageIndex.Size = new System.Drawing.Size(79, 21);
            this.txtCurrentPageIndex.TabIndex = 8;
            this.txtCurrentPageIndex.ValueObjectChanged += new System.EventHandler(this.txtCurrentPageIndex_ValueObjectChanged);
            this.txtCurrentPageIndex.ConvertFreeTextEntry += new DevComponents.Editors.FreeTextEntryConversionEventHandler(this.txtCurrentPageIndex_ConvertFreeTextEntry);
            this.txtCurrentPageIndex.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrentPageIndex_KeyPress);
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh.Location = new System.Drawing.Point(405, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefresh.Symbol = "";
            this.btnRefresh.SymbolSize = 12F;
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // progressBarX1
            // 
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarX1.Location = new System.Drawing.Point(486, 4);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.Size = new System.Drawing.Size(247, 23);
            this.progressBarX1.TabIndex = 10;
            this.progressBarX1.TextVisible = true;
            // 
            // PageBarEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBarX1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.txtCurrentPageIndex);
            this.Controls.Add(this.labPageInfo);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.btnLastPage);
            this.Controls.Add(this.btnFirstPage);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.line2);
            this.DoubleBuffered = true;
            this.Name = "PageBarEx";
            this.Size = new System.Drawing.Size(753, 33);
            this.SizeChanged += new System.EventHandler(this.PageBarEx_SizeChanged);
            this.Resize += new System.EventHandler(this.PageBarEx_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.btnFirstPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPreviousPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNextPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLastPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentPageIndex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox btnFirstPage;
        private BalloonTip balloonTip1;
        private System.Windows.Forms.PictureBox btnPreviousPage;
        private System.Windows.Forms.PictureBox btnNextPage;
        private System.Windows.Forms.PictureBox btnLastPage;
        private Controls.Line line1;
        private Controls.Line line2;
        private LabelX labPageInfo;
        private Editors.IntegerInput txtCurrentPageIndex;
        private ButtonX btnRefresh;
        private Controls.ProgressBarX progressBarX1;
    }
}
