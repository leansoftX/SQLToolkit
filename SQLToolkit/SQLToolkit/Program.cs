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
            var rootFolder = Directory.GetCurrentDirectory();
            var sqlFiles = Directory.GetFiles(Path.Combine(rootFolder, "Scripts"));
            string sqlConnectionString = @"Server=labs-test.devopshub.cn,1333;Initial Catalog=devopslabs;Persist Security Info=False;User ID=sa;Password=P2ssw0rd;Connection Timeout=30;";         
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server server = new Server(new ServerConnection(conn));
            foreach (string file in sqlFiles)
            {
                server.ConnectionContext.ExecuteNonQuery(File.ReadAllText(file));
            }
        }
    }
}
