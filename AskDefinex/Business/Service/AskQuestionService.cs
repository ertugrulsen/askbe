using AutoMapper;
using DefineXwork.Library.Business;
using DefineXwork.Library.DataAccess;
using AskDefinex.Business.Model;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using System;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Common.Const;
using DefineXwork.Library.Security;
using System.Collections.Generic;
using Nest;
using System.Threading.Tasks;
using AskDefinex.Business.Model.AskQuestionModule;
using Microsoft.Extensions.Logging;

namespace AskDefinex.Business.Service
{
    /// <summary>
    /// Contains all services for communicating company table.
    /// </summary>
    /// <remarks>
    /// <para>This class can create, update, delete companies.</para>
    /// <para>Also can check if a company exits.</para>
    /// </remarks>
    public class AskQuestionService : BaseService, IAskQuestionService
    {
        public IElasticClient EsClient { get; set; }
        private readonly ILogger<AskQuestionService> _logManager;
        private readonly IAskQuestionDAO _askQuestionDAO;
        private readonly IUserContextManager<IUserContextModel> _userContextManager;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;

        public AskQuestionService(ILogger<AskQuestionService> logManager, IAskQuestionDAO askQuestionDAO, IUserContextManager<IUserContextModel> userContextManager, IMapper mapper, IElasticClient elasticClient)
        {
            _logManager = logManager;
            _askQuestionDAO = askQuestionDAO;
            _userContextManager = userContextManager;
            _mapper = mapper;
            _elasticClient = elasticClient;
        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            base.AddToTransaction(databaseManager, _askQuestionDAO);
        }
        public async Task<ISearchResponse<T>> SearchQuestion<T>(string query, int page = 1, int pageSize = 5) where T : QuestionSearchModel
        {
            try
            {
                var response = await _elasticClient.SearchAsync<QuestionDetailModel>(
                    s => s.Query(q => q.QueryString(d => d.Query(query)))
                        .From((page - 1) * pageSize)
                        .Size(pageSize));
                return (ISearchResponse<T>)response;
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at SearchQuestion service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;

            }
        }
        public async Task<int> CreateQuestion(QuestionCreateModel questionModel)
        {
            try
            {
                questionModel.CreateDate = DateTime.Now;
                questionModel.CreateUser = _userContextManager.GetUser()?.UserName;
                if (_userContextManager.GetUser() == null)
                {
                    _logManager.LogWarning("User context manager get User is null");
                    questionModel.UserId = 0;
                }
                else
                {
                    questionModel.UserId = (int)(_userContextManager.GetUser().UserId);
                }

                AskQuestionDAOModel daoModel = _mapper.Map<QuestionCreateModel, AskQuestionDAOModel>(questionModel);
                QuestionSearchModel questionSearchModel = _mapper.Map<QuestionCreateModel, QuestionSearchModel>(questionModel);
                int result = _askQuestionDAO.CreateQuestion(daoModel);
                questionSearchModel.Id = result;
                if (result != 0)
                {
                    await _elasticClient.IndexDocumentAsync(questionSearchModel);
                }
                return result;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at CreateQuestion service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public BaseResponseModel CheckIfQuestionExist(string header)
        {
            try
            {
                BaseResponseModel response = new BaseResponseModel();
                AskQuestionDAOModel dao = _askQuestionDAO.GetQuestionByHeader(header);
                if (dao == null)
                {
                    _logManager.LogWarning("CheckIfQuestionExist: dao is null");
                    response.IsSucceed = false;
                    response.Code = MessageCodes.QUESTION_NO_DATA_FOUND;
                    return response;
                }

                QuestionDetailModel questionMap = _mapper.Map<AskQuestionDAOModel, QuestionDetailModel>(dao);
                response.IsSucceed = true;
                response.Code = MessageCodes.QUESTION_ALREADY_EXIST;
                response.Data = questionMap;
                return response;
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at CheckIfQuestionExist service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public async void UpdateQuestion(QuestionUpdateModel updateModel)
        {
            try
            {
                updateModel.LastUpdateDate = DateTime.Now;
                updateModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;
                if (_userContextManager.GetUser() == null)
                {
                    _logManager.LogWarning("User context manager get User is null");
                    updateModel.UserId = 0;
                }
                else
                {
                    updateModel.UserId = (int)(_userContextManager.GetUser().UserId);
                }

                AskQuestionDAOModel daoModel = _mapper.Map<QuestionUpdateModel, AskQuestionDAOModel>(updateModel);
                _askQuestionDAO.UpdateQuestion(daoModel);
                QuestionSearchModel questionSearchModel = _mapper.Map<QuestionUpdateModel, QuestionSearchModel>(updateModel);
                await _elasticClient.UpdateAsync<QuestionSearchModel>(questionSearchModel, u => u.Doc(questionSearchModel));
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at UpdateQuestion service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public async void DeleteQuestion(QuestionDeleteModel deleteModel)
        {
            try
            {
                deleteModel.LastUpdateDate = DateTime.Now;
                deleteModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;
                deleteModel.IsActive = false;

                AskQuestionDAOModel daoModel = _mapper.Map<QuestionDeleteModel, AskQuestionDAOModel>(deleteModel);

                _askQuestionDAO.DeleteQuestion(daoModel);
                QuestionDetailModel questionDetailModel = GetQuestionById(deleteModel.Id);
                QuestionSearchModel questionSearchModel = _mapper.Map<QuestionDetailModel, QuestionSearchModel>(questionDetailModel);
                await _elasticClient.DeleteAsync<QuestionSearchModel>(questionSearchModel);
                _logManager.LogWarning("DeleteQuestion User is {UserName}" + deleteModel.LastUpdateUser);

            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at DeleteQuestion service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public List<QuestionDetailModel> GetAllQuestions()
        {
            try
            {
                List<AskQuestionDAOModel> dao = _askQuestionDAO.GetAllQuestions();
                if (dao == null)
                {
                    _logManager.LogWarning("GetAllQuestions: dao is null");
                    return new List<QuestionDetailModel>();
                }
                else
                {
                    List<QuestionDetailModel> question = _mapper.Map<List<AskQuestionDAOModel>, List<QuestionDetailModel>>(dao);
                    return question;
                }
               
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at GetAllQuestions service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }

        }
        public QuestionDetailModel GetQuestionById(int id)
        {
            try
            {
                AskQuestionDAOModel dao = _askQuestionDAO.GetQuestionById(id);
                if (dao == null)
                {
                    _logManager.LogWarning("GetQuestionById: dao is null");
                    return null;
                }

                QuestionDetailModel question = _mapper.Map<AskQuestionDAOModel, QuestionDetailModel>(dao);

                return question;
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at GetQuestionById service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }

        }
        public int QuestionUpdateUpVote(QuestionUpdateModel updateModel)
        {
            try
            {
                AskQuestionDAOModel questionDao = _askQuestionDAO.GetQuestionById(updateModel.Id);
                if (questionDao != null)
                {
                    _logManager.LogWarning("QuestionUpdateUpVote: questionDao is null");
                    updateModel.LastUpdateDate = DateTime.Now;
                    updateModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;
                    updateModel.UpVotes = questionDao.UpVotes + 1;
                }

                AskQuestionDAOModel daoModel = _mapper.Map<QuestionUpdateModel, AskQuestionDAOModel>(updateModel);
                int data = _askQuestionDAO.QuestionUpdateUpVote(daoModel);

                return data;
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at QuestionUpdateUpVote service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public int QuestionUpdateDownVote(QuestionUpdateModel updateModel)
        {
            try
            {
                AskQuestionDAOModel questionDao = _askQuestionDAO.GetQuestionById(updateModel.Id);
                if (questionDao != null)
                {
                    _logManager.LogWarning("QuestionUpdateDownVote: questionDao is null");
                    updateModel.LastUpdateDate = DateTime.Now;
                    updateModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;
                    updateModel.DownVotes = questionDao.DownVotes + 1;
                }

                AskQuestionDAOModel daoModel = _mapper.Map<QuestionUpdateModel, AskQuestionDAOModel>(updateModel);
                int data = _askQuestionDAO.QuestionUpdateDownVote(daoModel);

                return data;
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at QuestionUpdateDownVote service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public void QuestionIsClosedById(QuestionUpdateModel updateModel)
        {
            try
            {
                AskQuestionDAOModel daoModel = _mapper.Map<QuestionUpdateModel, AskQuestionDAOModel>(updateModel);
                _askQuestionDAO.QuestionIsClosedById(daoModel);
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at QuestionIsClosedById service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public List<QuestionDetailModel> GetAllQuestionsForMain(AllQuestionForMainModel questionModel)
        {
            try
            {
                List<AskQuestionDAOModel> dao = _askQuestionDAO.GetAllQuestionsForMain(questionModel);
                if (dao == null)
                {
                    _logManager.LogWarning("GetAllQuestionsForMain: Dao is null");
                    return new List<QuestionDetailModel>();

                }
                else
                {
                    List<QuestionDetailModel> question = _mapper.Map<List<AskQuestionDAOModel>, List<QuestionDetailModel>>(dao);
                    return question;
                }
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at GetAllQuestionsForMain service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public QuestionDetailModel GetQuestionWithAnswers(QuestionDetailModel questionModel)
        {
            try
            {
                AskQuestionDAOModel dao = _askQuestionDAO.GetQuestionWithAnswers(questionModel);
                if (dao == null)
                {
                    _logManager.LogWarning("GetQuestionWithAnswers: Dao is null");
                    return new QuestionDetailModel();
                }
                else
                {
                    QuestionDetailModel question = _mapper.Map<AskQuestionDAOModel, QuestionDetailModel>(dao);
                    return question;
                }
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at GetQuestionWithAnswers service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public List<QuestionDetailModel> GetUnansweredQuestions(QuestionDetailModel questionModel)
        {
            try
            {
                List<AskQuestionDAOModel> dao = _askQuestionDAO.GetUnansweredQuestions(questionModel);
                if (dao == null)
                {
                    _logManager.LogWarning("GetUnansweredQuestions: Dao is null");
                    return new List<QuestionDetailModel>();
                }
                else
                {
                    List<QuestionDetailModel> question = _mapper.Map<List<AskQuestionDAOModel>, List<QuestionDetailModel>>(dao);
                    return question;
                }
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at GetUnansweredQuestions service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public List<QuestionDetailModel> GetMostUpVotedQuestions(QuestionDetailModel questionModel)
        {
            try
            {
                List<AskQuestionDAOModel> dao = _askQuestionDAO.GetMostUpVotedQuestions(questionModel);
                if (dao == null)
                {
                    _logManager.LogWarning("GetMostUpVotedQuestions: Dao is null");
                    return new List<QuestionDetailModel>();

                }
                else
                {
                    List<QuestionDetailModel> question = _mapper.Map<List<AskQuestionDAOModel>, List<QuestionDetailModel>>(dao);
                    return question;
                }
               
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at GetMostUpVotedQuestions service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }

        }
        public List<QuestionDetailModel> GetQuestionsByUserId(QuestionDetailModel questionModel)
        {
            try
            {
                List<AskQuestionDAOModel> dao = _askQuestionDAO.GetQuestionsByUserId(questionModel);
                if (dao == null)
                    return new List<QuestionDetailModel>();

                List<QuestionDetailModel> question = _mapper.Map<List<AskQuestionDAOModel>, List<QuestionDetailModel>>(dao);

                return question;
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at GetQuestionsByUserId service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public QuestionCountModel GetQuestionCountByUserId(int userId)
        {
            try
            {

                QuestionCountModel model = new QuestionCountModel();
                model.Count = _askQuestionDAO.GetQuestionCountByUserId(userId);
                return model;
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at GetQuestionCountByUserId service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }

        }
    }
}
