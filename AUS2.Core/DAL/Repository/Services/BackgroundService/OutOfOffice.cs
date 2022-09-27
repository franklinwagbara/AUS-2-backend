using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AUS2.Core.DBObjects;
using AUS2.Core.Helper.SerilogService.Account;

namespace AUS2.Core.DAL.Repository.Services.BackgroundService
{
    public class OutOfOfficeService
    {
        private readonly ApplicationContext _context;
        private readonly GeneralLogger _generalLogger;
        private readonly string directory = "OutOfOffice";
        public OutOfOfficeService(ApplicationContext context, GeneralLogger generalLogger)
        {
            _context = context;
            _generalLogger = generalLogger;

        }


        public async void StaffStartOutofOffice()
        {
            try
            {
                var today = DateTime.Now.Date;
                List<OutOfOffice> office = (from o in _context.OutOfOffices where o.StartDate == today select o).ToList();
                if (office.Count > 0)
                {
                    foreach (var item in office)
                    {
                        item.Status = "Started";
                    }
                    await _context.SaveChangesAsync();
                    _generalLogger.LogRequest($"{"BackgroundService--Out of office have started"}{"-"}{DateTime.Now}", false, directory);
                }
                else
                {
                    _generalLogger.LogRequest($"{"StartOutOfOffice--No record found"}{"-"}{DateTime.Now}", false, directory);

                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"BackgroundService--An exception occurred trying to start out of office "+ ex.ToString()}{"-"}{DateTime.Now}", true, directory);
            }
        }

        public async void StaffEndOutofOffice()
        {
            try
            {

                var today = DateTime.Now.Date;
                List<OutOfOffice> office = (from o in _context.OutOfOffices where o.EndDate <= today select o).ToList();
                if (office.Count > 0)
                {
                    foreach (var item in office)
                    {
                        item.Status = "Finished";
                    }
                    await _context.SaveChangesAsync();
                    _generalLogger.LogRequest($"{"BackgroundService--Out of office have ended"}{"-"}{DateTime.Now}", false, directory);
                }
                else
                {
                    _generalLogger.LogRequest($"{"EndOutOfOffice--No record found"}{"-"}{DateTime.Now}", false, directory);
                }

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"BackgroundService--An exception occurred trying to end out of office " + ex.ToString()}{"-"}{DateTime.Now}", true, directory);

            }
        }
    }
}
