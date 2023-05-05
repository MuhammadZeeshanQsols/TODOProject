using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using MySqlConnector;
using System.Data.SqlClient;
using System.Web.UI;
using TODOProject.DataClasses;

namespace TODOProject.Exceptions
{
    public class DBExceptions
    {
        public static void LogException(SqlException exception, Control _control = null, string ErrorMessage = "")
        {
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create(), false);
            LogEntry log = new LogEntry();
            log.Message = ExceptionStyleFormat.DisplayFormat(exception, (_control != null ? _control.ID : ""), ErrorMessage);
            log.Categories.Add(Category.General);
            log.Priority = Priority.Normal;
            Logger.Write(log);
        }
        public static void DisplayMessage(SqlException _exception, Control _control)
        {
            string Msg;
            switch (_exception.Number)
            {
                case -1:
                    {
                        Msg = "An error has occurred while establishing a connection to the server, this failure may be caused by the fact that under the default settings Server does not allow remote connections.";
                        break;
                    }
                case -2:
                    {
                        Msg = "Timeout expired. The timeout period elapsed prior to completion of the operation or the server is not responding.";
                        break;
                    }
                case 547:
                    {
                        Msg = "You cannot delete this record because other data references it.";
                        break;
                    }
                case 1261:
                    {
                        Msg = "Remove Empty Row(s) From Uploaded File And Try Again!";
                        break;
                    }
                case 1048:
                    {
                        Msg = "Remove Empty Row(s) From Uploaded File And Try Again!";
                        break;
                    }
                case 1064:
                    {
                        Msg = "Invalid input! Please provide valid input";
                        break;
                    }
                default:
                    {
                        Msg = "Error Occurred";
                        break;
                    }
            }
            DataClass.ShowMessage(_control, DataClass.ErrorType.Error, Msg, true);
        }

        public static void LogException(MySqlException exception, Control _control = null, string ErrorMessage = "")
        {
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create(), false);
            LogEntry log = new LogEntry();
            log.Message = ExceptionStyleFormat.DisplayFormat(exception, (_control != null ? _control.ID : ""), ErrorMessage);
            log.Categories.Add(Category.General);
            log.Priority = Priority.Normal;
            Logger.Write(log);
        }
        public static void DisplayMessage(MySqlException _exception, Control _control)
        {
            string Msg;
            switch (_exception.Number)
            {
                case -1:
                    {
                        Msg = "An error has occurred while establishing a connection to the server, this failure may be caused by the fact that under the default settings Server does not allow remote connections.";
                        break;
                    }
                case -2:
                    {
                        Msg = "Timeout expired. The timeout period elapsed prior to completion of the operation or the server is not responding.";
                        break;
                    }
                case 547:
                    {
                        Msg = "You cannot delete this record because other data references it.";
                        break;
                    }
                case 1261:
                    {
                        Msg = "Remove Empty Row(s) From Uploaded File And Try Again!";
                        break;
                    }
                case 1048:
                    {
                        Msg = "Remove Empty Row(s) From Uploaded File And Try Again!";
                        break;
                    }
                case 1064:
                    {
                        Msg = "Invalid input! Please provide valid input";
                        break;
                    }
                default:
                    {
                        Msg = "Error Occurred";
                        break;
                    }
            }
            DataClass.ShowMessage(_control, DataClass.ErrorType.Error, Msg, true);
        }
    }
}