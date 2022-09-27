using AUS2.Core.DAL.IRepository;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.Dto.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AUS2.Core.DAL.IRepository
{
    public interface IAdminService
    {
        Task<WebApiResponse> AddOutOfOffice(OutOfOfficeRequestDto model);
        Task<WebApiResponse> DeleteOutOfOffice(Delete_EditOutOfOfficeRequestDto model);
        Task<WebApiResponse> GetAllStaffOutOfOffice();
        Task<WebApiResponse> GetRelievedStaffOutOfOffice();
        Task<WebApiResponse> EndLeave(Delete_EditOutOfOfficeRequestDto model); 
        Task<WebApiResponse> EditOutOfOffice(OutOfOfficeRequestDto model);
        Task<WebApiResponse> StaffDesk();
        Task<WebApiResponse> RerouteApplication(List<RerouteApplicationRequestDto> model);
        Task<WebApiResponse> RerouteApplicationDropdownlist(string email);
        Task<WebApiResponse> AllApplications();
        Task<WebApiResponse> ApplicationHistory(int id);
        Task<WebApiResponse> ViewApplication(int id);
        Task<WebApiResponse> SubmittedDocument(int id);
        Task<WebApiResponse> AllRegisteredCompany();
        Task<WebApiResponse> StaffDashboard();
       
    }
}
