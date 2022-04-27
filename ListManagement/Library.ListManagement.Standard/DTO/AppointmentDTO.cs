using Library.ListManagement.Standard.utilities;
using ListManagement.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ListManagement.Standard.DTO
{
    public class AppointmentDTO : ItemDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<string> Attendees { get; set; }
        public AppointmentDTO(Item i):base(i)
        {
            var appointment = i as Appointment;
            if (appointment != null)
            {
                Start = appointment.Start;
                End = appointment.End;
                Attendees = appointment.Attendees;
            }
        }
        public AppointmentDTO()
        {
            Priority = 1;
            Attendees = new List<string>();
        }
    }
}
