using AutoMapper;
using DefineXwork.Library.Business;
using DefineXwork.Library.Configuration;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.Security;
using DefineXwork.Library.Security.Common;
using DefineXwork.Library.Security.Jwt;
using AskDefinex.Business.Model;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Common.Extension;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using DefineXwork.Library.DataAccess.Manager;
using Microsoft.Extensions.Logging;

namespace AskDefinex.Business.Service
{
    /// <summary>
    /// Authentication Service
    /// Contains all services for authenticating users.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class AskAuthenticationService : BaseService, IAskAuthenticationService
    {
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly IConfigManager _configManager;
        private readonly IJwtOptions _jwtOptions;
        private readonly IUserContextManager<IUserContextModel> _userContextManager;
        private readonly IAskAuthenticationDAO _authenticationDao;
        private readonly IAskUserService _askUserService;
        private readonly IMapper _mapper;
        private readonly ILogger<AskAuthenticationService> _logManager;

        public AskAuthenticationService(IJwtTokenHandler jwtTokenHandler, IUserContextManager<IUserContextModel> userContextManager, IAskAuthenticationDAO authenticationDao, IMapper mapper, IConfigManager configManager, ILogger<AskAuthenticationService> logManager, IAskUserService askUserService)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _userContextManager = userContextManager;
            _authenticationDao = authenticationDao;
            _askUserService = askUserService;
            _mapper = mapper;
            _configManager = configManager;
            _logManager = logManager;

            _jwtOptions = new JwtOptions()
            {
                Audience = _configManager.GetConfig("JwtOptions:Audience"),
                Issuer = _configManager.GetConfig("JwtOptions:Issuer"),
                TokenExpiration = _configManager.GetConfig("JwtOptions:TokenExpiration").ToInt(),
                SecurityKey = _configManager.GetConfig("JwtOptions:SecurityKey"),
                RefreshTokenExpiration = _configManager.GetConfig("JwtOptions:RefreshTokenExpiration").ToInt()
            };
        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            base.AddToTransaction(databaseManager, _authenticationDao);
        }

        /// <summary>
        /// Login
        /// Gives access token to users
        /// </summary>
        /// <value>username, password</value>
        /// <returns>
        /// </returns>
        public AskLoginModel AskLogin(string username, string email, string password)
        {
            try
            {
                string hashPassword = HashingHelper.SHA256Hash(_configManager.GetConfig("AppSettings:PasswordHashKey"), password);

                AskLoginDAOModel loginUser = _authenticationDao.GetAskLoginUser(username, email, hashPassword);

                if (loginUser == null)
                {
                    _logManager.LogDebug("AskLogin service user {username} not found", username);
                    return null;
                }

                AskLoginModel login = _mapper.Map<AskLoginDAOModel, AskLoginModel>(loginUser);
                login.AccessToken = _jwtTokenHandler.GenerateAccessToken(login.UserName, login.Roles, _jwtOptions);
                login.RefreshToken = _jwtTokenHandler.GenerateRefreshToken();

                _authenticationDao.SetRefreshToken(login.UserName, login.RefreshToken, DateTime.Now);

                return login;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at AskLogin service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
            
        }

        /// <summary>
        /// LoginByRefreshToken
        /// Refresh access token of users
        /// </summary>
        /// <value>refreshToken, accessToken</value>
        /// <returns>
        /// </returns>
        public AskLoginModel LoginByRefreshToken(string refreshToken, string accessToken)
        {
            try
            {
                JwtSecurityToken decryptedAccessToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                string userName = decryptedAccessToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

                AskLoginDAOModel loginUser = _authenticationDao.GetLoginUserByRefreshToken(refreshToken, userName);

                if (loginUser != null && loginUser.RefreshTokenCreateDate != null && loginUser.RefreshTokenCreateDate.Value.AddMinutes(_jwtOptions.RefreshTokenExpiration) < DateTime.Now)
                {
                    return null;
                }

                AskLoginModel login = _mapper.Map<AskLoginDAOModel, AskLoginModel>(loginUser);
                login.AccessToken = _jwtTokenHandler.GenerateAccessToken(login.UserName, login.Roles, _jwtOptions);
                login.RefreshToken = _jwtTokenHandler.GenerateRefreshToken();

                _authenticationDao.UpdateRefreshToken(login.UserName, login.RefreshToken);

                return login;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at LoginByRefreshToken service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }


        /// <summary>
        /// LogOut
        /// Removes access token of user logon
        /// </summary>
        public void LogOut()
        {
            try
            {
                string logoutUser = _userContextManager.GetUser()?.UserName;
                _authenticationDao.RemoveRefreshToken(_userContextManager.GetUser()?.UserName);
                _userContextManager.DeleteUser();

                _logManager.LogDebug("Log Out. User: {LogOutUser}", logoutUser);
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at LogOut service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
            
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <value>RegistrationModel</value>
        /// <returns>
        /// </returns>
        public AskLoginModel Register(AskRegistrationModel model)
        {
            try
            {
                string hashPassword = HashingHelper.SHA256Hash(_configManager.GetConfig("AppSettings:PasswordHashKey"), model.Password);

                using (IDatabaseManager db = new MysqlDatabaseManager(_configManager))
                {
                    try
                    {
                        db.AddToTransaction(_askUserService);
                        db.BeginTransaction();
                        //user creation
                        AskUserCreateModel userCreateModel = new AskUserCreateModel();
                        userCreateModel.Name = model.Name;
                        userCreateModel.UserName = model.UserName;
                        userCreateModel.Surname = model.Surname;
                        userCreateModel.Password = hashPassword;
                        userCreateModel.IsActive = 1;
                        userCreateModel.Email = model.Email;
                        userCreateModel.CreateDate = DateTime.Now;
                        userCreateModel.CreateUser = "";
                        userCreateModel.LastUpdateDate = DateTime.Now;
                        userCreateModel.LastUpdateUser = "";
                        userCreateModel.Id = _askUserService.CreateUser(userCreateModel);

                        db.CommitTransaction();
                    }
                    catch (Exception)
                    {
                        db.RollbackTransaction();
                        throw;
                    }
                }

                AskLoginDAOModel loginUser = _authenticationDao.GetAskLoginUser(model.UserName, model.Email, hashPassword);

                if (loginUser == null)
                {
                    _logManager.LogWarning("Registration Failed. User:" + model.Email);
                    return null;
                }

                AskLoginModel login = _mapper.Map<AskLoginDAOModel, AskLoginModel>(loginUser);
                login.AccessToken = _jwtTokenHandler.GenerateAccessToken(login.UserName, login.Roles, _jwtOptions);
                login.RefreshToken = _jwtTokenHandler.GenerateRefreshToken();

                _authenticationDao.SetRefreshToken(login.UserName, login.RefreshToken, DateTime.Now);
                return login;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at Register service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
    }
}
