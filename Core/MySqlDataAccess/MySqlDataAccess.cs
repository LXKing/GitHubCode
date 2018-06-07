using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccess.ADO
{
    public class MySqlDataAccess:DataAccess.ADO.ADODataAccessBase
    {
        public MySqlDataAccess(string connString)
            : base(connString)
        {
            base._connection = new MySqlConnection(connString);
        }
        public MySqlDataAccess(MySqlConnection connection)
            : base(connection)
        {
            _connection = connection;
        }
        public override System.Data.DataSet DBExecuteAsDataSet(string sql, IEnumerable<System.Data.Common.DbParameter> parmCollection = null, System.Data.CommandType cmdType = CommandType.Text)
        {
            try
            {
                this.Open();
                _cmd = _connection.CreateCommand();
                _cmd.CommandType = cmdType;
                _cmd.CommandText = sql;
                if (parmCollection != null)
                {
                    parmCollection.ToList().ForEach(x =>
                    {
                        _cmd.Parameters.Add(x);
                    });

                }
                DataSet ds = new DataSet();
                base._dataAdapter = new MySqlDataAdapter(_cmd as MySqlCommand);
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
    }
}
