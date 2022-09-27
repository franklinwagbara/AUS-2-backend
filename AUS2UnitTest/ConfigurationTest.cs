using AUS2.Core.DAL.IRepository;
using AUS2.Core.ViewModels.Dto.Request;
using Moq;
using Xunit;

namespace AUS2UnitTest
{
    public class ConfigurationTest
    {
        ApplicationModulesRequestDTO applicationModulesRequestDTO = new ApplicationModulesRequestDTO();
        LicenseTypeRequestDTO licenseTypeRequestDTO = new LicenseTypeRequestDTO();
        AppRequestViewModel applicationRequestDTO = new AppRequestViewModel();
        RoleViewModel roleViewModel = new RoleViewModel();

        int Id = 1;
        int applicationId;
        private IConfigurationService MockConfiguration()
        {
            Mock<IConfigurationService> mockObject = new Mock<IConfigurationService>();
            mockObject.Setup(m => m.Get_ModuleConfiguration());
            mockObject.Setup(m => m.Post_ModuleConfiguration(applicationModulesRequestDTO));
            mockObject.Setup(m => m.Delete_ModuleConfiguration(Id));
            mockObject.Setup(m => m.Get_PermitConfiguration());
            mockObject.Setup(m => m.Post_PermitConfiguration(licenseTypeRequestDTO));
            mockObject.Setup(m => m.Delete_PermitConfiguration(Id));
            mockObject.Setup(m => m.Get_ApplicationConfiguration(applicationId));
            mockObject.Setup(m => m.Edit_ApplicationConfiguration(applicationRequestDTO));
            mockObject.Setup(m => m.GetAllDocs());
            mockObject.Setup(m => m.AddRole(roleViewModel));
            return mockObject.Object;
        }

        [Fact]
        public void Get_ModuleTest()
        {
            IConfigurationService getData = MockConfiguration();
            var result = getData.Get_ModuleConfiguration();
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);

        }
        [Fact]
        public void Post_ModuleTest()
        {
            IConfigurationService postData = MockConfiguration();
            var result = postData.Post_ModuleConfiguration(applicationModulesRequestDTO);
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);

        }
        [Fact]
        public void Delete_ModuleTest()
        {
            IConfigurationService DeleteData = MockConfiguration();
            var result = DeleteData.Delete_ModuleConfiguration(Id);
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);

        }
        [Fact]
        public void Get_PermitTest()
        {
            IConfigurationService getData = MockConfiguration();
            var result = getData.Get_PermitConfiguration();
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);

        }
        [Fact]
        public void Post_PermitTest()
        {
            IConfigurationService postData = MockConfiguration();
            var result = postData.Post_PermitConfiguration(licenseTypeRequestDTO);
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);

        }
        [Fact]
        public void Delete_PermitTest()
        {
            IConfigurationService DeleteData = MockConfiguration();
            var result = DeleteData.Delete_PermitConfiguration(Id);
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);

        }
        [Fact]
        public void Get_ApplicationTest()
        {
            IConfigurationService getData = MockConfiguration();
            var result = getData.Get_ApplicationConfiguration(applicationId);
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);

        }
        [Fact]
        public void Post_ApplicationTest()
        {
            IConfigurationService postData = MockConfiguration();
            var result = postData.Edit_ApplicationConfiguration(applicationRequestDTO);
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);

        }
        [Fact]
        public void GetALlDocumentTypes()
        {
            IConfigurationService postData = MockConfiguration();
            var result = postData.GetAllDocs();
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);

        }
        [Fact]
        public void AddRole()
        {
            IConfigurationService postData = MockConfiguration();
            var result = postData.AddRole(roleViewModel);
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);

        }
    }

}
