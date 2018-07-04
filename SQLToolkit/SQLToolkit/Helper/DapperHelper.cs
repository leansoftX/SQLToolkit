using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SQLToolkit.Helper
{
    public static class DapperHelper
    {
        public static int Execute(string sql)
        {
            using (IDbConnection conn = new SqlConnection(Program.SqlConnectionString))
            {
                return conn.Execute(sql);
            }
        }

        public static object ExecuteScalar(string sql)
        {
            using (IDbConnection conn = new SqlConnection(Program.SqlConnectionString))
            {
                return conn.ExecuteScalar(sql);
            }
        }


    }
}
