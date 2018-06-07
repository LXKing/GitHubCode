using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using XCI.Core;
using XCI.Extension;

namespace XCI.Helper
{
    /// <summary>
    /// Xml 文档操作管理
    /// </summary>
    public class XmlHelper
    {
        #region Xml序列化

        /// <summary>
        /// Xml序列化对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>序列化内容</returns>
        public static string XmlSerialize(object obj)
        {
            if (obj==null)
            {
                throw new ArgumentNullException("obj");
            }
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = false;
            settings.Indent = true;
            settings.IndentChars = "\t";
            settings.NewLineChars = Environment.NewLine;
            settings.ConformanceLevel = ConformanceLevel.Document;
            XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, obj, xmlns);
            }
            return Encoding.UTF8.GetString(stream.ToArray());
        }


        /// <summary>
        /// Xml序列化对象
        /// </summary>
        /// <param name="filePath">保存文件路径</param>
        /// <param name="obj">对象</param>
        public static void XmlSerializePath(string filePath, object obj)
        {
            try
            {
                FileHelper.CreateDirectoryByPath(filePath);
                var data = XmlSerialize(obj);
                
                File.WriteAllText(filePath, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Xml反序列化对象
        /// </summary>
        /// <param name="data">序列化内容</param>
        /// <param name="objType">对象类型</param>
        /// <returns>对象</returns>
        public static object XmlDeserialize(string data,Type objType)
        {
            XmlSerializer serializer = new XmlSerializer(objType);
            using (TextReader reader = new StringReader(data))
            {
                return serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Xml反序列化对象
        /// </summary>
        /// <param name="filePath">读取文件路径</param>
        /// <param name="objType">对象类型</param>
        /// <returns>对象</returns>
        public static object XmlDeserializePath(string filePath, Type objType)
        {
            try
            {
                FileHelper.CreateDirectoryByPath(filePath);
                if (File.Exists(filePath))
                {
                    string data = File.ReadAllText(filePath, Encoding.UTF8);
                    if (!string.IsNullOrEmpty(data))
                    {
                        return XmlDeserialize(data, objType);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Xml反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">序列化内容</param>
        /// <returns>对象</returns>
        public static T XmlDeserialize<T>(string data)
        {
            return (T)XmlDeserialize(data, typeof(T));
        }


        /// <summary>
        /// Xml反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="filePath">读取文件路径</param>
        /// <returns>对象</returns>
        public static T XmlDeserializePath<T>(string filePath)
        {
            return (T)XmlDeserializePath(filePath, typeof(T));
        }

        #endregion


        #region 其他方法

        /// <summary>
        /// 把Xml片段转为一个节点
        /// </summary>
        /// <param name="xmlFragment">Xml片段</param>
        public static XmlNode FragmentToNode(string xmlFragment)
        {
            XmlDocument xd = new XmlDocument();

            using (System.IO.StringReader sr = new StringReader(xmlFragment))
            {
                xd.Load(sr);
            }

            return xd.FirstChild;
        }


        /// <summary>
        /// 把Xml文档中的标示符变成转义字符
        /// </summary>
        /// <param name="xmlcontent">Xml内容</param>
        public static string EscapeXml(string xmlcontent)
        {
            if (xmlcontent.IndexOf('&') >= 0)
                xmlcontent = xmlcontent.Replace("&", "&amp;");

            if (xmlcontent.IndexOf('\'') >= 0)
                xmlcontent = xmlcontent.Replace("'", "&apos;");

            if (xmlcontent.IndexOf('\"') >= 0)
                xmlcontent = xmlcontent.Replace("\"", "&quot;");

            if (xmlcontent.IndexOf('<') >= 0)
                xmlcontent = xmlcontent.Replace("<", "&lt;");

            if (xmlcontent.IndexOf('>') >= 0)
                xmlcontent = xmlcontent.Replace(">", "&gt;");

            return xmlcontent;
        }


        /// <summary>
        /// 格式化Xml文档 使之美观
        /// </summary>
        /// <param name="xmlContent">Xml内容</param>
        public static string FormatXml(String xmlContent)
        {
            if (xmlContent == null || xmlContent.Trim().Length == 0)
                return string.Empty;

            string result;

            MemoryStream memStream = new MemoryStream();
            XmlTextWriter xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8);
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                // 加载Xml文档
                xmlDoc.LoadXml(xmlContent);

                xmlWriter.Formatting = Formatting.Indented;

                xmlDoc.WriteContentTo(xmlWriter);
                xmlWriter.Flush();
                memStream.Flush();

                memStream.Position = 0;

                StreamReader streamReader = new StreamReader(memStream);

                result = streamReader.ReadToEnd();

            }
            catch (Exception)
            {
                result = xmlContent;
            }
            finally
            {
                memStream.Close();
                xmlWriter.Close();
            }
            return result;
        }


        /// <summary>
        /// 使用样式表转换Xml文档
        /// </summary>
        /// <param name="inXml">Xml内容</param>
        /// <param name="styleSheet">样式表内容</param>
        /// <param name="outXml">输出Xml内容</param>
        /// <returns>如果成功返回Html字符串 如果转换失败抛出异常</returns>
        public static TextWriter TransformXml(TextReader inXml, TextReader styleSheet, TextWriter outXml)
        {
            if (null == inXml || null == styleSheet)
                return outXml;
            Guard.IsNotNull(outXml, "输出Xml对象不能为空");

            try
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                XsltSettings settings = new XsltSettings(false, false);
                using (XmlReader sheetReader = XmlReader.Create(styleSheet))
                {
                    xslt.Load(sheetReader, settings, null);
                }

                using (XmlReader inReader = XmlReader.Create(inXml))
                {
                    xslt.Transform(inReader, null, outXml);
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("转换失败", e);
            }
            return outXml;
        }


        /// <summary>
        /// 使用样式表转换Xml文档
        /// </summary>
        /// <param name="xmlContent">Xml内容</param>
        /// <param name="xslPath">样式表路径</param>
        /// <returns>如果成功返回Html字符串 如果转换失败返回空</returns>
        public static string TransformXml(string xmlContent, string xslPath)
        {
            if (string.IsNullOrEmpty(xmlContent) || !File.Exists(xslPath))
                return string.Empty;

            string rc;
            try
            {
                using (TextReader styleSheet = new StreamReader(new FileStream(
                  xslPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    TextReader inXml = new StringReader(xmlContent);
                    TextWriter outXml = new StringWriter();

                    TransformXml(inXml, styleSheet, outXml);

                    rc = outXml.ToString();
                }
            }
            catch (Exception)
            {
                rc = string.Empty;
            }
            return rc;
        }

        #endregion


        #region 读取AppSetting

        /// <summary>
        /// 读取appSettings配置
        /// </summary>
        /// <param name="key">键名</param>
        public static string GetAppConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 读取appSettings配置
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="defaultValue">如果找不到 返回的默认值</param>
        public static string GetAppConfig(string key,string defaultValue)
        {
            var v = GetAppConfig(key);
            if (string.IsNullOrEmpty(v))
            {
                return defaultValue;
            }
            return v;
        }
        #endregion


        #region 实例方法

        /// <summary>
        /// 默认构造
        /// </summary>
        public XmlHelper()
        {

        }


        /// <summary>
        /// 使用Xml文件路径构造
        /// </summary>
        /// <param name="xmlPath">Xml文件路径</param>
        public XmlHelper(string xmlPath)
        {
            FilePath = xmlPath;
        }


        private XmlDocument _doc;
        /// <summary>
        /// 当前Xml文档
        /// </summary>
        private XmlDocument Doc
        {
            get
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    throw new ArgumentException("Xml文件路径不能为空");
                }
                return _doc ?? (_doc = LoadXmlDocument());
            }
            set { _doc = value; }
        }


        /// <summary>
        /// 当前Xml文档路径
        /// </summary>
        public string FilePath { get; set; }


        private bool _isSave = true;
        /// <summary>
        /// 是否立即保存文档
        /// </summary>
        private bool IsSave
        {
            get { return _isSave; }
            set { _isSave = value; }
        }


        /// <summary>
        /// 加载Xml文档
        /// </summary>
        /// <returns>Xml 文档</returns>
        public XmlDocument LoadXmlDocument()
        {
            Doc = new XmlDocument();
            try
            {
                Doc.Load(FilePath);
            }
            catch (XmlException ex)
            {
                throw ex;
            }
            return Doc;
        }


        /// <summary>
        /// 创建Xml文档
        /// </summary>
        /// <param name="rootName">根节点名称 如果为空不创建根节点</param>
        public bool CreateXmlDocument(string rootName)
        {
            try
            {
                Doc = new XmlDocument();
                XmlDeclaration xmldecl = Doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                Doc.AppendChild(xmldecl);

                if (!string.IsNullOrEmpty(rootName))
                {
                    var rootNode = Doc.CreateElement(string.Empty, rootName, string.Empty);
                    Doc.AppendChild(rootNode);
                }

                SaveXmlDocument();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 保存Xml文档
        /// </summary>
        public void SaveXmlDocument()
        {
            try
            {
                if (IsSave)
                {
                    Doc.Save(FilePath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 开始操作文档 操作文档时不自动保存文件
        /// </summary>
        public void BeginUpdate()
        {
            IsSave = false;
        }


        /// <summary>
        /// 结束操作文档
        /// </summary>
        public void EndUpdate()
        {
            IsSave = true;
            SaveXmlDocument();
        }


        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeValue">节点内容</param>
        /// <param name="fatherNodeXPath">父节点Path</param>
        /// <param name="attrs">属性集合</param>
        /// <returns>返回新建的节点</returns>
        public XmlNode InsertNode(string nodeName, string nodeValue, string fatherNodeXPath, Dictionary<string, string> attrs)
        {
            try
            {
                XmlNode root = Doc.SelectSingleNode(fatherNodeXPath);
                if (root != null)
                {
                    var xmlelem = Doc.CreateElement(nodeName);
                    if (!string.IsNullOrEmpty(nodeValue))
                    {
                        xmlelem.InnerText = nodeValue;
                    }
                    
                    if (attrs != null)
                    {
                        attrs.ForEach(p => SetAttributeValue(xmlelem, p.Key, p.Value));
                    }

                    root.AppendChild(xmlelem);
                    SaveXmlDocument();
                    return xmlelem;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// 移除当前节点的所有子节点和/或属性
        /// </summary>
        /// <param name="nodeXPath">节点Path</param>
        public bool DeleteNode(string nodeXPath)
        {
            try
            {
                XmlNodeList nodeList = Doc.SelectNodes(nodeXPath);
                if (nodeList != null)
                {
                    foreach (XmlNode node in nodeList)
                    {
                        if (node.ParentNode != null)
                        {
                            node.ParentNode.RemoveChild(node);
                        }
                        else
                        {
                            node.RemoveAll();
                        }
                        SaveXmlDocument();
                    }
                    return true;
                }
                return false;
            }
            catch (XmlException ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="xpath">搜索表达式</param>
        public XmlNode GetNode(string xpath)
        {
            return Doc.SelectSingleNode(xpath);
        }


        /// <summary>
        /// 获取节点列表
        /// </summary>
        /// <param name="xpath">搜索表达式</param>
        public XmlNodeList GetNodeList(string xpath)
        {
            return Doc.SelectNodes(xpath);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attrName">属性名称</param>
        /// <param name="attrValue">属性值</param>
        public void SetAttributeValue(XmlElement node, string attrName, string attrValue)
        {
            node.SetAttribute(attrName, attrValue);
        }


        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attrName">属性名称</param>
        /// <returns>属性值</returns>
        public string GetAttributeValue(XmlElement node, string attrName)
        {
            return node.GetAttribute(attrName);
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attrName">属性名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>属性值</returns>
        public string GetAttributeValue(XmlElement node, string attrName, string defaultValue)
        {
            string value = GetAttributeValue(node, attrName);
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            return value;
        }

        #endregion

    }
}