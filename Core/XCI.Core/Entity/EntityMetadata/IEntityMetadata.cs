using System;
using System.Collections.Generic;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 实体元数据管理组件
    /// </summary>
    [XCIComponentDescription("实体元数据管理组件", "系统组件")]
    public interface IEntityMetadata : IManager
    {
        /// <summary>
        /// 添加元数据
        /// </summary>
        /// <param name="metadata">元数据</param>
        void Add(EntityMetadata metadata);

        /// <summary>
        /// 编辑元数据
        /// </summary>
        /// <param name="metadata">元数据</param>
        void Update(EntityMetadata metadata);

        /// <summary>
        /// 删除元数据
        /// </summary>
        /// <param name="name">实体名称</param>
        void Delete(object name);
        
        /// <summary>
        /// 获取实体元数据
        /// </summary>
        /// <param name="name">实体名称</param>
        EntityMetadata Get(object name);

        /// <summary>
        /// 获取元数据列表
        /// </summary>
        XCIList<EntityMetadata> GetList();
    }
}