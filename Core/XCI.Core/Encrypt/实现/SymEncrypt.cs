using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

namespace XCI.Component
{
    /// <summary>
    /// 字符串加密对称算法实现
    /// </summary>
    [XCIComponent(
        "对称加密模块",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "2.1.0.0",
        "©Copyright2011 lvyanyang2012@gmail.com", 
        "对称加密TripleDESCryptoServiceProvider实现模块",
        "XCI.Encrypt.EncryptLogo.png")]
    public class SymEncrypt : EncryptBase, IEncrypt
    {
        private string _internalKey = "!@#$TFFhanjianpdfdjk)(*&88330921NVJ878";
        /// <summary>
        /// 密钥
        /// </summary>
        public override string InternalKey
        {
            get { return _internalKey; }
            set { _internalKey = value; }
        }
        /// <summary>
        /// 对称算法
        /// </summary>
        protected SymmetricAlgorithm algorithm;
        
        /// <summary>
        /// 默认构造
        /// </summary>
        public SymEncrypt()
        {
            algorithm = new TripleDESCryptoServiceProvider();
        }
        
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns>密文</returns>
        public override string Encrypt(string plaintext)
        {
            if (!IsEncrypt) return plaintext;

            string base64Text = Encrypt(algorithm, plaintext, InternalKey);
            return base64Text;
        }

        /// <summary>
        /// 使用密钥加密
        /// </summary>
        /// <param name="algorithm">对称算法</param>
        /// <param name="plaintext">明文</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        private static string Encrypt(SymmetricAlgorithm algorithm, string plaintext, string key)
        {
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            algorithm.Key = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(key));
            algorithm.Mode = CipherMode.ECB;
            ICryptoTransform transformer = algorithm.CreateEncryptor();
            byte[] Buffer = Encoding.UTF8.GetBytes(plaintext);
            return Convert.ToBase64String(transformer.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <returns>明文</returns>
        public override string Decrypt(string encrypted)
        {
            if (!IsEncrypt) return encrypted;
            
            string plaintext = Decrypt(algorithm, encrypted, InternalKey);
            return plaintext;
        }


        /// <summary>
        /// 使用密钥解密
        /// </summary>
        /// <param name="algorithm">对称算法</param>
        /// <param name="base64Text">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        private static string Decrypt(SymmetricAlgorithm algorithm, string base64Text, string key)
        {
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            algorithm.Key = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(key));
            algorithm.Mode = CipherMode.ECB;
            ICryptoTransform transformer = algorithm.CreateDecryptor();
            byte[] Buffer = Convert.FromBase64String(base64Text);
            return Encoding.UTF8.GetString(transformer.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
    }
}
