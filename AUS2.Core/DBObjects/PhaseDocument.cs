using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class PhaseDocument
    {
        public int Id { get; set; }
        public int PhaseId { get; set; }
        public int ApptypeId { get; set; }
        public int DocId { get; set; }
        public string Name { get; set; }
        public string DocType { get; set; }
        public bool IsMandatory { get; set; }
        public bool Status { get; set; }
        public int? SortId { get; set; }
        [ForeignKey("ApptypeId")]
        public virtual ApplicationType Apptype { get; set; }
        [ForeignKey("PhaseId")]
        public virtual Phase Phase { get; set; }
    }
}
