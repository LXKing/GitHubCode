using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// 流管理
    /// </summary>
    public static class StreamHelper
    {
        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>字节数组</returns>
        public static byte[] Serialize(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }


        /// <summary>
        /// 序列化对象(二进制)
        /// </summary>
        /// <param name="data">要序列化的对象</param>
        /// <returns>数据</returns>
        public static byte[] Serialize(object data)
        {
            if (data == null) throw new ArgumentNullException("data");
            MemoryStream stream = new MemoryStream();
            Serialize(stream, data);
            byte[] bytes = stream.ToArray();
            return bytes;
        }

        /// <summary>
        /// 序列化对象(二进制)
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="data">数据</param>
        public static void Serialize(string path, object data)
        {
            using(FileStream fs = new FileStream(path, FileMode.Create))
            {
                Serialize(fs, data);
            }
        }

        /// <summary>
        /// 序列化对象(二进制)
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="data">要序列化的对象</param>
        /// <returns></returns>
        public static void Serialize(Stream fileStream,object data)
        {
            if (fileStream == null) throw new ArgumentNullException("fileStream");
            if (data == null) throw new ArgumentNullException("data");
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fileStream, data);
        }


        /// <summary>
        /// 反序列化对象(二进制)
        /// </summary>
        /// <param name="data">要反序列化的对象</param>
        /// <returns>对象</returns>
        public static object Deserialize(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(data);
            object obj = bf.Deserialize(ms);
            return obj;
        }

        public static object Deserialize(string path)
        {
            object result = null;
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    result = Deserialize(fs);
                }
            }
            return result;
        }

        /// <summary>
        /// 反序列化对象(二进制)
        /// </summary>
        /// <param name="stream">流(请自己释放流)</param>
        /// <returns>对象</returns>
        public static object Deserialize(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            BinaryFormatter formatter = new BinaryFormatter();
            object obj = formatter.Deserialize(stream);
            return obj;
        }


        /// <summary>
        /// 将Stream 转化成 string
        /// </summary>
        /// <param name="stream">流</param>
        public static string ConvertStreamToString(Stream stream)
        {
            #region
            string strResult = "";
            StreamReader sr = new StreamReader(stream, Encoding.UTF8);

            Char[] read = new Char[256];
            // Read 256 charcters at a time.    
            int count = sr.Read(read, 0, 256);

            while (count > 0)
            {
                // Dump the 256 characters on a string and display the string onto the console.
                string str = new String(read, 0, count);
                strResult += str;
                count = sr.Read(read, 0, 256);
            }

            // 释放资源
            sr.Close();
            sr.Dispose();
            return strResult;
            #endregion
        }


        /// <summary>
        /// 复制Stream
        /// </summary>
        /// <param name="input">复制前Stream</param>
        /// <param name="output">复制后Stream</param>
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8192];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        /// <summary>
        /// 将二进制文件读入byte[]（如图片等）
        /// </summary>
        /// <param name="fileName">文件名与路径</param>
        public static byte[] ReadFile(string fileName)
        {
            FileStream pFileStream = null;
            byte[] pReadByte = new byte[0];

            try
            {
                pFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(pFileStream);
                r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开
                pReadByte = r.ReadBytes((int)r.BaseStream.Length);
                return pReadByte;
            }
            catch
            {
                return pReadByte;
            }
            finally
            {
                if (pFileStream != null)
                    pFileStream.Close();
            }
        }


        /// <summary>
        /// 写byte[]数据到文件（如图片等二进制数据）
        /// </summary>
        /// <param name="pReadByte">二进制数</param>
        /// <param name="fileName">文件名</param>
        /// <returns>成功返回True 否返回False</returns>
        public static bool WriteFile(byte[] pReadByte, string fileName)
        {
            FileStream pFileStream = null;
            try
            {
                pFileStream = new FileStream(fileName, FileMode.OpenOrCreate);
                pFileStream.Write(pReadByte, 0, pReadByte.Length);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (pFileStream != null)
                    pFileStream.Close();
            }
            return true;
        }

        

        /// <summary>
        /// 字节数组转换为内存流 
        /// </summary>
        /// <param name="bytes">字节数组</param>
        public static MemoryStream ToMemoryStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }


        /// <summary>
        /// 内存流转为字节数组
        /// </summary>
        /// <param name="ms">内存流</param>
        public static byte[] ToByteArray(MemoryStream ms)
        {
            return ms.ToArray();
        }
    }
}