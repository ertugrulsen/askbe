using AskDefinex.Business.Model;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using AutoMapper;
using DefineXwork.Library.Business;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.Security.Common;
using Microsoft.Extensions.Logging;
using System;

namespace AskDefinex.Business.Service
{
    public class UserContextService : BaseService, IUserContextService
    {
        private readonly ILogger<UserContextService> _logManager;
        private readonly IUserContextDAO _userContextDao;
        private readonly IMapper _mapper;

        // Sadece frameworkten gelen JWT authenticatin sonrası user bilgisini almak için kullanılır
        public UserContextService(ILogger<UserContextService> logManager, IUserContextDAO userContextDao, IMapper mapper)
        {
            _logManager = logManager;
            _userContextDao = userContextDao;
            _mapper = mapper;
        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            AddToTransaction(databaseManager, _userContextDao);
        }
        public UserContextModel GetUser(string userName)
        {
            try
            {
                UserContextUserDAOModel userDao = _userContextDao.GetUser(userName);
                if (userDao == null)
                    return null;

                UserContextModel user = _mapper.Map<UserContextUserDAOModel, UserContextModel>(userDao);

                user.SetUserData<UserContextUserDetailModel>(_mapper.Map<UserContextUserDAOModel, UserContextUserDetailModel>(userDao));

                return user;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at GetUser service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
    }
}
