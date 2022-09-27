using AUS2.Core.DAL.IRepository;
using AUS2.Core.DBObjects;
using AUS2.Core.Helper.Helpers.ResponseCodes;
using AUS2.Core.Helper.SerilogService.Account;
using AUS2.Core.ViewModels.CompanyModel;
using AUS2.Core.ViewModels.Dto.Response;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AUS2.Core.DAL.Repository.Services.Company
{
    public class CompanyService
    {
        private readonly IElpsService _elpsServiceHelper;
        private readonly ApplicationContext _context;
        private readonly IUnitOfWork _unitOfWork;   
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GeneralLogger _generalLogRequest;
        private readonly string directory = "Company";

        public CompanyService(
            IElpsService elpsServiceHelper,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            GeneralLogger LogRequest,
            ApplicationContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _elpsServiceHelper = elpsServiceHelper;
            _generalLogRequest = LogRequest;
        }

        public async Task<WebApiResponse> GetCompanyDetails(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {

                try
                {
                    var nations = new List<SelectListItem>();
                    var user = await _userManager.FindByEmailAsync(email.Trim().ToLower());

                    _generalLogRequest.LogRequest($"About to get company's details by email on Elpss-{DateTime.Now}", true, directory);
                    //_accountLogRequest.LogRequest($"{"Done with UserMaster Information"}{" "}{model.Email}{" - "}{DateTime.Now}", false);

                    var company = (CompanyDetail)_elpsServiceHelper.GetCompanyDetailByEmail(email)?.Data;
                    _generalLogRequest.LogRequest($"Company's details by email retuned successfully-{DateTime.Now}", true, directory);

                    var countries = await Task.FromResult(_context.Nationalities.ToList());

                    _generalLogRequest.LogRequest($"About to get company's address by id on Elpss - {DateTime.Now}", true, directory);
                    var companyAdd = (RegisteredAddress)_elpsServiceHelper.GetCompanyAddressById(int.Parse(company.registered_Address_Id)).Data;
                    _generalLogRequest.LogRequest($"Company's address retuned successfully-{DateTime.Now}", true, directory);

                    foreach (var country in countries)
                    {
                        if (country.CountryName == company.nationality)
                        {
                            nations.Add(new SelectListItem
                            {
                                Text = country.Id.ToString(),
                                Value = country.CountryName,
                                Selected = true
                            });
                        }
                        else
                        {
                            nations.Add(new SelectListItem
                            {
                                Text = country.Id.ToString(),
                                Value = country.CountryName
                            });
                        }
                    }
                    return new WebApiResponse
                    {
                        ResponseCode = AppResponseCodes.Success,
                        Message = "Successful",
                        Data = new
                        {
                            Company = company,
                            RegisteredAddress = companyAdd,
                            Nationality = nations
                        },
                        StatusCode = ResponseCodes.Success
                    };
                }
                catch (Exception ex)
                {
                    //_accountLogRequest.LogRequest($"{"Unable to get company information"}{" "}{email}{" - "}{DateTime.Now}", true);
                    _generalLogRequest.LogRequest($"An exception error occured- {ex.Message} ::: {DateTime.Now}", true, directory);

                    return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

                }
            }
            return new WebApiResponse { ResponseCode = AppResponseCodes.UserNotFound, Message = "Company email is not valid", StatusCode = ResponseCodes.RecordNotFound };

        }

        public async Task<WebApiResponse> UpdateCompanyDetails(CompanyInformation model)
        {
            try
            {
                var response = new List<RegisteredAddress>();
                var user = await _userManager.FindByEmailAsync(model.oldemail.Trim().ToLower());
                model.Company.id = user.ElpsId;

                if (model.Company != null)
                {
                    _generalLogRequest.LogRequest($"About to update conmany's information on Elpss{" - "}{DateTime.Now}", false, directory);

                    model.Company = (CompanyModel)_elpsServiceHelper.UpdateCompanyDetails(model.Company, model.oldemail.Trim().ToLower(), false)?.Data;

                }

                if (model.RegisteredAddress != null)
                {
                    model.RegisteredAddress.country_Id = 156;
                    model.RegisteredAddress.type = "Registered";
                    model.RegisteredAddress.stateId = 2412;
                    var addList = new List<RegisteredAddress>();
                    addList.Add(model.RegisteredAddress);

                    _generalLogRequest.LogRequest($"About to update company's address on Elpss{" - "}{DateTime.Now}", false, directory);

                    if (!string.IsNullOrEmpty(user.Company.RegisteredAddress))
                    {
                        addList.Add(model.RegisteredAddress);
                        response = (List<RegisteredAddress>)_elpsServiceHelper.UpdateCompanyRegAddress(addList)?.Data;
                    }
                    else
                        response = (List<RegisteredAddress>)_elpsServiceHelper.AddCompanyRegAddress(addList, user.ElpsId)?.Data;

                    user.Company.RegisteredAddress = model.RegisteredAddress.address_1;
                   await _userManager.UpdateAsync(user);
                }
                return new WebApiResponse
                {
                    ResponseCode = AppResponseCodes.Success,
                    Message = "Successful",
                    Data = model,
                    StatusCode = ResponseCodes.Success
                };
            }
            catch (Exception ex)
            {
                _generalLogRequest.LogRequest($"An error occured on updating company's address on Elpss{" - "}{DateTime.Now}", false, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
        }

        public async Task<WebApiResponse> GetCompanyDocumentByEmail(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var company = await Task.FromResult(await _userManager.FindByEmailAsync(email.Trim().ToLower()));
                    if (company != null)
                    {
                        _generalLogRequest.LogRequest($"About to get documents for company with email => {email}{" - "}{DateTime.Now}", false, directory);
                        var docs = _elpsServiceHelper.GetCompanyDocumentsById(company.ElpsId);
                        if (docs != null && docs.Message.ToLower().Equals("success"))
                            return new WebApiResponse
                            {
                                ResponseCode = AppResponseCodes.Success,
                                Message = "Successful",
                                Data = (object)docs.Data,
                                StatusCode = ResponseCodes.Success
                            };
                    }
                }
            }
            catch (Exception ex)
            {
                _generalLogRequest.LogRequest($"An error occured while getting documents for company with email => {email}{" - "}{DateTime.Now}", false, directory);
                return new WebApiResponse { ResponseCode = AppResponseCodes.InternalError, Message = "Internal error occured " + ex.ToString(), StatusCode = ResponseCodes.InternalError };

            }
            return new WebApiResponse
            {
                ResponseCode = AppResponseCodes.Failed,
                Message = "An error occured, please try again",
                StatusCode = ResponseCodes.Badrequest
            };
        }

        public async Task<WebApiResponse> GetPermits(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var company = await _userManager.FindByEmailAsync(email.Trim().ToLower());
                if (company == null)
                    return new WebApiResponse
                    {
                        ResponseCode = AppResponseCodes.RecordNotFound,
                        Message = "Company not found",
                        StatusCode = ResponseCodes.RecordNotFound
                    };

                var apps = _unitOfWork.Permit.Find(x => x.Application.User.Email.Equals(email)).ToList();

                return new WebApiResponse
                {
                    ResponseCode = apps.Count() > 0 ? AppResponseCodes.Success : AppResponseCodes.Failed,
                    Data = apps,
                    Message = apps.Count() > 0 ? "Successful" : "There are no approved permits for the company",
                    StatusCode = apps.Count() > 0 ? ResponseCodes.Success : ResponseCodes.RecordNotFound
                };
            }
            return new WebApiResponse
            {
                ResponseCode = AppResponseCodes.Failed,
                Message = "Company email cannot be null/empty",
                StatusCode = ResponseCodes.Badrequest
            };
        }

        public async Task<WebApiResponse> ViewPermit(string email, string licenseno)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(licenseno))
            {
                var company = await _userManager.FindByEmailAsync(email.Trim().ToLower());
                if (company == null)
                    return new WebApiResponse
                    {
                        ResponseCode = AppResponseCodes.RecordNotFound,
                        Message = "Company not found",
                        StatusCode = ResponseCodes.RecordNotFound
                    };

                var permit = _unitOfWork.Permit.Find(x => x.PermiTNo.Equals(licenseno) && x.Application.User.Email.Equals(email), "Application.User,Application.Facility.LGA.State,Phase").FirstOrDefault();
                
                return new WebApiResponse
                {
                    ResponseCode = permit != null ? AppResponseCodes.Success : AppResponseCodes.Failed,
                    Data = permit != null ? new
                    {
                        CompanyName = $"{company.Company.Name}",
                        CompanyAddress = company.Company.RegisteredAddress,
                        FacilityName = $"{permit.Application.Phase.Code}-{permit.Application.Facility.ElpsId}",
                        FacilityAddress = permit.Application.Facility.Address,
                        FacilityState = permit.Application.Facility.LGA.State.StateName,
                        permit?.IssuedDate,
                        permit?.ExpireDate,
                        PermitNumber = permit.PermiTNo,
                    } : null,
                    Message = permit != null ? "Successful" : "Permit not found",
                    StatusCode = permit != null ? ResponseCodes.Success : ResponseCodes.RecordNotFound
                };
            }
            return new WebApiResponse
            {
                ResponseCode = AppResponseCodes.Failed,
                Message = "Company email/Permit Number cannot be null/empty",
                StatusCode = ResponseCodes.Badrequest
            };
        }

        public async Task<WebApiResponse> MyApplications(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var company = await _userManager.FindByEmailAsync(email.Trim().ToLower());
                if (company == null)
                    return new WebApiResponse
                    {
                        ResponseCode = AppResponseCodes.RecordNotFound,
                        Message = "Company not found",
                        StatusCode = ResponseCodes.RecordNotFound
                    };

                var apps = _unitOfWork.Application.Find(x => x.User.Email.Equals(email), "Facility.LGA.State,User.Coompny,Phase,Payments,ApplicationType").Select(x =>
               new {
                   x.AddedDate,
                   CompanyName = x.User.Company.Name,
                   FacilityName = $"{x.Phase.Code}-{x.Facility.ElpsId}",
                   FacilityAddress = x.Facility.Address,
                   GpsCordinates = "",
                   x.Facility.LGA.State.StateName,
                   RRR = x.Payments.FirstOrDefault().Rrreference,
                   AppReference = x.Reference,
                   x.Status,
                   x.ApplicationType,
                   x.Phase.Code,
               }).ToList();

                return new WebApiResponse
                {
                    ResponseCode = apps.Count() > 0 ? AppResponseCodes.Success : AppResponseCodes.Failed,
                    Data = apps,
                    Message = apps.Count() > 0 ? "Successful" : "No applications for the company",
                    StatusCode = apps.Count() > 0 ? ResponseCodes.Success : ResponseCodes.RecordNotFound
                };
            }
            return new WebApiResponse
            {
                ResponseCode = AppResponseCodes.Failed,
                Message = "Company email cannot be null/empty",
                StatusCode = ResponseCodes.Badrequest
            };
        }
    }
}
