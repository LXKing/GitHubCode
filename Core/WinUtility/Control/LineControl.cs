using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace XCI.WinUtility
{
    /// <summary>
    /// Ïß¿Ø¼þ
    /// </summary>
    public class LineControl : Control
    {
        public LineControl()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics graphics = e.Graphics;
            Rectangle clientRectangle = base.ClientRectangle;
            clientRectangle.Y = clientRectangle.Height / 2;
            clientRectangle.Height = 1;
            using (LinearGradientBrush brush = new LinearGradientBrush(clientRectangle, Color.FromArgb(0x69, 0x69, 0x69), Color.Transparent, 180f))
            {
                Blend blend = new Blend();
                blend.Positions = new float[] { 0f, 0.3f, 0.5f, 0.7f, 1f };
                blend.Factors = new float[] { 1f, 0.8f, 0.4f, 0.2f, 0f };
                brush.Blend = blend;
                graphics.FillRectangle(brush, clientRectangle);
            }
        }
    }
}