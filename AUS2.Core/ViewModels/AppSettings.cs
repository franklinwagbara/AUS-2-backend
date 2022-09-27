using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels
{
    public class AppSettings
    {
        public string ServiceId { get; set; }
        public string AccountNumber { get; set; }
        public string SecreteKey { get; set; }
        public string AppKey { get; set; }
        public string AppEmail { get; set; }
        public string PortalBaseUrl { get; set; }
        public string ElpsUrl { get; set; }
        public string sessionEmail { get; set; }
        public string CompanyName { get; set; }
        public string FacilityAddress { get; set; }
        public string State { get; set; }
        public string All { get; set; }
        public string PaymentCompleted { get; set; }
        public string PaymentPending { get; set; }
        public string OutOfOfficeStarting { get; set; }
        public string OutOfOfficeStarted { get; set; }
        public string OutOfOfficeFinished { get; set; }
        public bool IsExtraPayment { get; set; }
        public string RemitaPayment { get; set; }
        public string SuccessExtraPayment { get; set; }
        public string FailedExtraPayment { get; set; }
        public string LoginUrl { get; set; }

    }

}
