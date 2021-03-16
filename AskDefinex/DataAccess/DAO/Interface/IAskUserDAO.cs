using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;

namespace AskDefinex.DataAccess.DAO.Interface
{
    public interface IAskUserDAO : IDAO
    {
        public AskUserDAOModel GetUser(string userName);
        public AskUserDAOModel GetUserById(int? id);
        public AskUserDAOModel GetUserByEmail(string email);
        public AskUserDAOModel GetUserWithUserNameAndEmail(string userName, string email);
        public void DeleteUserById(int id);

        public int CreateUser(AskUserDAOModel daoModel);

    }
}
