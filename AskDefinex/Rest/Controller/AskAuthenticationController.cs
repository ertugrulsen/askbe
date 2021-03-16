
using AskDefinex.Business.Model;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Common.Const;
using AskDefinex.Rest.Model.Request;
using AskDefinex.Rest.Model.Request.Auth;
using AskDefinex.Rest.Model.Response;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AskDefinex.Rest.Controller
{
    [Route("api/[controller]")]
    public class AskAuthenticationController : BaseController
    {
        private readonly ILogger<AskAuthenticationController> _logManager;
        private readonly IAskAuthenticationService _authenticationService;
        private readonly IAskUserService _askUserService;
        private readonly IRecaptchaValidatorService _recaptchaValidatorService;
        private readonly IMapper _mapper;

        public AskAuthenticationController(ILogger<AskAuthenticationController> logManager, IAskAuthenticationService authenticationService, IMapper mapper, IAskUserService askUserService, IRecaptchaValidatorService recaptchaValidatorService)
        {
            _logManager = logManager;
            _authenticationService = authenticationService;
            _askUserService = askUserService;
            _mapper = mapper;
            _recaptchaValidatorService = recaptchaValidatorService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("AskLogin")]
        public IActionResult Login([FromBody] AskLoginRequestModel loginRequest)
        {
            _logManager.LogDebug("Login service started with request: {@AskLoginRequestModel}", loginRequest);

            RestResponseContainer<AskLoginResponseModel> response = new RestResponseContainer<AskLoginResponseModel>();

            if (!_recaptchaValidatorService.IsRecaptchaValid(loginRequest.RecaptchaToken))
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.RECAPTCHA_FAILED;
                response.ErrorMessage = "Recaptcha Failed";
                _logManager.LogDebug("Login api finished with message : Recaptcha failed");
                return Ok(response);
            }

            AskLoginModel loginUser = _authenticationService.AskLogin(loginRequest.UserName, loginRequest.Email, loginRequest.Password);

            if (loginUser == null)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.LOGIN_FAILED;
                response.ErrorMessage = "Login Failed";
                _logManager.LogDebug("Login api finished with message : Login failed");
            }
            else
            {
                response.IsSucceed = true;
                response.Response = _mapper.Map<AskLoginModel, AskLoginResponseModel>(loginUser);
            }
            _logManager.LogDebug("Login api finished successfully");
            return Ok(response);
        }

        [HttpPost]
        [Route("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            RestResponseContainer<object> response = new RestResponseContainer<object>();

            _authenticationService.LogOut();

            response.IsSucceed = true;

            _logManager.LogDebug("Logout api finished successfully");

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RefreshAccessToken")]
        public IActionResult RefreshAccessToken([FromBody] RefreshTokenRequestModel refreshTokenModel)
        {
            _logManager.LogDebug("RefreshAccessToken api started");

            RestResponseContainer<AskLoginResponseModel> response = new RestResponseContainer<AskLoginResponseModel>();

            AskLoginModel loginUser = _authenticationService.LoginByRefreshToken(refreshTokenModel.RefreshToken, refreshTokenModel.AccessToken);

            if (loginUser == null)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.LOGIN_FAILED;
                response.ErrorMessage = "Login Failed";
                _logManager.LogDebug("Login api finished with message : Login failed");
            }
            else
            {
                response.IsSucceed = true;
                response.Response = _mapper.Map<AskLoginModel, AskLoginResponseModel>(loginUser);
            }
            _logManager.LogDebug("Login api finished successfully");
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("AskRegister")]
        public IActionResult Register([FromBody] AskRegistrationRequestModel request)
        {
            _logManager.LogDebug("Register api started");

            RestResponseContainer<AskLoginResponseModel> response = new RestResponseContainer<AskLoginResponseModel>();
            //check if user exist
            BaseResponseModel checkUser = _askUserService.CheckIfUserExist(request.UserName, request.Email);
            if (checkUser.IsSucceed)
            {
                response.IsSucceed = false;
                response.ErrorCode = checkUser.Code;//user already exist
                response.ErrorMessage = "User already exist.";
                _logManager.LogDebug("Login api finished with message : User already exist");
                return Ok(response);
            }
            AskRegistrationModel registrationModel = _mapper.Map<AskRegistrationRequestModel, AskRegistrationModel>(request);
            AskLoginModel loginUser = _authenticationService.Register(registrationModel);
            response.IsSucceed = true;
            response.Response = _mapper.Map<AskLoginModel, AskLoginResponseModel>(loginUser);

            _logManager.LogDebug("Register api finished successfully");

            return Ok(response);
        }
    }
}
