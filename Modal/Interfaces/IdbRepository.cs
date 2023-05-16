using System.Data;
using System.Data.Common;

namespace TODOProject.Modal.Interfaces
{
    public interface IdbRepository
    {
        string DBConnectionString();
        DataSet GetDataSetByDbCommand(DbCommand objCMD);
        

        object ExecuteScalar(DbCommand command);
        int ExecuteNonQuery(string Query);
        int ExecuteNonQuery(DbCommand command);
        IDataReader ExecuteReader(DbCommand command);
        DbProviderFactory GetProviderFactory();
    }
}
