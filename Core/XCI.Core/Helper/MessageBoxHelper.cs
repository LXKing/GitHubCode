using System;
using System.Windows.Forms;

namespace XCI.Helper
{
    /// <summary>
    /// 消息提醒帮助类
    /// </summary>
    public static class MessageBoxHelper
    {
        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="message">信息</param>
        public static DialogResult ShowError(string message)
        {
            return ShowMessageBox(message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }
        

        /// <summary>
        /// 显示提醒信息
        /// </summary>
        /// <param name="message">信息</param>
        public static DialogResult ShowTips(string message)
        {
            return ShowMessageBox(message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }


        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="message">信息</param>
        public static DialogResult ShowWarning(string message)
        {
            return ShowMessageBox(message, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }


        /// <summary>
        /// 显示 是,否 错误信息
        /// </summary>
        /// <param name="message">信息</param>
        public static DialogResult ShowYesNoAndError(string message)
        {
            return ShowMessageBox(message, "错误信息", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }



        /// <summary>
        /// 显示 是,否 错误信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exec">是按钮回调函数</param>
        public static void ShowYesNoAndError(string message, Action<DialogResult> exec)
        {
            DialogResult dr = ShowYesNoAndError(message);
            if (dr == DialogResult.Yes)
            {
                exec(dr);
            }
        }


        /// <summary>
        /// 显示 是,否 提醒信息
        /// </summary>
        /// <param name="message">信息</param>
        public static DialogResult ShowYesNoAndTips(string message)
        {
            return ShowMessageBox(message, "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }


        /// <summary>
        /// 显示 是,否 提醒信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exec">是按钮回调函数</param>
        public static void ShowYesNoAndTips(string message, Action<DialogResult> exec)
        {
            DialogResult dr = ShowYesNoAndTips(message);
            if (dr == DialogResult.Yes)
            {
                exec(dr);
            }
        }

        
        /// <summary>
        /// 显示 是,否 警告信息
        /// </summary>
        /// <param name="message">信息</param>
        public static DialogResult ShowYesNoAndWarning(string message)
        {
            return ShowMessageBox(message, "警告信息", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }


        /// <summary>
        /// 显示 是,否 警告信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exec">是按钮回调函数</param>
        public static void ShowYesNoAndWarning(string message, Action<DialogResult> exec)
        {
            DialogResult dr = ShowYesNoAndWarning(message);
            if (dr == DialogResult.Yes)
            {
                exec(dr);
            }
        }
        

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="title">标题</param>
        /// <param name="buttons">按钮</param>
        /// <param name="icon">图标</param>
        /// <param name="defaultButton">默认按钮</param>
        public static DialogResult ShowMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return MessageBox.Show(message, title, buttons, icon, defaultButton);
        }
        
    }
}