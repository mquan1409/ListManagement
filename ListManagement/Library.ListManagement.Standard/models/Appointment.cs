using Library.ListManagement.Standard.DTO;
using ListManagement.models;
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
        public List<string> Attendees { get; set;  }
        public Appointment()
        {
            Priority = 1;
            Attendees = new List<string>();
        }
        public Appointment(AppointmentDTO appointment)
        {
            Id = appointment.Id;
            Name = appointment.Name;
            Description = appointment.Description;
            Start = appointment.Start;
            End = appointment.End;
            Attendees = appointment.Attendees;
            Priority = appointment.Priority;
        }
    }
}
