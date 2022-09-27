using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class ExtraPayment
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string OrderId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string TransactionId { get; set; }
        public string Description { get; set; }
        public string Rrreference { get; set; }
        public string AppReceiptId { get; set; }
        public decimal? TxnAmount { get; set; }
        public decimal Arrears { get; set; }
        public string BankCode { get; set; }
        public string Account { get; set; }
        public string TxnMessage { get; set; }
        public string Status { get; set; }
        public int? RetryCount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastRetryDate { get; set; }
        public string ExtraPaymentAppRef { get; set; }
        public string SanctionType { get; set; }
        public string ExtraPaymentBy { get; set; }
        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }

    }
}
