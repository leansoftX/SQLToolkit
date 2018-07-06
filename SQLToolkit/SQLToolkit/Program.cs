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
            string command = string.Empty;
          

            if (args.Length == 0)
            {
                Helper.LogHelper.Log("No args provided.");
                PrintInstructions();
                return;
            }

            command = args[0].ToLower();

            if (command == "runscripts")
            {
                RunScripts(args);
            }
            else if (command == "backup")
            {
                Backup(args);
            }
            else if (command == "restore")
            {
                Restore(args);

            }
            else
            {
                Console.Write("Wrong command\n");
                PrintInstructions();
                return;
            }



        }



        /// <summary>
        /// upgarde sql scheme
        /// </summary>
        private static void RunScripts(string[] args)
        {
            string database_server = string.Empty;
            string database_name = string.Empty;
            string database_username = string.Empty;
            string database_password = string.Empty;
            string sqlscripts_path = string.Empty;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-s":
                        if (i + 1 < args.Length)
                        {
                            database_server = args[i + 1];
                        }
                        else
                        {
                            Console.Write(("Missing argument to -s.\n Supply argument as -s \"database server with port\"\n"));
                            return;
                        }
                        break;
                    case "-n":
                        if (i + 1 < args.Length)
                        {
                            database_name = args[i + 1];
                        }
                        else
                        {
                            Console.Write(("Missing argument to -n.\n Supply argument as -n \"database name\"\n"));
                            return;
                        }
                        break;
                    case "-u":
                        if (i + 1 < args.Length)
                        {
                            database_username = args[i + 1];
                        }
                        else
                        {
                            Console.Write(("Missing argument to -u.\n Supply argument as -u \"database username\"\n"));
                            return;
                        }
                        break;
                    case "-p":
                        if (i + 1 < args.Length)
                        {
                            database_password = args[i + 1];
                        }
                        else
                        {
                            Console.Write(("Missing argument to -p.\n Supply argument as -p \"database password\"\n"));
                            return;
                        }
                        break;
                    case "-path":
                        if (i + 1 < args.Length)
                        {
                            sqlscripts_path = args[i + 1];
                        }
                        else
                        {
                            Console.Write(("Missing argument to -path.\n Supply argument as -path \"sql scripts path\"\n"));
                            return;
                        }
                        break;
                }
            }

            if (string.IsNullOrEmpty(database_server))
            {
                Console.Write(("Missing argument -s.\n Supply argument -s \"database server with port\"\n"));
                return;
            }
            if (string.IsNullOrEmpty(database_name))
            {
                Console.Write(("Missing argument -n.\n Supply argument -n \"database name\"\n"));
                return;
            }
            if (string.IsNullOrEmpty(database_username))
            {
                Console.Write(("Missing argument -u.\n Supply argument -u \"database username\"\n"));
                return;
            }
            if (string.IsNullOrEmpty(database_password))
            {
                Console.Write(("Missing argument -p.\n Supply argument -p \"database password\"\n"));
                return;
            }
            if (string.IsNullOrEmpty(sqlscripts_path)) {
                Console.Write(("Missing argument -path.\n Supply argument -path \"sql scripts path\"\n"));
                return;
            }


            SqlConnectionString = string.Format(@"Server={0};Initial Catalog={1};Persist Security Info=False;User ID={2};Password={3};Connection Timeout=30;", database_server,database_name, database_username, database_password);
            
            //init basic scheme for sqltoolkit
            Business.DatabaseVersion.Init();
            Helper.LogHelper.Log("===============BEGIN=================");
            //run upgrade sql scripts
            Business.DatabaseVersion.Upgrade(sqlscripts_path);
            Helper.LogHelper.Log("===============End=================");

        }

        /// <summary>
        /// backup database 
        /// </summary>
        private static void Backup(string[] args)
        {
            string database_server = string.Empty;
            string database_name = string.Empty;
            string database_username = string.Empty;
            string database_password = string.Empty;
            string backup_path = string.Empty;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-s":
                        if (i + 1 < args.Length)
                        {
                            database_server = args[i + 1];
                        }
                        else
                        {
                            Helper.LogHelper.Log(("Missing argument to -s.\n Supply argument as -s \"database server with port\"\n"));
                            return;
                        }
                        break;
                    case "-n":
                        if (i + 1 < args.Length)
                        {
                            database_name = args[i + 1];
                        }
                        else
                        {
                            Helper.LogHelper.Log(("Missing argument to -n.\n Supply argument as -n \"database name\"\n"));
                            return;
                        }
                        break;
                    case "-u":
                        if (i + 1 < args.Length)
                        {
                            database_username = args[i + 1];
                        }
                        else
                        {
                            Helper.LogHelper.Log(("Missing argument to -u.\n Supply argument as -u \"database username\"\n"));
                            return;
                        }
                        break;
                    case "-p":
                        if (i + 1 < args.Length)
                        {
                            database_password = args[i + 1];
                        }
                        else
                        {
                            Helper.LogHelper.Log(("Missing argument to -p.\n Supply argument as -p \"database password\"\n"));
                            return;
                        }
                        break;
                    case "-path":
                        if (i + 1 < args.Length)
                        {
                            backup_path = args[i + 1];
                        }
                        else
                        {
                            Helper.LogHelper.Log(("Missing argument to -path.\n Supply argument as -path \"backup path\"\n"));
                            return;
                        }
                        break;
                }
            }
            if (string.IsNullOrEmpty(database_server))
            {
                Console.Write(("Missing argument -s.\n Supply argument -s \"database server with port\"\n"));
                return;
            }
            if (string.IsNullOrEmpty(database_name))
            {
                Console.Write(("Missing argument -n.\n Supply argument -n \"database name\"\n"));
                return;
            }
            if (string.IsNullOrEmpty(database_username))
            {
                Console.Write(("Missing argument -u.\n Supply argument -u \"database username\"\n"));
                return;
            }
            if (string.IsNullOrEmpty(database_password))
            {
                Console.Write(("Missing argument -p.\n Supply argument -p \"database password\"\n"));
                return;
            }
            if (string.IsNullOrEmpty(backup_path))
            {
                Console.Write(("Missing argument -path.\n Supply argument -path \"backup path\"\n"));
                return;
            }



            SqlConnectionString = string.Format(@"Server={0};Initial Catalog={1};Persist Security Info=False;User ID={2};Password={3};Connection Timeout=30;", database_server, "master", database_username, database_password);

            //backup database before upgrade
            Business.SQLServer.Backup(database_name, backup_path);

        }


        /// <summary>
        /// backup database 
        /// </summary>
        private static void Restore(string[] args)
        {
            string database_server = string.Empty;
            string database_name = string.Empty;
            string database_username = string.Empty;
            string database_password = string.Empty;
            string backup_path = string.Empty;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-s":
                        if (i + 1 < args.Length)
                        {
                            database_server = args[i + 1];
                        }
                        else
                        {
                            Helper.LogHelper.Log(("Missing argument to -s.\n Supply argument as -s \"database server with port\"\n"));
                            return;
                        }
                        break;
                    case "-n":
                        if (i + 1 < args.Length)
                        {
                            database_name = args[i + 1];
                        }
                        else
                        {
                            Helper.LogHelper.Log(("Missing argument to -n.\n Supply argument as -n \"database name\"\n"));
                            return;
                        }
                        break;
                    case "-u":
                        if (i + 1 < args.Length)
                        {
                            database_username = args[i + 1];
                        }
                        else
                        {
                            Helper.LogHelper.Log(("Missing argument to -u.\n Supply argument as -u \"database username\"\n"));
                            return;
                        }
                        break;
                    case "-p":
                        if (i + 1 < args.Length)
                        {
                            database_password = args[i + 1];
                        }
                        else
                        {
                            Helper.LogHelper.Log(("Missing argument to -p.\n Supply argument as -p \"database password\"\n"));
                            return;
                        }
                        break;
                    case "-path":
                        if (i + 1 < args.Length)
                        {
                            backup_path = args[i + 1];
                        }
                        else
                        {
                            Helper.LogHelper.Log(("Missing argument to -path.\n Supply argument as -path \"backup path\"\n"));
                            return;
                        }
                        break;
                }
            }
            if (string.IsNullOrEmpty(database_server))
            {
                Console.Write(("Missing argument -s.\n Supply argument -s \"database server with port\"\n"));
                return;
            }
            if (string.IsNullOrEmpty(database_name))
            {
                Console.Write(("Missing argument -n.\n Supply argument -n \"database name\"\n"));
                return;
            }
            if (string.IsNullOrEmpty(database_username))
            {
                Console.Write(("Missing argument -u.\n Supply argument -u \"database username\"\n"));
                return;
            }
            if (string.IsNullOrEmpty(database_password))
            {
                Console.Write(("Missing argument -p.\n Supply argument -p \"database password\"\n"));
                return;
            }
            if (string.IsNullOrEmpty(backup_path))
            {
                Console.Write(("Missing argument -path.\n Supply argument -path \"backup path\"\n"));
                return;
            }



            SqlConnectionString = string.Format(@"Server={0};Initial Catalog={1};Persist Security Info=False;User ID={2};Password={3};Connection Timeout=30;", database_server, "master", database_username, database_password);

            //backup database before upgrade
            Business.SQLServer.Restore(database_name, backup_path);

        }

        /// <summary>
        /// Tool Instructions
        /// </summary>
        private static void PrintInstructions()
        {
            String inst = "Welcome to Lean-Soft SQLToolkit.\n" +
                          "Provide command you want to run\n" +
                          "---- Usage ----\n" +
                          "runscripts [-s <database server>][-n <database name>][-u <database user>][-p <database password>][-path <sql scripts path>]\n" +
                          "backup  [-s <database server>][-n <database name>][-u <database user>][-p <database password>][-path <backup path>]";
            Console.WriteLine(inst);
        }


    }
}
