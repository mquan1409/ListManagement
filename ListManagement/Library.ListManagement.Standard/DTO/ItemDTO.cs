using ListManagement.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ListManagement.Standard.DTO
{
    public class ItemDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public int Priority { get; set; }
        public ItemDTO()
        {

        }
        public ItemDTO(Item i)
        {
            Name = i.Name;
            Description = i.Description;
            Id = i.Id;
            Priority = i.Priority;
        }

        public override string ToString()
        {
            return $"{Id} {Name} {Description}";
        }
    }
}
