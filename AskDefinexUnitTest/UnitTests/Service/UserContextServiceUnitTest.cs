using AskDefinex.Business.Model;
using AskDefinex.Business.Service;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using AutoMapper;
using DefineXwork.Library.Security.Common;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace AskDefinexUnitTest.UnitTests.Service
{
    public class UserContextServiceUnitTest
    {
        private readonly Mock<ILogger<UserContextService>> _logManager;
        private readonly Mock<IUserContextDAO> _userContextService;
        private readonly Mock<IMapper> _mapper;
        private readonly UserContextUserDAOModel _userContextDaoModel;
        private readonly UserContextModel _userContextModel;
        public UserContextServiceUnitTest()
        {
            _userContextDaoModel = new UserContextUserDAOModel()
            {
                Id = 1,
                IsActive = true,
                Name = "test",
                Roles = null,
                Surname = "test",
                UserName = "test",
                UserType = "test",
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _userContextModel = new UserContextModel()
            {
                IsActive = true,
                UserName = "test",
                Roles = null,
                UserId = 1
            };

            _logManager = new Mock<ILogger<UserContextService>>();
            _userContextService = new Mock<IUserContextDAO>();
            _mapper = new Mock<IMapper>();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void GetUser()
        {
            _userContextService.Setup(x => x.GetUser("test")).Returns(_userContextDaoModel);
            _mapper.Setup(x => x.Map<UserContextUserDAOModel, UserContextModel>(_userContextDaoModel)).Returns(_userContextModel);
            _mapper.Setup(x => x.Map<UserContextUserDAOModel, UserContextUserDetailModel>(_userContextDaoModel));
            var userContextService = new UserContextService(_logManager.Object, _userContextService.Object, _mapper.Object);
            var actual = userContextService.GetUser("test");
            Assert.Equal(_userContextModel, actual);
        }

        [Fact]
        public void GetUser_When_Occurred_Exception()
        {
            _userContextService.Setup(x => x.GetUser("test")).Throws(new Exception());
            var userContextService = new UserContextService(_logManager.Object, _userContextService.Object, _mapper.Object);
            Assert.Throws<Exception>(() => userContextService.GetUser("test"));
        }

        [Fact]
        public void GetUser_When_Dao_Null()
        {
            _userContextService.Setup(x => x.GetUser("test")).Returns((UserContextUserDAOModel)null);
            var userContextService = new UserContextService(_logManager.Object, _userContextService.Object, _mapper.Object);
            var actual = userContextService.GetUser("test");
            Assert.Null(actual);
        }
    }
}
