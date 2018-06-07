using System;
using System.Collections;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using XCI.Helper;

namespace XCI.Component
{
    /// <summary>
    /// 查询命令操作对象
    /// </summary>
    public class Query
    {
        /// <summary>
        /// 构造函数 禁止外部New对象
        /// </summary>
        internal Query()
        {

        }

        /// <summary>
        /// 查询数据
        /// </summary>
        public QueryData Data = new QueryData();
        /// <summary>
        /// 最后定义的条件语句
        /// </summary>
        protected Condition _lastCondition;
        private IQueryBuild _queryBuildProvider;
        private IDatabase _databaseProvider;
        private bool _isParamMode = true;

        /// <summary>
        /// 新建查询对象
        /// </summary>
        public static Query New
        {
            get { return new Query(); }
        }

        /// <summary>
        /// 数据配置名称
        /// </summary>
        protected string DataConfigName { get; set; }

        /// <summary>
        /// 是否使用参数方式(默认以参数方式)
        /// </summary>
        public bool IsParamMode
        {
            get { return _isParamMode; }
            set { _isParamMode = value; }
        }

        /// <summary>
        /// 脚本生成对象
        /// </summary>
        public IQueryBuild QueryBuildProvider
        {
            get
            {
                if (_queryBuildProvider == null)
                {
                    if (!string.IsNullOrEmpty(DataConfigName))
                    {
                        _queryBuildProvider = QueryBuildFactory.Factory.Get(DataConfigName);
                    }
                    else
                    {
                        _queryBuildProvider = QueryBuildFactory.Current;
                    }
                }
                return _queryBuildProvider;
            }
            set { _queryBuildProvider = value; }
        }

        /// <summary>
        /// 数据库对象
        /// </summary>
        public IDatabase DatabaseProvider
        {
            get
            {
                if (_databaseProvider == null)
                {
                    if (!string.IsNullOrEmpty(DataConfigName))
                    {
                        _databaseProvider = DatabaseFactory.Factory.Get(DataConfigName);
                    }
                    else
                    {
                        _databaseProvider = DatabaseFactory.Current;
                    }
                }
                return _databaseProvider;
            }
            set { _databaseProvider = value; }
        }

        /// <summary>
        /// 设置数据配置名称
        /// </summary>
        /// <param name="configName">数据配置名称</param>
        public Query SetDataConfigName(string configName)
        {
            this.DataConfigName = configName;
            return this;
        }

        /// <summary>
        /// 设置脚本生成方式 true 参数方式
        /// </summary>
        /// <param name="isParamMode">脚本生成方式</param>
        public Query SetParamMode(bool isParamMode = true)
        {
            this.IsParamMode = isParamMode;
            return this;
        }

        /// <summary>
        /// 设置脚本生成对象
        /// </summary>
        /// <param name="queryBuildProvider">脚本生成对象</param>
        public Query SetQueryBuildProvider(IQueryBuild queryBuildProvider)
        {
            this.QueryBuildProvider = queryBuildProvider;
            return this;
        }

        /// <summary>
        /// 设置数据库对象
        /// </summary>
        /// <param name="databaseProvider">数据库对象</param>
        public Query SetDatabaseProvider(IDatabase databaseProvider)
        {
            this.DatabaseProvider = databaseProvider;
            return this;
        }

        /// <summary>
        /// 合并指定查询对象到当前对象
        /// </summary>
        public Query MergeQuery(Query query)
        {
            if (query != null)
            {
                this.Data = query.Data;
                IsParamMode = query.IsParamMode;
            }
            return this;
        }

        /// <summary>
        /// 是否有查询条件
        /// </summary>
        /// <returns>存在返回True</returns>
        public bool HasCondition()
        {
            return Data.ConditionList.Any() || _lastCondition != null;
        }


        /// <summary>
        /// 新建查询条件对象 保存前一个条件对象
        /// </summary>
        /// <param name="condition">条件关系</param>
        /// <param name="fieldName">字段名称</param>
        protected Query NewCondition(ConditionType condition, string fieldName)
        {
            if (_lastCondition != null)
            {
                Data.ConditionList.Add(_lastCondition);
            }
            _lastCondition = new Condition(condition);
            _lastCondition.FieldName = fieldName;
            return this;
        }


