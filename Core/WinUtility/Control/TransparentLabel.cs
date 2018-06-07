using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XCI.WinUtility
{
    public class TransparentLabel : System.Windows.Forms.PictureBox
    {
        public TransparentLabel()
        {
        }
          //Fields
        private Point _Location = new Point(0,0);
        //Properties
        public Point TextLocation
        {
            get
            {
                return this._Location;
            }
            set
            {
                this._Location = value;
            }
        }
        [Browsable(true)]
        new public string Text
        {
            get{return base.Text;}
            set{base.Text = value;}
        }
        [Browsable(true)]
        new public Font Font
        {
            get{return base.Font;}
            set{base.Font = value;}
        }
        

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint (e);
            SizeF m_size = e.Graphics.MeasureString(this.Text,this.Font);
            e.Graphics.DrawString(this.Text,this.Font,Brushes.Black,new RectangleF(this._Location,m_size));
        }
    }
}