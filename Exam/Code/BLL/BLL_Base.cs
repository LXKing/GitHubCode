using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL;
using DataAccess.ADO;
using DataAccess.EF;
namespace BLL
{
    public class BLL_Base : DB_ExamContext
    {
        public string _connString = string.Empty;
        public SqlDataAccess adoDb;
        public EFDataAccessBase dbContext;
        public EFDataAccessAsTran dbContextTran;
        public BLL_Base()
        {
            _connString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_ExamSqlConnstring"].ConnectionString;
            adoDb = new SqlDataAccess(_connString);

            dbContext = new DataAccess.EF.EFDataAccessBase(this);
            
            dbContextTran = new DataAccess.EF.EFDataAccessAsTran(this);
        }

    }
}
