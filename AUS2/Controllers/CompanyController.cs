using AUS2.Core.DAL.Repository.Services.Company;
using AUS2.Core.Helper.Notification;
using AUS2.Core.ViewModels.CompanyModel;
using AUS2.Core.ViewModels.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AUS2.Controllers
{
    [Authorize]
    [Route("api/aus/company")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly CompanyService _companyService;
        public CompanyController(INotification notification, CompanyService companyService) : base(notification)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// This endpoint returns an object containing the company's details and address using the company's email
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("get-profile")]
        public async Task<IActionResult> Profile(string email = null) => Response(await _companyService.GetCompanyDetails(email));


        /// <summary>
        /// This endpoint returns the model for viewing a permit/approval using the company's emails and approval/license no
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpPost]
        [Route("profile")]
        public async Task<IActionResult> Profile(CompanyInformation model) => Response(await _companyService.UpdateCompanyDetails(model));

        /// <summary>
        /// This endpoint returns all uploaded documents using the company's email
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("document-library")]
        public async Task<IActionResult> DocumentLibrary(string email) => Response(await _companyService.GetCompanyDocumentByEmail(email));


        /// <summary>
        /// This endpoint returns all permits/approvals or licensesusing the company's email
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("get-permits")]
        public async Task<IActionResult> GetPermits(string email) => Response(await _companyService.GetPermits(email).ConfigureAwait(false));


        /// <summary>
        /// This endpoint returns the model for viewing a permit/approval using the company's email and approval/license no
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("view-permit")]
        public async Task<IActionResult> ViewPermit(string email, string permitno) => Response(await _companyService.ViewPermit(email, permitno).ConfigureAwait(false));


        /// <summary>
        /// This endpoint fetches all applications using a reh=gistered company's email
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("my-apps")]
        public async Task<IActionResult> MyApplications(string email) => Response(await _companyService.MyApplications(email).ConfigureAwait(false));
    }
}
