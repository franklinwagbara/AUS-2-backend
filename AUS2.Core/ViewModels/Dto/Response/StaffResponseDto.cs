using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Response
{
   public class StaffResponseDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userId { get; set; }
        public string email { get; set; }
        public object? phoneNo { get; set; }
        public int id { get; set; }
    }
}
