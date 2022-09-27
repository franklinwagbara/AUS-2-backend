using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public class ActivateDeActivateStaffRequestDto
    {
        public bool Status { get; set; }
        public string StaffEmail { get; set; }
        public string Comment { get; set; }

    }
}
