using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Response
{
    public class ViewApplicationResponseDto
    {
        public string CompanyName { get; set; }
        public string PermitName { get; set; }
        public string ApplicationType { get; set; }
        public string CurrentSatge { get; set; }
        public string IsLegacy { get; set; }
        public decimal? ProcessingFee { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal Arrears { get; set; }
        public decimal? TotalAmountPaid { get; set; }
        public string RemitaRef { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? DatePaymentRecieved { get; set; }
        public string ApplicationStatus { get; set; }
        public string ApplicationRecievedFrom { get; set; }
        public DateTime? ApplicationRecievedDate { get; set; }
        public string ApplicationRecievedOffice { get; set; }
        public string ApplicationRecievedCurrentDesk { get; set; }
        public string? Remarks { get; set; }
       
    }
}
