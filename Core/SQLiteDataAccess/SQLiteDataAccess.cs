using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace DataAccess.ADO
{
    /// <summary>
    /// 
    /// </summary>
    public class SQLiteDataAccess:ADODataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        public SQLiteDataAccess(string connString)
            : base(connString)
        {
            base._connection = new SQLiteConnection(connString);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public SQLiteDataAccess(SQLiteConnection connection)
            : base(connection)
        {
            _connection = connection;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parmCollection"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public override DataSet DBExecuteAsDataSet(string sql, IEnumerable<DbParameter> parmCollection = null, CommandType cmdType = CommandType.Text)
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
                base._dataAdapter = new SQLiteDataAdapter(_cmd as SQLiteCommand);
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
