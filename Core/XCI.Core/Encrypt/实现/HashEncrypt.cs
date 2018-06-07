using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

namespace XCI.Component
{
    /// <summary>
    /// 字符串加密哈希算法(MD5)实现
    /// </summary>
    [XCIComponent(
        "MD5加密模块",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "2.1.0.0",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "MD5加密MD5CryptoServiceProvider实现模块",
        "XCI.Encrypt.EncryptLogo.png")]
    public class HashEncrypt : EncryptBase,IEncrypt
    {
        /// <summary>
        /// 哈希算法
        /// </summary>
        protected HashAlgorithm algorithm;

        /// <summary>
        /// 默认构造
        /// </summary>
        public HashEncrypt()
        {
            algorithm = new MD5CryptoServiceProvider();
        }
        
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns>密文</returns>
        public override string Encrypt(string plaintext)
        {
            if (!IsEncrypt) return plaintext;
            
            string base64Text = Encrypt(algorithm, plaintext);
            return base64Text;
        }


        /// <summary>
        /// 哈希加密
        /// </summary>
        /// <param name="hashAlgorithm">哈希算法</param>
        /// <param name="plaintext">明文</param>
        /// <returns>密文</returns>
        private static string Encrypt(HashAlgorithm hashAlgorithm, string plaintext)
        {
            string hexResult = string.Empty;
            string[] tabStringHex = new string[16];

            byte[] data = Encoding.ASCII.GetBytes(plaintext);
            byte[] result = hashAlgorithm.ComputeHash(data);
            for (int i = 0; i < result.Length; i++)
            {
                tabStringHex[i] = (result[i]).ToString("x");
                hexResult += tabStringHex[i];
            }
            return hexResult;
        }



        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <returns>明文</returns>
        public override string Decrypt(string encrypted)
        {
            throw new NotSupportedException("Hash算法无法解密");
        }
    }
}
