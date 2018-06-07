namespace XCI.Component
{
    /// <summary>
    /// 字符加密解密组件
    /// </summary>
    [XCIComponentDescription("字符加密解密组件", "系统组件")]
    public interface IEncrypt : IManager, IXCIComponentTest
    {
        /// <summary>
        /// 是否加密
        /// </summary>
        bool IsEncrypt { get; set; }
        
        /// <summary>
        /// 密钥
        /// </summary>
        string InternalKey { get; set; }
        
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns>密文</returns>
        string Encrypt(string plainText);
        
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <returns>明文</returns>
        string Decrypt(string encrypted);

        /// <summary>
        /// 比较明文和密文是否相同
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <param name="encrypted">密文</param>
        /// <returns>如果相同返回True</returns>
        bool IsMatch(string plainText, string encrypted);
    }
}