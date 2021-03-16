using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskUserModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Common.Const;
using AskDefinex.Rest.Model.Request;
using AskDefinex.Rest.Model.Request.AskUserModule;
using AskDefinex.Rest.Model.Response;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AskDefinex.Rest.Controller
{
    /// <summary>
    /// User API
    /// Contains all services for performing basic crud functions.
    /// </summary>
    /// <remarks>
    /// <para>This class can create, update, delete users.</para>
    /// <para>Can get user detail and create role for a user.</para>
    /// <para>Also can check if user exits.</para>
    /// </remarks>
    [Route("api/[controller]")]
    public class AskUserController : BaseController
    {
        private readonly ILogger<AskUserController> _logManager;
        private readonly IAskUserService _askUserService;
        private readonly IMapper _mapper;

        public AskUserController(ILogger<AskUserController> logManager, IMapper mapper, IAskUserService askUserService)
        {
            _logManager = logManager;
            _askUserService = askUserService;
            _mapper = mapper;
        }

        /// <summary>
        /// GetUser
        /// Brings detailed user information by given username
        /// </summary>
        /// <value>username</value>
        /// <returns>
        /// <para>Detailed user model (username, isActive, companyId etc)</para>
        /// </returns>
        [HttpPost]
        [Route("AskGetUser")]
        [AllowAnonymous]
        public IActionResult GetUser([FromBody] AskUserDetailRequestModel userDetailRequestModel)
        {
            RestResponseContainer<AskUserDetailResponseModel> response = new RestResponseContainer<AskUserDetailResponseModel>();
            _logManager.LogDebug("GetUser api started with :{@AskUserDetailRequestModel} ", userDetailRequestModel);
            AskUserDetailModel userModel = _askUserService.GetUser(userDetailRequestModel.UserName);

            if (userModel == null)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.USER_NO_DATA_FOUND;
                response.ErrorMessage = "User not found";
                _logManager.LogDebug("GetUser api finished with this message user not found where username: ", userDetailRequestModel.UserName);
            }
            else
            {
                response.IsSucceed = true;
                response.Response = _mapper.Map<AskUserDetailModel, AskUserDetailResponseModel>(userModel);
                _logManager.LogDebug("AskGetUser service is finished success with these parameters: ", userDetailRequestModel.UserName);
            }
            return Ok(response);
        }
        [HttpPost]
        [Route("AskGetUserById")]
        [AllowAnonymous]
        public IActionResult GetUserById([FromBody] AskGetUserByIdRequest askGetUserByIdRequest)
        {
            _logManager.LogDebug("GetUserById api started with parameter: {@AskGetUserByIdRequest}", askGetUserByIdRequest);

            RestResponseContainer<AskUserDetailResponseModel> response = new RestResponseContainer<AskUserDetailResponseModel>();

            AskUserDetailModel userModel = _askUserService.GetUserById(askGetUserByIdRequest.Id);

            if (userModel == null)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.USER_NO_DATA_FOUND;
                response.ErrorMessage = "User not found";
                _logManager.LogDebug("GetUserById api finished with message : User not found");
            }
            else
            {
                response.IsSucceed = true;
                response.Response = _mapper.Map<AskUserDetailModel, AskUserDetailResponseModel>(userModel);
                _logManager.LogDebug("GetUserById api finished with response: {@AskUserDetailModel}", userModel);
            }
            return Ok(response);
        }
        [HttpPost]
        [Route("AskGetUserByEmail")]
        [AllowAnonymous]
        public IActionResult GetUserByEmail([FromBody] AskGetUserByEmailRequest askGetUserByEmailRequest)
        {
            _logManager.LogDebug("GetUserByEmail api started with parameter: {@AskGetUserByEmailRequest}", askGetUserByEmailRequest);

            RestResponseContainer<AskUserDetailResponseModel> response = new RestResponseContainer<AskUserDetailResponseModel>();

            AskUserDetailModel userModel = _askUserService.GetUserByEmail(askGetUserByEmailRequest.Email);

            if (userModel == null)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.USER_NO_DATA_FOUND;
                response.ErrorMessage = "User not found";
                _logManager.LogDebug("GetUserByEmail api finished with message : User not found");
            }
            else
            {
                response.IsSucceed = true;
                response.Response = _mapper.Map<AskUserDetailModel, AskUserDetailResponseModel>(userModel);
                _logManager.LogDebug("GetUserByEmail api finished with response: {@AskUserDetailModel}", userModel);
            }
            return Ok(response);
        }
        /// <summary>
        /// CreateUser
        /// Creates a user record given user information detail
        /// </summary>
        /// <value>User creation request (username, isActive, companyId etc)</value>
        /// <returns>
        /// <para>user id</para>
        /// </returns>
        [HttpPost]
        [Route("CreateUser")]
        [Authorize]
        public IActionResult CreateUser([FromBody] AskUserCreateRequestModel request)
        {
            _logManager.LogDebug("CreateUser api started with parameter: {@AskUserCreateRequestModel}", request);

            RestResponseContainer<AskUserCreateResponseModel> response = new RestResponseContainer<AskUserCreateResponseModel>();

            AskUserCreateModel userModel = _mapper.Map<AskUserCreateRequestModel, AskUserCreateModel>(request);

            BaseResponseModel checkUser = _askUserService.CheckIfUserExist(request.UserName, request.Email);
            if (checkUser.IsSucceed)
            {
                response.IsSucceed = false;
                response.ErrorCode = checkUser.Code;//user already exist
                response.ErrorMessage = "User already exist";//user already exist
                _logManager.LogDebug("GetUserByEmail api finished with message : User already exist");
                return Ok(response);
            }

            int id = _askUserService.CreateUser(userModel);
            AskUserCreateResponseModel responseModel = new AskUserCreateResponseModel();
            responseModel.Id = id;
            response.IsSucceed = true;
            response.Response = responseModel;

            _logManager.LogDebug("CreateUser api finished with response: {@AskUserCreateResponseModel}", responseModel);

            return Ok(response);
        }

        /// <summary>
        /// UpdateUser
        /// Updates an existing record of user table given update information along with user id
        /// </summary>
        /// <value>User update model (username, isActive, companyId etc)</value>
        /// <returns>void</returns>


        /// <summary>
        /// DeleteUser
        /// Soft deletes an existing record of user table
        /// </summary>
        /// <value>delete request containing Id and delete flag</value>
        /// <returns>void</returns>
        [HttpPost]
        [Route("DeleteUser")]
        [Authorize]
        public IActionResult DeleteUserById([FromBody] AskDeleteUserByIdRequest request)
        {
            _logManager.LogDebug("DeleteUserById api started with parameter: {@AskDeleteUserByIdRequest}", request);

            RestResponseContainer<EmptyResponseModel> response = new RestResponseContainer<EmptyResponseModel>();

            _askUserService.DeleteUserById(request.Id);

            response.IsSucceed = true;

            _logManager.LogDebug("DeleteUserById api finished successfully");

            return Ok(response);
        }
    }
}
