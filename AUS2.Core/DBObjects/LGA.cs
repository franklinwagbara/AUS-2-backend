using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class LGA
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string LgaCode { get; set; }
        public string LgaName { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }
    }
}
