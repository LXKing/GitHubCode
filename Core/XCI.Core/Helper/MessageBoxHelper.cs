using System;
using System.Windows.Forms;

namespace XCI.Helper
{
    /// <summary>
    /// ��Ϣ���Ѱ�����
    /// </summary>
    public static class MessageBoxHelper
    {
        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public static DialogResult ShowError(string message)
        {
            return ShowMessageBox(message, "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }
        

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public static DialogResult ShowTips(string message)
        {
            return ShowMessageBox(message, "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }


        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public static DialogResult ShowWarning(string message)
        {
            return ShowMessageBox(message, "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }


        /// <summary>
        /// ��ʾ ��,�� ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public static DialogResult ShowYesNoAndError(string message)
        {
            return ShowMessageBox(message, "������Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }



        /// <summary>
        /// ��ʾ ��,�� ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="exec">�ǰ�ť�ص�����</param>
        public static void ShowYesNoAndError(string message, Action<DialogResult> exec)
        {
            DialogResult dr = ShowYesNoAndError(message);
            if (dr == DialogResult.Yes)
            {
                exec(dr);
            }
        }


        /// <summary>
        /// ��ʾ ��,�� ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public static DialogResult ShowYesNoAndTips(string message)
        {
            return ShowMessageBox(message, "��ʾ��Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }


        /// <summary>
        /// ��ʾ ��,�� ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="exec">�ǰ�ť�ص�����</param>
        public static void ShowYesNoAndTips(string message, Action<DialogResult> exec)
        {
            DialogResult dr = ShowYesNoAndTips(message);
            if (dr == DialogResult.Yes)
            {
                exec(dr);
            }
        }

        
        /// <summary>
        /// ��ʾ ��,�� ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public static DialogResult ShowYesNoAndWarning(string message)
        {
            return ShowMessageBox(message, "������Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }


        /// <summary>
        /// ��ʾ ��,�� ������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="exec">�ǰ�ť�ص�����</param>
        public static void ShowYesNoAndWarning(string message, Action<DialogResult> exec)
        {
            DialogResult dr = ShowYesNoAndWarning(message);
            if (dr == DialogResult.Yes)
            {
                exec(dr);
            }
        }
        

        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="title">����</param>
        /// <param name="buttons">��ť</param>
        /// <param name="icon">ͼ��</param>
        /// <param name="defaultButton">Ĭ�ϰ�ť</param>
        public static DialogResult ShowMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return MessageBox.Show(message, title, buttons, icon, defaultButton);
        }
        
    }
}