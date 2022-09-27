using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class Facility
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int LgaId { get; set; }
        public int ElpsId { get; set; }
        public string Address { get; set; }
        public string Cordinates { get; set; }
        [ForeignKey("LgaId")]
        public LGA LGA { get; set; }
    }
}
