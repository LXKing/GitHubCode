using System;
using DevExpress.XtraEditors;

namespace XCI.WinUtility
{
    /// <summary>
    /// 验证规则
    /// </summary>
    public class ValidateRule
    {
        private bool _isFocusControl = true;

        /// <summary>
        /// 是否给控件焦点
        /// </summary>
        public bool IsFocusControl
        {
            get { return _isFocusControl; }
            set { _isFocusControl = value; }
        }

        /// <summary>
        /// 验证消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 验证控件
        /// </summary>
        public BaseEdit Control { get; set; }

        /// <summary>
        /// 验证回调
        /// </summary>
        public Func<bool> ValidateFun { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns>成功返回true</returns>
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