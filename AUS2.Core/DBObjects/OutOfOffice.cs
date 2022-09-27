using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class OutOfOffice
    {
        public int Id { get; set; }
        public string Reliever { get; set; }
        public string Relieved { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
    }
}
