using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Request
{
   public class ApplicationBulkEditRequestDto
    {
        public int ApplicationRequestId { get; set; }
        public string StateCode { get; set; }
        public string LgaCode { get; set; }
    }
}
