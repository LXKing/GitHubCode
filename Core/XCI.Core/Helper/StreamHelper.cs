using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// ������
    /// </summary>
    public static class StreamHelper
    {
        /// <summary>
        /// �� Stream ת�� byte[]
        /// </summary>
        /// <param name="stream">��</param>
        /// <returns>�ֽ�����</returns>
        public static byte[] Serialize(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // ���õ�ǰ����λ��Ϊ���Ŀ�ʼ
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }


        /// <summary>
        /// ���л�����(������)
        /// </summary>
        /// <param name="data">Ҫ���л��Ķ���</param>
        /// <returns>����</returns>
        public static byte[] Serialize(object data)
        {
            if (data == null) throw new ArgumentNullException("data");
            MemoryStream stream = new MemoryStream();
            Serialize(stream, data);
            byte[] bytes = stream.ToArray();
            return bytes;
        }

        /// <summary>
        /// ���л�����(������)
        /// </summary>
        /// <param name="path">�ļ�·��</param>
        /// <param name="data">����</param>
        public static void Serialize(string path, object data)
        {
            using(FileStream fs = new FileStream(path, FileMode.Create))
            {
                Serialize(fs, data);
            }
        }

        /// <summary>
        /// ���л�����(������)
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="data">Ҫ���л��Ķ���</param>
        /// <returns></returns>
        public static void Serialize(Stream fileStream,object data)
        {
            if (fileStream == null) throw new ArgumentNullException("fileStream");
            if (data == null) throw new ArgumentNullException("data");
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fileStream, data);
        }


        /// <summary>
        /// �����л�����(������)
        /// </summary>
        /// <param name="data">Ҫ�����л��Ķ���</param>
        /// <returns>����</returns>
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
        /// �����л�����(������)
        /// </summary>
        /// <param name="stream">��(���Լ��ͷ���)</param>
        /// <returns>����</returns>
        public static object Deserialize(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            BinaryFormatter formatter = new BinaryFormatter();
            object obj = formatter.Deserialize(stream);
            return obj;
        }


        /// <summary>
        /// ��Stream ת���� string
        /// </summary>
        /// <param name="stream">��</param>
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

            // �ͷ���Դ
            sr.Close();
            sr.Dispose();
            return strResult;
            #endregion
        }


        /// <summary>
        /// ����Stream
        /// </summary>
        /// <param name="input">����ǰStream</param>
        /// <param name="output">���ƺ�Stream</param>
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
        /// ���������ļ�����byte[]����ͼƬ�ȣ�
        /// </summary>
        /// <param name="fileName">�ļ�����·��</param>
        public static byte[] ReadFile(string fileName)
        {
            FileStream pFileStream = null;
            byte[] pReadByte = new byte[0];

            try
            {
                pFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(pFileStream);
                r.BaseStream.Seek(0, SeekOrigin.Begin);    //���ļ�ָ�����õ��ļ���
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
        /// дbyte[]���ݵ��ļ�����ͼƬ�ȶ��������ݣ�
        /// </summary>
        /// <param name="pReadByte">��������</param>
        /// <param name="fileName">�ļ���</param>
        /// <returns>�ɹ�����True �񷵻�False</returns>
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
        /// �ֽ�����ת��Ϊ�ڴ��� 
        /// </summary>
        /// <param name="bytes">�ֽ�����</param>
        public static MemoryStream ToMemoryStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }


        /// <summary>
        /// �ڴ���תΪ�ֽ�����
        /// </summary>
        /// <param name="ms">�ڴ���</param>
        public static byte[] ToByteArray(MemoryStream ms)
        {
            return ms.ToArray();
        }
    }
}