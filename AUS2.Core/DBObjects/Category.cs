using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public ICollection<Phase> Phases { get; set; }
    }
}
