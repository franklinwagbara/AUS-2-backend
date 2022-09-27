using AUS2.Core.DAL.Repository.Services.BackgroundService;
using AUS2.Core.DBObjects;
using AUS2.Core.Helper.SerilogService.Account;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AUS2.Core.DAL.Repository.Services.BackgroundService
{
  public  class OutOfOfficeBackgroundService : BackgroundJobs
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly GeneralLogger _generalLogger;
        OutOfOfficeService _outOfOffice;
        private readonly string directory = "OutOfOffice";
        public OutOfOfficeBackgroundService(IServiceScopeFactory scopeFactory, GeneralLogger generalLogger)
        {
            _scopeFactory = scopeFactory;
            _generalLogger = generalLogger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
               _generalLogger.LogRequest($"{"OutOfOfficeService background task is stopping."}", false, directory));

            while (!stoppingToken.IsCancellationRequested)
            {
                var dbContext = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>();
                _outOfOffice = new OutOfOfficeService(dbContext, _generalLogger);
               _outOfOffice.StaffStartOutofOffice();
                _outOfOffice.StaffEndOutofOffice();
                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
        }
    }
}
