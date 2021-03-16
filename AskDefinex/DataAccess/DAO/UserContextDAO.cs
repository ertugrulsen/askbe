using DefineXwork.Library.DataAccess;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AskDefinex.DataAccess.DAO
{
    public class UserContextDAO : BaseDAO<IDatabaseManager>, IUserContextDAO
    {

        // Sadece frameworkten gelen JWT authenticatin sonrası user bilgisini almak için kullanılır

        public UserContextDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public UserContextDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }

        public UserContextUserDAOModel GetUser(string userName)
        {

            if (string.IsNullOrEmpty(userName))
                return null;

            UserContextUserDAOModel user = base.SelectWithTemplate<UserContextUserDAOModel>("UserContextDAO.GetUser", new { UserName = userName }).FirstOrDefault();

            if (user == null)
                return null;

            List<UserContextUserRoleModel> userRoles = base.SelectWithTemplate<UserContextUserRoleModel>("UserContextDAO.GetUserRoles", new { UserId = user.Id }).ToList();

            if (userRoles.Count > 0)
                user.Roles = userRoles.Select(x => x.Role).ToList();

            return user;

        }
    }
}
