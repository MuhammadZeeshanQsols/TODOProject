using System;
using System.Text;
namespace TODOProject.DataClasses
{
    public class ExceptionStyleFormat
    {
        public static string DisplayFormat(Exception exception, string ControlID = "", string UserErrorMessage = "")
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                stringBuilder.AppendLine("-------------------------------------------------------------------------------------------------------------");
                //sb.AppendLine("ERROR DATE \t: " + System.DateTime.UtcNow.ToString(System.Globalization.CultureInfo.InvariantCulture));
                stringBuilder.AppendLine("ERROR DATE \t: " + System.DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture));
                stringBuilder.AppendLine("-------------------------------------------------------------------------------------------------------------");
                stringBuilder.AppendLine("ERROR MESSAGE \t: " + ControlID + " " + exception.Message);
                stringBuilder.AppendLine("-------------------------------------------------------------------------------------------------------------");
                stringBuilder.AppendLine("USER ERROR MESSAGE \t: " + ControlID + " " + UserErrorMessage);
                stringBuilder.AppendLine("-------------------------------------------------------------------------------------------------------------");
                stringBuilder.AppendLine("SOURCE \t\t: " + exception.Source);
                stringBuilder.AppendLine("-------------------------------------------------------------------------------------------------------------");
                stringBuilder.AppendLine("FORM NAME \t: " + System.Web.HttpContext.Current.Request.Url.ToString());
                stringBuilder.AppendLine("-------------------------------------------------------------------------------------------------------------");
                stringBuilder.AppendLine("QUERYSTRING \t: " + System.Web.HttpContext.Current.Request.QueryString.ToString());
                stringBuilder.AppendLine("-------------------------------------------------------------------------------------------------------------");
                stringBuilder.AppendLine("TARGETSITE \t: " + exception.TargetSite.ToString());
                stringBuilder.AppendLine("-------------------------------------------------------------------------------------------------------------");
                stringBuilder.AppendLine("STACKTRACE \t: " + exception.StackTrace + System.Diagnostics.EventLogEntryType.Error);
                stringBuilder.AppendLine("-------------------------------------------------------------------------------------------------------------");
            }
            catch (Exception ex)
            { }
            return stringBuilder.ToString();
        }
    }
}