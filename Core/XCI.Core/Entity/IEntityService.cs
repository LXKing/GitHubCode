using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 实体操作接口
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public interface IEntityService<T> where T : EntityBase
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        IDatabase Database{get;}
        /// <summary>
        /// 创建查询对象 如果已经存在查询对象则设置数据属性后返回
        /// </summary>
        /// <param name="alreadyQuery">已存在的查询对象</param>
        Query<T> CreateQuery(Query alreadyQuery = null);

        /// <summary>
        /// 数据表转为实体列表
        /// </summary>
        /// <param name="table">数据表</param>
        XCIList<T> ConvertToEntityList(DataTable table);

        /// <summary>
        /// 映射数据行 把DataTable一行映射为一个实体对象
        /// </summary>
        /// <param name="dataRow">数据行</param>
        T MapToEntity(DataRow dataRow);

        /// <summary>
        /// 映射实体 把实体对象转为DataTable一行
        /// </summary>
        /// <param name="table">数据表对象 主要用于创建行对象</param>
        /// <param name="entity">实体对象</param>
        /// <returns>数据行</returns>
        DataRow MapToDataRow(DataTable table, T entity);

        /// <summary>
        /// 更新数据行
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="entity">实体对象</param>
        void UpdateDataRow(DataRow row, T entity);

        /// <summary>
        /// 测试对象是否存在
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns>如果存在返回True</returns>
        bool Exists(Query query);

        /// <summary>
        /// 测试对象是否存在
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="propertyNames">检测的属性名称</param>
        /// <returns>如果存在返回True</returns>
        bool Exists(T entity, params string[] propertyNames);

        /// <summary>
        /// 测试对象是否存在
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="exps">检测的属性名称表达式</param>
        /// <returns>如果存在返回True</returns>
        bool Exists(T entity, params Expression<Func<T, object>>[] exps);

        /// <summary>
        /// 添加实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Create(T entity);

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Update(T entity);

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="query">查询对象</param>
        void Update(Query query);

        /// <summary>
        /// 检测实体版本
        /// </summary>
        /// <param name="entity">实体对象</param>
        void CheckVersion(T entity);

        /// <summary>
        /// 批量更新字段 
        /// </summary>
        /// <param name="fieldDic">改变的数据字典 (主键 (列名 列值))</param>
        void BatchUpdateField(Dictionary<object, Dictionary<string, object>> fieldDic);

        /// <summary>
        /// 批量更新字段 
        /// </summary>
        /// <param name="fieldList">改变的数据 (主键 列名 列值)</param>
        void BatchUpdateField(IList<Tuple<object,string,object>> fieldList);

        /// <summary>
        /// 删除实体对象(如果存在删除字段 则更新删除字段值)
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Delete(T entity);

        /// <summary>
        /// 删除实体对象 根据实体主键
        /// </summary>
        /// <param name="ID">实体主键</param>
        void Delete(int ID);

        /// <summary>
        /// 删除实体对象 根据实体主键数组
        /// </summary>
        /// <param name="IDs">实体主键数组</param>
        void Delete(int[] IDs);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="query">查询对象</param>
        void Delete(Query query);

        /// <summary>
        /// 删除全部实体
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="ID">实体ID</param>
        T Get(int ID);

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="query">查询对象</param>
        T Get(Query query);

        /// <summary>
        /// 获取数据表(指定是否包含禁用或者删除数据)
        /// </summary>
        /// <param name="isContainValid">是否包含禁用数据</param>
        /// <param name="isContainDelete">是否包含删除数据</param>
        /// <param name="query">查询对象</param>
        DataTable GetTable(bool isContainValid, bool isContainDelete, Query query);

        /// <summary>
        /// 获取数据表根据指定的查询对象(如果要获取全部数据 查询对象传空即可)
        /// </summary>
        /// <param name="query">查询对象</param>
        DataTable GetTable(Query query);

        /// <summary>
        /// 获取有效数据表(只包含没有删除 没有禁用的记录)
        /// </summary>
        DataTable GetTable();

        /// <summary>
        /// 获取删除的数据表
        /// </summary>
        DataTable GetDeleteTable();

        /// <summary>
        /// 获取数据列表(指定是否包含禁用或者删除数据)
        /// </summary>
        /// <param name="isContainValid">是否包含禁用数据</param>
        /// <param name="isContainDelete">是否包含删除数据</param>
        /// <param name="query">查询对象</param>
        XCIList<T> GetList(bool isContainValid, bool isContainDelete, Query query);

        /// <summary>
        /// 获取数据列表根据指定的查询对象(如果要获取全部数据 查询对象传空即可)
        /// </summary>
        /// <param name="query">查询对象</param>
        XCIList<T> GetList(Query query);

        /// <summary>
        /// 获取有效数据列表(只包含没有删除 没有禁用的记录)
        /// </summary>
        XCIList<T> GetList();

        /// <summary>
        /// 获取删除的数据列表
        /// </summary>
        XCIList<T> GetDeleteList();
        
        /// <summary>
        /// 实体是否允许编辑
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>如果允许编辑 返回True</returns>
        bool IsAllowEdit(T entity);

        /// <summary>
        /// 实体是否允许删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>如果允许编辑 返回True</returns>
        bool IsAllowDelete(T entity);
    }
}