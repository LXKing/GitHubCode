using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using XCI.Core;
using XCI.Helper;

namespace XCI.Component
{
    /// <summary>
    /// 实体元数据Xml实现
    /// </summary>
    [XCIComponent(
        "实体元数据Xml存储实现",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "2.1.0.0",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "实体元数据Xml存储实现",
        "XCI.XCIComponent.ComponentLogo.png")]
    public class XmlEntityMetadata : IEntityMetadata
    {
        private XCIList<EntityMetadata> MetadataList = new XCIList<EntityMetadata>();

        private static string _metadatapath;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string MetadataPath
        {
            get
            {
                if (_metadatapath == null)
                {
                    _metadatapath = PathHelper.AddStartupPath(Path.Combine("Config", "EntityMetadata.xml"));
                }
                return _metadatapath;
            }
        }

        public XmlEntityMetadata()
        {
            MetadataList.LoadDataFromXml(MetadataPath);
        }

        /// <summary>
        /// 添加元数据
        /// </summary>
        /// <param name="metadata">元数据</param>
        public void Add(EntityMetadata metadata)
        {
            MetadataList.Add(metadata);
            Save();
        }

        /// <summary>
        /// 编辑元数据
        /// </summary>
        /// <param name="metadata">元数据</param>
        public void Update(EntityMetadata metadata)
        {
            MetadataList.Edit(metadata);
            Save();
        }

        /// <summary>
        /// 删除元数据
        /// </summary>
        /// <param name="name">实体名称</param>
        public void Delete(object name)
        {
            MetadataList.Remove(p => p.Name.Equals(name));
            Save();
        }

        /// <summary>
        /// 获取实体元数据
        /// </summary>
        /// <param name="name">实体名称</param>
        public EntityMetadata Get(object name)
        {
            return MetadataList.First(p => p.Name.Equals(name));
        }

        /// <summary>
        /// 获取元数据列表
        /// </summary>
        public XCIList<EntityMetadata> GetList()
        {
            return MetadataList;
        }

        public void Save()
        {
            MetadataList.SaveDataAsXml(MetadataPath);
        }
    }
}