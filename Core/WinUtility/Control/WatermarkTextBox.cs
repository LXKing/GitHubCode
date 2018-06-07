using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace XCI.WinUtility
{
    /// <summary>
    /// 实现了水印效果的文本框控件
    /// </summary>
    public class WatermarkTextBox : TextBox
    {
        const int WM_PAINT = 0x000F;

        string _watermark;

        /// <summary>
        /// 初始化新实例
        /// </summary>
        public WatermarkTextBox()
        {
            _watermark = string.Empty;
        }

        /// <summary>
        /// 获取或设置文本框的水印文字
        /// </summary>
        [DefaultValue(""),Description("设置文本框的水印文字")]
        public string Watermark
        {
            get { return _watermark; }
            set
            {
                // 让 _watermark 始终保持非空值。
                if (value == null) _watermark = string.Empty;
                else _watermark = value.Trim();

                // 更改水印文字后立刻刷新以使效果生效，特别是对设计时有较大的帮助。
                this.Refresh();
            }
        }

        /// <summary>
        /// 已重载，用于绘制水印。
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            // 由基类先处理消息，因为水印需要在控件绘制完毕以后再绘制。
            base.WndProc(ref m);


            if (m.Msg == WM_PAINT)
            {
                // 在文本框没有内容，并且已经指定了水印文字的情况下进行绘制。
                if (this.Text.Length == 0 && _watermark.Length > 0)
                {
                    Brush brush = SystemBrushes.GrayText;
                    Font font = this.Font;
                    Rectangle rect = this.ClientRectangle;
                    StringFormat stringFormat = new StringFormat();

                    // 设置水印文字位于文本框的 左-中 部。
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    // 设置以字符方式来裁剪水印文本，并且不加省略号。
                    stringFormat.Trimming = StringTrimming.Character;

                    using (Graphics g = this.CreateGraphics())
                    {
                        // 开始绘制。
                        g.DrawString(_watermark, font, brush, rect, stringFormat);
                    }

                    // 释放非托管资源。
                    stringFormat.Dispose();
                }
            }
        }
    }
}
