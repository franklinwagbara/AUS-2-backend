using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public class AppRequestViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int PhaseId { get; set; }
        public int ApplicationTypeId = 1;
        public int LgaId { get; set; }
        public string Location { get; set; }
        [Display(Name = "Uploaded Excel file")]
        public IFormFile doc { get; set; }
        public List<ApplicationFormDto> Applicationforms { get; set; }
    }
}
