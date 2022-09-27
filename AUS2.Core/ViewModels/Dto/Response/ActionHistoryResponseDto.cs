using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Response
{
   public class ActionHistoryResponseDto
    {
        public DateTime? ActionDate { get; set; }
        public string Location { get; set; }
        public string Action { get; set; }
        public string ProcessedBy { get; set; }
        public string ReceivedBy { get; set; }
        public string Message { get; set; }


    }
}
