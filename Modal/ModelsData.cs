using System.Collections.Generic;
using TODOProject.EventArgs;
using TODOProject.Modal.Interfaces;

namespace TODOProject.Modal
{
    public class ModelsData
    {
        IdbRepository dbRepository;
        DataAccess dataAccess;
        public ModelsData(IdbRepository _dbRepository)
        {
            dbRepository = _dbRepository;
            dataAccess=  new DataAccess(dbRepository);
        }

        
        public  List<TaskEventArgs> GetList()
        {
            
            return dataAccess.ExecuteList();
        }
       
        public string SaveQuery(TaskEventArgs taskEventArgs)
        {
            
            return dataAccess.SaveQuery(taskEventArgs);
         }


    }
}