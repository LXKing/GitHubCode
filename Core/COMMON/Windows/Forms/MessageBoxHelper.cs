using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// Window提示框帮助类
    /// </summary>
    public class MessageBoxExHelper
    {
        public static DialogResult ShowInfor(string msg, string title = "消息")
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult ShowInforYesNo(string msg, string title = "消息")
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
        }

        public static DialogResult  ShowWarning(string msg,string title="警告")
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
        }
        public static DialogResult ShowWarningYesNo(string msg, string title = "警告")
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);
        }

        public static DialogResult ShowError(string msg, string title = "错误")
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult ShowErrorYesNo(string msg, string title = "错误")
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
        }

        public static DialogResult ShowQuestionoOKCancel(string msg, string title = "询问")
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult ShowQuestionYesNo(string msg, string title = "询问")
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        /// <summary>
        /// 显示异常详细信息
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static DialogResult ShowException(Exception ex, string title = "错误")
        {
            var frm=new ThreadExceptionDialog(ex);
            frm.Text = title;
            return frm.ShowDialog();
        }
    }
}
