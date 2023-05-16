using TODOProject.EventArgs;
using TODOProject.Modal;
using TODOProject.Modal.Interfaces;
using TODOProject.View;

namespace TODOProject.Presentation
{
    public class TaskPresenter 
    {
        private readonly ITaskView _taskView;
        IdbRepository dbRepository;
        Modal.ModelsData modelsData ;
        public TaskPresenter(ITaskView taskView, IdbRepository _dbRepository)
        {
            this._taskView = taskView;
            dbRepository = _dbRepository;
             modelsData = new ModelsData(dbRepository);
            _taskView.LoadHandler += _taskView_LoadHandler;
            _taskView.SaveHandler += _taskView_SaveHandler;
            _taskView.ChangeOrder += _taskView_ChangeOrder;
            _taskView.ChangeTaskNameHandler += _taskView_ChangeTaskNameHandler;
            _taskView.DeleteHandler += _taskView_DeleteHandler;
            _taskView.ChangeTaskColorHandler += _taskView_ChangeTaskColorHandler;
            
          
        }
        private void _taskView_ChangeOrder(object sender, TaskEventArgs e)
        {
            modelsData.SaveQuery(new TaskEventArgs { ID = e.ID, TaskOrder = e.TaskOrder });
        }
        private void _taskView_ChangeTaskColorHandler(object sender, TaskEventArgs e)
        {
           
            modelsData.SaveQuery(new TaskEventArgs
            {
                ID = e.ID,
                TaskColor=e.TaskColor,

                query = 7
            });
        }

        private void _taskView_ChangeTaskNameHandler(object sender, TaskEventArgs e)
        {
            
            modelsData.SaveQuery(new TaskEventArgs
            {
                ID = e.ID,
                TaskName = e.TaskName,
                query = 3
            });
        }

        private void _taskView_DeleteHandler(object sender, TaskEventArgs e)
        {
            
            modelsData.SaveQuery(new TaskEventArgs
            {
                ID = e.ID,
                TaskName = string.Empty,
                TaskColor = string.Empty,
                TaskOrder = 0,
                query = 5
            });
        }

       

        private void _taskView_SaveHandler(object sender, TaskEventArgs e)
        {
            
            modelsData.SaveQuery(new TaskEventArgs {ID=0,
            TaskName=e.TaskName,
            TaskColor=e.TaskColor,
            TaskOrder=e.TaskOrder,
            query=1});
        }

        private void _taskView_LoadHandler(object sender, TaskEventArgs e)
        {
           
            e.TodoList =  modelsData.GetList();
         }

       

       
       
    }
}