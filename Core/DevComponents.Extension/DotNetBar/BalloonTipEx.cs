using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public static  class BalloonTipEx
    {
        public static  void ShowBalloonTip(this BalloonTip balloonTip,Control controlTrigger,string msg,bool autoFous=false,string title="提示",eBallonStyle ballonStyle=eBallonStyle.Balloon)
        {
            controlTrigger.Invoke(new Action(()=>{
                var ballon = new Balloon();
                ballon.CaptionText = title;
                ballon.Text = msg;
                balloonTip.BalloonControl = ballon;
                balloonTip.ShowAlways = true;
                balloonTip.AutoClose = false;
                ballon.TopLevel = true;
                ballon.TopMost = false;
                if (autoFous)
                {
                    controlTrigger.Focus();
                }
                balloonTip.Style = ballonStyle;
                ballon.Show(controlTrigger,autoFous);
            }));
        }

        public static void HideBalloonTip(this BalloonTip balloonTip,Control control=null)
        {
            if(control!=null)
            {
                balloonTip.Remove(control);
            }
            else
                balloonTip.RemoveAll();
        }
    }
}
