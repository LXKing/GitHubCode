using System.Data;
using System.Data.Common;
namespace XCI.Component
{
    /// <summary>
    /// 数据访问组件
    /// </summary>
    [XCIComponentDescription("数据访问组件", "系统组件")]
    public interface IDatabase : IManager
    {
        #region 属性

        /// <summary>
        /// 连接字符串
        /// </summary>
        string ConnectionString { get; set; }

        #endregion

        #region 事务

        /// <summary>
        /// 开始事物
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>事务对象</returns>
        DbTransaction BeginTransaction(DbConnection connection);

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction">事务对象</param>
        void CommitTransaction(DbTransaction transaction);

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="transaction">事务对象</param>
        void RollbackTransaction(DbTransaction transaction);

        #endregion

        #region 命令对象

        /// <summary>
        /// 创建SQL语句命令
        /// </summary>
        /// <param name="sqlString">SQL语句</param>               
        DbCommand CreateSqlStringCommand(string sqlString);

        /// <summary>
        /// 创建存储过程命令
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        DbCommand CreateStoredProcCommand(string storedProcName);

        /// <summary>
        /// 创建新的数据库连接 并设置连接字符串
        /// </summary>
        /// <returns>新的数据库连接</returns>    
        DbConnection CreateConnection();

        /// <summary>
        /// 创建新的命令对象
        /// </summary>
        /// <returns>新的命令对象</returns>
        DbCommand CreateCommand();

        /// <summary>
        /// 创建新的参数对象
        /// </summary>
        /// <returns></returns>
        DbParameter CreateParameter();

        /// <summary>
        /// 创建新的数据适配器
        /// </summary>
        /// <returns>新的数据适配器</returns>
        DbDataAdapter CreateDataAdapter();

        #endregion

        #region 参数

        /// <summary>
        /// 生成参数名称
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>正确的格式化参数的名称</returns>
        string BuildParameterName(string name);

        /// <summary>
        /// 添加输入参数
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        DbParameter AddInParameter(DbCommand command, string name, object value);

        /// <summary>
        /// 添加输入参数
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="name">参数名称</param>
        /// <param name="size">参数值长度</param>
        /// <param name="dbType">参数数据类型</param>
        /// <param name="value">参数值</param>
        DbParameter AddInParameter(DbCommand command, string name, object value, int size, DbType dbType);

        /// <summary>
        /// 添加输出参数
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="name">参数名称</param>
        DbParameter AddOutParameter(DbCommand command, string name);

        /// <summary>
        /// 添加返回值参数
        /// </summary>
        /// <param name="command">命令对象</param>
        DbParameter AddReturnParameter(DbCommand command);

        #endregion

        #region 执行方法

        /// <summary>
        /// 执行命令并返回影响的行数
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>返回影响的行数</returns>
        int ExecuteNonQuery(string sqlString);

        /// <summary>
        /// 执行命令并返回影响的行数
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns>返回影响的行数</returns>
        int ExecuteNonQuery(DbCommand command);

        /// <summary>
        /// 执行命令并返回影响的行数
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="transaction">事务对象</param>
        /// <returns>返回影响的行数</returns>
        int ExecuteNonQuery(DbCommand command, DbTransaction transaction);

        /// <summary>
        /// 执行命令并返回第一行第一列数据
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>返回第一行第一列数据</returns>
        object ExecuteScalar(string sqlString);

        /// <summary>
        /// 执行命令并返回第一行第一列数据
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns>返回第一行第一列数据</returns>
        object ExecuteScalar(DbCommand command);

        /// <summary>
        /// 执行命令并返回第一行第一列数据
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="transaction">事务对象</param>
        /// <returns>返回第一行第一列数据</returns>
        object ExecuteScalar(DbCommand command, DbTransaction transaction);

        /// <summary>
        /// 执行命令并返回DataSet
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>返回DataSet</returns>
        DataSet ExecuteDataSet(string sqlString);

        /// <summary>
        /// 执行命令并返回DataSet
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns>返回DataSet</returns>
        DataSet ExecuteDataSet(DbCommand command);

        /// <summary>
        /// 执行命令并返回DataSet
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="transaction">事务对象</param>
        /// <returns>返回DataSet</returns>
        DataSet ExecuteDataSet(DbCommand command, DbTransaction transaction);

        /// <summary>
        /// 执行命令并返回DataTable
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>返回DataTable</returns>
        DataTable ExecuteDataTable(string sqlString);

        /// <summary>
        /// 执行命令并返回DataTable
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns>返回DataTable</returns>
        DataTable ExecuteDataTable(DbCommand command);

        /// <summary>
        /// 执行命令并返回DataTable
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="transaction">事务对象</param>
        /// <returns>返回DataTable</returns>
        DataTable ExecuteDataTable(DbCommand command, DbTransaction transaction);

        #endregion

    }
}