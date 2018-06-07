using System;
using System.Data;
using System.Data.Common;

namespace XCI.Component
{
    [XCIComponent(
        "SQLite数据访问实现",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "2.1.0.0",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "SQLite数据访问实现",
        "XCI.Data.DataLogo.png")]
    public class SqliteDatabase : DatabaseBase, IDatabase
    {
        protected override string ProviderInvariantName
        {
            get { return "System.Data.SQLite"; }
        }

        /// <summary>
        /// 创建新的数据库连接 并设置连接字符串
        /// </summary>
        /// <returns>新的数据库连接</returns>    
        public override DbConnection CreateConnection()
        {
            DbConnection conn = DbFactory.CreateConnection();
            if (conn != null)
            {
                conn.ConnectionString = this.ConnectionString;
                return conn;
            }
            return null;
        }

        /// <summary>
        /// 创建新的命令对象
        /// </summary>
        /// <returns>新的命令对象</returns>
        public override DbCommand CreateCommand()
        {
            return DbFactory.CreateCommand();
        }

        /// <summary>
        /// 创建新的参数对象
        /// </summary>
        /// <returns></returns>
        public override DbParameter CreateParameter()
        {
            return DbFactory.CreateParameter();
        }

        /// <summary>
        /// 创建新的数据适配器
        /// </summary>
        /// <returns>新的数据适配器</returns>
        public override DbDataAdapter CreateDataAdapter()
        {
            return DbFactory.CreateDataAdapter();
        }

        /// <summary>
        /// 添加返回值参数
        /// </summary>
        /// <param name="command">命令对象</param>
        public override DbParameter AddReturnParameter(DbCommand command)
        {
            throw new System.NotSupportedException("Sqlite不支持返回值参数");
        }
    }
}