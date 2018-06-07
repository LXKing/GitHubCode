using System;
using System.Collections.Generic;

namespace XCI.Core
{ 
    public static class Factory
    {
        //private static readonly Dictionary<string, XCIList<ConfigEntity>> ConfigContainer = new Dictionary<string, XCIList<ConfigEntity>>();
        //private static readonly Dictionary<string, IConfigProvider> ConfigProviderContainer = new Dictionary<string, IConfigProvider>();

        //public static void LoadConfig(string interfaceName)
        //{
        //    var configProviderList = ConfigFactory.Factory.GetInstanceList();
        //    foreach (var config in configProviderList)
        //    {
        //        XCIList<ConfigEntity> interfaceConfig = config.LoadConfig(interfaceName);
        //        if (interfaceConfig != null)
        //        {
        //            ConfigContainer[interfaceName] = interfaceConfig;
        //            ConfigProviderContainer[interfaceName] = config;
        //            break;
        //        }
        //    }
        //}

        //public static void SaveConfig(string interfaceName)
        //{
        //    var provider = GetConfigProvider(interfaceName);
        //    provider.SaveConfig(interfaceName, ConfigContainer[interfaceName]);
        //}
        
        //public static XCIList<ConfigEntity> GetConfigContainer(string interfaceName)
        //{
        //    if (!ConfigContainer.ContainsKey(interfaceName))
        //    {
        //        ConfigContainer.Add(interfaceName,new XCIList<ConfigEntity>());
        //    }
        //    return ConfigContainer[interfaceName];
        //}

        //public static IConfigProvider GetConfigProvider(string interfaceName)
        //{
        //    if (ConfigProviderContainer.ContainsKey(interfaceName))
        //    {
        //        return ConfigProviderContainer[interfaceName];
        //    }
        //    return ConfigFactory.Current;
        //}
    }
}