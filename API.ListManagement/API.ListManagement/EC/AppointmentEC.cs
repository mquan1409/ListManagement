using API.ListManagement.database;
using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using ListManagement.services;
using System;
using System.Collections.Generic;
using System.Text;
using Api.ToDoApplication.Persistence;

namespace API.ListManagement.EC
{
    public class AppointmentEC
    {
        public IEnumerable<AppointmentDTO> Get()
        {
            return Filebase.Current.Appointments.Select(appointment => new AppointmentDTO(appointment));
        }
        public AppointmentDTO AddOrUpdate(AppointmentDTO appointment)
        {
            if (appointment.Id <= 0)
            {
                appointment.Id = ItemService.Current.NextId;
                Filebase.Current.Appointments.Add(new Appointment(appointment));
            }
            else
            {
                var updatedAppointment = FakeDatabase.Appointments.FirstOrDefault(a => a.Id == appointment.Id);
                if (updatedAppointment != null)
                {
                    var index = FakeDatabase.Appointments.IndexOf(updatedAppointment);
                    Filebase.Current.Delete(updatedAppointment);
                    Filebase.Current.AddOrUpdate(new Appointment(appointment));
                }
                else
                {
                    Filebase.Current.AddOrUpdate(new Appointment(appointment));
                }
            }

            return appointment;
        }

        public AppointmentDTO Delete(int id)
        {
            var appointmentToDelete = Filebase.Current.Appointments.FirstOrDefault(i => i.Id == id);
            if (appointmentToDelete != null)
            {
                Filebase.Current.Delete(appointmentToDelete);
            }

            return new AppointmentDTO(appointmentToDelete);
        }
    }
}
