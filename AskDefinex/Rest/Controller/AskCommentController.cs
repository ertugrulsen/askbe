using AskDefinex.Business.Model.AskCommentModule;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Model.Request.AskCommentModule;
using AskDefinex.Rest.Model.Response;
using AskDefinex.Rest.Model.Response.AskCommentModule;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AskDefinex.Rest.Controller
{
    [Route("api/[controller]")]
    public class AskCommentController : BaseController
    {
        private readonly ILogger<AskCommentController> _logManager;
        private readonly IAskCommentService _commentService;
        private readonly IMapper _mapper;

        public AskCommentController(ILogger<AskCommentController> logManager, IAskCommentService commentService, IMapper mapper)
        {
            _logManager = logManager;
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("CreateComment")]
        [Authorize]
        public IActionResult CreateComment([FromBody] CommentCreateRequestModel request)
        {
            _logManager.LogDebug("CreateComment api started with request: {@CommentCreateRequestModel}", request);

            RestResponseContainer<CommentCreateResponseModel> response = new RestResponseContainer<CommentCreateResponseModel>();

            CommentCreateModel model = _mapper.Map<CommentCreateRequestModel, CommentCreateModel>(request);

            int id = _commentService.CreateComment(model);
            CommentCreateResponseModel responseModel = new CommentCreateResponseModel();
            responseModel.Id = id;
            response.IsSucceed = true;
            response.Response = responseModel;

            _logManager.LogDebug("CreateComment api finished with response: {@CommentCreateResponseModel}", responseModel);

            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateComment")]
        [Authorize]
        public IActionResult UpdateComment([FromBody] CommentUpdateRequestModel request)
        {
            _logManager.LogDebug("UpdateComment api started with request: {@CommentUpdateRequestModel}", request);

            RestResponseContainer<CommentUpdateResponseModel> response = new RestResponseContainer<CommentUpdateResponseModel>();

            CommentUpdateModel model = _mapper.Map<CommentUpdateRequestModel, CommentUpdateModel>(request);
            _commentService.UpdateComment(model);
            response.IsSucceed = true;

            _logManager.LogDebug("UpdateComment api finished with response: {@CommentUpdateModel}", model);

            return Ok(response);
        }

        [HttpPut]
        [Route("DeleteComment")]
        [Authorize]
        public IActionResult DeleteComment([FromBody] CommentDeleteRequestModel request)
        {
            _logManager.LogDebug("DeleteComment api started with request: {@CommentDeleteRequestModel}", request);

            RestResponseContainer<CommentDeleteResponseModel> response = new RestResponseContainer<CommentDeleteResponseModel>();

            CommentDeleteModel model = _mapper.Map<CommentDeleteRequestModel, CommentDeleteModel>(request);
            _commentService.DeleteComment(model);

            response.IsSucceed = true;

            _logManager.LogDebug("DeleteComment api finished with response: {@CommentDeleteModel}", model);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetCommentsByQuestionId")]
        [AllowAnonymous]
        public IActionResult GetCommentsByQuestionId(int questionid)
        {
            _logManager.LogDebug("GetCommentsByQuestionId api started with parameter: {QuestionId}", questionid);

            RestResponseContainer<List<CommentDetailResponseModel>> response = new RestResponseContainer<List<CommentDetailResponseModel>>();

            List<CommentDetailModel> model = _commentService.GetCommentsByQuestionId(questionid);

            if (model != null)
            {
                response.Response = _mapper.Map<List<CommentDetailModel>, List<CommentDetailResponseModel>>(model);
            }
            response.IsSucceed = true;

            _logManager.LogDebug("GetCommentsByQuestionId api finished with response: {@CommentDetailModel}", model);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetCommentsByAnswerId")]
        [AllowAnonymous]
        public IActionResult GetCommentsByAnswerId(int answerid)
        {
            _logManager.LogDebug("GetCommentsByAnswerId api started with parameter: {AnswerId}", answerid);

            RestResponseContainer<List<CommentDetailResponseModel>> response = new RestResponseContainer<List<CommentDetailResponseModel>>();

            List<CommentDetailModel> model = _commentService.GetCommentsByAnswerId(answerid);

            if (model != null)
            {
                response.Response = _mapper.Map<List<CommentDetailModel>, List<CommentDetailResponseModel>>(model);
            }
            response.IsSucceed = true;

            _logManager.LogDebug("GetCommentsByAnswerId api finished with response: {@CommentDetailModel}", model);

            return Ok(response);
        }
        [HttpPut]
        [Route("DeleteCommentByQuestionId")]
        [Authorize]
        public IActionResult DeleteCommentByQuestionId([FromBody] CommentDeleteRequestModel request)
        {
            _logManager.LogDebug("DeleteCommentByQuestionId api started with parameter: {qCommentDeleteRequestModel}", request);

            RestResponseContainer<CommentDeleteResponseModel> response = new RestResponseContainer<CommentDeleteResponseModel>();

            CommentDeleteModel model = _mapper.Map<CommentDeleteRequestModel, CommentDeleteModel>(request);
            _commentService.DeleteCommentByQuestionId(model);

            response.IsSucceed = true;

            _logManager.LogDebug("DeleteCommentByQuestionId api finished with response: {@CommentDeleteModel}", model);

            return Ok(response);
        }
    }
}
