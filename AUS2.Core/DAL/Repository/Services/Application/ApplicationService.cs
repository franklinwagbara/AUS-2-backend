using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Data;
using ExcelDataReader;
using AUS2.Core.DAL.IRepository;
using AUS2.Core.DBObjects;
using Microsoft.AspNetCore.Identity;
using AUS2.Core.Helper.SerilogService.Account;
using AUS2.Core.ViewModels.Dto.Response;
using AUS2.Core.Helper;
using AUS2.Core.ViewModels;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.Helper.Helpers.ResponseCodes;
using AUS2.Core.ViewModels.AdminModel;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace AUS2.Core.Repository.Services.Account
{
    public class ApplicationService
    {
        private readonly ApplicationContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GeneralLogger _generalLogger;
        WebApiResponse webApiResponse = new WebApiResponse();
        GeneralClass _generalClass;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string directory = "Application";
        private readonly IElpsService _elpsServiceHelper;
        public ApplicationService(
            IOptions<AppSettings> appSettings,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            ApplicationContext context, 
            IElpsService elpsServiceHelper, 
            GeneralLogger generalLogger, 
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)

        {
            _context = context;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _elpsServiceHelper = elpsServiceHelper;
            _generalLogger = generalLogger;
            _httpContextAccessor = httpContextAccessor;
            _generalClass = new GeneralClass(_httpContextAccessor, appSettings);
            _mapper = mapper;
        }

        public async Task<WebApiResponse> MyDesk()
        {
            try
            {
                WebApiResponse myDesk = await GetStaffDesk(false);
                return myDesk;
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> MyDeskCount()
        {
            try
            {
                WebApiResponse myDeskCount = await GetStaffDesk(true);
                return myDeskCount;

            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> GetStaffDesk(bool condition)
        {
            try
            {
                //var loginuser = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                //var userMaster = _context.UserMaster.Where(x => x.UserId == loginuser).FirstOrDefault();
                var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email));

                if (user != null)
                {
                    //var mydesk = (from u in _context.ApplicationRequest where u.LastAssignedUser == userMaster.UserId select u).ToList();
                    var mydesk = _unitOfWork.Application.Find(x => x.CurrentUser.Equals(user.Email));

                    if (condition == true)
                    {
                        _generalLogger.LogRequest($"{"Fetching all applications on staff desk: "}{user.FirstName} {user.LastName}{" by"}{user.Email} {" - "}{DateTime.Now}", false, directory);
                        return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = mydesk.Count(), StatusCode = ResponseCodes.Success };
                    }
                    else
                    {
                        _generalLogger.LogRequest($"{"Getting count of processing applications on staff desk: "}{user.FirstName} {user.LastName}{" by"}{user.Email} {" - "}{DateTime.Now}", false, directory);
                        return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = mydesk, StatusCode = ResponseCodes.Success };
                    }
                }
                else
                {
                    return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "An error occured while trying to get staff details. ", StatusCode = ResponseCodes.InternalError };
                }
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> ApplicationsCount(string type = null)
        {
            try
            {
                int applicationsCount = 0;

                if (type != null)
                    applicationsCount = _unitOfWork.Application.Find(x => x.Status.Equals(type)).Count();
                else
                    applicationsCount = _unitOfWork.Application.GetAll().Count();

                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = type + " Applications Count Success", Data = applicationsCount, StatusCode = ResponseCodes.Success };
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> PermitsCount()
        {
            try
            {
                var permitsCount = _unitOfWork.Permit.GetAll().Count();
                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Success", Data = permitsCount, StatusCode = ResponseCodes.Success };
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }
        }

        public async Task<WebApiResponse> CompanyApplications(string email)
        {
            try
            {
                var apps = _unitOfWork.Application
                    .Find(x => x.User.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    .Select(a => new CompanyApplicationResponseDto
                    {
                        Id = a.Id,
                        Reference = a.Reference,
                        ApplicationStatus = a.Status,
                        ApplicationType = a.ApplicationType.Name,
                        AppliedDate = a.AddedDate,
                        CompanyName = a.User.Company.Name,
                        Description = a.Phase.Description
                    }).ToList();

                if (apps.Count > 0)
                {
                    _generalLogger.LogRequest($"{"All company applications was successfully fetched"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = apps, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"All company applications-- No Record Found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No Record Found", StatusCode = ResponseCodes.RecordNotFound };
                }
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }
        }

        public async Task<WebApiResponse> CompanyPermits(string email)
        {
            try
            {
                var companypermitList = _unitOfWork.Permit.Find(x => x.Application.User.Email.Equals(email), "Application.User.Company.Name,Application.Phase")
                                         .Select(a => new CompanyPermitResponseDto
                                         {
                                             Id = a.ApplicationId,
                                             Reference = a.Application.Reference,
                                             ApplicationStatus = a.Application.Status,
                                             ApplicationType = a.Application.ApplicationType.Name,
                                             AppliedDate = a.Application.AddedDate,
                                             CompanyName = a.Application.User.Company.Name,
                                             IssuedDate = a.IssuedDate,
                                             ExpiryDate = a.ExpireDate,
                                             Description = a.Application.Phase.Description
                                         }).ToList();
                if (companypermitList.Count > 0)
                {
                    _generalLogger.LogRequest($"{"Company permit was successfully fetched"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = companypermitList, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"Company permit-- No Record Found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No Record Found", StatusCode = ResponseCodes.RecordNotFound };
                }
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal server error occurred " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> ApplicationForm(AppRequestViewModel model)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = _userManager.Users.Include("Company").FirstOrDefault(x => x.Email.ToLower().Equals(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email).ToLower()));
                    string fileExtension = Path.GetExtension(model.doc.FileName);
                    if (model.doc == null || model.doc.Length == 0)
                    {
                        _generalLogger.LogRequest($"{"No content for Excel upload"}{"-"}{DateTime.Now}", false, directory);
                        webApiResponse = new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No content for Excel upload", StatusCode = ResponseCodes.RecordNotFound };
                    }
                    else if (fileExtension != ".csv" && fileExtension != ".xlsx" && fileExtension != ".xls")
                    {
                        _generalLogger.LogRequest($"{"Invalid Excel format"}{"-"}{DateTime.Now}", false, directory);
                        webApiResponse = new WebApiResponse { ResponseCode = AppResponseCodes.InvalidCSV_ExcelFormat, Message = "Invalid Excel format", StatusCode = ResponseCodes.Badrequest };
                    }
                    else
                    {
                        var app = new Application();
                        var phase = _unitOfWork.Phase.Find(x => x.Id == model.PhaseId).FirstOrDefault();
                        var forms = ReadExcelFile(model.doc, model.CategoryId);
                        if (forms != null && forms.Count > 0)
                        {
                            var fac = await CreateFacility(user, model, phase);
                            app = new Application
                            {
                                AddedDate = DateTime.Now,
                                ApplicationTypeId = model.ApplicationTypeId,
                                FacilityId = fac.Id,
                                IsLegacy = "No",
                                ModifiedDate = DateTime.Now,
                                PhaseId = model.PhaseId,
                                Reference = _generalClass.GenerateApplicationNo(),
                                Status = GeneralClass.Initiated,
                                UserId = user.Id,
                                CurrentUser = user.Email,
                            };

                            _unitOfWork.ApplicationForm.AddRange(forms);
                            await _unitOfWork.SaveChangesAsync(user.Id);

                            _unitOfWork.Application.Add(app);
                            await _unitOfWork.SaveChangesAsync(user.Id);
                        }
                       
                        await transaction.CommitAsync();
                        _generalLogger.LogRequest($"{"Excel upload--Both application form and facility record was successfully saved"}{"-"}{DateTime.Now}", false, directory);
                        var application = _unitOfWork.Application.Find(x => x.Id == app.Id, "Phase.Category,Applicationforms").FirstOrDefault();
                        var form = _mapper.Map<AppRespnseDto>(application);
                        webApiResponse = new WebApiResponse
                        {
                            ResponseCode = AppResponseCodes.Success,
                            Message = "Successful",
                            StatusCode = ResponseCodes.Success,
                            Data = form
                        };
                    }

                }
                catch (Exception ex)
                {
                    _generalLogger.LogRequest($"{"Excel upload--Internel server error occurred"}{"-"}{ex}{"-"}{DateTime.Now}", false, directory);
                    await transaction.RollbackAsync();
                    webApiResponse = new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = ex.ToString(), StatusCode = ResponseCodes.InternalError };
                }
                return webApiResponse;
            }
        }

        public async Task<WebApiResponse> EditBulkApplication(AppRequestViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email));
                if (model != null)
                {
                    var app = _unitOfWork.Application.Find(x => x.Id == model.Id, "ApplicationForms,Facility.LGA.State").FirstOrDefault();
                    if (app != null)
                    {
                        _unitOfWork.ApplicationForm.RemoveRange(app.Applicationforms);

                        var appforms = ReadExcelFile(model.doc, model.CategoryId);
                        appforms.ForEach(x => x.ApplicationId = model.Id);
                        _unitOfWork.ApplicationForm.AddRange(appforms);


                        app.Facility.LgaId = model.LgaId;
                        app.PhaseId = model.PhaseId;
                        app.ModifiedDate = DateTime.Now;
                        app.ApplicationTypeId = model.ApplicationTypeId;
                        _unitOfWork.Application.Update(app);

                        await _unitOfWork.SaveChangesAsync(user.Id);
                    }
                    _generalLogger.LogRequest($"{"EditBulkApplication--Application records was successfully updated"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"EditBulkApplication--No record was passed"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No content was passed", StatusCode = ResponseCodes.RecordNotFound };
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"EditBulkApplication--Internel server error occurred"}{"-"}{ex}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }

        }

        public async Task<WebApiResponse> StateList()
        {
            try
            {
                var statelist = _unitOfWork.State.GetAll().Select(s => new { s.Id, s.StateCode, s.StateName }).ToList();
                if (statelist.Count > 0)
                {
                    _generalLogger.LogRequest($"{"StateList--Successfully retrieved state list"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = statelist, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"StateList--No record found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No record found", StatusCode = ResponseCodes.RecordNotFound };
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"StateList--Internel server error occurred"}{"-"}{ex}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }
        }

        public async Task<WebApiResponse> LGAList()
        {
            try
            {
                var lgalist = _unitOfWork.LGA.GetAll("State").Select(s => new { s.Id, s.LgaCode, s.LgaName }).ToList();
                if (lgalist.Count > 0)
                {
                    _generalLogger.LogRequest($"{"LGAList--Successfully retrieved LGA list"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = lgalist, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"LGAList--No record found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No record found", StatusCode = ResponseCodes.RecordNotFound };
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"LGAList--Internel server error occurred"}{"-"}{ex}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> LGAById(int id)
        {
            try
            {
                var lgalistbyId = _unitOfWork.LGA.Find(x => x.StateId == id, "State").Select(s => new { s.Id, s.LgaCode, s.LgaName }).ToList();
                if (lgalistbyId.Count > 0)
                {
                    _generalLogger.LogRequest($"{"LGAListById--Successfully retrieved LGA list by Id"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = lgalistbyId, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"LGAListById--No record found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No record found", StatusCode = ResponseCodes.RecordNotFound };
                }

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"LGAListById--Internel server error occurred"}{"-"}{ex}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> CategoryPhases()
        {
            try
            {
                var appcat = _unitOfWork.Phase.GetAll("Category").Select(x => new { x.Id, x.Description, x.Code, CategoryName = x.Category.Name })?.ToList();
                if (appcat.Count > 0)
                {
                    _generalLogger.LogRequest($"{"ApplicationCategory--Successfully retrieved application category"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = appcat, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"ApplicationCategory--No record found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No record found", StatusCode = ResponseCodes.RecordNotFound };
                }

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"ApplicationCategory--Internel server error occurred"}{"-"}{ex}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> ApplicationModules()
        {
            try
            {
                var applicationmodule = _unitOfWork.Category.GetAll().Select(s => new { s.Id, Name = s.Name, Description = s.Description})?.ToList();
                if (applicationmodule.Count > 0)
                {
                    _generalLogger.LogRequest($"{"ApplicationModules--Successfully retrieved application modules"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = applicationmodule, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"ApplicationModules--No record found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No record found", StatusCode = ResponseCodes.RecordNotFound };
                }

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"ApplicationModules--Internel server error occurred"}{"-"}{ex}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> ApplicationPhasesByModuleId(int id)
        {
            try
            {
                var appcatbymoduleid = _unitOfWork.Phase.Find(s => s.CategoryId == id, "Category").Select(s => new { s.Id, s.Code, s.ShortName, s.Description }).ToList();
                if (appcatbymoduleid.Count > 0)
                {
                    _generalLogger.LogRequest($"{"ApplicationCategory--Successfully retrieved application category"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = appcatbymoduleid, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"ApplicationCategory--No record found"}{"-"}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "No record found", StatusCode = ResponseCodes.RecordNotFound };
                }

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"ApplicationCategory--Internel server error occurred"}{"-"}{ex}{"-"}{DateTime.Now}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public Task<WebApiResponse> ProcessApplication(ProcessApplicationRequestDto model)
        {
            throw new NotImplementedException();
        }

        private async Task<Facility> CreateFacility(ApplicationUser user, AppRequestViewModel model, Phase phase)
        {
            var fac = _unitOfWork.Facility.Find(x => x.Address.ToLower().Equals(model.Location.ToLower()) && x.CompanyId == user.CompanyId).FirstOrDefault();
            if (fac == null)
            {
                fac = new Facility
                {
                    Address = model.Location,
                    CompanyId = user.CompanyId.Value,
                    Cordinates = "",
                    LgaId = model.LgaId
                };
                var lga = _unitOfWork.LGA.Find(x => x.Id == model.LgaId).FirstOrDefault();
                var elpsfac = _elpsServiceHelper.PostFacility(new Facilities
                {
                    City = lga.LgaName,
                    CompanyId = user.ElpsId,
                    DateAdded = DateTime.Now,
                    Name = user.Company.Name,
                    FacilityType = $"AUS II - {phase.Description}",
                    LGAId = model.LgaId,
                    StateId = lga.StateId,
                    StreetAddress = model.Location
                });

                if (elpsfac.message.Equals("success", StringComparison.OrdinalIgnoreCase))
                {
                    var res = (Facilities)elpsfac.value;
                    fac.ElpsId = res.Id;
                    _unitOfWork.Facility.Add(fac);
                    await _unitOfWork.SaveChangesAsync(user.Email);
                }
            }
            return fac;
        }

        private List<ApplicationForm> ReadExcelFile(IFormFile file, int catId)
        {
            var appForm = new List<ApplicationForm>();
            DataSet dsexcelRecords = new DataSet();
            IExcelDataReader reader = null;
            Stream FileStream = null;
            FileStream = file.OpenReadStream();
            string fileExtension = Path.GetExtension(file.FileName);

            if (fileExtension == ".xls")
                reader = ExcelReaderFactory.CreateBinaryReader(FileStream);

            if (fileExtension == ".xlsx" || fileExtension == ".csv")
                reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);

            dsexcelRecords = reader.AsDataSet();
            reader.Close();

            if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
            {
                DataTable dt = dsexcelRecords.Tables[0];

                var category = _unitOfWork.Category.Find(x => x.Id == catId).FirstOrDefault();
                switch (category.Code)
                {
                    case "WLN":
                        for (int i = 2; i < dt.Rows.Count; i++)
                        {
                            appForm.Add(new ApplicationForm
                            {
                                WellLocationCategory = dt.Rows[i][0].ToString(),
                                Field = dt.Rows[i][1].ToString(),
                                Block = dt.Rows[i][2].ToString(),
                                Terrain = dt.Rows[i][3].ToString(),
                                WellPreSpudName = dt.Rows[i][4].ToString(),
                                SpudDate = Convert.ToDateTime(dt.Rows[i][5]),
                                WellSpudName = dt.Rows[i][6].ToString(),
                                WellClassApplied = dt.Rows[i][7].ToString(),
                                WellSurfaceCoordinates = dt.Rows[i][8].ToString(),
                                ProposedRig = dt.Rows[i][9].ToString(),
                                ExpectedVolumes = dt.Rows[i][10].ToString(),
                                TargetReserves = dt.Rows[i][11].ToString(),
                                Afe = dt.Rows[i][12].ToString(),
                                EstimatedOperationsDays = int.Parse(dt.Rows[i][13].ToString())
                            });
                        }
                        break;
                    case "WRP-A":
                        for (int i = 2; i < dt.Rows.Count; i++)
                        {
                            appForm.Add(new ApplicationForm
                            {
                                WellLocationCategory = dt.Rows[i][0].ToString(),
                                WellName = dt.Rows[i][1].ToString(),
                                NatureOfOperation = dt.Rows[i][2].ToString(),
                                PlugbackInterval = dt.Rows[i][3].ToString(),
                                RigForOperation = dt.Rows[i][4].ToString(),
                                LastProductionRate = dt.Rows[i][5].ToString(),
                                InitialReservesAllocationOfWell = dt.Rows[i][6].ToString(),
                                CumulativeProductionForWell = dt.Rows[i][7].ToString(),
                                Afe = dt.Rows[i][8].ToString(),
                                EstimatedOperationsDays = int.Parse(dt.Rows[i][9].ToString())
                            });
                        }
                        break;
                    case "WRP-B":
                        for (int i = 2; i < dt.Rows.Count; i++)
                        {
                            appForm.Add(new ApplicationForm
                            {
                                WellLocationCategory = dt.Rows[i][0].ToString(),
                                WellName = dt.Rows[i][1].ToString(),
                                NatureOfOperation = dt.Rows[i][2].ToString(),
                                WellCompletionInterval = dt.Rows[i][3].ToString(),
                                RigForOperation = dt.Rows[i][4].ToString(),
                                PreOperationProductionRate = dt.Rows[i][5].ToString(),
                                PostOperationProductionRate1 = dt.Rows[i][6].ToString(),
                                InitialReservesAllocationOfWell = dt.Rows[i][7].ToString(),
                                CumulativeProductionForWell = dt.Rows[i][8].ToString(),
                                TargetReserves = dt.Rows[i][9].ToString(),
                                Afe = dt.Rows[i][10].ToString(),
                                EstimatedOperationsDays = int.Parse(dt.Rows[i][11].ToString())
                            });
                        }
                        break;
                    default:
                        break;
                }
            }
            return appForm;
        }
    }
}
