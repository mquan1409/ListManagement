using API.ListManagement.database;
using Library.ListManagement.Standard.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ListManagement.Standard.EC
{
    public class TaskEC
    {
        public IEnumerable<TaskDTO> Get()
        {
            return FakeDatabase.Tasks.Select(task => new TaskDTO(task));
        }
    }
}
