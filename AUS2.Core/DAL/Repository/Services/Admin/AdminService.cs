using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using AUS2.Core.DAL.IRepository;
using AUS2.Core.DBObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AUS2.Core.Helper.SerilogService.Account;
using AUS2.Core.ViewModels;
using AUS2.Core.Helper;
using AUS2.Core.ViewModels.Dto.Response;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.Helper.Helpers.ResponseCodes;
using AUS2.Core.ViewModels.AdminModel;

namespace AUS2.Core.Repository.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GeneralLogger _generalLogger;
        private readonly AppSettings _appSettings;
        WebApiResponse webApiResponse = new WebApiResponse();
        private readonly IHttpContextAccessor _httpContextAccessor;
        GeneralClass _generalClass;
        private readonly string directory = "OutOfOffice";
        public AdminService(
            IOptions<AppSettings> appSettings,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ApplicationContext context,
            GeneralLogger adminLogger,
            IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _unitOfWork = unitOfWork;
            _generalLogger = adminLogger;
            _httpContextAccessor = httpContextAccessor;
            _generalClass = new GeneralClass(_httpContextAccessor, appSettings);

        }

        public async Task<WebApiResponse> AddOutOfOffice(OutOfOfficeRequestDto model)
        {
            try
            {
                if (model == null)
                {
                    _generalLogger.LogRequest($"{"AddOutOfOffice-- Required input fields are empty"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Failed, Message = "Required input fields are empty", StatusCode = ResponseCodes.RecordNotFound };
                }
                else
                {
                    var requestmodel = _mapper.Map<OutOfOffice>(model);
                    requestmodel.Status = _appSettings.OutOfOfficeStarting;

                    _unitOfWork.OutOfOffice.Add(requestmodel);
                    await _unitOfWork.SaveChangesAsync("system");
                    _generalLogger.LogRequest($"{"AddOutOfOffice-- Record was successfully added"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = requestmodel, StatusCode = ResponseCodes.Success };
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"AddOutOfOffice-- An exception occurred " + ex.ToString()}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "An exception occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> DeleteOutOfOffice(Delete_EditOutOfOfficeRequestDto model)
        {
            try
            {
                //var useremail = (from u in _context.OutofOffice where u.Relieved == model.RelievedStaffEmail && u.OutofOfficeId == model.OutofOfficeId && u.Status == _appSettings.OutOfOfficeStarting select u).ToList();
                var item = _unitOfWork.OutOfOffice.Find(x => x.Id == model.OutofOfficeId).FirstOrDefault();
                if (item != null)
                {
                    _unitOfWork.OutOfOffice.Remove(item);
                    await _unitOfWork.SaveChangesAsync("system");
                    //_context.OutofOffice.Remove(useremail.FirstOrDefault());
                    //await _context.SaveChangesAsync();
                    _generalLogger.LogRequest($"{"Out of office was successfully deleted"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Out of office was successfully deleted", Data = model, StatusCode = ResponseCodes.InternalError };

                }
                else
                {
                    _generalLogger.LogRequest($"{" Unable to delete out of office. The status must be (Starting before it can be deleted)"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Failed, Message = "Unable to delete out of office. The status must be (Starting before it can be deleted)", Data = model, StatusCode = ResponseCodes.RecordNotFound };
                }

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"An exception occurred" + model.RelievedStaffEmail}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "An exception occurred " + ex.ToString(), Data = model, StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> EditOutOfOffice(OutOfOfficeRequestDto model)
        {
            try
            {
                var outofoffice = _unitOfWork.OutOfOffice.Find(x => x.Id == model.OutofOfficeId).FirstOrDefault();
                if (outofoffice != null)
                {
                    outofoffice.Reliever = model.RelieverEmail;
                    outofoffice.Relieved = model.RelievedEmail;
                    outofoffice.EndDate = model.EndDate;
                    outofoffice.StartDate = model.StartDate;
                    outofoffice.Comment = model.Comment;

                    _unitOfWork.OutOfOffice.Update(outofoffice);
                    await _unitOfWork.SaveChangesAsync("system");

                    _generalLogger.LogRequest($"{"Out of office update was successful"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "successful", Data = model, StatusCode = ResponseCodes.Success };

                }
                else
                {
                    _generalLogger.LogRequest($"{"EditOutOfOffice -- No record found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No record found", StatusCode = ResponseCodes.RecordNotFound };
                }

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"EditOutOfOffice -- An exception occurred"}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "An exception occurred " + ex.ToString(), Data = model, StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> EndLeave(Delete_EditOutOfOfficeRequestDto model)
        {
            try
            {
                var Outoffice = _unitOfWork.OutOfOffice.Find(x => x.Id == model.OutofOfficeId).FirstOrDefault();
                if (Outoffice != null)
                {
                    Outoffice.Status = _appSettings.OutOfOfficeFinished;
                    _unitOfWork.OutOfOffice.Update(Outoffice);
                    await _unitOfWork.SaveChangesAsync("system");
                    _generalLogger.LogRequest($"{"End Leave was successfully executed)"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "successful", StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"Unable to retrieve record to update)"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No record found", StatusCode = ResponseCodes.RecordNotFound };
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"EndLeave --Internal server error occurred)"}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }
        }

        public async Task<WebApiResponse> GetAllStaffOutOfOffice()
        {
            try
            {
                var outofoffice = _unitOfWork.OutOfOffice.GetAll().Select(p => 
                                   new OutOfOfficeRequestDto()
                                   {
                                       RelieverEmail = p.Reliever,
                                       RelievedEmail = p.Relieved,
                                       StartDate = Convert.ToDateTime(p.StartDate),
                                       EndDate = Convert.ToDateTime(p.EndDate),
                                       Status = p.Status,
                                       Comment = p.Comment
                                   }).ToList();

                if (outofoffice.Count > 0)
                {
                    _generalLogger.LogRequest($"{"All out office staff was successfully fetched"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = outofoffice, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"All out office staff-- NO Record Found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "NO Record Found", StatusCode = ResponseCodes.RecordNotFound };
                }


            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Internal server error occurred while trying to fetch all out office staff"}{"-"}{DateTime.Now}", false, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }
        }

        public async Task<WebApiResponse> GetRelievedStaffOutOfOffice()
        {
            try
            {
                var outofoffice = (from p in _context.OutOfOffices.AsEnumerable()
                                   join r in _context.Applications on p.Relieved equals r.CurrentUser
                                   where p.Reliever == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email) && p.Status == _appSettings.OutOfOfficeStarted
                                   select new OutOfOfficeRequestDto()
                                   {
                                       RelieverEmail = p.Reliever,
                                       RelievedEmail = p.Relieved,
                                       StartDate = Convert.ToDateTime(p.StartDate),
                                       EndDate = Convert.ToDateTime(p.EndDate),
                                       Status = p.Status,
                                       Comment = p.Comment
                                   }).ToList();

                if (outofoffice.Count > 0)
                {
                    _generalLogger.LogRequest($"{"All relieved staff out office was successfully fetched"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = outofoffice, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"Relieved staff out office-- No Record Found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No Record Found", StatusCode = ResponseCodes.RecordNotFound };
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Internal server error occurred while trying to fetch relieved staff out of office"}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }
        }

        public async Task<WebApiResponse> StaffDesk()
        {
            try
            {
                var roles = new[] { "SuperAdmin", "Support", "Company" };
                var desk = (from u in _userManager.Users.Include(x => x.Office).Include(x => x.UserRoles).ThenInclude(x => x.Role).AsEnumerable()
                            where !roles.Contains(u.UserRoles.FirstOrDefault().Role.Name)
                            select new StaffDeskRequestDto
                            {
                                Role = u.UserRoles.LastOrDefault().Role.Name,
                                StaffEmail = u.Email,
                                StaffName = $"{u.LastName} {u.FirstName}",
                                Location = u.Office.Name,
                                Status = u.Status,
                                OnDesk = _unitOfWork.Application.Find(x => x.CurrentUser.Equals(u.Email)).Count()
                            }).ToList();
                if (desk.Count > 0)
                {
                    _generalLogger.LogRequest($"{"Staff desk was successfully fetched"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = desk, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"Staff desk-- No Record Found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No Record Found", StatusCode = ResponseCodes.RecordNotFound };
                }

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Internal server error occurred while trying to fetch staff desk"}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }


        }

        public async Task<WebApiResponse> RerouteApplication(List<RerouteApplicationRequestDto> model)
        {
            try
            {
                if (model.Count > 0)
                {
                    var ids = model.Select(x => x.Reference);
                    var apps = _unitOfWork.Application.Find(x => x.CurrentUser.Equals(model.FirstOrDefault().OldStaffEmail) && ids.Contains(x.Reference), "AppHistories").ToList();

                    apps.ForEach(x =>
                    {
                        x.CurrentUser = model.FirstOrDefault().NewStaffEmail;
                        x.ModifiedDate = DateTime.Now;
                        x.AppHistories.LastOrDefault().Message += $"\n Application was Re-Assigned to {model.FirstOrDefault().NewStaffEmail} by {_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)}";
                    });

                    _unitOfWork.Application.UpdateRange(apps);
                    await _unitOfWork.SaveChangesAsync("system");

                    var subject = "Re-Routed Application";
                    string content = "Application with the reference number(s) - " + string.Join(",", ids) + " have been re-routed to your desk.";
                    var sendemail = _generalClass.SendStaffEmailMessage(model.FirstOrDefault().NewStaffEmail, subject, content);

                    _generalLogger.LogRequest($"{"Application(s) was/were successfully re-routed"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"Application Re-route list was empty"}{"-"}{DateTime.Now}", true, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "Application Re-route list was empty", StatusCode = ResponseCodes.RecordNotFound };
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Internal server error occurred while trying to Re-route Application"}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }
        }

        public async Task<WebApiResponse> RerouteApplicationDropdownlist(string email)
        {
            try
            {
                var staff = _userManager.Users
                    .Include(x => x.Office)
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .FirstOrDefault(x => x.Email.Equals(email));

                var users = _userManager.Users
                    .Include(x => x.Office)
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .Where( x => x.UserRoles.FirstOrDefault().Role.Name.Equals(staff.UserRoles.FirstOrDefault().Role.Name) 
                    && x.Status
                    && x.OfficeId == staff.OfficeId).ToList();

                if (users.Count > 0)
                {
                    var EmailList = users.Select(x => x.Email);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = EmailList, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"Unable to retrieve record from UserMaster table"}{"-"}{DateTime.Now}", true, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "UserMaster list was empty", StatusCode = ResponseCodes.RecordNotFound };
                }

            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> AllApplications()
        {
            try
            {
                var apps = _unitOfWork.Application.Find(x => x.IsLegacy.ToLower().Equals("no"), "Facility.LGA.State,Phase.Category,Applicationforms,ApplicationType")
                        .Select(x => new AppRespnseDto
                        {
                            Id = x.Id,
                            ApplicationType = x.ApplicationType.Name,
                            CategoryCode = x.Phase.Category.Code,
                            CategoryId = x.Phase.CategoryId,
                            LgaId = x.Facility.LgaId,
                            Location = x.Facility.Address,
                            PhaseId = x.PhaseId,
                            Applicationforms = _mapper.Map<List<ApplicationFormDto>>(x.Applicationforms)
                        }).ToList(); ;
                if (apps.Count > 0)
                {
                    _generalLogger.LogRequest($"{"All Applications was successfully fetched"}{"-"}{DateTime.Now}", false, directory);
                    webApiResponse = new WebApiResponse
                    {
                        ResponseCode = AppResponseCodes.Success,
                        Message = "Successful",
                        StatusCode = ResponseCodes.Success,
                        Data = apps
                    };
                }
                else
                {
                    _generalLogger.LogRequest($"{"All Applications-- No Record Found"}{"-"}{DateTime.Now}", false, directory);
                    webApiResponse = new WebApiResponse
                    {
                        ResponseCode = AppResponseCodes.RecordNotFound,
                        Message = "All Applications-- No Record Found",
                        StatusCode = ResponseCodes.RecordNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                webApiResponse = new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
            return webApiResponse;
        }

        public async Task<WebApiResponse> ApplicationHistory(int id)
        {
            try
            {
                var actionHistories = _unitOfWork.Application.Find(x => x.Id == id, "AppHistories").ToList();

                if (actionHistories.Count > 0)
                {
                    _generalLogger.LogRequest($"{"Application History was successfully fetched"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = actionHistories, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"Application History-- No Record Found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No Record Found", StatusCode = ResponseCodes.RecordNotFound };
                }
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }
        }

        public async Task<WebApiResponse> ViewApplication(int id)
        {
            try
            {
                var application = _unitOfWork.Application.Find(x => x.Id == id, "Payments,Facility.LGA.State,ExtraPayments,FlowStage,AppHistories,User.Company").FirstOrDefault();
                var appInfo = new ViewApplicationResponseDto 
                {
                    Remarks = application.AppHistories.LastOrDefault().Message,
                    ApplicationRecievedCurrentDesk = application.CurrentUser,
                    ApplicationRecievedDate = application.ModifiedDate,
                    ApplicationRecievedOffice = "",
                    ApplicationRecievedFrom = application.AppHistories.LastOrDefault().TriggeredBy,
                    ApplicationStatus = application.Status,
                    DatePaymentRecieved = application.Payments.FirstOrDefault(x => x.Status.Equals("AUTH")).TransactionDate,
                    PaymentStatus = application.Payments.FirstOrDefault(x => x.Status.Equals("AUTH")).Status == GeneralClass.SuccessfulPaymentStatus ? GeneralClass.PaymentCompletedStatus : GeneralClass.PaymentPendingStatus,
                    RemitaRef = application.Payments.FirstOrDefault(x => x.Status.Equals("AUTH")).Rrreference,
                    Arrears = application.Payments.FirstOrDefault(x => x.Status.Equals("AUTH")).Arrears,
                    ServiceCharge = application.Payments.FirstOrDefault(x => x.Status.Equals("AUTH")).ServiceCharge,
                    ProcessingFee = application.Payments.FirstOrDefault(x => x.Status.Equals("AUTH")).ProcessingFee,
                    TotalAmountPaid = application.Payments.FirstOrDefault(x => x.Status.Equals("AUTH")).TxnAmount,
                    IsLegacy = application.IsLegacy,
                    CurrentSatge = application.FlowStage.Name,
                    ApplicationType = application.ApplicationType.Name,
                    PermitName = application.Phase.Description,
                    CompanyName = application.User.Company.Name
                };


                if (appInfo != null)
                {
                    _generalLogger.LogRequest($"{"View Application was successfully fetched"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = application, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"Application History-- No Record Found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No Record Found", StatusCode = ResponseCodes.RecordNotFound };
                }
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> SubmittedDocument(int id)
        {
            try
            {
                var submittedDocs = (from s in _unitOfWork.SubmittedDocument.GetAll()
                                     where s.ApplicationId == id
                                     select new SubmittedDocumentResponseDto
                                     {
                                         DocumentName = s.DocSource,
                                         DocumentSource = s.DocSource
                                     }).ToList();
                if (submittedDocs.Count > 0)
                {
                    _generalLogger.LogRequest($"{"Submitted Documents was successfully fetched"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = submittedDocs, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"Submitted Documents-- No Record Found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No Record Found", StatusCode = ResponseCodes.RecordNotFound };
                }

            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }

        }

        public async Task<WebApiResponse> AllRegisteredCompany()
        {
            try
            {
                var registeredcompany = _userManager.Users
                    .Include(x => x.Company)
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .Where(x => x.UserRoles.FirstOrDefault().Role.Name.Equals("Company"))
                    .Select(u => new AllRegisteredCompanyResponseDto
                    {
                        CompanyElpsId = u.ElpsId,
                        CompanyEmail = u.Email,
                        CompanyName = u.Company.Name,
                        CompanyRegistrationNumber = u.Company.CacNumber
                    }).ToList();
                if (registeredcompany.Count > 0)
                {
                    _generalLogger.LogRequest($"{"All registered companies was successfully fetched"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = registeredcompany, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"All registered companies-- No Record Found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No Record Found", StatusCode = ResponseCodes.RecordNotFound };
                }
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> StaffDashboard()
        {
            try
            {
                //var location = (from l in _context.UserMaster where l.UserId == _generalClass.getSessionEmail() select l.UserLocation).FirstOrDefault();
                var InProcessingForThreeWeeks = _unitOfWork.Application.Find(x => x.Status != GeneralClass.PaymentPendingStatus && !x.Status.Equals(GeneralClass.WorkflowStateComplete) && x.AddedDate.Value <= DateTime.Now.AddDays(-21).Date).ToList();
                var OnStaffDeskForFiveDays = _unitOfWork.Application.Find(x => x.ModifiedDate <= DateTime.Now.AddDays(-5).Date && x.CurrentUser != x.User.Email).ToList();
                var requestmodel_1 = _mapper.Map<List<AppRequestViewModel>>(InProcessingForThreeWeeks);
                var requestmodel_2 = _mapper.Map<List<AppRequestViewModel>>(OnStaffDeskForFiveDays);

                DashboardModel dashboardModel = new DashboardModel()
                {
                    DeskCount = _unitOfWork.Application.Find(x => x.CurrentUser == _generalClass.getSessionEmail()).Count(),
                    ApprovedCount = _unitOfWork.Application.Find(x => x.Status == GeneralClass.Approved).Count(),
                    RejectedCount = _unitOfWork.Application.Find(x => x.Status == GeneralClass.Rejected).Count(),
                    ProcessingCount = _unitOfWork.Application.Find(x => x.Status == GeneralClass.Processing).Count(),
                    InProcessingForThreeWeeks = requestmodel_1,
                    OnStaffDeskForFiveDays = requestmodel_2
                };

                _generalLogger.LogRequest($"{"Staff dashboard was successfully fetched"}{"-"}{DateTime.Now}", false, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = dashboardModel, StatusCode = ResponseCodes.Success };

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Internal server error occurred while trying to fetch staff dashboard"}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }


        }

    }
}
