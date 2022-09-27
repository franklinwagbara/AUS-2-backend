using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public class ExtraPaymentRequestDto
    {
        public int ApplicationId { get; set; }
        public string LicenseTypeCode { get; set; }
        public string Description { get; set; }
        public decimal? TxnAmount { get; set; }
        public decimal Arrears { get; set; }
        public string TxnMessage { get; set; }
        public string SanctionType { get; set; }
    }

}
