using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.ADO
{
    public class SqlDataAccessAsync : SqlDataAccess
    {
        public SqlDataAccessAsync(string connString, DatabaseLogger dbLogger = null)
            : base(connString,dbLogger)
        {
        }
        public SqlDataAccessAsync(SqlConnection connection, DatabaseLogger dbLogger = null)
            : base(connection,dbLogger)
        {
        }

        public Task<DataSet> DBExecuteAsDataSetAsync(string sql, IEnumerable<DbParameter> sqlParameterCollection = null, CommandType cmdType = CommandType.Text)
        {
            Task<DataSet> task = new Task<DataSet>(new Func<DataSet>(() => {
                DataSet ds = new DataSet();
                try
                {
                    this.Open();
                    _cmd = _connection.CreateCommand();
                    _cmd.CommandType = cmdType;
                    _cmd.CommandText = sql;
                    if (sqlParameterCollection != null)
                    {
                        sqlParameterCollection.ToList().ForEach(x =>
                        {
                            _cmd.Parameters.Add(x);
                        });
                    }
                    
                    base._dataAdapter = new SqlDataAdapter(_cmd as SqlCommand);
                    base._dataAdapter.Fill(ds);
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.Close();
                }
                return ds;
            }));
            return task;
        }

        public Task<DataSet> DBExecuteAsDataSetAsync(string sql, IEnumerable<SqlParameter> sqlParameterCollection = null, CommandType cmdType = CommandType.Text)
        {
            return DBExecuteAsDataSetAsync(sql, sqlParameterCollection, cmdType);
        }

        public Task<DataTable> DBExecuteAsDataTableAsync(string sql, IEnumerable<SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text)
        {
            Task<DataTable> task = new Task<DataTable>(new Func<DataTable>(() =>
            {
                return base.DBExecuteAsDataTable(sql, parmCollection, cmdType);
            }));
            return task;
        }
        public Task<IEnumerable<T>> DBExecuteAsIEnumerableAsync<T>(string sql, IEnumerable<SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text) where T : new()
        {
            Task<IEnumerable<T>> task = new Task<IEnumerable<T>>(new Func<IEnumerable<T>>(() =>
            {
                return base.DBExecuteAsIEnumerable<T>(sql, parmCollection, cmdType);
            }));
            return task;
        }

        public Task<ResultInfo<object>> DBExecuteNonQueryAsync(string sql, IEnumerable<SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text)
        {
            Task<ResultInfo<object>> task = new Task<ResultInfo<object>>(new Func<ResultInfo<object>>(() =>
            {
                return base.DBExecuteNonQuery(sql, parmCollection, cmdType);
            }));
            return task;
        }

        public Task<ResultInfo<object>> DBExecuteNonQueryAsTranAsync(string sql, IEnumerable<SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text)
        {
            Task<ResultInfo<object>> task = new Task<ResultInfo<object>>(new Func<ResultInfo<object>>(() =>
            {
                return base.DBExecuteNonQueryAsTran(sql, parmCollection, cmdType);
            }));
            return task;
        }

        public Task<T> DBExecuteScalarAsync<T>(string sql, IEnumerable<SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text)
        {
            Task<T> task = new Task<T>(new Func<T>(() =>
            {
                return base.DBExecuteScalar<T>(sql, parmCollection, cmdType);
            }));
            return task;
        }
    }
}
