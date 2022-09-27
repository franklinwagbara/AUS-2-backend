using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class Phase
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int Sort { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public decimal Fee { get; set; }
        public decimal ServiceCharge { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int LicenseSerial { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<PhaseDocument> PhaseDocuments { get; set; }
    }
}
