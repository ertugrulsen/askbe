using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskBadgeModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Model.Request;
using AskDefinex.Rest.Model.Request.AskBadgeModule;
using AskDefinex.Rest.Model.Response;
using AskDefinex.Rest.Model.Response.AskBadgeModule;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AskDefinex.Rest.Controller
{
    [Route("api/[controller]")]
    public class AskBadgeController : BaseController
    {
        private readonly ILogger<AskBadgeController> _logManager;
        private readonly IAskBadgeService _badgeService;
        private readonly IMapper _mapper;

        public AskBadgeController(ILogger<AskBadgeController> logManager, IAskBadgeService askBadgeService, IMapper mapper)
        {
            _logManager = logManager;
            _badgeService = askBadgeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetBadgeByUserId")]
        [AllowAnonymous]
        public IActionResult GetBadgeByUserId([FromQuery] BadgeDetailRequestModel request)
        {
            _logManager.LogDebug("DeleteTag api started with parameter: {@TagDeleteRequestModel}", request);

            RestResponseContainer<BadgeDetailResponseModel> response = new RestResponseContainer<BadgeDetailResponseModel>();

            BadgeDetailModel model = _badgeService.GetBadgeByUserId(request.UserId);

            if (model != null)
            {
                response.Response = _mapper.Map<BadgeDetailModel, BadgeDetailResponseModel>(model);
            }
            response.IsSucceed = true;

            _logManager.LogDebug("GetBadgeByUserId api finished successfully for {UserId} user", request.UserId);

            return Ok(response);

        }

        [HttpPost]
        [Route("CreateBadge")]
        [Authorize]
        public IActionResult CreateBadge([FromBody] BadgeCreateRequestModel request)
        {
            _logManager.LogDebug("CreateBadge api started with parameter: {@BadgeCreateRequestModel}", request);

            RestResponseContainer<BadgeCreateResponseModel> response = new RestResponseContainer<BadgeCreateResponseModel>();

            BadgeCreateModel model = _mapper.Map<BadgeCreateRequestModel, BadgeCreateModel>(request);

            int id = _badgeService.CreateBadge(model);
            BadgeCreateResponseModel responseModel = new BadgeCreateResponseModel();
            responseModel.Id = id;
            response.IsSucceed = true;
            response.Response = responseModel;

            _logManager.LogDebug("CreateBadge api finished with {@BadgeCreateResponseModel} ", responseModel);

            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateBadge")]
        [Authorize]
        public IActionResult UpdateBadge([FromBody] BadgeUpdateRequestModel request)
        {
            _logManager.LogDebug("UpdateBadge api started with parameter: {@BadgeUpdateRequestModel}", request);

            RestResponseContainer<BadgeUpdateResponseModel> response = new RestResponseContainer<BadgeUpdateResponseModel>();

            BadgeUpdateModel model = _mapper.Map<BadgeUpdateRequestModel, BadgeUpdateModel>(request);
            _badgeService.UpdateBadge(model);
            response.IsSucceed = true;

            _logManager.LogDebug("UpdateBadge api finished successfully");

            return Ok(response);
        }

        [HttpPut]
        [Route("DeleteBadge")]
        [Authorize]
        public IActionResult DeleteBadge([FromBody] BadgeDeleteRequestModel request)
        {
            _logManager.LogDebug("DeleteBadge api started with parameter: {@BadgeDeleteRequestModel}", request);

            RestResponseContainer<BadgeDeleteResponseModel> response = new RestResponseContainer<BadgeDeleteResponseModel>();

            BadgeDeleteModel model = _mapper.Map<BadgeDeleteRequestModel, BadgeDeleteModel>(request);
            _badgeService.DeleteBadge(model);

            response.IsSucceed = true;

            _logManager.LogDebug("DeleteBadge api finished successfully");

            return Ok(response);
        }
    }
}
