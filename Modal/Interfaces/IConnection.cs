using Microsoft.Practices.EnterpriseLibrary.Data;

namespace TODOProject.Modal.Interfaces
{
    public  interface IConnection
    {
        Database Dev_GetDataBase();
        Database QA_GetDatabase();
        Database Stagging_GetDataBase();
    }
}
