using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public class LicenseTypeDocsViewModel
    {
        [Required]
        public int ModuleId { get; set; }
        [Required]
        public int LicenseTypeId { get; set; }
        [Required]
        public int AppTypeId { get; set; }
        [Required]
        public int DocId { get; set; }
        [Required]
        public string DocType { get; set; }
        public bool IsMandatory { get; set; }
        public bool Status { get; set; }
        public int? SortId { get; set; }
    }
}
