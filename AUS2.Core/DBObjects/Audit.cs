﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUS2.Core.DBObjects
{
    public class Audit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string TableName { get; set; }
        public DateTime Date { get; set; }
        public string? OldValues { get; set; }
        public string NewValues { get; set; }
        public string? AffectedColumns { get; set; }
        public string PrimaryKey { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
