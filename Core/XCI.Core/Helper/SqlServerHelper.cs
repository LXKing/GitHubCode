using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using System.IO;
namespace XCI.Helper
{
    /// <summary>
    /// SqlServer2000/2005 数据访问基础类
    /// </summary>
    public static class SqlServerHelper
    {
        /// <summary>
        /// 读取或者设置sql2000/sql2005数据库连接字符串
        /// </summary>
        public static string ConnectionString { get; set; }

        #region 公用方法
        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        public static bool ColumnExists(string tableName, string columnName)
        {
            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            object res = GetSingle(sql);
            if (res == null)
            {
                return false;
            }
            return Convert.ToInt32(res) > 0;
        }
        /// <summary>
        /// 获取数据库某个字段的最大值
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="TableName">表名</param>
        /// <returns>返回经过计算的最大值</returns>
        public static int GetMax(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = SqlServerHelper.GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 判断某条数据是否存在
        /// </summary>
        /// <param name="strSql">要检测得sql语句</param>
        /// <param name="cmdParms">sql参数</param>
        /// <returns>数据不存在返回false,存在返回true</returns>
        public static bool Exists(string strSql,SqlParameter[] cmdParms)
        {
            object obj = SqlServerHelper.GetSingle(strSql,cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns>存在返回真</returns>
        public static bool TabExists(string TableName)
        {
            string strsql = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
            //string strsql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]') AND type in (N'U')";
            object obj = SqlServerHelper.GetSingle(strsql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行sql语句,限定超时时间,过期自动返回,并抛出异常
        /// </summary>
        /// <remarks>
        /// 防止数据过大导致无响应
        /// </remarks>
        /// <param name="SQLString">sql语句</param>
        /// <param name="Times">超时时间(默认是秒)</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteSqlByTime(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
      
       
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>	
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch
                {
                    tx.Rollback();
                    return 0;
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static object ExecuteSqlGet(string SQLString, string content)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 在限定的超时时间内,获取数据库表中一条记录中的一个字段值
        /// <remarks>
        /// 防止数据过大导致无响应
        /// </remarks>
        /// </summary>
        /// <param name="SQLString">查询语句sql</param>
        /// <param name="Times">超时时间</param>
        /// <returns>返回要获取的对象值</returns>
        public static object GetSingle(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSQL)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }   

        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }
        /// <summary>
        /// 在限定的超时时间内,获取一个DataSet
        /// </summary>
        /// <remarks>
        /// 防止数据过大导致无响应
        /// </remarks>
        /// <param name="SQLString">sql查询语句</param>
        /// <param name="Times">超时时间</param>
        /// <returns>返回数据DataSet</returns>
        public static DataSet Query(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }



        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="cmdParms">查询参数</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int indentity = 0;
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="s">为null,为扩展参数</param>
        /// <param name="cmdParms">查询参数</param>
        /// <returns>查询结果（object）</returns>
        public static string GetSingle(string SQLString,string s, params SqlParameter[] cmdParms)
        {
            object obj = GetSingle(SQLString, cmdParms);
            if (obj == null)
                return string.Empty;
            else
                return obj.ToString();
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="cmdParms">查询参数</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <param name="cmdParms">查询参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
            //			finally
            //			{
            //				cmd.Dispose();
            //				connection.Close();
            //			}	

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <param name="cmdParms">查询参数</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        /// <summary>
        /// 构建sql/存储过程/参数(内部使用)
        /// </summary>
        /// <param name="cmd">SqlCommand 命令对象</param>
        /// <param name="conn">SqlConnection 连接对象</param>
        /// <param name="trans">SqlTransaction 事务对象</param>
        /// <param name="cmdText">sql语句/或者存储过程名称</param>
        /// <param name="cmdParms">参数</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            //if (conn.ConnectionString == null || conn.ConnectionString.Equals(""))
            //{                
            //    conn.ConnectionString = PubConstant.ConnectionString;
            //}
            //System.IO.File.WriteAllText("c:\\a.txt", "conn.ConnectionString=" + conn.ConnectionString + "\nPubConstant.ConnectionString=" + PubConstant.ConnectionString + "\nAppConfig.ConnectionString=" + AppConfig.ConnectionString);
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        #endregion

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;
            
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }
        /// <summary>
        /// 运行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">表名</param>
        /// <param name="Times">超时时间</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.SelectCommand.CommandTimeout = Times;
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }


        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.FieldValue.
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                            (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }
            }

            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns>返回存储过程的返回值</returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                command.CommandTimeout = 0;
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion

        #region 分页操作
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="tablename">表名字</param>
        /// <param name="primarykey">表主键</param>
        /// <param name="column">显示的列</param>
        /// <param name="sortfiled">排序字段</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="sordir">排序方式 非0 值则降序</param>
        /// <param name="search">搜索条件</param>
        /// <param name="Group">分组字段</param>
        /// <param name="outstring">返回的记录总数</param>      
        /// <returns>返回查询结果</returns>
        public static DataSet GetPageList(string tablename, string primarykey, string sortfiled, string sordir, int pageindex, int pagesize, string column, string search, string Group, out string outstring)
        {
            /*
            参数说明:
            1.Tables :表名称,视图
            2.PrimaryKeyFieldName :主关键字
            3.Sort :排序语句，不带Order By 比如：NewsID Desc,OrderRows Asc
            4.CurrentPage :当前页码
            5.PageSize :分页尺寸
            6.Fields :显示的字段
            7.Filter :过滤语句，不带Where 
            8.Group :Group语句,不带Group By
            9.@RecordCount 输出总记录数
            效果演示：http://www.cn5135.com/_App/Enterprise/QueryResult.aspx
                @Tables varchar(1000),
                @PrimaryKeyFieldName varchar(100),
                @Sort varchar(200) = NULL,
                @CurrentPage int = 1,
                @PageSize int = 10,
                @Fields varchar(1000) = '*',
                @Filter varchar(1000) = NULL,
                @Group varchar(1000) = NULL,
                @RecordCount int =0 OUTPUT
            */
            SqlParameter[] parameters ={		   
		   new SqlParameter("@Tables",SqlDbType.VarChar,1000),
		   new SqlParameter("@PrimaryKeyFieldName",SqlDbType.VarChar,100),
		   new SqlParameter("@Sort",SqlDbType.VarChar,200),
		   new SqlParameter("@CurrentPage",SqlDbType.Int,4),
		   new SqlParameter("@PageSize",SqlDbType.Int,4),
		   new SqlParameter("@Fields",SqlDbType.VarChar,1000),
		   new SqlParameter("@Filter",SqlDbType.VarChar,1000),
		   new SqlParameter("@Group",SqlDbType.VarChar,1000),
           new SqlParameter("@RecordCount",SqlDbType.Int,4)
        };
            string paixun = sortfiled + " " + sordir;
            parameters[0].Value = tablename;//表名称,视图
            parameters[1].Value = primarykey;//主关键字
            parameters[2].Value = paixun;//排序语句，不带Order By 比如：NewsID Desc,OrderRows Asc
            parameters[3].Value = pageindex;//当前页码
            parameters[4].Value = pagesize;//分页尺寸
            parameters[5].Value = column;//显示的字段
            parameters[6].Value = search;//过滤语句，不带Where
            parameters[7].Value = Group;//Group语句,不带Group By
            parameters[8].Direction = ParameterDirection.Output;//输出总记录数
            DataSet ds = SqlServerHelper.RunProcedure("PR_Page", parameters, "ds");
            outstring = parameters[8].Value.ToString();
            return ds;
        }
        #endregion 

        #region sql源码操作
        /// <summary>
        /// 获取指定表/视图的元数据信息(是否主键,注释说明)
        /// </summary>
        /// <param name="tableName">表或视图名称</param>
        /// <returns>datatable</returns>
        public static DataTable GetTableColumnInfo(string tableName)
        {
            #region 查询sql
            string sql = @"SELECT    a.colorder   ids,   a.name 
  name, (case   when   (SELECT   count(*) FROM   sysobjects WHERE 
  (name   in  (SELECT   name  FROM   sysindexes WHERE   (id   =   a.id)   
AND   (indid   in    (SELECT   indid FROM   sysindexkeys    WHERE   
(id   =   a.id)   AND   (colid   in (SELECT   colid     FROM   syscolumns 
  WHERE   (id   =   a.id)   AND (name   =   a.name)))))))   
AND    (xtype   =   'PK'))>0   then   'true'   else   'false'   end) 
                    pk,b.name   dbType,  COLUMNPROPERTY(a.id,a.name,'PRECISION')
   as   lengths, isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0)   as   decimallength, 
(case   when   a.isnullable=1   then   'true' else 'false'   end)   [isnull],
 isnull(e.text,'')   defaultvalue,   isnull(g.[value],'')   AS   remark FROM   
  syscolumns     a   left   join   systypes   b    on   
  a.xtype=b.xusertype inner   join   sysobjects   d     on   a.id=d.id   
  and     (d.xtype='U' or d.xtype='V') and     d.name<>'dtproperties'   
 left   join   syscomments   e  on   a.cdefault=e.id left   join  
 sysproperties   g    on   a.id=g.id   
AND   a.colid   =   g.smallid where 
d.name   =   '" + tableName + @"'  order   by   a.id,a.colorder ";
            #endregion
            DataTable dt = SqlServerHelper.Query(sql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 返回表的说明Dt
        /// </summary>
        /// <returns>返回表的说明</returns>
        public static DataTable GetTableComment(DatabaseType db)
        {
            DataTable dt = null;
            string sql = "";
            switch (db)
            {
                case DatabaseType.SQL2000:                   
                    sql = @"select b.name,a.value from sysproperties a,sysobjects b where a.id=b.id and smallid=0";
                    break;
                case DatabaseType.SQL2005:
                    sql = @"select b.name,a.value from sys.extended_properties a,sysobjects b where  xtype='U' and b.name<>'dtproperties' and b.id=a.major_id and minor_id='0'";

                    break;
                default:
                    break;
            }

            dt = SqlServerHelper.Query(sql).Tables[0];

            return dt;
        }
        

        /// <summary>
        /// 获取指定表/视图的列信息列表
        /// </summary>
        /// <param name="tableName">表或视图名称</param>
        /// <param name="isname">扩展</param>
        /// <returns>列数组</returns>
        public static string[] GetTableColumnList(string tableName,object isname)
        {
            string res = string.Empty;
            ArrayList list = new ArrayList();
            DataTable dt = GetTableColumnInfo(tableName);
            int count = dt.Rows.Count;
            string Name;
            DataRow dr;
            for (int i = 0; i < count; i++)
            {
                dr = dt.Rows[i];
                Name = dr[1].ToString();
                list.Add(Name);
            }
            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// 获取指定表/视图的列信息列表
        /// </summary>
        /// <param name="tableName">表或视图名称</param>
        /// <returns>列数组</returns>
        public static string[] GetTableColumnList(string tableName)
        {
            string res = string.Empty;
            ArrayList list = new ArrayList();
            DataTable dt = GetTableColumnInfo(tableName);
            int count = dt.Rows.Count;
            string Name;
            DataRow dr;
            for (int i = 0; i < count; i++)
            {
                dr = dt.Rows[i];
                Name = dr[1].ToString();
                string key = dr[2].ToString();
                string Datatype = dr[3].ToString();
                string Length = dr[4].ToString();
                string AllowNulls = dr[6].ToString();
                string Remark = dr[8].ToString();
                Remark = Remark.Length == 0 ? "" : "(" + Remark + "),";
                key = key.Length == 0 ? "" : "(主键),";
                AllowNulls = AllowNulls.Length == 0 ? "Not Null" : "Null";
                list.Add(Name + "(" + Datatype + "(" + Length + ")," + key + Remark + (AllowNulls) + ")");
            }
            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// 网络所有数据库服务器
        /// </summary>
        /// <returns>服务器数组</returns>
        public static string[] GetAllDatabaseServerList()
        {
            SqlDataSourceEnumerator sqlServer = SqlDataSourceEnumerator.Instance;
            DataTable db = sqlServer.GetDataSources();
            string[] Name = new string[db.Rows.Count + 1];
            Name[0] = "(Local) ";
            int i = 1;
            foreach (DataRow row in db.Rows)
            {
                Name[i] = row["ServerName"].ToString();
                i++;
            }
            return Name;
        }

        /// <summary>
        /// 获取当前服务器中所有数据库列表
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllDatabaseList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select name from master..sysdatabases where dbid>4");
            DataTable dt = SqlServerHelper.Query(sb.ToString()).Tables[0];
            ArrayList list = new ArrayList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                list.Add(dr[0]);
            }

            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// 获取指定数据库中的所有存储过程
        /// </summary>
        /// <returns>存储过程列表</returns>
        public static string[] GetAllProcedureList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from sysobjects where xtype='p' and substring(name,0,3)!='dt'");
            DataTable dt = SqlServerHelper.Query(sb.ToString()).Tables[0];
            ArrayList list = new ArrayList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                list.Add(dr[0]);
            }

            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }
        /// <summary>
        /// 获取指定数据库中的所有函数
        /// </summary>
        /// <returns>函数列表</returns>
        public static string[] GetAllFunctionList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select name,id from sysobjects where xtype='fn'");
            DataTable dt = SqlServerHelper.Query(sb.ToString()).Tables[0];
            ArrayList list = new ArrayList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                list.Add(dr[0]);
            }
            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }
        /// <summary>
        /// 获取指定数据库中的所有触发器
        /// </summary>
        /// <returns>触发器列表</returns>
        public static string[] GetAllTriggerList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select name,id from sysobjects where xtype='tr'");
            DataTable dt = SqlServerHelper.Query(sb.ToString()).Tables[0];
            ArrayList list = new ArrayList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                list.Add(dr[0]);
            }
            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// 获取指定数据库中的所有表
        /// </summary>    
        /// <returns>表数组</returns>
        public static string[] GetAllTableList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from sysobjects where xtype='u' and name!='dtproperties'");
            DataTable dt = SqlServerHelper.Query(sb.ToString()).Tables[0];
            ArrayList list = new ArrayList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                list.Add(dr[0]);
            }

            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// 获取指定数据库中的所有视图
        /// </summary>
        /// <returns>视图数组</returns>
        public static string[] GetAllViewList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from sysobjects where xtype='v' and substring([name],0,4)!='sys'");
            DataTable dt = SqlServerHelper.Query(sb.ToString()).Tables[0];
            ArrayList list = new ArrayList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                list.Add(dr[0]);
            }

            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// 获取指定数据库中的所有用户
        /// </summary>
        /// <param name="DatabaseName">数据库名称</param>
        /// <returns>用户数组</returns>
        public static string[] GetAllUserList(string DatabaseName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select name from sysusers where issqluser=1");
            DataTable dt = SqlServerHelper.Query(sb.ToString()).Tables[0];
            ArrayList list = new ArrayList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                list.Add(dr[0]);
            }

            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }
        /// <summary>
        /// 获取存储过程的参数
        /// </summary>
        /// <param name="procedureName">存储过程名称</param>
        /// <returns>存储过程参数数组</returns>
        public static string[] GetProcedureParameter(string procedureName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT a.name AS p_name,b.name AS p_type,a.length AS p_length,a.isoutparam AS p_isout ");
            sb.Append(" FROM syscolumns a, systypes b WHERE a.xtype=b.xtype AND b.name<>'sysname' ");
            sb.Append(" AND id = (select id from sysobjects where name = '" + procedureName + "') ");

            ArrayList list = new ArrayList();
            list.Add("@RETURN_VALUE(int(4),返回值)");
            DataTable dt = SqlServerHelper.Query(sb.ToString()).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                list.Add(dr["p_name"].ToString() + "(" + dr["p_type"].ToString() + "(" + dr["p_length"].ToString() + ")," + (dr["p_isout"].ToString().Equals("1") ? "输出" : "输入") + ")");
            }


            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }
        /// <summary>
        /// 创建用户脚本
        /// </summary>
        /// <param name="uid">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns>返回创建用户的脚本</returns>
        public static string GetUserCreateCode(string uid, string pwd)
        {
            //EXEC   sp_addlogin   'a ' ,'1' --创建登陆
            //EXEC   sp_adduser   'a ' --创建用户
            string s1 = "EXEC sp_addlogin '" + uid + "' ,'" + pwd + "'\n";
            string s2 = "EXEC sp_adduser '" + uid + "'";
            //ESqlServer.ExecuteSql(s1);
            //ESqlServer.ExecuteSql(s2);
            return s1+s2;
        }
        /// <summary>
        /// 获取授权代码
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="table">表名/视图名/存储过程名</param>
        /// <param name="qx">权限</param>
        /// <returns>授权代码</returns>
        public static string GetGrantCode(string user, string table, string qx)
        {
            string str = "grant {0} on {1} to {2} --授权用户{3}对象{4}的{5}权限";
            str = string.Format(str, qx, table, user, user, table, qx);
            return str;
        }
        /// <summary>
        /// 撤销授权代码
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="table">表名/视图名/存储过程名</param>
        /// <param name="qx">权限</param>
        /// <returns>撤销授权代码</returns>
        public static string GetRevokeCode(string user, string table, string qx)
        {
            string str = "revoke {0} on {1} to {2} --撤销用户{3}对象{4}的{5}权限";
            str = string.Format(str, qx, table, user, user, table, qx);
            return str;
        }        

        /// <summary>
        /// 获取授权系统权限代码
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="qx">权限</param>
        /// <returns>授权系统权限代码</returns>
        public static string GetGrantCode(string user, string qx)
        {
            string str = "grant {0} to {1} --授权用户{2}的{3}权限";
            str = string.Format(str, qx, user, user, qx);
            return str;
        }
        /// <summary>
        /// 撤销系统权限代码
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="qx">权限</param>
        /// <returns>撤销系统权限代码</returns>
        public static string GetRevokeCode(string user, string qx)
        {
            string str = "revoke {0} to {1} --撤销用户{2}的{3}权限";
            str = string.Format(str, qx, user, user, qx);
            return str;
        }
        /// <summary>
        /// 获取规则、默认值、未加密的存储过程、用户定义函数、触发器或视图的创建文本。
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static string GetCode(string objName)
        {
            StringBuilder sb = new StringBuilder();
            
            //SqlParameter[] sp = { 
            //                    new SqlParameter("@objname",SqlDbType.NVarChar)
            //                    };
            //sp[0].FieldValue = objName;
            //DataSet ds = ESqlServer.RunProcedure("sp_helptext", sp, "ds");//ESqlServer.Query(sql);
            string sql = "EXEC sp_helptext '" + objName + "'";
            DataSet ds = SqlServerHelper.Query(sql);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sb.Append(dr[0].ToString());
            }
            return sb.ToString();
        }



        #endregion

        #region 备份恢复


        /// <summary>
        /// 备份sql2000数据库
        /// </summary>
        /// <param name="dbname">要备份的数据库名称</param>
        /// <param name="path">备份文件的路径包括文件名(使用server mappath)</param>
        /// <returns>成功返回true</returns>
        public static bool Backup(string dbname, string path)
        {
            string tempconn = "";// ConnectionString.Replace(AppConfig.Database, "master");
            using (SqlConnection connection = new SqlConnection(tempconn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.CommandText = @"backup database " + dbname + @" to disk='" + path + @"' with init";

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        //return false;
                        //throw new Exception(ex.Message);

                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }

                }
            }

        }

        /// <summary>
        /// 还原sql2000数据库
        /// </summary>
        /// <param name="dbname">数据库名称</param>
        /// <param name="path">备份文件路径包括文件名</param>
        /// <returns>成功返回true</returns>
        public static bool Restore(string dbname, string path)
        {
            string tempconn = "";// ConnectionString.Replace(AppConfig.Database, "master");
            using (SqlConnection connection = new SqlConnection(tempconn))
            {
                connection.Open();

                //KILL DataBase Process
                SqlCommand cmd = new SqlCommand("SELECT spid FROM sysprocesses ,sysdatabases WHERE sysprocesses.dbid=sysdatabases.dbid AND sysdatabases.Name='" + dbname + "'", connection);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                ArrayList list = new ArrayList();
                while (dr.Read())
                {
                    list.Add(dr.GetInt16(0));
                }
                dr.Close();
                for (int i = 0; i < list.Count; i++)
                {
                    cmd = new SqlCommand(string.Format("KILL {0}", list[i]), connection);
                    cmd.ExecuteNonQuery();
                }

                SqlCommand cmdRT = new SqlCommand();
                cmdRT.CommandType = CommandType.Text;
                cmdRT.Connection = connection;
                cmdRT.CommandText = @"restore database " + dbname + @" from disk='" + path + @"'";

                try
                {
                    cmdRT.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                    //throw new Exception(ex.Message);
                    //return false;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }


            }
        }

        #endregion

        #region AddParameter
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        /// <returns>返回这个参数</returns>
        public static SqlParameter AddParameter(string name, SqlDbType type, object value)
        {
            SqlParameter prm = new SqlParameter();
            prm.Direction = ParameterDirection.Input;
            prm.ParameterName = name;
            prm.SqlDbType = type;
            prm.Value = PrepareSqlValue(value);
            return prm;
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        /// <param name="convertZeroToDBNull">如果对象为空,是否转为DBNull.FieldValue</param>
        /// <returns>返回这个参数</returns>
        public static SqlParameter AddParameter(string name, SqlDbType type, object value, bool convertZeroToDBNull)
        {
            SqlParameter prm = new SqlParameter();
            prm.Direction = ParameterDirection.Input;
            prm.ParameterName = name;
            prm.SqlDbType = type;
            prm.Value = PrepareSqlValue(value, convertZeroToDBNull);
            return prm;
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        /// <param name="convertZeroToDBNull">如果对象为空,是否转为DBNull.FieldValue</param>
        /// <returns>返回这个参数</returns>
        public static SqlParameter AddParameter(string name, DbType type, object value, bool convertZeroToDBNull)
        {
            SqlParameter prm = new SqlParameter();
            prm.Direction = ParameterDirection.Input;
            prm.ParameterName = name;
            prm.DbType = type;
            prm.Value = PrepareSqlValue(value, convertZeroToDBNull);
            return prm;
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        /// <param name="size">大小</param>
        /// <returns>返回这个参数</returns>
        public static SqlParameter AddParameter(string name, SqlDbType type, object value, int size)
        {
            SqlParameter prm = new SqlParameter();
            prm.Direction = ParameterDirection.Input;
            prm.ParameterName = name;
            prm.SqlDbType = type;
            prm.Size = size;
            prm.Value = PrepareSqlValue(value);
            return prm;
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        /// <param name="direction">输入还是输出</param>
        /// <returns>返回这个参数</returns>
        public static SqlParameter AddParameter(string name, SqlDbType type, object value, ParameterDirection direction)
        {
            SqlParameter prm = new SqlParameter();
            prm.Direction = direction;
            prm.ParameterName = name;
            prm.SqlDbType = type;
            prm.Value = PrepareSqlValue(value);
            return prm;
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        /// <param name="size">大小</param>
        /// <param name="direction">输入还是输出</param>
        /// <returns>返回这个参数</returns>
        public static SqlParameter AddParameter(string name, SqlDbType type, object value, int size, ParameterDirection direction)
        {
            SqlParameter prm = new SqlParameter();
            prm.Direction = direction;
            prm.ParameterName = name;
            prm.SqlDbType = type;
            prm.Size = size;
            prm.Value = PrepareSqlValue(value);
            return prm;
        }
        /// <summary>
        /// 构建参数值
        /// </summary>
        /// <param name="value">参数值</param>
        /// <returns>参数对象</returns>
        private static object PrepareSqlValue(object value)
        {
            return PrepareSqlValue(value, false);
        }
        /// <summary>
        /// 构建参数值
        /// </summary>
        /// <param name="value">参数值</param>
        /// <param name="convertZeroToDBNull">如果对象为空,是否转为DBNull.FieldValue</param>
        /// <returns>参数对象</returns>
        private static object PrepareSqlValue(object value, bool convertZeroToDBNull)
        {
            if (value is String)
            {
                if ((string)value == String.Empty)
                {
                    return DBNull.Value;
                }
                else
                {
                    return value;
                }
            }
            else if (value is Guid)
            {
                if ((Guid)value == Guid.Empty)
                {
                    return DBNull.Value;
                }
                else
                {
                    return value;
                }
            }
            else if (value is DateTime)
            {
                if (((DateTime)value == DateTime.MinValue)
                    || ((DateTime)value == DateTime.MaxValue))
                {
                    return DBNull.Value;
                }
                else
                {
                    return value;
                }
            }
            else if (value is Int16)
            {
                if (((Int16)value == Int16.MinValue)
                    || ((Int16)value == Int16.MaxValue)
                    || (convertZeroToDBNull && (Int16)value == 0))
                {
                    return DBNull.Value;
                }
                else
                {
                    return value;
                }
            }
            else if (value is Int32)
            {
                if (((Int32)value == Int32.MinValue)
                    || ((Int32)value == Int32.MaxValue)
                    || (convertZeroToDBNull && (Int32)value == 0))
                {
                    return DBNull.Value;
                }
                else
                {
                    return value;
                }
            }
            else if (value is Int64)
            {
                if (((Int64)value == Int64.MinValue)
                    || ((Int64)value == Int64.MaxValue)
                    || (convertZeroToDBNull && (Int64)value == 0))
                {
                    return DBNull.Value;
                }
                else
                {
                    return value;
                }
            }
            else if (value is Single)
            {
                if (((Single)value == Single.MinValue)
                    || ((Single)value == Single.MaxValue)
                    || (convertZeroToDBNull && (Single)value == 0))
                {
                    return DBNull.Value;
                }
                else
                {
                    return value;
                }
            }
            else if (value is Double)
            {
                if (((Double)value == Double.MinValue)
                    || ((Double)value == Double.MaxValue)
                    || (convertZeroToDBNull && (Double)value == 0))
                {
                    return DBNull.Value;
                }
                else
                {
                    return value;
                }
            }
            else if (value is Decimal)
            {
                if (((Decimal)value == Decimal.MinValue)
                    || ((Decimal)value == Decimal.MaxValue)
                    || (convertZeroToDBNull && (Decimal)value == 0))
                {
                    return DBNull.Value;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }
        #endregion AddParameter


        #region 生成表数据
        /// <summary>
        /// 生成表数据
        /// </summary>
        /// <param name="tablename">表名称</param>
        /// <param name="newtablename">生成表的名称</param>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="isaddpk">是否自动添加一个不重复的主键列</param>
        /// <param name="isadddate">是否自动增加一个添加日期</param>
        /// <param name="numberlist">数字型字段列表</param>
        /// <param name="datelist">日期型字段列表</param>
        /// <param name="varcharlist">字符型字段列表</param>
        /// <param name="columnduiying">列对应名称(key 原列名,value新列名)</param>
        /// <param name="column">显示的列(col1,col2)(*)</param>
        /// <param name="top">显示前几条</param>
        /// <param name="where">查询条件,不带where</param>
        /// <param name="isbigdata">是否是大数据,超过5万的数据量请选true</param>
        /// <param name="path">生成文件保存路径</param>
        /// <returns></returns>
        public static bool BuildData(string tablename, string newtablename, DatabaseType dbtype, bool isaddpk, bool isadddate, List<string> numberlist, List<string> datelist, List<string> varcharlist,Hashtable columnduiying, List<string> column, int top, string where, bool isbigdata, string path)
        {
          
            string[] columns = SqlServerHelper.GetTableColumnList("BYXueYuanPXJLWHTest", null);
            
            int counts = 0;
            int pagesize = 50000;
            int temppagecount = 0;
            int pagecount = 0;
            int row = 0;
            SqlServerHelper.RunProcedure("clean", null, out row);
            SqlParameter[] sp = 
            {
                new SqlParameter("@count",SqlDbType.Int,4)
            };
            sp[0].Direction = ParameterDirection.Output;
            SqlServerHelper.RunProcedure("CreateTemp", sp, out row);
            counts = Convert.ToInt32(sp[0].Value);
            temppagecount = counts / pagesize;
            pagecount = counts % pagesize == 0 ? temppagecount : temppagecount + 1;

            StringBuilder columnsb = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            StringBuilder valuesb = null;
            StreamWriter sw = File.AppendText("c:\\t.sql");
            DataSet ds = null;
            DataTable dt = null;
            foreach (string item in columns)
            {
                columnsb.Append(item);
                columnsb.Append(",");
            }
            string t = "insert into BYXueYuanPXJLWHTest ({0}) values({1})";
            string tempvalue;
            decimal indu = 0;
            valuesb = new StringBuilder();
            for (int j = 1; j <= pagecount; j++)
            {
                SqlParameter[] sp1 = 
                {
                    new SqlParameter("@pagesize",SqlDbType.Int,4),
                    new SqlParameter("@pageindex",SqlDbType.Int,4)
                };
                sp1[0].Value = pagesize;
                sp1[1].Value = j;
                ds = SqlServerHelper.RunProcedure("GetPageList", sp1, "ds", 0);
                dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    valuesb.Remove(0, valuesb.Length);
                    foreach (string item in columns)
                    {
                        tempvalue = dr[item].ToString();
                        if (dr.IsNull(item))
                        {
                            tempvalue = "null";
                        }
                        valuesb.Append(tempvalue);
                        valuesb.Append(",");
                    }
                    sb.Append(string.Format(t, columnsb.ToString(), valuesb.ToString()));
                    sb.Append("\n");
                    sw.WriteLine(sb.ToString());
                    sb.Remove(0, sb.Length);
                    indu = ((decimal)(j) / (decimal)pagecount) * 100;
                    //this.Invoke(new Action<int>(this.Changejingdu), (int)indu);                    
                }
            }
            dt.Dispose();
            ds.Dispose();
            sw.Flush();
            sw.Close();
            sw.Dispose();
            sw = null;
            SqlServerHelper.RunProcedure("clean", null, out row);           
            
            return true;
        }
        #endregion

        /// <summary>
        /// 获取数据库的提供程序,通过数据库类型
        /// </summary>
        /// <param name="databasetype">数据库类型</param>
        /// <returns></returns>
        public static string GetDataProviderNameByDatabaseType(DatabaseType databasetype)
        {
            string res = "";
            switch (databasetype)
            {
                case DatabaseType.SQL2000:
                case DatabaseType.SQL2005:
                case DatabaseType.SQL2008:
                    res = "System.Data.SqlClient";
                    break;
                case DatabaseType.ACCESS:
                    res = "System.Data.OleDb";
                    break;
                case DatabaseType.ORACEL:
                    res = "System.Data.OracleClient";
                    break;
                case DatabaseType.MySql:
                    res = "MySql.Data.MySqlClient";
                    break;
                case DatabaseType.SQLite:
                    res = "System.Data.SQLite";
                    break;
            }
            return res;
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="dbtype">数据库类型(sql,access)</param>
        /// <param name="yztype">验证方式(window,sql)</param>
        /// <param name="dbpath">Access数据库路径</param>
        /// <param name="uid">帐号</param>
        /// <param name="pwd">密码</param>
        /// <param name="database">数据库名称</param>
        /// <param name="server">服务器地址</param>
        /// <returns>返回对应的连接字符串</returns>
        public static string GetConnectionString(DatabaseType dbtype, YanZhengType yztype, string dbpath, string uid, string pwd, string database, string server)
        {
            string connectionstring = string.Empty;
            if ((dbtype == DatabaseType.SQL2000) || ((dbtype == DatabaseType.SQL2005)))
            {
                if (yztype == YanZhengType.SQLSERVER)
                {
                    connectionstring = string.Format("server={0};uid={1};pwd={2};database={3}", server, uid, pwd, database);
                }
                else if (yztype == YanZhengType.Windows)
                {
                    connectionstring = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", server, database);
                }
            }
            else if (dbtype == DatabaseType.ACCESS)
            {
                connectionstring = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password={1}", dbpath, pwd);
            }
            else if (dbtype == DatabaseType.ORACEL)
            {
                connectionstring = string.Format("server={0};uid={1};pwd={2}", server, uid, pwd);
            }
            return connectionstring;
        }
    }
    
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// SQL2000数据库
        /// </summary>
        SQL2000,
        /// <summary>
        /// SQL2005数据库
        /// </summary>
        SQL2005,
        /// <summary>
        /// SQL2008数据库
        /// </summary>
        SQL2008,
        /// <summary>
        /// ACCESS数据库
        /// </summary>
        ACCESS,
        /// <summary>
        /// Oracle数据库
        /// </summary>
        ORACEL,
        /// <summary>
        /// MySql数据库
        /// </summary>
        MySql,
        /// <summary>
        /// SQLite数据库
        /// </summary>
        SQLite
    }
   
    /// <summary>
    /// 数据库验证方式
    /// </summary>
    public enum YanZhengType
    {
        /// <summary>
        /// Window验证方式
        /// </summary>
        Windows,
        /// <summary>
        /// Sql Server验证方式
        /// </summary>
        SQLSERVER
    }
}
