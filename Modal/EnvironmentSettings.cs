using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace TODOProject.Modal
{
    public static class EnvironmentSettings
    {
        public static string Dev_Connection()
        {
            ConnectionStringSettings connection = ConfigurationSetting.SetConfiguration("connection");
            string databaseConnectionStringName = connection.Name;
            return databaseConnectionStringName;
        }
        public static string Stagging_Connection()
        {
            ConnectionStringSettings connection = ConfigurationSetting.SetConfiguration("connection");
            string databaseConnectionStringName = connection.Name;
            return databaseConnectionStringName;
        }
        public static string QA_Connection()
        {
            ConnectionStringSettings connection = ConfigurationSetting.SetConfiguration("connection");
            string databaseConnectionStringName = connection.Name;
            return databaseConnectionStringName;
        }
    }
}