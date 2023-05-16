using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using TODOProject.EventArgs;
using TODOProject.Modal.Interfaces;
namespace TODOProject.Modal
{
    public class Queries
    {
        protected DbProviderFactory dbFactory;
        Collection<object> paramObject;
        public Queries(DbProviderFactory dbFactory) => this.dbFactory = dbFactory;
        public DbCommand GetDbCommandByQuery(string query) =>new StringBuilder().Append(query).ToCommand(dbFactory);
        #region Convert into List
     
        /*  public int ID { get; set; }
        public string TaskName { get; set; }
        public int TaskOrder { get; set; }
        public bool IsDeleted { get; set; }
        public bool Ispending { get; set; }
        public string TaskColor { get; set; }*/
        //public static T GetItem<T>(DataRow dr)
        //{
        //    Type temp = typeof(T);
        //    T obj = Activator.CreateInstance<T>();

        //    foreach (DataColumn column in dr.Table.Columns)
        //    {
        //        foreach (PropertyInfo pro in temp.GetProperties())
        //        {
        //            if (pro.Name == column.ColumnName)
        //                pro.SetValue(obj, dr[column.ColumnName], null);
        //            else
        //                continue;
        //        }
        //    }
        //    return obj;
        //}
        #endregion
    }
}