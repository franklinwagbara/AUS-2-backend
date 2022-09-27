using AUS2.Core.DAL.IRepository;
using AUS2.Core.Helper.Notification;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AUS2.Controllers
{
    [Authorize]
    [Route("api/aus/configuration")]
    [ApiController]
    public class ConfigurationController : BaseController
    {
        private readonly IConfigurationService _configurationserviceRepository;
        public ConfigurationController(INotification notification, IConfigurationService ConfigurationServiceRepository) : base(notification)

        {
            _configurationserviceRepository = ConfigurationServiceRepository;
        }


        /// <summary>
        /// This endpoint returns all applications modules record.
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
        [Route("get-module-configuration")]
        public async Task<IActionResult> GetModuleConfiguration() => Response(await _configurationserviceRepository.Get_ModuleConfiguration().ConfigureAwait(false));



        /// <summary>
        /// This endpoint creates new application module.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="404">'Duplicate record' error message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpPost]
        [Route("post-module-configuration")]
        public async Task<IActionResult> PostModuleConfiguration([FromBody] ApplicationModulesRequestDTO model) => Response(await _configurationserviceRepository.Post_ModuleConfiguration(model).ConfigureAwait(false));
        

        /// <summary>
        /// This endpoint deletes application module.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="409">'Record not found' message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 409)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpDelete]
        [Route("delete-module-configuration")]
        public async Task<IActionResult> DeleteModuleConfiguration(int moduleID) => Response(await _configurationserviceRepository.Delete_ModuleConfiguration(moduleID).ConfigureAwait(false));
        


        /// <summary>
        /// This endpoint returns all permits configuration record.
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
        [Route("get-permit-configuration")]
        public async Task<IActionResult> GetPermitConfiguration() => Response(await _configurationserviceRepository.Get_PermitConfiguration().ConfigureAwait(false));



        /// <summary>
        /// This endpoint creates new permit.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="404">'Duplicate record' error message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpPost]
        [Route("post-permit-configuration")]
        public async Task<IActionResult> PostPermitConfiguration([FromBody] PhaseDto model) => Response(await _configurationserviceRepository.Post_PermitConfiguration(model).ConfigureAwait(false));



        /// <summary>
        /// This endpoint deletes permit.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="409">'Record not found' message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 409)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpDelete]
        [Route("delete-permit-configuration")]
        public async Task<IActionResult> DeletePermitConfiguration(int permitID) => Response(await _configurationserviceRepository.Delete_PermitConfiguration(permitID).ConfigureAwait(false));

        /// <summary>
        /// This endpoint returns a specific application information.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="409">'Record not found' message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 409)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("get-application-configuration")]
        public async Task<IActionResult> GetApplicationConfiguration(int applicationID) => Response(await _configurationserviceRepository.Get_ApplicationConfiguration(applicationID).ConfigureAwait(false));


        /// <summary>
        /// This endpoint updates specific application information.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="409">'Record not found' message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 409)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpPut]
        [Route("edit-application-configuration")]
        public async Task<IActionResult> EditApplicationConfiguration([FromForm] AppRequestViewModel model) => Response(await _configurationserviceRepository.Edit_ApplicationConfiguration(model).ConfigureAwait(false));



        /// <summary>
        /// This endpoint returns all documnet types from ELPS/NELPS.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="409">'Record not found' message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 409)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpGet]
        [Route("get-all-docs")]
        public async Task<IActionResult> GetAllDocs() => Response(await _configurationserviceRepository.GetAllDocs().ConfigureAwait(false));

        /// <summary>
        /// This endpoint is used to configure required documents for a permit.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="409">'Record not found' message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 409)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpPost]
        [Route("permit-type-docs")]
        public async Task<IActionResult> PermitTypeDocs(LicenseTypeDocsViewModel model) => Response(await _configurationserviceRepository.UpdatePermitDocs(model).ConfigureAwait(false));

        /// <summary>
        /// This endpoint is used to configure roles.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="409">'Record not found' message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 409)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpPost]
        [Route("roles")]
        public async Task<IActionResult> UpdateRoles(RoleViewModel model) => Response(await _configurationserviceRepository.AddRole(model).ConfigureAwait(false));

        /// <summary>
        /// This endpoint is fetch all configured Field and Zonal Offices.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="409">'Record not found' message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 409)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpPost]
        [Route("field-offices")]
        public async Task<IActionResult> GetOffices(RoleViewModel model) => Response(await _configurationserviceRepository.GetOffices().ConfigureAwait(false));

        /// <summary>
        /// This endpoint is used to add new field/zonal office to the portal.
        /// </summary>
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="409">'Record not found' message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 409)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpPost]
        [Route("add-field-office")]
        public async Task<IActionResult> AddFieldOffice(FieldOfficeDto model) => Response(await _configurationserviceRepository.AddFieldOffice(model).ConfigureAwait(false));
    }
}
