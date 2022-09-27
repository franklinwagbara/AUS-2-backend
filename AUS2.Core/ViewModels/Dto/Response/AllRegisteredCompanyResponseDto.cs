using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Response
{
    public class AllRegisteredCompanyResponseDto
    {
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public int CompanyElpsId { get; set; }
    }
}
