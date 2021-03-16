using AskDefinex.Business.Model;
using AskDefinex.Business.Service;
using AskDefinex.Business.Service.Interface;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using AutoMapper;
using DefineXwork.Library.Configuration;
using DefineXwork.Library.Security;
using DefineXwork.Library.Security.Jwt;
using Moq;
using System;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.DataAccess.Manager;
using Xunit;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AskDefinexUnitTest
{
    public class AskAuthenticationUnitTest : IDisposable
    {
        private AskLoginModel _loginModel;
        private AskLoginDAOModel _daoModel;
        private AskLoginDAOModel _daoModelNull;
        Mock<IAskAuthenticationDAO> _mockDaoService;
        Mock<IAskUserService> _mockUserService;
        Mock<IUserContextManager<IUserContextModel>> _userContextManager;
        Mock<IConfigManager> _configManager;
        Mock<IMapper> _mapper;
        Mock<IJwtTokenHandler> _jwtTokenHandler;
        Mock<ILogger<AskAuthenticationService>> _logManager;
        private Mock<MysqlDatabaseManager> _mockDatabaseManager;
        private readonly AskRegistrationModel _askRegistrationModel;
        public AskAuthenticationUnitTest()
        {
            _loginModel = new AskLoginModel()
            {
                AccessToken = "1",
                Email = "test@test.com",
                Id = 1,
                Name = "test",
                RefreshToken = "1",
                Roles = null,
                Surname = "test",
                UserName = "test"
            };

            _daoModel = new AskLoginDAOModel()
            {
                Email = "test@test.com",
                Id = 1,
                Name = "test",
                RefreshToken = "2",
                Roles = null,
                Surname = "test",
                UserName = "test",
                RefreshTokenCreateDate = DateTime.Now
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
                Name = "onur",
                Password = "123",
                Surname = "cil",
                UserName = "root"
            };

            _mockDaoService = new Mock<IAskAuthenticationDAO>();
            _mockUserService = new Mock<IAskUserService>();
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(x => x.GetUser().UserName).Returns("test");
            _userContextManager.Setup(x => x.GetUser().UserId).Returns(1);
            _mapper = new Mock<IMapper>();
            _configManager = new Mock<IConfigManager>();
            _jwtTokenHandler = new Mock<IJwtTokenHandler>();
            _logManager = new Mock<ILogger<AskAuthenticationService>>();
            _mockDatabaseManager = new Mock<MysqlDatabaseManager>(_configManager);
        }

        public void Dispose()
        {

        }

        [Fact]
        public void AskLogin()
        {
            _mockDaoService.Setup(x => x.GetAskLoginUser(_loginModel.UserName, _loginModel.Email, "QeCpRI+R7bpLBcbC/A7bHWQYqikrWylCY3vsQ6KblSM=")).Returns(_daoModel);
            _mockDaoService.Setup(x => x.SetRefreshToken(_loginModel.UserName, "1", DateTime.Now));
            _mapper.Setup(x => x.Map<AskLoginDAOModel, AskLoginModel>(_daoModel)).Returns(_loginModel);
            var authenticationService = new AskAuthenticationService(_jwtTokenHandler.Object, _userContextManager.Object, _mockDaoService.Object,  _mapper.Object, _configManager.Object, _logManager.Object, _mockUserService.Object);
            var actual = authenticationService.AskLogin("test", "test@test.com", "1");
            Assert.Equal(_loginModel, actual);
        }
        [Fact]
        public void AskLogin_When_Occurred_Exception()
        {
            _mockDaoService.Setup(x => x.GetAskLoginUser(_loginModel.UserName, _loginModel.Email, "QeCpRI+R7bpLBcbC/A7bHWQYqikrWylCY3vsQ6KblSM=")).Throws(new Exception());
            _mockDaoService.Setup(x => x.SetRefreshToken(_loginModel.UserName, "1", DateTime.Now));
            _mapper.Setup(x => x.Map<AskLoginDAOModel, AskLoginModel>(_daoModel)).Returns(_loginModel);
            var authenticationService = new AskAuthenticationService(_jwtTokenHandler.Object, _userContextManager.Object, _mockDaoService.Object, _mapper.Object, _configManager.Object, _logManager.Object, _mockUserService.Object);
            Assert.Throws<Exception>(() => authenticationService.AskLogin("test", "test@test.com", "1"));
            
        }
        [Fact]
        public void AskLogin_When_User_Null()
        {
            _daoModelNull = new AskLoginDAOModel();
            _daoModelNull = null;
            _mockDaoService.Setup(x => x.GetAskLoginUser(_loginModel.UserName, _loginModel.Email, "QeCpRI+R7bpLBcbC/A7bHWQYqikrWylCY3vsQ6KblSM=")).Returns(_daoModelNull);
            _mockDaoService.Setup(x => x.SetRefreshToken(_loginModel.UserName, "1", DateTime.Now));
            _mapper.Setup(x => x.Map<AskLoginDAOModel, AskLoginModel>(_daoModel)).Returns(_loginModel);
            var authenticationService = new AskAuthenticationService(_jwtTokenHandler.Object, _userContextManager.Object, _mockDaoService.Object, _mapper.Object, _configManager.Object, _logManager.Object, _mockUserService.Object);
            var actual = authenticationService.AskLogin("test", "test@test.com", "1");
            Assert.Null(actual);
        }

        [Fact]
        public void LogOut()
        {
            _userContextManager.Setup(x => x.GetUser().UserName).Returns("test");
            _mockDaoService.Setup(x => x.RemoveRefreshToken("test"));
            var authenticationService = new AskAuthenticationService(_jwtTokenHandler.Object, _userContextManager.Object, _mockDaoService.Object,  _mapper.Object, _configManager.Object, _logManager.Object, _mockUserService.Object);
            authenticationService.LogOut();
            _userContextManager.Verify(x => x.DeleteUser());
        }
    }
}
