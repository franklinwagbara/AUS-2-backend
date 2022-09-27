using AUS2.Core.ViewModels.Account;
using AUS2.Core.ViewModels.AdminModel;
using AUS2.Core.ViewModels.CompanyModel;
using AUS2.Core.ViewModels.Dto.Response;
using AUS2.Core.ViewModels.Payment;
using System.Collections.Generic;

namespace AUS2.Core.DAL.IRepository
{
   public interface IElpsService
    {

        WebApiResponse GetCompanyDetailByEmail(string email);
        WebApiResponse GetAllStaff();
        WebApiResponse GetStaff(string email);
        ElpsResponse GetElpAppUpdateStatus(string Appid, string ElpsID, string Status);
        WebApiResponse UpdateCompanyDetails(CompanyModel model, string email, bool update);
        string UpdateCompanyNameEmail(object model);
        WebApiResponse AddCompanyRegAddress(List<RegisteredAddress> model, int companyId);
        WebApiResponse UpdateCompanyRegAddress(List<RegisteredAddress> model);
        WebApiResponse UpdatePassword(PasswordModel model);
        WebApiResponse GetCompanyAddressById(int id);
        public WebApiResponse GetCompanyDocumentsById(int id);
        WebApiResponse GetExtraPayment_RRR(string companyELPSID, string email, RemitaSplit rm);
        ElpsResponse PostFacility(Facilities facilityDetails);
        WebApiResponse GetAllDocs(string type);
        string ReferenceCode();
    }
}
