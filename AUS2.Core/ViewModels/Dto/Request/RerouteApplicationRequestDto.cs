using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public class RerouteApplicationRequestDto
    {
        public string Reference { get; set; }
        public string NewStaffEmail { get; set; }
        public string OldStaffEmail { get; set; }
        public string Comment { get; set; }
    }
}
