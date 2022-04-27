using Library.ListManagement.Standard.utilities;
using ListManagement.models;
using Newtonsoft.Json;
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
                Deadline = task.Deadline;
                isCompleted = task.isCompleted;
            }
        }
        public TaskDTO()
        {
            Priority = 1;
        }
        public override string ToString()
        {
            return $"{Id} {Name} {Description} {isCompleted}"; 
        }
    }
}
