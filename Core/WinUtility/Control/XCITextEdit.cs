using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace XCI.WinUtility
{
    public class XCITextBoxMaskBox:TextBoxMaskBox
    {
        #region 属性

        public TextEdit ChinaOwnerEdit { get; set; }
        private Font oldFont = null;
        private Boolean waterMarkTextEnabled = false;


        public Font WaterMarkFont { get; set; }

        private Color _waterMarkColor = Color.Gray;
        public Color WaterMarkColor
        {
            get { return _waterMarkColor; }
            set { _waterMarkColor = value; Invalidate(); }
        }

        private string _waterMarkText;
        public string WaterMarkText
        {
            get { return _waterMarkText; }
            set { _waterMarkText = value; Invalidate(); }
        }

        #endregion

        public XCITextBoxMaskBox(DevExpress.XtraEditors.TextEdit ownerEdit)
            : base(ownerEdit)
        {
            this.ChinaOwnerEdit = ownerEdit;
            WaterMarkFont = base.Font;
            JoinEvents(true);
        }
        
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            WaterMark_Toggel(null, null);
        }

        protected override void OnPaint(PaintEventArgs args)
        {
            if (waterMarkTextEnabled)
            {
                args.Graphics.DrawString(WaterMarkText, WaterMarkFont, new SolidBrush(WaterMarkColor), new PointF(0.0F, 0.0F));
            }
            else
            {
                args.Graphics.DrawString(ChinaOwnerEdit.Text, Font, new SolidBrush(base.ForeColor), new PointF(0.0F, 0.0F)); 
            }
            base.OnPaint(args);
        }

        private void JoinEvents(Boolean join)
        {
            if (join)
            {
                this.TextChanged += new System.EventHandler(this.WaterMark_Toggel);
                this.LostFocus += new System.EventHandler(this.WaterMark_Toggel);
                this.FontChanged += new System.EventHandler(this.WaterMark_FontChanged);
            }
        }

        private void WaterMark_Toggel(object sender, EventArgs args)
        {
            if (this.ChinaOwnerEdit.Text.Length <= 0)
                EnableWaterMark();
            else
                DisbaleWaterMark();
        }

        private void EnableWaterMark()
        {
            oldFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style, Font.Unit);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.waterMarkTextEnabled = true;
            Refresh();
        }

        private void DisbaleWaterMark()
        {
            this.waterMarkTextEnabled = false;
            this.SetStyle(ControlStyles.UserPaint, false);
            if (oldFont != null)
                this.Font = new System.Drawing.Font(oldFont.FontFamily, oldFont.Size, oldFont.Style, oldFont.Unit);
        }

        private void WaterMark_FontChanged(object sender, EventArgs args)
        {
            if (waterMarkTextEnabled)
            {
                oldFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style, Font.Unit);
                Refresh();
            }
        }

    }

    /// <summary>
    /// 水印文本框
    /// </summary>
    public class XCITextEdit : TextEdit
    {
        private Color CurrentBackColor { get; set; }

        public bool EnableChangerBackColor { get; set; }

        public bool EnableZeroConvertEmpty { get; set; }

        public Font WaterMarkFont { get; set; }

        private Color _waterMarkColor = Color.Gray;
        public Color WaterMarkColor
        {
            get { return _waterMarkColor; }
            set { _waterMarkColor = value;}
        }

        [Browsable(true)]
        public string WaterMarkText { get; set; }

        /// <summary>
        /// 初始化文本水印
        /// </summary>
        public void WaterMarkInit(bool isCleanText=true)
        {
            XCITextBoxMaskBox maskBox = this.MaskBox as XCITextBoxMaskBox;
            if (maskBox != null)
            {
                if (this.WaterMarkFont==null)
                {
                    maskBox.WaterMarkFont = new Font(this.Font.FontFamily,
                                                     this.Font.Size, FontStyle.Regular);
                }
                else
                {
                    maskBox.WaterMarkFont = this.WaterMarkFont;
                }
                maskBox.WaterMarkColor = this.WaterMarkColor;
                maskBox.WaterMarkText = this.WaterMarkText;
                if (isCleanText)
                {
                    this.Text = string.Empty;
                }
            }
        }


        protected override TextBoxMaskBox CreateMaskBoxInstance()
        {
            return new XCITextBoxMaskBox(this);
        }
        
        protected override void OnGotFocus(EventArgs e)
        {
            if (EnableChangerBackColor)
            {
                this.CurrentBackColor = this.BackColor;
                this.BackColor = Color.FromArgb(255, 249, 169);
            }
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (EnableChangerBackColor)
            {
                this.BackColor = this.CurrentBackColor;
            }
            base.OnLostFocus(e);
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (EnableZeroConvertEmpty)
                {
                    decimal temp = -1;
                    if (decimal.TryParse(value, out temp) && temp == 0)
                    {
                        base.Text = string.Empty;
                    }
                    else
                    {
                        base.Text = value;
                    }
                }
                else
                {
                    base.Text = value;
                }
            }
        }

    }

    /// <summary>
    /// 水印按钮文本框
    /// </summary>
    public class XCIButtonEdit : ButtonEdit
    {
        private Color CurrentBackColor { get; set; }

        public bool EnableChangerBackColor { get; set; }

        public bool EnableZeroConvertEmpty { get; set; }

        public Font WaterMarkFont { get; set; }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                this.WaterMarkFont = value;
                base.Font = value;
            }
        }

        private Color _waterMarkColor = Color.Gray;
        public Color WaterMarkColor
        {
            get { return _waterMarkColor; }
            set { _waterMarkColor = value; }
        }

        [Browsable(true)]
        public string WaterMarkText { get; set; }

        /// <summary>
        /// 初始化文本水印
        /// </summary>
        public void WaterMarkInit(bool isCleanText = true)
        {
            XCITextBoxMaskBox maskBox = this.MaskBox as XCITextBoxMaskBox;
            if (maskBox != null)
            {
                if (this.WaterMarkFont == null)
                {
                    maskBox.WaterMarkFont = new Font(this.Font.FontFamily,
                                                     this.Font.Size, FontStyle.Regular);
                }
                else
                {
                    maskBox.WaterMarkFont = this.WaterMarkFont;
                }
                maskBox.WaterMarkColor = this.WaterMarkColor;
                maskBox.WaterMarkText = this.WaterMarkText;
                if (isCleanText)
                {
                    this.Text = string.Empty;
                }
            }
        }

        protected override TextBoxMaskBox CreateMaskBoxInstance()
        {
            return new XCITextBoxMaskBox(this);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (EnableChangerBackColor)
            {
                this.CurrentBackColor = this.BackColor;
                this.BackColor = Color.FromArgb(255, 249, 169);
            }
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (EnableChangerBackColor)
            {
                this.BackColor = this.CurrentBackColor;
            }
            base.OnLostFocus(e);
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (EnableZeroConvertEmpty)
                {
                    decimal temp = -1;
                    if (decimal.TryParse(value, out temp) && temp == 0)
                    {
                        base.Text = string.Empty;
                    }
                    else
                    {
                        base.Text = value;
                    }
                }
                else
                {
                    base.Text = value;
                }
            }
        }
    }
}