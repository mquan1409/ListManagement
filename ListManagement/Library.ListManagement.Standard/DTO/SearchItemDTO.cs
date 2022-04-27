using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ListManagement.Standard.DTO
{
    public class SearchItemDTO
    {
        public string Query { get; set; }
        public bool ShowQuery { get; set; }
        public bool ShowComplete { get; set; }
    }
}
