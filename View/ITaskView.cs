using System;
using TODOProject.EventArgs;
using TODOProject.Presentation;

namespace TODOProject.View
{
    public interface ITaskView 
    {
       

        void AttachPresenter(TaskPresenter taskPresenter);
        event EventHandler<TaskEventArgs> LoadHandler;
        event EventHandler<TaskEventArgs> SaveHandler;
        //event EventHandler<TaskEventArgs> ChangeOrder;
        event EventHandler<TaskEventArgs> DeleteHandler;
        event EventHandler<TaskEventArgs> ChangeTaskNameHandler;
        event EventHandler<TaskEventArgs> ChangeTaskColorHandler;
    }
}
