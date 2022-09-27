using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using AUS2.Core.DBObjects;
using AUS2.Core.DAL.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AUS2.Core.Helper.SerilogService.Account;
using AUS2.Core.ViewModels.Dto.Response;
using AUS2.Core.Helper;
using static AUS2.Core.Helper.GeneralClass;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using AUS2.Core.ViewModels.CompanyModel;
using AUS2.Core.ViewModels;
using AUS2.Core.Helper.Helpers.ResponseCodes;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.Account;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace AUS2.Core.DAL.Repository.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IElpsService _elpsServiceHelper;
        private readonly GeneralLogger _accountLogger;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        WebApiResponse webApiResponse = new WebApiResponse();
        GeneralClass _generalClass;
        Authentications auth = new Authentications();
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IHttpContextAccessor _httpContextAccessor;
        IConfiguration _configuration;
        private readonly string directory = "Account";

        public AccountService(
            IOptions<AppSettings> appSettings,
            ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signManager,
            IElpsService elpsServiceHelper, 
            GeneralLogger accountLogger,
            IMapper mapper, 
            IHostingEnvironment hostingEnv, 
            IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signManager;
            _elpsServiceHelper = elpsServiceHelper;
            _accountLogger = accountLogger;
            _mapper = mapper;
            _hostingEnv = hostingEnv;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _generalClass = new GeneralClass(_httpContextAccessor, appSettings);
        }

        public async Task<WebApiResponse> ValidateLogin(string email, string code)
        {
            var user = new ApplicationUser();
            try
            {
                webApiResponse = _elpsServiceHelper.GetCompanyDetailByEmail(email);
                if (webApiResponse.Message == "SUCCESS")
                {
                    var companyDetail = (CompanyDetail)webApiResponse.Data;
                    user = _userManager.Users
                        .Include(c => c.Company)
                        .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                        .FirstOrDefault(x => x.ElpsId == companyDetail.id && x.Status);
                    if(user == null)
                        user = new ApplicationUser
                        {
                            Email = email,
                            UserName = email,
                            FirstName = companyDetail.contact_FirstName,
                            PhoneNumber = companyDetail.contact_Phone,
                            LastName = companyDetail.contact_LastName,
                            UserType = "Company",
                            ElpsId = companyDetail.id,
                            CreatedBy = "System",
                            CreatedOn = DateTime.UtcNow,
                            Status = true,
                            LastLogin = DateTime.UtcNow,
                            LoginCount = 0,
                            Company = new Core.DBObjects.Company
                            {
                                Name = companyDetail.name,
                                CacNumber = companyDetail.rC_Number,
                                YearIncorporated = companyDetail.year_Incorporated,
                                TIN = companyDetail.tin_Number,
                                RegisteredAddressId = string.IsNullOrEmpty(companyDetail.registered_Address_Id) ? 0 : int.Parse(companyDetail.registered_Address_Id),
                            }
                        };
                    else
                    {
                        if (!user.Email.ToLower().Equals(email.ToLower()))
                        {
                            user.Email = email;
                            user.UserName = email;
                        }
                        user.FirstName = companyDetail.contact_FirstName;
                        user.PhoneNumber = companyDetail.contact_Phone;
                        user.LastName = companyDetail.contact_LastName;
                        user.Company.Name = companyDetail.name;
                        user.Company.CacNumber = companyDetail.rC_Number;
                        user.Company.YearIncorporated = companyDetail.year_Incorporated;
                        user.Company.TIN = companyDetail.tin_Number;
                        user.Browser = auth.BrowserName(_httpContextAccessor.HttpContext);
                        user.LoginCount += 1;

                        await _userManager.UpdateAsync(user);
                    }
                }
                else
                {
                    webApiResponse = _elpsServiceHelper.GetStaff(email);
                    if(webApiResponse.Message == "SUCCESS")
                    {
                        var staff = (StaffResponseDto)webApiResponse.Data;
                        user = _userManager.Users
                            .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                            .FirstOrDefault(x => x.ElpsId == staff.id && x.Status);

                        if(user == null)
                            user = new ApplicationUser
                            {
                                Email = email,
                                UserName = email,
                                FirstName = staff.firstName,
                                PhoneNumber = staff.phoneNo.ToString(),
                                LastName = staff.lastName,
                                UserType = "Staff",
                                ElpsId = staff.id,
                                CreatedBy = "System",
                                CreatedOn = DateTime.UtcNow,
                                Status = true,
                                LastLogin = DateTime.UtcNow,
                                LoginCount = 0
                            };
                        else
                        {
                            if (!user.Email.ToLower().Equals(email.ToLower()))
                            {
                                user.Email = email;
                                user.UserName = email;
                            }
                            user.FirstName = staff.firstName;
                            user.PhoneNumber = staff?.phoneNo?.ToString();
                            user.LastName = staff.lastName;
                            user.Browser = auth.BrowserName(_httpContextAccessor.HttpContext);
                            user.LoginCount += 1;

                            await _userManager.UpdateAsync(user);
                        }
                    }
                }

                if (webApiResponse.Message.ToLower().Equals("success"))
                {
                    if(user.LoginCount == 0)
                    {
                        var result = await _userManager.CreateAsync(user);
                        if (result.Succeeded && user.UserType.ToLower().Equals("staff"))
                            await _userManager.AddToRoleAsync(user, "Staff");
                        else if (result.Succeeded && user.UserType.ToLower().Equals("company"))
                            await _userManager.AddToRoleAsync(user, "Company");
                    }

                    _accountLogger.LogRequest($"{"User Information with the"}{" "}{email}{"was successfully added/updated - "}{DateTime.Now}", false, directory);
                    _httpContextAccessor.HttpContext.Session.SetString(_appSettings.sessionEmail, email);//save email sessess of loggedin user
                    await _signInManager.SignInAsync(user, false);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Email, email),
                            new Claim(ClaimTypes.Name, user.UserType == GeneralClass.Company?user.FirstName: user.FirstName+" "+ user.LastName),
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return new WebApiResponse
                    {
                        ResponseCode = AppResponseCodes.Success,
                        Message = "Successful",
                        Data = new LoginResponseDto
                        {
                            UserId = user.Email,
                            UserType = user.UserType,
                            ElpsId = user.ElpsId,
                            CaCNumber = user?.Company?.CacNumber,
                            FirstName = user.UserType.Equals("Staff") ? user.FirstName : user.Company.Name,
                            LastName = user.LastName,
                            UserRoles = user.UserRoles.FirstOrDefault(x => !x.Role.Name.Equals("Staff"))?.Role.Name,
                            CreatedBy = user.CreatedBy,
                            CreatedOn = user.CreatedOn,
                            Status = user.Status,
                            LastLogin = user.LastLogin,
                            LoginCount = user.LoginCount,
                            Token = tokenHandler.WriteToken(token)
                        },
                        StatusCode = ResponseCodes.Success
                    };
                }
                _accountLogger.LogRequest($"{"Unable to fetch user details from Elps"}{" "}{email}{" - "}{DateTime.Now}", true, directory);

                return new WebApiResponse { ResponseCode = AppResponseCodes.Failed, Message = "Unable to fetch user details from Nelps with the email " + email, StatusCode = ResponseCodes.RecordNotFound };
            }
            catch (Exception ex)
            {
                _accountLogger.LogRequest($"{"Internal server error occured why trying to login user"}{" "}{email}{" - "}{DateTime.Now}", true, directory);

                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> GetAllStaff()
        {
            try
            {
                webApiResponse = _elpsServiceHelper.GetAllStaff();
                if (webApiResponse.Message == "SUCCESS")
                {
                    var staffDetail = (List<StaffResponseDto>)webApiResponse.Data;
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = staffDetail, StatusCode = ResponseCodes.Success };
                }
                else
                {
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Failed, Message = "Unable to retrieve staff details ", StatusCode = ResponseCodes.RecordNotFound };

                }
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> AddNewStaff(UserMasterRequestDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                    return new WebApiResponse { ResponseCode = AppResponseCodes.UserAlreadyExist, Message = "Already exist", StatusCode = ResponseCodes.Duplicate };

                user = _mapper.Map<ApplicationUser>(model);

                if (model.SignatureImage != null)
                {
                    var newfilename = Path.Combine(new Guid().ToString(), Path.GetExtension(model.SignatureImage.FileName));
                    var filePath = Path.Combine(_hostingEnv.WebRootPath, "UploadedFiles", newfilename);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                        await model.SignatureImage.CopyToAsync(fileSteam);

                    user.Signature = "~/UploadedFiles/" + newfilename;
                }

                user.UpdatedBy = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                user.CreatedBy = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                user.LoginCount = 1;
                user.Status = true;
                user.ElpsId = model.ElpsId;
                user.CreatedOn = DateTime.Now;
                user.UpdatedOn = DateTime.Now;
                user.LastLogin = DateTime.Now;
                user.NormalizedEmail = model.Email;

                await _userManager.CreateAsync(user);
                await _userManager.AddToRolesAsync(user, new[] { "Staff", model.UserRole });

                var allusers = _userManager.Users
                    .Include("Company")
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.Office)
                    .Where(x => !x.UserRoles.FirstOrDefault().Role.Name.ToLower().Equals("company")).Select(y => new
                    {
                        phoneNo = y.PhoneNumber,
                        userId = y.Email,
                        y.FirstName,
                        y.LastName,
                        role = y.UserRoles.FirstOrDefault(x => !x.Role.Name.Equals("Staff")).Role.Name,
                        y.Status,
                        Office = y.Office.Name
                    }).ToList();

                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = allusers, StatusCode = ResponseCodes.Success };
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }

        }

        public async Task<WebApiResponse> EditStaff(UserMasterRequestDto model)
        {
            try
            {
                //var requestmodel = _mapper.Map<ApplicationUser>(model);
                var user = _userManager.Users
                        .Include(c => c.Company)
                        .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                        .FirstOrDefault(x => x.Email.Equals(model.Email) && x.Status);

                if (model.SignatureImage != null)
                {
                    string getfilename = ContentDispositionHeaderValue.Parse(model.SignatureImage.ContentDisposition).FileName.Trim('"');

                    var removefileName = Path.GetFileName(getfilename);
                    var fileExt = Path.GetExtension(model.SignatureImage.FileName);
                    var removefilePath = Path.Combine(_hostingEnv.WebRootPath, "UploadedFiles", removefileName);
                    if (File.Exists(user.Signature))//if signature image file already exist in folder path, delete it
                    {
                        // If file found, delete it 
                        File.Delete(user.Signature);
                    }

                    var fileName = Path.GetFileName(model.SignatureImage.FileName);
                    var newfilename = Path.Combine(new Guid().ToString(), fileExt);

                    var filePath = Path.Combine(_hostingEnv.WebRootPath, "UploadedFiles", newfilename);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))//add signature image to path
                    {
                        await model.SignatureImage.CopyToAsync(fileSteam);
                    }
                    user.Signature = "~/UploadedFiles/" + newfilename;
                }
                user.BranchId = model.BranchId;
                user.UpdatedBy = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                user.UpdatedOn = DateTime.Now;
                user.Status = bool.Parse(model.Status);
                await _userManager.UpdateAsync(user);

                return new WebApiResponse 
                { 
                    ResponseCode = AppResponseCodes.Success,
                    Message = "Successful", 
                    Data = new LoginResponseDto
                    {
                        UserId = user.Email,
                        UserType = user.UserType,
                        ElpsId = user.ElpsId,
                        CaCNumber = user?.Company.CacNumber,
                        FirstName = user.UserType.Equals("Staff") ? user.FirstName : user.Company.Name,
                        LastName = user.LastName,
                        UserRoles = user.UserRoles.LastOrDefault().Role.Name,
                        CreatedBy = user.CreatedBy,
                        CreatedOn = user.CreatedOn,
                        Status = user.Status,
                        LastLogin = user.LastLogin,
                        LoginCount = user.LoginCount,
                    }, 
                    StatusCode = ResponseCodes.Success 
                };
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> ChangePassword(PasswordModel model)
        {
            if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.OldPassword) && !string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
            {
                try
                {
                    var response = await Task.FromResult(_elpsServiceHelper.UpdatePassword(model));
                    return new WebApiResponse
                    {
                        ResponseCode = response.Message.ToLower().Equals("success") ? AppResponseCodes.Success : AppResponseCodes.Failed,
                        Message = response.Message.ToLower().Equals("success") ? "Successful" : "Password update failed",
                        Data = (object)response.Data,
                        StatusCode = response.Message.ToLower().Equals("success") ? ResponseCodes.Success : ResponseCodes.InternalError
                    };
                }
                catch (Exception ex)
                {
                    return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "ChangePassword--Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

                }
            }
            return new WebApiResponse { ResponseCode = AppResponseCodes.Failed, Message = "Model is Invalid", StatusCode = ResponseCodes.Badrequest };

        }

        public async Task<WebApiResponse> DeleteStaff(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if(user != null)
                {
                    await _userManager.DeleteAsync(user);
                    _accountLogger.LogRequest($"{"Staff was successfully deleted => " + email}{" - "}{DateTime.Now}", false, directory);
                    return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", StatusCode = ResponseCodes.Success };
                }
                return new WebApiResponse { ResponseCode = AppResponseCodes.UserNotFound, Message = "DeleteStaff--Invalid user ", StatusCode = ResponseCodes.RecordNotFound };
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "DeleteStaff--Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };
            }
        }

        public async Task<WebApiResponse> GetAllUsers()
        {
            try
            {
                var staffDetail = _userManager.Users
                    .Include("Company")
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .Include(x => x.Office)
                    .Where(x => !x.UserRoles.FirstOrDefault().Role.Name.ToLower().Equals("company")).Select(y => new
                    {
                        phoneNo = y.PhoneNumber,
                        userId = y.Email,
                        y.FirstName,
                        y.LastName,
                        role = y.UserRoles.FirstOrDefault(x => !x.Role.Name.Equals("Staff")).Role.Name,
                        y.Status,
                        Office = y.Office.Name
                        }).ToList();
                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = staffDetail, StatusCode = ResponseCodes.Success };
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

        public async Task<WebApiResponse> GetAllRoles()
        {
            try
            {
                var staffDetail = await _roleManager.Roles.Where(x => !x.Name.ToLower().Equals("superadmin") && !x.Name.ToLower().Equals("company")).ToListAsync();
                return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "Successful", Data = staffDetail, StatusCode = ResponseCodes.Success };
            }
            catch (Exception ex)
            {
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }

        }

    }


}
