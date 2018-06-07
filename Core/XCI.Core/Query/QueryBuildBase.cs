using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using XCI.Extension;
using XCI.Helper;

namespace XCI.Component
{
    /// <summary>
    /// 脚本生成基类
    /// </summary>
    public abstract class QueryBuildBase : IQueryBuild
    {
        #region Insert
        
        /// <summary>
        /// 生成Insert语句
        /// </summary>
        /// <param name="query">查询对象</param>
        public virtual DbCommand BuildInsert(Query query)
        {
            query.Complete();
            QueryData data = query.Data;
            IDatabase database = query.DatabaseProvider;
            DbCommand command = database.CreateSqlStringCommand(string.Empty);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Insert Into {0} ( {1} ) "
                , GetFieldName(data.TableName), GetInsertFieldNameStrings(data.InsertFieldList));
            sb.AppendFormat("Values ( {0} )"
                , GetInsertFieldValueStrings(data.InsertFieldList, query.IsParamMode, command, database));

            command.CommandText = sb.ToString();
            return command;
        }

        /// <summary>
        /// 生成Insert字段列表
        /// </summary>
        /// <param name="insertFieldList">插入字段列表</param>
        protected virtual string GetInsertFieldNameStrings(List<InsertField> insertFieldList)
        {
            if (insertFieldList == null || insertFieldList.Count == 0)
                return string.Empty;
            int length = insertFieldList.Count;
            StringBuilder sb = new StringBuilder();
            sb.Append(GetFieldName(insertFieldList[0].FieldName));
            if (length > 1)
            {
                for (int ndx = 1; ndx < length; ndx++)
                {
                    sb.AppendFormat(", {0}", GetFieldName(insertFieldList[ndx].FieldName));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成Insert字段值列表
        /// </summary>
        /// <param name="insertFieldList">插入字段列表</param>
        /// <param name="isParamMode">是否使用参数方式</param>
        /// <param name="command">命令对象</param>
        /// <param name="database">数据库对象</param>
        protected virtual string GetInsertFieldValueStrings(List<InsertField> insertFieldList, bool isParamMode, DbCommand command, IDatabase database)
        {
            if (insertFieldList == null || insertFieldList.Count == 0)
                return string.Empty;
            int length = insertFieldList.Count;
            StringBuilder sb = new StringBuilder();
            sb.Append(GetFieldValue(insertFieldList[0].FieldName, insertFieldList[0].FieldValue, isParamMode, command, database));

            if (length > 1)
            {
                for (int ndx = 1; ndx < length; ndx++)
                {
                    sb.AppendFormat(", {0}", GetFieldValue(insertFieldList[ndx].FieldName, insertFieldList[ndx].FieldValue, isParamMode, command, database));
                }
            }
            return sb.ToString();
        }
        
        #endregion


        #region Update
        
        /// <summary>
        /// 生成Update语句
        /// </summary>
        /// <param name="query">查询对象</param>
        public virtual DbCommand BuildUpdate(Query query)
        {
            query.Complete();
            QueryData data = query.Data;
            IDatabase database = query.DatabaseProvider;
            DbCommand command = database.CreateSqlStringCommand(string.Empty);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Update {0} ", data.TableName);
            sb.AppendFormat("Set {0} ", GetUpdateFieldStrings(data.UpdateFieldList, query.IsParamMode, command, database));
            sb.AppendFormat(GetConditionStrings(data.ConditionList, query.IsParamMode, command, database));

            command.CommandText = sb.ToString();
            return command;
        }


        /// <summary>
        /// 生成Update字段键值
        /// </summary>
        /// <param name="updateFieldList">更新字段列表</param>
        /// <param name="isParamMode">是否使用参数方式</param>
        /// <param name="command">命令对象</param>
        /// <param name="database">数据库对象</param>
        protected virtual string GetUpdateFieldStrings(List<UpdateField> updateFieldList, bool isParamMode, DbCommand command, IDatabase database)
        {
            if (updateFieldList == null || updateFieldList.Count == 0)
                return string.Empty;
            int length = updateFieldList.Count;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}={1}", GetFieldName(updateFieldList[0].FieldName), GetFieldValue(updateFieldList[0].FieldName, updateFieldList[0].FieldValue, isParamMode, command, database));

            if (length > 1)
            {
                for (int ndx = 1; ndx < length; ndx++)
                {
                    sb.AppendFormat(",{0}={1}", GetFieldName(updateFieldList[ndx].FieldName), GetFieldValue(updateFieldList[ndx].FieldName, updateFieldList[ndx].FieldValue, isParamMode, command, database));
                }
            }
            return sb.ToString();
        }

        #endregion


        #region Delete
        
        /// <summary>
        /// 生成Delete语句
        /// </summary>
        /// <param name="query">查询对象</param>
        public virtual DbCommand BuildDelete(Query query)
        {
            query.Complete();
            QueryData data = query.Data;
            IDatabase database = query.DatabaseProvider;
            DbCommand command = database.CreateSqlStringCommand(string.Empty);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Delete From {0} ", data.TableName);
            sb.Append(GetConditionStrings(data.ConditionList, query.IsParamMode, command, database));
            command.CommandText = sb.ToString();
            return command;
        }

        #endregion

        
        #region Select

        /// <summary>
        /// 生成Select语句
        /// </summary>
        /// <param name="query">查询对象</param>
        public abstract DbCommand BuildSelect(Query query);

        /// <summary>
        /// 生成Insert字段列表
        /// </summary>
        /// <param name="selectFieldList">查询字段列表</param>
        protected virtual string GetSelectFieldStrings(List<SelectField> selectFieldList)
        {
            if (selectFieldList == null || selectFieldList.Count == 0)
                return "*";

            StringBuilder sb = new StringBuilder();

            int length = selectFieldList.Count;
            sb.Append(GetFieldName(selectFieldList[0].FieldName));
            if (length > 1)
            {
                for (int ndx = 1; ndx < length; ndx++)
                {
                    sb.AppendFormat(", {0}", GetFieldName(selectFieldList[ndx].FieldName));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成取前几条记录脚本
        /// </summary>
        /// <param name="query">查询对象</param>
        public abstract string BuildTop(Query query);

        /// <summary>
        /// 生成取最后一次插入的自增ID
        /// </summary>
        /// <param name="query">查询对象</param>
        public abstract string BuildLastAutoIncrementID(Query query);


        /// <summary>
        /// 生成排序语句
        /// </summary>
        /// <param name="orderByList">排序字段列表</param>
        protected virtual string GetOrderBy(List<OrderByClause> orderByList)
        {
            if (orderByList == null || orderByList.Count == 0)
                return string.Empty;
            int length = orderByList.Count;
            var buffer = new StringBuilder();
            buffer.AppendFormat("Order By {0} {1} ", GetFieldName(orderByList[0].FieldName), orderByList[0].Ordering.ToString());
            if (length > 1)
            {
                for (int ndx = 1; ndx < length; ndx++)
                {
                    buffer.AppendFormat(" , {0} {1} ", GetFieldName(orderByList[ndx].FieldName), orderByList[ndx].Ordering.ToString());
                }
            }

            return buffer.ToString();
        }

        /// <summary>
        /// 生成分组语句
        /// </summary>
        /// <param name="groupByList">分组字段列表</param>
        protected virtual string GetGroupBy(List<GroupField> groupByList)
        {
            if (groupByList == null || groupByList.Count == 0)
                return string.Empty;
            int length = groupByList.Count;
            var buffer = new StringBuilder();
            buffer.AppendFormat("Group By {0} ", GetFieldName(groupByList[0].FieldName));
            if (length > 1)
            {
                for (int ndx = 1; ndx < length; ndx++)
                {
                    buffer.AppendFormat(" , {0} ", GetFieldName(groupByList[ndx].FieldName));
                }
            }

            return buffer.ToString();
        }
        
        #endregion


        #region Where

        /// <summary>
        /// 获取Where语句
        /// </summary>
        /// <param name="conditionList">查询条件列表</param>
        /// <param name="isParamMode">是否使用参数方式</param>
        /// <param name="command">命令对象</param>
        /// <param name="database">数据库对象</param>
        protected virtual string GetConditionStrings(List<Condition> conditionList, bool isParamMode, DbCommand command, IDatabase database)
        {
            if (conditionList == null || conditionList.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("Where ");
            foreach (var condition in conditionList)
            {
                var conType = GetConditionType(condition.CondTypeType);
                sb.AppendFormat(" {0} ", conType);

                if (condition.IsBeginBracket)
                {
                    sb.Append(" ( ");
                }
                sb.AppendFormat("{0}", GetFieldName(condition.FieldName));
                sb.AppendFormat("{0}", GetComparisonType(condition.Comparison));
                sb.AppendFormat("{0} ", GetConditionFieldValue(condition, isParamMode, command, database));

                if (condition.IsEndBracket)
                {
                    sb.Append(" ) ");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取查询语句字段值
        /// </summary>
        /// <param name="condition">条件语句</param>
        /// <param name="isParamMode">是否使用参数方式</param>
        /// <param name="command">命令对象</param>
        /// <param name="database">数据库对象</param>
        protected virtual string GetConditionFieldValue(Condition condition, bool isParamMode, DbCommand command, IDatabase database)
        {
            if (condition.Comparison == ComparisonType.Is || condition.Comparison == ComparisonType.IsNot)
            {
                return " NULL ";
            }
            if (condition.Comparison == ComparisonType.BetweenAnd)
            {
                return string.Format("{0} And {1}",
                    GetFieldValue(condition.FieldName, condition.StartValue, isParamMode, command, database, "parStart"),
                    GetFieldValue(condition.FieldName, condition.EndValue, isParamMode, command, database, "parEnd"));
            }
            if (condition.Comparison == ComparisonType.In || condition.Comparison == ComparisonType.NotIn)
            {
                if (condition.InValues != null && condition.InValues.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("({0}", GetFieldValue(condition.FieldName, condition.InValues[0], isParamMode, command, database, "par0"));
                    if (condition.InValues.Count > 1)
                    {
                        for (int index = 1; index < condition.InValues.Count; index++)
                        {
                            object item = condition.InValues[index];
                            sb.AppendFormat(",{0}", GetFieldValue(condition.FieldName, item, isParamMode, command, database, "par" + index));
                        }
                    }
                    sb.Append(")");
                    return sb.ToString();
                }
            }
            if (condition.Comparison == ComparisonType.Like
               || condition.Comparison == ComparisonType.NotLike)
            {
                string result = GetFieldValue(condition.FieldName, condition.FieldValue, isParamMode, command, database);

                return string.Concat("'", condition.LikePrefix, result.Replace("\'", ""), condition.LikeSuffix, "'");
            }

            return GetFieldValue(condition.FieldName, condition.FieldValue, isParamMode, command, database);
        }
        
        #endregion


        #region 公用

        /// <summary>
        /// 获取条件关系字符串
        /// </summary>
        /// <param name="conditionType">条件关系</param>
        protected virtual string GetConditionType(ConditionType conditionType)
        {
            if (conditionType != ConditionType.None)
            {
                return conditionType.ToString();
            }
            return string.Empty;
        }
        
        /// <summary>
        /// 获取SQL操作符字符串
        /// </summary>
        /// <param name="comparisonType">SQL操作符</param>
        protected virtual string GetComparisonType(ComparisonType comparisonType)
        {
            switch (comparisonType)
            {
                case ComparisonType.Equals:
                    return " = ";
                case ComparisonType.NotEquals:
                    return " <> ";
                case ComparisonType.GreaterThan:
                    return " > ";
                case ComparisonType.GreaterOrEquals:
                    return " >= ";
                case ComparisonType.LessThan:
                    return " < ";
                case ComparisonType.LessOrEquals:
                    return " <= ";
                case ComparisonType.Like:
                    return " LIKE ";
                case ComparisonType.NotLike:
                    return " NOT LIKE ";
                case ComparisonType.Is:
                    return " IS ";
                case ComparisonType.IsNot:
                    return " IS NOT ";
                case ComparisonType.In:
                    return " In ";
                case ComparisonType.NotIn:
                    return " Not In ";
                case ComparisonType.BetweenAnd:
                    return " Between ";

                default:
                    return " " + comparisonType.ToString() + " ";
            }
        }
        
        /// <summary>
        /// 获取字段名称
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        protected virtual string GetFieldName(string fieldName)
        {
            if (CheckSystemWord(fieldName))
            {
                return string.Concat("[", fieldName, "]");
            }
            return fieldName;
        }

        /// <summary>
        /// 获取字段值(如果使用参数方式 返回参数名称)
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="isParamMode">是否参数方式</param>
        /// <param name="command">命令对象</param>
        /// <param name="database">数据库对象</param>
        /// <param name="parPix">参数名称修饰</param>
        protected virtual string GetFieldValue(string fieldName, object fieldValue, bool isParamMode, DbCommand command, IDatabase database, string parPix = "par")
        {
            if (isParamMode)
            {
                Type objType = typeof(string);
                if (fieldValue != null)
                {
                    objType = fieldValue.GetType();
                }
                string paramName = string.Format("{0}{1}", parPix, fieldName);
                if (objType == typeof(SQLValue))
                {
                    var sqlValue = (SQLValue) fieldValue;
                    if (sqlValue != null)
                    {
                        fieldValue =sqlValue.FieldValue.ToString();
                    }
                }
                var paramObj = database.AddInParameter(command, paramName, fieldValue, 0, CSharpTypeToDbType(objType, fieldName));
                return paramObj.ParameterName;
            }

            if (fieldValue == null)
            {
                return "NULL";
            }
            Type fileType = fieldValue.GetType();
            if (TypeHelper.IsNumeric(fileType))
            {
                return fieldValue.ToString();
            }
            if (TypeHelper.IsBoolean(fileType))
            {
                bool colValue = fieldValue.ToBool();
                if (colValue)
                    return "1";
                return "0";
            }
            if (fileType==typeof(SQLValue))
            {
                return ((SQLValue) fieldValue).FieldValue.ToString();
            }
            return string.Format("'{0}'", EncodeFieldValue(fieldValue.ToString()));

        }

        /// <summary>
        /// 获取字段值类型对应的SQL类型
        /// </summary>
        /// <param name="type">字段值类型</param>
        protected virtual DbType CSharpTypeToDbType(Type type, string fieldName)
        {
            if (fieldName == "ZhiWen1" || fieldName == "ZhiWen1Image" || fieldName == "ZhiWen2" || fieldName == "ZhiWen2Image" || fieldName == "ZhiPian")
                return DbType.Binary;

            if (type == typeof(string))
                return DbType.String;
            if (type == typeof(Int32))
                return DbType.Int32;
            if (type == typeof(bool))
                return DbType.Boolean;
            if (type == typeof(DateTime))
                return DbType.DateTime;
            if (type == typeof(Decimal))
                return DbType.Decimal;
            if (type == typeof(Double))
                return DbType.Double;
            if (type == typeof(byte[]))
                return DbType.Binary;
            if (type == typeof(Int16))
                return DbType.Int16;
            if (type == typeof(Int64))
                return DbType.Int64;
            if (type == typeof(byte))
                return DbType.Byte;
            if (type == typeof(Guid))
                return DbType.Guid;
            if (type == typeof(char))
                return DbType.String;

            

            return DbType.String;
        }

        /// <summary>
        /// 编码字段值
        /// </summary>
        /// <param name="fieldValue">字段值</param>
        protected virtual string EncodeFieldValue(string fieldValue)
        {
            if (string.IsNullOrEmpty(fieldValue)) return fieldValue;

            return fieldValue.Replace("'", "''");
        }

        /// <summary>
        /// 检测字段名称是否是关键字(可以重写)
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        protected virtual bool CheckSystemWord(string fieldName)
        {
            switch (fieldName.ToLower())
            {
                case "name":
                case "group":
                case "year":
                case "month":
                case "key":
                case "select":
                    return true;
                default:
                    return false;
            }
        }

        #endregion
    }
}