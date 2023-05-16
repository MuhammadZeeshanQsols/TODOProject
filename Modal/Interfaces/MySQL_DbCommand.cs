using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;

namespace TODOProject.Modal.Interfaces
{
    public static class MySQL_DbCommand
    {
        public static DbCommand ToCommand(this StringBuilder txtCommand, DbProviderFactory providerFactory)
        {
            DbCommand dbCmd;
            if (providerFactory == null) throw new ArgumentNullException("providerFactory");
            using ( dbCmd = providerFactory.CreateCommand())
            {
              
                    dbCmd.CommandText = txtCommand.ToString();
                    return dbCmd;
                
                
            
            }
            return  dbCmd ; 
        }

        public static DbCommand ToCommand(this StringBuilder txtCommand, DbProviderFactory providerFactory, Collection<object> paramObject)
        {
            if (providerFactory == null) throw new ArgumentNullException("providerFactory");
            using (DbCommand dbCmd = providerFactory.CreateCommand())
            {
                if (paramObject == null || paramObject.Count == 0)
                {
                    dbCmd.CommandText = txtCommand.ToString();
                    return dbCmd;
                }
                List<object> placeholder = new List<object>();
                for (int i = 0; i < paramObject.Count; i++)
                {
                    object param = paramObject[i];
                    DbParameter dbParam = dbCmd.CreateParameter();
                    dbParam.ParameterName = "@p" + i;
                    dbParam.Value = param ?? DBNull.Value;

                    dbCmd.Parameters.Add(dbParam);
                    placeholder.Add(dbParam.ParameterName);
                }
                dbCmd.CommandText = String.Format(CultureInfo.InvariantCulture, txtCommand.ToString(), placeholder.ToArray());
                return dbCmd;
            }
        }

        public static DbCommand ToCommandSP(this string txtCommand, DbProviderFactory providerFactory, Collection<object> paramObject, string spColumnNamesLst)
        {
            if (providerFactory == null) throw new ArgumentNullException("providerFactory");
            using (DbCommand dbCmd = providerFactory.CreateCommand())
            {
                dbCmd.CommandType = CommandType.StoredProcedure;

                if (paramObject == null || paramObject.Count == 0)
                {
                    dbCmd.CommandText = txtCommand;
                    return dbCmd;
                }
                string[] bdCN = (spColumnNamesLst != null ? spColumnNamesLst.Split(',') : null);

                List<object> placeholder = new List<object>();
                for (int i = 0; i < paramObject.Count; i++)
                {
                    object param = paramObject[i];
                    DbParameter dbParam = dbCmd.CreateParameter();
                    if (bdCN != null)
                    {
                        dbParam.ParameterName = "@" + bdCN[i].ToString();
                    }
                    else { dbParam.ParameterName = "@p" + i; }

                    dbParam.Value = param ?? DBNull.Value;

                    dbCmd.Parameters.Add(dbParam);
                    placeholder.Add(dbParam.ParameterName);
                }
                dbCmd.CommandText = String.Format(CultureInfo.InvariantCulture, txtCommand.ToString(), placeholder.ToArray());
                return dbCmd;
            }
        }


        public static List<List<string>> SplitList(this List<string> dtList, int nSize = 500)
        {
            var list = new List<List<string>>();
            var count = dtList.Count;
            for (int i = 0; i < count; i += nSize)
            {
                list.Add(dtList.GetRange(i, Math.Min(nSize, count - i)));
            }
            return list;
        }
    }
}