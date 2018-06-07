using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;

namespace XCI.WinUtility
{
    /// <summary>
    /// ��Ϣ���Ѱ�����
    /// </summary>
    public static class XtraMessageBoxHelper
    {

        #region AlterMessage

        /// <summary>
        /// ��ʾ����������Ϣ
        /// </summary>
        /// <param name="alertController">���ѿؼ�</param>
        /// <param name="form">��������</param>
        /// <param name="message">��ʾ��Ϣ</param>
        public static void ShowAlterError(AlertControl alertController, Form form, string message)
        {
            ShowAlter(alertController, form, null, null, message);
        }

        /// <summary>
        /// ��ʾ����������Ϣ
        /// </summary>
        /// <param name="alertController">���ѿؼ�</param>
        /// <param name="form">��������</param>
        /// <param name="message">��ʾ��Ϣ</param>
        public static void ShowAlterTips(AlertControl alertController, Form form, string message)
        {
            ShowAlter(alertController, form, null, null, message);
        }

        /// <summary>
        /// ��ʾ����������Ϣ
        /// </summary>
        /// <param name="alertController">���ѿؼ�</param>
        /// <param name="form">��������</param>
        /// <param name="message">��ʾ��Ϣ</param>
        public static void ShowAlterSucess(AlertControl alertController, Form form, string message)
        {
            ShowAlter(alertController, form, null, null, message);
        }

        /// <summary>
        /// ��ʾ����������Ϣ
        /// </summary>
        /// <param name="alertController">���ѿؼ�</param>
        /// <param name="form">��������</param>
        /// <param name="message">��ʾ��Ϣ</param>
        public static void ShowAlterWarning(AlertControl alertController, Form form, string message)
        {
            ShowAlter(alertController, form, null, null, message);
        }

        /// <summary>
        /// ��ʾ����������Ϣ
        /// </summary>
        /// <param name="alertController">���ѿؼ�</param>
        /// <param name="form">��������</param>
        /// <param name="img">ͼƬ</param>
        /// <param name="caption">����</param>
        /// <param name="message">��ʾ��Ϣ</param>
        private static void ShowAlter(AlertControl alertController, Form form, Image img, string caption, string message)
        {
            if (alertController.AlertFormList.Count > 0)
            {
                alertController.AlertFormList[0].Close();
                alertController.AlertFormList.Clear();
            }
            caption = caption ?? "��������";
            alertController.Show(form, caption, message, img);
        }

        #endregion


        /// <summary>
        /// ��ʾDevExpress������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public static DialogResult ShowError(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, win32);
        }


        /// <summary>
        /// ��ʾDevExpress������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public static DialogResult ShowTips(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1,win32);
        }


        /// <summary>
        /// ��ʾDevExpress������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public static DialogResult ShowWarning(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,win32);
        }

        /// <summary>
        /// ��ʾ DevExpress��,�� ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public static DialogResult ShowYesNoAndError(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "������Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1,win32);
        }

        /// <summary>
        /// ��ʾ DevExpress��,�� ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="exec">�ص�����</param>
        public static void ShowYesNoAndError(string message, Action<DialogResult> exec, IWin32Window win32 = null)
        {
            DialogResult dr = ShowYesNoAndError(message,win32);
            if (dr == DialogResult.Yes)
            {
                exec(dr);
            }
        }

        /// <summary>
        /// ��ʾ DevExpress��,�� ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public static DialogResult ShowYesNoAndTips(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "��ʾ��Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1,win32);
        }

        /// <summary>
        /// ��ʾ DevExpress��,�� ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="exec">�ص�����</param>
        public static void ShowYesNoAndTips(string message, Action<DialogResult> exec, IWin32Window win32 = null)
        {
            DialogResult dr = ShowYesNoAndTips(message,win32);
            if (dr == DialogResult.Yes)
            {
                exec(dr);
            }
        }
        

        /// <summary>
        /// ��ʾ DevExpress��,�� ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public static DialogResult ShowYesNoAndWarning(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "������Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,win32);
        }

        /// <summary>
        /// ��ʾ DevExpress��,�� ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="exec">�ص�����</param>
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
            return ShowMessageBox(message, "ȷ����Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, win32);
        }

        public static DialogResult ShowOkCancelAndQuestion(string message, IWin32Window win32 = null)
        {
            return ShowMessageBox(message, "ȷ����Ϣ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, win32);
        }


        /// <summary>
        /// ��ʾ DevExpress ��Ϣ��
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="title">����</param>
        /// <param name="buttons">��ť</param>
        /// <param name="icon">ͼ��</param>
        /// <param name="defaultButton">Ĭ�ϰ�ť</param>
        private static DialogResult ShowMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, IWin32Window win32=null)
        {
            return XtraMessageBox.Show(win32,message, title, buttons, icon, defaultButton);
        }

    }
}