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
        public int PhaseId { get; set; }
        public string ApplicationType = "New";
        public int LgaId { get; set; }
        public string Location { get; set; }
        public List<ApplicationFormDto> Applicationforms { get; set; }
    }
}
