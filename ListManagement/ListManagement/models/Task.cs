using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManagement.models
{
    public class Task
    {
        public string? Name { get; set; }
        public string? Description { get; set; }        
        public string? Deadline { get; set; }
        public Boolean? isCompleted { get; set; }

    }
}
