using System.IO;
using XCI.Helper;

namespace XCI.Component
{
    [XCIComponent(
        "Xml存储系统参数实现模块",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "1.0.0.2",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "系统参数Xml实现模块",
        "XCI.Param.ParamLogo.png")]
    public class XmlParam : ParamBase
    {
        public XmlParam()
        {
            Load();
        }

        public XmlParam(string fileName)
        {
            this.FileName = fileName;
            Load();
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
            get { return _fileName ?? (_fileName = "Param.xml"); }
            set { _fileName = value; }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            ParamData.SaveDataAsXml(ConfigPath);
        }

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            ParamData.LoadDataFromXml(ConfigPath);
        }
    }
}