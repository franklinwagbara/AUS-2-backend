using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public class ProcessApplicationRequestDto
    {
        public int applicationId { get; set; }
        public string requestaction { get; set; }
        public string delegateduser { get; set; }
        public string comment { get; set; }
    }
}
