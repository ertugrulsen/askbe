using AskDefinex.Business.Model;
using AskDefinex.Business.Service;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using AutoMapper;
using DefineXwork.Library.Configuration;
using DefineXwork.Library.Security;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AskDefinexUnitTest
{
    public class AskTagUnitTest : IDisposable
    {
        private AskTagDAOModel _daoModel;
        private List<AskTagDAOModel> _daoModelList;
        private TagUpdateModel _updateModel;
        private TagCreateModel _createModel;
        Mock<IAskTagDAO> _mockDaoService;
        Mock<IUserContextManager<IUserContextModel>> _userContextManager;
        Mock<IMapper> _mapper;
        Mock<ILogger<AskTagService>> _logManager;

        public AskTagUnitTest()
        {
            _daoModelList = new List<AskTagDAOModel>()
            {
                new AskTagDAOModel
                {
                    Id = 1,
                    Name = "test",
                    Type = "1",
                    IsActive = true,
                    QuestionId = 1,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                },
                new AskTagDAOModel
                {
                    Id = 1,
                    Name = "test",
                    Type = "1",
                    IsActive = true,
                    QuestionId = 1,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                }
            };

            _daoModel = new AskTagDAOModel()
            {
                Id = 1,
                Name = "test",
                Type = "1",
                IsActive = true,
                QuestionId = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _updateModel = new TagUpdateModel()
            {
                Id = 1,
                Name = "test",
                Type = "1",
                QuestionId = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _createModel = new TagCreateModel()
            {
                Name = "test",
                Type = "1",
                IsActive = true,
                QuestionId = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _mockDaoService = new Mock<IAskTagDAO>();
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(x => x.GetUser().UserName).Returns("test");
            _userContextManager.Setup(x => x.GetUser().UserId).Returns(1);
            _mapper = new Mock<IMapper>();
            _logManager = new Mock<ILogger<AskTagService>>();

        }

        public void Dispose()
        {

        }

        [Fact]
        public void CreateTag()
        {
            _mockDaoService.Setup(x => x.CreateTag(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<TagCreateModel, AskTagDAOModel>(_createModel)).Returns(_daoModel);
            var tagService = new AskTagService(_logManager.Object,_mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = tagService.CreateTag(_createModel);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void UpdateTag()
        {
            _mockDaoService.Setup(x => x.UpdateTag(_daoModel));
            _mapper.Setup(x => x.Map<TagUpdateModel, AskTagDAOModel>(_updateModel)).Returns(_daoModel);
            var tagService = new AskTagService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            tagService.UpdateTag(_updateModel);
            _mockDaoService.Verify(x => x.UpdateTag(_daoModel));
        }

        [Fact]
        public void DeleteTag()
        {
            _mockDaoService.Setup(x => x.DeleteTag(_daoModel));
            _mapper.Setup(x => x.Map<TagUpdateModel, AskTagDAOModel>(_updateModel)).Returns(_daoModel);
            var tagService = new AskTagService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            tagService.DeleteTag(_updateModel);
            _mockDaoService.Verify(x => x.DeleteTag(_daoModel));
        }
    }
}
