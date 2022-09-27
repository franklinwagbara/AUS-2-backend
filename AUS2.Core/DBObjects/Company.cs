using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? RegisteredAddressId { get; set; }
        public string RegisteredAddress { get; set; }
        public string CacNumber { get; set; }
        public string TIN { get; set; }
        public int NationalityId { get; set; }
        public int StateId { get; set; }
        public string YearIncorporated { get; set; }
        public string PostalCode { get; set; }

    }
}
