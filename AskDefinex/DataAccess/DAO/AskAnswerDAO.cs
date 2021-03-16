using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace AskDefinex.DataAccess.DAO
{
    public class AskAnswerDAO : BaseDAO<IDatabaseManager>, IAskAnswerDAO
    {
        public AskAnswerDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public AskAnswerDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }
        public int CreateAnswer(AskAnswerDAOModel daoModel)
        {
            if (null == daoModel)
                return 0;

            int newId = base.InsertWithTemplate("AskAnswerDAO.CreateAnswer", daoModel);

            return newId;
        }
        public void UpdateAnswer(AskAnswerDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskAnswerDAO.UpdateAnswer", daoModel);
        }
        public void DeleteAnswer(AskAnswerDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskAnswerDAO.DeleteAnswer", daoModel);
        }
        public int AnswerUpdateUpVote(AskAnswerDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskAnswerDAO.AnswerUpdateUpVote", daoModel);
            return daoModel.UpVotes;
        }
        public int AnswerUpdateDownVote(AskAnswerDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskAnswerDAO.AnswerUpdateDownVote", daoModel);
            return daoModel.DownVotes;
        }
        public AskAnswerDAOModel GetAnswerById(int id)
        {
            AskAnswerDAOModel model = base.SelectWithTemplate<AskAnswerDAOModel>("AskAnswerDAO.GetAnswerById", new { Id = id }).FirstOrDefault();

            if (model == null)
                return null;

            return model;

        }
        public List<AskAnswerDAOModel> GetAnswersByQuestionId(int questionid)
        {
            List<AskAnswerDAOModel> model = base.SelectWithTemplate<AskAnswerDAOModel>("AskAnswerDAO.GetAnswersByQuestionId", new { QuestionId = questionid }).ToList();
            return model;

        }
    }
}
