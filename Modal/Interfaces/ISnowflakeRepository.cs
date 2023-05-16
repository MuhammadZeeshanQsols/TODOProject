using System.Data;
using System.Data.Common;

namespace TODOProject.Modal.Interfaces
{
    public interface ISnowflakeRepository
    {
        object ExecuteScalar(DbCommand command);
        int ExecuteNonQuery(DbCommand Query);
        DataSet GetDataSet(DbCommand command);
        IDataReader ExecuteReader(DbCommand command);
        DbProviderFactory GetProviderFactory();

    }
}
