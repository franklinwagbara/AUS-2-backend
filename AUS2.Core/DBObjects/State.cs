using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class State
    {
        public int Id { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string StateAddress { get; set; }
    }
}
