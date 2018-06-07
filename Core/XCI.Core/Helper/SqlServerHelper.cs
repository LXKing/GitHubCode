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
    /// SqlServer2000/2005 ���ݷ��ʻ�����
    /// </summary>
    public static class SqlServerHelper
    {
        /// <summary>
        /// ��ȡ��������sql2000/sql2005���ݿ������ַ���
        /// </summary>
        public static string ConnectionString { get; set; }

        #region ���÷���
        /// <summary>
        /// �ж��Ƿ����ĳ���ĳ���ֶ�
        /// </summary>
        /// <param name="tableName">������</param>
        /// <param name="columnName">������</param>
        /// <returns>�Ƿ����</returns>
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
        /// ��ȡ���ݿ�ĳ���ֶε����ֵ
        /// </summary>
        /// <param name="FieldName">�ֶ���</param>
        /// <param name="TableName">����</param>
        /// <returns>���ؾ�����������ֵ</returns>
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
        /// �ж�ĳ�������Ƿ����
        /// </summary>
        /// <param name="strSql">Ҫ����sql���</param>
        /// <param name="cmdParms">sql����</param>
        /// <returns>���ݲ����ڷ���false,���ڷ���true</returns>
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
        /// ���Ƿ����
        /// </summary>
        /// <param name="TableName">����</param>
        /// <returns>���ڷ�����</returns>
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

        #region  ִ�м�SQL���

        /// <summary>
        /// ִ��SQL��䣬����Ӱ��ļ�¼��
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>Ӱ��ļ�¼��</returns>
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
        /// ִ��sql���,�޶���ʱʱ��,�����Զ�����,���׳��쳣
        /// </summary>
        /// <remarks>
        /// ��ֹ���ݹ���������Ӧ
        /// </remarks>
        /// <param name="SQLString">sql���</param>
        /// <param name="Times">��ʱʱ��(Ĭ������)</param>
        /// <returns>����Ӱ�������</returns>
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
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="SQLStringList">����SQL���</param>	
        /// <returns>������Ӱ�������</returns>
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
        /// ִ�д�һ���洢���̲����ĵ�SQL��䡣
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <param name="content">��������,����һ���ֶ��Ǹ�ʽ���ӵ����£���������ţ�����ͨ�������ʽ���</param>
        /// <returns>Ӱ��ļ�¼��</returns>
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
        /// ִ�д�һ���洢���̲����ĵ�SQL��䡣
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <param name="content">��������,����һ���ֶ��Ǹ�ʽ���ӵ����£���������ţ�����ͨ�������ʽ���</param>
        /// <returns>Ӱ��ļ�¼��</returns>
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
        /// �����ݿ������ͼ���ʽ���ֶ�(������������Ƶ���һ��ʵ��)
        /// </summary>
        /// <param name="strSQL">SQL���</param>
        /// <param name="fs">ͼ���ֽ�,���ݿ���ֶ�����Ϊimage�����</param>
        /// <returns>Ӱ��ļ�¼��</returns>
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
        /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����
        /// </summary>
        /// <param name="SQLString">�����ѯ������</param>
        /// <returns>��ѯ�����object��</returns>
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
        /// ���޶��ĳ�ʱʱ����,��ȡ���ݿ����һ����¼�е�һ���ֶ�ֵ
        /// <remarks>
        /// ��ֹ���ݹ���������Ӧ
        /// </remarks>
        /// </summary>
        /// <param name="SQLString">��ѯ���sql</param>
        /// <param name="Times">��ʱʱ��</param>
        /// <returns>����Ҫ��ȡ�Ķ���ֵ</returns>
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
        /// ִ�в�ѯ��䣬����SqlDataReader ( ע�⣺���ø÷�����һ��Ҫ��SqlDataReader����Close )
        /// </summary>
        /// <param name="strSQL">��ѯ���</param>
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
        /// ִ�в�ѯ��䣬����DataSet
        /// </summary>
        /// <param name="SQLString">��ѯ���</param>
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
        /// ���޶��ĳ�ʱʱ����,��ȡһ��DataSet
        /// </summary>
        /// <remarks>
        /// ��ֹ���ݹ���������Ӧ
        /// </remarks>
        /// <param name="SQLString">sql��ѯ���</param>
        /// <param name="Times">��ʱʱ��</param>
        /// <returns>��������DataSet</returns>
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

        #region ִ�д�������SQL���

        /// <summary>
        /// ִ��SQL��䣬����Ӱ��ļ�¼��
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <param name="cmdParms">��ѯ����</param>
        /// <returns>Ӱ��ļ�¼��</returns>
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
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="SQLStringList">SQL���Ĺ�ϣ��keyΪsql��䣬value�Ǹ�����SqlParameter[]��</param>
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
                        //ѭ��
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
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="SQLStringList">SQL���Ĺ�ϣ��keyΪsql��䣬value�Ǹ�����SqlParameter[]��</param>
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
                        //ѭ��
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
        /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����
        /// </summary>
        /// <param name="SQLString">�����ѯ������</param>
        /// <param name="s">Ϊnull,Ϊ��չ����</param>
        /// <param name="cmdParms">��ѯ����</param>
        /// <returns>��ѯ�����object��</returns>
        public static string GetSingle(string SQLString,string s, params SqlParameter[] cmdParms)
        {
            object obj = GetSingle(SQLString, cmdParms);
            if (obj == null)
                return string.Empty;
            else
                return obj.ToString();
        }
        /// <summary>
        /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����
        /// </summary>
        /// <param name="SQLString">�����ѯ������</param>
        /// <param name="cmdParms">��ѯ����</param>
        /// <returns>��ѯ�����object��</returns>
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
        /// ִ�в�ѯ��䣬����SqlDataReader ( ע�⣺���ø÷�����һ��Ҫ��SqlDataReader����Close )
        /// </summary>
        /// <param name="SQLString">��ѯ���</param>
        /// <param name="cmdParms">��ѯ����</param>
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
        /// ִ�в�ѯ��䣬����DataSet
        /// </summary>
        /// <param name="SQLString">��ѯ���</param>
        /// <param name="cmdParms">��ѯ����</param>
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
        /// ����sql/�洢����/����(�ڲ�ʹ��)
        /// </summary>
        /// <param name="cmd">SqlCommand �������</param>
        /// <param name="conn">SqlConnection ���Ӷ���</param>
        /// <param name="trans">SqlTransaction �������</param>
        /// <param name="cmdText">sql���/���ߴ洢��������</param>
        /// <param name="cmdParms">����</param>
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

        #region �洢���̲���

        /// <summary>
        /// ִ�д洢���̣�����SqlDataReader ( ע�⣺���ø÷�����һ��Ҫ��SqlDataReader����Close )
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
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
        /// ִ�д洢����
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <param name="tableName">DataSet����еı���</param>
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
        /// ���д洢����
        /// </summary>
        /// <param name="storedProcName">�洢��������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <param name="tableName">����</param>
        /// <param name="Times">��ʱʱ��</param>
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
        /// ���� SqlCommand ����(��������һ���������������һ������ֵ)
        /// </summary>
        /// <param name="connection">���ݿ�����</param>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
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
                        // ���δ����ֵ���������,���������DBNull.FieldValue.
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
        /// ִ�д洢���̣�����Ӱ�������		
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <param name="rowsAffected">Ӱ�������</param>
        /// <returns>���ش洢���̵ķ���ֵ</returns>
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
        /// ���� SqlCommand ����ʵ��(��������һ������ֵ)	
        /// </summary>
        /// <param name="connection">���ݿ����Ӷ���</param>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <returns>SqlCommand ����ʵ��</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion

        #region ��ҳ����
        /// <summary>
        /// ��ҳ��ȡ�����б�
        /// </summary>
        /// <param name="tablename">������</param>
        /// <param name="primarykey">������</param>
        /// <param name="column">��ʾ����</param>
        /// <param name="sortfiled">�����ֶ�</param>
        /// <param name="pagesize">ÿҳ��¼��</param>
        /// <param name="pageindex">ҳ������</param>
        /// <param name="sordir">����ʽ ��0 ֵ����</param>
        /// <param name="search">��������</param>
        /// <param name="Group">�����ֶ�</param>
        /// <param name="outstring">���صļ�¼����</param>      
        /// <returns>���ز�ѯ���</returns>
        public static DataSet GetPageList(string tablename, string primarykey, string sortfiled, string sordir, int pageindex, int pagesize, string column, string search, string Group, out string outstring)
        {
            /*
            ����˵��:
            1.Tables :������,��ͼ
            2.PrimaryKeyFieldName :���ؼ���
            3.Sort :������䣬����Order By ���磺NewsID Desc,OrderRows Asc
            4.CurrentPage :��ǰҳ��
            5.PageSize :��ҳ�ߴ�
            6.Fields :��ʾ���ֶ�
            7.Filter :������䣬����Where 
            8.Group :Group���,����Group By
            9.@RecordCount ����ܼ�¼��
            Ч����ʾ��http://www.cn5135.com/_App/Enterprise/QueryResult.aspx
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
            parameters[0].Value = tablename;//������,��ͼ
            parameters[1].Value = primarykey;//���ؼ���
            parameters[2].Value = paixun;//������䣬����Order By ���磺NewsID Desc,OrderRows Asc
            parameters[3].Value = pageindex;//��ǰҳ��
            parameters[4].Value = pagesize;//��ҳ�ߴ�
            parameters[5].Value = column;//��ʾ���ֶ�
            parameters[6].Value = search;//������䣬����Where
            parameters[7].Value = Group;//Group���,����Group By
            parameters[8].Direction = ParameterDirection.Output;//����ܼ�¼��
            DataSet ds = SqlServerHelper.RunProcedure("PR_Page", parameters, "ds");
            outstring = parameters[8].Value.ToString();
            return ds;
        }
        #endregion 

        #region sqlԴ�����
        /// <summary>
        /// ��ȡָ����/��ͼ��Ԫ������Ϣ(�Ƿ�����,ע��˵��)
        /// </summary>
        /// <param name="tableName">�����ͼ����</param>
        /// <returns>datatable</returns>
        public static DataTable GetTableColumnInfo(string tableName)
        {
            #region ��ѯsql
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
        /// ���ر��˵��Dt
        /// </summary>
        /// <returns>���ر��˵��</returns>
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
        /// ��ȡָ����/��ͼ������Ϣ�б�
        /// </summary>
        /// <param name="tableName">�����ͼ����</param>
        /// <param name="isname">��չ</param>
        /// <returns>������</returns>
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
        /// ��ȡָ����/��ͼ������Ϣ�б�
        /// </summary>
        /// <param name="tableName">�����ͼ����</param>
        /// <returns>������</returns>
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
                key = key.Length == 0 ? "" : "(����),";
                AllowNulls = AllowNulls.Length == 0 ? "Not Null" : "Null";
                list.Add(Name + "(" + Datatype + "(" + Length + ")," + key + Remark + (AllowNulls) + ")");
            }
            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// �����������ݿ������
        /// </summary>
        /// <returns>����������</returns>
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
        /// ��ȡ��ǰ���������������ݿ��б�
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
        /// ��ȡָ�����ݿ��е����д洢����
        /// </summary>
        /// <returns>�洢�����б�</returns>
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
        /// ��ȡָ�����ݿ��е����к���
        /// </summary>
        /// <returns>�����б�</returns>
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
        /// ��ȡָ�����ݿ��е����д�����
        /// </summary>
        /// <returns>�������б�</returns>
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
        /// ��ȡָ�����ݿ��е����б�
        /// </summary>    
        /// <returns>������</returns>
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
        /// ��ȡָ�����ݿ��е�������ͼ
        /// </summary>
        /// <returns>��ͼ����</returns>
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
        /// ��ȡָ�����ݿ��е������û�
        /// </summary>
        /// <param name="DatabaseName">���ݿ�����</param>
        /// <returns>�û�����</returns>
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
        /// ��ȡ�洢���̵Ĳ���
        /// </summary>
        /// <param name="procedureName">�洢��������</param>
        /// <returns>�洢���̲�������</returns>
        public static string[] GetProcedureParameter(string procedureName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT a.name AS p_name,b.name AS p_type,a.length AS p_length,a.isoutparam AS p_isout ");
            sb.Append(" FROM syscolumns a, systypes b WHERE a.xtype=b.xtype AND b.name<>'sysname' ");
            sb.Append(" AND id = (select id from sysobjects where name = '" + procedureName + "') ");

            ArrayList list = new ArrayList();
            list.Add("@RETURN_VALUE(int(4),����ֵ)");
            DataTable dt = SqlServerHelper.Query(sb.ToString()).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                list.Add(dr["p_name"].ToString() + "(" + dr["p_type"].ToString() + "(" + dr["p_length"].ToString() + ")," + (dr["p_isout"].ToString().Equals("1") ? "���" : "����") + ")");
            }


            if (list.Count == 0)
                return new string[0];
            else
                return (string[])list.ToArray(typeof(string));
        }
        /// <summary>
        /// �����û��ű�
        /// </summary>
        /// <param name="uid">�˺�</param>
        /// <param name="pwd">����</param>
        /// <returns>���ش����û��Ľű�</returns>
        public static string GetUserCreateCode(string uid, string pwd)
        {
            //EXEC   sp_addlogin   'a ' ,'1' --������½
            //EXEC   sp_adduser   'a ' --�����û�
            string s1 = "EXEC sp_addlogin '" + uid + "' ,'" + pwd + "'\n";
            string s2 = "EXEC sp_adduser '" + uid + "'";
            //ESqlServer.ExecuteSql(s1);
            //ESqlServer.ExecuteSql(s2);
            return s1+s2;
        }
        /// <summary>
        /// ��ȡ��Ȩ����
        /// </summary>
        /// <param name="user">�û�</param>
        /// <param name="table">����/��ͼ��/�洢������</param>
        /// <param name="qx">Ȩ��</param>
        /// <returns>��Ȩ����</returns>
        public static string GetGrantCode(string user, string table, string qx)
        {
            string str = "grant {0} on {1} to {2} --��Ȩ�û�{3}����{4}��{5}Ȩ��";
            str = string.Format(str, qx, table, user, user, table, qx);
            return str;
        }
        /// <summary>
        /// ������Ȩ����
        /// </summary>
        /// <param name="user">�û�</param>
        /// <param name="table">����/��ͼ��/�洢������</param>
        /// <param name="qx">Ȩ��</param>
        /// <returns>������Ȩ����</returns>
        public static string GetRevokeCode(string user, string table, string qx)
        {
            string str = "revoke {0} on {1} to {2} --�����û�{3}����{4}��{5}Ȩ��";
            str = string.Format(str, qx, table, user, user, table, qx);
            return str;
        }        

        /// <summary>
        /// ��ȡ��ȨϵͳȨ�޴���
        /// </summary>
        /// <param name="user">�û�</param>
        /// <param name="qx">Ȩ��</param>
        /// <returns>��ȨϵͳȨ�޴���</returns>
        public static string GetGrantCode(string user, string qx)
        {
            string str = "grant {0} to {1} --��Ȩ�û�{2}��{3}Ȩ��";
            str = string.Format(str, qx, user, user, qx);
            return str;
        }
        /// <summary>
        /// ����ϵͳȨ�޴���
        /// </summary>
        /// <param name="user">�û�</param>
        /// <param name="qx">Ȩ��</param>
        /// <returns>����ϵͳȨ�޴���</returns>
        public static string GetRevokeCode(string user, string qx)
        {
            string str = "revoke {0} to {1} --�����û�{2}��{3}Ȩ��";
            str = string.Format(str, qx, user, user, qx);
            return str;
        }
        /// <summary>
        /// ��ȡ����Ĭ��ֵ��δ���ܵĴ洢���̡��û����庯��������������ͼ�Ĵ����ı���
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

        #region ���ݻָ�


        /// <summary>
        /// ����sql2000���ݿ�
        /// </summary>
        /// <param name="dbname">Ҫ���ݵ����ݿ�����</param>
        /// <param name="path">�����ļ���·�������ļ���(ʹ��server mappath)</param>
        /// <returns>�ɹ�����true</returns>
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
        /// ��ԭsql2000���ݿ�
        /// </summary>
        /// <param name="dbname">���ݿ�����</param>
        /// <param name="path">�����ļ�·�������ļ���</param>
        /// <returns>�ɹ�����true</returns>
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
        /// ��Ӳ���
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="type">����</param>
        /// <param name="value">ֵ</param>
        /// <returns>�����������</returns>
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
        /// ��Ӳ���
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="type">����</param>
        /// <param name="value">ֵ</param>
        /// <param name="convertZeroToDBNull">�������Ϊ��,�Ƿ�תΪDBNull.FieldValue</param>
        /// <returns>�����������</returns>
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
        /// ��Ӳ���
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="type">����</param>
        /// <param name="value">ֵ</param>
        /// <param name="convertZeroToDBNull">�������Ϊ��,�Ƿ�תΪDBNull.FieldValue</param>
        /// <returns>�����������</returns>
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
        /// ��Ӳ���
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="type">����</param>
        /// <param name="value">ֵ</param>
        /// <param name="size">��С</param>
        /// <returns>�����������</returns>
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
        /// ��Ӳ���
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="type">����</param>
        /// <param name="value">ֵ</param>
        /// <param name="direction">���뻹�����</param>
        /// <returns>�����������</returns>
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
        /// ��Ӳ���
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="type">����</param>
        /// <param name="value">ֵ</param>
        /// <param name="size">��С</param>
        /// <param name="direction">���뻹�����</param>
        /// <returns>�����������</returns>
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
        /// ��������ֵ
        /// </summary>
        /// <param name="value">����ֵ</param>
        /// <returns>��������</returns>
        private static object PrepareSqlValue(object value)
        {
            return PrepareSqlValue(value, false);
        }
        /// <summary>
        /// ��������ֵ
        /// </summary>
        /// <param name="value">����ֵ</param>
        /// <param name="convertZeroToDBNull">�������Ϊ��,�Ƿ�תΪDBNull.FieldValue</param>
        /// <returns>��������</returns>
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


        #region ���ɱ�����
        /// <summary>
        /// ���ɱ�����
        /// </summary>
        /// <param name="tablename">������</param>
        /// <param name="newtablename">���ɱ������</param>
        /// <param name="dbtype">���ݿ�����</param>
        /// <param name="isaddpk">�Ƿ��Զ����һ�����ظ���������</param>
        /// <param name="isadddate">�Ƿ��Զ�����һ���������</param>
        /// <param name="numberlist">�������ֶ��б�</param>
        /// <param name="datelist">�������ֶ��б�</param>
        /// <param name="varcharlist">�ַ����ֶ��б�</param>
        /// <param name="columnduiying">�ж�Ӧ����(key ԭ����,value������)</param>
        /// <param name="column">��ʾ����(col1,col2)(*)</param>
        /// <param name="top">��ʾǰ����</param>
        /// <param name="where">��ѯ����,����where</param>
        /// <param name="isbigdata">�Ƿ��Ǵ�����,����5�����������ѡtrue</param>
        /// <param name="path">�����ļ�����·��</param>
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
        /// ��ȡ���ݿ���ṩ����,ͨ�����ݿ�����
        /// </summary>
        /// <param name="databasetype">���ݿ�����</param>
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
        /// ��ȡ���ݿ������ַ���
        /// </summary>
        /// <param name="dbtype">���ݿ�����(sql,access)</param>
        /// <param name="yztype">��֤��ʽ(window,sql)</param>
        /// <param name="dbpath">Access���ݿ�·��</param>
        /// <param name="uid">�ʺ�</param>
        /// <param name="pwd">����</param>
        /// <param name="database">���ݿ�����</param>
        /// <param name="server">��������ַ</param>
        /// <returns>���ض�Ӧ�������ַ���</returns>
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
    /// ���ݿ�����
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// SQL2000���ݿ�
        /// </summary>
        SQL2000,
        /// <summary>
        /// SQL2005���ݿ�
        /// </summary>
        SQL2005,
        /// <summary>
        /// SQL2008���ݿ�
        /// </summary>
        SQL2008,
        /// <summary>
        /// ACCESS���ݿ�
        /// </summary>
        ACCESS,
        /// <summary>
        /// Oracle���ݿ�
        /// </summary>
        ORACEL,
        /// <summary>
        /// MySql���ݿ�
        /// </summary>
        MySql,
        /// <summary>
        /// SQLite���ݿ�
        /// </summary>
        SQLite
    }
   
    /// <summary>
    /// ���ݿ���֤��ʽ
    /// </summary>
    public enum YanZhengType
    {
        /// <summary>
        /// Window��֤��ʽ
        /// </summary>
        Windows,
        /// <summary>
        /// Sql Server��֤��ʽ
        /// </summary>
        SQLSERVER
    }
}
