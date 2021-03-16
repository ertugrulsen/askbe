using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskUserModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Common.Const;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using AutoMapper;
using DefineXwork.Library.Business;
using DefineXwork.Library.Configuration;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.Security.Common;
using Microsoft.Extensions.Logging;
using System;

namespace AskDefinex.Business.Service
{
    /// <summary>
    /// User Service
    /// Contains all services for communicating user table.
    /// </summary>
    /// <remarks>
    /// <para>This class can create, update, delete users.</para>
    /// <para>Can get user detail and create role for a user.</para>
    /// <para>Also can check if user exits.</para>
    /// </remarks>
    public class AskUserService : BaseService, IAskUserService
    {
        private readonly ILogger<AskUserService> _logManager;
        private readonly IAskUserDAO _userDao;
        private readonly IConfigManager _configManager;
        private readonly IMapper _mapper;

        public AskUserService(ILogger<AskUserService> logManager, IAskUserDAO userDao, IConfigManager configManager, IMapper mapper)
        {
            _logManager = logManager;
            _userDao = userDao;
            _configManager = configManager;
            _mapper = mapper;
        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            base.AddToTransaction(databaseManager, _userDao);
        }

        /// <summary>
        /// GetUser
        /// Brings detailed user information by given username
        /// </summary>
        /// <value>username</value>
        /// <returns>
        /// <para>Detailed user model (username, isActive, companyId etc)</para>
        /// </returns>
        public AskUserDetailModel GetUser(string userName)
        {
            try
            {
                AskUserDAOModel userDao = _userDao.GetUser(userName);

                if (userDao == null)
                    return null;

                AskUserDetailModel user = _mapper.Map<AskUserDAOModel, AskUserDetailModel>(userDao);
                return user;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at GetUser service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public AskUserDetailModel GetUserById(int id)
        {
            try
            {
                AskUserDAOModel userDao = _userDao.GetUserById(id);

                if (userDao == null)
                    return null;

                AskUserDetailModel user = _mapper.Map<AskUserDAOModel, AskUserDetailModel>(userDao);
                return user;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at GetUserById service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public AskUserDetailModel GetUserByEmail(string email)
        {
            try
            {
                AskUserDAOModel userDao = _userDao.GetUserByEmail(email);

                if (userDao == null)
                    return null;

                AskUserDetailModel user = _mapper.Map<AskUserDAOModel, AskUserDetailModel>(userDao);
                return user;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at GetUserByEmail service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }

        }
        /// <summary>
        /// CreateUser
        /// Insert a new user to database
        /// </summary>
        /// <value>User creation model (username, isActive, companyId etc)</value>
        /// <returns>
        /// <para>user id</para>
        /// </returns>
        public int CreateUser(AskUserCreateModel createModel)
        {
            try
            {
                if (String.IsNullOrEmpty(createModel.Password))
                {
                    string hashPassword = HashingHelper.SHA256Hash(_configManager.GetConfig("AppSettings:PasswordHashKey"), createModel.Password);
                    createModel.Password = hashPassword;
                }
                createModel.CreateDate = DateTime.Now;

                AskUserDAOModel userDaoModel = _mapper.Map<AskUserCreateModel, AskUserDAOModel>(createModel);
                int newUserId = _userDao.CreateUser(userDaoModel);

                return newUserId;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at CreateUser service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// UpdateUser
        /// Updates a record of user table
        /// </summary>
        /// <value>User update model (username, isActive, companyId etc)</value>
        /// <returns>void</returns>

        /// <summary>
        /// DeleteUser
        /// Soft deletes a record of user table
        /// </summary>
        /// <value>Id and delete flag</value>
        /// <returns>void</returns>
        public void DeleteUserById(int id)
        {
            try
            {
                _userDao.DeleteUserById(id);
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at DeleteUserById service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        /// <summary>
        /// ListUsersByCompanyId
        /// Retrieve users by company id
        /// </summary>
        /// <value>companyId</value>
        /// <returns>
        /// <para>List of users</para>
        /// </returns>

        /// <summary>
        /// CheckIfUserExist
        /// Controls that if there is an active user in platform or not
        /// </summary>
        /// <value>username</value>
        /// <returns>
        /// <para>MessageCode: USER_NO_DATA_FOUND</para>
        /// <para>MessageCode: USER_ALREADY_EXIST along with user information</para>
        /// </returns>
        public BaseResponseModel CheckIfUserExist(string userName, string email)
        {
            try
            {
                BaseResponseModel response = new BaseResponseModel();
                AskUserDAOModel userDao = _userDao.GetUserWithUserNameAndEmail(userName, email);
                if (userDao == null)
                {
                    response.IsSucceed = false;
                    response.Code = MessageCodes.USER_NO_DATA_FOUND;
                    return response;
                }

                AskUserDetailModel user = _mapper.Map<AskUserDAOModel, AskUserDetailModel>(userDao);
                response.IsSucceed = true;
                response.Code = MessageCodes.USER_ALREADY_EXIST;
                response.Data = user;
                return response;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at CheckIfUserExist service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
    }
}
