using DataAccess.ADO;
using IC.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IC.BLL
{
    public class BLL_Base:IC_TEST_Context
    {
        public string _connString = string.Empty;
        public SqlDataAccess db;
        public BLL_Base()
        {
            _connString = System.Configuration.ConfigurationManager.ConnectionStrings["IC_TEST_Connstring1"].ConnectionString;
            db = new SqlDataAccess(_connString);
        }
    }
}
