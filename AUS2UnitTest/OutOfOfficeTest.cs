using AUS2.Controllers;
using AUS2.Core.DAL.IRepository;
using AUS2.Core.Helper.Notification;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.Dto.Response;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AUS2UnitTest
{
    public class OutOfOfficeTest
    {
        
        OutOfOfficeRequestDto model = new OutOfOfficeRequestDto();
        Delete_EditOutOfOfficeRequestDto delete_editmodel = new Delete_EditOutOfOfficeRequestDto();

        private Mock<INotification> mockNotification;
        private Mock<IAdminService> mockAdminService;
        private AdminController sut;

        public OutOfOfficeTest()
        {
            mockNotification = new Mock<INotification>();
            mockAdminService = new Mock<IAdminService>();
            sut = new AdminController(mockNotification.Object, mockAdminService.Object);
        }
        private IAdminService MockOutOfOffice()
        {
         
            Mock<IAdminService> mockObject = new Mock<IAdminService>();
            mockObject.Setup(m => m.DeleteOutOfOffice(delete_editmodel));
            mockObject.Setup(m => m.EditOutOfOffice(model));
            mockObject.Setup(m => m.EndLeave(delete_editmodel));
            mockObject.Setup(m => m.GetAllStaffOutOfOffice());
            mockObject.Setup(m => m.GetRelievedStaffOutOfOffice());
            return mockObject.Object;
        }


        [Fact]
        public async Task AddOutOfOfficeTest_ShouldReturnCreatedResultWhenOutOfOfficeIsSaved()
        {
            //Arrange
            WebApiResponse res = new WebApiResponse();

            OutOfOfficeRequestDto model1 = new OutOfOfficeRequestDto();
            model1.OutofOfficeId = 1;
            model1.RelievedEmail = "sheriff.ebelebe@gmail.com";
            model1.RelieverEmail = "offficer@gmail.com";
            model1.StartDate = DateTime.Now;
            model1.EndDate = DateTime.Now;

            res.ResponseCode = "00";
            res.StatusCode = 201;
            res.Data = model1;

           

            mockAdminService.Setup(m => m.AddOutOfOffice(model1)).ReturnsAsync(res);

            //Act
            var outofoffice = await sut.AddOutofOffice(model1);

            //Assert
            outofoffice.Should().BeOfType<CreatedResult>();
            var objectResult = (CreatedResult)outofoffice;
            objectResult.StatusCode.Should().Be(res.StatusCode);
            objectResult.Value.Should().BeEquivalentTo(res);
            var objectValue = (WebApiResponse)objectResult.Value;
            objectValue.ResponseCode.Should().Be(res.ResponseCode);
            objectValue.Data.Should().BeEquivalentTo(res.Data, x => x.ComparingRecordsByMembers().ComparingByValue<OutOfOfficeRequestDto>());
        }




        [Fact]
        public async Task AddOutOfOfficeTest_ShouldReturnNoContentIfNoModelWasPassed()
        {
            //Arrange
            WebApiResponse res = new WebApiResponse();


            res.StatusCode = 204;

            mockAdminService.Setup(m => m.AddOutOfOffice(null)).ReturnsAsync(res);

            //Act
            var outofoffice = await sut.AddOutofOffice(null);

            //Assert
            outofoffice.Should().BeOfType<NoContentResult>();
            var objectResult = (NoContentResult)outofoffice;
            objectResult.StatusCode.Should().Be(204);

        }





        [Fact]
        public void DeleteOutOfOfficeTest()
        {
            IAdminService outofofficemock = MockOutOfOffice();
            var outofoffice = outofofficemock.DeleteOutOfOffice(delete_editmodel);
            Assert.NotNull(outofoffice);
            Assert.True(outofoffice.IsCompletedSuccessfully);
        }


        [Fact]
        public void EditOutOfOfficeTest()
        {
            IAdminService outofofficemock = MockOutOfOffice();
            var outofoffice = outofofficemock.EditOutOfOffice(model);
            Assert.NotNull(outofoffice);
            Assert.True(outofoffice.IsCompletedSuccessfully);
        }


        [Fact]
        public void EndLeaveTest()
        {
            IAdminService outofofficemock = MockOutOfOffice();
            var outofoffice = outofofficemock.EndLeave(delete_editmodel);
            Assert.NotNull(outofoffice);
            Assert.True(outofoffice.IsCompletedSuccessfully);
        }


        [Fact]
        public void GetAllStaffOutOfOfficeTest()
        {
            IAdminService outofofficemock = MockOutOfOffice();
            var outofoffice = outofofficemock.GetAllStaffOutOfOffice();
            Assert.NotNull(outofoffice);
            Assert.True(outofoffice.IsCompletedSuccessfully);
        }


        [Fact]
        public void GetRelievedStaffOutOfOfficeTest()
        {
            IAdminService outofofficemock = MockOutOfOffice();
            var outofoffice = outofofficemock.GetRelievedStaffOutOfOffice();
            Assert.NotNull(outofoffice);
            Assert.True(outofoffice.IsCompletedSuccessfully);
        }
    }
}
