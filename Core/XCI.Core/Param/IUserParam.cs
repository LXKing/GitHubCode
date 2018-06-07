using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 用户参数操作接口
    /// </summary>
    [XCIComponentDescription("用户参数组件", "系统组件")]
    public interface IUserParam : IManager
    {
        /// <summary>
        /// 获取或者设置用户ID
        /// </summary>
        int UserID { get; set; }

        /// <summary>
        /// 获取参数总数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取或设置指定参数名称的参数值
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>参数值</returns>
        string this[string key] { get; set; }

        /// <summary>
        /// 添加或者更新参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="value">参数值</param>
        void AddOrUpdate(string key, string value);

        /// <summary>
        /// 添加或者更新参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="comment">描述</param>
        /// <param name="category">分类</param>
        void AddOrUpdate(string key, string value, string comment, string category);

        /// <summary>
        /// 删除参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>成功返回true</returns>
        void Remove(string key);

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>参数值</returns>
        string Get(string key);

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="defaultValue">获取失败时的默认值</param>
        /// <returns>参数值</returns>
        string Get(string key, string defaultValue);

        /// <summary>
        /// 获取参数 如果没有则添加参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="defaultValue">参数默认值</param>
        /// <returns>参数值</returns>
        string GetOrAdd(string key, string defaultValue);

        /// <summary>
        /// 是否存在参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>存在返回true</returns>
        bool Contains(string key);

        /// <summary>
        /// 获取参数位置
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>参数索引号</returns>
        int IndexOf(string key);

        /// <summary>
        /// 清空参数
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <returns>参数列表</returns>
        XCIList<ParamEntity> GetParamList();
        
        /// <summary>
        /// 获取分类列表
        /// </summary>
        XCIList<string> GetCategory();

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