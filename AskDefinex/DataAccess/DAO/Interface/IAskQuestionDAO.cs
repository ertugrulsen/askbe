using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskQuestionModule;
using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;
using System.Collections.Generic;

namespace AskDefinex.DataAccess.DAO.Interface
{
    public interface IAskQuestionDAO : IDAO
    {
        public int CreateQuestion(AskQuestionDAOModel questionModel);
        public AskQuestionDAOModel GetQuestionByHeader(string header);
        public void UpdateQuestion(AskQuestionDAOModel daoModel);
        public void DeleteQuestion(AskQuestionDAOModel daoModel);
        public List<AskQuestionDAOModel> GetAllQuestions();
        public AskQuestionDAOModel GetQuestionById(int id);
        public int QuestionUpdateUpVote(AskQuestionDAOModel daoModel);
        public int QuestionUpdateDownVote(AskQuestionDAOModel daoModel);
        public void QuestionIsClosedById(AskQuestionDAOModel daoModel);
        public List<AskQuestionDAOModel> GetAllQuestionsForMain(AllQuestionForMainModel daoModel);
        public AskQuestionDAOModel GetQuestionWithAnswers(QuestionDetailModel daoModel);
        public List<AskQuestionDAOModel> GetUnansweredQuestions(QuestionDetailModel questionModel);
        public List<AskQuestionDAOModel> GetMostUpVotedQuestions(QuestionDetailModel questionModel);
        public List<AskQuestionDAOModel> GetQuestionsByUserId(QuestionDetailModel daoModel);
        public int GetQuestionCountByUserId(int userId);

    }
}
