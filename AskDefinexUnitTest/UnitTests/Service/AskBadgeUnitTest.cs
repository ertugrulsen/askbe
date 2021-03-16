using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskBadgeModule;
using AskDefinex.Business.Service;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using AutoMapper;
using DefineXwork.Library.Configuration;
using DefineXwork.Library.Security;
using Moq;
using System;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AskDefinexUnitTest
{
    public class AskBadgeUnitTest : IDisposable
    {
        private AskBadgeDAOModel _daoModel;
        private BadgeUpdateModel _updateModel;
        private BadgeCreateModel _createModel;
        private BadgeDetailModel _detailModel;
        private BadgeDeleteModel _deleteModel;
        Mock<IAskBadgeDAO> _mockDaoService;
        Mock<IUserContextManager<IUserContextModel>> _userContextManager;
        Mock<IMapper> _mapper;
        Mock<ILogger<AskBadgeService>> _logManager;
        public AskBadgeUnitTest()
        {
            _daoModel = new AskBadgeDAOModel()
            {
                Id = 1,
                Name = "test",
                Type = "1",
                IsActive = true,
                UserId = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _updateModel = new BadgeUpdateModel()
            {
                Id = 1,
                Name = "test",
                Type = "1",
                IsActive = true,
                UserId = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _createModel = new BadgeCreateModel()
            {
                Id = 1,
                Name = "test",
                Type = "1",
                IsActive = true,
                UserId = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _detailModel = new BadgeDetailModel()
            {
                Id = 1,
                Name = "test",
                Type = "1",
                IsActive = true,
                UserId = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _deleteModel = new BadgeDeleteModel()
            {
                Id = 1,
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _mockDaoService = new Mock<IAskBadgeDAO>();
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(x => x.GetUser().UserName).Returns("test");
            _userContextManager.Setup(x => x.GetUser().UserId).Returns(1);
            _mapper = new Mock<IMapper>();
            _logManager = new Mock<ILogger<AskBadgeService>>();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void GetBadgeByUserId()
        {
            _mockDaoService.Setup(x => x.GetBadgeByUserId(1)).Returns(_daoModel);
            _mapper.Setup(x => x.Map<AskBadgeDAOModel, BadgeDetailModel>(_daoModel)).Returns(_detailModel);
            var badgeService = new AskBadgeService(_logManager.Object,_mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = badgeService.GetBadgeByUserId(1);
            Assert.Equal(_detailModel, actual);
        }
        [Fact]
        public void GetBadgeByUserId_When_Occurred_Exception()
        {
            _mockDaoService.Setup(x => x.GetBadgeByUserId(1)).Throws(new Exception());
            _mapper.Setup(x => x.Map<AskBadgeDAOModel, BadgeDetailModel>(_daoModel)).Returns(_detailModel);
            var badgeService = new AskBadgeService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
          
            Assert.Throws<Exception>(() => badgeService.GetBadgeByUserId(1));
        }

        [Fact]
        public void CreateBadge()
        {
            _mockDaoService.Setup(x => x.CreateBadge(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<BadgeCreateModel, AskBadgeDAOModel>(_createModel)).Returns(_daoModel);
            var badgeService = new AskBadgeService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = badgeService.CreateBadge(_createModel);
            Assert.Equal(1, actual);
        }
        [Fact]
        public void CreateBadge_When_User_Null()
        {
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(m => m.GetUser()).Returns((IUserContextModel)null);
            _mockDaoService.Setup(x => x.CreateBadge(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<BadgeCreateModel, AskBadgeDAOModel>(_createModel)).Returns(_daoModel);
            var badgeService = new AskBadgeService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            Assert.Throws<NullReferenceException>(() => badgeService.CreateBadge(_createModel));
        }

        [Fact]
        public void UpdateBadge()
        {
            _mockDaoService.Setup(x => x.UpdateBadge(_daoModel));
            _mapper.Setup(x => x.Map<BadgeUpdateModel, AskBadgeDAOModel>(_updateModel)).Returns(_daoModel);
            var badgeService = new AskBadgeService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            badgeService.UpdateBadge(_updateModel);
            _mockDaoService.Verify(x => x.UpdateBadge(_daoModel));
        }
        [Fact]
        public void UpdateBadge_When_User_Null()
        {
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(m => m.GetUser()).Returns((IUserContextModel)null);
            _mockDaoService.Setup(x => x.CreateBadge(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<BadgeCreateModel, AskBadgeDAOModel>(_createModel)).Returns(_daoModel);
            var badgeService = new AskBadgeService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            Assert.Throws<NullReferenceException>(() => badgeService.UpdateBadge(_updateModel));
        }

        [Fact]
        public void DeleteBadge()
        {
            _mockDaoService.Setup(x => x.DeleteBadge(_daoModel));
            _mapper.Setup(x => x.Map<BadgeDeleteModel, AskBadgeDAOModel>(_deleteModel)).Returns(_daoModel);
            var badgeService = new AskBadgeService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            badgeService.DeleteBadge(_deleteModel);
            _mockDaoService.Verify(x => x.DeleteBadge(_daoModel));
        }
    }
}
