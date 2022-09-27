
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AUS2.Core.DBObjects;
using AUS2.Core.Helper.SerilogService.Account;
using AUS2.Core.ViewModels;
using AUS2.Core.Helper;
using AUS2.Core.DAL.IRepository;
using AUS2.Core.ViewModels.Dto.Response;
using AUS2.Core.Helper.Helpers.ResponseCodes;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.AdminModel;
using Microsoft.AspNetCore.Identity;
using AUS2.Core.ViewModels.CompanyModel;

namespace AUS2.AUS2.Core.DAL.Repository.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly GeneralLogger _generalLogger;
        private readonly AppSettings _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        GeneralClass _generalClass;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IElpsService _elps;
        private readonly string directory = "Configuration";
        WebApiResponse result = new WebApiResponse();

        public ConfigurationService(
            IOptions<AppSettings> appSettings, 
            ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IUnitOfWork unitOfWork,
            GeneralLogger generalLogger,
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor, 
            IElpsService elps)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _generalLogger = generalLogger;
            _httpContextAccessor = httpContextAccessor;
            _elps = elps;
            _generalClass = new GeneralClass(_httpContextAccessor, appSettings);

        }

        private string userEmail => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        //private string userEmail => "testDO@mailinator.com";


        public async Task<WebApiResponse> Get_PermitConfiguration()
        {
            try
            {
                var applicationModules = _unitOfWork.Category.GetAll()
                                          .Select(a => new ApplicationModuleRequestDto
                                          {
                                              ModuleId = a.Id,
                                              Name = a.Name,
                                              Code = a.Code
                                          }).ToList();
                var allPermits = _unitOfWork.Phase.GetAll("Category")
                                  .Select(p => new LicenseTypeRequestDTO
                                  {
                                      ShortName = p.ShortName,
                                      Sort = p.Sort,
                                      CategoryId = p.CategoryId,
                                      LicenseSerial = p.LicenseSerial,
                                      Code = p.Code,
                                      Description = p.Description,
                                      Status = p.Status,
                                      CategoryName = p.Category.Name,
                                      Fee = p.Fee,
                                      ServiceCharge = p.ServiceCharge,
                                  }).ToList();

                var permitViewModel = new PermitViewModel()
                {
                    AllModules = applicationModules,
                    AllPermits = allPermits
                };

                if (applicationModules.Count > 0)
                {
                    _generalLogger.LogRequest($"{"Getting license/permit configuration view: "}{" by"}{_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)} {" - "}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = permitViewModel, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    _generalLogger.LogRequest($"{"Application modules record was not found: "}{" by"}{_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)} {" - "}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "Failure", StatusCode = ResponseCodes.RecordNotFound };
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"Staff Configurations desk count error:: {ex.Message.ToString()} by {_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> Post_PermitConfiguration(LicenseTypeRequestDTO model)
        {
            var currentUser = await _userManager.FindByEmailAsync(userEmail.ToLower());
            try
            {
                var requestmodel = _mapper.Map<Phase>(model);
                
                var checkLicense = _unitOfWork.Phase.Find(x => x.Code.Equals(model.Code.Trim()) || x.ShortName.Equals(model.ShortName)).FirstOrDefault();
                if (checkLicense != null)
                {
                    string errorMessage = "Permit type code/short name/sequence number matches an already existing permit configuration detail(s)";
                    _generalLogger.LogRequest($"${errorMessage} for {model.ShortName} by {userEmail}", true, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.DuplicateRecord, Message = errorMessage, StatusCode = ResponseCodes.Duplicate };
                }
                else
                {
                    requestmodel.Category = null;
                    requestmodel.Status = true;
                    _unitOfWork.Phase.Add(requestmodel);
                    await _unitOfWork.SaveChangesAsync(userEmail.Trim());
                    var applicationModule = _unitOfWork.Category.Find(x => x.Id == model.CategoryId).FirstOrDefault();

                    var allPermits = _unitOfWork.Phase.Find(x => x.CategoryId == model.CategoryId, "Category")
                                      .Select(p => new LicenseTypeRequestDTO
                                      {
                                          ShortName = p.ShortName,
                                          Sort = p.Sort,
                                          LicenseSerial = p.LicenseSerial,
                                          Code = p.Code,
                                          Description = p.Description,
                                          Status = p.Status,
                                          CategoryName = p.Category.Name,
                                          Fee = p.Fee,
                                          ServiceCharge = p.ServiceCharge,
                                      }).ToList();

                    var applicationModules = _unitOfWork.Category.GetAll()
                                              .Select(a => new ApplicationModuleRequestDto
                                              {
                                                  ModuleId = a.Id,
                                                  Name = a.Name,
                                                  Code = a.Code
                                              }).ToList();

                    var permitViewModel = new PermitViewModel()
                    {
                        AllModules = applicationModules,
                        AllPermits = allPermits
                    };
                    string response = $"{"Permit configuration for: "}{requestmodel.ShortName}{" for "}{applicationModule?.Name}{" was created successfully by"}{userEmail}";
                    _generalLogger.LogRequest($"{response} {" - "}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = permitViewModel, StatusCode = ResponseCodes.Success };

                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"Permit configuration error:: {ex.Message.ToString()} by {userEmail}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> Delete_PermitConfiguration(int licenseTypeID)
        {
            try
            {
                var permitConfigDetails = _unitOfWork.Phase.Find(x => x.Id == licenseTypeID).FirstOrDefault();
                if (permitConfigDetails == null)
                {
                    _generalLogger.LogRequest($"Permit configuration details for {licenseTypeID} was not found by {userEmail}", true, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = $"Permit configuration details for {licenseTypeID} was not found.", StatusCode = ResponseCodes.RecordNotFound };
                }
                else
                {
                    string permiName = permitConfigDetails.ShortName;
                    _unitOfWork.Phase.Remove(permitConfigDetails);
                    await _unitOfWork.SaveChangesAsync(userEmail);

                    #region ReturnData Section
                    var applicationModules = _unitOfWork.Category.GetAll()
                                              .Select(a => new ApplicationModuleRequestDto
                                              {
                                                  ModuleId = a.Id,
                                                  Name = a.Name
                                              }).ToList();

                    var allPermits = _unitOfWork.Phase.GetAll("Category")
                                      .Select(p => new LicenseTypeRequestDTO
                                      {
                                          ShortName = p.ShortName,
                                          Sort = p.Sort,
                                          LicenseSerial = p.LicenseSerial,
                                          Code = p.Code,
                                          Description = p.Description,
                                          Status = p.Status,
                                          ModuleName = p.Category.Name
                                      }).ToList();

                    var permitViewModel = new PermitViewModel()
                    {
                        AllModules = applicationModules,
                        AllPermits = allPermits
                    };
                    #endregion
                    _generalLogger.LogRequest($"Deleting permit configuration for {permiName} by {userEmail} {" - "}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Deletion of permit configuration was successful", Data = permitViewModel, StatusCode = ResponseCodes.Success };

                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"Delete permit configuration error:: {ex.Message.ToString()} by {userEmail}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> Get_ApplicationConfiguration(int appliationId)
        {
            try
            {
                var appDetails = _unitOfWork.Application.Find(x => x.Id == appliationId).FirstOrDefault();
                if (appDetails == null)
                {
                    _generalLogger.LogRequest($"Application details for {appliationId} was not found by {userEmail}", true, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = $"Application details for {appliationId} was not found.", StatusCode = ResponseCodes.RecordNotFound };
                }
                else
                {
                    _generalLogger.LogRequest($"Fetching application configuration view for {appliationId} by {userEmail} {" - "}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = appDetails, StatusCode = ResponseCodes.Success };

                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"Application configuration view error:: {ex.Message.ToString()} by {userEmail}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> Edit_ApplicationConfiguration(AppRequestViewModel model)
        {
            try
            {
                var editapplication = _unitOfWork.Application.Find(x => x.Id == model.Id).FirstOrDefault();
                
                
                if (editapplication == null)
                {
                    _generalLogger.LogRequest($"Application details was not found {model.Id} by {userEmail}", true, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = "Application details was not found.", StatusCode = ResponseCodes.RecordNotFound };
                }
                else
                {
                    var application = _mapper.Map<Application>(editapplication);
                    application.ModifiedDate = DateTime.Now;
                    _unitOfWork.Application.Update(application);
                    await _unitOfWork.SaveChangesAsync(userEmail);

                    string response = $"{$"Application details was modified successfully by ID: {model.Id}"}{userEmail}";
                    _generalLogger.LogRequest($"{response} {" - "}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = "", StatusCode = ResponseCodes.Success };

                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"Application modification error:: {ex.Message.ToString()} by {userEmail}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> Get_ModuleConfiguration()
        {
            try
            {
                var applicationModules = _unitOfWork.Category.GetAll().ToList();
                _generalLogger.LogRequest($"{"Getting application modules configuration view: "}{" by"}{_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)} {" - "}{DateTime.Now}", false, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = applicationModules, StatusCode = ResponseCodes.Success };

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"Staff Configurations desk count error:: {ex.Message.ToString()} by {userEmail}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> Post_ModuleConfiguration(ApplicationModulesRequestDTO model)
        {
            try
            {
                var category = _mapper.Map<Category>(model);
                var checkModule = _unitOfWork.Category.Find(x => x.Name.ToLower().Equals(model.Name.ToLower())).FirstOrDefault();
                if (checkModule != null)
                {
                    string errorMessage = $"Application module is already existing for {model.Name}.";
                    _generalLogger.LogRequest($"{errorMessage}{" by"}{userEmail} {" - "}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.DuplicateRecord, Message = errorMessage, StatusCode = ResponseCodes.Duplicate };
                }
                else
                {
                    _unitOfWork.Category.Add(category);
                    await _unitOfWork.SaveChangesAsync(userEmail);

                    var appModules = _unitOfWork.Category.GetAll().ToList();

                    _generalLogger.LogRequest($"{$"Creating new application module for: {model.Name}"}{" by"}{userEmail} {" - "}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "New application module was created successfully", Data = appModules, StatusCode = ResponseCodes.Success };

                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"Application module configuration error:: {ex.Message.ToString()} by {userEmail}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> Delete_ModuleConfiguration(int moduleID)
        {
            try
            {
                var moduleDetails = _unitOfWork.Category.Find(x => x.Id == moduleID).FirstOrDefault();
                if (moduleDetails == null)
                {
                    _generalLogger.LogRequest($"Module configuration details not found for {moduleID} by {userEmail}", true, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.RecordNotFound, Message = $"Module details for {moduleID} was not found.", StatusCode = ResponseCodes.RecordNotFound };
                }
                else
                {
                    string moduleName = moduleDetails.Name;
                    _unitOfWork.Category.Remove(moduleDetails);
                    await _unitOfWork.SaveChangesAsync(userEmail);

                    _generalLogger.LogRequest($"Deleting module configuration for {moduleName} by {userEmail} {" - "}{DateTime.Now}", false, directory);

                    var applicationModules = _unitOfWork.Category.GetAll().ToList();
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Deletion of module configuration was successful", Data = applicationModules, StatusCode = ResponseCodes.Success };

                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"Delete module configuration error:: {ex.Message.ToString()} by {userEmail}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> GetAllDocs()
        {
            try
            {
                var compdocs = (List<DocumentType>)_elps.GetAllDocs("").Data;
                var facdos = (List<DocumentType>)_elps.GetAllDocs("facility").Data;

                compdocs.AddRange(facdos);

                var docs = compdocs.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type
                });
                var modules = _unitOfWork.Category.GetAll().ToList();
                var submodules = _unitOfWork.Phase.GetAll("Category").ToList();
                var apptypes = await _context.ApplicationTypes.Select(x => new { x.Id, x.Name }).ToListAsync();


                _generalLogger.LogRequest($"Returning all company and facility documents {" - "}{DateTime.Now}", false, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "SUccessful", Data = new { apptypes, modules, submodules, docs }, StatusCode = ResponseCodes.Success };

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"An error occured:: {ex.Message.ToString()} by {_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> UpdatePermitDocs(LicenseTypeDocsViewModel model)
        {
            try
            {
                var doc = _unitOfWork.PhaseDocument
                    .Find(x => x.DocId == model.DocId && x.ApptypeId == model.AppTypeId && x.PhaseId == model.LicenseTypeId, "Phase,ApplicationType")
                    .FirstOrDefault();
                //

                if (doc == null)
                {
                    doc = _mapper.Map<PhaseDocument>(model);
                    _unitOfWork.PhaseDocument.Add(doc);
                }
                else
                {
                    doc.DocType = model.DocType;
                    doc.Status = model.Status;
                    doc.IsMandatory = model.IsMandatory;

                    _unitOfWork.PhaseDocument.Update(doc);
                }
                await _unitOfWork.SaveChangesAsync(userEmail);

                _generalLogger.LogRequest($"Permit type document was updated successfully {" - "}{DateTime.Now}", false, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = "Successful", StatusCode = ResponseCodes.Success };

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"An error occured:: {ex.Message.ToString()} by {_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> AddRole(RoleViewModel model)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(model.Name);

                if (role == null)
                {
                    role = _mapper.Map<ApplicationRole>(model);
                    await _roleManager.CreateAsync(role);
                }
                else
                {
                    role.Name = model.Name;
                    role.Description = model.Description;

                    await _roleManager.UpdateAsync(role);
                }
                _generalLogger.LogRequest($"Role was updated successfully {" - "}{DateTime.Now}", false, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = _roleManager.Roles.ToList().Select(x => new { x.Name, x.Description }).ToList(), StatusCode = ResponseCodes.Success };

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"An error occured:: {ex.Message.ToString()} by {_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> GetOffices()
        {
            try
            {
                var offices = _unitOfWork.FieldOffice.GetAll("State").Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Address,
                    x.State.StateName,
                    x.Status
                });
                _generalLogger.LogRequest($"{"Getting application modules configuration view: "}{" by"}{_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)} {" - "}{DateTime.Now}", false, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = offices, StatusCode = ResponseCodes.Success };

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"Staff Configurations desk count error:: {ex.Message.ToString()} by {userEmail}", true, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> AddFieldOffice(FieldOfficeDto model)
        {
            try
            {
                var office = _unitOfWork.FieldOffice.Find(x => x.Name.ToLower().Equals(model.Name.ToLower()) && x.StateId == model.StateId).FirstOrDefault();
                if(office == null)
                {
                    office = _mapper.Map<FieldOffice>(model);
                    office.Status = true;
                    office.AddedDate = DateTime.UtcNow.AddHours(1);
                    _unitOfWork.FieldOffice.Add(office);
                    await _unitOfWork.SaveChangesAsync(userEmail);

                    _generalLogger.LogRequest($"{"New field office added successfully "}{" by"}{_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)} {" - "}{DateTime.Now}", false, directory);
                    result = new WebApiResponse
                    {
                        ResponseCode = AppResponseCodes.Success,
                        Data = _mapper.Map<List<FieldOfficeDto>>(_unitOfWork.FieldOffice.GetAll("State").ToList()),
                        Message = "Successful",
                        StatusCode = ResponseCodes.Success
                    };
                }
                else
                {
                    _generalLogger.LogRequest($"{"Field office already exist"}", false, directory);
                    result = new WebApiResponse
                    {
                        ResponseCode = AppResponseCodes.DuplicateRecord,
                        Message = "This office already exists",
                        StatusCode = ResponseCodes.Duplicate
                    };
                }
                
            }
            catch(Exception ex)
            {
                result = new WebApiResponse
                {
                    ResponseCode = AppResponseCodes.InternalError,
                    Message = $"Internal error occured {ex.Message.ToString()} \n {ex.InnerException.ToString()}",
                    StatusCode = ResponseCodes.InternalError
                };
            }
            return result;

        }
    }
}