        /// <summary>
        /// 配置查询条件对象
        /// </summary>
        /// <param name="comparison">SQL比较符</param>
        /// <param name="val">字段值</param>
        protected virtual Query ConfigCondition(ComparisonType comparison, object val)
        {
            _lastCondition.Comparison = comparison;
            _lastCondition.FieldValue = val;
            return this;
        }



        #region Insert

        /// <summary>
        /// 创建Insert语句
        /// </summary>
        /// <param name="tableName">表名</param>
        public Query Insert(string tableName)
        {
            Data.TableName = tableName;
            return this;
        }

        /// <summary>
        /// 添加Insert值名对
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        public Query Values(string fieldName, object fieldValue)
        {
            Data.InsertFieldList.Add(new InsertField(fieldName, fieldValue));
            return this;
        }

        #endregion


        #region Update

        /// <summary>
        /// 创建Update语句
        /// </summary>
        /// <param name="tableName">表名</param>
        public Query Update(string tableName)
        {
            Data.TableName = tableName;
            return this;
        }

        /// <summary>
        /// 设置Update值名对
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="fieldValue">字段值</param>
        public Query Set(string fieldName, object fieldValue)
        {
            Data.UpdateFieldList.Add(new UpdateField(fieldName, fieldValue));
            return this;
        }


        #endregion


        #region Delete

        /// <summary>
        /// 创建Delete语句
        /// </summary>
        /// <param name="tableName">表名</param>
        public Query Delete(string tableName)
        {
            Data.TableName = tableName;
            return this;
        }

        #endregion


        #region Select

        /// <summary>
        /// 创建Select语句
        /// </summary>
        /// <param name="tableName">表名</param>
        public Query From(string tableName)
        {
            Data.TableName = tableName;
            return this;
        }

        /// <summary>
        /// 添加Select字段
        /// </summary>
        /// <param name="fieldNames">Select字段数组</param>
        public Query Select(params string[] fieldNames)
        {
            foreach (var item in fieldNames)
            {
                Data.SelectFieldList.Add(new SelectField(item));
            }
            return this;
        }

        /// <summary>
        /// 添加GroupBy语句
        /// </summary>
        /// <param name="fieldNames">GroupBy字段数组</param>
        public Query GroupBy(params string[] fieldNames)
        {
            foreach (var item in fieldNames)
            {
                Data.GroupFieldList.Add(new GroupField(item));
            }
            return this;
        }

        /// <summary>
        /// 指定返回的记录数
        /// </summary>
        /// <param name="recordNum">返回记录数</param>
        public Query Top(int recordNum)
        {
            Data.Top = recordNum;
            return this;
        }

        /// <summary>
        /// 添加升序语句
        /// </summary>
        /// <param name="fieldNames">排序字段数组</param>
        public Query OrderBy(params string[] fieldNames)
        {
            foreach (var item in fieldNames)
            {
                OrderByInternal(item, OrderByType.Asc);
            }
            return this;
        }


        /// <summary>
        /// 添加降序语句
        /// </summary>
        /// <param name="fieldNames">排序字段数组</param>
        public Query OrderByDescending(params string[] fieldNames)
        {
            foreach (var item in fieldNames)
            {
                OrderByInternal(item, OrderByType.Desc);
            }
            return this;
        }

        /// <summary>
        /// 添加排序语句
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="ordering">排序类型</param>
        protected Query OrderByInternal(string fieldName, OrderByType ordering)
        {
            var orderBy = new OrderByClause { FieldName = fieldName, Ordering = ordering };
            Data.OrderByList.Add(orderBy);
            return this;
        }


        #endregion


        #region Where

        /// <summary>
        /// 自定义查询字符串
        /// </summary>
        /// <param name="queryString">自定义查询字符串</param>
        public Query QueryString(string queryString)
        {
            Data.QueryString = queryString;
            return this;
        }

        /// <summary>
        /// 根据条件列表自动添加查询条件 如果条件列表为空 添加Where 否则添加 指定类型
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="isAnd">条件类型是否是And</param>
        public Query AutoAddCondition(string fieldName, bool isAnd = true)
        {
            if (HasCondition())
            {
                ConditionType conType = ConditionType.And;
                if (!isAnd)
                {
                    conType = ConditionType.Or;
                }
                NewCondition(conType, fieldName);
            }
            else
            {
                NewCondition(ConditionType.None, fieldName);
            }
            return this;
        }

