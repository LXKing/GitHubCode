using RSAKeyFormatConvert;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //RSAKeyFormatConvert.RSAKeyFormatConverter.ConvertKeyPemToXml();
            var key1 = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"primary.txt");
            var privateKeyText = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"primary.txt");//.Replace("\n", "").Replace("-----END PRIVATE KEY-----", "").Replace("-----BEGIN PRIVATE KEY-----", "");
            //var r = new RSACrypto(privateKeyText);
            //var s = r.Encrypt("4716439d5ab0f4b82d526162e8a0c50a40398eaca5adf2fe6161aad8cb52b862");
            var xml = RSAKeyConvert.RSAPrivateKeyJava2DotNet(privateKeyText);
            var key2=RSAKeyConvert.RSAPrivateKeyDotNet2Java(xml);

            var data="4716439d5ab0f4b82d526162e8a0c50a40398eaca5adf2fe6161aad8cb52b862";
            var v = new RSACryption_New().RSAEncryptByPrivateKey(xml, data);
            var aa = "DNyn3vyHO2dKzN6pu1oE3VYg0DKkdUe1bySKwLEJ47mUT1M/AyDl+pOkXxCSM3pWNh+mMdf5NhSgoWWNw8AFfRsIbaoBAyaXcYj5UpnZcpOfxQ47xiAofq9kLQKAk9spuYbZgSooIzRIyi4qhLWkdqqFntCgDlXklLVBjgoRkME=";
            var vv= new RSACryption_New().RSADecryptByPrivateKey(xml, aa);

            if(vv==data)
            {
                Console.WriteLine("验签正确!");
            }
            else
            {
                Console.WriteLine("验签失败!");
            }
            Console.Read();
        }
    }
}
