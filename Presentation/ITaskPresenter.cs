using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOProject.Modal;

namespace TODOProject.Presentation
{
   public interface ITaskPresenter
    {
        List<TaskModal> GetTasks();
        string SaveTask(TaskModal taskModal);
            string UpdateTaskOrder(int TaskId,int OrderChangeID);
        string DeleteTaskOrder(int id);
    }
}
