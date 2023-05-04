using System.Data;
using TODOProject.EventArgs;

namespace TODOProject.Modal
{
    public class ModelsData
    {
        public DataTable GetList() => GenrcModel.GetAll();
        public string SaveQuery(TaskEventArgs taskEventArgs) => GenrcModel.SaveQuery(taskEventArgs);
        

    }
}