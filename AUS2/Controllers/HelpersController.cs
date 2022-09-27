
using AUS2.Core.DAL.Repository.Services.Payment;
using AUS2.Core.Helper.Notification;
using AUS2.Core.ViewModels;
using Microsoft.Extensions.Options;

namespace AUS2.Controllers
{
    public class HelpersController : BaseController
    {
        private readonly PaymentService _paymentServiceRepository;
        private readonly AppSettings _appSettings;
        public HelpersController(INotification notification, PaymentService paymentServiceRepository, IOptions<AppSettings> appSettings) : base(notification)

        {
            _paymentServiceRepository = paymentServiceRepository;
            _appSettings = appSettings.Value;
        }

    

    }
}