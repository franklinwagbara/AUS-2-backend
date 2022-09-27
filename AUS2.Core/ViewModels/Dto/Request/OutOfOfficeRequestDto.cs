using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public class OutOfOfficeRequestDto
    {
        public int OutofOfficeId { get; set; }
        public string RelieverEmail { get; set; }
        public string RelievedEmail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
    }
}
