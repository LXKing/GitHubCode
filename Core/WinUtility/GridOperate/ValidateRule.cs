using System;
using DevExpress.XtraEditors;

namespace XCI.WinUtility
{
    /// <summary>
    /// ��֤����
    /// </summary>
    public class ValidateRule
    {
        private bool _isFocusControl = true;

        /// <summary>
        /// �Ƿ���ؼ�����
        /// </summary>
        public bool IsFocusControl
        {
            get { return _isFocusControl; }
            set { _isFocusControl = value; }
        }

        /// <summary>
        /// ��֤��Ϣ
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ��֤�ؼ�
        /// </summary>
        public BaseEdit Control { get; set; }

        /// <summary>
        /// ��֤�ص�
        /// </summary>
        public Func<bool> ValidateFun { get; set; }

        /// <summary>
        /// ��֤
        /// </summary>
        /// <returns>�ɹ�����true</returns>
        public bool Validate()
        {
            if (ValidateFun != null)
            {
                return ValidateFun();
            }
            return false;
        }
    }
}