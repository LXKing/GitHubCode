using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.OracleClient;
using System.Data;
using System.Data.Common;

namespace DataAccess.Oracle
{
    public class OracleClientHelper:DataAccess.ADO.ADODataAccessBase
    {
        public OracleClientHelper(string connString)
            : base(connString)
        {
            base._connection = new OracleConnection(connString);
        }
        public OracleClientHelper(OracleConnection connection)
            : base(connection)
        {
            _connection = connection;
        }
        public override DataSet DBExecuteAsDataSet(string sql, IEnumerable<DbParameter> parmCollection = null, CommandType cmdType = CommandType.Text)
        {
            try
            {
                this.Open();
                _cmd = _connection.CreateCommand();
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = sql;
                if (parmCollection != null)
                {
                    parmCollection.ToList().ForEach(x =>
                    {
                        _cmd.Parameters.Add(x);
                    });
                    //_cmd.Parameters.AddRange(OracleParameterCollection.ToArray());
                }
                DataSet ds = new DataSet();
                base._dataAdapter = new OracleDataAdapter(_cmd as OracleCommand);
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

        public DataTable DBExecuteProcedureAsDataTable(string procedureName, IEnumerable<DbParameter> oracleParameterCollection = null)
        {
            try
            {
                this.Open();
                _cmd = _connection.CreateCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = procedureName;
                if (oracleParameterCollection != null)
                {
                    oracleParameterCollection.ToList().ForEach(x =>
                    {
                        _cmd.Parameters.Add(x);
                    });
                    //_cmd.Parameters.AddRange(OracleParameterCollection.ToArray());
                }

                DataSet ds = new DataSet();
                var cmd0 = _cmd as OracleCommand;
                base._dataAdapter = new OracleDataAdapter(_cmd as OracleCommand);
                base._dataAdapter.Fill(ds);
                return ds.Tables.Count>0? ds.Tables[0]:new DataTable();
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

        public int DBExecuteAsProcedure(string procedureName, IEnumerable<DbParameter> oracleParameterCollection = null)
        {
            try
            {
                this.Open();
                _cmd = _connection.CreateCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = procedureName;
                if (oracleParameterCollection != null)
                {
                    oracleParameterCollection.ToList().ForEach(x =>
                    {
                        _cmd.Parameters.Add(x);
                    });
                    //_cmd.Parameters.AddRange(OracleParameterCollection.ToArray());
                }
                var count = _cmd.ExecuteNonQuery();
                return count;
                //DataSet ds = new DataSet();
                //var cmd0 = _cmd as OracleCommand;
                //base._dataAdapter = new OracleDataAdapter(_cmd as OracleCommand);
                //base._dataAdapter.Fill(ds);
                //return ds;
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
    }
}
