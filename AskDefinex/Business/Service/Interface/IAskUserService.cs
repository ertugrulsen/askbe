using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskUserModule;
using DefineXwork.Library.Business;

namespace AskDefinex.Business.Service.Interface
{
    public interface IAskUserService : IService
    {
        public AskUserDetailModel GetUser(string userName);
        public AskUserDetailModel GetUserById(int id);
        public AskUserDetailModel GetUserByEmail(string email);
        public int CreateUser(AskUserCreateModel createModel);
        public void DeleteUserById(int id);

        public BaseResponseModel CheckIfUserExist(string userName, string email);
    }
}
