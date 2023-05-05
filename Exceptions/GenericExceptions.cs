using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Web.UI;
using TODOProject.DataClasses;

namespace ToDoList.Exceptions
{
    public class GenericExceptions
    {
        public static void LogException(Exception exception) => LogExceptionMessage(exception);

        public static void LogException(Exception exception, string ErrorMessage = "") => LogExceptionMessage(exception, "", ErrorMessage);
        public static void LogException(Exception exception, Control cnt) => LogExceptionMessage(exception, cnt.ID);

        private static void LogExceptionMessage(Exception exception, string controlID = "", string UserErrorMessage = "")
        {
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create(), false);
            LogEntry log = new LogEntry();
            log.Message = ExceptionStyleFormat.DisplayFormat(exception, controlID, UserErrorMessage);
            log.Categories.Add(Category.General);
            log.Priority = Priority.Normal;
            Logger.Write(log);
        }
    }
}