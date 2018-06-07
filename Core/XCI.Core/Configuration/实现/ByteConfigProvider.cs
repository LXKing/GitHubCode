using System;
using System.IO;
using XCI.Core;
namespace XCI.Component
{
    [XCIComponent(
        "二进制存储组件配置容器实现模块",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "1.0.0.50",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "",
        "XCI.Configuration.ConfigProviderLogo.png")]
    public class ByteConfigProvider : ConfigProviderBase
    {
        public ByteConfigProvider()
        {

        }

        public ByteConfigProvider(string fileName)
        {
            this.FileName = fileName;
        }

        public override void LoadConfig()
        {
            ConfigData.LoadDataFromByte(ConfigPath);
        }

        public override void SaveConfig()
        {
            ConfigData.SaveDataAsByte(ConfigPath);
        }
    }
}