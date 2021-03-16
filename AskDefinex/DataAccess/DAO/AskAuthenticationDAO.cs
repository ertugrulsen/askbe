using DefineXwork.Library.DataAccess;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AskDefinex.DataAccess.DAO
{
    public class AskAuthenticationDAO : BaseDAO<IDatabaseManager>, IAskAuthenticationDAO
    {
        public AskAuthenticationDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public AskAuthenticationDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }
        public AskLoginDAOModel GetAskLoginUser(string username, string email, string password)
        {

            AskLoginDAOModel user = base.SelectWithTemplate<AskLoginDAOModel>("AskAuthenticationDAO.GetAskLoginUser", new { Username = username, Email = email, Password = password }).FirstOrDefault();

            if (user == null)
                return null;



            return user;

        }

        public void RemoveRefreshToken(string userName)
        {
            base.UpdateWithTemplate("AskAuthenticationDAO.RemoveRefreshToken", new { UserName = userName });
        }

        public void UpdateRefreshToken(string userName, string refreshToken)
        {
            base.UpdateWithTemplate("AskAuthenticationDAO.UpdateRefreshToken", new { RefreshToken = refreshToken, UserName = userName });
        }

        public void SetRefreshToken(string userName, string refreshToken, DateTime refreshtokencreatedate)
        {
            base.UpdateWithTemplate("AskAuthenticationDAO.SetRefreshToken", new { RefreshToken = refreshToken, RefreshTokenCreateDate = refreshtokencreatedate, UserName = userName });
        }

        public AskLoginDAOModel GetLoginUserByRefreshToken(string refreshToken, string userName)
        {

            AskLoginDAOModel user = base.SelectWithTemplate<AskLoginDAOModel>("AskAuthenticationDAO.GetLoginUserByRefreshToken", new { RefreshToken = refreshToken, UserName = userName }).FirstOrDefault();

            if (user == null)
                return null;

            return user;

        }
    }
}
