
using AskDefinex.Business.Model;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Model.Response;
using AskDefinex.Rest.Model.Response.AskQuestionModule;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AskDefinex.Rest.Controller
{
    [Route("api/[controller]")]
    public class AskTagController : BaseController
    {
        private readonly ILogger<AskTagController> _logManager;
        private readonly IAskTagService _tagService;
        private readonly IMapper _mapper;

        public AskTagController(ILogger<AskTagController> logManager, IAskTagService tagService, IMapper mapper)
        {
            _logManager = logManager;
            _tagService = tagService;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("CreateTag")]
        [Authorize]
        public IActionResult CreateTag([FromBody] TagCreateRequestModel request)
        {
            _logManager.LogDebug("CreateTag api started with parameter: {@TagCreateRequestModel}", request);

            RestResponseContainer<TagCreateResponseModel> response = new RestResponseContainer<TagCreateResponseModel>();

            TagCreateModel model = _mapper.Map<TagCreateRequestModel, TagCreateModel>(request);


            int id = _tagService.CreateTag(model);
            TagCreateResponseModel responseModel = new TagCreateResponseModel();
            responseModel.Id = id;
            response.IsSucceed = true;
            response.Response = responseModel;

            _logManager.LogDebug("CreateTag api finished with response: {@TagCreateResponseModel}", responseModel);

            return Ok(response);
        }
        [HttpPost]
        [Route("UpdateTag")]
        [Authorize]
        public IActionResult UpdateTag([FromBody] TagUpdateRequestModel request)
        {
            _logManager.LogDebug("UpdateTag api started with parameter: {@TagUpdateRequestModel}", request);

            RestResponseContainer<TagUpdateResponseModel> response = new RestResponseContainer<TagUpdateResponseModel>();

            TagUpdateModel model = _mapper.Map<TagUpdateRequestModel, TagUpdateModel>(request);
            _tagService.UpdateTag(model);
            response.IsSucceed = true;

            _logManager.LogDebug("UpdateTag api finished successfully");

            return Ok(response);
        }
        [HttpPut]
        [Route("DeleteTag")]
        [Authorize]
        public IActionResult DeleteTag([FromBody] TagDeleteRequestModel request)
        {
            _logManager.LogDebug("DeleteTag api started with parameter: {@TagDeleteRequestModel}", request);

            RestResponseContainer<TagDeleteResponseModel> response = new RestResponseContainer<TagDeleteResponseModel>();

            TagUpdateModel model = _mapper.Map<TagDeleteRequestModel, TagUpdateModel>(request);
            _tagService.DeleteTag(model);

            response.IsSucceed = true;

            _logManager.LogDebug("DeleteTag api finished successfully");

            return Ok(response);
        }
    }
}
