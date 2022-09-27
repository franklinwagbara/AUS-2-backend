using AUS2.Core.Repository.Services.Admin;
using Moq;
using Xunit;

namespace AUS2UnitTest
{
    public class AdminTest
    {
        int ApplicationId;
        string Email = string.Empty;

        private AdminService MockAdmin()
        {
            Mock<AdminService> mockObject = new Mock<AdminService>();
            mockObject.Setup(m => m.AllApplications());
            mockObject.Setup(m => m.ApplicationHistory(ApplicationId));
            mockObject.Setup(m => m.ViewApplication(ApplicationId));
            mockObject.Setup(m => m.SubmittedDocument(ApplicationId));
            mockObject.Setup(m => m.AllRegisteredCompany());
            return mockObject.Object;
        }

        [Fact]
        public void AllApplicationsTest()
        {
            AdminService adminmock = MockAdmin();
            var admin = adminmock.AllApplications();
            Assert.NotNull(admin);
            Assert.True(admin.IsCompletedSuccessfully);
        }

        [Fact]
        public void ApplicationHistoryTest()
        {
            AdminService adminmock = MockAdmin();
            var admin = adminmock.ApplicationHistory(ApplicationId);
            Assert.NotNull(admin);
            Assert.True(admin.IsCompletedSuccessfully);
        }


        [Fact]
        public void ViewApplicationTest()
        {
            AdminService adminmock = MockAdmin();
            var admin = adminmock.ViewApplication(ApplicationId);
            Assert.NotNull(admin);
            Assert.True(admin.IsCompletedSuccessfully);
        }


        [Fact]
        public void SubmittedDocumentTest()
        {
            AdminService adminmock = MockAdmin();
            var admin = adminmock.SubmittedDocument(ApplicationId);
            Assert.NotNull(admin);
            Assert.True(admin.IsCompletedSuccessfully);
        }


        [Fact]
        public void AllRegisteredCompanyTest()
        {
            AdminService adminmock = MockAdmin();
            var admin = adminmock.AllRegisteredCompany();
            Assert.NotNull(admin);
            Assert.True(admin.IsCompletedSuccessfully);
        }

    }
}
