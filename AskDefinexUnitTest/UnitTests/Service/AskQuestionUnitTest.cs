using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskQuestionModule;
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
using Nest;
using Xunit;
using Microsoft.Extensions.Logging;

namespace AskDefinexUnitTest
{
    public class AskQuestionUnitTest : IDisposable
    {
        Mock<IAskQuestionDAO> _mockDaoService;
        Mock<IUserContextManager<IUserContextModel>> _userContextManager;
        Mock<IMapper> _mapper;
        Mock<ILogger<AskQuestionService>> _logManager;
        private BaseResponseModel _baseResponseModel;
        private QuestionCountModel _questionCountModel;
        private AskQuestionDAOModel _daoModel;
        private QuestionCreateModel _createModel;
        private Mock<IElasticClient> _mockElasticClient;
        private QuestionDetailModel _detailModel;
        private List<QuestionDetailModel> _detailModelList;
        private QuestionSearchModel _searchModel;
        private QuestionDeleteModel _deleteModel;
        private AllQuestionForMainModel _allQuestionForMainModel;
        private List<AskQuestionDAOModel> _daoModelList;
        private QuestionUpdateModel _updateModel;
        public AskQuestionUnitTest()
        {
            _daoModelList = new List<AskQuestionDAOModel>()
            {
                new AskQuestionDAOModel
                {
                    Id = 1,
                    UserId = 1,
                    Header = "test",
                    Detail = "test",
                    AnswerList = null,
                    CommentList = null,
                    UpVotes = 0,
                    DownVotes = 0,
                    IsAccepted = true,
                    IsActive = true,
                    IsClose = false,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                },
                new AskQuestionDAOModel
                {
                    Id = 2,
                    UserId = 1,
                    Header = "test",
                    Detail = "test",
                    AnswerList = null,
                    CommentList = null,
                    UpVotes = 0,
                    DownVotes = 0,
                    IsAccepted = true,
                    IsActive = true,
                    IsClose = false,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                }
            };

            _daoModel = new AskQuestionDAOModel()
            {
                Id = 1,
                UserId = 1,
                Header = "test",
                Detail = "test",
                AnswerList = null,
                CommentList = null,
                UpVotes = 0,
                DownVotes = 0,
                IsAccepted = true,
                IsActive = true,
                IsClose = false,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _updateModel = new QuestionUpdateModel()
            {
                Id = 1,
                UserId = 1,
                Header = "test",
                Detail = "test",
                UpVotes = 0,
                DownVotes = 0,
                IsAccepted = true,
                IsActive = true,
                IsClose = false,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _createModel = new QuestionCreateModel()
            {
                UserId = 1,
                Header = "test",
                Detail = "test",
                UpVotes = 0,
                DownVotes = 0,
                IsAccepted = true,
                IsActive = true,
                IsClose = false,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _detailModel = new QuestionDetailModel()
            {
                Id = 1,
                UserId = 1,
                Header = "test",
                Detail = "test",
                AnswerList = null,
                CommentList = null,
                UpVotes = 0,
                DownVotes = 0,
                IsAccepted = true,
                IsActive = true,
                IsClose = false,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _detailModelList = new List<QuestionDetailModel>()
            {
                new QuestionDetailModel
                {
                    Id = 1,
                    UserId = 1,
                    Header = "test",
                    Detail = "test",
                    AnswerList = null,
                    CommentList = null,
                    UpVotes = 0,
                    DownVotes = 0,
                    IsAccepted = true,
                    IsActive = true,
                    IsClose = false,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                },
                new QuestionDetailModel
                {
                    Id = 1,
                    UserId = 1,
                    Header = "test",
                    Detail = "test",
                    AnswerList = null,
                    CommentList = null,
                    UpVotes = 0,
                    DownVotes = 0,
                    IsAccepted = true,
                    IsActive = true,
                    IsClose = false,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                },
            };

                _deleteModel = new QuestionDeleteModel()
            {
                Id = 1,
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _baseResponseModel = new BaseResponseModel()
            {
                Code = "10001",
                Data = _detailModel,
                IsSucceed = true,
                Message = null
            };

            _questionCountModel = new QuestionCountModel()
            {
                Count = 1
            };
            _searchModel = new QuestionSearchModel()
            {

                Id = 1,
                UserId = 1,
                Header = "test",
                Detail = "test",
                UpVotes = 0,
                DownVotes = 0,
                IsAccepted = true,
                IsActive = true,
                IsClose = false,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test",
                UserName = "test"

            };

            _allQuestionForMainModel = new AllQuestionForMainModel()
            {
                Count = 1,
                StartOffSet = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test",
            };

            _mockDaoService = new Mock<IAskQuestionDAO>();
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(x => x.GetUser().UserName).Returns("test");
            _userContextManager.Setup(x => x.GetUser().UserId).Returns(1);
            _mapper = new Mock<IMapper>();
            _mockElasticClient = new Mock<IElasticClient>();
            _logManager = new Mock<ILogger<AskQuestionService>>();

        }

        public void Dispose()
        {

        }

        [Fact]
        public void CreateQuestion()
        {
            _mockDaoService.Setup(x => x.CreateQuestion(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<QuestionCreateModel, AskQuestionDAOModel>(_createModel)).Returns(_daoModel);
            _mapper.Setup(x => x.Map<QuestionCreateModel, QuestionSearchModel>(_createModel)).Returns(_searchModel);
            var questionService = new AskQuestionService(_logManager.Object,_mockDaoService.Object, _userContextManager.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionService.CreateQuestion(_createModel);
            Assert.Equal(1, actual.Result);
        }

        [Fact]
        public void CheckIfQuestionExist()
        {
            _mockDaoService.Setup(x => x.GetQuestionByHeader("test")).Returns(_daoModel);
            _mapper.Setup(x => x.Map<AskQuestionDAOModel, QuestionDetailModel>(_daoModel)).Returns(_detailModel);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object,_mockElasticClient.Object);
            var actual = questionService.CheckIfQuestionExist("test");
            Assert.Equal(_baseResponseModel.Data, actual.Data);
        }

        [Fact]
        public void UpdateQuestion()
        {
            _mockDaoService.Setup(x => x.UpdateQuestion(_daoModel));
            _mapper.Setup(x => x.Map<QuestionUpdateModel, AskQuestionDAOModel>(_updateModel)).Returns(_daoModel);
            _mapper.Setup(x => x.Map<QuestionUpdateModel, QuestionSearchModel>(_updateModel)).Returns(_searchModel);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object,_mockElasticClient.Object);
            questionService.UpdateQuestion(_updateModel);
            _mockDaoService.Verify(x => x.UpdateQuestion(_daoModel));
        }

        [Fact]
        public void DeleteQuestion()
        {
            _mockDaoService.Setup(x => x.DeleteQuestion(_daoModel));
            _mapper.Setup(x => x.Map<QuestionDeleteModel, AskQuestionDAOModel>(_deleteModel)).Returns(_daoModel);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object, _mockElasticClient.Object);
            questionService.DeleteQuestion(_deleteModel);
            _mockDaoService.Verify(x => x.DeleteQuestion(_daoModel));
        }

        [Fact]
        public void GetAllQuestions()
        {
            _mockDaoService.Setup(x => x.GetAllQuestions()).Returns(_daoModelList);
            _mapper.Setup(x => x.Map<List<AskQuestionDAOModel>, List<QuestionDetailModel>>(_daoModelList)).Returns(_detailModelList);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionService.GetAllQuestions();
            Assert.Equal(_detailModelList, actual);
        }

        [Fact]
        public void GetQuestionById()
        {
            _mockDaoService.Setup(x => x.GetQuestionById(1)).Returns(_daoModel);
            _mapper.Setup(x => x.Map<AskQuestionDAOModel, QuestionDetailModel>(_daoModel)).Returns(_detailModel);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionService.GetQuestionById(1);
            Assert.Equal(_detailModel, actual);
        }

        [Fact]
        public void QuestionUpdateUpVote()
        {
            _mockDaoService.Setup(x => x.GetQuestionById(1)).Returns(_daoModel);
            _mockDaoService.Setup(x => x.QuestionUpdateUpVote(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<QuestionUpdateModel, AskQuestionDAOModel>(_updateModel)).Returns(_daoModel);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionService.QuestionUpdateUpVote(_updateModel);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void QuestionUpdateDownVote()
        {
            _mockDaoService.Setup(x => x.GetQuestionById(1)).Returns(_daoModel);
            _mockDaoService.Setup(x => x.QuestionUpdateDownVote(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<QuestionUpdateModel, AskQuestionDAOModel>(_updateModel)).Returns(_daoModel);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionService.QuestionUpdateDownVote(_updateModel);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void QuestionIsClosedById()
        {
            _mockDaoService.Setup(x => x.QuestionIsClosedById(_daoModel));
            _mapper.Setup(x => x.Map<QuestionUpdateModel, AskQuestionDAOModel>(_updateModel)).Returns(_daoModel);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object, _mockElasticClient.Object);
            questionService.QuestionIsClosedById(_updateModel);
            _mockDaoService.Verify(x => x.QuestionIsClosedById(_daoModel));
        }

        [Fact]
        public void GetAllQuestionsForMain()
        {
            _mockDaoService.Setup(x => x.GetAllQuestionsForMain(_allQuestionForMainModel)).Returns(_daoModelList);
            _mapper.Setup(x => x.Map<List<AskQuestionDAOModel>, List<QuestionDetailModel>>(_daoModelList)).Returns(_detailModelList);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionService.GetAllQuestionsForMain(_allQuestionForMainModel);
            Assert.Equal(_detailModelList, actual);
        }

        [Fact]
        public void GetQuestionWithAnswers()
        {
            _mockDaoService.Setup(x => x.GetQuestionWithAnswers(_detailModel)).Returns(_daoModel);
            _mapper.Setup(x => x.Map<AskQuestionDAOModel, QuestionDetailModel>(_daoModel)).Returns(_detailModel);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionService.GetQuestionWithAnswers(_detailModel);
            Assert.Equal(_detailModel, actual);
        }

        [Fact]
        public void GetUnansweredQuestions()
        {
            _mockDaoService.Setup(x => x.GetUnansweredQuestions(_detailModel)).Returns(_daoModelList);
            _mapper.Setup(x => x.Map<List<AskQuestionDAOModel>, List<QuestionDetailModel>>(_daoModelList)).Returns(_detailModelList);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionService.GetUnansweredQuestions(_detailModel);
            Assert.Equal(_detailModelList, actual);
        }

        [Fact]
        public void GetMostUpVotedQuestions()
        {
            _mockDaoService.Setup(x => x.GetMostUpVotedQuestions(_detailModel)).Returns(_daoModelList);
            _mapper.Setup(x => x.Map<List<AskQuestionDAOModel>, List<QuestionDetailModel>>(_daoModelList)).Returns(_detailModelList);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionService.GetMostUpVotedQuestions(_detailModel);
            Assert.Equal(_detailModelList, actual);
        }

        [Fact]
        public void GetQuestionCountByUserId()
        {
            _mockDaoService.Setup(x => x.GetQuestionCountByUserId(1)).Returns(1);
            var questionService = new AskQuestionService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionService.GetQuestionCountByUserId(1);
            Assert.Equal(_questionCountModel.Count, actual.Count);
        }
    }
}
