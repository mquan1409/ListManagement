using ListManagement.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ListManagement.Standard.DTO
{
    public class TaskDTO : ItemDTO
    {
        public DateTime Deadline { get; set; }
        public bool isCompleted { get; set; }
        public TaskDTO(Item i):base(i)
        {
            var task = i as Task;
            if (task != null)
            {
                this.Deadline = task.Deadline;
                this.isCompleted = task.isCompleted;
            }
        }
        public TaskDTO()
        {

        }
    }
}
