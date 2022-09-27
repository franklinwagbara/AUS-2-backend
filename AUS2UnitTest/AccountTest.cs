using AUS2.Core.DAL.IRepository;
using AUS2.Core.ViewModels.Account;
using AUS2.Core.ViewModels.Dto.Request;
using Moq;
using Xunit;


namespace AUS2UnitTest
{
    public class AccountTest
    {
        string email = ""; string code = "";
        UserMasterRequestDto usermodel = new UserMasterRequestDto();
        PasswordModel passwordObj = new PasswordModel(); 
        //{ Email = "drayprogrammer45@yahoo.com", OldPassword = "Jibade123_!", NewPassword = "Jibade123_!@", ConfirmPassword = "Jibade123_!@" };
        private IAccountService MockAccount()
        {
            Mock<IAccountService> mockObject = new Mock<IAccountService>();
            mockObject.Setup(m => m.ValidateLogin(email, code));
            mockObject.Setup(m => m.AddNewStaff(usermodel));
            mockObject.Setup(p => p.ChangePassword(passwordObj));
            return mockObject.Object;
        }


        [Fact]
        public void LoginTest()
        {
            IAccountService loggedindetails = MockAccount();
            var acc = loggedindetails.ValidateLogin(email, code);
            Assert.NotNull(acc);
            Assert.True(acc.IsCompletedSuccessfully);
        }


        [Fact]
        public void AddNewStaffTest()
        {
            IAccountService loggedindetails = MockAccount();
            var acc = loggedindetails.AddNewStaff(usermodel);
            Assert.NotNull(acc);
            Assert.True(acc.IsCompletedSuccessfully);
        }

        [Fact]
        public void ChnagePasswordTest()
        {
            IAccountService loggedindetails = MockAccount();
            var passwordresult = loggedindetails.ChangePassword(passwordObj);
            Assert.NotNull(passwordresult);
            Assert.True(passwordresult.IsCompletedSuccessfully);
        }
    } 
}
