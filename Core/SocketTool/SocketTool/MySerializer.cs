using SocketTool.Core;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace SocketTool
{
	public class MySerializer
	{
		public static void Serialize(SocketInfo[] sis, string xmlFileName)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(SocketInfo[]));
			TextWriter textWriter = new StreamWriter(xmlFileName);
			xmlSerializer.Serialize(textWriter, sis);
		}
		public static SocketInfo[] DeSerialize(string xmlFileName)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(SocketInfo[]));
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(xmlFileName, FileMode.Open);
				XmlReader xmlReader = new XmlTextReader(fileStream);
				return (SocketInfo[])xmlSerializer.Deserialize(xmlReader);
			}
			catch (FileNotFoundException)
			{
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return null;
		}
		public static void Serialize<T>(T value, string xmlFileName)
		{
			if (value == null)
			{
				return;
			}
            #region Old
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = new UnicodeEncoding(false, false);
            xmlWriterSettings.Indent = false;
            xmlWriterSettings.OmitXmlDeclaration = false;
            FileStream fileStream = new FileStream(xmlFileName, FileMode.OpenOrCreate);
            xmlSerializer.Serialize(fileStream, value);
            fileStream.Close(); 
            #endregion
		}
		public static T Deserialize<T>(string xmlFileName)
		{
			if (string.IsNullOrEmpty(xmlFileName))
			{
				return default(T);
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			new XmlReaderSettings();
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(xmlFileName, FileMode.Open);
				XmlReader xmlReader = new XmlTextReader(fileStream);
				return (T)((object)xmlSerializer.Deserialize(xmlReader));
			}
			catch (FileNotFoundException)
			{
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return default(T);
		}
	}
}
