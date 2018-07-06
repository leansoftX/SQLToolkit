using System;
using System.Collections.Generic;
using System.Text;

namespace SQLToolkit.Business
{
    public static class SQLServer
    {
        public static void Backup(string database,string path)
        {
            try
            {
                string sql = string.Format(@"IF db_id('{0}') IS NOT NULL 
                BACKUP DATABASE [{1}] 
                TO DISK = N'{2}' 
                WITH FORMAT", database, database, path);
                Helper.SQLHelper.ExecuteNonQuery(sql);
                Helper.LogHelper.Log(string.Format("Successfully Backup Database:{0} to {1}", database,path));

            }
            catch (Exception ex)
            {
                Helper.LogHelper.Log(string.Format("Error:{0}",ex.Message));

            }
        }

        public static void Restore(string database, string path)
        {
            try
            {
                string sql = string.Format(@"
                ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE
                GO
                RESTORE DATABASE [{1}] 
                FROM DISK = N'{2}' 
                WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 5", database, database, path);
                Helper.SQLHelper.ExecuteNonQuery(sql);
                Helper.LogHelper.Log(string.Format("Successfully RESTORE Database:{0} from {1}", database, path));

            }
            catch (Exception ex)
            {
                Helper.LogHelper.Log(string.Format("Error:{0}", ex.Message));

            }
        }

    }
}
