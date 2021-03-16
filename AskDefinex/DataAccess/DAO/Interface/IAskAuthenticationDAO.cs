using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;
using System;

namespace AskDefinex.DataAccess.DAO.Interface
{
    public interface IAskAuthenticationDAO : IDAO
    {
        AskLoginDAOModel GetAskLoginUser(string username, string email, string password);

        AskLoginDAOModel GetLoginUserByRefreshToken(string refreshToken, string userName);
        void RemoveRefreshToken(string userName);
        void UpdateRefreshToken(string userName, string refreshToken);
        void SetRefreshToken(string userName, string refreshToken, DateTime refreshtokencreatedate);
    }
}
