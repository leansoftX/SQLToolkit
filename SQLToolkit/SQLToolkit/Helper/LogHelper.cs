using System;
using System.Collections.Generic;
using System.Text;

namespace SQLToolkit.Helper
{
    public static class LogHelper
    {
        public static void Log(string message)
        {
            Console.Write(string.Format("{0}-{1}\n", DateTime.Now.ToString(), message));
        }
    }
}
