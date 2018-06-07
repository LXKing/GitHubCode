using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevComponents.DotNetBar
{
    public static class ProgressBarXEx
    {
        public static  void SetProgressBarValue( this ProgressBarX progressBar,int value,string msg="")
        {
            progressBar.Invoke(new Action(() =>{
                progressBar.Value = value;
                if (string.IsNullOrEmpty(msg))
                {
                    progressBar.Text = msg;
                    progressBar.TextVisible = true;
                }
            }));
        }
    }
}
