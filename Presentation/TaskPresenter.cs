using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TODOProject.Modal;

namespace TODOProject.Presentation
{
    public class TaskPresenter : ITaskPresenter
    {
        
        public TaskPresenter()
        { 
        }
        public string DeleteTaskOrder(int id)
        {
            throw new NotImplementedException();
        }

        public List<TaskModal> GetTasks()
        {
            throw new NotImplementedException();
        }

        public string SaveTask(TaskModal taskModal)
        {
            throw new NotImplementedException();
        }

        public string UpdateTaskOrder(int TaskId, int OrderChangeID)
        {
            throw new NotImplementedException();
        }
    }
}