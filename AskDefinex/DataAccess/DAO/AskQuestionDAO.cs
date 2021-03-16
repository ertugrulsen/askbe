using DefineXwork.Library.DataAccess;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using System.Linq;
using System.Collections.Generic;
using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskQuestionModule;

namespace AskDefinex.DataAccess.DAO
{
    public class AskQuestionDAO : BaseDAO<IDatabaseManager>, IAskQuestionDAO
    {
        public AskQuestionDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public AskQuestionDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }

        public int CreateQuestion(AskQuestionDAOModel questionModel)
        {
            if (null == questionModel)
                return 0;

            int newId = base.InsertWithTemplate("AskQuestionDAO.CreateQuestion", questionModel);

            return newId;
        }
        public AskQuestionDAOModel GetQuestionByHeader(string header)
        {

            AskQuestionDAOModel model = base.SelectWithTemplate<AskQuestionDAOModel>("AskQuestionDAO.GetQuestionByHeader", new { Header = header }).FirstOrDefault();

            return model;

        }
        public void UpdateQuestion(AskQuestionDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskQuestionDAO.UpdateQuestion", daoModel);
        }
        public void DeleteQuestion(AskQuestionDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskQuestionDAO.DeleteQuestion", daoModel);
        }
        public List<AskQuestionDAOModel> GetAllQuestions()
        {
            List<AskQuestionDAOModel> model = base.SelectWithTemplate<AskQuestionDAOModel>("AskQuestionDAO.GetAllQuestions").ToList();

            return model;
        }
        public AskQuestionDAOModel GetQuestionById(int id)
        {
            AskQuestionDAOModel model = base.SelectWithTemplate<AskQuestionDAOModel>("AskQuestionDAO.GetQuestionById", new { Id = id }).FirstOrDefault();

            return model;
        }
        public int QuestionUpdateUpVote(AskQuestionDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskQuestionDAO.QuestionUpdateUpVote", daoModel);
            return daoModel.UpVotes;
        }
        public int QuestionUpdateDownVote(AskQuestionDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskQuestionDAO.QuestionUpdateDownVote", daoModel);
            return daoModel.DownVotes;
        }
        public void QuestionIsClosedById(AskQuestionDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskQuestionDAO.QuestionIsClosedById", daoModel);
        }
        public List<AskQuestionDAOModel> GetAllQuestionsForMain(AllQuestionForMainModel daoModel)
        {
            var questionCount = base.SelectWithTemplate<int>("AskQuestionDAO.GetAllQuestionsCount").FirstOrDefault();
            List<AskQuestionDAOModel> model = base.SelectWithTemplate<AskQuestionDAOModel>("AskQuestionDAO.GetAllQuestionsForMain", new { Offset = (daoModel.StartOffSet * daoModel.Count) , Limit = daoModel.Count}).ToList();

            for (int i = 0; i < model.Count; i++)
            {
                model[i].TotalCount = questionCount;
                model[i].StartOffSet = daoModel.StartOffSet;
                model[i].Count = daoModel.Count;
            }

            return model;
        }
        public AskQuestionDAOModel GetQuestionWithAnswers(QuestionDetailModel daoModel)
        {
            AskQuestionDAOModel questionByIdModel = base.SelectWithTemplate<AskQuestionDAOModel>("AskQuestionDAO.GetQuestionById", new { Id = daoModel.Id }).FirstOrDefault();

            List<AskAnswerDAOModel> answerModel = base.SelectWithTemplate<AskAnswerDAOModel>("AskAnswerDAO.GetAnswersByQuestionId", new { QuestionId = questionByIdModel.Id }).ToList();

            List<AskCommentDAOModel> commentQuestionModel = base.SelectWithTemplate<AskCommentDAOModel>("AskCommentDAO.GetCommentsByQuestionId", new { QuestionId = questionByIdModel.Id, Type = "1" }).ToList();

            for (int i = 0; i < answerModel.Count; i++)
            {
                List<AskCommentDAOModel> commentAnswerModel = base.SelectWithTemplate<AskCommentDAOModel>("AskCommentDAO.GetCommentsByAnswerId", new { AnswerId = answerModel[i].Id, Type = "2" }).ToList();
                answerModel[i].CommentList = commentAnswerModel;
            }

            questionByIdModel.AnswerList = answerModel;
            questionByIdModel.CommentList = commentQuestionModel;

            return questionByIdModel;
        }
        public List<AskQuestionDAOModel> GetUnansweredQuestions(QuestionDetailModel questionModel)
        {
            var questionCount = base.SelectWithTemplate<int>("AskQuestionDAO.GetUnansweredQuestionsCount").FirstOrDefault();
            List<AskQuestionDAOModel> model = base.SelectWithTemplate<AskQuestionDAOModel>("AskQuestionDAO.GetUnansweredQuestions", new { Offset = (questionModel.StartOffSet * questionModel.Count), Limit = questionModel.Count }).ToList();

            for (int i = 0; i < model.Count; i++)
            {
                model[i].TotalCount = questionCount;
                model[i].StartOffSet = questionModel.StartOffSet;
                model[i].Count = questionModel.Count;
            }

            return model;
        }
        public List<AskQuestionDAOModel> GetMostUpVotedQuestions(QuestionDetailModel questionModel)
        {
            var questionCount = base.SelectWithTemplate<int>("AskQuestionDAO.GetMostUpVotedQuestionsCount").FirstOrDefault();
            List<AskQuestionDAOModel> model = base.SelectWithTemplate<AskQuestionDAOModel>("AskQuestionDAO.GetMostUpVotedQuestions", new { Offset = (questionModel.StartOffSet * questionModel.Count), Limit = questionModel.Count }).ToList();

            for (int i = 0; i < model.Count; i++)
            {
                model[i].TotalCount = questionCount;
                model[i].StartOffSet = questionModel.StartOffSet;
                model[i].Count = questionModel.Count;
            }

            return model;
        }
        public List<AskQuestionDAOModel> GetQuestionsByUserId(QuestionDetailModel daoModel)
        {
            List<AskQuestionDAOModel> model = base.SelectWithTemplate<AskQuestionDAOModel>("AskQuestionDAO.GetQuestionsByUserId", new {UserId = daoModel.UserId, Offset = (daoModel.StartOffSet * daoModel.Count), Limit = daoModel.Count }).ToList();
            return model;
        }
        public int GetQuestionCountByUserId(int userId)
        {
            int count = base.SelectWithTemplate<int>("AskQuestionDAO.GetQuestionCountByUserId", new { UserId = userId }).FirstOrDefault();

            return count;
        }
    }
}
