﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class FieldOffice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int StateId { get; set; }
        public DateTime AddedDate { get; set; }
        public bool Status { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }
    }
}
