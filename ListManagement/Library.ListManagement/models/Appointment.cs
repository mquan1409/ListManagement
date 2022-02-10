using ListManagement.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ListManagement.models
{
    public class Appointment : Item
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<string>? Attendess { get; set;  }
        public Appointment()
        {
            Attendess = new List<string>();
        }
    }
}
