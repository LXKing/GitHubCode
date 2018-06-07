using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.ADO
{
    public class SqlDataAccess:ADODataAccessBase, ISqlDataAccess
    {
        public SqlDataAccess(string connString, DatabaseLogger dbLogger = null)
            : base(connString)
        {
            base._connection = new SqlConnection(connString);
            if (dbLogger.NotNull_DA())
            {
                DbInterception.Remove(dbLogger);
                DbInterception.Add(dbLogger);
            }
        }
        public SqlDataAccess(SqlConnection connection, DatabaseLogger dbLogger = null)
            : base(connection)
        {
            _connection = connection;
            if (dbLogger.NotNull_DA())
            {
                DbInterception.Remove(dbLogger);
                DbInterception.Add(dbLogger);
            }
        }
        public override DataSet DBExecuteAsDataSet(string sql, IEnumerable<DbParameter> sqlParameterCollection = null, CommandType cmdType = CommandType.Text)
        {
            try
            {
                this.Open();
                _cmd = _connection.CreateCommand();
                _cmd.CommandType = cmdType;
                _cmd.CommandText = sql;
                if (sqlParameterCollection != null)
                {
                    sqlParameterCollection.ToList().ForEach(x => {
                        _cmd.Parameters.Add(x);
                    });
                    
                }
                DataSet ds = new DataSet();
                base._dataAdapter = new SqlDataAdapter(_cmd as SqlCommand);
                base._dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Close();
            }
        }

        public DataSet DBExecuteAsDataSet(string sql, IEnumerable<SqlParameter> sqlParameterCollection = null, CommandType cmdType = CommandType.Text)
        {
            return DBExecuteAsDataSet(sql, (IEnumerable<DbParameter>)sqlParameterCollection, cmdType);
        }

        public DataTable DBExecuteAsDataTable(string sql, IEnumerable<SqlParameter> parmCollection = null,CommandType cmdType = CommandType.Text)
        {
            return base.DBExecuteAsDataTable(sql, parmCollection, cmdType);
        }
        public IEnumerable<T> DBExecuteAsIEnumerable<T>(string sql, IEnumerable<SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text) where T : new()
        {
            return base.DBExecuteAsIEnumerable<T>(sql, parmCollection, cmdType);
        }

        public ResultInfo<object> DBExecuteNonQuery(string sql, IEnumerable<SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text)
        {
            return base.DBExecuteNonQuery(sql, parmCollection, cmdType);
        }

        public ResultInfo<object> DBExecuteNonQueryAsTran(string sql, IEnumerable<SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text)
        {
            return base.DBExecuteNonQueryAsTran(sql, parmCollection, cmdType);
        }

        public T DBExecuteScalar<T>(string sql, IEnumerable<SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text)
        {
            return base.DBExecuteScalar<T>(sql, parmCollection,cmdType);
        }
    }
}
