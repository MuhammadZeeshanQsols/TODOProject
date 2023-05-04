using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using TODOProject.EventArgs;

namespace TODOProject.Modal
{
    public static class GenrcModel
    {
        static Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);

        
        public static DataTable GetAll()
        {
            try
            {
                DbCommand dbCmdWrapper = objDB.GetSqlStringCommand("SELECT * from Vu_Task");
                return objDB.ExecuteDataSet(dbCmdWrapper).Tables[0];
            }
            catch (Exception ex)
            {
                //Console.WriteLine("No record found");
                throw ex;
            }
        }
        #region CUD Operations
        // inserting and update from one sp 
    

        public static string SaveQuery(TaskEventArgs taskEventArgs)
        {
            string msg = string.Empty;
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append($"CALL qcms.SP_SaveToDo (");
                query.Append($"{taskEventArgs.ID},");
                query.Append($"{taskEventArgs.TaskName},");
                query.Append($"{taskEventArgs.TaskOrder},");
                query.Append($"{taskEventArgs.TaskColor},");
                query.Append($"{taskEventArgs.query},");
                DbCommand dbCmdWrapper = objDB.GetSqlStringCommand(query.ToString());
                var dataReader = objDB.ExecuteReader(dbCmdWrapper);
                msg = (dataReader.Read() ? dataReader[0].ToString() : null);
                return msg;
            }
            catch (Exception ex)
            { msg = ex.Message; }
            return msg;
        }




        #endregion
    }
}