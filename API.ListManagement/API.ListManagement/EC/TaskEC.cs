using API.ListManagement.database;
using Library.ListManagement.Standard.DTO;
using ListManagement.services;
using ListManagement.models;
using System;
using System.Collections.Generic;
using System.Text;
using Task = ListManagement.models.Task;

namespace API.ListManagement.EC
{
    public class TaskEC
    {
        public IEnumerable<TaskDTO> Get()
        {
            return FakeDatabase.Tasks.Select(task => new TaskDTO(task));
        }
        public TaskDTO AddOrUpdate(TaskDTO task)
        {
            if(task.Id <= 0)
            {
                task.Id = ItemService.Current.NextId;
                FakeDatabase.Tasks.Add(new Task(task));
            }
            else
            {
                var updatedTask = FakeDatabase.Tasks.FirstOrDefault(t => t.Id == task.Id);
                if(updatedTask != null)
                {
                    var index = FakeDatabase.Tasks.IndexOf(updatedTask);
                    FakeDatabase.Tasks.Remove(updatedTask);
                    FakeDatabase.Tasks.Insert(index, new Task(task));
                }
                else
                {
                    FakeDatabase.Tasks.Add(new Task(task));
                }
            }

            return task;
        }
    }
}
