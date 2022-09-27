using AUS2.Core.DAL.IRepository;
using AUS2.Core.Helper.Notification;
using AUS2.Core.ViewModels.Account;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AUS2.Controllers
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [ApiController]
    [Authorize]
    [Route("api/aus/account")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountServiceRepository;
        public AccountController(INotification notification, IAccountService accountServiceRepository) : base(notification)
        {
            _accountServiceRepository = accountServiceRepository;
        }

        /// <summary>
        /// This endpoint allow user to login 
        /// </summary>
        /// <param name="email">ELPS registered email</param>
        /// <param name="code">Code generated from ELPS on login</param>
        /// <returns>This endpoint returns login user email</returns>
        /// <response code="200">Success response</response>
        /// <response code="404">Not found response</response>
        /// <response code="500">Internal server response</response>
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [AllowAnonymous]
        //[HttpPost]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string code)// => Response(await _accountServiceRepository.ValidateLogin(email, code).ConfigureAwait(false));
        {
            if (code == "" || email == "")
                return NoContent();
            else
            {
                var result = await _accountServiceRepository.ValidateLogin(email, code);
                if (result.StatusCode == 200)
                    return Created("", result);
                if (result.StatusCode == 404)
                    return NotFound();
                if (result.StatusCode == 500)
                    return Forbid();
                return BadRequest();
            }
        }

        /// <summary>
        /// This endpoint fetches all staff registered on elps
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
        [Route("all-elps-staff")]
        public async Task<IActionResult> GetAllStaff() => Response(await _accountServiceRepository.GetAllStaff().ConfigureAwait(false));


        /// <summary>
        /// This endpoint fetches all staff registered on the portal
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
        [Route("all-staff")]
        public async Task<IActionResult> GetAllUsers() => Response(await _accountServiceRepository.GetAllUsers().ConfigureAwait(false));



        /// <summary>
        /// This endpoint is use to add new staff on the portal
        /// </summary>
        ///        
        /// <param name="model">This parameter is use to add new staff on the portal</param>
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
        [Route("add-staff")]
        public async Task<IActionResult> AddNewStaff([FromBody] UserMasterRequestDto model) => Response(await _accountServiceRepository.AddNewStaff(model).ConfigureAwait(false));



        /// <summary>
        /// This endpoint is use to edit an existing staff on the portal
        /// </summary>
        ///        
        /// <param name="model">This parameter is use to edit an existing staff on the portal</param>
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
        [Route("edit-staff")]
        public async Task<IActionResult> EditStaff([FromForm] UserMasterRequestDto model) => Response(await _accountServiceRepository.EditStaff(model).ConfigureAwait(false));



        /// <summary>
        /// This endpoint is use to activate an existing staff on the portal
        /// </summary>
        ///        
        /// <param name="model">This parameter is use to activate an existing staff on the portal</param>
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
        [Route("activate-staff")]
        public async Task<IActionResult> ActivateStaff([FromForm] UserMasterRequestDto model) => Response(await _accountServiceRepository.EditStaff(model).ConfigureAwait(false));



        /// <summary>
        /// This endpoint is use to deactivate an existing staff on the portal
        /// </summary>
        ///        
        /// <param name="model">This parameter is use to deactivate an existing staff on the portal</param>
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
        [Route("deactivate-staff")]
        public async Task<IActionResult> DeactivateStaff([FromForm] UserMasterRequestDto model) => Response(await _accountServiceRepository.EditStaff(model).ConfigureAwait(false));



        /// <summary>
        /// This endpoint is use to delete an existing staff from the portal
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
        [HttpDelete]
        [Route("deactivate-staff")]
        public async Task<IActionResult> DeleteStaff(string StaffEmail) => Response(await _accountServiceRepository.DeleteStaff(StaffEmail).ConfigureAwait(false));

        /// <summary>
        /// This endpoint is use to get all roles on the portal
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
        [Route("all-staff-roles")]
        public async Task<IActionResult> AllStaffRoles(string StaffEmail) => Response(await _accountServiceRepository.GetAllRoles().ConfigureAwait(false));


        /// <summary>
        /// This endpoint is use to change password on the portal
        /// </summary>
        ///        
        /// <param name="model">This parameter is use to change password on the portal</param>
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
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordModel model) => Response(_accountServiceRepository.ChangePassword(model));

    }
}
