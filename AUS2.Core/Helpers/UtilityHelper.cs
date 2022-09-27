using AUS2.Core.DAL.IRepository;
using AUS2.Core.DBObjects;
using AUS2.Core.ViewModels;
using AUS2.Core.ViewModels.Payment;
using AUS2.Domain.Models.GeneralModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AUS2.Core.DAL.Helper.Helpers
{
   public class UtilityHelper : Controller
    {
        private readonly AppSettings _appSettings;
        IConfiguration _configuration;
        private readonly ApplicationContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public UtilityHelper(
            IOptions<AppSettings> appSettings,
            IConfiguration configuration,
            ApplicationContext context,
            IUnitOfWork unitOfWork)
        {
            _appSettings = appSettings.Value;
            _configuration = configuration;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public List<RPartner> BuildRemitaItems(Application application, RemitaSplit rmSplit, decimal amount = 0, bool extra = false)
        {

            string ServiceTypeID = _configuration.GetSection("RemitaSplit").GetSection("ServiceTypeID").Value.ToString();

            string beneficiaryName = _configuration.GetSection("RemitaSplit").GetSection("AccName_1").Value.ToString();

            string bankCode = _configuration.GetSection("RemitaSplit").GetSection("bankCode").Value.ToString();

            string beneficiaryAccount = _configuration.GetSection("RemitaSplit").GetSection("Acc_1").Value.ToString();

            string deductFeeFrom = _configuration.GetSection("RemitaSplit").GetSection("AccDeduct_1").Value.ToString();

            string deductIGRFeeFrom = _configuration.GetSection("RemitaSplit").GetSection("AccDeduct_2").Value.ToString();

            var IGRAccount = _configuration.GetSection("RemitaSplit").GetSection("IGRAccount").Value.ToString();

            var IGRBankCode = _configuration.GetSection("RemitaSplit").GetSection("IGRBankCode").Value.ToString();

            var TargetBankCode = _configuration.GetSection("RemitaSplit").GetSection("TargetBankCode").Value.ToString();

            var TargetBankAccount = _configuration.GetSection("RemitaSplit").GetSection("TargetAccount").Value.ToString();


            rmSplit.serviceTypeId = ServiceTypeID;

            var rp = new List<RPartner>();

            #region build remita split

            if (extra == false) //Not an extra payment
            {
                if (application.Phase.Code == "")
                {


                }

            }
            else //extra payment
            {

                rp.Add(new RPartner

                {

                    lineItemsId = "1",

                    beneficiaryName = beneficiaryName,

                    bankCode = bankCode,

                    beneficiaryAccount = beneficiaryAccount,

                    beneficiaryAmount = rmSplit.Amount,

                    deductFeeFrom = deductFeeFrom

                });

            }
            return rp;
            #endregion

        }

        public async Task<AppMessage> SaveMessage(int AppID, int userID, string subject, string content, string userElpsID)
        {
            Message messages = new Message()
            {
                CompanyId = userID,
                UserId = userID,
                ApplicationId = AppID,
                Subject = subject,
                Content = content,
                SenderId = userElpsID,
                Read = 0,
                Date = DateTime.UtcNow.AddHours(1)
            };
            _unitOfWork.Message.Add(messages);
            await _unitOfWork.SaveChangesAsync("system");
            _context.SaveChanges();

            var appMsg = _unitOfWork.Application
                .Find(x => x.Id == AppID && x.Messages.FirstOrDefault().Id == messages.Id)
                .Select(x =>new AppMessage
                {
                    Subject = x.Messages.FirstOrDefault().Subject,
                    Content = x.Messages.FirstOrDefault().Content,
                    RefNo = x.Reference,
                    Status = x.Status,
                    Seen = x.Messages.FirstOrDefault().Read,
                    CompanyName = x.User.Company.Name,
                    FacilityName = $"{x.Phase.Code}-{x.Facility.ElpsId}",
                    CategoryName = x.Phase.Code,
                    StatutoryLicenceFee = x.Phase.Fee,
                    TotalAmountDue = x.Payments.Sum(x => x.TxnAmount),
                    DateApplied = x.AddedDate
                }).FirstOrDefault();

            return appMsg;

        }

        public string SendEmailMessage(string email_to, string email_to_name, AppMessage AppMessage, byte[] attach)
        {
            var result = "";
            var password = _configuration.GetSection("MailSetting").GetSection("mailPass").Value.ToString();
            var username = _configuration.GetSection("MailSetting").GetSection("UserName").Value.ToString();
            var emailFrom = _configuration.GetSection("MailSetting").GetSection("mailSender").Value.ToString();
            var Host = _configuration.GetSection("MailSetting").GetSection("mailHost").Value.ToString();
            var Port = Convert.ToInt16(_configuration.GetSection("MailSetting").GetSection("ServerPort").Value.ToString());

            var msgBody = CompanyMessageTemplate(AppMessage);

            MailMessage _mail = new MailMessage();
            SmtpClient client = new SmtpClient(Host, Port);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(username, password);
            _mail.From = new MailAddress(emailFrom);
            _mail.To.Add(new MailAddress(email_to, email_to_name));
            _mail.Subject = AppMessage.Subject.ToString();
            _mail.IsBodyHtml = true;
            _mail.Body = msgBody;
            if (attach != null)
            {
                string name = "App Letter";
                Attachment at = new Attachment(new MemoryStream(attach), name);
                _mail.Attachments.Add(at);
            }
            //_mail.CC=
            try
            {
                client.Send(_mail);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string CompanyMessageTemplate(AppMessage msg)
        {
            string body = "<div>";


            body += "<div style='width: 800px; background-color: #ece8d4; padding: 5px 0 5px 0;'><img style='width: 98%; height: 120px; display: block; margin: 0 auto;' src='~/Images/NUPRC_Logo.JPG' alt='Logo'/></div>";
            body += "<div class='text-left' style='background-color: #ece8d4; width: 800px; min-height: 200px;'>";
            body += "<div style='padding: 10px 30px 30px 30px;'>";
            body += "<h5 style='text-align: center; font-weight: 300; padding-bottom: 10px; border-bottom: 1px solid #ddd;'>" + msg.Subject + "</h5>";
            body += "<p>Dear Sir/Madam,</p>";
            body += "<p style='line-height: 30px; text-align: justify;'>" + msg.Content + "</p>";

            body += "<table style = 'width: 100%;'><tbody>";
            body += "<tr><td style='width: 200px;'><strong>App Ref No:</strong></td><td> " + msg.RefNo + " </td></tr>";
            body += "<tr><td style='width: 200px;'><strong>Company Name:</strong></td><td> " + msg.CompanyName + " </td></tr>";
            body += "<tr><td><strong>Facility Name:</strong></td><td> " + msg.FacilityName + " </td></tr>";
            body += "<tr><td><strong>Statutory Licence Fee:</strong></td><td> " + msg.StatutoryLicenceFee + " </td></tr>";
            //body += "<tr><td><strong>Service Charge:</strong></td><td> " + msg.ServiceCharge + " </td></tr>";
            body += "<tr><td><strong>Total Amount Due:</strong></td><td> " + msg.TotalAmountDue + " </td></tr>";
            body += "<tr><td><strong>Category:</strong></td><td> " + msg.CategoryName + "</td></tr>";
            body += "<tr><td><strong>Date Applied:</strong></td><td> " + msg.DateApplied + " </td></tr>";
            body += "<tr><td><strong>Status:</strong></td><td> " + msg.Status + " </td></tr>";
            body += "</tbody></table><br/>";

            body += "<p> </p>";
            body += "<p>Nigerian Upstream Petroleum Regulatory Commission<br/> <small>(AUS) </small></p> </div>";
            body += "<div style='padding:10px 0 10px; 10px; background-color:#888; color:#f9f9f9; width:800px;'> &copy; " + DateTime.UtcNow.AddHours(1).Year + " Nigerian Upstream Petroleum Regulatory Commission &minus; NUPRC Nigeria</div></div></div>";

            return body;
        }
    }


}
