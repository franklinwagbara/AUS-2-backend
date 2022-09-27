using AUS2.Core.ViewModels.Account;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.Dto.Response;
using System.Threading.Tasks;

namespace AUS2.Core.DAL.IRepository
{
    public interface IAccountService
    {
        Task<WebApiResponse> ValidateLogin(string email, string code);
        Task<WebApiResponse> GetAllUsers();
        Task<WebApiResponse> GetAllStaff();
        Task<WebApiResponse> AddNewStaff(UserMasterRequestDto model);
        Task<WebApiResponse> EditStaff(UserMasterRequestDto model);
        Task<WebApiResponse> ChangePassword(PasswordModel model);
        Task<WebApiResponse> DeleteStaff(string StaffEmail);
        Task<WebApiResponse> GetAllRoles();
    }
}
