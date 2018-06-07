using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DataAccess.ADO
{
    public class OraDataAccess : OrclDataAccessBase
    {
        public OraDataAccess(string connString)
            : base(connString)
        {
            base._connection = new OracleConnection(connString);
        }
        public OraDataAccess(OracleConnection connection)
            : base(connection)
        {
            _connection = connection;
        }
    }
}
