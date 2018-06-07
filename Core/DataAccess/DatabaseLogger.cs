using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class DatabaseLogger : IDbCommandInterceptor
    {
        private static string _fileEx = "";

        public static string FileEx
        {
            get { return _fileEx; }
            set { _fileEx = value; }
        }
        public DatabaseLogger(string fileEx="")
        {
            FileEx = fileEx;
        }
        public void NonQueryExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogSQLInfo(command, interceptionContext, "NonQueryExecuted");
        }
        public void NonQueryExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            //LogSQLInfo(command, interceptionContext, "NonQueryExecuting");
        }
        public void ReaderExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            LogSQLInfo(command, interceptionContext, "ReaderExecuted");
        }
        public void ReaderExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            LogSQLInfo(command, interceptionContext, "ReaderExecuting");
        }

        public void ScalarExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogSQLInfo(command, interceptionContext, "ScalarExecuted");
        }

        public void ScalarExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogSQLInfo(command,interceptionContext, "ScalarExecuting");
        }
        private static void LogSQLInfo<T>(System.Data.Common.DbCommand command, DbCommandInterceptionContext<T> interceptionContext, string functionName)
        {
            List<string> parms = new List<string>() {
                        "CommandType(类型):" + command.CommandType.ToString(), 
                            "CommandText(文本):" + command.CommandText };
            if (command.Parameters != null)
            {
                var list = command.Parameters.Cast<DbParameter>().Select(x => x.ParameterName + "(参数):" + x.Value.ToString() + "(" + x.DbType.ToString() + ")").ToList();
                parms.AddRange(list);
            }
            if(interceptionContext.Exception!=null)
            {
                parms.Add("Exception(异常):"+interceptionContext.Exception.Message);
            }
            Logs.Log.WriteLog("DB执行" + functionName,
                parms,
                "DB" + _fileEx);
        }
    }
}
