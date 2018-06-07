using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Management;
using Microsoft.Win32;
using System.Text;
using System.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Asn1;

namespace RSAKeyFormatConvert
{
    public class RSACryption_New
    {
        /// <summary>

        /// 生成公私钥

        /// </summary>

        /// <param name="PrivateKeyPath"></param>

        /// <param name="PublicKeyPath"></param>

        public void RSAKey(string PrivateKeyPath, string PublicKeyPath)
        {

            try
            {

                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();

                this.CreatePrivateKeyXML(PrivateKeyPath, provider.ToXmlString(true));

                this.CreatePublicKeyXML(PublicKeyPath, provider.ToXmlString(false));

            }

            catch (Exception exception)
            {

                throw exception;

            }

        }

        /// <summary>

        /// 对原始数据进行MD5加密

        /// </summary>

        /// <param name="m_strSource">待加密数据</param>

        /// <returns>返回机密后的数据</returns>

        public string GetHash(string m_strSource)
        {

            HashAlgorithm algorithm = HashAlgorithm.Create("MD5");

            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(m_strSource);

            byte[] inArray = algorithm.ComputeHash(bytes);

            return Convert.ToBase64String(inArray);

        }

        /// <summary>

        /// RSA加密

        /// </summary>

        /// <param name="xmlPublicKey">公钥</param>

        /// <param name="m_strEncryptString">MD5加密后的数据</param>

        /// <returns>RSA公钥加密后的数据</returns>

        public string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {

            string str2;

            try
            {

                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();

                provider.FromXmlString(xmlPublicKey);

                byte[] bytes = new UnicodeEncoding().GetBytes(m_strEncryptString);

                str2 = Convert.ToBase64String(provider.Encrypt(bytes, false));

            }

            catch (Exception exception)
            {

                throw exception;

            }

            return str2;

        }

        /// <summary>

        /// RSA解密

        /// </summary>

        /// <param name="xmlPrivateKey">私钥</param>

        /// <param name="m_strDecryptString">待解密的数据</param>

        /// <returns>解密后的结果</returns>

        public string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {

            string str2;

            try
            {

                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();

                provider.FromXmlString(xmlPrivateKey);

                byte[] rgb = Convert.FromBase64String(m_strDecryptString);

                byte[] buffer2 = provider.Decrypt(rgb, false);

                str2 = new UnicodeEncoding().GetString(buffer2);

            }

            catch (Exception exception)
            {

                throw exception;

            }

            return str2;

        }

        /// <summary>

        /// 对MD5加密后的密文进行签名

        /// </summary>

        /// <param name="p_strKeyPrivate">私钥</param>

        /// <param name="m_strHashbyteSignature">MD5加密后的密文</param>

        /// <returns></returns>

        public string SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature)
        {

            byte[] rgbHash = Convert.FromBase64String(m_strHashbyteSignature);

            RSACryptoServiceProvider key = new RSACryptoServiceProvider();

            key.FromXmlString(p_strKeyPrivate);

            //RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);

            //formatter.SetHashAlgorithm("MD5");

            //byte[] inArray = formatter.CreateSignature(rgbHash);
            var bt = System.Text.Encoding.UTF8.GetBytes(m_strHashbyteSignature);
            var inArray =  key.Encrypt(bt, false);

            return Convert.ToBase64String(inArray);

        }

        /// <summary>

        /// 签名验证

        /// </summary>

        /// <param name="p_strKeyPublic">公钥</param>

        /// <param name="p_strHashbyteDeformatter">待验证的用户名</param>

        /// <param name="p_strDeformatterData">注册码</param>

        /// <returns></returns>

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {

            try
            {

                byte[] rgbHash = Convert.FromBase64String(p_strHashbyteDeformatter);

                RSACryptoServiceProvider key = new RSACryptoServiceProvider();

                key.FromXmlString(p_strKeyPublic);

                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);

                deformatter.SetHashAlgorithm("MD5");

                byte[] rgbSignature = Convert.FromBase64String(p_strDeformatterData);

                if (deformatter.VerifySignature(rgbHash, rgbSignature))
                {

                    return true;

                }

                return false;

            }

