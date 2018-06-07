using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 配置接口
    /// </summary>
    [XCIComponentDescription("组件配置组件", "配置组件")]
    public interface IConfigProvider:IManager
    {
        XCIList<ComponentEntity> ConfigData { get; }

        XCIList<ComponentEntity> GetData();

        void SetData(XCIList<ComponentEntity> configData);

        XCIList<ConfigEntity> GetConfig(string interfaceName);

        ConfigEntity GetConfig(string interfaceName, string key);
        
        void AddOrUpdateConfig(string interfaceName, ConfigEntity config);

        void DeleteConfig(string interfaceName, ConfigEntity config);

        void DeleteConfig(string interfaceName, string key);

        void CleanConfig(string interfaceName);

        void SaveConfig(string interfaceName, XCIList<ConfigEntity> configList);

        void CleanConfig();

        void LoadConfig();

        void SaveConfig();
    }
}