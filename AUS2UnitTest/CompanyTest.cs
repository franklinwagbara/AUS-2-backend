using AUS2.Core.DAL.Repository.Services.Company;
using AUS2.Core.ViewModels.CompanyModel;
using AUS2.Core.ViewModels.Dto.Response;
using Moq;
using Xunit;

namespace AUS2UnitTest
{
    public class CompanyTest
    {
        private string companyemial = string.Empty;//"drayprogrammer45@yahoo.com";
        CompanyInformation companyInfoModel = new CompanyInformation(); //{ Company = { }, Director = { }, DocumentModel = { }, oldemail = null, OperationalAddress = { }, RegisteredAddress = { } };
        private string companyemail = "drayprogrammer45@yahoo.com";
        private string licenseno = "";

        private CompanyService MockGetCompanyDetails()
        {
            Mock<CompanyService> _company = new Mock<CompanyService>();
            _company.Setup(c => c.GetCompanyDetails(companyemial));
            _company.Setup(c => c.UpdateCompanyDetails(companyInfoModel));
            _company.Setup(c => c.GetCompanyDocumentByEmail(companyemial));

            return _company.Object;
        }

        [Fact]
        public void GetCompanyDetailsTest()
        {
            CompanyService _company = MockGetCompanyDetails();
            var dic = _company.GetCompanyDetails(companyemial);
            Assert.NotNull(dic);
            Assert.True(dic.IsCompletedSuccessfully);
        }

        [Fact]
        public void MockCompanyLibraryTest()
        {
            CompanyService _company = MockGetCompanyDetails();
            var dic = _company.GetCompanyDocumentByEmail(companyemial);
            Assert.NotNull(dic);
            Assert.True(dic.IsCompletedSuccessfully);
        }

        [Fact]
        public void MockUpdateCompanyInfo()
        {
            CompanyService _company = MockGetCompanyDetails();
            var dic = _company.UpdateCompanyDetails(companyInfoModel);
            Assert.NotNull(dic);
            Assert.True(dic.IsCompletedSuccessfully);
        }

        [Fact]
        public void MockGetApprovedCompanyPermits()
        {
            CompanyService _company = MockGetCompanyDetails();
            var dic = _company.GetPermits(companyemail);
            Assert.NotNull(dic);
            Assert.True(dic.IsCompletedSuccessfully);
        }

        [Fact]
        public void MockViewCompanyPermit()
        {
            CompanyService _company = MockGetCompanyDetails();
            var dic = _company.ViewPermit(companyemail, licenseno);
            Assert.NotNull(dic);
            Assert.True(dic.IsCompletedSuccessfully);
        }

        [Fact]
        public void MockMyApplications()
        {
            CompanyService _company = MockGetCompanyDetails();
            var dic = _company.MyApplications(companyemail);
            Assert.NotNull(dic);
            Assert.True(dic.IsCompletedSuccessfully);
        }
    }
}
