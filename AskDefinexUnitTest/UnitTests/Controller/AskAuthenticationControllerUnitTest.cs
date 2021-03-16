using System;
using AskDefinex.Business.Model;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Controller;
using AskDefinex.Rest.Model.Request;
using AskDefinex.Rest.Model.Request.Auth;
using AskDefinex.Rest.Model.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AskDefinexUnitTest.UnitTests.Controller
{
    public class AskAuthenticationControllerUnitTest
    {
        private readonly Mock<ILogger<AskAuthenticationController>> _logManager;
        private readonly Mock<IAskAuthenticationService> _authenticationService;
        private readonly Mock<IAskUserService> _userService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IRecaptchaValidatorService> _recaptchaValidatorService;
        private readonly AskLoginRequestModel _askLoginRequestModel;
        private readonly RefreshTokenRequestModel _refreshTokenRequestModel;
        private readonly AskRegistrationRequestModel _askRegistrationRequestModel;
        private readonly AskRegistrationModel _askRegistrationModel;
        private readonly AskLoginModel _askLoginModel;
        private  AskLoginModel _askLoginModelNull;
        private readonly BaseResponseModel _baseResponseModel;
        private readonly BaseResponseModel _baseResponseModelNull;
        public AskAuthenticationControllerUnitTest()
        {
            _askLoginModel = new AskLoginModel()
            {
                Id = 1,
                Name = "test",
                Email = "test@test.com",
                Surname = "test",
                UserName = "test",
                AccessToken = "1",
                RefreshToken = "1"
            };

            _askLoginRequestModel = new AskLoginRequestModel()
            {
                UserName = "test",
                Email = "test@test.com",
                Password = "1",
                RecaptchaToken = "1"
            };

            _refreshTokenRequestModel = new RefreshTokenRequestModel()
            {
                AccessToken = "1",
                RefreshToken = "1"
            };

            _askRegistrationRequestModel = new AskRegistrationRequestModel()
            {
                Email = "test@test.com",
                Name = "test",
                Password = "1",
                Surname = "test",
                UserName = "test"
            };

            _baseResponseModel = new BaseResponseModel()
            {
                Code = "1",
                Data = "1",
                IsSucceed = true,
                Message = ""
            };
            _baseResponseModelNull = new BaseResponseModel()
            {
                Code = "1",
                Data = "1",
                IsSucceed = false,
                Message = ""
            };
            _askRegistrationModel = new AskRegistrationModel()
            {
                CreateDate = new DateTime(),
                CreateUser = "test",
                Email = "test@test.com",
                Id = 1,
                IsActive = 1,
                LastUpdateDate = new DateTime(),
                LastUpdateUser = "test",
                Name = "test",
                Password = "123",
                Surname = "test",
                UserName = "test"
            };

            _userService = new Mock<IAskUserService>();
            _logManager = new Mock<ILogger<AskAuthenticationController>>();
            _authenticationService = new Mock<IAskAuthenticationService>();
            _mapper = new Mock<IMapper>();
            _recaptchaValidatorService = new Mock<IRecaptchaValidatorService>();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void Login_When_Recaptcha_Invalid()
        {
            _authenticationService.Setup(x => x.AskLogin("test", "test", "1")).Returns(_askLoginModel);
            _recaptchaValidatorService.Setup(x => x.IsRecaptchaValid("test")).Returns(false);
            _mapper.Setup(x => x.Map<AskLoginModel, AskLoginResponseModel>(_askLoginModel));
            var authenticationController = new AskAuthenticationController(_logManager.Object, _authenticationService.Object, _mapper.Object, _userService.Object, _recaptchaValidatorService.Object);
            var actual = authenticationController.Login(_askLoginRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Login()
        {
            _authenticationService.Setup(x => x.AskLogin("test","test","1")).Returns(_askLoginModel);
            _recaptchaValidatorService.Setup(x => x.IsRecaptchaValid("test")).Returns(true);
            _mapper.Setup(x => x.Map<AskLoginModel, AskLoginResponseModel>(_askLoginModel));
            var authenticationController = new AskAuthenticationController(_logManager.Object, _authenticationService.Object, _mapper.Object, _userService.Object, _recaptchaValidatorService.Object);
            var actual = authenticationController.Login(_askLoginRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Login_When_User_Null()
        {
            _askLoginModelNull = new AskLoginModel();
            _askLoginModelNull = null;
            _authenticationService.Setup(x => x.AskLogin("test", "test", "1")).Returns(_askLoginModelNull);
            _mapper.Setup(x => x.Map<AskLoginModel, AskLoginResponseModel>(_askLoginModel));
            var authenticationController = new AskAuthenticationController(_logManager.Object, _authenticationService.Object, _mapper.Object, _userService.Object, _recaptchaValidatorService.Object);
            var actual = authenticationController.Login(_askLoginRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Logout()
        {
            _authenticationService.Setup(x => x.LogOut());
            var authenticationController = new AskAuthenticationController(_logManager.Object, _authenticationService.Object, _mapper.Object, _userService.Object, _recaptchaValidatorService.Object);
            var actual = authenticationController.Logout();
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void RefreshAccessToken()
        {
            _authenticationService.Setup(x => x.LoginByRefreshToken("1","1")).Returns(_askLoginModel);
            _mapper.Setup(x => x.Map<AskLoginModel, AskLoginResponseModel>(_askLoginModel));
            var authenticationController = new AskAuthenticationController(_logManager.Object, _authenticationService.Object, _mapper.Object, _userService.Object, _recaptchaValidatorService.Object);
            var actual = authenticationController.RefreshAccessToken(_refreshTokenRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void RefreshAccessToken_When_User_Null()
        {
            _askLoginModelNull = new AskLoginModel();
            _askLoginModelNull = null;
            _authenticationService.Setup(x => x.LoginByRefreshToken("1", "1")).Returns(_askLoginModelNull);
            _mapper.Setup(x => x.Map<AskLoginModel, AskLoginResponseModel>(_askLoginModel));
            var authenticationController = new AskAuthenticationController(_logManager.Object, _authenticationService.Object, _mapper.Object, _userService.Object, _recaptchaValidatorService.Object);
            var actual = authenticationController.RefreshAccessToken(_refreshTokenRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Register()
        {
            _userService.Setup(x => x.CheckIfUserExist("test", "test@test.com")).Returns(_baseResponseModel);
            _mapper.Setup(x => x.Map<AskRegistrationRequestModel, AskRegistrationModel>(_askRegistrationRequestModel));
            _authenticationService.Setup(x => x.Register(_askRegistrationModel)).Returns(_askLoginModel);
            var authenticationController = new AskAuthenticationController(_logManager.Object, _authenticationService.Object, _mapper.Object, _userService.Object, _recaptchaValidatorService.Object);
            var actual = authenticationController.Register(_askRegistrationRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Register_When_User_Null()
        {
            _userService.Setup(x => x.CheckIfUserExist("test", "test@test.com")).Returns(_baseResponseModelNull);
            _mapper.Setup(x => x.Map<AskRegistrationRequestModel, AskRegistrationModel>(_askRegistrationRequestModel));
            _authenticationService.Setup(x => x.Register(_askRegistrationModel)).Returns(_askLoginModel);
            var authenticationController = new AskAuthenticationController(_logManager.Object, _authenticationService.Object, _mapper.Object, _userService.Object, _recaptchaValidatorService.Object);
            var actual = authenticationController.Register(_askRegistrationRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
    }
}
