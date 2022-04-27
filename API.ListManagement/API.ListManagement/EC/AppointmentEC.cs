﻿using API.ListManagement.database;
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
            }
            else
            {
                var updatedAppointment = FakeDatabase.Tasks.FirstOrDefault(a => a.Id == appointment.Id);
                if (updatedAppointment != null)
                {
                    var index = FakeDatabase.Tasks.IndexOf(updatedAppointment);
                    FakeDatabase.Tasks.Remove(updatedAppointment);
                    FakeDatabase.Tasks.Insert(index, new Appointment(appointment));
                }
                else
                {
                    FakeDatabase.Tasks.Add(new Appointment(appointment));
                }
            }

            return appointment;
        }

    }
}