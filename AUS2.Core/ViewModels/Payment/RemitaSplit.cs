﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Payment
{
    
        public class RemitaSplit
        {
            //public string merchantId { get; set; }
            public string serviceTypeId { get; set; }
            public string totalAmount { get; set; }
            public string hash { get; set; }
            public string payerName { get; set; }
            public string payerEmail { get; set; }
            public string payerPhone { get; set; }
            public string orderId { get; set; }
            public List<RPartner> lineItems { get; set; }
            public string ServiceCharge { get; set; }
            public string AmountDue { get; set; }
            public string Amount { get; set; }
            public string ReturnSuccessUrl { get; set; }
            public string ReturnFailureUrl { get; set; }
            public string ReturnBankPaymentUrl { get; set; }
            public List<int> DocumentTypes { get; set; }
            public string CategoryName { get; set; }
            public string IGRFee { get; set; }
            public List<ApplicationItem> ApplicationItems { get; set; }
            public List<CustomField> CustomFields { get; set; }

        }

        public class RPartner
        {
            public string lineItemsId { get; set; }
            public string beneficiaryName { get; set; }
            public string beneficiaryAccount { get; set; }
            public string bankCode { get; set; }
            public string beneficiaryAmount { get; set; }
            public string deductFeeFrom { get; set; }
        }

        public class ApplicationItem
        {
            public string Group { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }


        }

        public class CustomField
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string Type { get; set; }
        }

    public class RemitaResponse
    {
        public string statusmessage { get; set; }
        public string merchantId { get; set; }
        public string status { get; set; }
        public string RRR { get; set; }
        public string Amount { get; set; }
        public string transactiontime { get; set; }
        public string orderId { get; set; }


    }

    public class PrePaymentResponse
    {
        public string StatusMessage { get; set; }
        public string AppId { get; set; }
        public string Status { get; set; }
        public string RRR { get; set; }
        public string Transactiontime { get; set; }
        public string TransactionId { get; set; }
    }

    public class NewRemitaResponse
    {
        public string orderId { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string transactiontime { get; set; }
        public string amount { get; set; }

        //public static NewRemitaResponse CheckRRRPayment(string rrr)
        //{
        //    try
        //    {



        //        string MERCHANTID = _configuration.GetSection("AmountSetting").GetSection("merchantID").Value.ToString();
        //        string APIKEY = _configuration.GetSection("AmountSetting").GetSection("rKey").Value.ToString();
        //        string hash_string = rrr.Trim() + APIKEY + MERCHANTID;
        //        string hash = generalClass.Encrypt(hash_string);

        //        var paramDatas = _restService.parameterData("id", rrr);
        //        var responsee = _restService.Response("/Payment/checkifpaid/{id}/{email}/{apiHash}", paramDatas, "POST");


        //        NewRemitaResponse response = JsonConvert.DeserializeObject<NewRemitaResponse>(responsee.ToString());

        //        if (response != null && ((response.message.ToString().ToLower() == "approved" && response.status.ToString() == "00") || (response.message.ToString().ToLower() == "successful" && response.status.ToString() == "00")))

        //        {
        //            return response;

        //        }
        //        return response;

        //    }

        //    catch (Exception x)
        //    {
        //        _helpersController.LogMessages(x.ToString());
        //        return null;
        //    }
        //}
    }

}
