using AskDefinex.Business.Model.AskAnswerModule;
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
using System.Linq;

namespace AskDefinex.Business.Service
{
    public class AskAnswerService : BaseService, IAskAnswerService
    {
        private readonly ILogger<AskAnswerService> _logManager;
        private readonly IAskAnswerDAO _askAnswerDao;
        private readonly IUserContextManager<IUserContextModel> _userContextManager;
        private readonly IMapper _mapper;

        public AskAnswerService(ILogger<AskAnswerService> logManager, IAskAnswerDAO askAnswerDao, IUserContextManager<IUserContextModel> userContextManager, IMapper mapper)
        {
            _logManager = logManager;
            _askAnswerDao = askAnswerDao;
            _userContextManager = userContextManager;
            _mapper = mapper;
        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            base.AddToTransaction(databaseManager, _askAnswerDao);
        }
        public int CreateAnswer(AnswerCreateModel answerModel)
        {
            try
            {
                if (_userContextManager.GetUser() == null)
                {
                    _logManager.LogWarning("User context manager get User is null");
                    throw new ArgumentNullException(_userContextManager.GetUser().ToString());
                }
                answerModel.DownVotes = 0;
                answerModel.UpVotes = 0;
                answerModel.IsAccepted = false;
                answerModel.IsActive = true;
                answerModel.CreateDate = DateTime.Now;
                answerModel.CreateUser = _userContextManager.GetUser().UserName;
                answerModel.UserId = _userContextManager.GetUser().UserId;

                AskAnswerDAOModel daoModel = _mapper.Map<AnswerCreateModel, AskAnswerDAOModel>(answerModel);
                int newAnswerId = _askAnswerDao.CreateAnswer(daoModel);
                return newAnswerId;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at CreateAnswer service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public void UpdateAnswer(AnswerUpdateModel updateModel)
        {
            try
            {
                if (_userContextManager.GetUser() == null)
                {
                    _logManager.LogWarning("User context manager get User is null");
                    throw new ArgumentNullException(_userContextManager.GetUser().ToString());
                }
                updateModel.LastUpdateDate = DateTime.Now;
                updateModel.LastUpdateUser = _userContextManager.GetUser().UserName;
                updateModel.UserId = _userContextManager.GetUser().UserId;
                AskAnswerDAOModel daoModel = _mapper.Map<AnswerUpdateModel, AskAnswerDAOModel>(updateModel);
                _askAnswerDao.UpdateAnswer(daoModel);
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at UpdateAnswer service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public void DeleteAnswer(AnswerDeleteModel deleteModel)
        {
            try
            {
                if (_userContextManager.GetUser() == null)
                {
                    _logManager.LogWarning("User context manager get User is null");
                    throw new ArgumentNullException(_userContextManager.GetUser().ToString());
                }
                deleteModel.LastUpdateDate = DateTime.Now;
                deleteModel.LastUpdateUser = _userContextManager.GetUser().UserName;
                deleteModel.IsActive = false;

                AskAnswerDAOModel daoModel = _mapper.Map<AnswerDeleteModel, AskAnswerDAOModel>(deleteModel);
                _askAnswerDao.DeleteAnswer(daoModel);
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at DeleteAnswer service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public int AnswerUpdateUpVote(AnswerUpdateModel updateModel)
        {
            try
            {
                AskAnswerDAOModel answerDao = _askAnswerDao.GetAnswerById(updateModel.Id);
                if (answerDao != null)
                {
                    updateModel.LastUpdateDate = DateTime.Now;
                    updateModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;
                    updateModel.UpVotes = answerDao.UpVotes + 1;
                }
                AskAnswerDAOModel daoModel = _mapper.Map<AnswerUpdateModel, AskAnswerDAOModel>(updateModel);
                int data = _askAnswerDao.AnswerUpdateUpVote(daoModel);

                return data;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at AnswerUpdateUpVote Service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public int AnswerUpdateDownVote(AnswerUpdateModel updateModel)
        {
            try
            {
                AskAnswerDAOModel answerDao = _askAnswerDao.GetAnswerById(updateModel.Id);
                if (answerDao != null)
                {
                    updateModel.LastUpdateDate = DateTime.Now;
                    updateModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;
                    updateModel.DownVotes = answerDao.DownVotes + 1;
                }

                AskAnswerDAOModel daoModel = _mapper.Map<AnswerUpdateModel, AskAnswerDAOModel>(updateModel);
                int data = _askAnswerDao.AnswerUpdateDownVote(daoModel);

                return data;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at AnswerUpdateDownVote Service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public AnswerDetailModel GetAnswerById(int id)
        {
            try
            {
                AskAnswerDAOModel dao = _askAnswerDao.GetAnswerById(id);
                if (dao == null)
                    return new AnswerDetailModel();
                AnswerDetailModel question = _mapper.Map<AskAnswerDAOModel, AnswerDetailModel>(dao);
                return question;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at GetAnswerById Service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public List<AnswerDetailModel> GetAnswersByQuestionId(int questionId)
        {
            try
            {
                List<AskAnswerDAOModel> dao = _askAnswerDao.GetAnswersByQuestionId(questionId);

                List<AnswerDetailModel> question = _mapper.Map<List<AskAnswerDAOModel>, List<AnswerDetailModel>>(dao);

                return question;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at GetAnswersByQuestionId Service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
    }
}
