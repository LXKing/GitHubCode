using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCI.Core;
using XCI.Helper;

namespace XCI.Component
{
    /// <summary>
    /// 配置对象管理
    /// </summary>
    public static class ConfigFactory
    {
        static ConfigFactory()
        {
            Load();
        }

        private static readonly XCIList<ConfigEntity> ConfigData = new XCIList<ConfigEntity>();

        private static string _configPath;
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private static string ConfigPath
        {
            get
            {
                if (_configPath == null)
                {
                    var directoryPath = XmlHelper.GetAppConfig("ConfigDirectory", "Config");
                    _configPath = PathHelper.AddStartupPath(Path.Combine(directoryPath, "ConfigProvider.xml"));
                }
                return _configPath;
            }
        }

        private static IConfigProvider _current;
        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IConfigProvider Current
        {
            get
            {
                if (_current == null)
                {
                    var entity = ConfigData.First(p => p.IsDefault);
                    if (entity == null)
                    {
                        entity = ConfigData[0];
                    }
                    _current = (IConfigProvider)CreateInstance(entity);
                    _current.LoadConfig();
                }
                return _current;
            }
            set { _current = value; }
        }

        public static void LoadComponentConfig()
        {
            Current.LoadConfig();
        }

        public static void ResetDefault()
        {
            _current = null;
        }

        public static void SaveComponentConfig()
        {
            Current.SaveConfig();
        }

        public static void AddOrUpdate(ConfigEntity entity)
        {
            ConfigData.AddOrUpdate(entity);
        }

        public static void Delete(ConfigEntity entity)
        {
            ConfigData.AddOrUpdate(entity);
        }

        public static void Clean()
        {
            ConfigData.Clear();
        }

        public static ConfigEntity Get(string name)
        {
            return ConfigData.First(p => p.Name.Equals(name));
        }

        public static XCIList<ConfigEntity> GetList()
        {
            return ConfigData;
        }

        public static void Load()
        {
            ConfigData.LoadDataFromXml(ConfigPath);
            if (ConfigData.Count == 0)
            {
                ConfigEntity entity = new ConfigEntity();
                entity.Name = "Default";
                entity.IsDefault = true;
                entity.Provider = AssemblyHelper.GetTypeFullName(typeof(XmlConfigProvider));
                entity.ParamCollection = new XCIList<ComponentParamEntity>();
                entity.ParamCollection.Add(new ComponentParamEntity { Name = "FileName", Value = "ComponentConfig.xml" });
                ConfigData.AddOrUpdate(entity);
            }
        }

        public static void Save()
        {
            ConfigData.SaveDataAsXml(ConfigPath);
        }

        public static object CreateInstance(ConfigEntity config)
        {
            var type = Type.GetType(config.Provider, false, true);
            if (type != null)
            {
                var provider = ObjectHelper.CreateInstance(type);
                SetProviderProperty(provider, config);//设置属性
                return provider;
            }
            return null;
        }


        /// <summary>
        /// 设置实例对象属性
        /// </summary>
        /// <param name="instance">实例对象</param>
        /// <param name="config">配置对象</param>
        public static void SetProviderProperty(object instance, ConfigEntity config)
        {
            if (config.ParamCollection == null)
            {
                return;
            }
            var propertyList = new XCIList<XCIKeyValuePair<string, string>>();
            propertyList.AddRange(config.ParamCollection.Select(item => new XCIKeyValuePair<string, string>(item.Name, item.Value)));
            ObjectHelper.SetObjectBasicProperty(instance, propertyList);
        }

        /// <summary>
        /// 设置配置对象属性
        /// </summary>
        /// <param name="instance">实例对象</param>
        /// <param name="config">配置对象</param>
        public static void SetConfigProperty(object instance, ConfigEntity config)
        {
            if (config.ParamCollection == null)
            {
                return;
            }
            var propertyList = ObjectHelper.GetObjectBasicProperty(instance);
            foreach (var item in propertyList)
            {
                string name = item.Key;
                if(!config.ParamCollection.Contains(p=>p.Name.Equals(name)))
                {
                    continue;
                }
                ComponentParamEntity componentParamEntity = new ComponentParamEntity();
                componentParamEntity.Name = name;
                componentParamEntity.Value = item.Value;
                var index = config.ParamCollection.IndexOf(componentParamEntity);
                if (index == -1)
                {
                    config.ParamCollection.Add(componentParamEntity);
                }
                else
                {
                    config.ParamCollection[index].Name = componentParamEntity.Name;
                    config.ParamCollection[index].Value = componentParamEntity.Value;
                }
            }
        }
    }
}