using AUS2.Core.DAL.IRepository;
using AUS2.Core.DBObjects;
using AUS2.Core.Helper;
using AUS2.Core.Helper.Helpers.ResponseCodes;
using AUS2.Core.Helper.SerilogService.Account;
using AUS2.Core.ViewModels;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.Dto.Response;
using AUS2.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace AUS2.Core.DAL.Repository.Services.Application
{
    public class ApplicationWorkflowService
    {
        private readonly ApplicationContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        GeneralClass _generalClass;
        private readonly GeneralLogger _generalLogger;
        private readonly AppSettings _appSettings;
        private readonly IElpsService _elpsServiceHelper;
        private readonly string directory = "Application";

        public ApplicationWorkflowService(
            IOptions<AppSettings> appSettings, 
            GeneralLogger adminLogger,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IElpsService elpsServiceHelper, 
            ApplicationContext context, 
            IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _generalLogger = adminLogger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _generalClass = new GeneralClass(_httpContextAccessor, appSettings);
        }

        public async Task<WebApiResponse> Processapplication(ProcessApplicationRequestDto model)
        {
            _generalLogger.LogRequest($"{"Processapplication-- "} {"- "}{DateTime.Now}", false, directory);
            return new WebApiResponse { ResponseCode = AppResponseCodes.Success, Message = "", StatusCode = ResponseCodes.Success };

        }

        public string GetNextProcessingStaff(string targetRole)
        {
            return "";
        }
    }
}
