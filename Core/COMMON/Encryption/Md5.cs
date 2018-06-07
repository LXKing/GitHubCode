using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace COMMON
{
    /// <summary>
    ///md5 的摘要说明
    /// </summary>
    public class Md5
    {
        public Md5()
        {

        }
        #region 无密钥加密
        /// <summary>
        /// MD5 16位加密
        /// </summary>
        /// <param name="ConvertString">要加密的字符串</param>
        /// <returns></returns>
        public static string GetMd5Str16(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string str = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            return str.Replace("-", "").ToUpper();
        }
        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5Str32(string str)
        {
            string pwd = "";
            if (str.Length == 1)
            {
                str += "QX";
            }
            if (str.Length % 2 == 0)
            {
                pwd = GetMd5Str16(str.Substring(0, str.Length / 2)) + GetMd5Str16(str.Substring(str.Length / 2, str.Length / 2));
            }
            else
            {
                pwd = GetMd5Str16(str.Substring(0, str.Length / 2 + 1)) + GetMd5Str16(str.Substring(str.Length / 2 + 1, str.Length / 2));
            }
            return pwd;
        }
        #endregion
        #region 有密钥的加密
        // 创建Key
        public static string GenerateKey()
        {
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }
        /// <summary>
        /// MD5可逆加密
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string inputStr, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(inputStr);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();

        }
        /// <summary>
        /// MD5解密
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string MD5Decrypt(string inputStr, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[inputStr.Length / 2];
            for (int x = 0; x < inputStr.Length / 2; x++)
            {
                int i = (Convert.ToInt32(inputStr.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        #endregion
    }
}
