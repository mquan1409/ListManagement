using Library.ListManagement.Standard.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManagement.models
{
    public class Task : Item
    {
        public DateTime Deadline { get; set; }
        public bool isCompleted { get; set; }
        public Task()
        {
            Priority = 1;
        }
        public Task(TaskDTO task)
        {
            Id = task.Id;
            Name = task.Name;
            Description = task.Description;
            Deadline = task.Deadline;
            Priority = task.Priority;
            isCompleted = task.isCompleted;
        }
    }
}
