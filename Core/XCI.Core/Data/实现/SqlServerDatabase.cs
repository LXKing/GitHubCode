using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace XCI.Component
{
    [XCIComponent(
        "SQLServer数据访问实现",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "2.1.0.0",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "SQLServer数据访问实现",
        "XCI.Data.DataLogo.png")]
    public class SqlServerDatabase : DatabaseBase, IDatabase
    {
        /// <summary>
        /// 创建新的数据库连接 并设置连接字符串
        /// </summary>
        /// <returns>新的数据库连接</returns>    
        public override DbConnection CreateConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = this.ConnectionString;
            return conn;
        }

        /// <summary>
        /// 创建新的命令对象
        /// </summary>
        /// <returns>新的命令对象</returns>
        public override DbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        /// <summary>
        /// 创建新的参数对象
        /// </summary>
        /// <returns></returns>
        public override DbParameter CreateParameter()
        {
            return new SqlParameter();
        }

        /// <summary>
        /// 创建新的数据适配器
        /// </summary>
        /// <returns>新的数据适配器</returns>
        public override DbDataAdapter CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }

        /// <summary>
        /// 添加返回值参数
        /// </summary>
        /// <param name="command">命令对象</param>
        public override DbParameter AddReturnParameter(DbCommand command)
        {
            DbParameter parameter = CreateParameter(BuildParameterName("ReturnValue"), DBNull.Value, 0, DbType.String,
               ParameterDirection.ReturnValue, true);
            command.Parameters.Add(parameter);
            return parameter;
        }
    }
}