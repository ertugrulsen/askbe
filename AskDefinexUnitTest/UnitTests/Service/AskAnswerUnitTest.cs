using AskDefinex.Business.Model.AskAnswerModule;
using AskDefinex.Business.Service;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using AutoMapper;
using DefineXwork.Library.Configuration;
using DefineXwork.Library.Security;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AskDefinexUnitTest
{
    public class AskAnswerControllerUnitTest : IDisposable
    {
        private AskAnswerDAOModel _daoModel;
        private List<AskAnswerDAOModel> _daoModelList;
        private AnswerUpdateModel _updateModel;
        private AnswerCreateModel _createModel;
        private AnswerDetailModel _detailModel;
        private List<AnswerDetailModel> _detailModelList;
        private AnswerDeleteModel _deleteModel;
        Mock<IAskAnswerDAO> _mockDaoService;
        Mock<IUserContextManager<IUserContextModel>> _userContextManager;
        Mock<IMapper> _mapper;
        Mock<ILogger<AskAnswerService>> _logManager;
        public AskAnswerControllerUnitTest()
        {
            _daoModelList = new List<AskAnswerDAOModel>()
            {
                new AskAnswerDAOModel
                {
                    Answer = "",
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    DownVotes = 0,
                    IsAccepted = false,
                    IsActive = true,
                    QuestionId = 1,
                    UserId = 1,
                    UpVotes = 0,
                    Id = 1,
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                },
                new AskAnswerDAOModel
                {
                    Answer = "",
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    DownVotes = 0,
                    IsAccepted = false,
                    IsActive = true,
                    QuestionId = 1,
                    UserId = 1,
                    UpVotes = 0,
                    Id = 2,
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                }
            };

            _daoModel = new AskAnswerDAOModel()
            {
                Answer = "",
                CreateDate = DateTime.Now,
                CreateUser = "test",
                DownVotes = 0,
                IsAccepted = false,
                IsActive = true,
                QuestionId = 1,
                UserId = 1,
                UpVotes = 0,
                Id = 1,
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _updateModel = new AnswerUpdateModel()
            {
                Answer = "",
                CreateDate = DateTime.Now,
                CreateUser = "test",
                DownVotes = 0,
                IsAccepted = false,
                IsActive = true,
                QuestionId = 1,
                UserId = 1,
                UpVotes = 0,
                Id = 1,
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _createModel = new AnswerCreateModel()
            {
                Answer = "",
                CreateDate = DateTime.Now,
                CreateUser = "test",
                DownVotes = 0,
                IsAccepted = false,
                IsActive = true,
                QuestionId = 1,
                UserId = 1,
                UpVotes = 0,
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _detailModel = new AnswerDetailModel()
            {
                Answer = "",
                CreateDate = DateTime.Now,
                CreateUser = "test",
                DownVotes = 0,
                IsAccepted = false,
                IsActive = true,
                QuestionId = 1,
                UserId = 1,
                UpVotes = 0,
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _detailModelList = new List<AnswerDetailModel>()
            {
                new AnswerDetailModel
                {
                    Answer = "",
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    DownVotes = 0,
                    IsAccepted = false,
                    IsActive = true,
                    QuestionId = 1,
                    UserId = 1,
                    UpVotes = 0,
                    Id = 1,
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                },
                new AnswerDetailModel
                {
                    Answer = "",
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    DownVotes = 0,
                    IsAccepted = false,
                    IsActive = true,
                    QuestionId = 1,
                    UserId = 1,
                    UpVotes = 0,
                    Id = 2,
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                }
            };

            _deleteModel = new AnswerDeleteModel()
            {
                Id = 1,
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _mockDaoService = new Mock<IAskAnswerDAO>();
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(x => x.GetUser().UserName).Returns("test");
            _userContextManager.Setup(x => x.GetUser().UserId).Returns(1);
            _mapper = new Mock<IMapper>();
            _logManager = new Mock<ILogger<AskAnswerService>>();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void AnswerUpdateDownVote()
        {
            _mockDaoService.Setup(x => x.GetAnswerById(_updateModel.Id)).Returns(_daoModel);
            _mockDaoService.Setup(x => x.AnswerUpdateDownVote(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<AnswerUpdateModel, AskAnswerDAOModel>(_updateModel)).Returns(_daoModel);
            var answerService = new AskAnswerService(_logManager.Object,_mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = answerService.AnswerUpdateDownVote(_updateModel);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void AnswerUpdateDownVote_When_Occurred_Exception()
        {
            _mockDaoService.Setup(x => x.GetAnswerById(_updateModel.Id)).Throws(new Exception());
            
            _mapper.Setup(x => x.Map<AnswerUpdateModel, AskAnswerDAOModel>(_updateModel)).Returns(_daoModel);
            var answerService = new AskAnswerService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            Assert.Throws<Exception>(() => answerService.AnswerUpdateDownVote(_updateModel));
        }

        [Fact]
        public void AnswerUpdateUpVote()
        {
            _mockDaoService.Setup(x => x.GetAnswerById(_updateModel.Id)).Returns(_daoModel);
            _mockDaoService.Setup(x => x.AnswerUpdateUpVote(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<AnswerUpdateModel, AskAnswerDAOModel>(_updateModel)).Returns(_daoModel);
            var answerService = new AskAnswerService(_logManager.Object,_mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = answerService.AnswerUpdateUpVote(_updateModel);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void AnswerUpdateUpVote_When_Occurred_Exception()
        {
            _mockDaoService.Setup(x => x.GetAnswerById(_updateModel.Id)).Throws(new Exception());

            _mapper.Setup(x => x.Map<AnswerUpdateModel, AskAnswerDAOModel>(_updateModel)).Returns(_daoModel);
            var answerService = new AskAnswerService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            Assert.Throws<Exception>(() => answerService.AnswerUpdateUpVote(_updateModel));
        }

        [Fact]
        public void CreateAnswer()
        {
            _mockDaoService.Setup(x => x.CreateAnswer(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<AnswerCreateModel, AskAnswerDAOModel>(_createModel)).Returns(_daoModel);
            var answerService = new AskAnswerService(_logManager.Object,_mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = answerService.CreateAnswer(_createModel);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void CreateAnswer_When_User_Null()
        {
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(m => m.GetUser()).Returns((IUserContextModel)null);

            var answerService = new AskAnswerService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            
            Assert.Throws<NullReferenceException>(() => answerService.CreateAnswer(_createModel));
        }

        [Fact]
        public void UpdateAnswer()
        {
            _mockDaoService.Setup(x => x.UpdateAnswer(_daoModel));
            _mapper.Setup(x => x.Map<AnswerUpdateModel, AskAnswerDAOModel>(_updateModel)).Returns(_daoModel);
            var answerService = new AskAnswerService(_logManager.Object,_mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            answerService.UpdateAnswer(_updateModel);
            _mockDaoService.Verify(x => x.UpdateAnswer(_daoModel));
        }

        [Fact]
        public void UpdateAnswer_When_User_Null()
        {
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(m => m.GetUser()).Returns((IUserContextModel)null);
            _mapper.Setup(x => x.Map<AnswerUpdateModel, AskAnswerDAOModel>(_updateModel)).Returns(_daoModel);
            var answerService = new AskAnswerService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);

            Assert.Throws<NullReferenceException>(() => answerService.UpdateAnswer(_updateModel));
        }

        [Fact]
        public void DeleteAnswer()
        {
            _mockDaoService.Setup(x => x.DeleteAnswer(_daoModel));
            _mapper.Setup(x => x.Map<AnswerDeleteModel, AskAnswerDAOModel>(_deleteModel)).Returns(_daoModel);
            var answerService = new AskAnswerService(_logManager.Object,_mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            answerService.DeleteAnswer(_deleteModel);
            _mockDaoService.Verify(x => x.DeleteAnswer(_daoModel));
        }

        [Fact]
        public void DeleteAnswer_When_User_Null()
        {
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(m => m.GetUser()).Returns((IUserContextModel)null);
            _mockDaoService.Setup(x => x.DeleteAnswer(_daoModel));
            _mapper.Setup(x => x.Map<AnswerDeleteModel, AskAnswerDAOModel>(_deleteModel)).Returns(_daoModel);
            var answerService = new AskAnswerService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);

            Assert.Throws<NullReferenceException>(() => answerService.DeleteAnswer(_deleteModel));
        }

        [Fact]
        public void GetAnswerById()
        {
            _mockDaoService.Setup(x => x.GetAnswerById(1)).Returns(_daoModel);
            _mapper.Setup(x => x.Map<AskAnswerDAOModel, AnswerDetailModel>(_daoModel)).Returns(_detailModel);
            var answerService = new AskAnswerService(_logManager.Object,_mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = answerService.GetAnswerById(1);
            Assert.Equal(_detailModel, actual);
        }
        [Fact]
        public void GetAnswerById_When_Occurred_Exception()
        {
            _mockDaoService.Setup(x => x.GetAnswerById(1)).Throws(new Exception());

            _mapper.Setup(x => x.Map<AskAnswerDAOModel, AnswerDetailModel>(_daoModel)).Returns(_detailModel);
            var answerService = new AskAnswerService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            Assert.Throws<Exception>(() => answerService.GetAnswerById(1));
        }

        [Fact]
        public void GetAnswersByQuestionId()
        {
            _mockDaoService.Setup(x => x.GetAnswersByQuestionId(1)).Returns(_daoModelList);
            _mapper.Setup(x => x.Map<List<AskAnswerDAOModel>, List<AnswerDetailModel>>(_daoModelList)).Returns(_detailModelList);
            var answerService = new AskAnswerService(_logManager.Object,_mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = answerService.GetAnswersByQuestionId(1);
            Assert.Equal(_detailModelList, actual);
        }
        [Fact]
        public void GetAnswersByQuestionId_When_Occurred_Exception()
        {
            _mockDaoService.Setup(x => x.GetAnswersByQuestionId(1)).Throws(new Exception());

            _mapper.Setup(x => x.Map<List<AskAnswerDAOModel>, List<AnswerDetailModel>>(_daoModelList)).Returns(_detailModelList);
            var answerService = new AskAnswerService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            Assert.Throws<Exception>(() => answerService.GetAnswersByQuestionId(1));
        }

    }
}
