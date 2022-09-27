using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Response
{
    public class CompanyPermitResponseDto
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string PermitNumber { get; set; }
        public string CompanyName { get; set; }
        public string ApplicationType { get; set; }
        public string ApplicationStatus { get; set; }
        public DateTime? AppliedDate { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Description { get; set; }
    }
}
