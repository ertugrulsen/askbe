using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskAnswerModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Controller;
using AskDefinex.Rest.Model.Request.AskAnswerModule;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace AskDefinexUnitTest.UnitTests.Controller
{
    public class AskTagControllerUnitTest
    {
        private readonly Mock<ILogger<AskTagController>> _logManager;
        private readonly Mock<IAskTagService> _tagService;
        private readonly Mock<IMapper> _mapper;
        private readonly TagCreateModel _tagCreateModel;
        private readonly TagUpdateModel _tagUpdateModel;
        private readonly TagCreateRequestModel _tagCreateRequestModel;
        private readonly TagUpdateRequestModel _tagUpdateRequestModel;
        private readonly TagDeleteRequestModel _tagDeleteRequestModel;
        public AskTagControllerUnitTest()
        {
            _tagCreateModel = new TagCreateModel()
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

            _tagUpdateModel = new TagUpdateModel()
            {
                Name = "test",
                Type = "1",
                QuestionId = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test",
                Id = 1
            };

            _tagCreateRequestModel = new TagCreateRequestModel()
            {
                QuestionId = "1",
                Name = "test",
                Type = 1,
                IsActive = true
            };

            _tagUpdateRequestModel = new TagUpdateRequestModel()
            {
                Id = "1",
                Type = 1,
                QuestionId = "1",
                Name = "test"
            };

            _tagDeleteRequestModel = new TagDeleteRequestModel()
            {
                Id = 1
            };

            _logManager = new Mock<ILogger<AskTagController>>();
            _tagService = new Mock<IAskTagService>();
            _mapper = new Mock<IMapper>();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void CreateTag()
        {
            _tagService.Setup(x => x.CreateTag(_tagCreateModel)).Returns(1);
            _mapper.Setup(x => x.Map<TagCreateRequestModel, TagCreateModel>(_tagCreateRequestModel)).Returns(_tagCreateModel);
            var tagController = new AskTagController(_logManager.Object, _tagService.Object, _mapper.Object);
            var actual = tagController.CreateTag(_tagCreateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void UpdateTag()
        {
            _tagService.Setup(x => x.UpdateTag(_tagUpdateModel));
            _mapper.Setup(x => x.Map<TagUpdateRequestModel, TagUpdateModel>(_tagUpdateRequestModel)).Returns(_tagUpdateModel);
            var tagController = new AskTagController(_logManager.Object, _tagService.Object, _mapper.Object);
            var actual = tagController.UpdateTag(_tagUpdateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void DeleteTag()
        {
            _tagService.Setup(x => x.DeleteTag(_tagUpdateModel));
            _mapper.Setup(x => x.Map<TagDeleteRequestModel, TagUpdateModel>(_tagDeleteRequestModel)).Returns(_tagUpdateModel);
            var tagController = new AskTagController(_logManager.Object, _tagService.Object, _mapper.Object);
            var actual = tagController.DeleteTag(_tagDeleteRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
    }
}
