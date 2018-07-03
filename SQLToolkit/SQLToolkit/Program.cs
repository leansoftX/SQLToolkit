using System;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Data.SqlClient;
using System.Linq;

namespace SQLToolkit
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!ValidateArgs(args))
            {
                args = new string[100];
                Console.WriteLine(@"Please Enter Server Address: ");
                args[0] = Console.ReadLine();

                Console.WriteLine(@"Please Enter Server Port: ");
                args[1] = Console.ReadLine();

                Console.WriteLine(@"Please Enter Database Name: ");
                args[2] = Console.ReadLine();

                Console.WriteLine(@"Please Enter Database Username: ");
                args[3] = Console.ReadLine();

                Console.WriteLine(@"Please Enter Database Password: ");
                args[4] = Console.ReadLine();

                Console.WriteLine(@"Please Enter SQL Scripts Path: ");
                args[5] = Console.ReadLine();
            }

            if (!ValidateSqlPath(args[5]))
            {
                return;
            }
            

            Log("===============BEGIN=================");
           
            var scriptsFolder = args[5];
            Log("===============SQL=================");
            Log(string.Format("ScriptFolder:{0}", scriptsFolder));
            var sqlFiles = Directory.GetFiles(scriptsFolder).OrderBy(i=>i);
            Log(string.Format("Find {0} sql scripts", sqlFiles.Count()));
            string sqlConnectionString = string.Format(@"Server={0},{1};Initial Catalog={2};Persist Security Info=False;User ID={3};Password={4};Connection Timeout=30;", args[0], args[1], args[2], args[3],args[4]);     
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server server = new Server(new ServerConnection(conn));
            foreach (string file in sqlFiles)
            {
                Log(string.Format("Ready to exec sql script:{0}", file));

                try
                {
                    server.ConnectionContext.ExecuteNonQuery(File.ReadAllText(file));
                }
                catch (Exception ex)
                {
                    Log(string.Format("Error:{0}", ex.ToString()));
                }

                Log(string.Format("Finish to exec sql script:{0}", file));

            }

            Console.Write("===============END=================\n");

        }
        
        static bool ValidateArgs(string[] args) {
            if (args.Length < 6)
            {
                Log("请提供完整的参数信息");
                return false;
            }
            return true;
        }

        static bool ValidateSqlPath(string path)
        {
            if (!Directory.Exists(path))
            {
                Log(string.Format("Error:The path you supply is not exist,path:{0}", path));
                return false;
            }
            return true;
        }

        static void Log(string message)
        {
            Console.Write(string.Format("{0}-{1}\n", DateTime.Now.ToString(), message));

        }

    }
}
