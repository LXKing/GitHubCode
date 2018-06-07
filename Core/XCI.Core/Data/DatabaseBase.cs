using System;
using System.Data;
using System.Data.Common;

namespace XCI.Component
{
    /// <summary>
    /// 数据访问组件基类
    /// </summary>
    public abstract class DatabaseBase : IDatabase
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 提供程序的固定名称
        /// </summary>
        protected virtual string ProviderInvariantName { get; set; }

        private ILog _log;
        protected virtual ILog Log
        {
            get
            {
                if (_log == null)
                {
                    _log = new ConsoleLog();
                }
                return _log;
            }
        }

        private DbProviderFactory dbProviderFactory;
        /// <summary>
        /// 数据对象创建工厂
        /// </summary>
        protected DbProviderFactory DbFactory
        {
            get { return dbProviderFactory ?? (dbProviderFactory = DbProviderFactories.GetFactory(ProviderInvariantName)); }
        }

        #region 事务

        /// <summary>
        /// 开始事物
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>事务对象</returns>
        public DbTransaction BeginTransaction(DbConnection connection)
        {
            DbTransaction tran = connection.BeginTransaction();
            return tran;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction">事务对象</param>
        public void CommitTransaction(DbTransaction transaction)
        {
            transaction.Commit();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="transaction">事务对象</param>
        public void RollbackTransaction(DbTransaction transaction)
        {
            transaction.Rollback();
        }

        #endregion


        /// <summary>
        /// 创建新的数据库连接 并设置连接字符串
        /// </summary>
        /// <returns>新的数据库连接</returns>    
        public abstract DbConnection CreateConnection();

        /// <summary>
        /// 创建新的命令对象
        /// </summary>
        /// <returns>新的命令对象</returns>
        public abstract DbCommand CreateCommand();

        /// <summary>
        /// 创建新的参数对象
        /// </summary>
        /// <returns></returns>
        public abstract DbParameter CreateParameter();

        /// <summary>
        /// 创建新的数据适配器
        /// </summary>
        /// <returns>新的数据适配器</returns>
        public abstract DbDataAdapter CreateDataAdapter();

        /// <summary>
        /// 添加返回值参数
        /// </summary>
        /// <param name="command">命令对象</param>
        public abstract DbParameter AddReturnParameter(DbCommand command);


        /// <summary>
        /// 创建SQL语句命令
        /// </summary>
        /// <param name="sqlString">SQL语句</param>               
        public DbCommand CreateSqlStringCommand(string sqlString)
        {
            return CreateCommandByCommandType(CommandType.Text, sqlString);
        }

        /// <summary>
        /// 创建存储过程命令
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        public DbCommand CreateStoredProcCommand(string storedProcName)
        {
            return CreateCommandByCommandType(CommandType.StoredProcedure, storedProcName);
        }

        /// <summary>
        /// 创建命令对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令内容</param>
        protected DbCommand CreateCommandByCommandType(CommandType commandType, string commandText)
        {
            DbCommand command = CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            return command;
        }



        /// <summary>
        /// 生成参数名称
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>正确的格式化参数的名称</returns>
        public virtual string BuildParameterName(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            const string parameterToken = "@";
            if (name[0].ToString() != parameterToken)
            {
                return name.Insert(0, parameterToken);
            }
            return name;
        }

        /// <summary>
        /// 添加输入参数
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        public DbParameter AddInParameter(DbCommand command, string name, object value)
        {
            return AddInParameter(command, name, value, 4000, DbType.String);
        }

        /// <summary>
        /// 添加输入参数
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="name">参数名称</param>
        /// <param name="size">参数值长度</param>
        /// <param name="dbType">参数数据类型</param>
        /// <param name="value">参数值</param>
        public DbParameter AddInParameter(DbCommand command, string name, object value, int size, DbType dbType)
        {
            DbParameter parameter = CreateParameter(BuildParameterName(name), value, size, dbType,
                ParameterDirection.Input, true);
            command.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// 添加输出参数
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="name">参数名称</param>
        public DbParameter AddOutParameter(DbCommand command, string name)
        {
            DbParameter parameter = CreateParameter(BuildParameterName(name), DBNull.Value, 100, DbType.String,
                ParameterDirection.Output, true);
            command.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="size">参数值长度</param>
        /// <param name="dbType">参数数据类型</param>
        /// <param name="direction">参数方向类型</param>
        /// <param name="nullable">参数是否接受空值</param>
        /// <returns>已初始化的参数对象</returns>
        protected DbParameter CreateParameter(string name, object value, int size, DbType dbType,
                                           ParameterDirection direction, bool nullable)
        {
            DbParameter param = CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            if (size != 0)
            {
                param.Size = size;
            }
            param.Direction = direction;
            param.DbType = dbType;
            param.IsNullable = nullable;
            return param;
        }


        /// <summary>
        /// 执行命令并返回影响的行数
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>返回影响的行数</returns>
        public int ExecuteNonQuery(string sqlString)
        {
            var command = CreateSqlStringCommand(sqlString);
            return ExecuteNonQuery(command);
        }


        /// <summary>
        /// 执行命令并返回影响的行数
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns>返回影响的行数</returns>
        public int ExecuteNonQuery(DbCommand command)
        {
            return ExecuteNonQuery(command, null);
        }

        /// <summary>
        /// 执行命令并返回影响的行数
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="transaction">事务对象</param>
        /// <returns>返回影响的行数</returns>
        public virtual int ExecuteNonQuery(DbCommand command, DbTransaction transaction)
        {
            int rowsAffected = 0;
            if (transaction == null)
            {
                using (DbConnection connection = CreateConnection())
                {
                    connection.Open();
                    PrepareCommand(command, connection);
                    rowsAffected = DoExecuteNonQuery(command);
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                PrepareCommand(command, transaction);
                rowsAffected = DoExecuteNonQuery(command);
            }
            return rowsAffected;
        }

        /// <summary>
        /// 执行命令并返回影响的行数
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns>返回影响的行数</returns>
        protected virtual int DoExecuteNonQuery(DbCommand command)
        {
            try
            {
                int milliStart = Environment.TickCount;
                int rowsAffected = command.ExecuteNonQuery();
                int tickCount = Environment.TickCount;

                Log.Debug(
                    string.Format("SQLExec {0} 执行时间:毫秒{1}",
                    command.CommandText, TimeSpan.FromMilliseconds((tickCount - milliStart)).ToString()));

                return rowsAffected;
            }
            catch (Exception e)
            {
                Log.Debug(e.Message, "Database");
                throw e;
            }
        }

        /// <summary>
        /// 执行命令并返回第一行第一列数据
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>返回第一行第一列数据</returns>
        public object ExecuteScalar(string sqlString)
        {
            var command = CreateSqlStringCommand(sqlString);
            return ExecuteScalar(command);
        }

        /// <summary>
        /// 执行命令并返回第一行第一列数据
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns>返回第一行第一列数据</returns>
        public object ExecuteScalar(DbCommand command)
        {
            return ExecuteScalar(command, null);
        }

        /// <summary>
        /// 执行命令并返回第一行第一列数据
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="transaction">事务对象</param>
        /// <returns>返回第一行第一列数据</returns>
        public object ExecuteScalar(DbCommand command, DbTransaction transaction)
        {
            object result;
            if (transaction == null)
            {
                using (DbConnection connection = CreateConnection())
                {
                    connection.Open();
                    PrepareCommand(command, connection);
                    result = DoExecuteScalar(command);
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                PrepareCommand(command, transaction);
                result = DoExecuteScalar(command);
            }
            return result;
        }

        /// <summary>
        /// 执行命令并返回第一行第一列数据
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns>返回第一行第一列数据</returns>
        protected virtual object DoExecuteScalar(DbCommand command)
        {
            try
            {
                int milliStart = Environment.TickCount;
                object returnValue = command.ExecuteScalar();
                int tickCount = Environment.TickCount;

                Log.Debug(
                    string.Format("SQLExec {0} 执行时间:毫秒{1}",
                    command.CommandText, TimeSpan.FromMilliseconds((tickCount - milliStart)).ToString()));

                return returnValue;
            }
            catch (Exception e)
            {
                Log.Debug(e.Message, "Database");
                throw e;
            }
        }

        /// <summary>
        /// 执行命令并返回DataSet
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>返回DataSet</returns>
        public DataSet ExecuteDataSet(string sqlString)
        {
            var command = CreateSqlStringCommand(sqlString);
            return ExecuteDataSet(command);
        }

        /// <summary>
        /// 执行命令并返回DataSet
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns>返回DataSet</returns>
        public DataSet ExecuteDataSet(DbCommand command)
        {
            return ExecuteDataSet(command, null);
        }

        /// <summary>
        /// 执行命令并返回DataSet
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="transaction">事务对象</param>
        /// <returns>返回DataSet</returns>
        public DataSet ExecuteDataSet(DbCommand command, DbTransaction transaction)
        {
            DataSet ds;
            if (transaction == null)
            {
                using (DbConnection connection = CreateConnection())
                {
                    connection.Open();
                    PrepareCommand(command, connection);
                    ds = DoExecuteDataSet(command);
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                PrepareCommand(command, transaction);
                ds = DoExecuteDataSet(command);
            }
            return ds;
        }

        /// <summary>
        /// 加载数据到指定的DataSet
        /// </summary>
        /// <param name="command">命令对象</param>
        protected virtual DataSet DoExecuteDataSet(DbCommand command)
        {
            DataSet dataSet = new DataSet();
            using (DbDataAdapter adapter = CreateDataAdapter())
            {
                try
                {
                    adapter.SelectCommand = command;
                    command.CommandTimeout = 3000000;
                    int milliStart = Environment.TickCount;
                    adapter.Fill(dataSet);
                    int tickCount = Environment.TickCount;
                    Log.Debug(
                        string.Format("SQLExec {0} 执行时间:毫秒{1}",
                        command.CommandText, TimeSpan.FromMilliseconds((tickCount - milliStart)).ToString()));
                }
                catch (Exception e)
                {
                    Log.Debug(e.Message, "Database");
                    throw e;
                }
            }
            return dataSet;
        }

        /// <summary>
        /// 执行命令并返回DataTable
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>返回DataTable</returns>
        public DataTable ExecuteDataTable(string sqlString)
        {
            var command = CreateSqlStringCommand(sqlString);
            return ExecuteDataTable(command);
        }

        /// <summary>
        /// 执行命令并返回DataTable
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns>返回DataTable</returns>
        public DataTable ExecuteDataTable(DbCommand command)
        {
            return ExecuteDataTable(command, null);
        }

        /// <summary>
        /// 执行命令并返回DataTable
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="transaction">事务对象</param>
        /// <returns>返回DataTable</returns>
        public virtual DataTable ExecuteDataTable(DbCommand command, DbTransaction transaction)
        {
            return ExecuteDataSet(command, transaction).Tables[0];
        }



        /// <summary>
        /// 指定命令对象的数据连接
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="connection">数据连接</param>
        protected void PrepareCommand(DbCommand command,
                                             DbConnection connection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (connection == null) throw new ArgumentNullException("connection");

            command.Connection = connection;
        }

        /// <summary>
        /// 指定命令对象的事务和数据连接
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="transaction">事务对象</param>
        protected void PrepareCommand(DbCommand command,
                                             DbTransaction transaction)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (transaction == null) throw new ArgumentNullException("transaction");

            PrepareCommand(command, transaction.Connection);
            command.Transaction = transaction;
        }
    }
}