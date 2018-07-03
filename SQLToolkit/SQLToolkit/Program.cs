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
            Console.Write("===============BEGIN=================\n");
            var rootFolder = Directory.GetCurrentDirectory();
            var scriptsFolder = Path.Combine(rootFolder, "Scripts");
            Console.Write("===============SQL=================\n");
            Console.Write(string.Format("ScriptFolder:{0}\n",scriptsFolder));
            var sqlFiles = Directory.GetFiles(scriptsFolder);
            Console.Write(string.Format("Find {0} sql scripts\n", sqlFiles.Length));
            string sqlConnectionString = string.Format(@"Server={0},{1};Initial Catalog={2};Persist Security Info=False;User ID={3};Password={4};Connection Timeout=30;", args[0], args[1], args[2], args[3],args[4]);     
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server server = new Server(new ServerConnection(conn));
            foreach (string file in sqlFiles)
            {
                Console.Write(string.Format("Ready to exec sql script:{0}\n", file));
                try
                {
                    server.ConnectionContext.ExecuteNonQuery(File.ReadAllText(file));
                }
                catch (Exception ex)
                {
                    Console.Write("Error:{0}\n", ex.ToString());
                }
                Console.Write(string.Format("Finish to exec sql script:{0}\n", file));

            }

            Console.Write("===============END=================\n");

        }
    }
}
