using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskBadgeModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Controller;
using AskDefinex.Rest.Model.Request;
using AskDefinex.Rest.Model.Request.AskBadgeModule;
using AskDefinex.Rest.Model.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace AskDefinexUnitTest.UnitTests.Controller
{
    public class AskBadgeControllerUnitTest
    {
        private readonly Mock<ILogger<AskBadgeController>> _logManager;
        private readonly Mock<IAskBadgeService> _badgeService;
        private readonly Mock<IMapper> _mapper;
        private readonly BadgeDetailModel _badgeDetailModel;
        private readonly BadgeCreateModel _badgeCreateModel;
        private readonly BadgeUpdateModel _badgeUpdateModel;
        private readonly BadgeDeleteModel _badgeDeleteModel;
        private readonly BadgeDetailRequestModel _badgeDetailRequestModel;
        private readonly BadgeCreateRequestModel _badgeCreateRequestModel;
        private readonly BadgeUpdateRequestModel _badgeUpdateRequestModel;
        private readonly BadgeDeleteRequestModel _badgeDeleteRequestModel;
        public AskBadgeControllerUnitTest()
        {
            _badgeDetailModel = new BadgeDetailModel()
            {
                Id = 1,
                Name = "test",
                Type = "1",
                UserId = 1,
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _badgeCreateModel = new BadgeCreateModel() 
            {
                Id = 1,
                Name = "test",
                Type = "1",
                UserId = 1,
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _badgeUpdateModel = new BadgeUpdateModel()
            {
                Id = 1,
                Name = "test",
                Type = "1",
                UserId = 1,
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _badgeDeleteModel = new BadgeDeleteModel()
            {
                Id = 1,
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _badgeDetailRequestModel = new BadgeDetailRequestModel()
            {
                UserId = 1
            };

            _badgeCreateRequestModel = new BadgeCreateRequestModel()
            {
                UserId = 1,
                Name = "test",
                IsActive = true,
                Type = "1"
            };

            _badgeUpdateRequestModel = new BadgeUpdateRequestModel()
            {
                Id = 1,
                Type = "1",
                Name = "test",
                IsActive = true,
                UserId = 1
            };

            _badgeDeleteRequestModel = new BadgeDeleteRequestModel()
            {
                Id = 1,
                IsActive = true
            };

            _logManager = new Mock<ILogger<AskBadgeController>>();
            _badgeService = new Mock<IAskBadgeService>();
            _mapper = new Mock<IMapper>();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void GetBadgeByUserId()
        {
            _badgeService.Setup(x => x.GetBadgeByUserId(_badgeDetailRequestModel.UserId)).Returns(_badgeDetailModel);
            _mapper.Setup(x => x.Map<BadgeDetailModel, BadgeDetailResponseModel>(_badgeDetailModel));
            var badgeController = new AskBadgeController(_logManager.Object, _badgeService.Object, _mapper.Object);
            var actual = badgeController.GetBadgeByUserId(_badgeDetailRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void CreateBadge()
        {
            _badgeService.Setup(x => x.CreateBadge(_badgeCreateModel)).Returns(1);
            _mapper.Setup(x => x.Map<BadgeCreateRequestModel, BadgeCreateModel>(_badgeCreateRequestModel)).Returns(_badgeCreateModel);
            var badgeController = new AskBadgeController(_logManager.Object, _badgeService.Object, _mapper.Object);
            var actual = badgeController.CreateBadge(_badgeCreateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void UpdateBadge()
        {
            _badgeService.Setup(x => x.UpdateBadge(_badgeUpdateModel));
            _mapper.Setup(x => x.Map<BadgeUpdateRequestModel, BadgeUpdateModel>(_badgeUpdateRequestModel)).Returns(_badgeUpdateModel);
            var badgeController = new AskBadgeController(_logManager.Object, _badgeService.Object, _mapper.Object);
            var actual = badgeController.UpdateBadge(_badgeUpdateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void DeleteBadge()
        {
            _badgeService.Setup(x => x.DeleteBadge(_badgeDeleteModel));
            _mapper.Setup(x => x.Map<BadgeDeleteRequestModel, BadgeDeleteModel>(_badgeDeleteRequestModel)).Returns(_badgeDeleteModel);
            var badgeController = new AskBadgeController(_logManager.Object, _badgeService.Object, _mapper.Object);
            var actual = badgeController.DeleteBadge(_badgeDeleteRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
    }
}
