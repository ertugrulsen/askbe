using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;
using System.Collections.Generic;

namespace AskDefinex.DataAccess.DAO.Interface
{
    public interface IAskCommentDAO : IDAO
    {
        public int CreateComment(AskCommentDAOModel commentModel);
        public void UpdateComment(AskCommentDAOModel daoModel);
        public void DeleteComment(AskCommentDAOModel daoModel);
        public void DeleteCommentByQuestionId(AskCommentDAOModel daoModel);
        public List<AskCommentDAOModel> GetCommentsByQuestionId(int questionid);
        public List<AskCommentDAOModel> GetCommentsByAnswerId(int answerid);
    }
}
