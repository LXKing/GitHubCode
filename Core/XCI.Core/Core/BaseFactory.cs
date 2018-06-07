using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using XCI.Component;
using XCI.Helper;

namespace XCI.Core
{
    /// <summary>
    /// 组件管理基类
    /// </summary>
    /// <typeparam name="I">接口类型</typeparam>
    public class BaseFactory<I> where I : class
    {
        protected Dictionary<string, I> InstanceContainer = new Dictionary<string, I>();

        /// <summary>
        /// 默认实例名称
        /// </summary>
        protected string DefaultName { get; set; }

        private string _interfacename;
        /// <summary>
        /// 接口名称
        /// </summary>
        public string InterfaceName
        {
            get
            {
                if (string.IsNullOrEmpty(_interfacename))
                {

                    _interfacename = AssemblyHelper.GetTypeFullName(typeof(I));

                }
                return _interfacename;
            }
        }

        /// <summary>
        /// 构造函数 自动读取配置
        /// </summary>
        internal BaseFactory()
        {
            LoadConfig();
        }

        private I _default;
        /// <summary>
        /// 当前默认实现对象
        /// </summary>
        public I Default
        {
            get
            {
                if (_default == null)
                {
                    if (string.IsNullOrEmpty(DefaultName))
                    {
                        _default = GetDefaultProvider();
                    }
                    else if (InstanceContainer.ContainsKey(DefaultName))
                    {
                        _default = InstanceContainer[DefaultName];
                    }
                    if (_default == null)
                    {
                        throw new SystemException(string.Format("接口{0} 没有定义实现类", InterfaceName));
                    }
                }
                return _default;
            }
            private set { _default = value; }
        }

        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public virtual I GetDefaultProvider()
        {
            return null;
        }


        /// <summary>
        /// 添加实现对象
        /// </summary>
        /// <param name="configs">配置列表</param>
        public void AddOrUpdate(XCIList<ConfigEntity> configs)
        {
            if (configs != null)
            {
                for (int index = 0; index < configs.Count; index++)
                {
                    ConfigEntity config = configs[index];
                    AddOrUpdate(config);
                }
            }
        }

        /// <summary>
        /// 添加实现对象
        /// </summary>
        /// <param name="config">配置对象</param>
        public I AddOrUpdate(ConfigEntity config)
        {
            return AddOrUpdate(null, config);
        }

        /// <summary>
        /// 添加或者更新实现对象
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="provider">实现对象</param>
        /// <param name="comment">描述</param>
        public I AddOrUpdate(string name, I provider, string comment = null)
        {
            ConfigEntity entity = new ConfigEntity();
            entity.Name = name;
            entity.Comment = comment;
            return AddOrUpdate(provider, entity);
        }

        /// <summary>
        /// 添加或者更新实现对象
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="providerString">实现对象字符串</param>
        /// <param name="comment">描述</param>
        /// <returns>返回创建的实现对象</returns>
        public I AddOrUpdate(string name, string providerString, string comment = null)
        {
            ConfigEntity entity = new ConfigEntity();
            entity.Name = name;
            entity.Comment = comment;
            entity.Provider = providerString;
            return AddOrUpdate(entity);
        }

        private I AddOrUpdate(I provider, ConfigEntity config)
        {
            if (provider == null)
            {
                provider = (I)ConfigFactory.CreateInstance(config);
            }
            if (string.IsNullOrEmpty(config.Provider))
            {
                config.Provider = AssemblyHelper.GetTypeFullName(provider.GetType());
            }
            string name = config.Name;
            if (InstanceContainer.Count == 0 || config.IsDefault)
            {
                DefaultName = name;
            }
            InstanceContainer[name] = provider;
            ConfigFactory.Current.AddOrUpdateConfig(InterfaceName, config);
            return provider;
        }


