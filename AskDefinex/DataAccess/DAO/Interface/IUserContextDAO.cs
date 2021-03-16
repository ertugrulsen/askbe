using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;

namespace AskDefinex.DataAccess.DAO.Interface
{
    public interface IUserContextDAO : IDAO
    {
        public UserContextUserDAOModel GetUser(string userName);
    }
}
