using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevComponents.DotNetBar.Controls;
using System.Windows.Forms;
namespace DevComponents.DotNetBar
{
    public  class MessageBoxHelp:MessageBoxEx
    {
        public static DialogResult ShowTip(string text,string caption="消息")
        {
            return MessageBoxEx.Show(text, caption,MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        public static DialogResult ShowWarning(string text, string caption = "警告")
        {
            return MessageBoxEx.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static DialogResult ShowError(string text, string caption = "错误")
        {
            return MessageBoxEx.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static DialogResult ShowQuestionYesNo(string text, string caption = "询问")
        {
            return MessageBoxEx.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static DialogResult ShowQuestionOKCancel(string text, string caption = "询问")
        {
            return MessageBoxEx.Show(text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }
        public static DialogResult ShowQuestionRetryCancel(string text, string caption = "询问")
        {
            return MessageBoxEx.Show(text, caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
        }
        public static DialogResult ShowQuestionYesNoCancel(string text, string caption = "询问")
        {
            return MessageBoxEx.Show(text, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
    }
}