        /// <summary>
        /// 添加或者更新实现对象
        /// </summary>
        /// <param name="provider">实现对象</param>
        public void AddDefault(I provider)
        {
            const string name = "Default";
            AddOrUpdate(name, provider);
            SetDefault(name);
        }

        /// <summary>
        /// 添加或者更新默认实现对象
        /// </summary>
        /// <param name="name">默认名称</param>
        public void SetDefault(string name)
        {
            DefaultName = name;
            Default = null;
        }

        /// <summary>
        /// 是否存在指定名称的实例
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>存在返回true</returns>
        public bool Contains(string name)
        {
            return InstanceContainer.ContainsKey(name);
        }

        /// <summary>
        /// 获取命名的实例对象
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>实例对象</returns>
        public I Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Default;
            }
            if (InstanceContainer.ContainsKey(name))
            {
                return InstanceContainer[name];
            }
            return null;
        }

        /// <summary>
        /// 获取指定序号的实例对象
        /// </summary>
        /// <param name="index">索引号 从0开始</param>
        /// <returns>实例对象</returns>
        public I Get(int index)
        {
            var configList = ConfigFactory.Current.GetConfig(InterfaceName);
            if (configList != null && configList.Count < index)
            {
                string name = configList[index].Name;
                return InstanceContainer[name];
            }
            return null;
        }

        /// <summary>
        /// 获取新创建的命名实例对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public I GetNew(string name)
        {
            var entity = ConfigFactory.Current.GetConfig(InterfaceName, name);
            if (entity != null)
            {
                return (I)ConfigFactory.CreateInstance(entity);
            }
            return null;
        }


        /// <summary>
        /// 清空实现对象
        /// </summary>
        public void Clear()
        {
            ConfigFactory.Current.CleanConfig(InterfaceName);
            InstanceContainer.Clear();
            Default = null;
        }

        /// <summary>
        /// 删除命名的实现对象
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>如果成功找到并移除该元素，则为 true；否则为 false</returns>
        public void Remove(string name)
        {
            if (name.Equals(DefaultName))
            {
                Default = null;
            }
            ConfigFactory.Current.DeleteConfig(InterfaceName, name);
            InstanceContainer.Remove(name);
        }

        /// <summary>
        /// 多个实例对象同时执行函数
        /// </summary>
        /// <param name="action">执行函数</param>
        public void ForEachExec(Action<I> action)
        {
            foreach (KeyValuePair<string, I> item in InstanceContainer)
            {
                action(item.Value);
            }
        }

        public XCIList<I> GetInstanceList()
        {
            var list = new XCIList<I>();
            foreach (var item in InstanceContainer)
            {
                list.Add(item.Value);
            }
            return list;
        }

        /// <summary>
        /// 获取对象配置
        /// </summary>
        /// <param name="name">名称 为空使用默认值</param>
        public ConfigEntity GetConfig(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = DefaultName;
            }
            return ConfigFactory.Current.GetConfig(InterfaceName, name);
        }

        /// <summary>
        /// 获取配置列表
        /// </summary>
        public XCIList<ConfigEntity> GetConfigList()
        {
            var configList = ConfigFactory.Current.GetConfig(InterfaceName);

            if (configList != null)
            {
                foreach (ConfigEntity item in configList)
                {
                    string name = item.Name;
                    SetPropertyToConfig(name, item);
                }
            }
            return configList;
        }
        
        public void SetPropertyToConfig(string name, ConfigEntity config = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = DefaultName;
            }
            var instance = InstanceContainer[name];
            config = config ?? ConfigFactory.Current.GetConfig(InterfaceName, name);
            ConfigFactory.SetConfigProperty(instance, config);
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        public virtual void LoadConfig()
        {
            var list = ConfigFactory.Current.GetConfig(InterfaceName);
            if (list != null)
            {
                AddOrUpdate(list);
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public void SaveConfig()
        {
            var list = GetConfigList();
            ConfigFactory.Current.SaveConfig(InterfaceName, list);
        }
    }
}