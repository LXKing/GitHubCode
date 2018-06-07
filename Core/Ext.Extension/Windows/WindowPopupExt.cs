using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Ext.Net;
using Ext.Net.Utilities;
namespace Ext.Extension.Windows
{
    public class WindowPopupExt
    {
        public static Ext.Net.Window PopupWindow(string laodPageUrl, string winID, int width, int height, bool maxsizable = false)
        {
            var win = new Window
            {
                ID = winID,
                Width = width,
                Height = height,
                BodyPadding = 5,
                Constrain = true,
                Modal = true,
                Maximizable=maxsizable,
                CloseAction = CloseAction.Destroy,
                Layout = "fit",
                Loader = new ComponentLoader
                {
                    Url = laodPageUrl,
                    Mode = LoadMode.Frame,
                }
            };
            return win;
        }
    }
}
