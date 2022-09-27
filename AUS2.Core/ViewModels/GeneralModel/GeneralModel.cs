using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Domain.Models.GeneralModel
{
    public class AppMessage
        {
            public object Subject { get; set; }
            public object Content { get; set; }
            public object RefNo { get; set; }
            public object Status { get; set; }
            public object Stage { get; set; }
            public object TotalAmount { get; set; }
            public object Seen { get; set; }
            public string GeneratedNo { get; set; }
            public object CompanyName { get; set; }
            public object CategoryName { get; set; }
            public object FacilityName { get; set; }
            public object StatutoryLicenceFee { get; set; }
            public object ServiceCharge { get; set; }
            public object TotalAmountDue { get; set; }
            public object DateSubmitted { get; set; }
            public object DateApplied { get; set; }

        }

}
