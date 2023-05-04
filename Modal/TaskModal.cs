using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TODOProject.Modal
{
    public class TaskModal
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public int TaskOrder { get; set; }
        public bool IsDeleted { get; set; }
        public bool Ispending { get; set; }
        public string TaskColor { get; set; }

    }
}