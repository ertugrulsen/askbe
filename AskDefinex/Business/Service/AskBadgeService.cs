using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskBadgeModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using AutoMapper;
using DefineXwork.Library.Business;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.Security;
using Microsoft.Extensions.Logging;
using System;

namespace AskDefinex.Business.Service
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// <para>This class can create, update, delete companies.</para>
    /// </remarks>
    public class AskBadgeService : BaseService, IAskBadgeService
    {
        private readonly ILogger<AskBadgeService> _logManager;
        private readonly IAskBadgeDAO _askBadgeDao;
        private readonly IUserContextManager<IUserContextModel> _userContextManager;
        private readonly IMapper _mapper;

        public AskBadgeService(ILogger<AskBadgeService> logManager, IAskBadgeDAO askBadgeDao, IUserContextManager<IUserContextModel> userContextManager, IMapper mapper)
        {
            _logManager = logManager;
            _askBadgeDao = askBadgeDao;
            _userContextManager = userContextManager;
            _mapper = mapper;
        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            base.AddToTransaction(databaseManager, _askBadgeDao);
        }
        public BadgeDetailModel GetBadgeByUserId(int userid)
        {
            try
            {
                AskBadgeDAOModel dao = _askBadgeDao.GetBadgeByUserId(userid);
                if (dao == null)
                    return null;
                BadgeDetailModel user = _mapper.Map<AskBadgeDAOModel, BadgeDetailModel>(dao);
                return user;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at GetBadgeByUserId service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public int CreateBadge(BadgeCreateModel badgeModel)
        {
            try
            {
                if (_userContextManager.GetUser() == null)
                {
                    _logManager.LogWarning("User context manager get User is null");
                    throw new ArgumentNullException(_userContextManager.GetUser().ToString());
                }
                badgeModel.CreateDate = DateTime.Now;
                badgeModel.CreateUser = _userContextManager.GetUser()?.UserName;
                badgeModel.UserId = _userContextManager.GetUser().UserId;

                AskBadgeDAOModel daoModel = _mapper.Map<BadgeCreateModel, AskBadgeDAOModel>(badgeModel);
                int newBadgeId = _askBadgeDao.CreateBadge(daoModel);

                return newBadgeId;
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at CreateBadge service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public void UpdateBadge(BadgeUpdateModel updateModel)
        {
            try
            {
                if (_userContextManager.GetUser() == null)
                {
                    _logManager.LogWarning("User context manager get User is null");
                    throw new ArgumentNullException(_userContextManager.GetUser().ToString());
                }
                updateModel.LastUpdateDate = DateTime.Now;
                updateModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;
                updateModel.UserId = _userContextManager.GetUser().UserId;

                AskBadgeDAOModel daoModel = _mapper.Map<BadgeUpdateModel, AskBadgeDAOModel>(updateModel);
                _askBadgeDao.UpdateBadge(daoModel);
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at UpdateBadge service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }

        }
        public void DeleteBadge(BadgeDeleteModel deleteModel)
        {
            try
            {
                deleteModel.LastUpdateDate = DateTime.Now;
                deleteModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;
                deleteModel.IsActive = false;

                AskBadgeDAOModel daoModel = _mapper.Map<BadgeDeleteModel, AskBadgeDAOModel>(deleteModel);
                _askBadgeDao.DeleteBadge(daoModel);
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at DeleteBadge service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }

        }
    }
}
