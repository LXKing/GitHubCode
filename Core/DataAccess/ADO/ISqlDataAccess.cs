using System;
using System.Data;
namespace DataAccess.ADO
{
    interface ISqlDataAccess
    {
        System.Data.DataSet DBExecuteAsDataSet(string sql, System.Collections.Generic.IEnumerable<System.Data.SqlClient.SqlParameter> sqlParameterCollection = null, CommandType cmdType = CommandType.Text);
        System.Data.DataTable DBExecuteAsDataTable(string sql, System.Collections.Generic.IEnumerable<System.Data.SqlClient.SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text);
        System.Collections.Generic.IEnumerable<T> DBExecuteAsIEnumerable<T>(string sql, System.Collections.Generic.IEnumerable<System.Data.SqlClient.SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text) where T : new();
        ResultInfo<object> DBExecuteNonQuery(string sql, System.Collections.Generic.IEnumerable<System.Data.SqlClient.SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text);
        ResultInfo<object> DBExecuteNonQueryAsTran(string sql, System.Collections.Generic.IEnumerable<System.Data.SqlClient.SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text);
        T DBExecuteScalar<T>(string sql, System.Collections.Generic.IEnumerable<System.Data.SqlClient.SqlParameter> parmCollection = null, CommandType cmdType = CommandType.Text);
    }
}
