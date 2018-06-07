using System.Data.Common;
using System.Text;
namespace XCI.Component
{
    /// <summary>
    /// Sqlite数据库脚本生成
    /// </summary>
    [XCIComponent(
        "Sqlite数据库脚本生成",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "2.1.0.0",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "Sqlite数据库脚本生成",
        "XCI.Query.QueryBuildLogo.png")]
    public class SqliteQueryBuild:QueryBuildBase
    {
        /// <summary>
        /// 生成Select语句
        /// </summary>
        /// <param name="query">查询对象</param>
        public override DbCommand BuildSelect(Query query)
        {
            query.Complete();
            QueryData data = query.Data;
            IDatabase database = query.DatabaseProvider;
            DbCommand command = database.CreateSqlStringCommand(string.Empty);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Select {0} ", GetSelectFieldStrings(data.SelectFieldList));
            sb.AppendFormat("From {0} ", data.TableName);
            sb.AppendFormat(GetConditionStrings(data.ConditionList, query.IsParamMode, command, database));
            sb.AppendFormat(GetGroupBy(data.GroupFieldList));
            sb.AppendFormat(GetOrderBy(data.OrderByList));
            sb.Append(BuildTop(query));
            if (!string.IsNullOrEmpty(query.Data.QueryString))
            {
                sb.Append(query.Data.QueryString);
            }
            command.CommandText = sb.ToString();
            return command;
        }

        /// <summary>
        /// 生成取前几条记录脚本
        /// </summary>
        /// <param name="query">查询对象</param>
        public override string BuildTop(Query query)
        {
            if (query.Data.Top > 0)
            {
                return string.Format(" Limit 0,{0}", query.Data.Top);
            }
            return string.Empty;
        }

        /// <summary>
        /// 生成取最后一次插入的自增ID
        /// </summary>
        /// <param name="query">查询对象</param>
        public override string BuildLastAutoIncrementID(Query query)
        {
            return "SELECT last_insert_rowid();";
        }

    }
}