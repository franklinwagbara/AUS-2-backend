using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using AUS2.Core.DBObjects;
using AUS2.Core.ViewModels;
using AUS2.Core.DAL.IRepository;
using AUS2.Core.Helper.SerilogService.Account;
using AUS2.Core.ViewModels.Dto.Response;
using AUS2.Core.DAL.Helper.Helpers;
using AUS2.Core.Helper;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.Helper.Helpers.ResponseCodes;
using AUS2.Core.ViewModels.Payment;

namespace AUS2.Core.DAL.Repository.Services.Payment
{
    public class PaymentService
    {
        private readonly ApplicationContext _context;
        private readonly IElpsService _elpsServiceHelper;
        private readonly GeneralLogger _generalLogger;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        WebApiResponse webApiResponse = new WebApiResponse();
        UtilityHelper _utilityHelper;
        GeneralClass _generalClass;
        IConfiguration _configuration;       
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string directory = "Utility";

        public PaymentService(
            IOptions<AppSettings> appSettings,
            ApplicationContext context, 
            IElpsService elpsServiceHelper, 
            GeneralLogger generalLogger, 
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration)
        {
            _configuration = configuration;
            _appSettings = appSettings.Value;
            _context = context;
            _elpsServiceHelper = elpsServiceHelper;
            _generalLogger = generalLogger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _utilityHelper = new UtilityHelper(appSettings, _configuration, _context, _unitOfWork);
            _generalClass = new GeneralClass(_httpContextAccessor, appSettings);

        }

        public async Task<WebApiResponse> CreateExtraPayment(ExtraPaymentRequestDto model)
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var portalURL = _appSettings.PortalBaseUrl;
            var IsExtraPayment= _appSettings.IsExtraPayment;
            var RemitaPayment_URL=  portalURL + _appSettings.RemitaPayment;
            var SuccessExtraPayment_URL= portalURL + _appSettings.SuccessExtraPayment;
            var FailedExtraPayment_URL= portalURL + _appSettings.FailedExtraPayment;

