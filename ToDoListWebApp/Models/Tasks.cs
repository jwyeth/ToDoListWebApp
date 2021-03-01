using System;
using System.Collections.Generic;

namespace ToDoListWebApp.Models
{
    public partial class Tasks
    {
        public int TaskId { get; set; }
        public string OwnerId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }

        public virtual AspNetUsers Owner { get; set; }
    }
}
