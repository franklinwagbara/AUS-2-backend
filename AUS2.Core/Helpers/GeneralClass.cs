using AUS2.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using UAParser;

namespace AUS2.Core.Helper
{
    public class GeneralClass : Controller
    {
       IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;
        private Object thisLock = new Object();
        public static string Superadmin = "SUPERADMIN";
        public static string Company = "COMPANY";
        public static string ICT = "ICT";
        public static string Reviewer = "REVIEWER";
        public static string Supervisor = "SUPERVISOR";
        public static string Manager = "MANAGER";
        public static string Support = "SUPPORT";
        public static string PaymentPendingStatus = "Payment Pending";
        public static string PaymentCompletedStatus = "Payment Recieved";
        public static string SuccessfulPaymentStatus = "AUTH";
        public static string PendingPaymentStatus = "INIT";
        public static string Approved = "Approved";
        public static string Processing = "Processing";
        public static string Rejected = "Rejected";
        public static string Initiated = "Initiated";
        public static string ActiveStaff = "ACTIVE";
        public static string PassiveStaff = "PASSIVE";
        public static string HeadOffice = "2";
        public static string WellSuspensionPermit = "WSP";
        public static string PlugAbandonmentPermit = "PAP";
        public static string WellLocationNotice = "WLN";
        public static string WellReEntriesPermit = "WRP";
        public static string ProcessFlowDelegate = "Delegate";
        public static string ProcessFlowReject = "Reject";
        public static string ProcessFlowAccept = "Accept";
        public static string ProcessFlowReSubmit = "ReSubmit"; 
        public static string ProcessFlowGenerateRRR = "GenerateRRR";
        public static string ProcessFlowProceed = "Proceed";
        public static string ProcessFlowSubmit = "Submit";
        public static int LastApprovalStage = 28;
        public static string WorkflowStateContinue = "CONTINUE";
        public static string WorkflowStateProgress = "PROGRESS";
        public static string WorkflowStateComplete = "COMPLETE";

        public GeneralClass(IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> appSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings.Value;
        }

        public string getSessionEmail()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        }


        public string GenerateApplicationNo()
        {
            lock (thisLock)
            {
                Thread.Sleep(1000);
                return DateTime.Now.ToString("MMddyyHHmmss");
            }
        }

        public string StaffMessageTemplate(string subject, string content)
        {
            string body = "<div>";
            body += "<div style='width: 700px; background-color: #ece8d4; padding: 5px 0 5px 0;'><img style='width: 98%; height: 120px; display: block; margin: 0 auto;' src='https://ROMS.dpr.gov.ng/Content/Images/mainlogo.png' alt='Logo'/></div>";
            body += "<div class='text-left' style='background-color: #ece8d4; width: 700px; min-height: 200px;'>";
            body += "<div style='padding: 10px 30px 30px 30px;'>";
            body += "<h5 style='text-align: center; font-weight: 300; padding-bottom: 10px; border-bottom: 1px solid #ddd;'>" + subject + "</h5>";
            body += "<p>Dear Sir/Madam,</p>";
            body += "<p style='line-height: 30px; text-align: justify;'>" + content + "</p>";
            body += "<br>";
            body += "<p>Kindly go to <a href='https://aus2.dpr.gov.ng/'>AUS2 PORTAL(CLICK HERE)</a></p>";
            body += "<p>Department of Petroleum Resources<br/> <small>(AUS) </small></p> </div>";
            body += "<div style='padding:10px 0 10px; 10px; background-color:#888; color:#f9f9f9; width:700px;'> &copy; " + DateTime.Now.Year + " Department of Petroleum Resources &minus; DPR Nigeria</div></div></div>";
            return body;
        }

        public async Task<string> SendStaffEmailMessage(string staffemail, string subject, string content)
        {
            var result = "";
            var apiKey = "BNW5He3DoWQAJVMkeMlEzPTtbYIXNveS4t+GuGtXzxQJ";
            var username = "AKIAQCM2OPFBW35OSTFV";
            var emailFrom = "no-reply@dpr.gov.ng";
            var Host = "email-smtp.us-west-2.amazonaws.com";
            var Port = 587;

            var msgBody = StaffMessageTemplate(subject, content);

            MailMessage _mail = new MailMessage();
            SmtpClient client = new SmtpClient(Host, Port);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(username, apiKey);
            _mail.From = new MailAddress(emailFrom);
            _mail.To.Add(new MailAddress(staffemail));
            _mail.Subject = subject;
            _mail.IsBodyHtml = true;
            _mail.Body = msgBody;
            try
            {
                client.Send(await Task.FromResult(_mail));
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public class Authentications
        {
            public string GetMACAddress()
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String sMacAddress = string.Empty;
                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == String.Empty)// only return MAC Address from first card  
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        properties.GetIPv4Properties();
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }
                return sMacAddress;
            }

            public string GetHostName() => Dns.GetHostName();

            public string BrowserName(HttpContext httpContext)
            {
                //HttpContext context = null;
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                var uaParser = Parser.GetDefault();
                ClientInfo c = uaParser.Parse(userAgent);

                return c.UA.Family + " " + c.UA.Major + "." + c.UA.Minor;
            }

            public string GetLocalIPAddress()
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                var Ip = "";
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Ip = ip.ToString();
                    }
                    else
                    {
                        Ip = ip.ToString();
                    }
                }
                return Ip;
            }

        }
        
    }
}

