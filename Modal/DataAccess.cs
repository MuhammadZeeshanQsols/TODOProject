using Microsoft.Practices.Unity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using TODOProject.EventArgs;
using TODOProject.Exceptions;
using TODOProject.Modal.Interfaces;

namespace TODOProject.Modal
{
    public class DataAccess
    {
        public const string LogError = "Error generating notification, check Log for details";

        IdbRepository dbRepository;
       
            public DataAccess(IdbRepository _dbRepository)
            {
                dbRepository = _dbRepository;
            }
        public  DataSet ExecuteDataSet(string query)
        {
            IUnityContainer container = new UnityContainer();
            //container.LoadConfiguration("containerOne");
            IdbRepository dbclass = container.Resolve<IdbRepository>();
            Queries _queries = new Queries(dbclass.GetProviderFactory());
            DbCommand command = _queries.GetDbCommandByQuery(query);
            return dbclass.GetDataSetByDbCommand(command);
          }
        public  DataTable ExecuteDataTable(string query)
        {
            IUnityContainer container = new UnityContainer();
            //container.LoadConfiguration("containerOne");
            IdbRepository dbclass = container.Resolve<IdbRepository>();
            Queries _queries = new Queries(dbclass.GetProviderFactory());
            DbCommand command = _queries.GetDbCommandByQuery(query);
            return dbclass.GetDataSetByDbCommand(command).Tables[0];
           
        }
        public List<TaskEventArgs> ExecuteList()
        {
          
            return ConvertDataTable(ExecuteDataTable("SELECT * from Vu_Task"));

        }
        public List<TaskEventArgs> ConvertDataTable(DataTable dt)
        {
            List<TaskEventArgs> data = new List<TaskEventArgs>();
            foreach (DataRow row in dt.Rows)
            {

                data.Add(new TaskEventArgs
                {
                    ID = Convert.ToInt32(row["ID"].ToString()),
                    TaskName = Convert.ToString(row["TaskName"].ToString()),
                    TaskOrder = Convert.ToInt32(row["TaskOrder"].ToString()),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"].ToString()),
                    Ispending = Convert.ToBoolean(row["Ispending"].ToString()),
                    TaskColor = Convert.ToString(row["TaskColor"].ToString())


                });
            }
            return data;
        }
        public static string ExecuteQuery(string query)
        {
            IUnityContainer container = new UnityContainer();
            //container.LoadConfiguration("containerOne");
            IdbRepository dbclass = container.Resolve<IdbRepository>();
            Queries _queries = new Queries(dbclass.GetProviderFactory());
            DbCommand command = _queries.GetDbCommandByQuery(query);
            return dbclass.ExecuteScalar(command).ToString();
         

        }
        #region CUD Operations
        // inserting and update from one sp 


        public  string SaveQuery(TaskEventArgs taskEventArgs)
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
            { DBExceptions.LogException(ex,null, ex.Message); }
            return msg;
        }



        public static string Exceptiontype(TaskEventArgs.ExceptionType type, Exception exception)
        {
            ArgumentExceptions.LogException(exception, LogError);
            return notification("Error", LogError) + " - " + exception.Message;
        }


        public static string notification(string type, string msg)
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