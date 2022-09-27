using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class Permit
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string PermiTNo { get; set; }
        public int ElpsId { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool Printed { get; set; }
        public string Signature { get; set; }
        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }
    }
}