        /// <summary>
        /// 添加Where条件
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public Query Where(string fieldName)
        {
            return NewCondition(ConditionType.None, fieldName);
        }

        /// <summary>
        /// 添加Where条件加括号
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public Query WhereBracket(string fieldName)
        {
            NewCondition(ConditionType.None, fieldName);
            BeginBracket();
            return this;
        }

        /// <summary>
        /// 添加And条件
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public Query And(string fieldName)
        {
            return NewCondition(ConditionType.And, fieldName);
        }

        /// <summary>
        /// 添加And条件加括号
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public Query AndBracket(string fieldName)
        {
            NewCondition(ConditionType.And, fieldName);
            BeginBracket();
            return this;
        }


        /// <summary>
        /// 添加OR条件
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public Query Or(string fieldName)
        {
            return NewCondition(ConditionType.Or, fieldName);
        }

        /// <summary>
        /// 添加OR条件加括号
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public Query OrBracket(string fieldName)
        {
            NewCondition(ConditionType.Or, fieldName);
            BeginBracket();
            return this;
        }

        /// <summary>
        /// 等于条件
        /// </summary>
        /// <param name="val">字段值</param>
        public Query IsEqualTo(object val)
        {
            return ConfigCondition(ComparisonType.Equals, val);
        }


        /// <summary>
        /// 不等于条件
        /// </summary>
        /// <param name="val">字段值</param>
        public Query IsNotEqualTo(object val)
        {
            return ConfigCondition(ComparisonType.NotEquals, val);
        }


        /// <summary>
        /// Null条件
        /// </summary>
        public Query IsNull()
        {
            ConfigCondition(ComparisonType.Is, null);
            return this;
        }


        /// <summary>
        /// 不为Null条件
        /// </summary>
        public Query IsNotNull()
        {
            ConfigCondition(ComparisonType.IsNot, null);
            return this;
        }


        /// <summary>
        /// In条件
        /// </summary>
        /// <param name="vals">检查的数组值</param>
        public Query In(IList vals)
        {
            _lastCondition.Comparison = ComparisonType.In;
            _lastCondition.InValues = vals;
            return this;
        }

        /// <summary>
        /// Not IN条件
        /// </summary>
        /// <param name="vals">检查的数组值</param>
        public Query NotIn(IList vals)
        {
            _lastCondition.Comparison = ComparisonType.NotIn;
            _lastCondition.InValues = vals;
            return this;
        }

        /// <summary>
        /// Between条件
        /// </summary>
        /// <param name="start">开始值</param>
        /// <param name="end">结束值</param>
        public Query Between(object start, object end)
        {
            _lastCondition.Comparison = ComparisonType.BetweenAnd;
            _lastCondition.StartValue = start;
            _lastCondition.EndValue = end;
            return this;
        }

        /// <summary>
        /// Like条件
        /// </summary>
        /// <param name="val">字段值</param>
        public Query Like(object val)
        {
            _lastCondition.LikePrefix = "%";
            _lastCondition.LikeSuffix = "%";
            return ConfigCondition(ComparisonType.Like, val);
        }


        /// <summary>
        /// Like条件
        /// </summary>
        /// <param name="val">字段值</param>
        /// <param name="addWildcardPrefix">前缀通配符</param>
        /// <param name="addWildcardSuffix">后缀通配符</param>
        public Query Like(object val, string addWildcardPrefix, string addWildcardSuffix)
        {
            _lastCondition.LikePrefix = addWildcardPrefix;
            _lastCondition.LikeSuffix = addWildcardSuffix;
            return ConfigCondition(ComparisonType.Like, val);
        }


        /// <summary>
        /// 大于条件
        /// </summary>
        /// <param name="val">字段值</param>
        public Query GreaterThan(object val)
        {
            return ConfigCondition(ComparisonType.GreaterThan, val);
        }


        /// <summary>
        /// 大于等于条件
        /// </summary>
        /// <param name="val">字段值</param>
        public Query GreaterThanOrEqualTo(object val)
        {
            return ConfigCondition(ComparisonType.GreaterOrEquals, val);
        }


