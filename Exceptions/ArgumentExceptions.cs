using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using MySqlConnector;
using System;
using System.Data.SqlClient;
using System.Web.UI;
using TODOProject.DataClasses;

namespace TODOProject.Exceptions
{
    public class ArgumentExceptions
    {
        public static void LogException(Exception exception, Control cnt)
        {
            LogExceptionMessage(exception, cnt.ID);
        }
        public static void LogException(Exception exception)
        {
            LogExceptionMessage(exception);
        }
        public static void LogException(Exception exception, string ErrorMessage = "")
        {
            LogExceptionMessage(exception, ErrorMessage);
        }
        public static void LogException(MySqlException exception, string ErrorMessage = "")
        {
            LogExceptionMessage(exception, ErrorMessage);
        }
        public static void LogException(SqlException exception, string ErrorMessage = "")
        {
            LogExceptionMessage(exception, ErrorMessage);
        }
       
        private static void LogExceptionMessage(Exception exception, string controlID = "", string ErrorMessage = "")
        {
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create(), false);
            LogEntry log = new LogEntry();
            log.Message = ExceptionStyleFormat.DisplayFormat(exception, controlID, ErrorMessage);
            log.Categories.Add(Category.General);
            log.Priority = Priority.Normal;
            Logger.Write(log);
        }
       
    }
}