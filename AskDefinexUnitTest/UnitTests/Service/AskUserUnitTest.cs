using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskUserModule;
using AskDefinex.Business.Service;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using AutoMapper;
using DefineXwork.Library.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AskDefinexUnitTest
{
    public class AskUserUnitTest : IDisposable
    {
        private AskUserDAOModel _daoModel;
        private AskUserDAOModel _daoModel2;
        private List<AskUserDAOModel> _daoModelList;
        private AskUserCreateModel _createModel;
        private AskUserDetailModel _detailModel;
        private BaseResponseModel _baseResponseModel;
        Mock<IAskUserDAO> _mockDaoService;
        Mock<IConfigManager> _configManager;
        Mock<IMapper> _mapper;
        Mock<ILogger<AskUserService>> _logManager;
        public AskUserUnitTest()
        {
            _daoModelList = new List<AskUserDAOModel>()
            {
                new AskUserDAOModel
                {
                    Id = 1,
                    Email = "test@test.com",
                    Name = "test",
                    Surname = "test",
                    UserName = "test",
                    Password = "test",
                    IsActive = 1,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                },
                new AskUserDAOModel
                {
                    Id = 1,
                    Email = "test@test.com",
                    Name = "test",
                    Surname = "test",
                    UserName = "test",
                    Password = "test",
                    IsActive = 1,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                }
            };

            _daoModel = new AskUserDAOModel()
            {
                Id = 1,
                Email = "test@test.com",
                Name = "test",
                Surname = "test",
                UserName = "test",
                Password = "test",
                IsActive = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _createModel = new AskUserCreateModel()
            {
                Id = 1,
                Name = "test",
                Surname = "test",
                UserName = "test",
                Password = "",
                IsActive = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _detailModel = new AskUserDetailModel()
            {
                Id = 1,
                Name = "test",
                Surname = "test",
                UserName = "test",
                Password = "test",
                IsActive = 1
            };

            _baseResponseModel = new BaseResponseModel()
            {
                Code = "10102",
                Data = _detailModel,
                IsSucceed = true,
                Message = null
            };

            _mockDaoService = new Mock<IAskUserDAO>();
            _mapper = new Mock<IMapper>();
            _configManager = new Mock<IConfigManager>();
            _logManager = new Mock<ILogger<AskUserService>>();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void GetUser()
        {
            _mockDaoService.Setup(x => x.GetUser("test")).Returns(_daoModel);
            _mapper.Setup(x => x.Map<AskUserDAOModel, AskUserDetailModel>(_daoModel)).Returns(_detailModel);
            var UserService = new AskUserService(_logManager.Object,_mockDaoService.Object, _configManager.Object, _mapper.Object);
            var actual = UserService.GetUser("test");
            Assert.Equal(_detailModel, actual);
        }

        [Fact]
        public void GetUserById()
        {
            _mockDaoService.Setup(x => x.GetUserById(1)).Returns(_daoModel);
            _mapper.Setup(x => x.Map<AskUserDAOModel, AskUserDetailModel>(_daoModel)).Returns(_detailModel);
            var UserService = new AskUserService(_logManager.Object,_mockDaoService.Object, _configManager.Object, _mapper.Object);
            var actual = UserService.GetUserById(1);
            Assert.Equal(_detailModel, actual);
        }

        [Fact]
        public void GetUserByEmail()
        {
            _mockDaoService.Setup(x => x.GetUserByEmail("test@test.com")).Returns(_daoModel);
            _mapper.Setup(x => x.Map<AskUserDAOModel, AskUserDetailModel>(_daoModel)).Returns(_detailModel);
            var UserService = new AskUserService(_logManager.Object, _mockDaoService.Object, _configManager.Object, _mapper.Object);
            var actual = UserService.GetUserByEmail("test@test.com");
            Assert.Equal(_detailModel, actual);
        }

        [Fact]
        public void CreateUser()
        {
            _mockDaoService.Setup(x => x.CreateUser(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<AskUserCreateModel, AskUserDAOModel>(_createModel)).Returns(_daoModel);
            var UserService = new AskUserService(_logManager.Object, _mockDaoService.Object, _configManager.Object, _mapper.Object);
            var actual = UserService.CreateUser(_createModel);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void DeleteUserById()
        {
            _mockDaoService.Setup(x => x.DeleteUserById(1));
            var UserService = new AskUserService(_logManager.Object, _mockDaoService.Object, _configManager.Object, _mapper.Object);
            UserService.DeleteUserById(1);
            _mockDaoService.Verify(x => x.DeleteUserById(1));
        }

        [Fact]
        public void CheckIfUserExist()
        {
            _mockDaoService.Setup(x => x.GetUserWithUserNameAndEmail("test", "test@test.com")).Returns(_daoModel);
            _mapper.Setup(x => x.Map<AskUserDAOModel, AskUserDetailModel>(_daoModel)).Returns(_detailModel);
            var UserService = new AskUserService(_logManager.Object, _mockDaoService.Object, _configManager.Object, _mapper.Object);
            var actual = UserService.CheckIfUserExist("test", "test@test.com");
            Assert.Equal(_baseResponseModel.Data, actual.Data);
        }
        [Fact]
        public void CheckIfUserExist_DAO_Null()
        {
            _daoModel2 = new AskUserDAOModel();
            _daoModel2 = null;
            _mockDaoService.Setup(x => x.GetUserWithUserNameAndEmail("test", "test@test.com")).Returns(_daoModel2);
            _mapper.Setup(x => x.Map<AskUserDAOModel, AskUserDetailModel>(_daoModel)).Returns(_detailModel);
            var UserService = new AskUserService(_logManager.Object, _mockDaoService.Object, _configManager.Object, _mapper.Object);
            var actual = UserService.CheckIfUserExist("test", "test@test.com");
            Assert.Null(actual.Data);
        }
    }
}
