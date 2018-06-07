namespace XCI.Component
{
    /// <summary>
    /// 加密解密基类
    /// </summary>
    public abstract class EncryptBase : IEncrypt
    {
        private bool _isEncrypt = true;
        /// <summary>
        /// 是否加密
        /// </summary>
        public virtual bool IsEncrypt
        {
            get { return _isEncrypt; }
            set { _isEncrypt = value; }
        }

        /// <summary>
        /// 密钥
        /// </summary>
        public virtual string InternalKey { get; set; }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns>密文</returns>
        public abstract string Encrypt(string plaintext);


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <returns>明文</returns>
        public abstract string Decrypt(string encrypted);

        /// <summary>
        /// 比较明文和密文是否相同
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <param name="encrypted">密文</param>
        /// <returns>如果相同返回True</returns>
        public virtual bool IsMatch(string plainText, string encrypted)
        {
            string encrypted2 = Encrypt(plainText);
            return System.String.CompareOrdinal(encrypted, encrypted2) == 0;
        }

        /// <summary>
        /// 显示测试窗体
        /// </summary>
        public void ShowTestForm(string configName)
        {
            var form = new frmEncryptTest(configName);
            form.ShowDialog();
            form.Dispose();
        }
    }
}