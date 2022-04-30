using API.ListManagement.database;
using Library.ListManagement.Standard.DTO;
using ListManagement.services;
using ListManagement.models;
using System;
using System.Collections.Generic;
using System.Text;
using Task = ListManagement.models.Task;
using Api.ToDoApplication.Persistence;

namespace API.ListManagement.EC
{
    public class TaskEC
    {
        public IEnumerable<TaskDTO> Get()
        {
            return Filebase.Current.Tasks.Select(t => new TaskDTO(t));
        }
        public TaskDTO AddOrUpdate(TaskDTO task)
        {
            if(task.Id <= 0)
            {
                task.Id = ItemService.Current.NextId;
                Filebase.Current.AddOrUpdate(new Task(task));
            }
            else
            {
                var updatedTask = FakeDatabase.Tasks.FirstOrDefault(t => t.Id == task.Id);
                if(updatedTask != null)
                {
                    var index = FakeDatabase.Tasks.IndexOf(updatedTask);
                    Filebase.Current.Delete(updatedTask);
                    Filebase.Current.AddOrUpdate(new Task(task));
                }
                else
                {
                    Filebase.Current.AddOrUpdate(new Task(task));
                }
            }
            return task;
        }
        public TaskDTO Delete(int id)
        {
            var taskToDelete = Filebase.Current.Tasks.FirstOrDefault(i => i.Id == id);
            if (taskToDelete != null)
            {
                Filebase.Current.Delete(taskToDelete);
            }

            return new TaskDTO(taskToDelete);
        }
    }
}