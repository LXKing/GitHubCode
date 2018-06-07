using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
namespace DataAccess.ADO
{
    interface IOraDataAccess
    {
        DataSet DBExecuteAsDataSet(string sql, IEnumerable<OracleParameter> OracleParameterCollection = null);
        DataTable DBExecuteAsDataTable(string sql, IEnumerable<OracleParameter> parmCollection = null);
        IEnumerable<T> DBExecuteAsIEnumerable<T>(string sql, IEnumerable<OracleParameter> parmCollection = null) where T : new();
        ResultInfo DBExecuteNonQuery(string sql, IEnumerable<OracleParameter> parmCollection = null);
        ResultInfo DBExecuteNonQueryAsTran(string sql, IEnumerable<OracleParameter> parmCollection = null, bool successAutoCommit = false);
        T DBExecuteScalar<T>(string sql, IEnumerable<OracleParameter> parmCollection = null);
    }
}
