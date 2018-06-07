using System.IO;
using XCI.Core;
using XCI.Helper;

namespace XCI.Component
{
    public abstract class ConfigProviderBase : IConfigProvider
    {
        private string _configPath;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        protected string ConfigPath
        {
            get
            {
                if (_configPath==null)
                {
                    var directoryPath = XmlHelper.GetAppConfig("ConfigDirectory","Config");
                    _configPath = PathHelper.AddStartupPath(Path.Combine(directoryPath, FileName));
                }
                return _configPath;
            }
            set { _configPath = value; }
        }

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public virtual string FileName { get; set; }
        
        private XCIList<ComponentEntity> _configData;
        public XCIList<ComponentEntity> ConfigData
        {
            get
            {
                if (_configData==null)
                {
                    _configData = new XCIList<ComponentEntity>();
                }
                return _configData;
            }
            internal set { _configData = value; }
        }

        public XCIList<ComponentEntity> GetData()
        {
            return ConfigData;
        }

        public void SetData(XCIList<ComponentEntity> configData)
        {
            ConfigData = configData;
        }

        public ComponentEntity GetComponentEntity(string interfaceName)
        {
            var entity = ConfigData.First(p => p.InterfaceName.Equals(interfaceName));
            if (entity == null)
            {
                entity = new ComponentEntity();
                entity.InterfaceName = interfaceName;
                entity.ProviderList = new XCIList<ConfigEntity>();
                ConfigData.Add(entity);
            }
            return entity;
        }

        public XCIList<ConfigEntity> GetConfig(string interfaceName)
        {
            var entity = GetComponentEntity(interfaceName);
            return entity != null ? entity.ProviderList : null;
        }

        public ConfigEntity GetConfig(string interfaceName, string key)
        {
            var entity = GetComponentEntity(interfaceName);
            if (entity!=null)
            {
                return entity.ProviderList.First(p => p.Name.Equals(key));
            }
            return null;
        }

        public void AddOrUpdateConfig(string interfaceName, ConfigEntity config)
        {
            var entity = GetComponentEntity(interfaceName);
            entity.ProviderList.AddOrUpdate(config);
        }

        public void DeleteConfig(string interfaceName, ConfigEntity config)
        {
            var entity = GetComponentEntity(interfaceName);
            entity.ProviderList.Remove(config);
        }

        public void DeleteConfig(string interfaceName, string key)
        {
            var entity = GetComponentEntity(interfaceName);
            var configEntity = entity.ProviderList.First(p => p.Name.Equals(key));
            if (configEntity!=null)
            {
                DeleteConfig(interfaceName, configEntity);
            }
        }

        public void CleanConfig(string interfaceName)
        {
            var entity = GetComponentEntity(interfaceName);
            entity.ProviderList.Clear();
        }

        public void SaveConfig(string interfaceName, XCIList<ConfigEntity> configList)
        {
            var entity = GetComponentEntity(interfaceName);
            entity.ProviderList = configList;
            SaveConfig();
        }

        public void CleanConfig()
        {
            ConfigData.Clear();
        }
        
        public abstract void LoadConfig();

        public abstract void SaveConfig();
    }
}