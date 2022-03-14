using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManagement.models
{
    public class Task : Item
    {      
        public string Deadline { get; set; }
        public bool isCompleted { get; set; }

    }
}
