using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace AskDefinex.DataAccess.DAO
{
    public class AskCommentDAO : BaseDAO<IDatabaseManager>, IAskCommentDAO
    {
        public AskCommentDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public AskCommentDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }
        public int CreateComment(AskCommentDAOModel commentModel)
        {
            if (null == commentModel)
                return 0;

            int newId = base.InsertWithTemplate("AskCommentDAO.CreateComment", commentModel);

            return newId;
        }
        public void UpdateComment(AskCommentDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskCommentDAO.UpdateComment", daoModel);
        }
        public void DeleteComment(AskCommentDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskCommentDAO.DeleteComment", daoModel);
        }
        public void DeleteCommentByQuestionId(AskCommentDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskCommentDAO.DeleteCommentByQuestionId", daoModel);
        }
        public List<AskCommentDAOModel> GetCommentsByQuestionId(int questionid)
        {
            List<AskCommentDAOModel> model = base.SelectWithTemplate<AskCommentDAOModel>("AskCommentDAO.GetCommentsByQuestionId", new { QuestionId = questionid, Type = "1" }).ToList();

            return model;

        }
        public List<AskCommentDAOModel> GetCommentsByAnswerId(int answerid)
        {
            List<AskCommentDAOModel> model = base.SelectWithTemplate<AskCommentDAOModel>("AskCommentDAO.GetCommentsByAnswerId", new { AnswerId = answerid, Type = "2" }).ToList();
            return model;

        }
    }
}
