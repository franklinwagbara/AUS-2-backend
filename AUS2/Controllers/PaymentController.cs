using AUS2.Core.DAL.Repository.Services.Payment;
using AUS2.Core.Helper.Notification;
using AUS2.Core.ViewModels;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace AUS2.Controllers
{
    [Authorize]
    [Route("api/aus/payment")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly PaymentService _paymentServiceRepository;
        private readonly AppSettings _appSettings;
        public PaymentController(INotification notification, PaymentService paymentServiceRepository, IOptions<AppSettings> appSettings) : base(notification)

        {
            _paymentServiceRepository = paymentServiceRepository;
            _appSettings = appSettings.Value;
        }


        /// <summary>
        /// This endpoint creates extra payment for application.
        /// </summary>
        /// 
        /// <returns>Returns a success or failure message.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        /// <response code="200">Returns success message </response>
        /// <response code="404">'Duplicate record' error message </response>
        /// <response code="409">'Record not found' message </response>
        /// <response code="500">Internal server error - bad request - something went wrong </response>
        /// 
        [ProducesResponseType(typeof(WebApiResponse), 200)]
        [ProducesResponseType(typeof(WebApiResponse), 404)]
        [ProducesResponseType(typeof(WebApiResponse), 409)]
        [ProducesResponseType(typeof(WebApiResponse), 500)]
        [HttpPost]
        [Route("create-extrapayment")]
        public async Task<IActionResult> CreateExtraPayment([FromForm] ExtraPaymentRequestDto model) => Response(await _paymentServiceRepository.CreateExtraPayment(model).ConfigureAwait(false));


        /// <summary>
        /// This endpoint fetches all company's extra payments.
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
        [Route("company-extrapayments")]
        public async Task<IActionResult> GetCompanyExtraPayments(string companyID) => Response(await _paymentServiceRepository.GetCompanyExtraPayments(companyID).ConfigureAwait(false));


        /// <summary>
        /// This endpoint fetches all company's payments.
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
        [Route("company-payments")]
        public async Task<IActionResult> GetCompanyPayments(string companyID) => Response(await _paymentServiceRepository.GetCompanyPayments(companyID).ConfigureAwait(false));


    }
}