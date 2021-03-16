using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskQuestionModule;
using DefineXwork.Library.Business;
using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AskDefinex.Business.Service.Interface
{
    public interface IAskQuestionService : IService
    {
        Task<ISearchResponse<T>> SearchQuestion<T>(string query, int page = 1, int pageSize = 5) where T : QuestionSearchModel;
        public Task<int> CreateQuestion(QuestionCreateModel questionModel);
        public BaseResponseModel CheckIfQuestionExist(string header);
        public void UpdateQuestion(QuestionUpdateModel updateModel);
        public void DeleteQuestion(QuestionDeleteModel deleteModel);
        public List<QuestionDetailModel> GetAllQuestions();
        public QuestionDetailModel GetQuestionById(int id);
        public int QuestionUpdateUpVote(QuestionUpdateModel updateModel);
        public int QuestionUpdateDownVote(QuestionUpdateModel updateModel);
        public void QuestionIsClosedById(QuestionUpdateModel updateModel);
        public List<QuestionDetailModel> GetAllQuestionsForMain(AllQuestionForMainModel questionModel);
        public QuestionDetailModel GetQuestionWithAnswers(QuestionDetailModel questionModel);
        public List<QuestionDetailModel> GetUnansweredQuestions(QuestionDetailModel questionModel);
        public List<QuestionDetailModel> GetMostUpVotedQuestions(QuestionDetailModel questionModel);
        public List<QuestionDetailModel> GetQuestionsByUserId(QuestionDetailModel questionModel);
        public QuestionCountModel GetQuestionCountByUserId(int userId);

    }
}
