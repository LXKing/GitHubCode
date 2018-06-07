using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;

namespace XCI.WinUtility
{
    /// <summary>
    /// 消息提醒帮助类
    /// </summary>
    public static class XtraMessageBoxHelper
    {

        #region AlterMessage

        /// <summary>
        /// 显示桌面提醒信息
        /// </summary>
        /// <param name="alertController">提醒控件</param>
        /// <param name="form">所属窗口</param>
        /// <param name="message">显示消息</param>
        public static void ShowAlterError(AlertControl alertController, Form form, string message)
        {
            ShowAlter(alertController, form, null, null, message);
        }

        /// <summary>
        /// 显示桌面提醒信息
        /// </summary>
        /// <param name="alertController">提醒控件</param>
        /// <param name="form">所属窗口</param>
        /// <param name="message">显示消息</param>
        public static void ShowAlterTips(AlertControl alertController, Form form, string message)
        {
            ShowAlter(alertController, form, null, null, message);
        }

        /// <summary>
        /// 显示桌面提醒信息
        /// </summary>
        /// <param name="alertController">提醒控件</param>
        /// <param name="form">所属窗口</param>
        /// <param name="message">显示消息</param>
        public static void ShowAlterSucess(AlertControl alertController, Form form, string message)
        {
            ShowAlter(alertController, form, null, null, message);
        }

        /// <summary>
        /// 显示桌面提醒信息
        /// </summary>
        /// <param name="alertController">提醒控件</param>
        /// <param name="form">所属窗口</param>
        /// <param name="message">显示消息</param>
        public static void ShowAlterWarning(AlertControl alertController, Form form, string message)
        {
            ShowAlter(alertController, form, null, null, message);
        }

        /// <summary>
        /// 显示桌面提醒信息
        /// </summary>
        /// <param name="alertController">提醒控件</param>
        /// <param name="form">所属窗口</param>
        /// <param name="img">图片</param>
        /// <param name="caption">标题</param>
        /// <param name="message">显示消息</param>
        private static void ShowAlter(AlertControl alertController, Form form, Image img, string caption, string message)
        {
            if (alertController.AlertFormList.Count > 0)
            {
                alertController.AlertFormList[0].Close();
                alertController.AlertFormList.Clear();
            }
            caption = caption ?? "操作提醒";
            alertController.Show(form, caption, message, img);
        }

        #endregion


        /// <summary>
        /// 显示DevExpress错误信息
        /// </summary>
        /// <param name="message">信息</param>
        public static DialogResult ShowError(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, win32);
        }


        /// <summary>
        /// 显示DevExpress提醒信息
        /// </summary>
        /// <param name="message">信息</param>
        public static DialogResult ShowTips(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1,win32);
        }


        /// <summary>
        /// 显示DevExpress警告信息
        /// </summary>
        /// <param name="message">信息</param>
        public static DialogResult ShowWarning(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,win32);
        }

        /// <summary>
        /// 显示 DevExpress是,否 错误信息
        /// </summary>
        /// <param name="message">信息</param>
        public static DialogResult ShowYesNoAndError(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "错误信息", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1,win32);
        }

        /// <summary>
        /// 显示 DevExpress是,否 错误信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exec">回调函数</param>
        public static void ShowYesNoAndError(string message, Action<DialogResult> exec, IWin32Window win32 = null)
        {
            DialogResult dr = ShowYesNoAndError(message,win32);
            if (dr == DialogResult.Yes)
            {
                exec(dr);
            }
        }

        /// <summary>
        /// 显示 DevExpress是,否 提醒信息
        /// </summary>
        /// <param name="message">信息</param>
        public static DialogResult ShowYesNoAndTips(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1,win32);
        }

        /// <summary>
        /// 显示 DevExpress是,否 提醒信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exec">回调函数</param>
        public static void ShowYesNoAndTips(string message, Action<DialogResult> exec, IWin32Window win32 = null)
        {
            DialogResult dr = ShowYesNoAndTips(message,win32);
            if (dr == DialogResult.Yes)
            {
                exec(dr);
            }
        }
        

        /// <summary>
        /// 显示 DevExpress是,否 警告信息
        /// </summary>
        /// <param name="message">信息</param>
        public static DialogResult ShowYesNoAndWarning(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "警告信息", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,win32);
        }

        /// <summary>
        /// 显示 DevExpress是,否 警告信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exec">回调函数</param>
        public static void ShowYesNoAndWarning(string message, Action<DialogResult> exec, IWin32Window win32= null)
        {
            DialogResult dr = ShowYesNoAndWarning(message,win32);
            if (dr == DialogResult.Yes)
            {
                exec(dr);
            }
        }

        public static DialogResult ShowYesNoAndQuestion(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "确认信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, win32);
        }

        public static DialogResult ShowOkCancelAndQuestion(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "确认信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, win32);
        }


        /// <summary>
        /// 显示 DevExpress 消息框
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="title">标题</param>
        /// <param name="buttons">按钮</param>
        /// <param name="icon">图标</param>
        /// <param name="defaultButton">默认按钮</param>
        private static DialogResult ShowMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, IWin32Window win32=null)
        {
            return XtraMessageBox.Show(win32,message, title, buttons, icon, defaultButton);
        }

    }
}