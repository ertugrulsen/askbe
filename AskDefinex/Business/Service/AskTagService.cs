using System;
using AskDefinex.Business.Model;
using AskDefinex.Business.Service.Interface;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using AutoMapper;
using DefineXwork.Library.Business;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.Security;
using Microsoft.Extensions.Logging;

namespace AskDefinex.Business.Service
{
    /// <summary>
    /// Company Service
    /// Contains all services for communicating company table.
    /// </summary>
    /// <remarks>
    /// <para>This class can create, update, delete companies.</para>
    /// <para>Also can check if a company exits.</para>
    /// </remarks>
    public class AskTagService : BaseService, IAskTagService
    {
        private readonly IAskTagDAO _askTagDAO;
        private readonly IUserContextManager<IUserContextModel> _userContextManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AskTagService> _logManager;


        public AskTagService(ILogger<AskTagService> logManager,IAskTagDAO askTagDAO, IUserContextManager<IUserContextModel> userContextManager, IMapper mapper)
        {
            _logManager = logManager;
            _askTagDAO = askTagDAO;
            _userContextManager = userContextManager;
            _mapper = mapper;
        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            base.AddToTransaction(databaseManager, _askTagDAO);
        }

        public int CreateTag(TagCreateModel TagModel)
        {
            try
            {
                TagModel.CreateDate = DateTime.Now;
                TagModel.CreateUser = _userContextManager.GetUser()?.UserName;

                AskTagDAOModel daoModel = _mapper.Map<TagCreateModel, AskTagDAOModel>(TagModel);
                int newTagId = _askTagDAO.CreateTag(daoModel);

                return newTagId;
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at CreateTag service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public void UpdateTag(TagUpdateModel updateModel)
        {
            try
            {

                updateModel.LastUpdateDate = DateTime.Now;
                updateModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;

                AskTagDAOModel daoModel = _mapper.Map<TagUpdateModel, AskTagDAOModel>(updateModel);
                _askTagDAO.UpdateTag(daoModel);
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at UpdateTag service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
        }
        public void DeleteTag(TagUpdateModel deleteModel)
        {
            try
            {
                deleteModel.LastUpdateDate = DateTime.Now;
                deleteModel.LastUpdateUser = _userContextManager.GetUser()?.UserName;

                AskTagDAOModel daoModel = _mapper.Map<TagUpdateModel, AskTagDAOModel>(deleteModel);
                _askTagDAO.DeleteTag(daoModel);
            }
            catch(Exception e)
            {
                _logManager.LogError("Exception is occurred at DeleteTag service exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;

            }
        }
    }
}
