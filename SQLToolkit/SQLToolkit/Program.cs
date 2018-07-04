using System;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using Dapper;

namespace SQLToolkit
{
    class Program
    {
        public static string SqlConnectionString;

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
            //generate sql connect string
            SqlConnectionString= string.Format(@"Server={0},{1};Initial Catalog={2};Persist Security Info=False;User ID={3};Password={4};Connection Timeout=30;", args[0], args[1], args[2], args[3], args[4]);
           
            //init basic scheme for sqltoolkit
            Business.DatabaseVersion.Init();

            Helper.LogHelper.Log("===============BEGIN=================");

            //run upgrade sql scripts
            var scriptsFolder = args[5];
            Business.DatabaseVersion.Upgrade(scriptsFolder);

            Helper.LogHelper.Log("===============End=================");

        }

        private static bool ValidateArgs(string[] args) {
            if (args.Length < 6)
            {
                Helper.LogHelper.Log("请提供完整的参数信息");
                return false;
            }
            return true;
        }

        private static bool ValidateSqlPath(string path)
        {
            if (!Directory.Exists(path))
            {
                Helper.LogHelper.Log(string.Format("Error:The path you supply is not exist,path:{0}", path));
                return false;
            }
            return true;
        }

       

       

    }
}
