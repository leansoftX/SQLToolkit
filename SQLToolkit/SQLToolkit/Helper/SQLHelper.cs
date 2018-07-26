using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SQLToolkit.Helper
{
    public static class SQLHelper
    {
        public static int ExecuteNonQuery(string sql)
        {
            SqlConnection conn = new SqlConnection(Program.SqlConnectionString);
            Server server = new Server(new ServerConnection(conn));
            return server.ConnectionContext.ExecuteNonQuery(sql);

        }

        public static object ExecuteScalar(string sql)
        {
            SqlConnection conn = new SqlConnection(Program.SqlConnectionString);
            Server server = new Server(new ServerConnection(conn));
            return server.ConnectionContext.ExecuteScalar(sql);

        }
    }
}
