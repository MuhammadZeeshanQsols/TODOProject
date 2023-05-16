using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using ToDoList.Exceptions;
using TODOProject.EventArgs;
using TODOProject.Exceptions;

namespace TODOProject.Modal
{
    public static class GenrcModel
    {
       
        public const string LogError = "Error generating notification, check Log for details";
        static Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
       
        public static DataSet ExecuteDataSet(string query)
        {
            DataSet ds = new DataSet();
            using (MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString))
            {
                MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                mySqlConnection.Open();
                using (var dataAdapter = new MySqlDataAdapter(command))
                {
                    dataAdapter.Fill(ds);
                }
                mySqlConnection.Close();
            }
            return ds;
        }
        public static DataTable ExecuteDataTable(string query)
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString))
            {
                MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                mySqlConnection.Open();
                using (var dataAdapter = new MySqlDataAdapter(command))
                {
                    dataAdapter.Fill(dataTable);
                }
                mySqlConnection.Close();
            }
            return dataTable;
        }
        public static string ExecuteQuery(string query)
        {
            string msg = string.Empty;
            using (MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString))
            {
                MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                //if (mySqlConnection.State == ConnectionState.Open)
                //{ mySqlConnection.Close(); }
                mySqlConnection.Open();
                MySqlDataReader dr = command.ExecuteReader();
                msg = (dr.Read() ? dr[0].ToString() : null);
                mySqlConnection.Close();
            }
            return msg;
           
        }
        public static DataTable GetAll() => ExecuteDataTable("SELECT * from Vu_Task");
       // public static List<TaskEventArgs> GetAll() => ConvertDataTable(ExecuteDataTable("SELECT * from Vu_Task");
        #region CUD Operations
        // inserting and update from one sp 
       

        public static string SaveQuery(TaskEventArgs taskEventArgs)
        {
            string msg = string.Empty;
            try
            {
                string emptycolor = "white";
                StringBuilder query = new StringBuilder();
                query.Append($"CALL qcms.SP_SaveToDo (");
                query.Append($"{taskEventArgs.ID},");
                query.Append($"'{taskEventArgs.TaskName}',");
                query.Append($"{taskEventArgs.TaskOrder},");
                query.Append($"'{taskEventArgs.TaskColor}',");
                query.Append($"{taskEventArgs.query})");
            
                 msg = ExecuteQuery(query.ToString());
                return msg;
            }
            catch (MySqlException ex)
            { DBExceptions.LogException(ex, null, ex.Message); }
            return msg;
        }


        public static string Exceptiontype(TaskEventArgs.ExceptionType type, MySqlException exception)
        {
           
            if (type== TaskEventArgs.ExceptionType.SqlException)
            DBExceptions.LogException(exception, null, LogError);
            if (type == TaskEventArgs.ExceptionType.ArgumentExceptions)
            ArgumentExceptions.LogException(exception, LogError);
            else if (type == TaskEventArgs.ExceptionType.Exception)
            GenericExceptions.LogException(exception, LogError);

            return notification("Error", LogError) + " - " + exception.Message;

        }
      
        public static string Exceptiontype(TaskEventArgs.ExceptionType type, Exception exception)
        {
            ArgumentExceptions.LogException(exception, LogError);
            return notification("Error", LogError) + " - " + exception.Message;
        }


        public static string  notification(string type, string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (type == "Error")
                {
                    return "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    return "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    return string.Empty;
                }
                else { return string.Empty; }
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion
    }
}