        /// <summary>
        /// 小于条件
        /// </summary>
        /// <param name="val">字段值</param>
        public Query LessThan(object val)
        {
            return ConfigCondition(ComparisonType.LessThan, val);
        }


        /// <summary>
        /// 小于等于条件
        /// </summary>
        /// <param name="val">字段值</param>
        public Query LessOrEquals(object val)
        {
            return ConfigCondition(ComparisonType.LessOrEquals, val);
        }

        /// <summary>
        /// 开始括号
        /// </summary>
        public Query BeginBracket()
        {
            if (_lastCondition != null)
            {
                _lastCondition.IsBeginBracket = true;
            }
            return this;
        }

        /// <summary>
        /// 结束括号
        /// </summary>
        public Query EndBracket()
        {
            if (_lastCondition != null)
            {
                _lastCondition.IsEndBracket = true;
            }
            return this;
        }

        /// <summary>
        /// 完成条件查询 保存数据 重置属性
        /// </summary>
        internal Query Complete()
        {
            if (_lastCondition != null)
            {
                Data.ConditionList.Add(_lastCondition);
                _lastCondition = null;
            }
            return this;
        }

        #endregion


        #region GetCommand

        /// <summary>
        /// 获取Insert语句
        /// </summary>
        public string ToInsertString()
        {
            return GetInsertCommand().CommandText;
        }

        /// <summary>
        /// 获取Update语句
        /// </summary>
        public string ToUpdateString()
        {
            return GetUpdateCommand().CommandText;
        }

        /// <summary>
        /// 获取Delete语句
        /// </summary>
        public string ToDeleteString()
        {
            return GetDeleteCommand().CommandText;
        }

        /// <summary>
        /// 获取Select语句
        /// </summary>
        public string ToSelectString()
        {
            return GetSelectCommand().CommandText;
        }


        /// <summary>
        /// 获取Insert命令对象
        /// </summary>
        public DbCommand GetInsertCommand()
        {
            return QueryBuildProvider.BuildInsert(this);
        }

        /// <summary>
        /// 获取Update命令对象
        /// </summary>
        public DbCommand GetUpdateCommand()
        {
            return QueryBuildProvider.BuildUpdate(this);
        }

        /// <summary>
        /// 获取Delete命令对象
        /// </summary>
        public DbCommand GetDeleteCommand()
        {
            return QueryBuildProvider.BuildDelete(this);
        }

        /// <summary>
        /// 获取Select命令对象
        /// </summary>
        public DbCommand GetSelectCommand()
        {
            return QueryBuildProvider.BuildSelect(this);
        }

        #endregion

    }

