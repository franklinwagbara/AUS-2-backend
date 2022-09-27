
using AUS2.Core.ViewModels.Dto.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.AdminModel
{
    public class PermitViewModel
    {
        public List<PhaseDto> AllPermits { get; set; }
        public List<ApplicationModuleRequestDto> AllModules { get; set; }
    }

    public class ElpsResponse
    {
        public string message { get; set; }
        public object value { get; set; }
    }
    public class Facilities
    {
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public DateTime DateAdded { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public int LGAId { get; set; }
        public string FacilityType { get; set; }
        public int Id { get; set; }
    }
}
