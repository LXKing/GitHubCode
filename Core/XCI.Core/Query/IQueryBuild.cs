using System.Data;
using System.Data.Common;

namespace XCI.Component
{
    /// <summary>
    /// 脚本生成组件
    /// </summary>
    [XCIComponentDescription("脚本生成组件", "系统组件")]
    public interface IQueryBuild : IManager
    {
        /// <summary>
        /// 生成Insert语句
        /// </summary>
        /// <param name="query">查询对象</param>
        DbCommand BuildInsert(Query query);

        /// <summary>
        /// 生成Update语句
        /// </summary>
        /// <param name="query">查询对象</param>
        DbCommand BuildUpdate(Query query);

        /// <summary>
        /// 生成Delete语句
        /// </summary>
        /// <param name="query">查询对象</param>
        DbCommand BuildDelete(Query query);

        /// <summary>
        /// 生成Select语句
        /// </summary>
        /// <param name="query">查询对象</param>
        DbCommand BuildSelect(Query query);

        /// <summary>
        /// 生成取前几条记录脚本
        /// </summary>
        /// <param name="query">查询对象</param>
        string BuildTop(Query query);

        /// <summary>
        /// 生成取最后一次插入的自增ID
        /// </summary>
        /// <param name="query">查询对象</param>
        string BuildLastAutoIncrementID(Query query);

    }
}