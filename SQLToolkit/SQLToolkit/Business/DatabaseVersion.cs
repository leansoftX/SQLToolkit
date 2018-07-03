using System;
using System.Collections.Generic;
using System.Text;

namespace SQLToolkit.Business
{
    public static class DatabaseVersion
    {
        public static void Init()
        {
            Helper.DapperHelper.Execute(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DatabaseVersion')
                CREATE TABLE DatabaseVersion(
                    [Id] [int]  PRIMARY KEY,
                    [Filename] [nvarchar](MAX)  NULL,
                    [ExecuteResult] [nvarchar](MAX)  NULL,
                    [ExecuteTime] [nvarchar](MAX)  NULL,
                )");
        }
    }
}
