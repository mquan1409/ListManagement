using API.ListManagement.database;
using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using ListManagement.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.ListManagement.EC
{
    public class AppointmentEC
    {
        public IEnumerable<AppointmentDTO> Get()
        {
            return FakeDatabase.Appointments.Select(appointment => new AppointmentDTO(appointment));
        }
        public AppointmentDTO AddOrUpdate(AppointmentDTO appointment)
        {
            if (appointment.Id <= 0)
            {
                appointment.Id = ItemService.Current.NextId;
                FakeDatabase.Appointments.Add(new Appointment(appointment));
            }
            else
            {
                var updatedAppointment = FakeDatabase.Appointments.FirstOrDefault(a => a.Id == appointment.Id);
                if (updatedAppointment != null)
                {
                    var index = FakeDatabase.Appointments.IndexOf(updatedAppointment);
                    FakeDatabase.Appointments.Remove(updatedAppointment);
                    FakeDatabase.Appointments.Insert(index, new Appointment(appointment));
                }
                else
                {
                    FakeDatabase.Appointments.Add(new Appointment(appointment));
                }
            }

            return appointment;
        }

        public AppointmentDTO Delete(int id)
        {
            var appointmentToDelete = FakeDatabase.Appointments.FirstOrDefault(i => i.Id == id);
            if (appointmentToDelete != null)
            {
                FakeDatabase.Appointments.Remove(appointmentToDelete);
            }

            return new AppointmentDTO(appointmentToDelete);
        }
    }
}
