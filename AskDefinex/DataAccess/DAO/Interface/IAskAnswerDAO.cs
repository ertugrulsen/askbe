using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;
using System.Collections.Generic;

namespace AskDefinex.DataAccess.DAO.Interface
{
    public interface IAskAnswerDAO : IDAO
    {
        public int CreateAnswer(AskAnswerDAOModel daoModel);
        public void UpdateAnswer(AskAnswerDAOModel daoModel);
        public void DeleteAnswer(AskAnswerDAOModel daoModel);
        public int AnswerUpdateUpVote(AskAnswerDAOModel daoModel);
        public int AnswerUpdateDownVote(AskAnswerDAOModel daoModel);
        public AskAnswerDAOModel GetAnswerById(int id);
        public List<AskAnswerDAOModel> GetAnswersByQuestionId(int questionid);

    }
}
