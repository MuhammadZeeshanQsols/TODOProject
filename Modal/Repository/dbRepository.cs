using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using TODOProject.Modal.Interfaces;

namespace TODOProject.Modal.Repository
{
    public class dbRepository : IdbRepository
    {
        private Database _db;
        private IConnection _idb;
        public dbRepository(IConnection idb)
        {
            _idb = idb as IConnection;
            _db = _idb.Dev_GetDataBase();
        }
        public string DBConnectionString()
        {
            return _db.ConnectionString;
        }
        public DataSet GetDataSetByDbCommand(DbCommand objCMD)
        {
            return _db.ExecuteDataSet(objCMD);
        }
      
       
        public object ExecuteScalar(DbCommand command)
        {
            return _db.ExecuteScalar(command);
        }
        public int ExecuteNonQuery(string Query)
        {
            return _db.ExecuteNonQuery(CommandType.Text, Query);
        }
        public int ExecuteNonQuery(DbCommand command)
        {
            return _db.ExecuteNonQuery(command);
        }
        public DbProviderFactory GetProviderFactory()
        {
            return _db.DbProviderFactory;
        }
        public IDataReader ExecuteReader(DbCommand command)
        {
            return _db.ExecuteReader(command);
        }

         
    }
}