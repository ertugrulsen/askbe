using AskDefinex.Business.Model.AskAnswerModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Model.Request.AskAnswerModule;
using AskDefinex.Rest.Model.Response;
using AskDefinex.Rest.Model.Response.AskAnswerModule;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AskDefinex.Rest.Controller
{
    [Route("api/[controller]")]
    public class AskAnswerController : BaseController
    {
        private readonly ILogger<AskAnswerController> _logManager;
        private readonly IAskAnswerService _answerService;
        private readonly IMapper _mapper;

        public AskAnswerController(ILogger<AskAnswerController> logManager, IAskAnswerService answerService, IMapper mapper)
        {
            _logManager = logManager;
            _answerService = answerService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("CreateAnswer")]
        [Authorize]
        public IActionResult CreateAnswer([FromBody] AnswerCreateRequestModel request)
        {
            _logManager.LogDebug("CreateAnswer service started with request: {@AnswerCreateRequestModel}", request);

            RestResponseContainer<AnswerCreateResponseModel> response = new RestResponseContainer<AnswerCreateResponseModel>();

            AnswerCreateModel model = _mapper.Map<AnswerCreateRequestModel, AnswerCreateModel>(request);

            int id = _answerService.CreateAnswer(model);
            AnswerCreateResponseModel responseModel = new AnswerCreateResponseModel();
            responseModel.Id = id;
            response.IsSucceed = true;
            response.Response = responseModel;

            _logManager.LogDebug("CreateAnswer service finished with response: {@AnswerCreateResponseModel}", responseModel);

            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateAnswer")]
        [Authorize]
        public IActionResult UpdateAnswer([FromBody] AnswerUpdateRequestModel request)
        {
            _logManager.LogDebug("UpdateAnswer service started with request: {@AnswerUpdateRequestModel}", request);

            RestResponseContainer<AnswerUpdateResponseModel> response = new RestResponseContainer<AnswerUpdateResponseModel>();

            AnswerUpdateModel model = _mapper.Map<AnswerUpdateRequestModel, AnswerUpdateModel>(request);
            _answerService.UpdateAnswer(model);
            response.IsSucceed = true;

            _logManager.LogDebug("CreateAnswer service finished with response: {@AnswerUpdateModel}", model);

            return Ok(response);
        }

        [HttpPut]
        [Route("DeleteAnswer")]
        [Authorize]
        public IActionResult DeleteAnswer([FromBody] AnswerDeleteRequestModel request)
        {
            _logManager.LogDebug("DeleteAnswer service started with request: {@AnswerDeleteRequestModel}", request);

            RestResponseContainer<AnswerDeleteResponseModel> response = new RestResponseContainer<AnswerDeleteResponseModel>();

            AnswerDeleteModel model = _mapper.Map<AnswerDeleteRequestModel, AnswerDeleteModel>(request);
            _answerService.DeleteAnswer(model);

            response.IsSucceed = true;

            _logManager.LogDebug("DeleteAnswer service finished with response: {@AnswerDeleteModel}", model);

            return Ok(response);
        }

        [HttpPost]
        [Route("AnswerUpdateUpVote")]
        [Authorize]
        public IActionResult AnswerUpdateUpVote([FromBody] AnswerUpdateRequestModel request)
        {
            _logManager.LogDebug("AnswerUpdateUpVote service started with request: {@AnswerUpdateRequestModel}", request);

            RestResponseContainer<AnswerUpdateResponseModel> response = new RestResponseContainer<AnswerUpdateResponseModel>();

            AnswerUpdateModel model = _mapper.Map<AnswerUpdateRequestModel, AnswerUpdateModel>(request);
            _answerService.AnswerUpdateUpVote(model);
            response.IsSucceed = true;

            _logManager.LogDebug("AnswerUpdateUpVote service finished with response: {@AnswerUpdateModel}", model);

            return Ok(model.UpVotes);
        }

        [HttpPost]
        [Route("AnswerUpdateDownVote")]
        [Authorize]
        public IActionResult AnswerUpdateDownVote([FromBody] AnswerUpdateRequestModel request)
        {
            _logManager.LogDebug("AnswerUpdateDownVote service started with request: {@AnswerUpdateRequestModel}", request);

            RestResponseContainer<AnswerUpdateResponseModel> response = new RestResponseContainer<AnswerUpdateResponseModel>();

            AnswerUpdateModel model = _mapper.Map<AnswerUpdateRequestModel, AnswerUpdateModel>(request);
            _answerService.AnswerUpdateDownVote(model);
            response.IsSucceed = true;

            _logManager.LogDebug("AnswerUpdateDownVote service finished with response: {@AnswerUpdateModel}", model);

            return Ok(model.DownVotes);
        }

        [HttpGet]
        [Route("GetAnswerById")]
        [AllowAnonymous]
        public IActionResult GetAnswerById([FromQuery] AnswerDetailRequestModel request)
        {
            _logManager.LogDebug("GetAnswerById service started with request: {@AnswerDetailRequestModel}", request);

            RestResponseContainer<AnswerDetailResponseModel> response = new RestResponseContainer<AnswerDetailResponseModel>();

            AnswerDetailModel model = _answerService.GetAnswerById(request.Id);

            if (model != null)
            {
                response.Response = _mapper.Map<AnswerDetailModel, AnswerDetailResponseModel>(model);
            }
            response.IsSucceed = true;

            _logManager.LogDebug("GetAnswerById service finished with response: {@AnswerDetailModel}", model);

            return Ok(response);
        }
        [HttpGet]
        [Route("GetAnswersByQuestionId")]
        [AllowAnonymous]
        public IActionResult GetAnswersByQuestionId(int questionid)
        {
            _logManager.LogDebug("GetAnswersByQuestionId service started with request: {QuestionId}", questionid);

            RestResponseContainer<List<AnswerDetailResponseModel>> response = new RestResponseContainer<List<AnswerDetailResponseModel>>();

            List<AnswerDetailModel> model = _answerService.GetAnswersByQuestionId(questionid);

            if (model != null)
            {
                response.Response = _mapper.Map<List<AnswerDetailModel>, List<AnswerDetailResponseModel>>(model);
            }
            response.IsSucceed = true;

            _logManager.LogDebug("GetAnswersByQuestionId service finished with response: {@AnswerDetailModel}", model);

            return Ok(response);
        }
    }
}
