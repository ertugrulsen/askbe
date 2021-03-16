using AskDefinex.Business.Model.AskCommentModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Controller;
using AskDefinex.Rest.Model.Request.AskCommentModule;
using AskDefinex.Rest.Model.Response.AskCommentModule;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AskDefinexUnitTest.UnitTests.Controller
{
    public class AskCommentControllerUnitTest
    {
        private readonly Mock<ILogger<AskCommentController>> _logManager;
        private readonly Mock<IAskCommentService> _commentService;
        private readonly Mock<IMapper> _mapper;
        private readonly CommentCreateModel _commentCreateModel;
        private readonly CommentUpdateModel _commentUpdateModel;
        private readonly CommentDeleteModel _commentDeleteModel;
        private readonly CommentDetailModel _commentDetailModel;
        private readonly List<CommentDetailModel> _commentDetailModelList;
        private readonly CommentCreateRequestModel _commentCreateRequestModel;
        private readonly CommentUpdateRequestModel _commentUpdateRequestModel;
        private readonly CommentDeleteRequestModel _commentDeleteRequestModel;
        public AskCommentControllerUnitTest()
        {
            _commentCreateModel = new CommentCreateModel()
            {
                UserId = 1,
                Type = 1,
                Comment = "test",
                IsActive = true,
                Question_Answer_Id = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _commentUpdateModel = new CommentUpdateModel()
            {
                Id = 1,
                UserId = 1,
                Comment = "test",
                Question_Answer_Id = 1,
                IsActive = true,
                Type = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _commentDeleteModel = new CommentDeleteModel()
            {
                Id = 1,
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _commentDetailModel = new CommentDetailModel()
            {
                Id = 1,
                UserId = 1,
                Comment = "test",
                Question_Answer_Id = 1,
                IsActive = true,
                Type = 1,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _commentDetailModelList = new List<CommentDetailModel>()
            {
                new CommentDetailModel()
                {
                    Id = 1,
                    UserId = 1,
                    Comment = "test",
                    Question_Answer_Id = 1,
                    IsActive = true,
                    Type = 1,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                },
                new CommentDetailModel()
                {
                    Id = 2,
                    UserId = 1,
                    Comment = "test",
                    Question_Answer_Id = 1,
                    IsActive = true,
                    Type = 1,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                }
            };

            _commentCreateRequestModel = new CommentCreateRequestModel()
            {
                UserId = 1,
                Question_Answer_Id = 1,
                Comment = "test",
                Type = 1,
                IsActive = true
            };

            _commentUpdateRequestModel = new CommentUpdateRequestModel()
            {
                Id = 1,
                UserId = 1,
                Type = 1,
                IsActive = true,
                Question_Answer_Id = 1,
                Comment = "test"
            };

            _commentDeleteRequestModel = new CommentDeleteRequestModel()
            {
                Id = 1,
                IsActive = true
            };

            _logManager = new Mock<ILogger<AskCommentController>>();
            _commentService = new Mock<IAskCommentService>();
            _mapper = new Mock<IMapper>();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void CreateComment()
        {
            _commentService.Setup(x => x.CreateComment(_commentCreateModel)).Returns(1);
            _mapper.Setup(x => x.Map<CommentCreateRequestModel, CommentCreateModel>(_commentCreateRequestModel)).Returns(_commentCreateModel);
            var commentController = new AskCommentController(_logManager.Object, _commentService.Object, _mapper.Object);
            var actual = commentController.CreateComment(_commentCreateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void UpdateComment()
        {
            _commentService.Setup(x => x.UpdateComment(_commentUpdateModel));
            _mapper.Setup(x => x.Map<CommentUpdateRequestModel, CommentUpdateModel>(_commentUpdateRequestModel)).Returns(_commentUpdateModel);
            var commentController = new AskCommentController(_logManager.Object, _commentService.Object, _mapper.Object);
            var actual = commentController.UpdateComment(_commentUpdateRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void DeleteComment()
        {
            _commentService.Setup(x => x.DeleteComment(_commentDeleteModel));
            _mapper.Setup(x => x.Map<CommentDeleteRequestModel, CommentDeleteModel>(_commentDeleteRequestModel)).Returns(_commentDeleteModel);
            var commentController = new AskCommentController(_logManager.Object, _commentService.Object, _mapper.Object);
            var actual = commentController.DeleteComment(_commentDeleteRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetCommentsByQuestionId()
        {
            _commentService.Setup(x => x.GetCommentsByQuestionId(1)).Returns(_commentDetailModelList);
            _mapper.Setup(x => x.Map<List<CommentDetailModel>, List<CommentDetailResponseModel>>(_commentDetailModelList));
            var commentController = new AskCommentController(_logManager.Object, _commentService.Object, _mapper.Object);
            var actual = commentController.GetCommentsByQuestionId(1);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetCommentsByAnswerId()
        {
            _commentService.Setup(x => x.GetCommentsByAnswerId(1)).Returns(_commentDetailModelList);
            _mapper.Setup(x => x.Map<List<CommentDetailModel>, List<CommentDetailResponseModel>>(_commentDetailModelList));
            var commentController = new AskCommentController(_logManager.Object, _commentService.Object, _mapper.Object);
            var actual = commentController.GetCommentsByAnswerId(1);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void DeleteCommentByQuestionId()
        {
            _commentService.Setup(x => x.DeleteCommentByQuestionId(_commentDeleteModel));
            _mapper.Setup(x => x.Map<CommentDeleteRequestModel, CommentDeleteModel>(_commentDeleteRequestModel));
            var commentController = new AskCommentController(_logManager.Object, _commentService.Object, _mapper.Object);
            var actual = commentController.DeleteCommentByQuestionId(_commentDeleteRequestModel);
            var result = actual as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
    }
}
