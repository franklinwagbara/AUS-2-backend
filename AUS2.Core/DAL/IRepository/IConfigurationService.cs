using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.Dto.Response;
using System.Threading.Tasks;

namespace AUS2.Core.DAL.IRepository
{
    public interface IConfigurationService
    {
        Task<WebApiResponse> Get_ModuleConfiguration();
        Task<WebApiResponse> Post_ModuleConfiguration(ApplicationModulesRequestDTO model);
        Task<WebApiResponse> Delete_ModuleConfiguration(int moduleID);
        Task<WebApiResponse> Get_PermitConfiguration();
        Task<WebApiResponse> Post_PermitConfiguration(LicenseTypeRequestDTO model);
        Task<WebApiResponse> Delete_PermitConfiguration(int licenseTypeID);
        Task<WebApiResponse> Get_ApplicationConfiguration(int applicationID);
        Task<WebApiResponse> Edit_ApplicationConfiguration(AppRequestViewModel model);
        Task<WebApiResponse> GetAllDocs();
        Task<WebApiResponse> UpdatePermitDocs(LicenseTypeDocsViewModel model);
        Task<WebApiResponse> AddRole(RoleViewModel model);
        Task<WebApiResponse> GetOffices();
        Task<WebApiResponse> AddFieldOffice(FieldOfficeDto model);
    }
}
