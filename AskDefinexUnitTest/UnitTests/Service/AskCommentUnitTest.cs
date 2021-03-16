using AskDefinex.Business.Model.AskCommentModule;
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
    public class AskCommentUnitTest : IDisposable
    {
        private List<AskCommentDAOModel> _daoModelList;
        private List<AskCommentDAOModel> _daoModelList2;
        private AskCommentDAOModel _daoModel;
        private CommentUpdateModel _updateModel;
        private CommentCreateModel _createModel;
        private CommentDetailModel _detailModel;
        private List<CommentDetailModel> _detailModelList;
        private CommentDeleteModel _deleteModel;
        Mock<IAskCommentDAO> _mockDaoService;
        Mock<IUserContextManager<IUserContextModel>> _userContextManager;
        Mock<IMapper> _mapper;
        Mock<ILogger<AskCommentService>> _logManager;

        public AskCommentUnitTest()
        {
            _daoModelList = new List<AskCommentDAOModel>()
            {
                new AskCommentDAOModel
                {
                    Id = 1,
                    Question_Answer_Id = 1,
                    UserId = 1,
                    Type = 1,
                    Comment = "test",
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    CreateUser ="test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                },
                new AskCommentDAOModel
                {
                    Id = 2,
                    Question_Answer_Id = 1,
                    UserId = 1,
                    Type = 1,
                    Comment = "test",
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    CreateUser ="test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                }
            };

            _daoModel = new AskCommentDAOModel()
            {
                Id = 1,
                Question_Answer_Id = 1,
                UserId = 1,
                Type = 1,
                Comment = "test",
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _updateModel = new CommentUpdateModel()
            {
                Id = 1,
                Question_Answer_Id = 1,
                UserId = 1,
                Type = 1,
                Comment = "test",
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _createModel = new CommentCreateModel()
            {
                Question_Answer_Id = 1,
                UserId = 1,
                Type = 1,
                Comment = "test",
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _detailModel = new CommentDetailModel()
            {
                Question_Answer_Id = 1,
                UserId = 1,
                Type = 1,
                Comment = "test",
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _deleteModel = new CommentDeleteModel()
            {
                Id = 1,
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = "test",
                LastUpdateDate = DateTime.Now,
                LastUpdateUser = "test"
            };

            _detailModelList = new List<CommentDetailModel>()
            {
                new CommentDetailModel
                {
                    Question_Answer_Id = 1,
                    UserId = 1,
                    Type = 1,
                    Comment = "test",
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                },
                new CommentDetailModel
                {
                    Question_Answer_Id = 1,
                    UserId = 1,
                    Type = 1,
                    Comment = "test",
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    CreateUser = "test",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = "test"
                }
            };

            _mockDaoService = new Mock<IAskCommentDAO>();
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(x => x.GetUser().UserName).Returns("test");
            _userContextManager.Setup(x => x.GetUser().UserId).Returns(1);
            _mapper = new Mock<IMapper>();
            _logManager = new Mock<ILogger<AskCommentService>>();

        }

        public void Dispose()
        {

        }

        [Fact]
        public void CreateComment()
        {
            _mockDaoService.Setup(x => x.CreateComment(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<CommentCreateModel, AskCommentDAOModel>(_createModel)).Returns(_daoModel);
            var commentService = new AskCommentService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = commentService.CreateComment(_createModel);
            Assert.Equal(1, actual);
        }
        [Fact]
        public void CreateComment_When_User_Null()
        {
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(m => m.GetUser()).Returns((IUserContextModel)null);
            _mockDaoService.Setup(x => x.CreateComment(_daoModel)).Returns(1);
            _mapper.Setup(x => x.Map<CommentCreateModel, AskCommentDAOModel>(_createModel)).Returns(_daoModel);
            var commentService = new AskCommentService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            Assert.Throws<NullReferenceException>(() => commentService.CreateComment(_createModel));
        }

        [Fact]
        public void UpdateComment()
        {
            _mockDaoService.Setup(x => x.UpdateComment(_daoModel));
            _mapper.Setup(x => x.Map<CommentUpdateModel, AskCommentDAOModel>(_updateModel)).Returns(_daoModel);
            var commentService = new AskCommentService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            commentService.UpdateComment(_updateModel);
            _mockDaoService.Verify(x => x.UpdateComment(_daoModel));
        }
        [Fact]
        public void UpdateComment_When_User_Null()
        {
            _userContextManager = new Mock<IUserContextManager<IUserContextModel>>();
            _userContextManager.Setup(m => m.GetUser()).Returns((IUserContextModel)null);
            _mockDaoService.Setup(x => x.UpdateComment(_daoModel));
            _mapper.Setup(x => x.Map<CommentUpdateModel, AskCommentDAOModel>(_updateModel)).Returns(_daoModel);
            var commentService = new AskCommentService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            Assert.Throws<NullReferenceException>(() => commentService.UpdateComment(_updateModel));
        }
        [Fact]
        public void DeleteComment()
        {
            _mockDaoService.Setup(x => x.DeleteComment(_daoModel));
            _mapper.Setup(x => x.Map<CommentDeleteModel, AskCommentDAOModel>(_deleteModel)).Returns(_daoModel);
            var commentService = new AskCommentService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            commentService.DeleteComment(_deleteModel);
            _mockDaoService.Verify(x => x.DeleteComment(_daoModel));
        }
        [Fact]
        public void DeleteCommentByQuestionId()
        {
            _mockDaoService.Setup(x => x.DeleteCommentByQuestionId(_daoModel));
            _mapper.Setup(x => x.Map<CommentDeleteModel, AskCommentDAOModel>(_deleteModel)).Returns(_daoModel);
            var commentService = new AskCommentService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            commentService.DeleteCommentByQuestionId(_deleteModel);
            _mockDaoService.Verify(x => x.DeleteCommentByQuestionId(_daoModel));
        }
        [Fact]
        public void DeleteCommentByQuestionId_When_Occurred_Exception()
        {
            _mockDaoService.Setup(x => x.DeleteCommentByQuestionId(_daoModel)).Throws(new Exception());
            _mapper.Setup(x => x.Map<CommentDeleteModel, AskCommentDAOModel>(_deleteModel)).Returns(_daoModel);
            var commentService = new AskCommentService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            Assert.Throws<Exception>(() => commentService.DeleteCommentByQuestionId(_deleteModel));
        }
        [Fact]
        public void GetCommentsByQuestionId()
        {
            _mockDaoService.Setup(x => x.GetCommentsByQuestionId(1)).Returns(_daoModelList);
            _mapper.Setup(x => x.Map<List<AskCommentDAOModel>, List<CommentDetailModel>>(_daoModelList)).Returns(_detailModelList);
            var commentService = new AskCommentService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = commentService.GetCommentsByQuestionId(1);
            Assert.Equal(_detailModelList, actual);
        }
        [Fact]
        public void GetCommentsByQuestionId_When_DAO_Null()
        {
            _daoModelList2 = new List<AskCommentDAOModel>();
            _daoModelList2 = null;
            _mockDaoService.Setup(x => x.GetCommentsByQuestionId(1)).Returns(_daoModelList2);
            _mapper.Setup(x => x.Map<List<AskCommentDAOModel>, List<CommentDetailModel>>(_daoModelList)).Returns(_detailModelList);
            var commentService = new AskCommentService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = commentService.GetCommentsByQuestionId(1);
            Assert.Equal(new List<CommentDetailModel>(), actual);
        }

        [Fact]
        public void GetCommentsByAnswerId()
        {
            _mockDaoService.Setup(x => x.GetCommentsByAnswerId(1)).Returns(_daoModelList);
            _mapper.Setup(x => x.Map<List<AskCommentDAOModel>, List<CommentDetailModel>>(_daoModelList)).Returns(_detailModelList);
            var commentService = new AskCommentService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = commentService.GetCommentsByAnswerId(1);
            Assert.Equal(_detailModelList, actual);
        }
        [Fact]
        public void GetCommentsByAnswerId_When_DAO_Null()
        {
            _daoModelList2 = new List<AskCommentDAOModel>();
            _daoModelList2 = null;
            _mockDaoService.Setup(x => x.GetCommentsByAnswerId(1)).Returns(_daoModelList2);
            _mapper.Setup(x => x.Map<List<AskCommentDAOModel>, List<CommentDetailModel>>(_daoModelList)).Returns(_detailModelList);
            var commentService = new AskCommentService(_logManager.Object, _mockDaoService.Object, _userContextManager.Object, _mapper.Object);
            var actual = commentService.GetCommentsByAnswerId(1);
            Assert.Equal(new List<CommentDetailModel>(), actual);
        }
    }
}
