using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOProject.Modal;

namespace TODOProject.View
{
   public interface ITaskView : IView<TaskModal>
    {
        List<TaskModal> Customers { get; set; }
        event EventHandler<TaskEventArgs> CustomerCreated;
        event EventHandler<TaskEventArgs> CustomerUpdated;
        event EventHandler<TaskEventArgs> CustomerDeleted;
    }
}
