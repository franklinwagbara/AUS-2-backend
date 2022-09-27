using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public class StaffDeskRequestDto
    {
        public string StaffEmail { get; set; }
        public string StaffName { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
        public int OnDesk { get; set; }
        public bool Status { get; set; }
    }
}
