using AUS2.Core.DAL.Repository.Services.Application;
using AUS2.Core.Helper.Notification;
using AUS2.Core.Repository.Services.Account;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AUS2.Controllers
{
    [Authorize]
    [Route("api/aus/application")]
    [ApiController]
    public class ApplicationController : BaseController
    {
        private readonly ApplicationService _applicationServiceRepository;
        private readonly ApplicationWorkflowService _applicationWorkflowService;

        public ApplicationController(INotification notification, ApplicationService applicationServiceRepository, ApplicationWorkflowService applicationWorkflowService) : base(notification)

        {
            _applicationServiceRepository = applicationServiceRepository;
            _applicationWorkflowService = applicationWorkflowService;
        }

        /// <summary>
        /// This endpoint fetches all pending applications on current logged-in staff desk
        /// </summary>
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("applications-on-my-desk")]
        public async Task<IActionResult> MyDesk() => Response(await _applicationServiceRepository.MyDesk().ConfigureAwait(false));


        /// <summary>
        /// This endpoint fetches all pending applications count on current logged-in staff desk
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("applications-on-my-desk-count")]
        public async Task<IActionResult> MyDeskCount() => Response(await _applicationServiceRepository.MyDeskCount().ConfigureAwait(false));


        /// <summary>
        /// This endpoint fetches all applications count, it accepts a "type" parameter (Approved, Rejected, Payment Pending) and returns
        /// all applications count if parameter is not specified.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("applications-count")]
        public async Task<IActionResult> ApplicationsCount(string type) => Response(await _applicationServiceRepository.ApplicationsCount(type).ConfigureAwait(false));


        /// <summary>
        /// This endpoint fetches all permits count.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("approved-permits-count")]
        public async Task<IActionResult> PermitsCount() => Response(await _applicationServiceRepository.PermitsCount().ConfigureAwait(false));



        /// <summary>
        /// This endpoint displays list of applications for a particular company
        /// </summary>
        ///        
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("company-applications")]
        public async Task<IActionResult> CompanyApplication(string email) => Response(await _applicationServiceRepository.CompanyApplications(email).ConfigureAwait(false));

        /// <summary>
        /// This endpoint displays all the permits of a particular company
        /// </summary>
        ///        
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("company-permits")]
        public async Task<IActionResult> CompanyPermits(string email) => Response(await _applicationServiceRepository.CompanyPermits(email).ConfigureAwait(false));

        /// <summary>
        /// This endpoint is used to apply for new permits on the portal
        /// </summary>
        ///        
        /// <returns>Returns a success or a failure message.</returns>
        /// <param name="model">Object for applying for new application which contains a doc field for the uploaded excel</param>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpPost]
        [Route("application-form")]
        public async Task<IActionResult> ApplicationForm([FromForm]AppRequestViewModel model) => Response(await _applicationServiceRepository.ApplicationForm(model).ConfigureAwait(false));

        /// <summary>
        /// This endpoint is majorly used to update state and location of an existing application
        /// </summary>
        ///        
        /// <param name="model">This parameter to used to update state and location of an existing application</param>
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpPut]
        [Route("edit-bulk-application")]
        public async Task<IActionResult> EditBulkApplication([FromBody] AppRequestViewModel model) => Response(await _applicationServiceRepository.EditBulkApplication(model).ConfigureAwait(false));

        /// <summary>
        /// This endpoint displays all state list
        /// </summary>
        ///        
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("state-list")]
        public async Task<IActionResult> StateList() => Response(await _applicationServiceRepository.StateList().ConfigureAwait(false));

        /// <summary>
        /// This endpoint displays all LGA list
        /// </summary>
        ///        
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("lga-list")]
        public async Task<IActionResult> LGAList() => Response(await _applicationServiceRepository.LGAList().ConfigureAwait(false));

        /// <summary>
        /// This endpoint displays all LGA list by passing Statecode parameter
        /// </summary>
        ///        
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("lga-list-by-Id")]
        public async Task<IActionResult> LGAById(int id) => Response(await _applicationServiceRepository.LGAById(id).ConfigureAwait(false));

        /// <summary>
        /// This endpoint displays all application categories
        /// </summary>
        ///        
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("application-category")]
        public async Task<IActionResult> ApplicationCategory() => Response(await _applicationServiceRepository.ApplicationModules().ConfigureAwait(false));

        /// <summary>
        /// This endpoint displays all application modules
        /// </summary>
        ///        
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("application-phases")]
        public async Task<IActionResult> CategoryPhases() => Response(await _applicationServiceRepository.CategoryPhases().ConfigureAwait(false));



        /// <summary>
        /// This endpoint displays application categories of a particular module by passing the module Id
        /// </summary>
        ///        
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("application-phases-by-categoryId")]
        public async Task<IActionResult> ApplicationPhasesByCategoryId(int moduleid) => Response(await _applicationServiceRepository.ApplicationPhasesByModuleId(moduleid).ConfigureAwait(false));

        /// <summary>
        /// This endpoint is get the full details of an application
        /// </summary>
        ///        
        /// <param name="id">The id of the application to be returned</param>
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("get-app-details-by-id")]
        public async Task<IActionResult> GetApplicationDetailsById(int id) => Response(await _applicationServiceRepository.GetAppDetailsById(id).ConfigureAwait(false));


        /// <summary>
        /// This endpoint is majorly used to pass or reject application to the next processing staff
        /// </summary>
        ///        
        /// <param name="model">This parameter is used to pass or reject application to the next processing staff</param>
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpPut]
        [Route("process-application")]
        public async Task<IActionResult> ProcessApplication([FromBody] ProcessApplicationRequestDto model) => Response(await _applicationWorkflowService.Processapplication(model).ConfigureAwait(false));
    }
}