using Library.ListManagement.Standard.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ListManagement.Standard.utilities
{
    public class ItemJsonConverter : JsonCreationConverter<ItemDTO>
    {
        protected override ItemDTO Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["isCompleted"] != null || jObject["IsCompleted"] != null)
            {
                return new TaskDTO();
            }
            else if (jObject["start"] != null || jObject["Start"] != null)
            {
                return new AppointmentDTO();
            }
            else
            {
                return new ItemDTO();
            }
        }
    }
}
