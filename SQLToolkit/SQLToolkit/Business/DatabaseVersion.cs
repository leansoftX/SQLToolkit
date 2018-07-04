using System;
using System.Collections.Generic;
using System.Text;

namespace SQLToolkit.Business
{
    public static class DatabaseVersion
    {
        public static int Init()
        {
            return Helper.DapperHelper.Execute(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ST_DatabaseVersion')
                CREATE TABLE ST_DatabaseVersion(
                    ID int IDENTITY(1,1) PRIMARY KEY,
                    [Filename] [nvarchar](MAX)  NULL,
                    [ExecuteResult] [nvarchar](MAX)  NULL,
                    [ExecuteTime] [nvarchar](MAX)  NULL,
                )");
        }

        public static int UpdateRecord(string filename, string result)
        {
            var sql = string.Format(@"INSERT INTO ST_DatabaseVersion (Filename, ExecuteResult, ExecuteTime)
                VALUES ('{0}', '{1}', '{2}');", filename, result, DateTime.Now.ToString());
            return Helper.DapperHelper.Execute(sql);
        }


    }
}
