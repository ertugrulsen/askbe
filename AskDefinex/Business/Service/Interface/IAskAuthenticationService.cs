using AskDefinex.Business.Model;
using DefineXwork.Library.Business;

namespace AskDefinex.Business.Service.Interface
{
    public interface IAskAuthenticationService : IService
    {
        public AskLoginModel AskLogin(string username, string email, string password);

        public AskLoginModel LoginByRefreshToken(string refreshToken, string accessToken);
        public void LogOut();
        public AskLoginModel Register(AskRegistrationModel model);


    }
}
