﻿using ListManagement.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManagement.models
{
    public class Appointment : Item
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<string>? Attendees { get; set;  }
        public Appointment()
        {
            Attendees = new List<string>();
        }
    }
}
