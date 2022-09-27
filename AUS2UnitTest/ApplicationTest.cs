using AUS2.Core.Repository.Services.Account;
using Moq;
using Xunit;

namespace AUS2UnitTest
{
    public class ApplicationTest
    {
        string applicationStatus = "Processing";
        string Email = string.Empty;
        private ApplicationService MockApplication()
        {
            Mock<ApplicationService> mockObject = new Mock<ApplicationService>();
            mockObject.Setup(m => m.MyDesk());
            mockObject.Setup(m => m.MyDeskCount());
            mockObject.Setup(m => m.ApplicationsCount(applicationStatus));
            mockObject.Setup(m => m.PermitsCount());
            mockObject.Setup(m => m.CompanyApplications(Email));
            mockObject.Setup(m => m.CompanyPermits(Email));
            return mockObject.Object;
        }

        [Fact]
        public void MyDeskTest()
        {
            ApplicationService mydesk = MockApplication();
            var getDesk = mydesk.MyDesk();
            Assert.NotNull(getDesk);
            Assert.True(getDesk.IsCompletedSuccessfully);

        }

        [Fact]
        public void MyDeskCountTest()
        {
            ApplicationService mydesk = MockApplication();
            var getDesk = mydesk.MyDeskCount();
            Assert.NotNull(getDesk);
            Assert.True(getDesk.IsCompletedSuccessfully);

        }

        [Fact]
        public void ApplicationCountTest()
        {
            ApplicationService appCount = MockApplication();
            var getCount = appCount.ApplicationsCount(applicationStatus);
            Assert.NotNull(getCount);
            Assert.True(getCount.IsCompletedSuccessfully);

        }

        [Fact]
        public void PermitCountTest()
        {
            ApplicationService permitCount = MockApplication();
            var getCount = permitCount.PermitsCount();
            Assert.NotNull(getCount);
            Assert.True(getCount.IsCompletedSuccessfully);

        }

        [Fact]
        public void CompanyApplicationsTest()
        {
            ApplicationService adminmock = MockApplication();
            var admin = adminmock.CompanyApplications(Email);
            Assert.NotNull(admin);
            Assert.True(admin.IsCompletedSuccessfully);
        }

        [Fact]
        public void CompanyPermitsTest()
        {
            ApplicationService adminmock = MockApplication();
            var admin = adminmock.CompanyPermits(Email);
            Assert.NotNull(admin);
            Assert.True(admin.IsCompletedSuccessfully);
        }

    }

}
