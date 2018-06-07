using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 数据字典操作接口
    /// </summary>
    [XCIComponentDescription("数据字典组件", "系统组件")]
    public interface IDataDictionary : IManager
    {
        /// <summary>
        /// 获取数据字典总数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取或设置指定数据字典名称的字典列表
        /// </summary>
        /// <param name="key">数据字典名称</param>
        /// <returns>字典列表</returns>
        XCIList<DataDictionaryEntity> this[string key] { get; set; }

        /// <summary>
        /// 添加或者更新数据字典
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <param name="itemName">项名称</param>
        /// <param name="itemValue">项值</param>
        void AddOrUpdate(string name, string itemName, string itemValue);

        /// <summary>
        /// 添加或者更新数据字典
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <param name="itemName">项名称</param>
        /// <param name="itemValue">项值</param>
        /// <param name="comment">备注</param>
        void AddOrUpdate(string name, string itemName, string itemValue, string comment);

        /// <summary>
        /// 添加或者更新数据字典
        /// </summary>
        /// <param name="entity">字典对象</param>
        void AddOrUpdate(DataDictionaryEntity entity);

        /// <summary>
        /// 添加或者更新数据字典
        /// </summary>
        /// <param name="data">字典列表</param>
        void AddOrUpdate(XCIList<DataDictionaryEntity> data);

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <returns>成功返回true</returns>
        void Remove(string name);

        /// <summary>
        /// 删除数据字典项
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <param name="itemName">项名称</param>
        /// <returns>成功返回true</returns>
        void Remove(string name, string itemName);

        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <param name="name">字典名称</param>
        XCIList<DataDictionaryEntity> Get(string name);

        /// <summary>
        /// 获取数据字典 如果没有指定名称的字典 则返回默认指定的项名称和项值
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <param name="defaultItemName">项名称</param>
        /// <param name="defaultItemValue">项值</param>
        XCIList<DataDictionaryEntity> Get(string name, string defaultItemName, string defaultItemValue);

        /// <summary>
        /// 获取数据字典 如果没有则添加数据字典
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <param name="defaultItemName">项名称</param>
        /// <param name="defaultItemValue">项值</param>
        XCIList<DataDictionaryEntity> GetOrAdd(string name, string defaultItemName, string defaultItemValue);

        /// <summary>
        /// 是否存在数据字典
        /// </summary>
        /// <param name="name">数据字典名称</param>
        /// <returns>存在返回true</returns>
        bool Contains(string name);

        /// <summary>
        /// 是否存在数据字典项
        /// </summary>
        /// <param name="name">数据字典名称</param>
        /// <param name="itemName">项名称</param>
        /// <returns>存在返回true</returns>
        bool Contains(string name, string itemName);

        /// <summary>
        /// 获取数据字典位置
        /// </summary>
        /// <param name="name">数据字典名称</param>
        /// <returns>数据字典索引号</returns>
        int IndexOf(string name);

        /// <summary>
        /// 获取数据字典项位置
        /// </summary>
        /// <param name="name">数据字典名称</param>
        /// <param name="itemName">项名称</param>
        /// <returns>数据字典索引号</returns>
        int IndexOf(string name, string itemName);

        /// <summary>
        /// 清空数据字典
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取数据字典列表
        /// </summary>
        XCIList<DataDictionaryEntity> GetList();

        /// <summary>
        /// 获取名称列表
        /// </summary>
        XCIList<string> GetNameList();

        /// <summary>
        /// 保存
        /// </summary>
        void Save();

        /// <summary>
        /// 加载
        /// </summary>
        void Load();
    }
}