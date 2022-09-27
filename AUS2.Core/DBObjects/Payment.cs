using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class Payment
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
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
        public DateTime? LastRetryDate { get; set; }
        public string ActionBy { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? ProcessingFee { get; set; }
        public int? ApplicationRequestId { get; set; }
        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }
    }
}
