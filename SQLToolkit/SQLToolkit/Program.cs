using System;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Data.SqlClient;


namespace SQLToolkit
{
    class Program
    {
        static void Main(string[] args)
        {
            string sqlConnectionString = @"Server=labs-test.devopshub.cn,1333;Initial Catalog=devopslabs;Persist Security Info=False;User ID=sa;Password=P2ssw0rd;Connection Timeout=30;";

            string script = File.ReadAllText(@"C:\SQLToolkit\SQLToolkit\SQLToolkit\Scripts\01-test.sql");
         
            SqlConnection conn = new SqlConnection(sqlConnectionString);

            Server server = new Server(new ServerConnection(conn));

            server.ConnectionContext.ExecuteNonQuery(script);
        }
    }
}
