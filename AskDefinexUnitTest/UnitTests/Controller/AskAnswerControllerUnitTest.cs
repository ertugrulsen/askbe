using AskDefinex.Business.Model.AskAnswerModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Controller;
using AskDefinex.Rest.Model.Request.AskAnswerModule;
using AskDefinex.Rest.Model.Response.AskAnswerModule;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AskDefinexUnitTest.UnitTests.Controller
{
    public class AskAnswerControllerUnitTest
    {
        private readonly Mock<ILogger<AskAnswerController>> _logManager;
        private readonly Mock<IAskAnswerService> _answerService;
        private readonly Mock<IMapper> _mapper;
        private readonly AnswerCreateRequestModel _answerCreateRequestModel;
        private readonly AnswerUpdateRequestModel _answerUpdateRequestModel;
        private readonly AnswerDeleteRequestModel _answerDeleteRequestModel;
        private readonly AnswerDetailRequestModel _answerDetailRequestModel;
        private readonly AnswerCreateModel _answerCreateModel;
        private readonly AnswerUpdateModel _answerUpdateModel;
        private readonly AnswerDeleteModel _answerDeleteModel;
        private readonly AnswerDetailModel _answerDetailModel;
        private readonly List<AnswerDetailModel> _answerDetailModelList;
        public AskAnswerControllerUnitTest()
        {
            _answerCreateRequestModel = new AnswerCreateRequestModel()
            {
                Answer = "test",
                QuestionId = 1,
                UserId = 1
            };

            _answerUpdateRequestModel = new AnswerUpdateRequestModel()
            {
                Id = 1,
                Answer = "test",
                IsAccepted = true,
                IsActive = true
            };

            _answerDeleteRequestModel = new AnswerDeleteRequestModel()
            {
                Id = 1,
                IsActive = true
            };

            _answerDetailRequestModel = new AnswerDetailRequestModel()
            {
                Id = 1,
                UserId = 1,
                QuestionId = 1,
                Answer = "test",
                DownVotes = 0,
                UpVotes = 0,
                IsAccepted = true,
                IsActive = true
            };

            _answerCreateModel = new AnswerCreateModel()
            {
                UserId = 1,
                UpVotes = 0,
                QuestionId = 1,
                Answer = "test",
                IsAccepted = true,
                IsActive = true,
                DownVotes = 0,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _answerUpdateModel = new AnswerUpdateModel()
            {
                UserId = 1,
                UpVotes = 0,
                QuestionId = 1,
                Answer = "test",
                IsAccepted = true,
                IsActive = true,
                DownVotes = 0,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test",
                Id = 1
            };

            _answerDeleteModel = new AnswerDeleteModel()
            {
                Id = 1,
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test",
            };

            _answerDetailModel = new AnswerDetailModel()
            {
                UserId = 1,
                UpVotes = 0,
                QuestionId = 1,
                Answer = "test",
                IsAccepted = true,
                IsActive = true,
                DownVotes = 0,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test",
                Id = 1
            };

            _answerDetailModelList = new List<AnswerDetailModel>()
            {
                new AnswerDetailModel()
                {
                    UserId = 1,
                    UpVotes = 0,
                    QuestionId = 1,
                    Answer = "test",
                    IsAccepted = true,
                    IsActive = true,
                    DownVotes = 0,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test",
                    Id = 1
                },
                new AnswerDetailModel()
                {
                    UserId = 1,
                    UpVotes = 0,
                    QuestionId = 1,
                    Answer = "test",
                    IsAccepted = true,
                    IsActive = true,
                    DownVotes = 0,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test",
                    Id = 2
                }
            };

            _logManager = new Mock<ILogger<AskAnswerController>>();
            _answerService = new Mock<IAskAnswerService>();
            _mapper = new Mock<IMapper>();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void CreateAnswer()
        {
            _answerService.Setup(x => x.CreateAnswer(_answerCreateModel)).Returns(1);
            _mapper.Setup(x => x.Map<AnswerCreateRequestModel, AnswerCreateModel>(_answerCreateRequestModel)).Returns(_answerCreateModel);
            var answerController= new AskAnswerController(_logManager.Object, _answerService.Object, _mapper.Object);
            var actual = answerController.CreateAnswer(_answerCreateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void UpdateAnswer()
        {
            _answerService.Setup(x => x.UpdateAnswer(_answerUpdateModel));
            _mapper.Setup(x => x.Map<AnswerUpdateRequestModel, AnswerUpdateModel>(_answerUpdateRequestModel)).Returns(_answerUpdateModel);
            var answerController = new AskAnswerController(_logManager.Object, _answerService.Object, _mapper.Object);
            var actual = answerController.UpdateAnswer(_answerUpdateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void DeleteAnswer()
        {
            _answerService.Setup(x => x.DeleteAnswer(_answerDeleteModel));
            _mapper.Setup(x => x.Map<AnswerDeleteRequestModel, AnswerDeleteModel>(_answerDeleteRequestModel)).Returns(_answerDeleteModel);
            var answerController = new AskAnswerController(_logManager.Object, _answerService.Object, _mapper.Object);
            var actual = answerController.DeleteAnswer(_answerDeleteRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void AnswerUpdateUpVote()
        {
            _answerService.Setup(x => x.AnswerUpdateUpVote(_answerUpdateModel));
            _mapper.Setup(x => x.Map<AnswerUpdateRequestModel, AnswerUpdateModel>(_answerUpdateRequestModel)).Returns(_answerUpdateModel);
            var answerController = new AskAnswerController(_logManager.Object, _answerService.Object, _mapper.Object);
            var actual = answerController.AnswerUpdateUpVote(_answerUpdateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void AnswerUpdateDownVote()
        {
            _answerService.Setup(x => x.AnswerUpdateDownVote(_answerUpdateModel));
            _mapper.Setup(x => x.Map<AnswerUpdateRequestModel, AnswerUpdateModel>(_answerUpdateRequestModel)).Returns(_answerUpdateModel);
            var answerController = new AskAnswerController(_logManager.Object, _answerService.Object, _mapper.Object);
            var actual = answerController.AnswerUpdateDownVote(_answerUpdateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAnswerById()
        {
            _answerService.Setup(x => x.GetAnswerById(1)).Returns(_answerDetailModel);
            _mapper.Setup(x => x.Map<AnswerDetailModel, AnswerDetailResponseModel>(_answerDetailModel));
            var answerController = new AskAnswerController(_logManager.Object, _answerService.Object, _mapper.Object);
            var actual = answerController.GetAnswerById(_answerDetailRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAnswersByQuestionId()
        {
            _answerService.Setup(x => x.GetAnswersByQuestionId(1)).Returns(_answerDetailModelList);
            _mapper.Setup(x => x.Map<List<AnswerDetailModel>, List<AnswerDetailResponseModel>>(_answerDetailModelList));
            var answerController = new AskAnswerController(_logManager.Object, _answerService.Object, _mapper.Object);
            var actual = answerController.GetAnswersByQuestionId(1);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
    }
}
