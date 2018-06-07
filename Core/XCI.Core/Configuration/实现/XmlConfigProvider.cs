using System;
using System.Diagnostics;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// Xml存储配置
    /// </summary>
    [XCIComponent(
        "Xml存储组件配置容器实现模块",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "1.0.0.50",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "",
        "XCI.Configuration.ConfigProviderLogo.png")]
    public class XmlConfigProvider : ConfigProviderBase
    {
        public XmlConfigProvider()
        {

        }

        public XmlConfigProvider(string fileName)
        {
            this.FileName = fileName;
        }

        public override void LoadConfig()
        {
            try
            {
                ConfigData.LoadDataFromXml(ConfigPath);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            

        }

        public override void SaveConfig()
        {
            ConfigData.RemoveAll(p => p.ProviderList == null || p.ProviderList.Count == 0);
            ConfigData.SaveDataAsXml(ConfigPath);
        }
    }
}