            try
            {
                var requestmodel = _mapper.Map<ExtraPayment>(model);
                var checkextrapayment = _unitOfWork.ExtraPayment.Find(x => x.ApplicationId == model.ApplicationId && x.SanctionType == model.SanctionType).FirstOrDefault();

                if (checkextrapayment != null)
                    return new WebApiResponse { ResponseCode = AppResponseCodes.ExtraPaymentAlreadyExist, Message = "Extra payment is already existing", StatusCode = ResponseCodes.Duplicate };
                else 
                {
                    var app = _unitOfWork.Application.Find(x => x.Id == model.ApplicationId, "User.Company,Facility.LGA.State,Phase").FirstOrDefault();
                    if (app == null)
                        return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "This application record does not exist or has been removed from the system, kindly contact support.", StatusCode = ResponseCodes.RecordNotFound };

                    var refno = _elpsServiceHelper.ReferenceCode();
                    refno = model.SanctionType.Substring(0, 1) + refno.Substring(0, refno.Length - 1);


                    requestmodel.ExtraPaymentAppRef = refno;
                    requestmodel.ExtraPaymentBy = userEmail;
                    requestmodel.CreatedOn = DateTime.Now;
                    _unitOfWork.ExtraPayment.Add(requestmodel);
                    await _unitOfWork.SaveChangesAsync("system");
                    _generalLogger.LogRequest($"{"Extra payment with ref: "}{" "}{requestmodel.ExtraPaymentAppRef}{" for "}{model.ApplicationId}{" was created successfully by"}{userEmail} {" - "}{DateTime.Now}", false, directory);

                    var rmSplit = new RemitaSplit();

                    //rmSplit.payerPhone = appDetail.company.phone;
                    rmSplit.orderId = requestmodel.ExtraPaymentAppRef;
                    rmSplit.CategoryName = app.Phase.Code;
                    rmSplit.payerEmail = app.User.Email;
                    rmSplit.payerName = app.User.Company.Name;
                    rmSplit.ReturnBankPaymentUrl = RemitaPayment_URL  +"/" + requestmodel;
                    rmSplit.ReturnFailureUrl = FailedExtraPayment_URL + "/" + requestmodel;
                    rmSplit.ReturnSuccessUrl = SuccessExtraPayment_URL+ "/" + requestmodel;
                    rmSplit.ServiceCharge = "0";
                    rmSplit.totalAmount = requestmodel.TxnAmount.ToString();
                    rmSplit.Amount = rmSplit.totalAmount;

                    var fields = new List<CustomField>();
                    fields.Add(new CustomField
                    {
                        Name = _appSettings.CompanyName,
                        Value = app.User.Company.Name,
                        Type = _appSettings.All
                    });
                    fields.Add(new CustomField
                    {
                        Name = _appSettings.FacilityAddress,
                        Value = app.Facility.Address,
                        Type = _appSettings.All
                    });
                    fields.Add(new CustomField
                    {
                        Name = _appSettings.State,
                        Value = app.Facility.LGA.State.StateCode,
                        Type = _appSettings.All
                    });

                    rmSplit.CustomFields = fields;

                    rmSplit.lineItems = _utilityHelper.BuildRemitaItems(app, rmSplit, Convert.ToDecimal(rmSplit.totalAmount), _appSettings.IsExtraPayment);

                    var jn = JsonConvert.SerializeObject(rmSplit);

                    _generalLogger.LogRequest("Creation of extra payment split for application with reference:" + app.Reference + "(" + app.User.Company.Name + ") by " + userEmail, false, directory);

                    #region Extra Payment API
                    try
                    {
                        webApiResponse = _elpsServiceHelper.GetExtraPayment_RRR(app.User.ElpsId.ToString(), app.User.Email, rmSplit);

                        #endregion
                        if (webApiResponse == null || webApiResponse.Data== null)
                        {
                            _unitOfWork.ExtraPayment.Remove(requestmodel);
                           await  _unitOfWork.SaveChangesAsync("system");
                            _generalLogger.LogRequest($"Extra payment output from Remita:: {webApiResponse.Data.ToString()} by {userEmail}", true, directory);
                            return new WebApiResponse { ResponseCode = AppResponseCodes.TransactionFailed, Message = "An error occured while generating this extra payment RRR. Please try again or contact support.", StatusCode = ResponseCodes.InternalError };

                        }
                        var resp = JsonConvert.DeserializeObject<PrePaymentResponse>(webApiResponse.Data.ToString());
                      
                        requestmodel.Rrreference = resp.RRR;
                        requestmodel.Status = _appSettings.PaymentPending;
                        _context.SaveChanges();

                        _generalLogger.LogRequest($"Extra payment table updated with RRR: {requestmodel.Rrreference} by {userEmail}", false, directory);

                        #region Send Extra Payment E-Mail To Company
                        string subject = $"Generation Of Extra Payment For Application With Ref:{app.Reference} ( {requestmodel.SanctionType}";

                        var emailBody = string.Format($"An Extra Payment RRR: {requestmodel.Rrreference} has been generated for your application with reference number: {app.Reference} (" + app.Phase.Code + ")" + "): " +
                            "<br /><ul>" +
                            "<li>Amount Generated: {0}</li>" +
                            "<li>Payment Type: {1}</li>" +
                            "<li>Remita RRR: {2}</li>" +
                            "<li>Payment Status: {3}</li>" +
                            "<li>Staff Comment: {4}</li>" +
                            "<li>Facility Name: {5}</li>" +
                            "<li>Facility Address: {6}</li> <br/>" +
                            "<p>Kindly note that your application will be pending until this payment is completed. </p>",
                            requestmodel.TxnAmount.ToString(), requestmodel.SanctionType, requestmodel.Rrreference, requestmodel.Status, requestmodel.TxnMessage, $"{app.Phase.Code}-{app.Facility.ElpsId}", app.Facility.Address);

                        #endregion

                        string successMsg = "New RRR (" + requestmodel.Rrreference + ") generated successfully for company: " + app.User.Company.Name + "; Facility: " + $"{app.Phase.Code}-{app.Facility.ElpsId}";
                        return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = successMsg, Data = requestmodel, StatusCode = ResponseCodes.Success };

                    }
                    catch (Exception x)
                    {
                        _generalLogger.LogRequest($"Extra payment Error Remita:: {x.Message.ToString()} by {userEmail}", true, directory);
                        return new WebApiResponse { ResponseCode = AppResponseCodes.TransactionFailed, Message = "An error occured while generating this extra payment RRR. Please try again or contact support.", StatusCode = ResponseCodes.InternalError };

                    }
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"An error {ex.Message} occured while trying to generate extra payment RRR for this application by {userEmail}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.TransactionFailed, Message = "An error occured while generating this extra payment RRR. Please try again or contact support.", StatusCode = ResponseCodes.InternalError };
            }

        }

        public async Task<WebApiResponse> GetCompanyExtraPayments(string email)
        {
            try
            {
                var extrapayments = _unitOfWork.ExtraPayment.Find(x => x.Application.User.Email.Equals(email), "Application.User").ToList();
               _generalLogger.LogRequest($"{"Fetching all extra payments for company with email: "}{email}{" by"}{_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)} {" - "}{DateTime.Now}", false, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = extrapayments, StatusCode = ResponseCodes.Success };
                
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> GetCompanyPayments(string email)
        {
            try
            {
                var payments = _unitOfWork.Payment.Find(x => x.Application.User.Email.Equals(email)).ToList();

                _generalLogger.LogRequest($"{"Fetching all payments for company with email: "}{email}{" by"}{_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)} {" - "}{DateTime.Now}", false, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = payments, StatusCode = ResponseCodes.Success };

            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

    }
}
