using API.ListManagement.database;
using Library.ListManagement.Standard.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ListManagement.Standard.EC
{
    public class AppointmentEC
    {
        public IEnumerable<AppointmentDTO> Get()
        {
            return FakeDatabase.Appointments.Select(appointment => new AppointmentDTO(appointment));
        }
    }
}
