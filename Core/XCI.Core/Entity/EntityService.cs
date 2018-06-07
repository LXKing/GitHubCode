using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;
using XCI.Core;
using XCI.Extension;
using XCI.Helper;

namespace XCI.Component
{
    /// <summary>
    /// 实体操作基类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public abstract class EntityService<T> : IEntityService<T> where T : EntityBase
    {
        #region 属性

        private Type _entityType = typeof(T);
        private EntityMetadata _metadata;
        private IDatabase _database;
        private IQueryBuild _queryBuild;

        /// <summary>
        /// 实体类型
        /// </summary>
        protected Type EntityType
        {
            get { return _entityType; }
            set { _entityType = value; }
        }

        /// <summary>
        /// 实体元数据
        /// </summary>
        protected EntityMetadata Metadata
        {
            get
            {
                if (_metadata == null)
                {
                    _metadata = EntityMetadataFactory.Current.Get(EntityType.FullName);
                }
                return _metadata;
            }
        }

        /// <summary>
        /// 数据配置名称
        /// </summary>
        public virtual string DataConfigName { get; set; }

        /// <summary>
        /// 数据库对象
        /// </summary>
        public virtual IDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    if (string.IsNullOrEmpty(DataConfigName))
                    {
                        _database = DatabaseFactory.Current;
                    }
                    else
                    {
                        _database = DatabaseFactory.Factory.Get(DataConfigName);
                    }
                }
                return _database;
            }
        }

        /// <summary>
        /// 脚本生成对象
        /// </summary>
        protected virtual IQueryBuild QueryBuild
        {
            get
            {
                if (_queryBuild == null)
                {
                    if (string.IsNullOrEmpty(DataConfigName))
                    {
                        _queryBuild = QueryBuildFactory.Current;
                    }
                    else
                    {
                        _queryBuild = QueryBuildFactory.Factory.Get(DataConfigName);
                    }
                }
                return _queryBuild;
            }
        }

        #endregion


        #region 公用

        /// <summary>
        /// 创建查询对象 如果已经存在查询对象则设置数据属性后返回
        /// </summary>
        /// <param name="alreadyQuery">已存在的查询对象</param>
        public Query<T> CreateQuery(Query alreadyQuery = null)
        {
            if (alreadyQuery == null)
            {
                Query query = new Query<T>()
                .SetDatabaseProvider(Database)
                .SetQueryBuildProvider(QueryBuild);
                query.Data.TableName = Metadata.TableName; 
                return (Query<T>)query;
            }
            alreadyQuery.SetDatabaseProvider(Database)
            .SetQueryBuildProvider(QueryBuild);
            return (Query<T>)alreadyQuery;
        }

        /// <summary>
        /// 数据表转为实体列表
        /// </summary>
        /// <param name="table">数据表</param>
        public XCIList<T> ConvertToEntityList(DataTable table)
        {
            XCIList<T> list = new XCIList<T>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(MapToEntity(row));
            }
            return list;
        }

        /// <summary>
        /// 映射数据行 把DataTable一行映射为一个实体对象
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public virtual T MapToEntity(DataRow dataRow)
        {
            if (dataRow == null || Metadata==null)
            {
                return default(T);
            }
            T entity = ObjectHelper.CreateInstance<T>();
            foreach (var item in Metadata.EntityFields)
            {
                string fieldName = item.FieldName;
                var fastProperty = EntityMetadata.GetFastProperty(EntityType, fieldName);
                if (fastProperty.Property == null)
                {
                    continue;
                }
                if (HasPropertyName(dataRow, fieldName))
                {
                    fastProperty.Set(entity, ObjectHelper
                                                 .ConvertObjectValue(dataRow[fieldName], fastProperty.Property.PropertyType));
                }
            }
            Metadata.GetEncryptFields(entity);
            return entity;
        }

        /// <summary>
        /// 映射实体 把实体对象转为DataTable一行
        /// </summary>
        /// <param name="table">数据表对象 主要用于创建行对象</param>
        /// <param name="entity">实体对象</param>
        /// <returns>数据行</returns>
        public DataRow MapToDataRow(DataTable table, T entity)
        {
            if (table == null)
            {
                return null;
            }
            DataRow row = table.NewRow();
            UpdateDataRow(row, entity);
            return row;
        }

        /// <summary>
        /// 更新数据行
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="entity">实体对象</param>
        public void UpdateDataRow(DataRow row, T entity)
        {
            Metadata.GetEncryptFields(entity);
            foreach (var item in Metadata.EntityFields)
            {
                string fieldName = item.FieldName;
                var fastProperty = EntityMetadata.GetFastProperty(EntityType, fieldName);
                if (fastProperty.Property == null)
                {
                    continue;
                }
                if (HasPropertyName(row, fieldName))
                {
                    var value = fastProperty.Get(entity);
                    if (value == null)
                    {
                        row[fieldName] = DBNull.Value;
                    }
                    else
                    {
                        row[fieldName] = ObjectHelper.ConvertObjectValue(value, fastProperty.Property.PropertyType);
                    }
                }
            }
        }

        /// <summary>
        /// 数据行中是否有指定属性名称
        /// </summary>
        /// <param name="dataRow">数据行</param>
        /// <param name="propertyName">属性名称</param>
        protected bool HasPropertyName(DataRow dataRow, string propertyName)
        {
            DataColumnCollection collection = dataRow.Table.Columns;
            if (collection.Count > 0)
            {
                foreach (DataColumn item in collection)
                {
                    if (propertyName.Equals(item.ColumnName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion


        #region Exists

        /// <summary>
        /// 测试对象是否存在
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns>如果存在返回True</returns>
        public bool Exists(Query query)
        {
            Query exists = CreateQuery(query)
                .From(Metadata.TableName)
                .Select(Metadata.PrimaryKeyFieldName);
            var obj = Database.ExecuteScalar(exists.GetSelectCommand());
            return obj != null;
        }

        /// <summary>
        /// 测试对象是否存在
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="propertyNames">检测的属性名称</param>
        /// <returns>如果存在返回True</returns>
        public bool Exists(T entity, params string[] propertyNames)
        {
            var query = Query<T>.New;

            foreach (string name in propertyNames)
            {
                query.AutoAddCondition(name).IsEqualTo(entity.GetPropertyValue(name));
            }

            query.AutoAddCondition(Metadata.PrimaryKeyFieldName).IsNotEqualTo(Metadata.GetPrimaryKeyValue(entity));

            //if (!string.IsNullOrEmpty(Metadata.DeleteFieldName))
            //{
            //    query.AutoAddCondition(Metadata.DeleteFieldName).IsEqualTo(0);
            //}
            return Exists(query);
        }

        /// <summary>
        /// 测试对象是否存在
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="exps">检测的属性名称表达式</param>
        /// <returns>如果存在返回True</returns>
        public bool Exists(T entity, params Expression<Func<T, object>>[] exps)
        {
            return Exists(entity, ExpressionHelper.GetColumnArray(exps));
        }

        #endregion


        #region Create

        /// <summary>
        /// 添加实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Create(T entity)
        {
            Metadata.SetSpellFields(entity);
            //Metadata.SetEncryptFields(entity);
            Metadata.SetAuditable(entity, false);
            Metadata.SetVersionValue(entity, 1);
            if (!string.IsNullOrEmpty(Metadata.SortCodeFieldName) 
                && string.IsNullOrEmpty(Metadata.ParentFieldName))
            {
                Metadata.SetSortCodeValue(entity, SequenceFactory.Current.GetSequence(Metadata.TableName));
            }

            Query insert = CreateQuery().Insert(Metadata.TableName);
            foreach (var item in Metadata.EntityFields)
            {
                string fieldName = item.FieldName;
                if (CheckCreateField(fieldName))
                {
                    insert.Values(fieldName, Metadata.GetFieldValue(entity,fieldName));
                }
            }
            DbCommand command = insert.GetInsertCommand();
            if (Metadata.IsAutoIncrement)
            {
                command.CommandText += ";" + QueryBuild.BuildLastAutoIncrementID(insert);
                object primaryKeyValue = Database.ExecuteScalar(command);
                if (Metadata.IsAutoIncrement)
                {
                    Metadata.SetPrimaryKeyValue(entity, primaryKeyValue.ToInt());
                }
            }
            else
            {
                Database.ExecuteNonQuery(command);
            }
        }

        protected virtual bool CheckCreateField(string fieldName)
        {
            if (Metadata.IsAutoIncrement && fieldName.Equals(Metadata.PrimaryKeyFieldName))
            {
                return false;
            }
            if (fieldName.Equals(Metadata.UpdateUserIdFieldName)
                || fieldName.Equals(Metadata.UpdateUserNameFieldName)
                || fieldName.Equals(Metadata.UpdateDateTimeFieldName))
            {
                return false;
            }
            return true;
        }

        #endregion


        #region Update

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Update(T entity)
        {
            if (!Metadata.GetAllowEditValue(entity))
            {
                throw new ArgumentException("此记录不允许编辑");
            }
            Metadata.SetSpellFields(entity);
            //Metadata.SetEncryptFields(entity);
            Metadata.SetAuditable(entity, true);

            CheckVersion(entity);

            Query update = CreateQuery().Update(Metadata.TableName)
                .Where(Metadata.PrimaryKeyFieldName).IsEqualTo(Metadata.GetPrimaryKeyValue(entity));
            foreach (var item in Metadata.EntityFields)
            {
                string fieldName = item.FieldName;
                if (CheckUpdateField(fieldName))
                {
                    update.Set(fieldName, Metadata.GetFieldValue(entity, fieldName));
                }
            }
            Update(update);
        }

        protected virtual bool CheckUpdateField(string fieldName)
        {
            if (fieldName.Equals(Metadata.PrimaryKeyFieldName)
                || fieldName.Equals(Metadata.CreateUserIdFieldName)
                || fieldName.Equals(Metadata.CreateUserNameFieldName)
                || fieldName.Equals(Metadata.CreateDateTimeFieldName)
                || fieldName.Equals(Metadata.SortCodeFieldName)
                || fieldName.Equals(Metadata.DeleteFieldName))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="query">查询对象</param>
        public void Update(Query query)
        {
            Query update = CreateQuery(query).Update(Metadata.TableName);
            Database.ExecuteNonQuery(update.GetUpdateCommand());
        }


        /// <summary>
        /// 检测实体版本
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void CheckVersion(T entity)
        {
            if (string.IsNullOrEmpty(Metadata.VersionFieldName))
            {
                return;
            }
            var pkValue = Metadata.GetPrimaryKeyValue(entity);
            var newEntity = Get(pkValue);
            int oldVersion = entity.GetPropertyValue(Metadata.VersionFieldName).ToInt();
            int newVersion = newEntity.GetPropertyValue(Metadata.VersionFieldName).ToInt();
            if (newVersion > oldVersion)
            {
                throw new VersionException("版本冲突,此数据已经在其他地方修改," +
                                               string.Format("最新版本为{0},当前版本为{1}",
                                               newVersion, oldVersion), newEntity);
            }

            newVersion = oldVersion + 1;
            Metadata.SetVersionValue(entity, newVersion);
        }

        /// <summary>
        /// 批量更新字段 
        /// </summary>
        /// <param name="fieldDic">改变的数据字典 (主键 (列名 列值))</param>
        public void BatchUpdateField(Dictionary<object, Dictionary<string, object>> fieldDic)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<object, Dictionary<string, object>> pair in fieldDic)
            {
                Query update = CreateQuery().Update(Metadata.TableName)
                    .SetParamMode(false).Where(Metadata.PrimaryKeyFieldName).IsEqualTo(pair.Key);
                foreach (KeyValuePair<string, object> keyValuePair in pair.Value)
                {
                    update.Set(keyValuePair.Key, keyValuePair.Value);
                }
                sb.AppendFormat("{0};", update.GetUpdateCommand().CommandText);
            }
            if (sb.Length > 0)
            {
                Database.ExecuteNonQuery(sb.ToString());
            }
        }

        /// <summary>
        /// 批量更新字段 
        /// </summary>
        /// <param name="fieldList">改变的数据 (主键 列名 列值)</param>
        public void BatchUpdateField(IList<Tuple<object, string, object>> fieldList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Tuple<object, string, object> tuple in fieldList)
            {
                Query update = CreateQuery().Update(Metadata.TableName).SetParamMode(false)
                    .Set(tuple.Item2, tuple.Item3).Where(Metadata.PrimaryKeyFieldName).IsEqualTo(tuple.Item1);
                sb.AppendFormat("{0};", update.GetUpdateCommand().CommandText);
            }
            if (sb.Length > 0)
            {
                Database.ExecuteNonQuery(sb.ToString());
            }
        }

        #endregion


        #region Delete

        /// <summary>
        /// 删除实体对象(如果存在删除字段 则更新删除字段值)
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Delete(T entity)
        {
            if (!Metadata.GetAllowDeleteValue(entity))
            {
                throw new ArgumentException("此记录不允许删除");
            }
            Delete(Metadata.GetPrimaryKeyValue(entity));
        }

        /// <summary>
        /// 删除实体对象 根据实体主键
        /// </summary>
        /// <param name="ID">实体主键</param>
        public void Delete(int ID)
        {
            if (!string.IsNullOrEmpty(Metadata.DeleteFieldName))
            {
                Query update = CreateQuery()
                .Set(Metadata.DeleteFieldName, 1)
                .Where(Metadata.PrimaryKeyFieldName).IsEqualTo(ID);
                Update(update);
            }
            else
            {
                Query delete = CreateQuery()
                    .Where(Metadata.PrimaryKeyFieldName).IsEqualTo(ID);
                Delete(delete);
            }
        }

        /// <summary>
        /// 删除实体对象 根据实体主键数组
        /// </summary>
        /// <param name="IDs">实体主键数组</param>
        public void Delete(int[] IDs)
        {
            Query delete = CreateQuery()
                .Where(Metadata.PrimaryKeyFieldName).In(IDs);
            Delete(delete);
        }


        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="query">查询对象</param>
        public void Delete(Query query)
        {
            Query delete = CreateQuery(query).Delete(Metadata.TableName);
            Database.ExecuteNonQuery(delete.GetDeleteCommand());
        }

        /// <summary>
        /// 删除全部实体
        /// </summary>
        public void DeleteAll()
        {
            Query delete = CreateQuery().Delete(Metadata.TableName);
            Delete(delete);
        }

        #endregion


        #region Get

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="ID">实体ID</param>
        public T Get(int ID)
        {
            Query select = CreateQuery()
                .Where(Metadata.PrimaryKeyFieldName).IsEqualTo(ID)
                .Top(1);

            return Get(select);
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="query">查询对象</param>
        public T Get(Query query)
        {
            Query select = CreateQuery(query);
            DataTable dt = GetTable(true, true, select);
            if (dt != null && dt.Rows.Count > 0)
            {
                return MapToEntity(dt.Rows[0]);
            }
            return default(T);
        }

        #endregion


        #region GetTable

        /// <summary>
        /// 获取数据表(指定是否包含禁用或者删除数据)
        /// </summary>
        /// <param name="isContainValid">是否包含禁用数据</param>
        /// <param name="isContainDelete">是否包含删除数据</param>
        /// <param name="query">查询对象</param>
        public DataTable GetTable(bool isContainValid, bool isContainDelete, Query query)
        {
            Query select = CreateQuery(query).From(Metadata.TableName);
            if (!isContainValid && !string.IsNullOrEmpty(Metadata.ValidFieldName))
            {
                select.AutoAddCondition(Metadata.ValidFieldName).IsEqualTo(1);
            }
            if (!isContainDelete && !string.IsNullOrEmpty(Metadata.DeleteFieldName))
            {
                select.AutoAddCondition(Metadata.DeleteFieldName).IsEqualTo(0);
            }
            if (!string.IsNullOrEmpty(Metadata.ParentFieldName))
            {
                select.OrderBy(Metadata.ParentFieldName);
            }
            if (!string.IsNullOrEmpty(Metadata.SortCodeFieldName))
            {
                select.OrderBy(Metadata.SortCodeFieldName);
            }
            return GetTable(select);
        }

        /// <summary>
        /// 获取数据表根据指定的查询对象(如果要获取全部数据 查询对象传空即可)
        /// </summary>
        /// <param name="query">查询对象</param>
        public DataTable GetTable(Query query)
        {
            Query select = CreateQuery(query);
            select.From(Metadata.TableName);

            if (select.Data.SelectFieldList.Count == 0)
            {
                foreach (var item in Metadata.EntityFields)
                {
                    select.Select(item.FieldName);
                }
            }

            DataTable dt = Database.ExecuteDataTable(select.GetSelectCommand());
            return dt;
        }

        /// <summary>
        /// 获取有效数据表(只包含没有删除 没有禁用的记录)
        /// </summary>
        public DataTable GetTable()
        {
            return GetTable(false, false, null);
        }

        /// <summary>
        /// 获取删除的数据表
        /// </summary>
        public DataTable GetDeleteTable()
        {
            if (!string.IsNullOrEmpty(Metadata.DeleteFieldName))
            {
                Query query = CreateQuery();
                query.AutoAddCondition(Metadata.DeleteFieldName).IsEqualTo(1);
                return GetTable(query);
            }
            return null;
        }

        #endregion


        #region GetList

        /// <summary>
        /// 获取数据列表(指定是否包含禁用或者删除数据)
        /// </summary>
        /// <param name="isContainValid">是否包含禁用数据</param>
        /// <param name="isContainDelete">是否包含删除数据</param>
        /// <param name="query">查询对象</param>
        public XCIList<T> GetList(bool isContainValid, bool isContainDelete, Query query)
        {
            DataTable dt = GetTable(isContainValid, isContainDelete, query);
            return ConvertToEntityList(dt);
        }

        /// <summary>
        /// 获取数据列表根据指定的查询对象(如果要获取全部数据 查询对象传空即可)
        /// </summary>
        /// <param name="query">查询对象</param>
        public XCIList<T> GetList(Query query)
        {
            DataTable dt = GetTable(query);
            return ConvertToEntityList(dt);
        }

        /// <summary>
        /// 获取有效数据列表(只包含没有删除 没有禁用的记录)
        /// </summary>
        public XCIList<T> GetList()
        {
            DataTable dt = GetTable();
            return ConvertToEntityList(dt);
        }

        /// <summary>
        /// 获取删除的数据列表
        /// </summary>
        public XCIList<T> GetDeleteList()
        {
            DataTable dt = GetDeleteTable();
            return ConvertToEntityList(dt);
        }

        #endregion



        #region AllowEdit AllowDelete

        /// <summary>
        /// 实体是否允许编辑
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>如果允许编辑 返回True</returns>
        public bool IsAllowEdit(T entity)
        {
            return Metadata.GetAllowEditValue(entity);
        }

        /// <summary>
        /// 实体是否允许删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>如果允许编辑 返回True</returns>
        public bool IsAllowDelete(T entity)
        {
            return Metadata.GetAllowDeleteValue(entity);
        }

        #endregion

    }
}