    /// <summary>
    /// 查询命令操作泛型对象
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    public class Query<T> : Query
    {
        /// <summary>
        /// 构造函数 禁止外部New对象
        /// </summary>
        internal Query()
        {

        }

        /// <summary>
        /// 新建查询对象
        /// </summary>
        public static new Query<T> New
        {
            get { return new Query<T>(); }
        }

        /// <summary>
        /// 设置数据配置名称
        /// </summary>
        /// <param name="configName">数据配置名称</param>
        public new Query<T> SetDataConfigName(string configName)
        {
            base.SetDataConfigName(configName);
            return this;
        }

        /// <summary>
        /// 设置脚本生成方式 true 参数方式
        /// </summary>
        /// <param name="isParamMode">脚本生成方式</param>
        public new Query<T> SetParamMode(bool isParamMode = true)
        {
            base.SetParamMode(isParamMode);
            return this;
        }

        /// <summary>
        /// 设置脚本生成对象
        /// </summary>
        /// <param name="queryBuildProvider">脚本生成对象</param>
        public new Query<T> SetQueryBuildProvider(IQueryBuild queryBuildProvider)
        {
            base.SetQueryBuildProvider(queryBuildProvider);
            return this;
        }

        /// <summary>
        /// 设置数据库对象
        /// </summary>
        /// <param name="databaseProvider">数据库对象</param>
        public new Query<T> SetDatabaseProvider(IDatabase databaseProvider)
        {
            base.SetDatabaseProvider(databaseProvider);
            return this;
        }

        /// <summary>
        /// 合并指定查询对象到当前对象
        /// </summary>
        public new Query<T> MergeQuery(Query query)
        {
            base.MergeQuery(query);
            return this;
        }

        /// <summary>
        /// 是否有查询条件
        /// </summary>
        /// <returns>存在返回True</returns>
        public new bool HasCondition()
        {
            return base.HasCondition();
        }


        #region Insert

        /// <summary>
        /// 创建Insert语句
        /// </summary>
        /// <param name="tableName">表名</param>
        public new Query<T> Insert(string tableName)
        {
            base.Insert(tableName);
            return this;
        }

        /// <summary>
        /// 添加Insert值名对
        /// </summary>
        /// <param name="obj">数据对象</param>
        /// <param name="exps">表达式数组</param>
        public Query<T> Values(T obj, params Expression<Func<T, object>>[] exps)
        {
            if (exps == null || exps.Length == 0)
                return this;

            foreach (Expression<Func<T, object>> t in exps)
            {
                string proName = ExpressionHelper.GetPropertyName(t);
                object proValue = ExpressionHelper.GetPropertyValue(obj, t);
                Values(proName, proValue);
            }

            return this;
        }

        /// <summary>
        /// 添加Insert值名对
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        public new Query<T> Values(string fieldName, object fieldValue)
        {
            base.Values(fieldName, fieldValue);
            return this;
        }

        #endregion


        #region Update

        /// <summary>
        /// 创建Update语句
        /// </summary>
        /// <param name="tableName">表名</param>
        public new Query<T> Update(string tableName)
        {
            base.Update(tableName);
            return this;
        }

        /// <summary>
        /// 设置Update值名对
        /// </summary>
        /// <param name="obj">数据对象</param>
        /// <param name="exps">表达式数组</param>
        public Query<T> Set(T obj, params Expression<Func<T, object>>[] exps)
        {
            if (exps == null || exps.Length == 0)
                return this;

            foreach (Expression<Func<T, object>> t in exps)
            {
                string proName = ExpressionHelper.GetPropertyName(t);
                object proValue = ExpressionHelper.GetPropertyValue(obj, t);
                Set(proName, proValue);
            }

            return this;
        }

        /// <summary>
        /// 设置Update值名对
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="fieldValue">字段值</param>
        public new Query<T> Set(string fieldName, object fieldValue)
        {
            base.Set(fieldName, fieldValue);
            return this;
        }

        /// <summary>
        /// 设置Update值名对
        /// </summary>
        /// <param name="exp">字段名称表达式</param>
        /// <param name="fieldValue">字段值</param>
        public Query<T> Set(Expression<Func<T, object>> exp, object fieldValue)
        {
            base.Set(ExpressionHelper.GetPropertyName(exp), fieldValue);
            return this;
        }

        #endregion


        #region Delete

        /// <summary>
        /// 创建Delete语句
        /// </summary>
        /// <param name="tableName">表名</param>
        public new Query<T> Delete(string tableName)
        {
            base.Delete(tableName);
            return this;
        }

        #endregion


        #region Select

        /// <summary>
        /// 创建Select语句
        /// </summary>
        /// <param name="tableName">表名</param>
        public new Query<T> From(string tableName)
        {
            base.From(tableName);
            return this;
        }

        /// <summary>
        /// 添加Select字段
        /// </summary>
        /// <param name="exps">Select字段表达式数组</param>
        public Query<T> Select(params Expression<Func<T, object>>[] exps)
        {
            Select(ExpressionHelper.GetColumnArray(exps));
            return this;
        }

        /// <summary>
        /// 添加Select字段
        /// </summary>
        /// <param name="fieldNames">Select字段数组</param>
        public new Query<T> Select(params string[] fieldNames)
        {
            base.Select(fieldNames);
            return this;
        }

        /// <summary>
        /// 添加GroupBy语句
        /// </summary>
        /// <param name="exps">GroupBy字段表达式数组</param>
        public Query<T> GroupBy(params Expression<Func<T, object>>[] exps)
        {
            GroupBy(ExpressionHelper.GetColumnArray(exps));
            return this;
        }

        /// <summary>
        /// 添加GroupBy语句
        /// </summary>
        /// <param name="fieldNames">GroupBy字段数组</param>
        public new Query<T> GroupBy(params string[] fieldNames)
        {
            base.GroupBy(fieldNames);
            return this;
        }


        /// <summary>
        /// 指定返回的记录数
        /// </summary>
        /// <param name="recordNum">返回的记录数</param>
        public new Query<T> Top(int recordNum)
        {
            base.Top(recordNum);
            return this;
        }

        /// <summary>
        /// 添加升序语句
        /// </summary>
        /// <param name="exps">排序字段表达式数组</param>
        public Query<T> OrderBy(params Expression<Func<T, object>>[] exps)
        {
            OrderBy(ExpressionHelper.GetColumnArray(exps));
            return this;
        }

        /// <summary>
        /// 添加降序语句
        /// </summary>
        /// <param name="exps">排序字段表达式数组</param>
        public Query<T> OrderByDescending(params Expression<Func<T, object>>[] exps)
        {
            OrderByDescending(ExpressionHelper.GetColumnArray(exps));
            return this;
        }

        /// <summary>
        /// 添加升序语句
        /// </summary>
        /// <param name="fieldNames">排序字段数组</param>
        public new Query OrderBy(params string[] fieldNames)
        {
            base.OrderBy(fieldNames);
            return this;
        }

        /// <summary>
        /// 添加降序语句
        /// </summary>
        /// <param name="fieldNames">排序字段数组</param>
        public new Query OrderByDescending(params string[] fieldNames)
        {
            base.OrderByDescending(fieldNames);
            return this;
        }

        #endregion


        #region Where

        /// <summary>
        /// 自定义查询字符串
        /// </summary>
        /// <param name="queryString">自定义查询字符串</param>
        public new Query<T> QueryString(string queryString)
        {
            base.QueryString(queryString);
            return this;
        }

        /// <summary>
        /// 根据条件列表自动添加查询条件 如果条件列表为空 添加Where 否则添加 指定类型
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="isAnd">条件类型是否是And</param>
        public new Query<T> AutoAddCondition(string fieldName, bool isAnd = true)
        {
            base.AutoAddCondition(fieldName, isAnd);
            return this;
        }

        /// <summary>
        /// 根据条件列表自动添加查询条件 如果条件列表为空 添加Where 否则添加 指定类型
        /// </summary>
        /// <param name="exp">字段表达式</param>
        /// <param name="isAnd">条件类型是否是And</param>
        public Query<T> AutoAddCondition(Expression<Func<T, object>> exp, bool isAnd = true)
        {
            base.AutoAddCondition(ExpressionHelper.GetPropertyName(exp), isAnd);
            return this;
        }

        /// <summary>
        /// 添加Where条件
        /// </summary>
        /// <param name="exp">字段表达式</param>
        public Query<T> Where(Expression<Func<T, object>> exp)
        {
            base.Where(ExpressionHelper.GetPropertyName(exp));
            return this;
        }

        /// <summary>
        /// 添加Where条件加括号
        /// </summary>
        /// <param name="exp">字段表达式</param>
        public Query<T> WhereBracket(Expression<Func<T, object>> exp)
        {
            base.WhereBracket(ExpressionHelper.GetPropertyName(exp));
            return this;
        }

        /// <summary>
        /// 添加Where条件
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public new Query<T> Where(string fieldName)
        {
            base.Where(fieldName);
            return this;
        }

        /// <summary>
        /// 添加Where条件加括号
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public new Query<T> WhereBracket(string fieldName)
        {
            base.WhereBracket(fieldName);
            return this;
        }



        /// <summary>
        /// 添加And条件
        /// </summary>
        /// <param name="exp">字段表达式</param>
        public Query<T> And(Expression<Func<T, object>> exp)
        {
            base.And(ExpressionHelper.GetPropertyName(exp));
            return this;
        }

        /// <summary>
        /// 添加And条件加括号
        /// </summary>
        /// <param name="exp">字段表达式</param>
        public Query<T> AndBracket(Expression<Func<T, object>> exp)
        {
            base.AndBracket(ExpressionHelper.GetPropertyName(exp));
            return this;
        }

        /// <summary>
        /// 添加And条件
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public new Query<T> And(string fieldName)
        {
            base.And(fieldName);
            return this;
        }

        /// <summary>
        /// 添加And条件加括号
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public new Query<T> AndBracket(string fieldName)
        {
            base.AndBracket(fieldName);
            return this;
        }


        /// <summary>
        /// 添加OR条件
        /// </summary>
        /// <param name="exp">字段表达式</param>
        public Query<T> Or(Expression<Func<T, object>> exp)
        {
            base.Or(ExpressionHelper.GetPropertyName(exp));
            return this;
        }

        /// <summary>
        /// 添加OR条件加括号
        /// </summary>
        /// <param name="exp">字段表达式</param>
        public Query<T> OrBracket(Expression<Func<T, object>> exp)
        {
            base.OrBracket(ExpressionHelper.GetPropertyName(exp));
            return this;
        }


        /// <summary>
        /// 添加OR条件
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public new Query<T> Or(string fieldName)
        {
            base.Or(fieldName);
            return this;
        }

        /// <summary>
        /// 添加OR条件加括号
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public new Query<T> OrBracket(string fieldName)
        {
            base.OrBracket(fieldName);
            return this;
        }




        /// <summary>
        /// 等于条件
        /// </summary>
        /// <param name="val">字段值</param>
        public new Query<T> IsEqualTo(object val)
        {
            base.IsEqualTo(val);
            return this;
        }


        /// <summary>
        /// 不等于条件
        /// </summary>
        /// <param name="val">字段值</param>
        public new Query<T> IsNotEqualTo(object val)
        {
            base.IsNotEqualTo(val);
            return this;
        }


        /// <summary>
        /// Null条件
        /// </summary>
        public new Query<T> IsNull()
        {
            base.IsNull();
            return this;
        }


        /// <summary>
        /// 不为Null条件
        /// </summary>
        public new Query<T> IsNotNull()
        {
            base.IsNotNull();
            return this;
        }


        /// <summary>
        /// In条件
        /// </summary>
        /// <param name="vals">检查的数组值</param>
        public new Query<T> In(IList vals)
        {
            base.In(vals);
            return this;
        }


        /// <summary>
        /// Not IN 条件
        /// </summary>
        /// <param name="vals">检查的数组值</param>
        public new Query<T> NotIn(IList vals)
        {
            base.NotIn(vals);
            return this;
        }

        /// <summary>
        /// Between条件
        /// </summary>
        /// <param name="start">开始值</param>
        /// <param name="end">结束值</param>
        public new Query<T> Between(object start, object end)
        {
            base.Between(start, end);
            return this;
        }

        /// <summary>
        /// Like条件
        /// </summary>
        /// <param name="val">字段值</param>
        public new Query<T> Like(object val)
        {
            base.Like(val);
            return this;
        }


        /// <summary>
        /// Like条件
        /// </summary>
        /// <param name="val">字段值</param>
        /// <param name="addWildcardPrefix">前缀通配符</param>
        /// <param name="addWildcardSuffix">后缀通配符</param>
        public new Query<T> Like(object val, string addWildcardPrefix, string addWildcardSuffix)
        {
            base.Like(val, addWildcardPrefix, addWildcardSuffix);
            return this;
        }


        /// <summary>
        /// 大于条件
        /// </summary>
        /// <param name="val">字段值</param>
        public new Query<T> GreaterThan(object val)
        {
            base.GreaterThan(val);
            return this;
        }


        /// <summary>
        /// 大于等于条件
        /// </summary>
        /// <param name="val">字段值</param>
        public new Query<T> GreaterThanOrEqualTo(object val)
        {
            base.GreaterThanOrEqualTo(val);
            return this;
        }


        /// <summary>
        /// 小于条件
        /// </summary>
        /// <param name="val">字段值</param>
        public new Query<T> LessThan(object val)
        {
            base.LessThan(val);
            return this;
        }


        /// <summary>
        /// 小于等于条件
        /// </summary>
        /// <param name="val">字段值</param>
        public new Query<T> LessOrEquals(object val)
        {
            base.LessOrEquals(val);
            return this;
        }

        /// <summary>
        /// 开始括号
        /// </summary>
        public new Query<T> BeginBracket()
        {
            base.BeginBracket();
            return this;
        }

        /// <summary>
        /// 结束括号
        /// </summary>
        public new Query<T> EndBracket()
        {
            base.EndBracket();
            return this;
        }

        #endregion

    }
}