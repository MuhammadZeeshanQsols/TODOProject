using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Web.UI;
using TODOProject.DataClasses;

namespace TODOProject.Exceptions
{
    public class DataClass
    {
        public static void ShowMessage(Control Cnt, Type CntType, string Key, string ErrType, string Message, bool AddTags) => ScriptManager.RegisterClientScriptBlock(Cnt, CntType, Key, "ShowMessage('" + ErrType + "','" + Message + "')", AddTags);
        public static void ShowMessage(Control Cnt, string ErrType, string Message, bool AddTags) => ScriptManager.RegisterStartupScript(Cnt, Cnt.GetType(), Guid.NewGuid().ToString(), "ShowMessage('" + ErrType + "','" + Message + "');", true);

        public void DownloadFileByHandler(Page page, string url) => ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), "window.open('" + url + "');", true);

        public void LogInfo(string _control, string _message)
        {
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create(), false);
            LogEntry log = new LogEntry();
            log.Message = string.Format(DateTime.UtcNow + " || " + _control + " || " + _message);
            log.Categories.Add(Category.General);
            log.Priority = Priority.Normal;
            Logger.Write(log);
        }

        public struct ErrorType
        {
            public const string Error = "error";
            public const string Info = "info";
            public const string Success = "success";
        }
    }
}