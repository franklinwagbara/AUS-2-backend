using AUS2.Core.ViewModels.Dto.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Response
{
    public class AppRespnseDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CompanyName { get; set; }
        public int PhaseId { get; set; }
        public string PhaseName { get; set; }
        public string ApplicationType { get; set; }
        public int LgaId { get; set; }
        public int StateId { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string CurrentUser { get; set; }
        public DateTime? AddedDate { get; set; }
        public string Reference { get; set; }
        public List<ApplicationFormDto> Applicationforms { get; set; }
    }
}
