using AUS2.Core.DAL.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AUS2.Controllers
{
    [AllowAnonymous]
    [Route("api/aus/auth")]
    public class AuthController : Controller
    {
        private readonly IAccountService _accountServiceRepository;
        public readonly IConfiguration _configuration;

        public AuthController(IAccountService accountServiceRepository, IConfiguration configuration)
        {
            _accountServiceRepository = accountServiceRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login-redirect")]
        public async Task<IActionResult> LoginRedirect(string email, string code)
        {
            var loginvalid = await _accountServiceRepository.ValidateLogin(email,code);
            if (loginvalid.ResponseCode == "00")
                return Redirect($"{_configuration["AppSettings:LoginUrl"]}/home?email={email}");
            else
                return Redirect($"{_configuration["AppSettings:LoginUrl"]}/home");
        }

        [HttpGet]
        [Route("log-out")]
        public async Task<IActionResult> Logout() 
        {
            var elpsLogOffUrl = $"{_configuration["AppSettings:ElpsUrl"]}/Account/RemoteLogOff";
            var returnUrl = $"{_configuration["AppSettings:LoginUrl"]}";
            var frm = "<form action='" + elpsLogOffUrl + "' id='frmTest' method='post'>" + "<input type='hidden' name='returnUrl' value='" + returnUrl + "' />" + "<input type='hidden' name='appId' value='" + _configuration["AppSettings:AppKey"] + "' />" + "</form>" + "<script>document.getElementById('frmTest').submit();</script>";
            return Content(frm, "text/html");
        }
    }
}
