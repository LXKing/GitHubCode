using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
namespace DataAccess
{
    interface IADODataAccess : IDisposable
    {
        void BeginTransaction();
        void BeginTransaction(System.Data.IsolationLevel level);
        void Open();
        void Close();
        void CommitTransaction();
        DataSet DBExecuteAsDataSet(string sql, IEnumerable<DbParameter> parmCollection = null, CommandType cmdType = CommandType.Text);
        DataTable DBExecuteAsDataTable(string sql, IEnumerable<DbParameter> parmCollection = null, CommandType cmdType = CommandType.Text);
        IEnumerable<T> DBExecuteAsIEnumerable<T>(string sql, IEnumerable<DbParameter> parmCollection = null, CommandType cmdType = CommandType.Text) where T : new();
        ResultInfo<object> DBExecuteNonQuery(string sql, IEnumerable<DbParameter> parmCollection = null,CommandType cmdType=CommandType.Text);
        ResultInfo<object> DBExecuteNonQueryAsTran(string sql, IEnumerable<DbParameter> parmCollection = null,CommandType cmdType=CommandType.Text, bool autoCommit = false);
        T DBExecuteScalar<T>(string sql, IEnumerable<DbParameter> parmCollection = null, CommandType cmdType = CommandType.Text);
        void RollBackTransaction();
    }
}
