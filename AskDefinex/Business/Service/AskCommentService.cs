using AskDefinex.Business.Model.AskCommentModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using AutoMapper;
using DefineXwork.Library.Business;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.Security;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AskDefinex.Business.Service
{
    public class AskCommentService : BaseService, IAskCommentService
    {
        private readonly ILogger<AskCommentService> _logManager;
        private readonly IAskCommentDAO _askCommentDAO;
        private readonly IUserContextManager<IUserContextModel> _userContextManager;
        private readonly IMapper _mapper;

        public AskCommentService(ILogger<AskCommentService> logManager, IAskCommentDAO askCommentDAO, IUserContextManager<IUserContextModel> userContextManager, IMapper mapper)
        {
            _logManager = logManager;
            _askCommentDAO = askCommentDAO;
            _userContextManager = userContextManager;
            _mapper = mapper;
        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            base.AddToTransaction(databaseManager, _askCommentDAO);
        }
        public int CreateComment(CommentCreateModel commentModel)
        {
            try
            {
                if (_userContextManager.GetUser() == null)
                {
                    _logManager.LogWarning("User context manager get User is null");
                    commentModel.UserId = 0;
                    throw new ArgumentNullException(_userContextManager.GetUser().ToString());
                }
                commentModel.CreateDate = DateTime.Now;
                commentModel.CreateUser = _userContextManager.GetUser()?.UserName;
                commentModel.UserId = _userContextManager.GetUser().UserId;

                AskCommentDAOModel daoModel = _mapper.Map<CommentCreateModel, AskCommentDAOModel>(commentModel);
                int newCommentId = _askCommentDAO.CreateComment(daoModel);

                return newCommentId;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at CreateComment service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public void UpdateComment(CommentUpdateModel updateModel)
        {
            try
            {
                updateModel.LastUpdateDate = DateTime.Now;
                updateModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;
                if (_userContextManager.GetUser() == null)
                {
                    _logManager.LogWarning("User context manager get User is null");
                    updateModel.UserId = 0;
                    throw new ArgumentNullException(_userContextManager.GetUser().ToString());
                }
                else
                {
                    updateModel.UserId = (int)(_userContextManager.GetUser().UserId);
                }

                AskCommentDAOModel daoModel = _mapper.Map<CommentUpdateModel, AskCommentDAOModel>(updateModel);
                _askCommentDAO.UpdateComment(daoModel);
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at UpdateComment service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public void DeleteComment(CommentDeleteModel deleteModel)
        {
            try
            {
                deleteModel.LastUpdateDate = DateTime.Now;
                deleteModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;
                deleteModel.IsActive = false;

                AskCommentDAOModel daoModel = _mapper.Map<CommentDeleteModel, AskCommentDAOModel>(deleteModel);
                _askCommentDAO.DeleteComment(daoModel);
                _logManager.LogWarning("Delete Comment User is {UserName}" + deleteModel.LastUpdateUser);

            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at DeleteComment service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public void DeleteCommentByQuestionId(CommentDeleteModel deleteModel)
        {
            try
            {
                deleteModel.LastUpdateDate = DateTime.Now;
                deleteModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;
                deleteModel.IsActive = false;

                AskCommentDAOModel daoModel = _mapper.Map<CommentDeleteModel, AskCommentDAOModel>(deleteModel);
                _askCommentDAO.DeleteCommentByQuestionId(daoModel);
                _logManager.LogWarning("DeleteCommentByQuestionId: User is {UserName}" + deleteModel.LastUpdateUser);

            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at DeleteCommentByQuestionId service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
            
        }

        public List<CommentDetailModel> GetCommentsByQuestionId(int questionid)
        {
            try
            {
                List<AskCommentDAOModel> dao = _askCommentDAO.GetCommentsByQuestionId(questionid);
                if (dao == null)
                {
                    _logManager.LogWarning("GetCommentsByQuestionId dao is null");
                    return new List<CommentDetailModel>();
                }
                else
                {
                    List<CommentDetailModel> comment = _mapper.Map<List<AskCommentDAOModel>, List<CommentDetailModel>>(dao);
                    return comment;
                }
               
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at GetCommentsByQuestionId service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }

        }
        public List<CommentDetailModel> GetCommentsByAnswerId(int answerid)
        {
            try
            {
                List<AskCommentDAOModel> dao = _askCommentDAO.GetCommentsByAnswerId(answerid);
                if (dao == null) {
                    _logManager.LogWarning("GetCommentsByAnswerId dao is null");
                    return new List<CommentDetailModel>();
                }
                else
                {
                    List<CommentDetailModel> comment = _mapper.Map<List<AskCommentDAOModel>, List<CommentDetailModel>>(dao);
                    return comment;
                }
                
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at GetCommentsByAnswerId service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }

        }
    }
}
