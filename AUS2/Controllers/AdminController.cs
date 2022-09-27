using AUS2.Core.DAL.IRepository;
using AUS2.Core.Helper.Notification;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AUS2.Controllers
{
    [Authorize]
    [Route("api/aus/admin")]
    [ApiController]
    public class AdminController : BaseController
    {
       private readonly IAdminService _adminService;
        public AdminController(INotification notification, IAdminService adminService) : base(notification)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// This endpoint is use to fetch staff dashboard
        /// </summary>
        ///        
        /// <returns>Returns a success or a failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Success response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("staff-dashboard")]
        public async Task<IActionResult> StaffDashboard() => Response(await _adminService.StaffDashboard().ConfigureAwait(false));

        /// <summary>
        /// This endpoint is use to add staff going on leave or vacation so that application doesn't get to them while on leave
        /// </summary>
        ///        
        /// <param name="model">This parameter registers staff going on leave</param>
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
        [HttpPost]
        [Route("add-out-of-office")]
        public async Task<IActionResult> AddOutofOffice([FromBody] OutOfOfficeRequestDto model) => Response(await _adminService.AddOutOfOffice(model).ConfigureAwait(false));
        //{
            //if (model is null)
            //{
            //    return NoContent();
            //}
            //else
            //{
            //    var result = await _adminService.AddOutOfOffice(model);
            //    if (result.StatusCode == 201)
            //        return Created("", result);
            //    if (result.StatusCode == 404)
            //        return NotFound();
            //    if (result.StatusCode == 500)
            //        return Forbid(); 
            //    return BadRequest();
            //}
        //}

        /// <summary>
        /// This endpoint is use to remove staff from out of office list
        /// </summary>
        ///        
        /// <param name="model">This parameter registers staff going on leave</param>
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

        [HttpDelete]
        [Route("delete-out-of-office")]
        public async Task<IActionResult> DeleteOutofOffice([FromBody] Delete_EditOutOfOfficeRequestDto model) => Response(await _adminService.DeleteOutOfOffice(model).ConfigureAwait(false));



        /// <summary>
        /// This endpoint is use to fetch list of staff out of office
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
        [Route("get-all-staff-out-of-office")]
        public async Task<IActionResult> AllStaffOutOfOffice() => Response(await _adminService.GetAllStaffOutOfOffice().ConfigureAwait(false));


        /// <summary>
        /// This endpoint is use to fetch relieved staff
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
        [Route("get-relieved-staff-out-of-office")]
        public async Task<IActionResult> RelievedStaffOutOfOffice() => Response(await _adminService.GetRelievedStaffOutOfOffice().ConfigureAwait(false));


        /// <summary>
        /// This endpoint is use to end staff leave
        /// </summary>
        ///        
        /// <param name="model">This parameter ends staff leave</param>
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
        [Route("end-staff-leave")]
        public async Task<IActionResult> EndStaffLeave([FromBody] Delete_EditOutOfOfficeRequestDto model) => Response(await _adminService.EndLeave(model).ConfigureAwait(false));

        /// <summary>
        /// This endpoint is use to edit staff leave
        /// </summary>
        ///        
        /// <param name="model">This parameter edits staff leave</param>
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
        [Route("edit-staff-out-of-office")]
        public async Task<IActionResult> EditOutOfOffice([FromBody] OutOfOfficeRequestDto model) => Response(await _adminService.EditOutOfOffice(model).ConfigureAwait(false));


        /// <summary>
        /// This endpoint gets the list of applications on staff desk
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
        [Route("get-staff-desk")]
        public async Task<IActionResult> StaffDesk() => Response(await _adminService.StaffDesk().ConfigureAwait(false));


        /// <summary>
        /// This endpoint is use to transfer application from one staff desk to another of same role and location
        /// </summary>
        ///        
        /// <param name="model">This parameter transfers application to a different staff of same role and location</param>
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
        [Route("reroute-application")]
        public async Task<IActionResult> RerouteApplication([FromBody] List<RerouteApplicationRequestDto> model) => Response(await _adminService.RerouteApplication(model).ConfigureAwait(false));


        /// <summary>
        /// This endpoint displays emails of staffs of which the application is be transferred to with same role and location in a dropdownlist
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
        [Route("reroute-application-Dropdownlist")]
        public async Task<IActionResult> RerouteApplicationDropdownlist(string email) => Response(await _adminService.RerouteApplicationDropdownlist(email).ConfigureAwait(false));

        /// <summary>
        /// This endpoint gets the list of all applications on the portal
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
        [Route("all-applications")]
        public async Task<IActionResult> AllApplications() => Response(await _adminService.AllApplications().ConfigureAwait(false));

        /// <summary>
        /// This endpoint displays the audit trail of applications be processed
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
        [Route("application-history")]
        public async Task<IActionResult> ApplicationHistory(int id) => Response(await _adminService.ApplicationHistory(id).ConfigureAwait(false));

        /// <summary>
        /// This endpoint displays detailed information of an application
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
        [Route("view-application")]
        public async Task<IActionResult> ViewApplication(int id) => Response(await _adminService.ViewApplication(id).ConfigureAwait(false));

        /// <summary>
        /// This endpoint displays documents submitted for a particular application
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
        [Route("submitted-documents")]
        public async Task<IActionResult> SubmittedDocument(int ApplicationId) => Response(await _adminService.SubmittedDocument(ApplicationId).ConfigureAwait(false));

       
    }
}
