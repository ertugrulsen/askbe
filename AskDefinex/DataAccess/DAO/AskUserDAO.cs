using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;
using System.Linq;

namespace AskDefinex.DataAccess.DAO
{
    public class AskUserDAO : BaseDAO<IDatabaseManager>, IAskUserDAO
    {
        public AskUserDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public AskUserDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }
        // Sadece frameworkten gelen JWT authenticatin sonrası user bilgisini almak için kullanılır   
        public AskUserDAOModel GetUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;

            AskUserDAOModel user = base.SelectWithTemplate<AskUserDAOModel>("AskUserDAO.GetUser", new { UserName = userName }).FirstOrDefault();

            if (user == null)
                return null;

            return user;

        }
        public AskUserDAOModel GetUserById(int? id)
        {
            if (id == null)
                return null;

            AskUserDAOModel user = base.SelectWithTemplate<AskUserDAOModel>("AskUserDAO.GetUserById", new { Id = id }).FirstOrDefault();

            if (user == null)
                return null;

            return user;

        }
        public AskUserDAOModel GetUserByEmail(string email)
        {
            if (email == null)
                return null;

            AskUserDAOModel user = base.SelectWithTemplate<AskUserDAOModel>("AskUserDAO.GetUserByEmail", new { Email = email }).FirstOrDefault();

            if (user == null)
                return null;

            return user;

        }
        public AskUserDAOModel GetUserWithUserNameAndEmail(string userName, string email)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email))
                return null;
            object param = new { UserName = userName, Email = email };
            AskUserDAOModel user = base.SelectWithTemplate<AskUserDAOModel>("AskUserDAO.GetUserWithUserNameAndEmail", param).FirstOrDefault();

            if (user == null)
                return null;

            return user;

        }
        public void DeleteUserById(int id)
        {
            base.UpdateWithTemplate("AskUserDAO.DeleteUserById", new { Id = id });
        }
        public int CreateUser(AskUserDAOModel daoModel)
        {
            if (null == daoModel)
                return 0;

            int newUserId = base.InsertWithTemplate("AskUserDAO.CreateUser", daoModel);

            return newUserId;

        }

    }
}
