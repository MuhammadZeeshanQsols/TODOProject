using System.Collections.Generic;
using System.Data;
namespace TODOProject.EventArgs
{
    public class TaskEventArgs : System.EventArgs
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public int TaskOrder { get; set; }
        public bool IsDeleted { get; set; }
        public bool Ispending { get; set; }
        public string TaskColor { get; set; }
        public int query { get; set; }
       // public DataTable TodoList { get; set; }
        public List<TaskEventArgs> TodoList { get; set; }
        public  enum ExceptionType
        {
            SqlException = 1,
            ArgumentExceptions = 2,
            Exception = 3,
            
        }
    }
   
}