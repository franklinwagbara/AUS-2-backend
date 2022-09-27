using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public class PhaseDto
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public int? Sort { get; set; }
        public bool Status { get; set; }
        public int? LicenseSerial { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? ModuleName { get; set; }
        public decimal Fee { get; set; }
        public decimal ServiceCharge { get; set; }
        public ApplicationModulesRequestDTO Category { get; set; }
    }
}
