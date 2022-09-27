using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public class FieldOfficeDto
    {
        public int Id { get; set; }
        [Display(Name = "Field Office Name")]
        public string Name { get; set; }
        [Display(Name = "Field Office Address")]
        public string Address { get; set; }
        [Display(Name = "Field Office State Location")]
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}
