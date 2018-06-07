using System.IO;
using XCI.Component;
using XCI.Helper;

namespace XCI.Component
{
    [XCIComponent(
        "数据字典Xml实现模块",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "1.0.0.2",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "",
        "XCI.DataDictionary.DataDictionaryLogo.png")]
    public class XmlDataDictionary:DataDictionaryBase
    {
        public XmlDataDictionary()
        {
            Load();
        }

        public XmlDataDictionary(string fileName)
            : this()
        {
            this.FileName = fileName;
        }

        private string _configPath;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        protected string ConfigPath
        {
            get
            {
                if (_configPath == null)
                {
                    var directoryPath = XmlHelper.GetAppConfig("DataDirectory", "Data");
                    _configPath = PathHelper.AddStartupPath(Path.Combine(directoryPath, FileName));
                }
                return _configPath;
            }
            set { _configPath = value; }
        }

        private string _fileName;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public virtual string FileName
        {
            get { return _fileName ?? (_fileName = "DataDictionary.xml"); }
            set { _fileName = value; }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            DictionaryData.SaveDataAsXml(ConfigPath);
        }

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            DictionaryData.LoadDataFromXml(ConfigPath);
        }
    }
}