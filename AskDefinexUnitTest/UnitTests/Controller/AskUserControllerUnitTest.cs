using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskAnswerModule;
using AskDefinex.Business.Model.AskUserModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Controller;
using AskDefinex.Rest.Model.Request;
using AskDefinex.Rest.Model.Request.AskAnswerModule;
using AskDefinex.Rest.Model.Request.AskUserModule;
using AskDefinex.Rest.Model.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AskDefinexUnitTest.UnitTests.Controller
{
    public class AskUserControllerUnitTest
    {
        private readonly Mock<ILogger<AskUserController>> _mockLogManager;
        private readonly Mock<IAskUserService> _mockUserService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AskUserDetailRequestModel _askUserDetailRequestModel;
        private readonly AskUserDetailModel _askUserDetailModel;
        private AskUserDetailModel _askUserDetailModelNull;
        private readonly AskUserDetailResponseModel _askUserDetailResponseModel;
        private readonly AskGetUserByIdRequest _askGetUserByIdRequest;
        private readonly AskGetUserByEmailRequest _askGetUserByEmailRequest;
        private readonly AskUserCreateRequestModel _askUserCreateRequestModel;
        private readonly BaseResponseModel _baseResponseModelTrue;
        private readonly BaseResponseModel _baseResponseModelFalse;
        private readonly AskDeleteUserByIdRequest _askDeleteUserByIdRequest;
        private readonly AskUserCreateModel _askUserCreateModel;
        public AskUserControllerUnitTest()
        {
            _mockLogManager = new Mock<ILogger<AskUserController>>();
            _mockUserService = new Mock<IAskUserService>();
            _mockMapper = new Mock<IMapper>();

            _askUserDetailRequestModel = new AskUserDetailRequestModel()
            {
                UserName = "test"
            };

            _askUserDetailModel = new AskUserDetailModel()
            {
                UserName = "test",
                Email = "test@test.com",
                Id = 1,
                IsActive = 1,
                Name = "test"
            };
            _askUserDetailResponseModel = new AskUserDetailResponseModel()
            {

            };
            _askGetUserByIdRequest = new AskGetUserByIdRequest()
            {
                Id = 1
            };
            _askGetUserByEmailRequest = new AskGetUserByEmailRequest()
            {
                Email = "test@test.com"
            };
            _askUserCreateRequestModel = new AskUserCreateRequestModel()
            {
                Email = "test@test.com",
                IsActive = 1,
                Name = "test",
                Password = "123",
                Surname = "cil",
                UserName = "onur"
            };
            _baseResponseModelTrue = new BaseResponseModel()
            {
                IsSucceed = true,
                Message = "success",
                Code = "",
                Data = ""
            };
            _baseResponseModelFalse = new BaseResponseModel()
            {
                IsSucceed = false,
                Message = "success",
                Code = "",
                Data = ""
            };
            _askDeleteUserByIdRequest = new AskDeleteUserByIdRequest()
            {
                Id = 1
            };
            _askUserCreateModel = new AskUserCreateModel()
            {
                Email = "test@test.com",
                IsActive = 1,
                Name = "test",
                Password = "123",
                Surname = "cil",
                UserName = "onur",
                Id = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };
        }

        public void Dispose()
        {

        }

        [Fact]
        public void Test_GetUser()
        {
            _mockUserService.Setup(x => x.GetUser(_askUserDetailRequestModel.UserName)).Returns(_askUserDetailModel);
            _mockMapper.Setup(x => x.Map<AskUserDetailModel, AskUserDetailResponseModel>(_askUserDetailModel)).Returns(_askUserDetailResponseModel);
            var userController = new AskUserController(_mockLogManager.Object, _mockMapper.Object,_mockUserService.Object);
            var actual = userController.GetUser(_askUserDetailRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Test_GetUser_When_User_Null()
        {
            _askUserDetailModelNull = new AskUserDetailModel();
            _askUserDetailModelNull = null;
            _mockUserService.Setup(x => x.GetUser(_askUserDetailRequestModel.UserName)).Returns(_askUserDetailModelNull);
            _mockMapper.Setup(x => x.Map<AskUserDetailModel, AskUserDetailResponseModel>(_askUserDetailModel)).Returns(_askUserDetailResponseModel);
            var userController = new AskUserController(_mockLogManager.Object, _mockMapper.Object, _mockUserService.Object);
            var actual = userController.GetUser(_askUserDetailRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Test_GetUserById()
        {
            _mockUserService.Setup(x => x.GetUserById(_askGetUserByIdRequest.Id)).Returns(_askUserDetailModel);
            _mockMapper.Setup(x => x.Map<AskUserDetailModel, AskUserDetailResponseModel>(_askUserDetailModel)).Returns(_askUserDetailResponseModel);
            var userController =
                new AskUserController(_mockLogManager.Object, _mockMapper.Object, _mockUserService.Object);
            var actual = userController.GetUserById(_askGetUserByIdRequest);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Test_GetUserById_When_User_Null()
        {
            _askUserDetailModelNull = new AskUserDetailModel();
            _askUserDetailModelNull = null;
            _mockUserService.Setup(x => x.GetUserById(_askGetUserByIdRequest.Id)).Returns(_askUserDetailModelNull);
            _mockMapper.Setup(x => x.Map<AskUserDetailModel, AskUserDetailResponseModel>(_askUserDetailModel)).Returns(_askUserDetailResponseModel);
            var userController =
                new AskUserController(_mockLogManager.Object, _mockMapper.Object, _mockUserService.Object);
            var actual = userController.GetUserById(_askGetUserByIdRequest);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Test_GetUserByEmail()
        {
            _mockUserService.Setup(x => x.GetUserByEmail(_askGetUserByEmailRequest.Email)).Returns(_askUserDetailModel);
            _mockMapper.Setup(x => x.Map<AskUserDetailModel, AskUserDetailResponseModel>(_askUserDetailModel)).Returns(_askUserDetailResponseModel);
            var userController =
                new AskUserController(_mockLogManager.Object, _mockMapper.Object, _mockUserService.Object);
            var actual = userController.GetUserByEmail(_askGetUserByEmailRequest);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Test_GetUserByEmail_When_User_Null()
        {
            _askUserDetailModelNull = new AskUserDetailModel();
            _askUserDetailModelNull = null;
            _mockUserService.Setup(x => x.GetUserByEmail(_askGetUserByEmailRequest.Email)).Returns(_askUserDetailModelNull);
            _mockMapper.Setup(x => x.Map<AskUserDetailModel, AskUserDetailResponseModel>(_askUserDetailModel)).Returns(_askUserDetailResponseModel);
            var userController =
                new AskUserController(_mockLogManager.Object, _mockMapper.Object, _mockUserService.Object);
            var actual = userController.GetUserByEmail(_askGetUserByEmailRequest);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Test_CreateUser()
        {
            _mockMapper.Setup(x => x.Map<AskUserCreateRequestModel, AskUserCreateModel>(_askUserCreateRequestModel)).Returns(_askUserCreateModel);
            _mockUserService.Setup(x => x.CheckIfUserExist(_askUserCreateRequestModel.UserName,_askUserCreateRequestModel.Email)).Returns(_baseResponseModelFalse);
            _mockUserService.Setup(x => x.CreateUser(_askUserCreateModel)).Returns(1);
            var userController =
                new AskUserController(_mockLogManager.Object, _mockMapper.Object, _mockUserService.Object);
            var actual = userController.CreateUser(_askUserCreateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void Test_CreateUser_Is_Exist()
        {
            _mockMapper.Setup(x => x.Map<AskUserCreateRequestModel, AskUserCreateModel>(_askUserCreateRequestModel)).Returns(_askUserCreateModel);
            _mockUserService.Setup(x => x.CheckIfUserExist(_askUserCreateRequestModel.UserName, _askUserCreateRequestModel.Email)).Returns(_baseResponseModelTrue);
            _mockUserService.Setup(x => x.CreateUser(_askUserCreateModel)).Returns(1);
            var userController =
                new AskUserController(_mockLogManager.Object, _mockMapper.Object, _mockUserService.Object);
            var actual = userController.CreateUser(_askUserCreateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Test_DeleteUser()
        {
            _mockUserService.Setup(x => x.DeleteUserById(_askDeleteUserByIdRequest.Id))
                .Callback((int id) => {
                    //do something
                }).Verifiable();
            var userController =
                new AskUserController(_mockLogManager.Object, _mockMapper.Object, _mockUserService.Object);
            var actual = userController.DeleteUserById(_askDeleteUserByIdRequest);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);

        }
    }
}
