using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskQuestionModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Controller;
using AskDefinex.Rest.Model.Request.AskQuestionModule;
using AskDefinex.Rest.Model.Response.AskQuestionModule;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AskDefinexUnitTest.UnitTests.Controller
{
    public class AskQuestionControllerUnitTest
    {
        private readonly Mock<ILogger<AskQuestionController>> _logManager;
        private readonly Mock<IAskQuestionService> _questionService;
        private readonly Mock<IMapper> _mapper;
        private readonly QuestionCreateRequestModel _questionCreateRequestModel;
        private readonly QuestionUpdateRequestModel _questionUpdateRequestModel;
        private readonly AllQuestionForMainRequestModel _questionAllQuestionForMainRequestModel;
        private readonly AllQuestionForMainModel _questionAllQuestionForMainModel;
        private readonly QuestionCreateModel _questionCreateModel;
        private readonly QuestionUpdateModel _questionUpdateModel;
        private readonly QuestionDeleteModel _questionDeleteModel;
        private readonly QuestionDetailModel _questionDetailModel;
        private readonly QuestionIdModel _questionIdModel;
        private readonly List<QuestionDetailModel> _questionDetailModelList;
        private readonly BaseResponseModel _questionBaseResponseModel;
        private readonly QuestionListByUserIdRequestModel _questionListByUserIdRequestModel;
        private readonly QuestionCountModel _questionCountModel;
        private readonly QuestionSearchModel _questionSearchModel;
        private readonly Mock<IElasticClient> _mockElasticClient;
        private readonly QuestionDeleteRequestModel _questionDeleteRequestModel;
        private readonly QuestionDetailRequestModel _questionDetailRequestModel;
        public AskQuestionControllerUnitTest()
        {
            _questionDeleteRequestModel = new QuestionDeleteRequestModel()
            {
                Id = 1,
                IsActive = true
            };

            _questionDetailRequestModel = new QuestionDetailRequestModel()
            {
                Header = "test",
                Detail = "test",
                UpVotes = 0,
                IsAccepted = true,
                IsActive = true,
                DownVotes = 0,
                Id = 1,
                Count = 1,
                IsClose = true,
                StartOffSet = 1,
                UserId = 1
            };

            _questionAllQuestionForMainRequestModel = new AllQuestionForMainRequestModel()
            {
                StartOffSet = 1,
                Count = 1,

            };
            _questionCreateRequestModel = new QuestionCreateRequestModel()
            {
                Header = "test",
                Detail = "test",
                UpVotes = 0,
                DownVotes = 0,
                IsAccepted = true,
                IsActive = true,

            };
            _questionUpdateRequestModel = new QuestionUpdateRequestModel()
            {
                Id = 1,
                Header = "test",
                Detail = "test",
                IsAccepted = true,
                IsActive = true,

            };

            _questionCreateModel = new QuestionCreateModel()
            {
                Header = "test",
                Detail = "test",
                UpVotes = 0,
                IsAccepted = true,
                IsActive = true,
                DownVotes = 0,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"


            };

            _questionBaseResponseModel = new BaseResponseModel()
            {
                IsSucceed = true,
                Code = "test",
                Message = "test",
                Data = "tes",


            };
            _questionUpdateModel = new QuestionUpdateModel()
            {
                Header = "test",
                Detail = "test",
                UpVotes = 0,
                IsAccepted = true,
                IsActive = true,
                DownVotes = 0,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"


            };
            _questionDeleteModel = new QuestionDeleteModel()
            {
                Id = 1,
                IsActive = true,


            };
            _questionIdModel = new QuestionIdModel()
            {
                Id = 1

            };
            _questionDetailModel = new QuestionDetailModel()
            {
                Id = 1,
                Header = "test",
                Detail = "test",
                UpVotes = 1,
                DownVotes = 1,
                IsAccepted = true,
                IsActive = true,
                IsClose = true,
                StartOffSet = 1,
                Count = 10,
                TotalCount = 10,
                AnswerList = null,
                CommentList = null,



            };
            _questionDetailModelList = new List<QuestionDetailModel>()
            {
                new QuestionDetailModel
                {
                    Id = 1,
                    Header = "test",
                    Detail = "test",
                    UpVotes = 1,
                    DownVotes = 1,
                    IsAccepted = true,
                    IsActive = true,
                    IsClose = true,
                    StartOffSet = 1,
                    Count = 10,
                    TotalCount = 10,
                    AnswerList = null,
                    CommentList = null,
                },

            };
            _questionAllQuestionForMainModel = new AllQuestionForMainModel()
            {
                Count = 1,
                StartOffSet = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };
            _questionListByUserIdRequestModel = new QuestionListByUserIdRequestModel()
            {
                UserId = 1
            };
            _questionCountModel = new QuestionCountModel()
            {
                Count = 1
            };
            _questionSearchModel = new QuestionSearchModel()
            {
                Id = 1,
                Header = "test",
                Detail = "test",
                UpVotes = 1,
                DownVotes = 1,
                IsAccepted = true,
                IsActive = true,
                IsClose = true,
                UserId = 1,
                UserName = "test",
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _logManager = new Mock<ILogger<AskQuestionController>>();
            _questionService = new Mock<IAskQuestionService>();
            _mapper = new Mock<IMapper>();
            _mockElasticClient = new Mock<IElasticClient>();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void CreateQuestion()
        {
            _questionService.Setup(x => x.CreateQuestion(_questionCreateModel)).Returns(Task.FromResult(default(int)));
            _questionService.Setup(x => x.CheckIfQuestionExist("test")).Returns(_questionBaseResponseModel);
            _mapper.Setup(x => x.Map<QuestionCreateRequestModel, QuestionCreateModel>(_questionCreateRequestModel)).Returns(_questionCreateModel);
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.CreateQuestion(_questionCreateRequestModel);
            var result = actual.Result as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void CreateQuestion_CheckQuestionIsExist()
        {
            _questionService.Setup(x => x.CreateQuestion(_questionCreateModel)).Returns(Task.FromResult(default(int)));
            _questionBaseResponseModel.IsSucceed = false;
            _questionService.Setup(x => x.CheckIfQuestionExist("test")).Returns(_questionBaseResponseModel);
            _mapper.Setup(x => x.Map<QuestionCreateRequestModel, QuestionCreateModel>(_questionCreateRequestModel)).Returns(_questionCreateModel);
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.CreateQuestion(_questionCreateRequestModel);
            var result = actual.Result as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void UpdateQuestion()
        {
            _questionService.Setup(x => x.UpdateQuestion(_questionUpdateModel));
            _mapper.Setup(x => x.Map<QuestionUpdateRequestModel, QuestionUpdateModel>(_questionUpdateRequestModel)).Returns(_questionUpdateModel);
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.UpdateQuestion(_questionUpdateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void DeleteQuestion()
        {
            _questionService.Setup(x => x.DeleteQuestion(_questionDeleteModel));
            _mapper.Setup(x => x.Map<QuestionDeleteRequestModel, QuestionDeleteModel>(_questionDeleteRequestModel)).Returns(_questionDeleteModel);
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.DeleteQuestion(_questionDeleteRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void GetAllQuestions()
        {
            _questionService.Setup(x => x.GetAllQuestions()).Returns(_questionDetailModelList);
            _mapper.Setup(x => x.Map<QuestionDetailRequestModel, QuestionDetailModel>(_questionDetailRequestModel)).Returns(_questionDetailModel);
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.GetAllQuestions();
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void GetQuestionById()
        {
            _questionService.Setup(x => x.GetQuestionById(1)).Returns(_questionDetailModel);
            _mapper.Setup(x => x.Map<QuestionDetailModel, QuestionDetailResponseModel>(_questionDetailModel));
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.GetQuestionById(_questionIdModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);

        }
        [Fact]
        public void QuestionUpdateUpVote()
        {
            _questionService.Setup(x => x.QuestionUpdateUpVote(_questionUpdateModel));
            _mapper.Setup(x => x.Map<QuestionUpdateRequestModel, QuestionUpdateModel>(_questionUpdateRequestModel)).Returns(_questionUpdateModel);
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.QuestionUpdateUpVote(_questionUpdateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);

        }
        [Fact]
        public void QuestionUpdateDownVote()
        {
            _questionService.Setup(x => x.QuestionUpdateDownVote(_questionUpdateModel));
            _mapper.Setup(x => x.Map<QuestionUpdateRequestModel, QuestionUpdateModel>(_questionUpdateRequestModel)).Returns(_questionUpdateModel);
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.QuestionUpdateDownVote(_questionUpdateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);

        }
        [Fact]
        public void QuestionIsClosedById()
        {
            _questionService.Setup(x => x.QuestionIsClosedById(_questionUpdateModel));
            _mapper.Setup(x => x.Map<QuestionUpdateRequestModel, QuestionUpdateModel>(_questionUpdateRequestModel)).Returns(_questionUpdateModel);
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.QuestionIsClosedById(_questionUpdateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);

        }
        [Fact]
        public void GetAllQuestionsForMain()
        {
            _questionService.Setup(x => x.GetAllQuestionsForMain(_questionAllQuestionForMainModel)).Returns(_questionDetailModelList);
            _mapper.Setup(x => x.Map<AllQuestionForMainRequestModel, AllQuestionForMainModel>(_questionAllQuestionForMainRequestModel)).Returns(_questionAllQuestionForMainModel);
            _mapper.Setup(x => x.Map<List<QuestionDetailModel>, List<QuestionDetailResponseModel>>(_questionDetailModelList));
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.GetAllQuestionsForMain(_questionAllQuestionForMainRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void GetQuestionWithAnswers()
        {
            _questionService.Setup(x => x.GetQuestionWithAnswers(_questionDetailModel)).Returns(_questionDetailModel);
            _mapper.Setup(x => x.Map<QuestionDetailModel, QuestionDetailResponseModel>(_questionDetailModel));
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.GetQuestionWithAnswers(_questionDetailModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void GetUnansweredQuestions()
        {
            _questionService.Setup(x => x.GetUnansweredQuestions(_questionDetailModel)).Returns(_questionDetailModelList);
            _mapper.Setup(x => x.Map<List<QuestionDetailModel>, List<QuestionDetailResponseModel>>(_questionDetailModelList));
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.GetUnansweredQuestions(_questionDetailModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void GetMostUpVotedQuestions()
        {
            _questionService.Setup(x => x.GetMostUpVotedQuestions(_questionDetailModel)).Returns(_questionDetailModelList);
            _mapper.Setup(x => x.Map<List<QuestionDetailModel>, List<QuestionDetailResponseModel>>(_questionDetailModelList));
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.GetMostUpVotedQuestions(_questionDetailModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void GetQuestionsByUserId()
        {
            _questionService.Setup(x => x.GetQuestionsByUserId(_questionDetailModel)).Returns(_questionDetailModelList);
            _mapper.Setup(x => x.Map<List<QuestionDetailModel>, List<QuestionDetailResponseModel>> (_questionDetailModelList));
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.GetQuestionsByUserId(_questionDetailModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void GetQuestionCountByUserId()
        {
            _questionService.Setup(x => x.GetQuestionCountByUserId(_questionListByUserIdRequestModel.UserId)).Returns(_questionCountModel);
            _mapper.Setup(x => x.Map<QuestionCountModel, QuestionCountResponseModel>(_questionCountModel));
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.GetQuestionCountByUserId(_questionListByUserIdRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void ReIndex()
        {
            _mockElasticClient.Setup(x => x.DeleteByQueryAsync<QuestionSearchModel>(null, default));
            _mockElasticClient.Setup(x => x.IndexDocumentAsync<QuestionDetailModel>(null, default));
            _questionService.Setup(x => x.GetAllQuestions()).Returns(_questionDetailModelList);
            var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
            var actual = questionController.ReIndex();
            var result = actual.Result as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
        //[Fact]
        //public void Find()
        //{
        //    _mockElasticClient.Setup(x => x.SearchAsync<QuestionDetailModel>(null,default));
        //    _questionService.Setup(x => x.GetAllQuestions()).Returns(_questionDetailModelList);
        //    var questionController = new AskQuestionController(_logManager.Object, _questionService.Object, _mapper.Object, _mockElasticClient.Object);
        //    var actual = questionController.Find("test",1,5);
        //    var result = actual.Result as OkObjectResult;
        //    Assert.Equal(200, result.StatusCode);
        //}
    }
}
