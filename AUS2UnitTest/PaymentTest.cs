using AUS2.Core.DAL.Repository.Services.Payment;
using AUS2.Core.ViewModels.Dto.Request;
using Moq;
using Xunit;

namespace AUS2UnitTest
{
    public class PaymentTest
    {
        ExtraPaymentRequestDto model = new ExtraPaymentRequestDto();
        string companyID = string.Empty;
        private PaymentService MockPayment()
        {
            Mock<PaymentService> mockObject = new Mock<PaymentService>();
            mockObject.Setup(m => m.CreateExtraPayment(model));
            mockObject.Setup(m => m.GetCompanyExtraPayments(companyID));
            return mockObject.Object;
        }

        [Fact]
        public void CreateExtraPaymentTest()
        {
            PaymentService extrapaymentdetails = MockPayment();
            var expay = extrapaymentdetails.CreateExtraPayment(model);
            Assert.NotNull(expay);
            Assert.True(expay.IsCompletedSuccessfully);

        }

        [Fact]
        public void GetCompanyExtraPaymentsTest()
        {
            PaymentService companydetail = MockPayment();
            var getexpay = companydetail.GetCompanyExtraPayments(companyID);
            Assert.NotNull(getexpay);
            Assert.True(getexpay.IsCompletedSuccessfully);

        }

        [Fact]
        public void GetCompanyPaymentsTest()
        {
            PaymentService companydetail = MockPayment();
            var getpay = companydetail.GetCompanyPayments(companyID);
            Assert.NotNull(getpay);
            Assert.True(getpay.IsCompletedSuccessfully);
        }
    }

}