            catch
            {

                return false;

            }

        }

        ///  <summary>

        ///  读注册表中指定键的值

        ///  </summary>

        ///  <param name="key">键名</param>

        ///  <returns>返回键值</returns>

        private string ReadReg(string key)
        {

            string temp = "";

            try
            {

                RegistryKey myKey = Registry.LocalMachine;

                RegistryKey subKey = myKey.OpenSubKey(@"SOFTWARE/JX/Register");



                temp = subKey.GetValue(key).ToString();

                subKey.Close();

                myKey.Close();

                return temp;

            }

            catch (Exception)
            {

                throw;//可能没有此注册项;

            }



        }

        ///  <summary>

        ///  创建注册表中指定的键和值

        ///  </summary>

        ///  <param name="key">键名</param>

        ///  <param name="value">键值</param>

        private void WriteReg(string key, string value)
        {

            try
            {

                RegistryKey rootKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE/JX/Register");

                rootKey.SetValue(key, value);

                rootKey.Close();

            }

            catch (Exception)
            {

                throw;

            }

        }

        ///  <summary>

        ///  创建公钥文件

        ///  </summary>

        ///  <param name="path"></param>

        ///  <param name="publickey"></param>

        public void CreatePublicKeyXML(string path, string publickey)
        {

            try
            {

                FileStream publickeyxml = new FileStream(path, FileMode.Create);

                StreamWriter sw = new StreamWriter(publickeyxml);

                sw.WriteLine(publickey);

                sw.Close();

                publickeyxml.Close();

            }

            catch
            {

                throw;

            }

        }

        ///  <summary>

        ///  创建私钥文件

        ///  </summary>

        ///  <param name="path"></param>

        ///  <param name="privatekey"></param>

        public void CreatePrivateKeyXML(string path, string privatekey)
        {

            try
            {

                FileStream privatekeyxml = new FileStream(path, FileMode.Create);

                StreamWriter sw = new StreamWriter(privatekeyxml);

                sw.WriteLine(privatekey);

                sw.Close();

                privatekeyxml.Close();

            }

            catch
            {

                throw;

            }

        }

        ///  <summary>

        ///  读取公钥

        ///  </summary>

        ///  <param name="path"></param>

        ///  <returns></returns>

        public string ReadPublicKey(string path)
        {

            StreamReader reader = new StreamReader(path);

            string publickey = reader.ReadToEnd();

            reader.Close();

            return publickey;

        }

        ///  <summary>

        ///  读取私钥

        ///  </summary>

        ///  <param name="path"></param>

        ///  <returns></returns>

        public string ReadPrivateKey(string path)
        {

            StreamReader reader = new StreamReader(path);

            string privatekey = reader.ReadToEnd();

            reader.Close();

            return privatekey;

        }

        ///  <summary>

        ///  初始化注册表，程序运行时调用，在调用之前更新公钥xml

        ///  </summary>

        ///  <param name="path">公钥路径</param>

        public void InitialReg(string path)
        {

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE/JX/Register");

            Random ra = new Random();

            string publickey = this.ReadPublicKey(path);

            if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE/JX/Register").ValueCount <= 0)
            {

                this.WriteReg("RegisterRandom", ra.Next(1, 100000).ToString());

                this.WriteReg("RegisterPublicKey", publickey);

            }

            else
            {

                this.WriteReg("RegisterPublicKey", publickey);

            }

        }


        #region 私钥加密
        /// <summary>
        /// 用私钥给数据进行RSA加密
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="m_strEncryptString">待加密数据</param>
        /// <returns>加密后的数据（Base64）</returns>
        public string RSAEncryptByPrivateKey(string xmlPrivateKey, string strEncryptString)
        {
            //加载私钥
            RSACryptoServiceProvider privateRsa = new RSACryptoServiceProvider();
            privateRsa.FromXmlString(xmlPrivateKey);

            //转换密钥
            AsymmetricCipherKeyPair keyPair = DotNetUtilities.GetKeyPair(privateRsa);

            IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");// 参数与Java中加密解密的参数一致     
            //第一个参数为true表示加密，为false表示解密；第二个参数表示密钥
            c.Init(true, keyPair.Private);

            byte[] DataToEncrypt = Encoding.UTF8.GetBytes(strEncryptString);
            byte[] outBytes = c.DoFinal(DataToEncrypt);//加密
            string strBase64 = Convert.ToBase64String(outBytes);

            return strBase64;
        }

        /// <summary>
        /// 用私钥给数据进行RSA加密(PEM格式)
        /// </summary>
        /// <param name="pemPrivateKey">pem格式私钥</param>
        /// <param name="strEncryptString">加密字符串</param>
        /// <returns></returns>
        public string RSAEncryptByPrivateKeyPem(string pemPrivateKey, string strEncryptString)
        {
            var xmlPrivateKey = RSAKeyConvert.RSAPrivateKeyJava2DotNet(pemPrivateKey.Replace("\n", "").Replace("-----END PRIVATE KEY-----", "").Replace("-----BEGIN PRIVATE KEY-----", "")).Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END ENCRYPTED PRIVATE KEY-----", "");
            //加载私钥
            RSACryptoServiceProvider privateRsa = new RSACryptoServiceProvider();
            privateRsa.FromXmlString(xmlPrivateKey);

            //转换密钥
            AsymmetricCipherKeyPair keyPair = DotNetUtilities.GetKeyPair(privateRsa);

            IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");// 参数与Java中加密解密的参数一致     
            //第一个参数为true表示加密，为false表示解密；第二个参数表示密钥
            c.Init(true, keyPair.Private);

            byte[] DataToEncrypt = Encoding.UTF8.GetBytes(strEncryptString);
            byte[] outBytes = c.DoFinal(DataToEncrypt);//加密
            string strBase64 = Convert.ToBase64String(outBytes);

            return strBase64;
        }
        /// <summary>
        /// 私钥解密(XML格式)
        /// </summary>
        /// <param name="xmlPrivateKey"></param>
        /// <param name="strDncryptString"></param>
        /// <returns></returns>
        public string RSADecryptByPrivateKey(string xmlPrivateKey, string strDncryptString)
        {
            //加载私钥
            RSACryptoServiceProvider privateRsa = new RSACryptoServiceProvider();
            privateRsa.FromXmlString(xmlPrivateKey);

            //转换密钥
            AsymmetricCipherKeyPair keyPair = DotNetUtilities.GetKeyPair(privateRsa);

            IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");// 参数与Java中加密解密的参数一致     
            //第一个参数为true表示加密，为false表示解密；第二个参数表示密钥
            c.Init(false, keyPair.Private);


            byte[] DataToDecrypt = Convert.FromBase64String(strDncryptString);
            byte[] outBytes = c.DoFinal(DataToDecrypt);//解密
            string str = System.Text.Encoding.UTF8.GetString(outBytes);//Convert.ToBase64String(outBytes);

            return str;
        }
        /// <summary>
        /// 私钥解密(PEM格式)
        /// </summary>
        /// <param name="pemPrivateKey"></param>
        /// <param name="strDncryptString"></param>
        /// <returns></returns>
        public string RSADecryptByPrivateKeyPem(string pemPrivateKey, string strDncryptString)
        {
            var xmlPrivateKey = RSAKeyConvert.RSAPrivateKeyJava2DotNet(pemPrivateKey.Replace("\n", "").Replace("-----END PRIVATE KEY-----", "").Replace("-----BEGIN PRIVATE KEY-----", "")).Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END ENCRYPTED PRIVATE KEY-----", "");
            //加载私钥
            RSACryptoServiceProvider privateRsa = new RSACryptoServiceProvider();
            privateRsa.FromXmlString(xmlPrivateKey);

            //转换密钥
            AsymmetricCipherKeyPair keyPair = DotNetUtilities.GetKeyPair(privateRsa);

            IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");// 参数与Java中加密解密的参数一致     
            //第一个参数为true表示加密，为false表示解密；第二个参数表示密钥
            c.Init(false, keyPair.Private);


            byte[] DataToDecrypt = Convert.FromBase64String(strDncryptString);
            byte[] outBytes = c.DoFinal(DataToDecrypt);//解密
            string str = System.Text.Encoding.UTF8.GetString(outBytes);//Convert.ToBase64String(outBytes);

            return str;
        }
        #endregion

        #region 公钥解密
        /// <summary>
        /// 公钥解密
        /// </summary>
        /// <param name="pemPublicKeyPem"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string RSADecryptByPublicKeyPem( string pemPublicKeyPem, string data,Encoding encoding)
        {
            pemPublicKeyPem = pemPublicKeyPem.Replace("\r", "").Replace("\n", "").Replace("-----END PUBLIC KEY-----", "").Replace("-----BEGIN PUBLIC KEY-----", "");
            //RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(pemPublicKey));
            //非对称加密算法，加解密用  
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());
            //解密  
            try
            {
                engine.Init(false, GetPublicKeyParameter(pemPublicKeyPem));
                byte[] byteData = Convert.FromBase64String(data);
                var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length); 
                return encoding.GetString(ResultData);
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }

        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private AsymmetricKeyParameter GetPublicKeyParameter(string s)
        {
            s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            byte[] publicInfoByte = Convert.FromBase64String(s);
            Asn1Object pubKeyObj = Asn1Object.FromByteArray(publicInfoByte);//这里也可以从流中读取，从本地导入
            AsymmetricKeyParameter pubKey = PublicKeyFactory.CreateKey(publicInfoByte);
            return pubKey;
        }

        #endregion
    }
}